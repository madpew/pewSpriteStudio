using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace pewSpriteStudio
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MessageBox.Show("Development Version\r\nHigh chance of breaking changes in future releases.", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Globals.MainWindow = new MainForm();
            Application.Run(Globals.MainWindow);
        }
    }
}
