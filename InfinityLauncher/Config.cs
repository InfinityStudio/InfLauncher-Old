using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfinityLauncher
{
    class Config
    {
        public static String LauncherPath = AppDomain.CurrentDomain.BaseDirectory + @"\user\DemoUser\launcher\InfinityLauncher.ini";
        public static INIClass FConfig = new INIClass(LauncherPath);
        public static String SPlayerName(String NewPlayerName)
        {
            if((NewPlayerName == null)&&(FConfig.IniReadValue("User", "PlayerName") == ""))
            {
                FConfig.IniWriteValue("User", "PlayerName", "Player");
                return "Player";
            }
            else if(NewPlayerName == null)
            {
                return FConfig.IniReadValue("User", "PlayerName");
            }
            else
            {
                FConfig.IniWriteValue("User", "PlayerName", NewPlayerName);
                return NewPlayerName;
            }
        }
        public static String SPlayerEmail;
        public static String SDisplayName;
        public static String SPlayerPassword(String NewPass)
        {
            if (NewPass == null)
            {
                return FConfig.IniReadValue("User", "Password");
            }
            else
            {
                FConfig.IniWriteValue("User", "Password", NewPass);
                return NewPass;
            }
        }
        public static int IMemory = 1024;
        public static String SJavaPath(String NewJavaPath)
        {
            if((NewJavaPath == null) && (FConfig.IniReadValue("Path", "JavaPath") == ""))
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
                                FConfig.IniWriteValue("Path", "JavaPath", key2.GetValue("InstallLocation", "").ToString() + @"bin\javaw.exe");
                                return key2.GetValue("InstallLocation", "").ToString() + @"bin\javaw.exe";
                            }
                        }
                    }
                    FConfig.IniWriteValue("Path", "JavaPath", "");
                    return string.Empty;
                }
                catch
                {
                    FConfig.IniWriteValue("Path", "JavaPath", "");
                    return string.Empty;
                }
            }
            else if ((NewJavaPath == null))
            {
                return FConfig.IniReadValue("Path", "JavaPath");
            }
            else
            {
                FConfig.IniWriteValue("Path", "JavaPath", NewJavaPath);
                return NewJavaPath;
            }
        }
        public static String SSelectVersion;
        public static Boolean BStartMode = false;
        public static Boolean BUpdateLauncher = true;
        public static Boolean BUpdateModpacks = true;
        public static Boolean BDebug = false;
        public static Boolean BExit = true;
        public static Boolean BOnline = false;
    }
}
