using GBSpriteStudio.FileFormat.Exporters;
using pewSpriteStudio.FileFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pewSpriteStudio.Exporters
{
    public static class ExportManager
    {
        public static Dictionary<string, IExporter> loadedExporters = new Dictionary<string, IExporter>();

        public static void Load()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(IExporter).IsAssignableFrom(p));

            foreach (var type in types)
            {
                if (type.IsInterface) continue;

                var instance = (IExporter)Activator.CreateInstance(type);
                var exporterName = instance.GetName();

                if (loadedExporters.ContainsKey(exporterName))
                {
                    MessageBox.Show($"An exporter with the name '{exporterName}' already exists.","Exporter skipped.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                loadedExporters.Add(exporterName, instance);
            }
        }
    }
}
