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
            Options.CPN += (us, ue) =>{
                this.LPlayerName.Content = Config.SPlayerName(null);
                try
                {
                    System.Drawing.Bitmap bmp = (Bitmap)System.Drawing.Image.FromStream(WebRequest.Create("http://mcuuid.net/face/" + Config.SPlayerName(null) + ".png").GetResponse().GetResponseStream());
                    IntPtr hBitmap = bmp.GetHbitmap();
                    IGravatar.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                catch (WebException)
                {

                }
            };
            //检测文件夹
            String NowPath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                Directory.CreateDirectory(NowPath + @"\user");
                Directory.CreateDirectory(NowPath + @"\user\DemoUser");
                Directory.CreateDirectory(NowPath + @"\user\DemoUser\launcher");
                Directory.CreateDirectory(NowPath + @"\user\DemoUser\modpacks");
                Directory.CreateDirectory(NowPath + @"\client");
            }
            catch (Exception)
            {

            }
            //读取一些配置文件
            if(Config.FConfig.IniReadValue("User", "StartMode") != "")
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
            this.LPlayerEmail.Content = Config.SPlayerEmail;
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
            if (Config.BStartMode)
            {
                var result = App.Core.Launch(new LaunchOptions
                {
                    Version = App.Core.GetVersion("1.8"),
                    Authenticator = new YggdrasilLogin(Config.SPlayerName(null), Config.SPlayerPassword(null), true),
                    MaxMemory = Config.IMemory,
                    MinMemory = Config.IMemory,
                    Mode = LaunchMode.MCLauncher,
                    Size = new WindowSize { Height = 768, Width = 1280 }
                }, (Action<MinecraftLaunchArguments>)(x => { }));
            }
            else
            {
                var result = App.Core.Launch(new LaunchOptions
                {
                    Version = App.Core.GetVersion("1.8"),
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
