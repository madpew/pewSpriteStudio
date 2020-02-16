using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pewSpriteStudio.FileFormat;
using System.Windows.Forms;
using System.IO;
using pewSpriteStudio;
using GBSpriteStudio.Misc;

namespace GBSpriteStudio.FileFormat.Exporters
{
    /// <summary>
    /// Exports the data into C-Headers to include and use with GBDK
    /// </summary>
    public class GbdkExporter : IExporter
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
            // show options dialog
            // pack maps by distinct tile count
            // export block maps

            var tileHeader = Path.Combine(outputDirectory, $"{exportName}_tiles.h");

            using (var header = File.CreateText(tileHeader))
            {
                header.WriteLine($"/* {exportName} tiles - exported by pewSpriteStudio on {DateTime.Now.ToString("s")} */");
                header.WriteLine();
                header.WriteLine($"#ifndef _{exportName.ToUpper()}_TILES_H");
                header.WriteLine($"#define _{exportName.ToUpper()}_TILES_H");
                header.WriteLine();
                header.WriteLine($"// {file.Tiles.Count} tiles");
                header.WriteLine();
                header.WriteLine($"unsigned char {exportName}_tiles[] = {{");

                foreach (var tile in file.Tiles.OrderBy(t => t.Index))
                {
                    var bytestring = "";

                    foreach (var b in tile.TileImage.ToGameboyBytes())
                    {
                        bytestring += "0x" + b.ToString("X2") + ", ";
                    }

                    header.WriteLine($"{bytestring} // {tile.Index}");
                }

                header.WriteLine("};");
                header.WriteLine();

                if (file.Tiles.Any(t => !string.IsNullOrEmpty(t.Label)))
                {
                    header.WriteLine("// labels:");
                    header.WriteLine();

                    foreach (var tile in file.Tiles.Where(t => !string.IsNullOrEmpty(t.Label)).OrderBy(t => t.Index))
                    {
                        header.WriteLine($"#define TILE_{tile.Label.ToUpper()} {tile.Index}");
                    }

                    header.WriteLine();
                }

                header.WriteLine($"#endif /* _{exportName.ToUpper()}_TILES_H */");
            }

            foreach (var map in file.Maps.OrderBy(m => m.Index))
            {
                var mapHeader = Path.Combine(outputDirectory, $"{exportName}_{map.Name}.h");

                using (var header = File.CreateText(mapHeader))
                {
                    header.WriteLine($"/* {exportName} - {map.Name} - exported by pewSpriteStudio on {DateTime.Now.ToString("s")} */");
                    header.WriteLine();
                    header.WriteLine($"#ifndef _{exportName.ToUpper()}_{map.Name.ToUpper()}_H");
                    header.WriteLine($"#define _{exportName.ToUpper()}_{map.Name.ToUpper()}_H");
                    header.WriteLine();
                    header.WriteLine($"// MAP {map.Index}: {map.Name} ({map.Width}x{map.Height})");
                    header.WriteLine();
                    header.WriteLine($"#define {map.Name}_WIDTH {map.Width}u");
                    header.WriteLine($"#define {map.Name}_HEIGHT {map.Height}u");

                    var maxTile = map.Tiles.Max();

                    if (maxTile <= 4)
                    {
                        // back into 2 bit-nibbles
                        header.WriteLine($"#define {map.Name}_PACKED 4u");
                        header.WriteLine();
                        header.WriteLine($"unsigned char {exportName}_{map.Name}_map[] = {{");

                        var packed = map.Tiles.Pack(2);
                        header.WriteLine(string.Join(", ", packed.Select(p => "0x" + p.ToString("X2")).ToArray()));
                    }
                    else if (maxTile <= 15)
                    {
                        // back into words (4bit)
                        header.WriteLine($"#define {map.Name}_PACKED 16u");
                        header.WriteLine();
                        header.WriteLine($"unsigned char {exportName}_{map.Name}_map[] = {{");

                        var packed = map.Tiles.Pack(4);
                        header.WriteLine(string.Join(", ", packed.Select(p => "0x" + p.ToString("X2")).ToArray()));
                    }
                    else
                    {
                        //var distinctCount = map.Tiles.Distinct().Count();

                        header.WriteLine($"#define {map.Name}_PACKED 0u");
                        header.WriteLine();
                        header.WriteLine($"unsigned char {exportName}_{map.Name}_map[] = {{");

                        for (var y = 0; y < map.Height; y++)
                        {
                            for (var x = 0; x < map.Width; x++)
                            {
                                header.Write("0x" + map.Tiles[x + y * map.Width].ToString("X2") + ", ");
                            }

                            header.WriteLine();
                        }
                    }

                    header.WriteLine("};");
                    header.WriteLine();

                    // only export block if set
                    if (map.Blocks.Any(b => b > 0))
                    {
                        if (map.Blocks.Max() == 1)
                        {
                            // only block info
                            header.WriteLine($"#define {exportName}_{map.Name}_BLOCKS_PACKED 1u");
                            header.WriteLine();
                            header.WriteLine($"unsigned char {exportName}_{map.Name}_blocks[]  = {{");

                            /*var packedBuffer = new List<byte>();

                            byte mask = 128;
                            byte current = 0;
                            bool more = false;

                            for (var i = 0; i < map.Blocks.Length; i++)
                            {
                                // set current bit
                                if (map.Blocks[i] > 0)
                                {
                                    current |= (byte)(mask >> (i & 0x07));
                                    more = true;
                                }

                                if ((i & 0x07) == 0x07)
                                {
                                    packedBuffer.Add(current);
                                    current = 0;
                                    more = false;
                                }
                            }

                            if (more)
                            {
                                packedBuffer.Add(current);
                            }*/

                            var packed = map.Blocks.Pack(1);
                            header.WriteLine(string.Join(", ", packed.Select(p => "0x" + p.ToString("X2")).ToArray()));
                        }
                        else
                        {
                            // full properties
                            header.WriteLine($"#define {exportName}_{map.Name}_BLOCKS_PACKED 0u");
                            header.WriteLine();
                            header.WriteLine($"unsigned char {exportName}_{map.Name}_blocks[]  = {{");

                            for (var y = 0; y < map.Height; y++)
                            {
                                for (var x = 0; x < map.Width; x++)
                                {
                                    header.Write("0x" + map.Blocks[x + y * map.Width].ToString("X2") + ", ");
                                }

                                header.WriteLine();
                            }
                        }

                        header.WriteLine("};");
                        header.WriteLine();
                    }

                    header.WriteLine($"#endif /* _{exportName.ToUpper()}_{map.Name.ToUpper()}_H */");
                }
            }

            var exportHeader = Path.Combine(outputDirectory, $"{exportName}.h");

            using (var header = File.CreateText(exportHeader))
            {
                header.WriteLine($"/* {exportName} - exported by pewSpriteStudio on {DateTime.Now.ToString("s")} */");
                header.WriteLine();
                header.WriteLine($"#ifndef _{exportName.ToUpper()}_H");
                header.WriteLine($"#define _{exportName.ToUpper()}_H");
                header.WriteLine();
                header.WriteLine($"#include \"{exportName}_tiles.h\"");

                foreach (var map in file.Maps.OrderBy(m => m.Index))
                {
                    header.WriteLine($"#include \"{exportName}_{map.Name}.h\"");
                }

                header.WriteLine();
                header.WriteLine($"#endif /* _{exportName.ToUpper()}_H */");
            }
        }

        public string GetName()
        {
            return "GBDK C-Headers";
        }
    }
}
