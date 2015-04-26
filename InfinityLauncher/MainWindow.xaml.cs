using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Net;
using System.Drawing;
using System.IO;
using KMCCC.Authentication;
using KMCCC.Launcher;
using Version = KMCCC.Launcher.Version;
using InfinityLauncher.pages;

namespace InfinityLauncher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Options.CPN += (us, ue) =>
            {
                this.LPlayerName.Content = Config.SPlayerName(null);
                if (!Config.BStartMode)
                {
                    this.LPlayerName.Content = Config.SDisplayName;
                    try
                    {
                        System.Drawing.Bitmap bmp = (Bitmap)System.Drawing.Image.FromStream(WebRequest.Create("http://mcuuid.net/face/" + Config.SDisplayName + ".png").GetResponse().GetResponseStream());
                        IntPtr hBitmap = bmp.GetHbitmap();
                        IGravatar.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }
                    catch (WebException)
                    {

                    }
                }
            };
            Options.CPE += (us, ue) =>
            {
                this.LPlayerEmail.Content = Config.SPlayerEmail;
            };

            //检测文件夹
            String NowPath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                Directory.CreateDirectory(NowPath + @"\user");
                Directory.CreateDirectory(NowPath + @"\user\DemoUser");
                Directory.CreateDirectory(NowPath + @"\user\DemoUser\launcher");
                Directory.CreateDirectory(NowPath + @"\user\DemoUser\modpacks");
            }
            catch (Exception)
            {

            }

            //遍历文件夹
            DirectoryInfo theFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\user\DemoUser\modpacks\");
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                this.CBVersion.Items.Add(NextFolder.Name);
                FileInfo[] fileInfo = NextFolder.GetFiles();
            }

            //读取一些配置文件
            if (Config.FConfig.IniReadValue("User", "StartMode") != "")
            {
                Config.BStartMode = Boolean.Parse(Config.FConfig.IniReadValue("User", "StartMode"));
            }
            if(Config.FConfig.IniReadValue("Path", "Memory") != "")
            {
                Config.IMemory = int.Parse(Config.FConfig.IniReadValue("Path", "Memory"));
            }
            if(Config.FConfig.IniReadValue("Launch", "UpdateLauncher") != "")
            {
                Config.BUpdateLauncher = Boolean.Parse(Config.FConfig.IniReadValue("Launch", "UpdateLauncher"));
            }
            if (Config.FConfig.IniReadValue("Launch", "UpdateModpacks") != "")
            {
                Config.BUpdateModpacks = Boolean.Parse(Config.FConfig.IniReadValue("Launch", "UpdateModpacks"));
            }
            if (Config.FConfig.IniReadValue("Launch", "Debug") != "")
            {
                Config.BDebug = Boolean.Parse(Config.FConfig.IniReadValue("Launch", "Debug"));
            }
            if (Config.FConfig.IniReadValue("Launch", "Exit") != "")
            {
                Config.BExit = Boolean.Parse(Config.FConfig.IniReadValue("Launch", "Exit"));
            }

            //初始化
            this.LPlayerName.Content = Config.SPlayerName(null);
            if (Config.BStartMode) this.LPlayerEmail.Content = Config.SPlayerName(null); else this.LPlayerEmail.Content = "";
            try
            {
                System.Drawing.Bitmap bmp = (Bitmap)System.Drawing.Image.FromStream(WebRequest.Create("http://mcuuid.net/face/" + Config.SPlayerName(null) + ".png").GetResponse().GetResponseStream());
                IntPtr hBitmap = bmp.GetHbitmap();
                IGravatar.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch (WebException)
            {

            }
            if ((DateTime.Now.Hour <= 12) && (DateTime.Now.Hour >= 6))
            {
                this.LHello.Content = "早上好，" + this.LHello.Content;
            }
            else if ((DateTime.Now.Hour > 12) && (DateTime.Now.Hour <= 6))
            {
                this.LHello.Content = "下午好，" + this.LHello.Content;
            }
            else this.LHello.Content = "晚上好，" + this.LHello.Content;
        }

        private void BStartGame_Click(object sender, RoutedEventArgs e)
        {
            //复制文件（未完成）
            //启动游戏（未完成）
            LauncherCore Core = LauncherCore.Create(
            new LauncherCoreCreationOption(
                gameRootPath: "client" + Config.SSelectVersion + @"\.minecraft",
                javaPath: Config.SJavaPath(null),
                versionLocator: null
            ));
            if (!Config.BStartMode)
            {
                var result = Core.Launch(new LaunchOptions
                {
                    Version = Core.GetVersion("1.8"),
                    Authenticator = Options.Authenticator,
                    MaxMemory = Config.IMemory,
                    MinMemory = Config.IMemory,
                    Mode = LaunchMode.MCLauncher,
                    Size = new WindowSize { Height = 768, Width = 1280 }
                }, (Action<MinecraftLaunchArguments>)(x => { }));
            }
            else
            {
                var result = Core.Launch(new LaunchOptions
                {
                    Version = Core.GetVersion("1.8"),
                    Authenticator = new OfflineAuthenticator(Config.SPlayerName(null)),
                    MaxMemory = Config.IMemory,
                    MinMemory = Config.IMemory,
                    Mode = LaunchMode.MCLauncher,
                    Size = new WindowSize { Height = 768, Width = 1280 }
                }, (Action<MinecraftLaunchArguments>)(x => { }));
            }
        }
    }
}
