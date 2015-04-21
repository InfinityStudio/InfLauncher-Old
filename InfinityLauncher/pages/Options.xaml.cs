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
            CBStartMode.SelectedIndex = Config.BStartMode ? 0 : 1;
            this.TBPassword.Text = Config.BStartMode ? Config.SPlayerPassword : null;
            this.TBPlayerName.Text = Config.SPlayerName;
            this.SMemory.Maximum = GetTotalMemory() / 1000 / 1000;
            this.SMemory.Value = 1024;
            this.TBPlayerName.TextChanged += TBPlayerName_TextChanged;
        }

        private void TBPlayerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.SPlayerName = TBPlayerName.Text;
        }

        public static UInt64 GetTotalMemory()
        {
            return new Computer().Info.TotalPhysicalMemory;
        }

        private void CBStartMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Config.BStartMode = CBStartMode.SelectedIndex == 1;
            if (Config.BStartMode)
            {
                this.Resources["PasswordBoxVisibility"] = Visibility.Collapsed;
            }
            else
            {
                this.Resources["PasswordBoxVisibility"] = Visibility.Visible;
            }
        }
    }
}
