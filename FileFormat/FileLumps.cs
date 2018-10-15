using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pewSpriteStudio.FileFormat
{
    public interface IFileLump
    {
        void Read(BinaryReader reader, byte version);
        void Write(BinaryWriter writer, byte version);
    }

    public class FileLump
    {
        public static Dictionary<byte, Type> LumpTypes = new Dictionary<byte, Type>();

        public static void EnsureLumpTypesFilled()
        {
            if (LumpTypes.Count == 0)
            {
                LumpTypes.Add((byte)'m', typeof(MapLump));
                LumpTypes.Add((byte)'t', typeof(TileLump));
                LumpTypes.Add((byte)'i', typeof(InfoLump));
            }
        }

        /*public static IFileLump Read(BinaryReader reader, byte version)
        {
            EnsureLumpTypesFilled();

            //var identifier = reader.ReadByte();

            if (LumpTypes.ContainsKey(identifier))
            {
                var lumpType = LumpTypes[identifier];
                var lump = (IFileLump)Activator.CreateInstance(lumpType);
                lump.Read(reader, version);
                return lump;
            }
            else
            {
                throw new Exception("unknown lump!");
            }
        }*/

        public static T Read<T>(BinaryReader reader, byte version) where T : IFileLump
        {
            //var identifier = reader.ReadByte();

            var lump = (T)Activator.CreateInstance(typeof(T));
            lump.Read(reader, version);
            return lump;
        }
    }


    public class InfoLump : IFileLump
    {
        public int Version { get; set; }

        public void Read(BinaryReader reader, byte version)
        {
            if (Encoding.ASCII.GetString(reader.ReadBytes(4)) != "GBSS") throw new Exception("Invalid File Format.");
            Version = reader.ReadByte();
        }

        public void Write(BinaryWriter writer, byte version)
        {
            writer.Write(Encoding.ASCII.GetBytes("GBSS"));
            writer.Write((byte)Version);
        }
    }

    public class TileLump : IFileLump
    {
        public List<Tile> Tiles = new List<Tile>();

        public void Read(BinaryReader reader, byte version)
        {
            var tileCount = reader.Read();

            for (var i = 0; i < tileCount; i++)
            {
                var tile = new Tile();

                tile.Index = i;

                var labelLength = reader.ReadByte();
                tile.Label = labelLength > 0 ? Encoding.ASCII.GetString(reader.ReadBytes(labelLength)) : string.Empty;

                var imageData = reader.ReadBytes(16);
                tile.TileImage = Tile.Empty().FromGameboyBytes(imageData);
                Tiles.Add(tile);
            }
        }

        public void Write(BinaryWriter writer, byte version)
        {
            writer.Write((byte)Tile.Tiles.Count);

            foreach (var tile in Tile.Tiles.Values.OrderBy(t => t.Index))
            {
                writer.Write((byte)(tile.Label?.Length ?? 0));

                if (!string.IsNullOrEmpty(tile.Label))
                {
                    writer.Write(Encoding.ASCII.GetBytes(tile.Label));
                }

                writer.Write(tile.TileImage.ToGameboyBytes());
            }
        }
    }

    public class MapLump : IFileLump
    {
        public List<TileMap> Maps = new List<TileMap>();

        public void Read(BinaryReader reader, byte version)
        {
            var mapCount = reader.Read();

            for (var i = 0; i < mapCount; i++)
            {
                var width = reader.ReadByte();
                var height = reader.ReadByte();
                var labelLength = reader.ReadByte();
                var name = labelLength > 0 ? Encoding.ASCII.GetString(reader.ReadBytes(labelLength)) : string.Empty;
                var mapFlags = reader.ReadByte();

                var mapData = reader.ReadBytes(width * height);

                byte[] blocks = null;

                if (mapFlags == 1)
                {
                    blocks = reader.ReadBytes(width * height);
                }

                var map = new TileMap(name, width, height, mapData, blocks) { Index = i };

                Maps.Add(map);
            }
        }

        public void Write(BinaryWriter writer, byte version)
        {
            writer.Write((byte)TileMap.TileMaps.Count);

            foreach (var map in TileMap.TileMaps.Values.OrderBy(t => t.Index))
            {
                writer.Write((byte)map.Width);
                writer.Write((byte)map.Height);
                writer.Write((byte)map.Name.Length);
                writer.Write(Encoding.ASCII.GetBytes(map.Name));
                writer.Write(map.Blocks == null ? (byte)0 : (byte)1);

                writer.Write(map.Tiles);

                if (map.Blocks != null)
                {
                    writer.Write(map.Blocks);
                }
            }
        }
    }
}
