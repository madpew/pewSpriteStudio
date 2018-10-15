using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pewSpriteStudio
{
    public partial class MapSizeChooser : Form
    {
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }

        public MapSizeChooser(string Title, int Width = 1, int Height = 1)
        {
            InitializeComponent();
            this.Text = Title;
            this.numWidth.Value = MapWidth = Width;
            this.numHeight.Value = MapHeight = Height;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MapWidth = Convert.ToInt32(numWidth.Value);
            MapHeight = Convert.ToInt32(numHeight.Value);
        }
    }
}
