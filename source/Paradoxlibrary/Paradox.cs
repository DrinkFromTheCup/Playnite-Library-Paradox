using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// Original code (c) JosefNemec
// Source: https://github.com/JosefNemec/PlayniteExtensions/blob/master/source/Libraries/GogLibrary/Gog.cs

namespace Paradoxlibrary
{
    public class Paradox
    {

        public static string ClientExecPath
        {
            get
            {
                var path = InstallationPath;
                // Paradox itself launches its stand-alone launcher through bootstrapper.exe only.
                return string.IsNullOrEmpty(path) ? string.Empty : Path.Combine(path, "Bootstrapper.exe");
            }
        }

        public static bool IsInstalled
        {
            get
            {
                // It'd be nice to have extra check if Bootstrapper.exe itself exists (instead of folder which might be empty) - but I'm not skilled enough yet.
                // Copying as is.
                if (string.IsNullOrEmpty(ClientExecPath) || !File.Exists(ClientExecPath))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static string InstallationPath
        {
            get
            {
                RegistryKey key;
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Paradox Interactive\Paradox Launcher\BootstrapperPath");

                if (key?.GetValueNames().Contains("Path") == true)
                {
                    return key.GetValue("Path").ToString().Replace("\\\\", "\\");
                }          

                return string.Empty;
            }
        }

        public static string Icon => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"icon.png");
    }
}