using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VDFParser;
using VDFParser.Models;

namespace Games2Steam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            WindowsGames.GetWindowsGames();
        }

        /// <summary>
        /// This should be pressed once the user has decided which games they want to export to Steam
        /// Searches for the folder to find the shortcut.vdf file
        /// if this does not exist then we will create one.
        /// Probably best to add to shortcutArray before creating a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportGames(object sender, RoutedEventArgs e)
        {
            string steamInstall = SteamScript.GetSteamFolder();
            string[] directories = SteamScript.GetDirectories(steamInstall);
            VDFEntry[] shortcutArray = new VDFEntry[0];

            //Search for the config folder and get the shortcuts.vdf
            foreach (string dir in directories)
            {
                shortcutArray = SteamScript.GetShortcutsFile(dir);

                //There are two Shortcuts.vdf present on my PC so it is ideal to stop at the first one that is found
                if(shortcutArray != null)
                {
                    //Store the path for writing too later
                    SteamScript.finalFilePath = dir;
                    break;
                }
            }

            //If the Directory does not exist create it and store the file path for writing too
            if(!Directory.Exists(steamInstall + SteamScript.userdata + SteamScript.config))
            {
                Directory.CreateDirectory(steamInstall + SteamScript.userdata + SteamScript.config);
                SteamScript.finalFilePath = steamInstall + SteamScript.userdata + SteamScript.config + SteamScript.shortcutFile;
            }
        }
    }
}
