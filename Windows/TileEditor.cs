using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static pewSpriteStudio.Globals.Events;

namespace pewSpriteStudio
{
    public partial class TileEditor : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public TileEditor()
        {
            InitializeComponent();
            CurrentColor = GameboyColors.Light;
            CurrentBackColor = GameboyColors.Black;
            btnColor1.BackColor = GameboyColors.None;
            btnColor2.BackColor = GameboyColors.Light;
            btnColor3.BackColor = GameboyColors.Dark;
            btnColor4.BackColor = GameboyColors.Black;

            CurrentTiles = new Tile[4];
            Globals.Events.TilesChanged += TilesChanged;
        }

        private void TilesChanged(object sender, ChangeEventArgs e)
        {
            
            if (e.ChangeType == ChangeEventArgs.EventType.Removed && CurrentTiles.Any(t => (t?.Index ?? -1) == e.Tile.Index))
            {
                this.Hide();
                this.CurrentTiles[0] = null;
                this.CurrentTiles[1] = null;
                this.CurrentTiles[2] = null;
                this.CurrentTiles[3] = null;
            }
            else if (e.ChangeType == ChangeEventArgs.EventType.Selected)
            {
                if (e.Tile == null) return;

                ChangeTile(e.Tile.Index);
            }
        }

        protected override string GetPersistString()
        {
            return GetType().ToString() + "," + DrawGrid + "," + ZoomLevel;
        }

        #region ColorPalette

        private Color currentColor;
        private Color currentBackColor;

        public Color CurrentColor
        {
            get
            {
                return currentColor;
            }
            set
            {
                currentColor = value;
                btnCurrentColor.BackColor = value;
            }
        }

        public Color CurrentBackColor
        {
            get
            {
                return currentBackColor;
            }
            set
            {
                currentBackColor = value;
                btnCurrentBackColor.BackColor = value;
            }
        }

        private void ColorSelect(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                CurrentColor = (sender as ToolStripButton).BackColor;
            else
                CurrentBackColor = (sender as ToolStripButton).BackColor;
        }

        #endregion

        #region Zoom

        private int zoomLevel = 8;

        public int ZoomLevel
        {
            get
            {
                return zoomLevel;
            }
            set
            {
                if (value < 1) zoomLevel = 1;
                var oldZoom = zoomLevel;
                var newZoom = value > 16 ? 16 : value;
                zoomLevel = newZoom;

                if (newZoom != oldZoom)
                {
                    Redraw();
                }

                btnZoomOut.Enabled = (zoomLevel > 1);
                btnZoomIn.Enabled = (zoomLevel < 16);
            }
        }

        public void AdjustZoom(object sender, EventArgs e)
        {
            if (sender == btnZoomIn) ZoomLevel++;
            if (sender == btnZoomOut) ZoomLevel--;
        }

        #endregion


        // WORKING AREA

        public bool DrawGrid { get; set; } = true;

        public Image Grid { get; set; }

        public void GridButtonClick(object sender, EventArgs e)
        {
            DrawGrid = !DrawGrid;
            Redraw();
        }

        public Tile[] CurrentTiles;

        public void ChangeTile(int index)
        {
            CurrentTiles[0] = Tile.Tiles.Values.SingleOrDefault(t => t.Index == index);
            CurrentTiles[1] = null;
            CurrentTiles[2] = null;
            CurrentTiles[3] = null;

            if (EditMode == EditorMode.Sprite || EditMode == EditorMode.BigSprite)
            {
                CurrentTiles[1] = Tile.Tiles.Values.SingleOrDefault(t => t.Index == index + 1);
            }

            if (EditMode == EditorMode.BigSprite)
            {
                CurrentTiles[2] = Tile.Tiles.Values.SingleOrDefault(t => t.Index == index + 2);
                CurrentTiles[3] = Tile.Tiles.Values.SingleOrDefault(t => t.Index == index + 3);
            }
            

            this.TabText = "Tile " + index;
            this.Text = "Tile " + index;
            Redraw();
        }

        public Size CanvasSize { get; set; } = new Size(8, 8);

