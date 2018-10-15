using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace pewSpriteStudio
{
    public static class ImageExtensions
    {
        public static byte[] ToGameboyBytes(this Image image)
        {
            var size = (image.Width * image.Height) / 4;
            var result = new byte[size];
            var bitmap = image as Bitmap;

            for (var ox = 0; ox < image.Width; ox += 8)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        var color = bitmap.GetPixel(ox + x, y);

                        byte xMask = (byte)(0x80 >> x);

                        byte colorLow = (byte)(((color == GameboyColors.Black || color == GameboyColors.Light) ? 0xFF : 0x00) & xMask);
                        byte colorHigh = (byte)(((color == GameboyColors.Dark || color == GameboyColors.Black) ? 0xFF : 0x00) & xMask);

                        result[(ox + y) * 2] |= colorLow;
                        result[(ox + y) * 2 + 1] |= colorHigh;
                    }
                }
            }

            return result;
        }

        public static Image FromGameboyBytes(this Image image, byte[] bytes)
        {
            var bitmap = new Bitmap(image);

            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    byte xMask = (byte)(0x80 >> x);

                    var low = bytes[y * 2] & xMask;
                    var high = bytes[y * 2 + 1] & xMask;

                    var color = GameboyColors.Black;

                    if (low == 0 && high == 0) color = GameboyColors.None;
                    if (low == 0 && high > 0) color = GameboyColors.Dark;
                    if (low > 0 && high == 0) color = GameboyColors.Light;

                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
    }
}
