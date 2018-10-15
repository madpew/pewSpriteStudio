namespace pewSpriteStudio
{
    partial class TileList : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileList));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewTiles = new System.Windows.Forms.ToolStripButton();
            this.btnCloneTile = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveTile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.imageListTiles = new System.Windows.Forms.ImageList(this.components);
            this.listTiles = new pewSpriteStudio.SortableListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewTiles,
            this.btnCloneTile,
            this.btnRemoveTile,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(224, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNewTiles
            // 
            this.btnNewTiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewTiles.Image = ((System.Drawing.Image)(resources.GetObject("btnNewTiles.Image")));
            this.btnNewTiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewTiles.Name = "btnNewTiles";
            this.btnNewTiles.Size = new System.Drawing.Size(23, 22);
            this.btnNewTiles.Text = "New Tile";
            this.btnNewTiles.Click += new System.EventHandler(this.AddTile);
            // 
            // btnCloneTile
            // 
            this.btnCloneTile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloneTile.Image = ((System.Drawing.Image)(resources.GetObject("btnCloneTile.Image")));
            this.btnCloneTile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloneTile.Name = "btnCloneTile";
            this.btnCloneTile.Size = new System.Drawing.Size(23, 22);
            this.btnCloneTile.Text = "Clone Tile";
            this.btnCloneTile.Click += new System.EventHandler(this.CloneTile);
            // 
            // btnRemoveTile
            // 
            this.btnRemoveTile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveTile.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveTile.Image")));
            this.btnRemoveTile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveTile.Name = "btnRemoveTile";
            this.btnRemoveTile.Size = new System.Drawing.Size(23, 22);
            this.btnRemoveTile.Text = "Remove Tile";
            this.btnRemoveTile.Click += new System.EventHandler(this.RemoveTiles);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // imageListTiles
            // 
            this.imageListTiles.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListTiles.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListTiles.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listTiles
            // 
            this.listTiles.AllowDrop = true;
            this.listTiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listTiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTiles.FullRowSelect = true;
            this.listTiles.GridLines = true;
            this.listTiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listTiles.LabelEdit = true;
            this.listTiles.LargeImageList = this.imageListTiles;
            this.listTiles.Location = new System.Drawing.Point(0, 25);
            this.listTiles.Name = "listTiles";
            this.listTiles.Size = new System.Drawing.Size(224, 316);
            this.listTiles.SmallImageList = this.imageListTiles;
            this.listTiles.TabIndex = 1;
            this.listTiles.UseCompatibleStateImageBehavior = false;
            this.listTiles.View = System.Windows.Forms.View.Details;
            this.listTiles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listTiles_AfterLabelEdit);
            this.listTiles.ItemActivate += new System.EventHandler(this.listTiles_ItemActivate);
            this.listTiles.SelectedIndexChanged += new System.EventHandler(this.listTiles_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Label";
            this.columnHeader1.Width = 124;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Index";
            this.columnHeader2.Width = 78;
            // 
            // TileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 341);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.listTiles);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TileList";
            this.Text = "Tiles";
            this.Load += new System.EventHandler(this.TileList_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnCloneTile;
        private System.Windows.Forms.ToolStripButton btnRemoveTile;
        private System.Windows.Forms.ImageList imageListTiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnNewTiles;
        private SortableListView listTiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}