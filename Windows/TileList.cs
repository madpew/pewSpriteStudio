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
    public partial class TileList : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public TileList()
        {
            InitializeComponent();

            listTiles.ItemsReordered += ListTiles_ItemsReordered;
        }

        private void ListTiles_ItemsReordered(object sender, EventArgs e)
        {
            // need to change all the tile indicies AND update the maps!
            var changes = new Dictionary<byte, byte>();

            listTiles.BeginUpdate();

            for (var i = 0; i < listTiles.Items.Count; i++)
            {
                var tile = Tile.GetById((Guid)listTiles.Items[i].Tag);
                var oldIndex = tile.Index;
                tile.Index = i;
                listTiles.Items[i].SubItems[1].Text = i.ToString();
                changes.Add((byte)oldIndex, (byte)i);
            }

            listTiles.EndUpdate();

            TileMap.FixTiles(changes);

            if (listTiles.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listTiles.SelectedItems)
                {
                    Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Selected, Tile = Tile.GetById((Guid)item.Tag) });
                    break;
                }
            }
            else
            {
                Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Selected });
            }
             
        }

        private void RefreshList()
        {
            listTiles.BeginUpdate();

            listTiles.Items.Clear();
            imageListTiles.Images.Clear();
            listTiles.LargeImageList = imageListTiles;

            foreach (var tile in Tile.Tiles.Values.OrderBy(v => v.Index))
            {
                imageListTiles.Images.Add(tile.Id.ToString(), tile.GetThumbnail());

                var item = new ListViewItem();
                item.ImageKey = tile.Id.ToString();
                item.Text = tile.Label;
                item.Tag = tile.Id;
                item.SubItems.Add(tile.Index.ToString());
                listTiles.Items.Add(item);
            }

            listTiles.EndUpdate();
            //listTiles.Invalidate();
        }

        public void SelectTile(Guid id, bool fireEvent = false)
        {
            foreach(ListViewItem item in listTiles.Items)
            {
                if ((Guid)item.Tag == id)
                {
                    item.Selected = true;
                    item.EnsureVisible();
                    break;
                }
            }
            
            if (fireEvent) Globals.Events.OnTilesChanged(new ChangeEventArgs() { ChangeType = ChangeEventArgs.EventType.Selected, Tile = Tile.GetById(id) });
        }

        private void AddTile(object sender, EventArgs e)
        {
            var index = Tile.Add();
        }

        private void CloneTile(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listTiles.SelectedItems)
            {
                Tile.Clone((Guid)item.Tag);
            }
        }

        private void RemoveTiles(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete the selected tiles?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            foreach (ListViewItem item in listTiles.SelectedItems)
            {
                Tile.Remove((Guid)item.Tag);
                listTiles.Items.Remove(item);
            }
        }

        private void TileList_Load(object sender, EventArgs e)
        {
            Globals.Events.TilesChanged += OnTilesChanged;
        }

        private void OnTilesChanged(object sender, ChangeEventArgs e)
        {
            switch (e.ChangeType)
            {
                case ChangeEventArgs.EventType.Modified:
                    {
                        var tile = Tile.GetById(e.Tile.Id);
                        var image = imageListTiles.Images[tile.Id.ToString()];
                        image.Dispose();
                        imageListTiles.Images.RemoveByKey(tile.Id.ToString());
                        imageListTiles.Images.Add(tile.Id.ToString(), tile.GetThumbnail());

                        break;
                    }
                case ChangeEventArgs.EventType.Added:
                    {
                        RefreshList();
                        Globals.Events.OnTilesChanged(new Globals.Events.ChangeEventArgs() { ChangeType = Globals.Events.ChangeEventArgs.EventType.Selected, Tile = e.Tile });
                        Globals.TileEditor.MakeVisible();
                        break;
                    }
                case ChangeEventArgs.EventType.Removed:
                    {
                        RefreshList();
                        break;
                    }
                case ChangeEventArgs.EventType.Selected:
                    {
                        if (e.Tile == null) return;

                        SelectTile(e.Tile.Id);
                        break;
                    }
            }
        }

        private void listTiles_ItemActivate(object sender, EventArgs e)
        {
            Globals.Events.OnTilesChanged(new Globals.Events.ChangeEventArgs() { ChangeType = Globals.Events.ChangeEventArgs.EventType.Selected, Tile = Tile.GetById((Guid)(listTiles.SelectedItems[0] as ListViewItem).Tag)});
            Globals.TileEditor.MakeVisible();
        }

        private void listTiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTiles.SelectedIndices.Count == 0) return;

            Globals.Events.OnTilesChanged(new Globals.Events.ChangeEventArgs() { ChangeType = Globals.Events.ChangeEventArgs.EventType.Selected, Tile = Tile.GetById((Guid)(listTiles.SelectedItems[0] as ListViewItem).Tag) });
        }

        private void listTiles_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            Tile.GetById((Guid)listTiles.Items[e.Item].Tag).Label = e.Label;
        }
    }
}
