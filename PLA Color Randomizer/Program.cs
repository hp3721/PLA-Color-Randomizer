using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Collections.Generic;
using pkNX.Containers;
using pkNX.Structures.FlatBuffers;

namespace PLA_Color_Randomizer
{
    internal class Program
    {
        static readonly Random rand = new Random();
        static readonly ContainerHandler ProgressTracker = new();
        static readonly CancellationTokenSource TokenSource = new();
        static List<Task> tasks = new List<Task>();
        static string outDirPath;

        static void Main(string[] args)
        {
            var inputDirArgument = new Argument<DirectoryInfo>(
                name: "Pokémon Model Directory",
                description: "Path to the /bin/archive/pokemon/ directory in PLA's RomFS"
            ).ExistingOnly();

            var randomizeModeOption = new Option<RandomizeMode>(
                new[] { "--randomize-mode", "-m" },
                getDefaultValue: () => RandomizeMode.Both,
                description: "Mode for selecting whether to randomize normal and/or shiny Pokémon colors"
            );

            RootCommand rootCommand = new RootCommand
            {
                inputDirArgument,
                randomizeModeOption
            };
            
            rootCommand.Description = "Randomizes the material colors for PLA Pokémon";

            rootCommand.SetHandler((DirectoryInfo dirPath, RandomizeMode randomizeMode) =>
            {
                GetPacks(dirPath.FullName, randomizeMode);
            }, inputDirArgument, randomizeModeOption);

            var parser = new CommandLineBuilder(rootCommand)
                .UseHelp()
                .UseEnvironmentVariableDirective()
                .UseParseDirective()
                .UseSuggestDirective()
                .RegisterWithDotnetSuggest()
                .UseTypoCorrections()
                .UseParseErrorReporting()
                .UseExceptionHandler()
                .CancelOnProcessTermination()
                .Build();

            parser.Invoke(args);
        }

        static void GetPacks(string dirPath, RandomizeMode randomizeMode)
        {
            outDirPath = Directory.CreateDirectory(Path.Combine(dirPath, "modified_pokemon")).FullName;

            var paks = Directory.EnumerateFiles(dirPath, "*.gfpak").Where(z => Path.GetFileName(z).StartsWith("pm"));
            try
            {
                foreach (var f in paks)
                {
                    var pak = new GFPack(f);
                    RandomizeBaseLayerColors(ref pak, randomizeMode);
                }
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("Missing required dependency: oo2coore_8_win64.dll");
                Console.ResetColor();
                return;
            }

            if (tasks.Count > 0 )
            {
                Console.WriteLine("Waiting for all GFPAK files to be exported");
                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"Saved modified GFPAK files to {outDirPath}");
            }
        }

        static void SaveGFPAK(ref GFPack pak)
        {
            Task task = pak.SaveAs(Path.Combine(outDirPath, $"{Path.GetFileNameWithoutExtension(pak.FilePath)}.gfpak"), ProgressTracker, TokenSource.Token);
            task.Start();
            tasks.Add(task);
        }

        static void RandomizeBaseLayerColors(ref GFPack pak, RandomizeMode randomizeMode)
        {
            switch (randomizeMode)
            {
                case RandomizeMode.NormalOnly:
                    RandomizeBaseLayerColors(ref pak, false);
                    break;
                case RandomizeMode.ShinyOnly:
                    RandomizeBaseLayerColors(ref pak, true);
                    break;
                case RandomizeMode.Both:
                default:
                    RandomizeBaseLayerColors(ref pak, false);
                    RandomizeBaseLayerColors(ref pak, true);
                    break;
            }
            SaveGFPAK(ref pak);
        }

        static void RandomizeBaseLayerColors(ref GFPack pak, bool randShiny)
        {
            string trmtrFileName = $"{Path.GetFileNameWithoutExtension(pak.FilePath)}{(randShiny ? "_rare" : "")}.trmtr";
            try
            {
                byte[] trmtrFile = pak.GetDataFileName(trmtrFileName);
                trmtr8a trmtrFlatbuffer = FlatBufferConverter.DeserializeFrom<trmtr8a>(trmtrFile);
                RandomizeBaseLayerColors(ref trmtrFlatbuffer);
                pak.SetDataFileName(trmtrFileName, trmtrFlatbuffer.Write());
                Console.WriteLine($"Randomized {trmtrFileName}");
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        static void RandomizeBaseLayerColors(ref trmtr8a trmtrFlatbuffer)
        {
            foreach (var material in trmtrFlatbuffer.Materials)
            {
                foreach (var colorValue in material.ColorValues)
                {
                    if (!colorValue.ColorName.StartsWith("BaseColorLayer"))
                        continue;

                    colorValue.ColorValue.R = (float)rand.NextDouble();
                    colorValue.ColorValue.G = (float)rand.NextDouble();
                    colorValue.ColorValue.B = (float)rand.NextDouble();
                }
            }
        }
    }
}
