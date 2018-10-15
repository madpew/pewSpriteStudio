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
    public class TileMap
    {
        public static void SwitchTiles(int a, int b)
        {
            foreach (var map in TileMaps.Values)
            {
                for (var i = 0; i < map.Tiles.Length; i++)
                {
                    if (map.Tiles[i] == a) map.Tiles[i] = (byte)b;
                    else if (map.Tiles[i] == b) map.Tiles[i] = (byte)a;
                }
            }
        }

        public static void ReplaceTiles(int a, int b)
        {
            foreach (var map in TileMaps.Values)
            {
                map.ReplaceTile(a, b);
            }
        }

        public static void FixTiles(Dictionary<byte,byte> changes)
        {
            foreach (var map in TileMaps.Values)
            {
                map.ApplyChanges(changes);
            }
        }

        public static Dictionary<int, TileMap> TileMaps = new Dictionary<int, TileMap>();

        public static int Add(TileMap tileMap)
        {
            var freeIdx = -1;

            for (var i = 0; i < 128; i++)
            {
                if (!TileMaps.ContainsKey(i))
                {
                    freeIdx = i;
                    break;
                }
            }

            if (freeIdx == -1)
            {
                MessageBox.Show("Can't add TileMap. Max Pool size reached.");
                return -1;
            }

            tileMap.Index = freeIdx;

            TileMaps.Add(freeIdx, tileMap);

            Globals.Events.OnMapsChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Added, MapIndex = freeIdx });

            return freeIdx;
        }

        public static void Remove(int mapIndex)
        {
            if (TileMaps.ContainsKey(mapIndex))
            {
                TileMaps.Remove(mapIndex);
                Globals.Events.OnMapsChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Removed, MapIndex = mapIndex });
            }
        }

        public static int Clone(int mapIndex)
        {
            if (TileMaps.ContainsKey(mapIndex))
            {
                var tileMap = TileMaps[mapIndex];

                return Add(new TileMap(tileMap.Name + "_2", tileMap.Width, tileMap.Height, (byte[])tileMap.Tiles.Clone(), (byte[])tileMap.Blocks.Clone()));
            }
            else
            {
                MessageBox.Show("TileMap to clone not found.");
                return -1;
            }
        }

        public int Index { get; set; }
        public string Name { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public byte[] Tiles { get; private set; }
        public byte[] Blocks { get; private set; }

        public TileMap(string name, int width, int height, byte[] tiles = null, byte[] blocks = null)
        {
            Name = name;
            Width = width;
            Height = height;
            Tiles = tiles ?? new byte[width * height];
            Blocks = blocks ?? new byte[width * height];
        }

        public string Description
        {
            get
            {
                return $"{Name} ({Width}x{Height})";
            }
        }

        public Image GetImage()
        {
            var image = new Bitmap(Width, Height);

            using (var g = Graphics.FromImage(image))
            using (var brush = new SolidBrush(GameboyColors.None))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;

                g.FillRectangle(brush, 0, 0, Width, Height);

                for (var y = 0; y < Height; y++)
                {
                    for (var x = 0; x < Width; x++)
                    {
                        var tileIndex = Tiles[x + y * Width];
                        var tileImage = Tile.Tiles.Values.Single(t => t.Index == tileIndex).TileImage;
                        g.DrawImage(tileImage, x, y);
                    }
                }

                g.Save();
            }

            return image;
        }

        public bool ContainsTile(int tileIndex)
        {
            return Array.Exists(Tiles,  t => t == tileIndex);
        }

        public bool SetTile(int x, int y, int tileIndex)
        {
            var index = x + y * Width;

            if (index >= 0 && index < Tiles.Length)
            {
                if (Tiles[index] != (byte)tileIndex)
                {
                    Tiles[index] = (byte)tileIndex;
                    Globals.Events.OnMapsChanged(new Globals.Events.ChangeEventArgs() { ChangeType = Globals.Events.ChangeEventArgs.EventType.Modified, MapIndex = Index });
                    return true;
                }
            }

            return false;
        }

        public byte GetTile(int x, int y)
        {
            var index = x + y * Width;

            if (index >= 0 && index < Tiles.Length)
            {
                return Tiles[index];
            }

            return 0;
        }

        public void SetBlock(int x, int y)
        {
            var index = x + y * Width;

            if (index >= 0 && index < Blocks.Length)
            {
                if ((Blocks[index] & (byte)1) == 0)
                {
                    Blocks[index] |= (byte)1;
                    Globals.Events.OnMapsChanged(new Globals.Events.ChangeEventArgs() { ChangeType = Globals.Events.ChangeEventArgs.EventType.Modified, MapIndex = Index });
                }
            }
        }

        public void ClearBlock(int x, int y)
        {
            var index = x + y * Width;

            if (index >= 0 && index < Blocks.Length)
            {
                if ((Blocks[index] & (byte)1) == 1)
                {
                    Blocks[index] &= (byte)254;
                    Globals.Events.OnMapsChanged(new Globals.Events.ChangeEventArgs() { ChangeType = Globals.Events.ChangeEventArgs.EventType.Modified, MapIndex = Index });
                }
            }
        }

        public byte GetBlock(int x, int y)
        {
            var index = x + y * Width;

            if (index >= 0 && index < Blocks.Length)
            {
                return Blocks[index];
            }

            return 0;
        }

        public void ReplaceTile(int from, int to)
        {
            for (int i = Tiles.Length - 1; i >= 0; i--)
            {
                if (Tiles[i] == (byte)from)
                {
                    Tiles[i] = (byte)to;
                }
            }

            Globals.Events.OnMapsChanged(new Globals.Events.ChangeEventArgs() { ChangeType = Globals.Events.ChangeEventArgs.EventType.Modified, MapIndex = Index });
        }

        public void ApplyChanges(Dictionary<byte,byte> changes)
        {
            for (int i = 0; i<Tiles.Length; i++)
            {
                var current = Tiles[i];

                if (changes.ContainsKey(current))
                {
                    Tiles[i] = changes[current];
                }
                else
                {
                    Tiles[i] = 0;
                }
            }

            Globals.Events.OnMapsChanged(new Globals.Events.ChangeEventArgs() { ChangeType = Globals.Events.ChangeEventArgs.EventType.Modified, MapIndex = Index });
        }


        public void Resize(int width, int height)
        {
            var oldWidth = this.Width;
            var oldHeight = this.Height;
            var oldTiles = this.Tiles;
            var oldBlocks = this.Blocks;

            this.Width = width;
            this.Height = height;

            this.Tiles = new byte[width * height];
            this.Blocks = new byte[width * height];
            
            var limitX = Math.Min(oldWidth, width);
            var limitY = Math.Min(oldHeight, height);

            for (var y = 0; y < limitY; y++)
            {
                for (var x = 0; x < limitX; x++)
                {
                    this.Tiles[x + y * width] = oldTiles[x + y * oldWidth];
                    this.Blocks[x + y * width] = oldBlocks[x + y * oldWidth];
                }
            }

            Globals.Events.OnMapsChanged(new Globals.Events.ChangeEventArgs() { ChangeType = Globals.Events.ChangeEventArgs.EventType.Modified, MapIndex = Index });
        }

        public static TileMap Create(string name, int width, int height)
        {
            return new TileMap(name, width, height);
        }
    }
}
