using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace CitiesSkylinesLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameDirIndex = Array.IndexOf(args, "--gameDir");
            if (gameDirIndex == -1)
            {
                var pathFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Paradox Interactive\launcherpath");
                Directory.CreateDirectory(Path.GetDirectoryName(pathFile));
                File.WriteAllText(pathFile, Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\\n");
                MessageBox.Show("Launcher path set.\n\nStart the game as usual.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var gameDir = args[gameDirIndex + 1];
            var json = new JavaScriptSerializer();
            var settings = json.Deserialize<LauncherSettings>(File.ReadAllText(Path.Combine(gameDir, "launcher-settings.json")));
            var argList = new List<string>(args);
            argList.RemoveRange(gameDirIndex, 2);
            Process.Start(Path.Combine(gameDir, settings.exePath), string.Join(" ", argList));
        }
    }

    public class LauncherSettings
    {
        public int formatVersion { get; set; }
        public string distPlatform { get; set; }
        public string gameId { get; set; }
        public string displayName { get; set; }
        public string exePath { get; set; }
        public string gameDataPath { get; set; }
        public string version { get; set; }
        public string themeFile { get; set; }
        public string browserDlcUrl { get; set; }
        public bool steamWorkshopDisabled { get; set; }
        public bool steamModsMigrationDisabled { get; set; }
    }

}
