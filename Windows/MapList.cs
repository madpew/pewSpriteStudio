using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static pewSpriteStudio.Globals.Events;

namespace pewSpriteStudio
{
    public partial class MapList : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public MapList()
        {
            InitializeComponent();
        }

        private void RefreshList()
        {
            listMaps.Items.Clear();

            foreach (var map in TileMap.TileMaps.Values.OrderBy(v => v.Index))
            {
                var lvi = new ListViewItem(map.Name) { Tag = map.Index.ToString(), ToolTipText = map.Description };
                lvi.SubItems.Add($"{map.Width} x {map.Height}");
                listMaps.Items.Add(lvi);
            }
        }

        private void AddScreen(object sender, EventArgs e)
        {
            TileMap.Add(TileMap.Create("UnnamedScreen", 20, 18));
        }

        private void AddMap(object sender, EventArgs e)
        {
            TileMap.Add(TileMap.Create("UnnamedMap", 32, 32));
        }

        private void AddCustom(object sender, EventArgs e)
        {
            var dialog = new MapSizeChooser("Custom Size...");

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TileMap.Add(TileMap.Create("CustomMap", dialog.MapWidth, dialog.MapHeight));
            }
        }

        private void AddHud(object sender, EventArgs e)
        {
            TileMap.Add(TileMap.Create("UnnamedHud", 20, 1));
        }

        private void ResizeMap(object sender, EventArgs e)
        {
            if (listMaps.SelectedItems.Count != 1) return;

            var index = int.Parse((string)listMaps.SelectedItems[0].Tag);
            var map = TileMap.TileMaps[index];

            var dialog = new MapSizeChooser("Resize Map...", map.Width, map.Height);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                map.Resize(dialog.MapWidth, dialog.MapHeight);
            }
        }

        private void CloneMap(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listMaps.SelectedItems)
            {
                var index = int.Parse((string)lvi.Tag);
                TileMap.Clone(index);
            }
        }

        private void RemoveMaps(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete the map?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            foreach (ListViewItem lvi in listMaps.SelectedItems)
            {
                var index = int.Parse((string)lvi.Tag);
                TileMap.Remove(index);
            }
        }

        private void MapList_Load(object sender, EventArgs e)
        {
            Globals.Events.MapsChanged += OnMapsChanged;
        }

        private void OnMapsChanged(object sender, ChangeEventArgs e)
        {
            switch (e.ChangeType)
            {
                case ChangeEventArgs.EventType.Modified:
                case ChangeEventArgs.EventType.Added:
                    {
                        RefreshList();

                        foreach(ListViewItem lvi in listMaps.Items)
                        {
                            var mapIndex = int.Parse(lvi.Tag as string);
                            lvi.Selected = (mapIndex == e.MapIndex);
                        }

                        Globals.MapEditor.MakeVisible();
                        break;
                    }
                case ChangeEventArgs.EventType.Removed:
                    {
                        RefreshList();
                        break;
                    }
            }
        }

        private void listMaps_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Label)) return;

            var mapIndex = int.Parse(listMaps.Items[e.Item].Tag as string);
            var map = TileMap.TileMaps[mapIndex];

            if (map.Name != e.Label)
            {
                map.Name = e.Label;
                Globals.Events.OnMapsChanged(new ChangeEventArgs(){ ChangeType = ChangeEventArgs.EventType.Modified, MapIndex = mapIndex });
            }
        }

        private void listMaps_ItemActivate(object sender, EventArgs e)
        {
            if (listMaps.SelectedItems != null)
            {
                if (listMaps.SelectedItems.Count == 1)
                {
                    var mapIndex = int.Parse(listMaps.SelectedItems[0].Tag as string);
                    Globals.Events.OnMapsChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Selected, MapIndex = mapIndex });
                    Globals.MapEditor.MakeVisible();
                }
            }
        }

        private void listMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listMaps.SelectedItems != null)
            {
                if (listMaps.SelectedItems.Count == 1)
                {
                    var mapIndex = int.Parse(listMaps.SelectedItems[0].Tag as string);
                    Globals.Events.OnMapsChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Selected, MapIndex = mapIndex });
                }
            }
        }
    }
}
