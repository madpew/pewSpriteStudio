using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace pewSpriteStudio
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Globals.Startup();
            this.Text = Globals.CurrentFilename + " - pewSpriteStudio";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.SaveLayout();
        }

        private void btnOpenFile_ButtonClick(object sender, EventArgs e)
        {
            if (Globals.FileChanged)
            {
                if (MessageBox.Show("Unsaved changes will be lost. Are you sure ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            }

            var dialog = new OpenFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                DefaultExt = ".gbss",
                Filter = "pewSpriteStudio File (*.gbss)|*.gbss",
                Multiselect = false,
                ValidateNames = true,
                Title = "Open File..."
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Globals.CloseFile();

                FileTransfer.LoadFile(dialog.FileName, true, true);
                Globals.CurrentFilename = dialog.FileName;
                this.Text = dialog.SafeFileName + " - pewSpriteStudio";
                Globals.FileLoaded = true;
                Globals.FileChanged = false;
            }
        }

        private void btnSave_ButtonClick(object sender, EventArgs e)
        {
            if (!Globals.FileLoaded)
            {
                var dialog = new SaveFileDialog()
                {
                    FileName = Globals.CurrentFilename,
                    CheckPathExists = true,
                    AddExtension = true,
                    OverwritePrompt = true,
                    DefaultExt = ".gbss",
                    Filter = "pewSpriteStudio File (*.gbss)|*.gbss",
                    ValidateNames = true,
                    Title = "Save File..."
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FileTransfer.SaveFile(dialog.FileName, true, true);
                    Globals.CurrentFilename = dialog.FileName;
                    this.Text = Globals.CurrentFilename + " - pewSpriteStudio";
                    Globals.FileLoaded = true;
                    Globals.FileChanged = false;
                }
            }
            else
            {
                FileTransfer.SaveFile(Globals.CurrentFilename, true, true);
                Globals.FileLoaded = true;
                Globals.FileChanged = false;
            }
        }

        private void btnSaveAs_ButtonClick(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                FileName = Globals.CurrentFilename,
                CheckPathExists = true,
                AddExtension = true,
                OverwritePrompt = true,
                DefaultExt = ".gbss",
                Filter = "pewSpriteStudio File (*.gbss)|*.gbss",
                ValidateNames = true,
                Title = "Save File..."
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                FileTransfer.SaveFile(dialog.FileName, true, true);
                Globals.CurrentFilename = dialog.FileName;
                this.Text = Globals.CurrentFilename + " - pewSpriteStudio";
                Globals.FileLoaded = true;
                Globals.FileChanged = false;
            }
        }

        private void btnExport_ButtonClick(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                FileName = System.IO.Path.GetFileNameWithoutExtension(Globals.CurrentFilename) + ".h",
                CheckPathExists = true,
                AddExtension = true,
                OverwritePrompt = true,
                DefaultExt = ".h",
                Filter = "C-header (*.h)|*.h",
                ValidateNames = true,
                Title = "Export Header..."
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileTransfer.SaveCHeader(dialog.FileName, true, true);
            }
        }

        private void btnNewFile_Click(object sender, EventArgs e)
        {
            if (Globals.FileChanged)
            {
                if (MessageBox.Show("Unsaved changes will be lost. Are you sure ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            }

            Globals.CloseFile();
            Globals.NewFile();
            this.Text = Globals.CurrentFilename + " - pewSpriteStudio";
            Globals.FileLoaded = false;
            Globals.FileChanged = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (Globals.FileChanged)
            {
                if (MessageBox.Show("Unsaved changes will be lost. Are you sure ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            }

            this.Close();
        }
    }
}
