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
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using MahApps.Metro.Controls;
using KMCCC.Authentication;

namespace InfinityLauncher.pages
{
    /// <summary>
    /// Options.xaml 的交互逻辑
    /// </summary>
    public partial class Options : Page
    {
        public Options()
        {
            InitializeComponent();
            //初始化
            CBStartMode.SelectedIndex = Config.BStartMode ? 0 : 1;
            this.TBJavaPath.Text = Config.SJavaPath(null);
            this.TBPassword.Text = Config.BStartMode ? Config.SPlayerPassword(null) : null;
            this.TBPlayerName.Text = Config.SPlayerName(null);
            this.SMemory.Maximum = GetTotalMemory() / 1000 / 1000;
            this.SMemory.Value = Config.IMemory;
            this.TSUpdateLauncher.IsChecked = Config.BUpdateLauncher;
            this.TSUpdateModpacks.IsChecked = Config.BUpdateModpacks;
            this.TSDebug.IsChecked = Config.BDebug;
            this.TSExit.IsChecked = Config.BExit;
            //实时更新
            this.TBPlayerName.TextChanged += TBPlayerName_TextChanged;
            this.TBPassword.TextChanged += TBPassword_TextChanged;
            this.TBJavaPath.PreviewMouseDown += TBJavaPath_PreviewMouseDown;
            this.TBStartFunction.TextChanged += TBStartFunction_TextChanged;
            this.SMemory.ValueChanged += SMemory_ValueChanged;
            this.TSUpdateLauncher.IsCheckedChanged += TSUpdateLauncher_IsCheckedChanged;
            this.TSUpdateModpacks.IsCheckedChanged += TSUpdateModpacks_IsCheckedChanged;
            this.TSDebug.IsCheckedChanged += TSDebug_IsCheckedChanged;
            this.TSExit.IsCheckedChanged += TSExit_IsCheckedChanged;
            this.BJoin.Click += BJoin_Click;
            //自动登录
            if (!Config.BStartMode)
            {
                Authenticator = new YggdrasilLogin(Config.SPlayerName(null), Config.SPlayerPassword(null), true);
                var Authenticatorif = Authenticator.Do();
                if (Authenticatorif.Error != "验证错误")
                {
                    Config.SDisplayName = Authenticatorif.DisplayName;
                    this.TBPassword.Visibility = Visibility.Collapsed;
                    this.label9.Visibility = Visibility.Collapsed;
                    this.BJoin.Content = "登出";
                    if (CPN != null) CPN(null, null);
                    Config.BOnline = true;
                }
                else
                {
                    MessageBox.Show("验证错误！请检查你的网络或者用户名！");
                }
            }
        }

        public static event EventHandler CPN;
        public static event EventHandler CPE;

        public static IAuthenticator Authenticator = new YggdrasilLogin(Config.SPlayerName(null), Config.SPlayerPassword(null), true);

        private void BJoin_Click(object sender, RoutedEventArgs e)
        {
            if (!Config.BOnline)
            {
                Authenticator= new YggdrasilLogin(Config.SPlayerName(null), Config.SPlayerPassword(null), true);
                var Authenticatorif = Authenticator.Do();
                if (Authenticatorif.Error != "验证错误")
                {
                    Config.SDisplayName = Authenticatorif.DisplayName;
                    this.TBPassword.Visibility = Visibility.Collapsed;
                    this.label9.Visibility = Visibility.Collapsed;
                    this.BJoin.Content = "登出";
                    if (CPN != null) CPN(sender, e);
                    Config.BOnline = true;
                }
                else
                {
                    MessageBox.Show("验证错误！请检查你的网络或者用户名！");
                }
            }
            else
            {
                this.TBPassword.Visibility = Visibility.Visible;
                this.TBPassword.Text = Config.SPlayerPassword(null);
                this.label9.Visibility = Visibility.Visible;
                Config.SDisplayName = "";
                this.BJoin.Content = "登入";
                if (CPN != null) CPN(sender, e);
                Config.BOnline = false;
            }
        }

        private void TBPlayerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.SPlayerName(this.TBPlayerName.Text);
            if ((CPN != null) && (Config.BStartMode)) CPN(sender, e);
            if ((CPE != null) && (!Config.BStartMode))
            {
                Config.SPlayerEmail = Config.SPlayerName(null);
                CPE(sender, e);
            }
        }

        private void TBPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.SPlayerPassword(this.TBPassword.Text);
        }

        private void SMemory_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Config.IMemory = int.Parse(this.TBLMemory.Text);
            Config.FConfig.IniWriteValue("Path", "Memory", this.TBLMemory.Text);
        }

        private void TSExit_IsCheckedChanged(object sender, EventArgs e)
        {
            Config.BExit = (bool)this.TSExit.IsChecked;
            Config.FConfig.IniWriteValue("Launch", "Exit", Config.BExit.ToString());
        }

        private void TSDebug_IsCheckedChanged(object sender, EventArgs e)
        {
            Config.BDebug = (bool)this.TSDebug.IsChecked;
            Config.FConfig.IniWriteValue("Launch", "Debug", Config.BDebug.ToString());
        }

        private void TSUpdateModpacks_IsCheckedChanged(object sender, EventArgs e)
        {
            Config.BUpdateModpacks = (bool)this.TSUpdateModpacks.IsChecked;
            Config.FConfig.IniWriteValue("Launch", "UpdateModpacks", Config.BUpdateModpacks.ToString());
        }

        private void TSUpdateLauncher_IsCheckedChanged(object sender, EventArgs e)
        {
            Config.BUpdateLauncher = (bool)this.TSUpdateLauncher.IsChecked;
            Config.FConfig.IniWriteValue("Launch", "UpdateLauncher", Config.BUpdateLauncher.ToString());
        }

        private void TBStartFunction_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TBJavaPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "请选择javaw.exe的位置",
                DefaultExt = ".exe",
                RestoreDirectory = true
            };
            ofd.ShowDialog();
            this.TBJavaPath.Text = ofd.FileName;
            Config.SJavaPath(ofd.FileName);
        }

        public static UInt64 GetTotalMemory()
        {
            return new Computer().Info.TotalPhysicalMemory;
        }

        private void CBStartMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Config.FConfig.IniWriteValue("User", "StartMode", Config.BStartMode.ToString());
            Config.BStartMode = CBStartMode.SelectedIndex == 1;
            if (Config.BStartMode)
            {
                this.Resources["PasswordBoxVisibility"] = Visibility.Collapsed;
                Config.SPlayerPassword("");
                Config.SPlayerEmail = "";
                if (CPE != null) CPE(sender, e);
                if (CPN != null) CPN(sender, e);
            }
            else
            {
                this.Resources["PasswordBoxVisibility"] = Visibility.Visible;
                if (CPN != null) CPN(sender, e);
                this.TBPassword.Text = Config.SPlayerPassword(null);
            }
        }
    }
}
