using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pewSpriteStudio
{
    public enum InsertionMode
    {
        Before,
        After
    }

    public class SortableListView : ListView
    {
        protected int InsertionIndex { get; set; }

        protected InsertionMode InsertionMode { get; set; }

        protected bool IsRowDragInProgress { get; set; }

        public SortableListView()
        {
            this.DoubleBuffered = true;
            this.InsertionIndex = -1;
        }

        private const int WM_PAINT = 0xF;

        [DebuggerStepThrough]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case WM_PAINT:
                    this.DrawInsertionLine();
                    break;
            }
        }

        private void DrawInsertionLine()
        {
            if (this.InsertionIndex != -1)
            {
                int index;

                index = this.InsertionIndex;

                if (index >= 0 && index < this.Items.Count)
                {
                    Rectangle bounds;
                    int x;
                    int y;
                    int width;

                    var item = this.Items[index];
                    if (item == null) return;

                    bounds = this.Items[index].GetBounds(ItemBoundsPortion.Entire);
                    x = 0; // aways fit the line to the client area, regardless of how the user is scrolling
                    y = this.InsertionMode == InsertionMode.Before ? bounds.Top : bounds.Bottom;
                    width = Math.Min(bounds.Width - bounds.Left, this.ClientSize.Width); // again, make sure the full width fits in the client area

                    this.DrawInsertionLine(x, y, width);
                }
            }
        }

        private void DrawInsertionLine(int x1, int y, int width)
        {
            using (Graphics g = this.CreateGraphics())
            {
                Point[] leftArrowHead;
                Point[] rightArrowHead;
                int arrowHeadSize;
                int x2;

                x2 = x1 + width;
                arrowHeadSize = 7;
                leftArrowHead = new[] { new Point(x1, y - (arrowHeadSize / 2)), new Point(x1 + arrowHeadSize, y), new Point(x1, y + (arrowHeadSize / 2)) };
                rightArrowHead = new[] { new Point(x2, y - (arrowHeadSize / 2)), new Point(x2 - arrowHeadSize, y), new Point(x2, y + (arrowHeadSize / 2)) };

                using (Pen pen = new Pen(Color.Black))
                {
                    g.DrawLine(pen, x1, y, x2 - 1, y);
                }

                using (Brush brush = new SolidBrush(Color.Black))
                {
                    g.FillPolygon(brush, leftArrowHead);
                    g.FillPolygon(brush, rightArrowHead);
                }
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            if (this.Items.Count > 1)
            {
                this.IsRowDragInProgress = true;
                this.DoDragDrop(e.Item, DragDropEffects.Move);
            }

            base.OnItemDrag(e);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            if (this.IsRowDragInProgress)
            {
                int insertionIndex;
                InsertionMode insertionMode;
                ListViewItem dropItem;
                Point clientPoint;

                clientPoint = this.PointToClient(new Point(drgevent.X, drgevent.Y));
                dropItem = this.GetItemAt(0, Math.Min(clientPoint.Y, this.Items[this.Items.Count - 1].GetBounds(ItemBoundsPortion.Entire).Bottom - 1));

                if (dropItem != null)
                {
                    Rectangle bounds;

                    bounds = dropItem.GetBounds(ItemBoundsPortion.Entire);
                    insertionIndex = dropItem.Index;
                    insertionMode = clientPoint.Y < bounds.Top + (bounds.Height / 2) ? InsertionMode.Before : InsertionMode.After;

                    drgevent.Effect = DragDropEffects.Move;
                }
                else
                {
                    insertionIndex = -1;
                    insertionMode = this.InsertionMode;

                    drgevent.Effect = DragDropEffects.None;
                }

                if (insertionIndex != this.InsertionIndex || insertionMode != this.InsertionMode)
                {
                    this.InsertionMode = insertionMode;
                    this.InsertionIndex = insertionIndex;
                    this.Invalidate();
                }
            }

            base.OnDragOver(drgevent);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            this.InsertionIndex = -1;
            this.Invalidate();

            base.OnDragLeave(e);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (this.IsRowDragInProgress)
            {
                ListViewItem dropItem;

                dropItem = this.InsertionIndex != -1 ? this.Items[this.InsertionIndex] : null;

                if (dropItem != null)
                {
                    ListViewItem dragItem;
                    int dropIndex;

                    dragItem = (ListViewItem)drgevent.Data.GetData(typeof(ListViewItem));
                    dropIndex = dropItem.Index;

                    if (dragItem.Index < dropIndex)
                    {
                        dropIndex--;
                    }
                    if (this.InsertionMode == InsertionMode.After && dragItem.Index < this.Items.Count - 1)
                    {
                        dropIndex++;
                    }

                    if (dropIndex != dragItem.Index)
                    {
                        var selection = this.SelectedItems;

                        foreach (var item in selection.Cast<ListViewItem>().OrderBy(i => i.Index))
                        {
                            this.Items.Remove(item);
                            this.Items.Insert(dropIndex, item);
                        }

                        ItemsReordered?.Invoke(this, new EventArgs());
                    }
                }

                this.InsertionIndex = -1;
                this.IsRowDragInProgress = false;
                this.Invalidate();
            }

            base.OnDragDrop(drgevent);
        }

        public event EventHandler ItemsReordered;

    }
}
