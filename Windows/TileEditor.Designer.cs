namespace pewSpriteStudio
{
    partial class TileEditor : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileEditor));
            this.stripContainer = new System.Windows.Forms.ToolStripContainer();
            this.editorSplit = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.picTile = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.preview1 = new System.Windows.Forms.PictureBox();
            this.preview3 = new System.Windows.Forms.PictureBox();
            this.preview2 = new System.Windows.Forms.PictureBox();
            this.preview4 = new System.Windows.Forms.PictureBox();
            this.tsPalette = new System.Windows.Forms.ToolStrip();
            this.btnColor1 = new System.Windows.Forms.ToolStripButton();
            this.btnColor2 = new System.Windows.Forms.ToolStripButton();
            this.btnColor3 = new System.Windows.Forms.ToolStripButton();
            this.btnColor4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCurrentColor = new System.Windows.Forms.ToolStripButton();
            this.btnCurrentBackColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFlipX = new System.Windows.Forms.ToolStripButton();
            this.btnFlipY = new System.Windows.Forms.ToolStripButton();
            this.btnRotate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnEditMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnEditModeTile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.stripContainer.ContentPanel.SuspendLayout();
            this.stripContainer.TopToolStripPanel.SuspendLayout();
            this.stripContainer.SuspendLayout();
            this.editorSplit.Panel1.SuspendLayout();
            this.editorSplit.Panel2.SuspendLayout();
            this.editorSplit.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTile)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preview1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview4)).BeginInit();
            this.tsPalette.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripContainer
            // 
            // 
            // stripContainer.ContentPanel
            // 
            this.stripContainer.ContentPanel.Controls.Add(this.editorSplit);
            this.stripContainer.ContentPanel.Size = new System.Drawing.Size(443, 273);
            this.stripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stripContainer.Location = new System.Drawing.Point(0, 0);
            this.stripContainer.Name = "stripContainer";
            this.stripContainer.Size = new System.Drawing.Size(443, 298);
            this.stripContainer.TabIndex = 0;
            this.stripContainer.Text = "toolStripContainer1";
            // 
            // stripContainer.TopToolStripPanel
            // 
            this.stripContainer.TopToolStripPanel.Controls.Add(this.tsPalette);
            // 
            // editorSplit
            // 
            this.editorSplit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editorSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSplit.Location = new System.Drawing.Point(0, 0);
            this.editorSplit.Name = "editorSplit";
            // 
            // editorSplit.Panel1
            // 
            this.editorSplit.Panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.editorSplit.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // editorSplit.Panel2
            // 
            this.editorSplit.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.editorSplit.Size = new System.Drawing.Size(443, 273);
            this.editorSplit.SplitterDistance = 282;
            this.editorSplit.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.picTile, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(278, 269);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // picTile
            // 
            this.picTile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picTile.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picTile.Location = new System.Drawing.Point(39, 34);
            this.picTile.Name = "picTile";
            this.picTile.Size = new System.Drawing.Size(200, 200);
            this.picTile.TabIndex = 0;
            this.picTile.TabStop = false;
            this.picTile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawPixel);
            this.picTile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawPixel);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.preview1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.preview3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.preview2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.preview4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(153, 269);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // preview1
            // 
            this.preview1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.preview1.Location = new System.Drawing.Point(34, 63);
            this.preview1.MaximumSize = new System.Drawing.Size(16, 16);
            this.preview1.Name = "preview1";
            this.preview1.Size = new System.Drawing.Size(8, 8);
            this.preview1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.preview1.TabIndex = 0;
            this.preview1.TabStop = false;
            // 
            // preview3
            // 
            this.preview3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.preview3.Location = new System.Drawing.Point(26, 189);
            this.preview3.MaximumSize = new System.Drawing.Size(48, 48);
            this.preview3.Name = "preview3";
            this.preview3.Size = new System.Drawing.Size(24, 24);
            this.preview3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.preview3.TabIndex = 1;
            this.preview3.TabStop = false;
            // 
            // preview2
            // 
            this.preview2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.preview2.Location = new System.Drawing.Point(106, 59);
            this.preview2.MaximumSize = new System.Drawing.Size(32, 32);
            this.preview2.Name = "preview2";
            this.preview2.Size = new System.Drawing.Size(16, 16);
            this.preview2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.preview2.TabIndex = 2;
            this.preview2.TabStop = false;
            // 
            // preview4
            // 
            this.preview4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.preview4.Location = new System.Drawing.Point(90, 177);
            this.preview4.MaximumSize = new System.Drawing.Size(96, 96);
            this.preview4.Name = "preview4";
            this.preview4.Size = new System.Drawing.Size(48, 48);
            this.preview4.TabIndex = 3;
            this.preview4.TabStop = false;
            // 
            // tsPalette
            // 
            this.tsPalette.Dock = System.Windows.Forms.DockStyle.None;
            this.tsPalette.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnColor1,
            this.btnColor2,
            this.btnColor3,
            this.btnColor4,
            this.toolStripSeparator1,
            this.btnZoomIn,
            this.btnZoomOut,
            this.btnGrid,
            this.toolStripSeparator2,
            this.btnCurrentColor,
            this.btnCurrentBackColor,
            this.toolStripSeparator3,
            this.btnFlipX,
            this.btnFlipY,
            this.btnRotate,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.btnEditMode});
            this.tsPalette.Location = new System.Drawing.Point(0, 0);
            this.tsPalette.Name = "tsPalette";
            this.tsPalette.Size = new System.Drawing.Size(443, 25);
            this.tsPalette.Stretch = true;
            this.tsPalette.TabIndex = 0;
            // 
            // btnColor1
            // 
            this.btnColor1.AutoSize = false;
            this.btnColor1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(188)))), ((int)(((byte)(15)))));
            this.btnColor1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColor1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor1.Name = "btnColor1";
            this.btnColor1.Size = new System.Drawing.Size(23, 22);
            this.btnColor1.Text = "Color 1";
            this.btnColor1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorSelect);
            // 
            // btnColor2
            // 
            this.btnColor2.AutoSize = false;
            this.btnColor2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(172)))), ((int)(((byte)(15)))));
            this.btnColor2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColor2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor2.Name = "btnColor2";
            this.btnColor2.Size = new System.Drawing.Size(23, 22);
            this.btnColor2.Text = "Color 2";
            this.btnColor2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorSelect);
            // 
            // btnColor3
            // 
            this.btnColor3.AutoSize = false;
            this.btnColor3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(98)))), ((int)(((byte)(48)))));
            this.btnColor3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColor3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor3.Name = "btnColor3";
            this.btnColor3.Size = new System.Drawing.Size(23, 22);
            this.btnColor3.Text = "Color 3";
            this.btnColor3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorSelect);
            // 
            // btnColor4
            // 
            this.btnColor4.AutoSize = false;
            this.btnColor4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(56)))), ((int)(((byte)(15)))));
            this.btnColor4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColor4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor4.Name = "btnColor4";
            this.btnColor4.Size = new System.Drawing.Size(23, 22);
            this.btnColor4.Text = "Color 4";
            this.btnColor4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorSelect);
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
            // btnCurrentColor
            // 
            this.btnCurrentColor.AutoSize = false;
            this.btnCurrentColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(188)))), ((int)(((byte)(15)))));
            this.btnCurrentColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCurrentColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCurrentColor.Name = "btnCurrentColor";
            this.btnCurrentColor.Size = new System.Drawing.Size(23, 22);
            this.btnCurrentColor.Text = "Current Color";
            // 
            // btnCurrentBackColor
            // 
            this.btnCurrentBackColor.AutoSize = false;
            this.btnCurrentBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(56)))), ((int)(((byte)(15)))));
            this.btnCurrentBackColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCurrentBackColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCurrentBackColor.Name = "btnCurrentBackColor";
            this.btnCurrentBackColor.Size = new System.Drawing.Size(23, 22);
            this.btnCurrentBackColor.Text = "Current Backcolor";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFlipX
            // 
            this.btnFlipX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFlipX.Image = ((System.Drawing.Image)(resources.GetObject("btnFlipX.Image")));
            this.btnFlipX.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFlipX.Name = "btnFlipX";
            this.btnFlipX.Size = new System.Drawing.Size(23, 22);
            this.btnFlipX.Text = "Flip X";
            this.btnFlipX.Click += new System.EventHandler(this.btnFlipX_Click);
            // 
            // btnFlipY
            // 
            this.btnFlipY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFlipY.Image = ((System.Drawing.Image)(resources.GetObject("btnFlipY.Image")));
            this.btnFlipY.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFlipY.Name = "btnFlipY";
            this.btnFlipY.Size = new System.Drawing.Size(23, 22);
            this.btnFlipY.Text = "Flip Y";
            this.btnFlipY.Click += new System.EventHandler(this.btnFlipY_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotate.Image = ((System.Drawing.Image)(resources.GetObject("btnRotate.Image")));
            this.btnRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(23, 22);
            this.btnRotate.Text = "Rotate";
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(41, 22);
            this.toolStripLabel1.Text = "Mode:";
            // 
            // btnEditMode
            // 
            this.btnEditMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEditMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEditModeTile,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.btnEditMode.Image = ((System.Drawing.Image)(resources.GetObject("btnEditMode.Image")));
            this.btnEditMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditMode.Name = "btnEditMode";
            this.btnEditMode.Size = new System.Drawing.Size(39, 22);
            this.btnEditMode.Text = "Tile";
            // 
            // btnEditModeTile
            // 
            this.btnEditModeTile.Name = "btnEditModeTile";
            this.btnEditModeTile.Size = new System.Drawing.Size(152, 22);
            this.btnEditModeTile.Tag = "0";
            this.btnEditModeTile.Text = "Tile";
            this.btnEditModeTile.Click += new System.EventHandler(this.btnEditModeTile_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Tag = "1";
            this.toolStripMenuItem2.Text = "Sprite (8x16)";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.btnEditModeTile_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem3.Tag = "2";
            this.toolStripMenuItem3.Text = "Sprite (16x16)";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.btnEditModeTile_Click);
            // 
            // TileEditor
            // 
            this.ClientSize = new System.Drawing.Size(443, 298);
            this.Controls.Add(this.stripContainer);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TileEditor";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "TileEditor";
            this.stripContainer.ContentPanel.ResumeLayout(false);
            this.stripContainer.TopToolStripPanel.ResumeLayout(false);
            this.stripContainer.TopToolStripPanel.PerformLayout();
            this.stripContainer.ResumeLayout(false);
            this.stripContainer.PerformLayout();
            this.editorSplit.Panel1.ResumeLayout(false);
            this.editorSplit.Panel2.ResumeLayout(false);
            this.editorSplit.Panel2.PerformLayout();
            this.editorSplit.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTile)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preview1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview4)).EndInit();
            this.tsPalette.ResumeLayout(false);
            this.tsPalette.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer stripContainer;
        private System.Windows.Forms.SplitContainer editorSplit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox preview1;
        private System.Windows.Forms.PictureBox preview3;
        private System.Windows.Forms.PictureBox preview2;
        private System.Windows.Forms.PictureBox preview4;
        private System.Windows.Forms.ToolStrip tsPalette;
        private System.Windows.Forms.ToolStripButton btnColor1;
        private System.Windows.Forms.ToolStripButton btnColor2;
        private System.Windows.Forms.ToolStripButton btnColor3;
        private System.Windows.Forms.ToolStripButton btnColor4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCurrentColor;
        private System.Windows.Forms.PictureBox picTile;
        private System.Windows.Forms.ToolStripButton btnGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripButton btnCurrentBackColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnFlipX;
        private System.Windows.Forms.ToolStripButton btnFlipY;
        private System.Windows.Forms.ToolStripButton btnRotate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripDropDownButton btnEditMode;
        private System.Windows.Forms.ToolStripMenuItem btnEditModeTile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    }
}