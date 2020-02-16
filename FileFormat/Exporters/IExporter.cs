using pewSpriteStudio.FileFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GBSpriteStudio.FileFormat.Exporters
{
    public interface IExporter
    {
        string GetName();
        bool CanExport(PSSFile file);
        void Export(PSSFile file);
    }
}
