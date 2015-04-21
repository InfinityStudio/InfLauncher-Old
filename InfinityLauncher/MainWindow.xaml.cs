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
            this.LPlayerName.Content = Config.SPlayerName;
            this.LPlayerEmail.Content = Config.SPlayerEmail;
            try
            {
                System.Drawing.Bitmap bmp = (Bitmap)System.Drawing.Image.FromStream(WebRequest.Create("http://mcuuid.net/face/" + Config.SPlayerName + ".png").GetResponse().GetResponseStream());
                IntPtr hBitmap = bmp.GetHbitmap();
                IGravatar.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch (WebException)
            {

            }
            if ((DateTime.Now.Hour <= 12) && (DateTime.Now.Hour >=6))
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

        }
    }
}
