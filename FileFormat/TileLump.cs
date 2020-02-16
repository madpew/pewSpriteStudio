using pewSpriteStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pewSpriteStudio.FileFormat
{
    public class TileLump : PSSLump
    {
        public TileLump() : base((byte)'t')
        {
        }

        public override void Read(BinaryReader reader, PSSFile file)
        {
            var tileCount = reader.ReadInt32();

            for (var i = 0; i < tileCount; i++)
            {
                var tile = new Tile();

                tile.Index = i;

                var labelLength = reader.ReadByte();
                tile.Label = labelLength > 0 ? Encoding.ASCII.GetString(reader.ReadBytes(labelLength)) : string.Empty;

                if (file.Platform == PSSFile.TargetPlatform.Gameboy)
                {
                    var imageData = reader.ReadBytes(16);
                    tile.TileImage = Tile.Empty().FromGameboyBytes(imageData);
                }
                else
                {
                    throw new NotImplementedException();
                }

                file.Tiles.Add(tile);
            }
        }

        public override void Write(BinaryWriter writer, PSSFile file)
        {
            if (file.Tiles.Count == 0) return;

            writer.Write(file.Tiles.Count);

            foreach (var tile in file.Tiles.OrderBy(t => t.Index))
            {
                writer.Write((byte)(tile.Label?.Length ?? 0));

                if (!string.IsNullOrEmpty(tile.Label))
                {
                    writer.Write(Encoding.ASCII.GetBytes(tile.Label));
                }

                if (file.Platform == PSSFile.TargetPlatform.Gameboy)
                {
                    writer.Write(tile.TileImage.ToGameboyBytes());
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
