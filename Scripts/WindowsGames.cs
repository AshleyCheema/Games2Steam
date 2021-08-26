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
using Windows.Management.Deployment;
using System.Security.Principal;

namespace Games2Steam
{
    public class WindowsGames
    {
        public static void GetWindowsGames()
        {
            PackageManager packageManager = new PackageManager();

            var packages = packageManager.FindPackagesForUser(WindowsIdentity.GetCurrent().User.Value);

            packages = packages.Where(p => !p.IsFramework && !p.IsDevelopmentMode && !string.IsNullOrEmpty(p.InstalledLocation.Path));

            foreach (var package in packages)
            {
                Console.WriteLine(package.Id.Name);
            }
        }
    }
}
