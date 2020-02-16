using pewSpriteStudio.Exporters;
using pewSpriteStudio.FileFormat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using static pewSpriteStudio.Globals.Events;

namespace pewSpriteStudio
{
    public static class Globals
    {
        public static string ApplicationName = "pewSpriteStudio";

        public static PSSFile CurrentFile;

        public static MainForm MainWindow;
        public static TileEditor TileEditor;
        public static TileList TileList;
        public static MapList MapList;
        public static MapEditor MapEditor;

        #region Events

        public static class Events
        {
            public class ChangeEventArgs : EventArgs
            {
                public enum EventType
                {
                    Added,
                    Removed,
                    Modified,
                    Selected
                }

                public EventType ChangeType { get; set; }
                public Tile Tile { get; set; }
                public int MapIndex { get; set; }
            }

            public static EventHandler<ChangeEventArgs> TilesChanged;
            public static EventHandler<ChangeEventArgs> MapsChanged;

            public static void OnTilesChanged(ChangeEventArgs e)
            {
                if (e.ChangeType != ChangeEventArgs.EventType.Selected) FileChanged = true;
                TilesChanged?.Invoke(null, e);
            }

            public static void OnMapsChanged(ChangeEventArgs e)
            {
                if (e.ChangeType != ChangeEventArgs.EventType.Selected) FileChanged = true;
                MapsChanged?.Invoke(null, e);
            }
        }

        #endregion

        public static void Startup()
        {
            ExportManager.Load();

            TileList = new TileList();
            TileEditor = new TileEditor();
            MapList = new MapList();
            MapEditor = new MapEditor();

            LoadLayout();

            var args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                if (File.Exists(args[1]))
                {
                    FileTransfer.LoadFile(args[1], true, true);
                    Globals.CurrentFilename = args[1];
                    Globals.MainWindow.Text = $"{Path.GetFileName(args[1])} - {ApplicationName}";
                    Globals.FileLoaded = true;
                    Globals.FileChanged = false;
                }
            }
            else
            {
                NewFile();
            }
        }

        #region Layout

        public static void LoadLayout()
        {
            var layoutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pewSpriteStudio.xml");
            if (File.Exists(layoutPath))
            {
                MainWindow.dockPanel1.LoadFromXml(layoutPath, new DeserializeDockContent((dataString) =>
                {

                    if (dataString == typeof(MapList).ToString()) return MapList;
                    if (dataString == typeof(TileList).ToString()) return TileList;

                    if (dataString.StartsWith(typeof(TileEditor).ToString()))
                    {
                        var data = dataString.Split(',');
                        if (data.Length > 1) TileEditor.DrawGrid = bool.Parse(data[1]);
                        if (data.Length > 2) TileEditor.ZoomLevel = int.Parse(data[2]);

                        return TileEditor;
                    }

                    if (dataString.StartsWith(typeof(MapEditor).ToString()))
                    {
                        var data = dataString.Split(',');
                        if (data.Length > 1) MapEditor.DrawGrid = bool.Parse(data[1]);
                        if (data.Length > 2) MapEditor.ZoomLevel = int.Parse(data[2]);
                        return MapEditor;
                    }

                    return null;
                }));
            }
            else
            {
                TileList.Show(MainWindow.dockPanel1, DockState.DockRight);
                MapList.Show(TileList.Pane, DockAlignment.Bottom, 0.5);
                TileEditor.Show(MainWindow.dockPanel1, DockState.DockLeft);
                MapEditor.Show(MainWindow.dockPanel1, DockState.Document);

                SaveLayout();
            }
        }

        public static void SaveLayout()
        {
            var layoutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pewSpriteStudio.xml");
            MainWindow.dockPanel1.SaveAsXml(layoutPath, Encoding.UTF8);
        }

        #endregion

        public static string CurrentFilename { get; set; } = "";
        public static bool FileChanged { get; set; } = false;
        public static bool FileLoaded { get; set; } = false;

        public static void CloseFile()
        {
            while (TileMap.TileMaps.Count > 0)
            {
                TileMap.Remove(TileMap.TileMaps.First().Key);
            }

            while (Tile.Tiles.Count > 0)
            {
                Tile.Remove(Tile.Tiles.First().Key);
            }

            CurrentFilename = "";
            FileLoaded = false;
        }

        public static void NewFile()
        {
            CloseFile();
            Globals.CurrentFile = new PSSFile() { Filename = "Unnamed.pss", Platform = PSSFile.TargetPlatform.Gameboy, Version = 1 };
            FileLoaded = false;
            FileChanged = true;
            Tile.Add();
            CurrentFilename = "Unnamed.pss";
        }

       
    }
}
