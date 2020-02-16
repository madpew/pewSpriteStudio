using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pewSpriteStudio.FileFormat
{
    public abstract class PSSLump
    {
        public static Dictionary<byte, Type> KnownLumpTypes = new Dictionary<byte, Type>();

        public static void EnsureLumpTypesFilled()
        {
            if (KnownLumpTypes.Count == 0)
            {
                KnownLumpTypes.Add((byte)'m', typeof(MapLump));
                KnownLumpTypes.Add((byte)'t', typeof(TileLump));
            }
        }

        public static Type GetLumpType(byte lumpTypeId)
        {
            EnsureLumpTypesFilled();

            if (KnownLumpTypes.ContainsKey(lumpTypeId)) return KnownLumpTypes[lumpTypeId];
            return null;
        }

        public readonly byte lumpTypeId;

        public PSSLump(byte identifier)
        {
            lumpTypeId = identifier;
        }

        public abstract void Write(BinaryWriter writer, PSSFile file);
        public abstract void Read(BinaryReader reader, PSSFile file);
    }
}
