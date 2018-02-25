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
    public partial class MainWindow : Window
    {
        public MainWindow()
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
        /// <summary>
        /// 媒体编辑器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MediaTool_Click(object sender, RoutedEventArgs e)
        {
            MediaWindow mw = new MediaWindow();
            mw.Show();
        }

        private void btn_codeTemp_Click(object sender, RoutedEventArgs e)
        {
            FunPage.CodeTemplateWindow ctw = new FunPage.CodeTemplateWindow();
            ctw.Show();
        }

        private void btn_WordToImage_Click(object sender, RoutedEventArgs e)
        {
            FunPage.WordToImageWindow wtiw = new FunPage.WordToImageWindow();
            wtiw.Show();
        }

    }
}
