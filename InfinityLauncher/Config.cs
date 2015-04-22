using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace InfinityLauncher
{
    class Config
    {
        public static String SPlayerName = "Name";
        public static String SPlayerEmail = "E-mail";
        public static String SPlayerPassword;
        public static int IMemory;
        public static String SJavaPath(String NewJavaPath)
        {
            if(NewJavaPath == null)
            {
                try
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall", false);
                    foreach (string keyName in key.GetSubKeyNames())
                    {
                        RegistryKey key2 = key.OpenSubKey(keyName, false);
                        if (key2 != null)
                        {
                            string softwareName = key2.GetValue("Contact", "").ToString();
                            if (softwareName == "http://java.com")
                            {
                                return key2.GetValue("InstallLocation", "").ToString() + @"bin\javaw.exe";
                            }
                        }
                    }
                    return string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            }
            else
            {
                return NewJavaPath;
            }
        }
        public static Boolean BStartMode = false;
        public static Boolean BUpdateLauncher = true;
        public static Boolean BUpdateModpacks = true;
        public static Boolean BDebug = false;
        public static Boolean BExit = true;
    }
}
