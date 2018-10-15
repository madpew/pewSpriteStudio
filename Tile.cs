using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static pewSpriteStudio.Globals.Events;

namespace pewSpriteStudio
{
    public class Tile
    {
        public static Image Empty(int w = 8, int h = 8)
        {
            var bitmap = new Bitmap(w, h);

            using (var brush = new SolidBrush(GameboyColors.None))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(brush, 0, 0, w, h);
                g.Save();
            }

            return bitmap;
        }

        public static Dictionary<Guid, Tile> Tiles = new Dictionary<Guid, Tile>();

        public static int GetNextFreeIndex()
        {
            for (var i = 0; i < 128; i++)
            {
                if (!Tiles.Values.Any(t => t.Index == i))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int Add(Image tileImage = null)
        {
            var freeTileIdx = GetNextFreeIndex();

            if (freeTileIdx == -1)
            {
                MessageBox.Show("Can't add Tile. Max Pool size reached.");
                return -1;
            }

            var tile = new Tile() { Index = freeTileIdx, TileImage = (tileImage == null) ? Tile.Empty() : tileImage };
            Tiles.Add(tile.Id, tile);

            Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Added, Tile = tile });

            return freeTileIdx;
        }

        public static void Remove(Guid id)
        {
            var tile = Tile.GetById(id);
            TileMap.ReplaceTiles(tile.Index, 0);
            Tiles.Remove(tile.Id);
            Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Removed, Tile = tile });
        }

        public static int Clone(Guid id)
        {
            var tile = Tile.GetById(id);
            return Add((Image)tile.TileImage.Clone());
        }

        public static Tile GetById(Guid id)
        {
            return Tiles.Values.SingleOrDefault(t => t.Id == id);
        }

        public static void Load(byte[] bytes)
        {
            var tileImage = Tile.Empty().FromGameboyBytes(bytes);
            var tile = new Tile() { Index = GetNextFreeIndex(), TileImage = tileImage };
            Tiles.Add(tile.Id, tile);
        }

        public Guid Id = Guid.NewGuid();
        public string Label = "";
        public Image TileImage { get; set; }
        public int Index;

        public Image GetThumbnail(int dim = 24)
        {
            var result = new Bitmap(dim, dim);

            using (var g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;
                g.DrawImage(TileImage, 0, 0, dim, dim);
                g.Save();
            }

            return result;
        }
    }
}
