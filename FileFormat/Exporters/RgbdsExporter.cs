using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pewSpriteStudio.FileFormat;
using System.Windows.Forms;
using System.IO;
using pewSpriteStudio;

namespace GBSpriteStudio.FileFormat.Exporters
{
    public class RgbdsExporter : IExporter
    {
        public bool CanExport(PSSFile file)
        {
            if (file == null) return false;
            if (file.Platform != PSSFile.TargetPlatform.Gameboy && file.Platform != PSSFile.TargetPlatform.GameboyColor) return false;
            return true;
        }

        public void Export(PSSFile file)
        {
            var folderSelector = new FolderBrowserDialog()
            {
                Description = "Select Output Directory...",
                ShowNewFolderButton = true
            };

            if (folderSelector.ShowDialog() != DialogResult.OK) return;

            var outputDirectory = folderSelector.SelectedPath;

            var exportName = Path.GetFileNameWithoutExtension(file.Filename);

            var exportFilename = Path.Combine(outputDirectory, $"{exportName}.inc");

            using (var header = File.CreateText(exportFilename))
            {
                var sourceName = exportName;

                header.WriteLine($"; Exported from sourcefile: '{file.Filename}'");
                header.WriteLine($"; Conversion: {DateTime.Now.ToString("s")}");
                header.WriteLine();

                if (true) // export tiles
                {
                    header.WriteLine($"; {Tile.Tiles.Count} TILES");
                    header.WriteLine($"{sourceName}_tile_count EQU {Tile.Tiles.Count}");
                    header.WriteLine($"{sourceName}_tile_data_size EQU {Tile.Tiles.Count * 16}");
                    header.WriteLine($"{sourceName}_tile_data:");

                    foreach (var tile in Tile.Tiles.Values.OrderBy(t => t.Index))
                    {
                        var bytestring = "DB ";

                        foreach (var b in tile.TileImage.ToGameboyBytes())
                        {
                            bytestring += "$" + b.ToString("X2") + ",";
                        }

                        header.WriteLine($"{bytestring.TrimEnd(',')} ; {(string.IsNullOrEmpty(tile.Label) ? "tile" : tile.Label)} {tile.Index}");
                    }

                    header.WriteLine();

                    foreach (var tile in Tile.Tiles.Values.OrderBy(t => t.Index))
                    {
                        if (!string.IsNullOrEmpty(tile.Label))
                        {
                            header.WriteLine("TILEIDX_" + tile.Label.ToUpper() + " EQU " + tile.Index);
                        }
                    }

                    if (true) header.WriteLine(); // export maps
                }

                if (true) // export maps
                {
                    header.WriteLine($"; {TileMap.TileMaps.Count} MAPS");

                    foreach (var map in TileMap.TileMaps.Values.OrderBy(m => m.Index))
                    {
                        header.WriteLine();
                        header.WriteLine($"; MAP {map.Index}: {map.Name} ({map.Width}x{map.Height})");
                        var safeMapName = map.Name.Replace(" ", "");
                        header.WriteLine();
                        header.WriteLine($"{safeMapName}_width EQU {map.Width}");
                        header.WriteLine($"{safeMapName}_height EQU {map.Height}");
                        header.WriteLine($"{safeMapName}_map_data_size EQU {map.Width * map.Height}");
                        header.WriteLine();
                        header.WriteLine($"{safeMapName}_map_data:");

                        for (var y = 0; y < map.Height; y++)
                        {
                            var line = "DB ";

                            for (var x = 0; x < map.Width; x++)
                            {
                                line += "$" + map.Tiles[x + y * map.Width].ToString("X2") + ",";
                            }

                            header.WriteLine(line.TrimEnd(','));
                        }

                        header.WriteLine();

                        if (map.Blocks.Any(b => b != 0))
                        {
                            header.WriteLine($"{safeMapName}_map_block_data:");

                            for (var y = 0; y < map.Height; y++)
                            {
                                var line = "DB";

                                for (var x = 0; x < map.Width; x++)
                                {
                                    line += " $" + map.Blocks[x + y * map.Width].ToString("X2") + ",";
                                }

                                header.WriteLine(line.TrimEnd(','));
                            }
                        }
                    }
                }

                header.Close();
            }
        }

        public string GetName()
        {
            return "RGBDS Assembly";
        }
    }
}
