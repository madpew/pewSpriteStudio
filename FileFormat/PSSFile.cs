using pewSpriteStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pewSpriteStudio.FileFormat
{
    public class PSSFile
    {
        public static byte MaxVersion = 1;

        public static PSSFile Load(string filename)
        {
            using (var fileStream = File.OpenRead(filename))
            using (var reader = new BinaryReader(fileStream))
            {
                var fileTag = Encoding.ASCII.GetString(reader.ReadBytes(3));

                if (fileTag != "PSS") throw new Exception("not a PSS File");

                var formatVersion = reader.ReadByte();
                var platform = reader.ReadByte();

                var loadedFile = new PSSFile() { Version = formatVersion, Platform = (TargetPlatform)platform, VersionMismatch = formatVersion != MaxVersion };

                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    loadedFile.ReadLump(reader);
                }

                reader.Close();
                fileStream.Close();
                return loadedFile;
            }
        }

        private void ReadLump(BinaryReader reader)
        {
            var lumpTypeId = reader.ReadByte();
            var lumpLength = reader.ReadInt32();

            var lumpType = PSSLump.GetLumpType(lumpTypeId);

            // skip unknown lumps
            if (lumpType == null)
            {
                reader.ReadBytes(lumpLength); // TODO: Seek would probably be faster
                return;
            }

            var lump = (PSSLump)Activator.CreateInstance(lumpType);
            lump.Read(reader, this);
        }

        public void Save(string filename)
        {
            using (var fileStream = new FileStream(filename, FileMode.Create))
            using (var writer = new BinaryWriter(fileStream))
            {
                // write header
                writer.Write(Encoding.ASCII.GetBytes("PSS"));
                writer.Write(MaxVersion);
                writer.Write((byte)this.Platform);

                WriteLump(writer, new TileLump());
                WriteLump(writer, new MapLump());
                
                writer.Flush();
                writer.Close();
                fileStream.Close();
            }
        }

        private void WriteLump(BinaryWriter writer, PSSLump lump)
        {
            using (var bufferStream = new MemoryStream())
            using (var bufferWriter = new BinaryWriter(bufferStream))
            {
                lump.Write(bufferWriter, this);

                bufferWriter.Flush();
                bufferWriter.Close();
                var payload = bufferStream.ToArray();

                if (payload.Length > 0)
                {
                    writer.Write(lump.lumpTypeId);
                    writer.Write(payload.Length);
                    writer.Write(payload);
                }
            }
        }

        public enum TargetPlatform
        {
            Unknown,
            Gameboy,
            GameboyColor,
            Genesis,
            PC
        }

        public int Version { get; set; } = MaxVersion;
        public TargetPlatform Platform { get; set; }
        public bool VersionMismatch { get; set; } = false;

        public List<Tile> Tiles = new List<Tile>();
        public List<TileMap> Maps = new List<TileMap>();

        public string Filename { get; set; }
        public string FullPath { get; set; }
    }
}
