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

        }

        private void TSDebug_IsCheckedChanged(object sender, EventArgs e)
        {

        }

        private void TSUpdateModpacks_IsCheckedChanged(object sender, EventArgs e)
        {

        }

        private void TSUpdateLauncher_IsCheckedChanged(object sender, EventArgs e)
        {

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

        private void TBPlayerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.SPlayerName(TBPlayerName.Text);
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
            }
            else
            {
                this.Resources["PasswordBoxVisibility"] = Visibility.Visible;
            }
        }
    }
}
