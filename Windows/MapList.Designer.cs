namespace pewSpriteStudio
{
    partial class MapList : WeifenLuo.WinFormsUI.Docking.DockContent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapList));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnNewScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewMap = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCustomMap = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCloneMap = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveMap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnResizeMap = new System.Windows.Forms.ToolStripButton();
            this.imageListMaps = new System.Windows.Forms.ImageList(this.components);
            this.listMaps = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnCloneMap,
            this.btnRemoveMap,
            this.toolStripSeparator1,
            this.btnResizeMap});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(202, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewMap,
            this.btnNewScreen,
            this.toolStripMenuItem1,
            this.btnCustomMap});
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(29, 22);
            this.btnAdd.Text = "Add...";
            // 
            // btnNewScreen
            // 
            this.btnNewScreen.Name = "btnNewScreen";
            this.btnNewScreen.Size = new System.Drawing.Size(152, 22);
            this.btnNewScreen.Text = "Screen (20x18)";
            this.btnNewScreen.Click += new System.EventHandler(this.AddScreen);
            // 
            // btnNewMap
            // 
            this.btnNewMap.Name = "btnNewMap";
            this.btnNewMap.Size = new System.Drawing.Size(152, 22);
            this.btnNewMap.Text = "Map (32x32)";
            this.btnNewMap.Click += new System.EventHandler(this.AddMap);
            // 
            // btnCustomMap
            // 
            this.btnCustomMap.Name = "btnCustomMap";
            this.btnCustomMap.Size = new System.Drawing.Size(152, 22);
            this.btnCustomMap.Text = "Custom...";
            this.btnCustomMap.Click += new System.EventHandler(this.AddCustom);
            // 
            // btnCloneMap
            // 
            this.btnCloneMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloneMap.Image = ((System.Drawing.Image)(resources.GetObject("btnCloneMap.Image")));
            this.btnCloneMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloneMap.Name = "btnCloneMap";
            this.btnCloneMap.Size = new System.Drawing.Size(23, 22);
            this.btnCloneMap.Text = "Clone Map";
            this.btnCloneMap.Click += new System.EventHandler(this.CloneMap);
            // 
            // btnRemoveMap
            // 
            this.btnRemoveMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveMap.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveMap.Image")));
            this.btnRemoveMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveMap.Name = "btnRemoveMap";
            this.btnRemoveMap.Size = new System.Drawing.Size(23, 22);
            this.btnRemoveMap.Text = "Remove Map";
            this.btnRemoveMap.Click += new System.EventHandler(this.RemoveMaps);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnResizeMap
            // 
            this.btnResizeMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnResizeMap.Image = ((System.Drawing.Image)(resources.GetObject("btnResizeMap.Image")));
            this.btnResizeMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResizeMap.Name = "btnResizeMap";
            this.btnResizeMap.Size = new System.Drawing.Size(23, 22);
            this.btnResizeMap.Text = "Resize Map";
            this.btnResizeMap.Click += new System.EventHandler(this.ResizeMap);
            // 
            // imageListMaps
            // 
            this.imageListMaps.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListMaps.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListMaps.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listMaps
            // 
            this.listMaps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMaps.FullRowSelect = true;
            this.listMaps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listMaps.LabelEdit = true;
            this.listMaps.Location = new System.Drawing.Point(0, 25);
            this.listMaps.MultiSelect = false;
            this.listMaps.Name = "listMaps";
            this.listMaps.ShowItemToolTips = true;
            this.listMaps.Size = new System.Drawing.Size(202, 316);
            this.listMaps.TabIndex = 1;
            this.listMaps.UseCompatibleStateImageBehavior = false;
            this.listMaps.View = System.Windows.Forms.View.Details;
            this.listMaps.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listMaps_AfterLabelEdit);
            this.listMaps.ItemActivate += new System.EventHandler(this.listMaps_ItemActivate);
            this.listMaps.SelectedIndexChanged += new System.EventHandler(this.listMaps_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 98;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Format";
            this.columnHeader2.Width = 72;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Hud (20x1)";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.AddHud);
            // 
            // MapList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 341);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.listMaps);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapList";
            this.Text = "Maps";
            this.Load += new System.EventHandler(this.MapList_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnCloneMap;
        private System.Windows.Forms.ToolStripButton btnRemoveMap;
        private System.Windows.Forms.ImageList imageListMaps;
        private System.Windows.Forms.ListView listMaps;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripDropDownButton btnAdd;
        private System.Windows.Forms.ToolStripMenuItem btnNewScreen;
        private System.Windows.Forms.ToolStripMenuItem btnNewMap;
        private System.Windows.Forms.ToolStripMenuItem btnCustomMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnResizeMap;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}