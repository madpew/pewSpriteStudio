using pewSpriteStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pewSpriteStudio.FileFormat
{
    public class MapLump : PSSLump
    {
        public MapLump() : base((byte)'m')
        {
        }

        public override void Read(BinaryReader reader, PSSFile file)
        {
            var mapCount = reader.ReadInt32();

            for (var i = 0; i < mapCount; i++)
            {
                var width = reader.ReadInt32();
                var height = reader.ReadInt32();
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

                file.Maps.Add(map);
            }
        }

        public override void Write(BinaryWriter writer, PSSFile file)
        {
            writer.Write(file.Maps.Count);

            foreach (var map in file.Maps.OrderBy(t => t.Index))
            {
                writer.Write(map.Width);
                writer.Write(map.Height);
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
