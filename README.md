# PLA-Color-Randomizer
Randomizes the material colors for PLA Pokémon

## Usage
* Run `"PLA Color Randomizer.exe" <path to /bin/archive/pokemon/ directory in romfs> --randomize-mode <Both|NormalOnly|ShinyOnly>`
* Default `randomizeMode` is `Both`
* Pokémon models with the modified material files can be found in the `/bin/archive/pokemon/modified_pokemon/` directory 

## Dependencies
Decompressing data within a `*.gfpak` archive for PLA requires having the [Oodle Decompressor dll](http://www.radgametools.com/oodlecompressors.htm) in the same folder as the executable. This program has a hardcoded reference to `oo2core_8_win64.dll`, which can be sourced from other games (for example Warframe, which is free on Steam).
