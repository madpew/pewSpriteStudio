using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static pewSpriteStudio.Globals.Events;
using pewSpriteStudio.FileFormat;

namespace pewSpriteStudio
{
    public static class FileTransfer
    {
        #region File Operations

        private static byte fileFormatVersion = 1;

        public static void SaveFile(string filename, bool exportTiles = true, bool exportMaps = true)
        {
            var exportFile = new PSSFile();
            // hardcode for now
            exportFile.Platform = PSSFile.TargetPlatform.Gameboy;
            exportFile.Tiles = Tile.Tiles.Values.ToList();
            exportFile.Maps = TileMap.TileMaps.Values.ToList();
            exportFile.Save(filename);
            exportFile.FullPath = filename;
            exportFile.Filename = Path.GetFileName(filename);
            Globals.CurrentFile = exportFile;
        }

        public static bool LoadFile(string filename, bool loadTiles = true, bool loadMaps = true)
        {
            var loadedFile = PSSFile.Load(filename);
            loadedFile.FullPath = filename;
            loadedFile.Filename = Path.GetFileName(filename);
            Globals.CurrentFile = loadedFile;

            if (loadTiles)
            {
                foreach (var tile in loadedFile.Tiles)
                {
                    Tile.Tiles.Add(tile.Id, tile);
                }
            }

            if (loadMaps)
            {
                foreach (var map in loadedFile.Maps)
                {
                    TileMap.TileMaps.Add(map.Index, map);
                }
            }
                
            if (loadTiles)
            {
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Added });
            }

            if (loadMaps)
            {
                Globals.Events.OnMapsChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Added, MapIndex = -1 });
            }

            return true;
        }

        public static void SaveCHeader(string filename, bool exportTiles = true, bool exportMaps = true)
        {
            using (var header = File.CreateText(filename))
            {
                var sourceName = Path.GetFileNameWithoutExtension(filename).Replace(" ", "");

                header.WriteLine("/*");
                header.WriteLine($" * Exported from sourcefile: '{Globals.CurrentFilename}'");
                header.WriteLine($" * Conversion: {DateTime.Now.ToString("s")}");
                header.WriteLine(" */");
                header.WriteLine();
                header.WriteLine($"#ifndef _{sourceName.ToUpper()}_H");
                header.WriteLine($"#define _{sourceName.ToUpper()}_H");
                header.WriteLine();

                if (exportTiles)
                {
                    header.WriteLine($"// {Tile.Tiles.Count} TILES");
                    header.WriteLine($"unsigned char {sourceName}_tiles[] = {{");

                    foreach (var tile in Tile.Tiles.Values.OrderBy(t => t.Index))
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

                    foreach (var tile in Tile.Tiles.Values.OrderBy(t => t.Index))
                    {
                        if (!string.IsNullOrEmpty(tile.Label))
                        {
                            header.Write("#define TILE_" + tile.Label.ToUpper() + " " + tile.Index);
                        }
                    }

                    if (exportMaps) header.WriteLine();
                }

                if (exportMaps)
                {
                    header.WriteLine($"// {TileMap.TileMaps.Count} MAPS");

                    foreach (var map in TileMap.TileMaps.Values.OrderBy(m => m.Index))
                    {
                        header.WriteLine();
                        header.WriteLine($"// MAP {map.Index}: {map.Name} ({map.Width}x{map.Height})");
                        var safeMapName = map.Name.Replace(" ", "");
                        header.WriteLine();
                        header.WriteLine($"#define {safeMapName}_WIDTH {map.Width}u");
                        header.WriteLine($"#define {safeMapName}_HEIGHT {map.Height}u");
                        header.WriteLine();
                        header.WriteLine($"unsigned char {safeMapName}_map[] = {{");

                        for (var y = 0; y < map.Height; y++)
                        {
                            for (var x = 0; x < map.Width; x++)
                            {
                                header.Write("0x" + map.Tiles[x + y * map.Width].ToString("X2") + ", ");
                            }

                            header.WriteLine();
                        }

                        header.WriteLine("};");
                        header.WriteLine();

                        if (map.Blocks.Any(b => b != 0))
                        {
                            header.WriteLine($"unsigned char {safeMapName}_blocks[] = {{");

                            for (var y = 0; y < map.Height; y++)
                            {
                                for (var x = 0; x < map.Width; x++)
                                {
                                    header.Write("0x" + map.Blocks[x + y * map.Width].ToString("X2") + ", ");
                                }

                                header.WriteLine();
                            }


                            /*                            var tempBuffer = new List<byte>();

                                                        for (var y = 0; y < map.Height; y++)
                                                        {
                                                            for (var x = 0; x < map.Width; x++)
                                                            {
                                                                tempBuffer.Add(map.Blocks[x + y * map.Width]);
                                                            }
                                                        }


                                                        var packedBuffer = new List<byte>();

                                                        byte mask = 128;
                                                        byte current = 0;
                                                        bool more = false;

                                                        for (var i = 0; i < tempBuffer.Count; i++)
                                                        {
                                                            // set current bit
                                                            if (tempBuffer[i] > 0)
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
                                                        }

                                                        foreach (var b in packedBuffer)
                                                        {
                                                            header.Write("0x" + b.ToString("X2") + ", ");
                                                        }*/

                            header.WriteLine();

                            header.WriteLine("};");
                        }
                    }
                }

                header.WriteLine();
                header.WriteLine("#endif");

                header.Close();
            }
        }
        #endregion
    }
}
