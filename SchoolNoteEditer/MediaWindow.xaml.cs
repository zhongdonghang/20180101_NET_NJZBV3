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

namespace SchoolNoteEditer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MediaWindow : Window
    {
        public MediaWindow()
        {
            InitializeComponent();
            
        }


        /// <summary>
        /// 座位编辑器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SeatTool_Click(object sender, RoutedEventArgs e)
        {
            SchoolNoteEditer.SeatWindow seat = new SchoolNoteEditer.SeatWindow();
            seat.Show();
        }
        public void CollapsedUC()
        {
            foreach (FrameworkElement fe in canva_UC.Children)
            {
                fe.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btn_SchoolNote_Click(object sender, RoutedEventArgs e)
        {
            CollapsedUC();
            UC_Note.Visibility = System.Windows.Visibility.Visible;
            UC_Note.GetDate();
        }

        private void btn_UserGuide_Click(object sender, RoutedEventArgs e)
        {
            CollapsedUC();
            UC_Guide.Visibility = System.Windows.Visibility.Visible;
            UC_Guide.GetData();
        }

        private void btn_MediaPlayer_Click(object sender, RoutedEventArgs e)
        {
            CollapsedUC();
            UC_MediaPlayer.Visibility = System.Windows.Visibility.Visible;
            UC_MediaPlayer.GetDate();
        }

        private void btn_TitleAd_Click(object sender, RoutedEventArgs e)
        {
            CollapsedUC();
            UC_TitleAd.Visibility = System.Windows.Visibility.Visible;
            UC_TitleAd.GetDate();
        }

        private void btn_PopImage_Click(object sender, RoutedEventArgs e)
        {
            CollapsedUC();
            UC_PopImage.Visibility = System.Windows.Visibility.Visible;
            UC_PopImage.GetDate();
        }

        private void btn_ReaderImage_Click(object sender, RoutedEventArgs e)
        {
            CollapsedUC();
            UC_ReaderAd.Visibility = System.Windows.Visibility.Visible;
            UC_ReaderAd.GetDate();
        }

        private void btn_Promotion_Click(object sender, RoutedEventArgs e)
        {
            CollapsedUC();
            UC_Promotion.Visibility = System.Windows.Visibility.Visible;
            UC_Promotion.GetDate();
        }

        private void btn_Receipt_Click(object sender, RoutedEventArgs e)
        {
            CollapsedUC();
            UC_Receipt.Visibility = System.Windows.Visibility.Visible;
            UC_Receipt.GetDate();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(App.IsJuneberry)
            {
                return;
            }
            try
            {
                if (!SeatManage.Bll.Registry.MediaReleaseIsAuthorize())
                {
                    btn_MediaPlayer.Visibility = System.Windows.Visibility.Collapsed;
                    btn_PopImage.Visibility = System.Windows.Visibility.Collapsed;
                    btn_Promotion.Visibility = System.Windows.Visibility.Collapsed;
                    btn_ReaderImage.Visibility = System.Windows.Visibility.Collapsed;
                    btn_TitleAd.Visibility = System.Windows.Visibility.Collapsed;
                    btn_Receipt.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            catch
            {
                btn_MediaPlayer.Visibility = System.Windows.Visibility.Collapsed;
                btn_PopImage.Visibility = System.Windows.Visibility.Collapsed;
                btn_Promotion.Visibility = System.Windows.Visibility.Collapsed;
                btn_ReaderImage.Visibility = System.Windows.Visibility.Collapsed;
                btn_TitleAd.Visibility = System.Windows.Visibility.Collapsed;
                btn_Receipt.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
