using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GBSpriteStudio.Misc
{
    public static class ByteArrayExtension
    {

        // TODO: Implement packing for multibyte bounds
        public static byte[] Pack(this byte[] source, int bitWidth)
        {
            if (bitWidth == 8 || bitWidth == 0) return source;
            
            byte mask = (byte)((1 << bitWidth) - 1);
            
            var packed = new List<byte>();

            byte Accumulator = 0;
            var AccumulatorBits = 0;

            bool more = false;

            for (var i = 0; i < source.Length; i++)
            {
                var currentValue = (byte)(source[i] & mask);

                Accumulator |= currentValue;
                AccumulatorBits += bitWidth;

                if (AccumulatorBits == 8)
                {
                    packed.Add(Accumulator);
                    Accumulator = 0;
                    AccumulatorBits = 0;
                    more = false;
                }
                else
                {
                    Accumulator = (byte)(Accumulator << bitWidth);
                    more = true;
                }
            }

            if (more)
            {
                Accumulator = (byte)(Accumulator << (8 - AccumulatorBits));
                packed.Add(Accumulator);
            }

            return packed.ToArray();
        }
    }
}