        private void Redraw()
        {
            if (CurrentTiles[0] == null) return;

            // resize pictureBox
            picTile.Width = CanvasSize.Width * ZoomLevel;
            picTile.Height = CanvasSize.Height * ZoomLevel;

            picTile.BackgroundImage?.Dispose();
            picTile.BackgroundImage = new Bitmap(picTile.Width, picTile.Height);

            using (var graphics = Graphics.FromImage(picTile.BackgroundImage))
            {
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.SmoothingMode = SmoothingMode.None;

                graphics.DrawImage(CurrentTiles[0].TileImage, 0, 0, 8 * ZoomLevel, 8 * ZoomLevel);

                if (EditMode == EditorMode.Sprite || EditMode == EditorMode.BigSprite)
                {
                    if (CurrentTiles[1] != null)
                        graphics.DrawImage(CurrentTiles[1].TileImage, 0, 8 * ZoomLevel, 8 * ZoomLevel, 8 * ZoomLevel);
                }

                if (EditMode == EditorMode.BigSprite)
                {
                    if (CurrentTiles[2] != null)
                        graphics.DrawImage(CurrentTiles[2].TileImage, 8 * ZoomLevel, 0, 8 * ZoomLevel, 8 * ZoomLevel);

                    if (CurrentTiles[3] != null)
                        graphics.DrawImage(CurrentTiles[3].TileImage, 8 * ZoomLevel, 8 * ZoomLevel, 8 * ZoomLevel, 8 * ZoomLevel);
                }

                graphics.Save();
            }

            picTile.Image?.Dispose();
            picTile.Image = null;

            if (zoomLevel > 3 && DrawGrid)
            {
                picTile.Image = new Bitmap(picTile.Width, picTile.Height);

                using (var pen = new Pen(Color.Gray))
                using (var graphics = Graphics.FromImage(picTile.Image))
                {
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    graphics.SmoothingMode = SmoothingMode.None;

                    var stepX = picTile.Width / CanvasSize.Width;
                    var stepY = picTile.Height / CanvasSize.Height;

                    for (var x = 0; x < CanvasSize.Width; x++)
                    {
                        graphics.DrawLine(pen, x * stepX, 0, x * stepX, picTile.Height);
                    }

                    for (var y = 0; y < CanvasSize.Height; y++)
                    { 
                        graphics.DrawLine(pen, 0, y * stepY, picTile.Width, y * stepY);
                    }

                    graphics.Save();
                }
            }

            UpdatePreviews();
        }

        private void UpdatePreviews()
        {
            // 1:1
            preview1.Image?.Dispose();
            preview1.Image = new Bitmap(CanvasSize.Width, CanvasSize.Height);
            preview1.Width = CanvasSize.Width;
            preview1.Height = CanvasSize.Height;

            using (var g = Graphics.FromImage(preview1.Image))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;
                g.DrawImage(CurrentTiles[0].TileImage, 0, 0, 8, 8);
                if (CurrentTiles[1] != null) g.DrawImage(CurrentTiles[1].TileImage, 0, 8, 8, 8);
                if (CurrentTiles[2] != null) g.DrawImage(CurrentTiles[2].TileImage, 8, 0, 8, 8);
                if (CurrentTiles[3] != null) g.DrawImage(CurrentTiles[3].TileImage, 8, 8, 8, 8);
                g.Save();
            }

            preview3.BackgroundImage = preview1.Image;
            preview3.Width = CanvasSize.Width * 3;
            preview3.Height = CanvasSize.Height * 3;
            preview3.Invalidate();


            preview2.Image?.Dispose();
            preview2.Image = new Bitmap(CanvasSize.Width * 2, CanvasSize.Height * 2);

