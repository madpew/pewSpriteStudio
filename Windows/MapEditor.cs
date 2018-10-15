using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static pewSpriteStudio.Globals.Events;

namespace pewSpriteStudio
{
    public partial class MapEditor : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public MapEditor()
        {
            InitializeComponent();

            Globals.Events.TilesChanged += TilesChanged;
            Globals.Events.MapsChanged += MapsChanged;
        }

        private void MapsChanged(object sender, ChangeEventArgs e)
        {
            if (e.ChangeType == ChangeEventArgs.EventType.Removed && e.MapIndex == (CurrentMap?.Index ?? e.MapIndex))
            {
                this.Hide();
                this.CurrentMap = null;
            }
            else if (e.ChangeType == ChangeEventArgs.EventType.Selected || e.ChangeType == ChangeEventArgs.EventType.Modified)
            {
                if (e.ChangeType == ChangeEventArgs.EventType.Modified && e.MapIndex != this.CurrentMap.Index) return;
                ChangeMap(e.MapIndex);
            }
        }

        private void TilesChanged(object sender, ChangeEventArgs e)
        {
            if (e.ChangeType == ChangeEventArgs.EventType.Modified && e.Tile.Index == currentTileIndex)
            {
                CurrentTileIndex = CurrentTileIndex;
            }

            if (e.ChangeType == ChangeEventArgs.EventType.Removed && e.Tile.Index == currentTileIndex)
            {
                CurrentTileIndex = -1;
            }

            if (e.ChangeType == ChangeEventArgs.EventType.Selected)
            {
                CurrentTileIndex = e.Tile?.Index ?? 0;
            }

            if (CurrentMap != null)
            {
                if (e.ChangeType == ChangeEventArgs.EventType.Removed && CurrentMap.ContainsTile(e.Tile.Index))
                {
                    CurrentMap.ReplaceTile(e.Tile.Index, 0);
                }

                if (e.ChangeType == ChangeEventArgs.EventType.Modified && CurrentMap.ContainsTile(e.Tile.Index))
                {
                    Redraw(e.Tile.Index);
                }
            }
        }

        protected override string GetPersistString()
        {
            return GetType().ToString() + "," + DrawGrid + "," + ZoomLevel;
        }

        #region ColorPalette

        private int currentTileIndex = -1;

        public int CurrentTileIndex
        {
            get
            {
                return currentTileIndex;
            }
            set
            {
                currentTileIndex = value;
                if (value != -1)
                {
                    btnCurrentTile.Image?.Dispose();
                    btnCurrentTile.Image = Tile.Tiles.Values.Single(t => t.Index == currentTileIndex).GetThumbnail();
                }
                else
                {
                    btnCurrentTile.Image?.Dispose();
                    btnCurrentTile.Image = null;
                }
            }
        }

        #endregion

        #region Zoom

        private int zoomLevel = 1;

        private int[] zoomMap = { 8, 16, 32, 64 };

