using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steamworks;
using Microsoft.Win32;
using System.IO;
using VDFParser.Models;
using VDFParser;

namespace Games2Steam
{
    public class SteamScript
    {
        public static readonly string userdata = @"\userdata";
        public static readonly string config = @"\config";
        public static readonly string shortcutFile = @"\shortcuts.vdf";
        public static string finalFilePath;

        public static string GetSteamFolder()
        {
            try
            {
                string steamInstallPath;
                steamInstallPath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam", "InstallPath", null);
                Console.WriteLine(steamInstallPath);

                return steamInstallPath;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static string[] GetDirectories(string steamInstallPath)
        {
            return Directory.GetDirectories(steamInstallPath + userdata);
        }

        public static VDFEntry[] GetShortcutsFile(string filePath)
        {
            string shortcutFileName = filePath + config + shortcutFile;

            if(!Directory.Exists(filePath + config) || !File.Exists(shortcutFile))
            {
                return null;
            }

            return VDFParser.VDFParser.Parse(shortcutFile);
        }

        public static void WriteToShortcutFile(string filePath, VDFEntry[] vdfEntries)
        {
            byte[] vdf = VDFSerializer.Serialize(vdfEntries);

            File.WriteAllBytes(filePath, vdf);
        }
    }
}