            using (var g = Graphics.FromImage(preview2.Image))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;
                g.DrawImage(CurrentTiles[0].TileImage, 0, 0, 16, 16);
                if (CurrentTiles[1] != null) g.DrawImage(CurrentTiles[1].TileImage, 0, 16, 16, 16);
                if (CurrentTiles[2] != null) g.DrawImage(CurrentTiles[2].TileImage, 16, 0, 16, 16);
                if (CurrentTiles[3] != null) g.DrawImage(CurrentTiles[3].TileImage, 16, 16, 16, 16);
                g.Save();
            }

            preview2.Width = CanvasSize.Width * 2;
            preview2.Height = CanvasSize.Height * 2;

            preview4.BackgroundImage = preview2.Image;
            preview4.Width = CanvasSize.Width * 6;
            preview4.Height = CanvasSize.Height * 6;
            preview4.Invalidate();
        }

        private void DrawPixel(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None) return;
            if (CurrentTiles[0] == null) return;

            var stepX = picTile.BackgroundImage.Width / CanvasSize.Width;
            var stepY = picTile.BackgroundImage.Height / CanvasSize.Height;

            var pixelX = e.Location.X / stepX;
            var pixelY = e.Location.Y / stepY;

            if (pixelX < 0 || pixelX >= CanvasSize.Width || pixelY < 0 || pixelY >= CanvasSize.Height) return;

            var color = e.Button == MouseButtons.Left ? CurrentColor : CurrentBackColor;

            var tileIndex = 0;

            if (pixelY > 7) tileIndex = 1;
            if (pixelX > 7) tileIndex += 2;

            var tile = CurrentTiles[tileIndex];

            if (tile != null)
            {
                var t = (tile.TileImage as Bitmap);

                if (t.GetPixel(pixelX % 8, pixelY % 8) == color) return;

                t.SetPixel(pixelX % 8, pixelY % 8, color);

                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = tile });

                Redraw();
            }
        }

        public void MakeVisible()
        {
            if (this.IsHidden)
            {
                this.Show();
            }
        }

        private void btnFlipX_Click(object sender, EventArgs e)
        {
            if (CurrentTiles[0] == null) return;

            if (EditMode == EditorMode.Tile)
            {
                CurrentTiles[0].TileImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[0] });
            }
            else if (EditMode == EditorMode.Sprite)
            {
                if (CurrentTiles[0] == null || CurrentTiles[1] == null) return;

                CurrentTiles[0].TileImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[0] });

                CurrentTiles[1].TileImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[1] });
            }
            else if (EditMode == EditorMode.BigSprite)
            {
                if (CurrentTiles.Any(t => t == null)) return;

                var tempImage = (Image)CurrentTiles[2].TileImage.Clone();

                CurrentTiles[2].TileImage = CurrentTiles[0].TileImage;
                CurrentTiles[2].TileImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

                tempImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                CurrentTiles[0].TileImage = tempImage;

                var tempImage2 = (Image)CurrentTiles[3].TileImage.Clone();

                CurrentTiles[3].TileImage = CurrentTiles[1].TileImage;
                CurrentTiles[3].TileImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

                tempImage2.RotateFlip(RotateFlipType.RotateNoneFlipX);
                CurrentTiles[1].TileImage = tempImage2;

                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[0] });
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[1] });
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[2] });
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[3] });
            }

            Redraw();
        }

        private void btnFlipY_Click(object sender, EventArgs e)
        {
            if (CurrentTiles[0] == null) return;
            if (EditMode == EditorMode.Tile)
            {
                CurrentTiles[0].TileImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[0] });
            }
            else if (EditMode == EditorMode.Sprite)
            {
                if (CurrentTiles[0] == null || CurrentTiles[1] == null) return;

                var tempImage = (Image)CurrentTiles[1].TileImage.Clone();

                CurrentTiles[1].TileImage = CurrentTiles[0].TileImage;
                CurrentTiles[1].TileImage.RotateFlip(RotateFlipType.RotateNoneFlipY);

                tempImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                CurrentTiles[0].TileImage = tempImage;

                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[0] });
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[1] });
            }
            else if (EditMode == EditorMode.BigSprite)
            {
                if (CurrentTiles.Any(t => t == null)) return;

                var tempImage = (Image)CurrentTiles[1].TileImage.Clone();

                CurrentTiles[1].TileImage = CurrentTiles[0].TileImage;
                CurrentTiles[1].TileImage.RotateFlip(RotateFlipType.RotateNoneFlipY);

                tempImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                CurrentTiles[0].TileImage = tempImage;

                var tempImage2 = (Image)CurrentTiles[3].TileImage.Clone();

                CurrentTiles[3].TileImage = CurrentTiles[2].TileImage;
                CurrentTiles[3].TileImage.RotateFlip(RotateFlipType.RotateNoneFlipY);

                tempImage2.RotateFlip(RotateFlipType.RotateNoneFlipY);
                CurrentTiles[2].TileImage = tempImage2;

                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[0] });
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[1] });
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[2] });
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[3] });

            }
            Redraw();
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (CurrentTiles[0] == null) return;

            if (EditMode == EditorMode.Tile)
            {
                CurrentTiles[0].TileImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[0] });
            }
            else if (EditMode == EditorMode.BigSprite)
            {
                if (CurrentTiles.Any(t => t == null)) return;

                var tempImage = (Image)CurrentTiles[0].TileImage.Clone();

                CurrentTiles[0].TileImage = CurrentTiles[1].TileImage;
                CurrentTiles[0].TileImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

                CurrentTiles[1].TileImage = CurrentTiles[3].TileImage;
                CurrentTiles[1].TileImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

                CurrentTiles[3].TileImage = CurrentTiles[2].TileImage;
                CurrentTiles[3].TileImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

                tempImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                CurrentTiles[2].TileImage = tempImage;

                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[0] });
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[1] });
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[2] });
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Modified, Tile = CurrentTiles[3] });
            }

            Redraw();
        }

        public enum EditorMode
        {
            Tile,
            Sprite,
            BigSprite
        }

        public EditorMode EditMode
        {
            get;set;
        }

        private void btnEditModeTile_Click(object sender, EventArgs e)
        {
            var button = (sender as ToolStripMenuItem);
            EditMode = (EditorMode)Enum.Parse(typeof(EditorMode), button.Tag as String);
            btnRotate.Enabled = EditMode != EditorMode.Sprite;
            btnEditMode.Text = button.Text;

            var size = new Size(8, 8);
            if (EditMode == EditorMode.Sprite || EditMode == EditorMode.BigSprite) size.Height = 16;
            if (EditMode == EditorMode.BigSprite) size.Width = 16;
            CanvasSize = size;

            ChangeTile(CurrentTiles[0].Index);
        }
    }
}