        public int ZoomLevel
        {
            get
            {
                return zoomLevel;
            }
            set
            {
                if (value < 0) zoomLevel = 0;
                var oldZoom = zoomLevel;
                var newZoom = value > 3 ? 3 : value;
                zoomLevel = newZoom;

                if (newZoom != oldZoom)
                {
                    Redraw();
                }

                btnZoomOut.Enabled = (zoomLevel > 0);
                btnZoomIn.Enabled = (zoomLevel < 3);
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

        public TileMap CurrentMap;

        public void ChangeMap(TileMap Map)
        {
            CurrentMap = Map;
            this.TabText = "Map - " + Map.Description;
            Redraw();
        }

        public void ChangeMap(int index)
        {
            CurrentMap = TileMap.TileMaps[index];
            this.TabText = "Map - " + CurrentMap.Description;
            this.Text = "Map - " + CurrentMap.Description;
            this.Update();
            Redraw();
        }

        /// <summary>
        /// //
        /// </summary>
        private void Redraw(int tileIndex = -1)
        {
            if (CurrentMap == null) return;

            // resize pictureBox
            var tileWidth = zoomMap[ZoomLevel];

            picMap.Width = CurrentMap.Width * tileWidth;
            picMap.Height = CurrentMap.Height * tileWidth;


            if (tileIndex == -1)
            {
                picMap.BackgroundImage?.Dispose();
                picMap.BackgroundImage = new Bitmap(CurrentMap.Width * tileWidth, CurrentMap.Height * tileWidth);
            }

            using (var graphics = Graphics.FromImage(picMap.BackgroundImage))
            {
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.SmoothingMode = SmoothingMode.None;

                for (var y = 0; y < CurrentMap.Height; y++)
                {
                    for (var x = 0; x < CurrentMap.Width; x++)
                    {
                        var index = CurrentMap.GetTile(x, y);

                        if (tileIndex == -1 || tileIndex == index)
                        {
                            var tileImg = Tile.Tiles.Values.SingleOrDefault(t => t.Index == (int)index)?.TileImage;
                            if (tileImg == null) continue;
                            graphics.DrawImage(tileImg, x * tileWidth, y * tileWidth, tileWidth, tileWidth);
                        }
                    }
                }

                graphics.Save();
            }

            picMap.Image?.Dispose();
            picMap.Image = null;

            if (DrawGrid || EditorMode == EditMode.BlockMap)
            {
                picMap.Image = new Bitmap(CurrentMap.Width * tileWidth, CurrentMap.Height * tileWidth);

                using (var pen = new Pen(Color.Gray))
                using (var brush = new SolidBrush(Color.FromArgb(64, Color.Red)))
                using (var graphics = Graphics.FromImage(picMap.Image))
                {
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    graphics.SmoothingMode = SmoothingMode.None;

                    var stepX = tileWidth;
                    var stepY = tileWidth;

                    if (EditorMode == EditMode.BlockMap)
                    {
                        for (var y = 0; y < CurrentMap.Height; y++)
                        {
                            for (var x = 0; x < CurrentMap.Width; x++)
                            {
                                var blocked = CurrentMap.GetBlock(x, y) > 0;

                                if (blocked)
                                {
                                    graphics.FillRectangle(brush, x * stepX, y * stepY, tileWidth, tileWidth);
                                }
                            }
                        }
                    }

                    if (DrawGrid)
                    {
                        graphics.DrawRectangle(pen, 1, 1, CurrentMap.Width * tileWidth - 1, CurrentMap.Height * tileWidth - 1);

                        for (var i = 0; i < CurrentMap.Width; i++)
                        {
                            graphics.DrawLine(pen, i * stepX, 0, i * stepX, CurrentMap.Height * tileWidth);
                        }

                        for (var i = 0; i < CurrentMap.Height; i++)
                        {
                            graphics.DrawLine(pen, 0, i * stepY, CurrentMap.Width * tileWidth, i * stepY);
                        }
                    }

                    graphics.Save();
                }
            }
        }

        private void SetTile(object sender, MouseEventArgs e)
        {
            if (CurrentMap == null) return;

            var stepX = picMap.BackgroundImage.Width / CurrentMap.Width;
            var stepY = picMap.BackgroundImage.Height / CurrentMap.Height;

            var pixelX = e.Location.X / stepX;
            var pixelY = e.Location.Y / stepY;

            txtMapInfo.Text = $"X: {pixelX} Y: {pixelY}";

            if (pixelX < 0 || pixelX >= CurrentMap.Width || pixelY < 0 || pixelY >= CurrentMap.Height) return;

            if (EditorMode == EditMode.Tiles)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (CurrentMap.SetTile(pixelX, pixelY, CurrentTileIndex)) Redraw(CurrentTileIndex);
                }
            }
            else if (EditorMode == EditMode.BlockMap)
            {
                if (e.Button == MouseButtons.Left) CurrentMap.SetBlock(pixelX, pixelY);
                if (e.Button == MouseButtons.Right) CurrentMap.ClearBlock(pixelX, pixelY);
                Redraw(-2);
            }
        }

        public void MakeVisible()
        {
            if (this.IsHidden)
            {
                this.Show();
            }
        }

        private void btnScreenshot_Click(object sender, EventArgs e)
        {
            if (CurrentMap != null)
            {
                var dialog = new SaveFileDialog()
                {
                    FileName = CurrentMap.Name + ".png",
                    CheckPathExists = true,
                    AddExtension = true,
                    OverwritePrompt = true,
                    DefaultExt = ".png",
                    Filter = "Portable Network Graphics (*.png)|*.png",
                    ValidateNames = true,
                    Title = "Save Image..."
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    picMap.BackgroundImage.Save(dialog.FileName, ImageFormat.Png);
                }
            }
        }

        public enum EditMode
        {
            Tiles,
            BlockMap
        }

        public EditMode EditorMode { get; set; } = EditMode.Tiles;

        private void btnBlockMap_Click(object sender, EventArgs e)
        {
            EditorMode = EditMode.BlockMap;
            Redraw();
        }

        private void btnCurrentTile_Click(object sender, EventArgs e)
        {
            EditorMode = EditMode.Tiles;
            Redraw();
        }
    }
}
