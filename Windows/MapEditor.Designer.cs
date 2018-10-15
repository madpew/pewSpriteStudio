namespace pewSpriteStudio
{
    partial class MapEditor : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapEditor));
            this.stripContainer = new System.Windows.Forms.ToolStripContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.tsPalette = new System.Windows.Forms.ToolStrip();
            this.btnCurrentTile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnScreenshot = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtMapInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnBlockMap = new System.Windows.Forms.ToolStripButton();
            this.stripContainer.ContentPanel.SuspendLayout();
            this.stripContainer.TopToolStripPanel.SuspendLayout();
            this.stripContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.tsPalette.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripContainer
            // 
            // 
            // stripContainer.ContentPanel
            // 
            this.stripContainer.ContentPanel.Controls.Add(this.panel1);
            this.stripContainer.ContentPanel.Size = new System.Drawing.Size(508, 420);
            this.stripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stripContainer.Location = new System.Drawing.Point(0, 0);
            this.stripContainer.Name = "stripContainer";
            this.stripContainer.Size = new System.Drawing.Size(508, 445);
            this.stripContainer.TabIndex = 0;
            this.stripContainer.Text = "toolStripContainer1";
            // 
            // stripContainer.TopToolStripPanel
            // 
            this.stripContainer.TopToolStripPanel.Controls.Add(this.tsPalette);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.picMap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 420);
            this.panel1.TabIndex = 1;
            // 
            // picMap
            // 
            this.picMap.BackColor = System.Drawing.SystemColors.ControlDark;
            this.picMap.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picMap.Location = new System.Drawing.Point(3, 3);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(200, 200);
            this.picMap.TabIndex = 0;
            this.picMap.TabStop = false;
            this.picMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SetTile);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SetTile);
            // 
            // tsPalette
            // 
            this.tsPalette.Dock = System.Windows.Forms.DockStyle.None;
            this.tsPalette.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCurrentTile,
            this.btnBlockMap,
            this.toolStripSeparator1,
            this.btnZoomIn,
            this.btnZoomOut,
            this.btnGrid,
            this.toolStripSeparator2,
            this.btnScreenshot});
            this.tsPalette.Location = new System.Drawing.Point(0, 0);
            this.tsPalette.Name = "tsPalette";
            this.tsPalette.Size = new System.Drawing.Size(508, 25);
            this.tsPalette.Stretch = true;
            this.tsPalette.TabIndex = 0;
            // 
            // btnCurrentTile
            // 
            this.btnCurrentTile.AutoSize = false;
            this.btnCurrentTile.BackColor = System.Drawing.SystemColors.Control;
            this.btnCurrentTile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCurrentTile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCurrentTile.Name = "btnCurrentTile";
            this.btnCurrentTile.Size = new System.Drawing.Size(23, 22);
            this.btnCurrentTile.Text = "Current Tile";
            this.btnCurrentTile.Click += new System.EventHandler(this.btnCurrentTile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.Image")));
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.Click += new System.EventHandler(this.AdjustZoom);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.Image")));
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.Click += new System.EventHandler(this.AdjustZoom);
            // 
            // btnGrid
            // 
            this.btnGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnGrid.Image")));
            this.btnGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGrid.Name = "btnGrid";
            this.btnGrid.Size = new System.Drawing.Size(23, 22);
            this.btnGrid.Text = "Toggle Grid";
            this.btnGrid.Click += new System.EventHandler(this.GridButtonClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnScreenshot
            // 
            this.btnScreenshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnScreenshot.Image = ((System.Drawing.Image)(resources.GetObject("btnScreenshot.Image")));
            this.btnScreenshot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScreenshot.Name = "btnScreenshot";
            this.btnScreenshot.Size = new System.Drawing.Size(23, 22);
            this.btnScreenshot.Text = "Save Image...";
            this.btnScreenshot.Click += new System.EventHandler(this.btnScreenshot_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtMapInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 445);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(508, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtMapInfo
            // 
            this.txtMapInfo.Name = "txtMapInfo";
            this.txtMapInfo.Size = new System.Drawing.Size(12, 17);
            this.txtMapInfo.Text = "-";
            // 
            // btnBlockMap
            // 
            this.btnBlockMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBlockMap.Image = ((System.Drawing.Image)(resources.GetObject("btnBlockMap.Image")));
            this.btnBlockMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBlockMap.Name = "btnBlockMap";
            this.btnBlockMap.Size = new System.Drawing.Size(23, 22);
            this.btnBlockMap.Text = "Edit Block Map";
            this.btnBlockMap.Click += new System.EventHandler(this.btnBlockMap_Click);
            // 
            // MapEditor
            // 
            this.ClientSize = new System.Drawing.Size(508, 467);
            this.Controls.Add(this.stripContainer);
            this.Controls.Add(this.statusStrip1);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapEditor";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "MapEditor";
            this.stripContainer.ContentPanel.ResumeLayout(false);
            this.stripContainer.TopToolStripPanel.ResumeLayout(false);
            this.stripContainer.TopToolStripPanel.PerformLayout();
            this.stripContainer.ResumeLayout(false);
            this.stripContainer.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.tsPalette.ResumeLayout(false);
            this.tsPalette.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer stripContainer;
        private System.Windows.Forms.ToolStrip tsPalette;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCurrentTile;
        private System.Windows.Forms.ToolStripButton btnGrid;
        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel txtMapInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnScreenshot;
        private System.Windows.Forms.ToolStripButton btnBlockMap;
    }
}