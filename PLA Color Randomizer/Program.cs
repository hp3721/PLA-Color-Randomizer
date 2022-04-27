using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            GetPacks(args[0]);
        }

        static void GetPacks(string dirPath)
        {
            outDirPath = Directory.CreateDirectory(Path.Combine(dirPath, "modified_pokemon")).FullName;

            var paks = Directory.EnumerateFiles(dirPath, "*.gfpak").Where(z => Path.GetFileName(z).StartsWith("pm"));
            foreach (var f in paks)
            {
                var pak = new GFPack(f);
                RandomizeBaseLayerColors(ref pak, true);
            }
            Console.WriteLine("Waiting for all gfpak files to be exported");
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Saved modified gfpak files to {outDirPath}");
        }

        static void SaveGFPAK(ref GFPack pak)
        {
            Task task = pak.SaveAs(Path.Combine(outDirPath, $"{Path.GetFileNameWithoutExtension(pak.FilePath)}.gfpak"), ProgressTracker, TokenSource.Token);
            task.Start();
            tasks.Add(task);
        }

        static void RandomizeBaseLayerColors(ref GFPack pak, bool randShiny = false)
        {
            string trmtrFileName = $"{Path.GetFileNameWithoutExtension(pak.FilePath)}{(randShiny ? "_rare" : "")}.trmtr";
            try
            {
                byte[] trmtrFile = pak.GetDataFileName(trmtrFileName);
                trmtr8a trmtrFlatbuffer = FlatBufferConverter.DeserializeFrom<trmtr8a>(trmtrFile);
                RandomizeBaseLayerColors(ref trmtrFlatbuffer);
                pak.SetDataFileName(trmtrFileName, trmtrFlatbuffer.Write());
                SaveGFPAK(ref pak);
                Console.WriteLine(trmtrFileName);
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
