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

namespace SeatConfig.UserCtl
{
    /// <summary>
    /// CtlNode.xaml 的交互逻辑
    /// </summary>
    public partial class CtlNode : UserControl
    {
        public CtlNode()
        {
            InitializeComponent();
            this.DataContext = note;
        }

        private SeatManage.ClassModel.Note note = new SeatManage.ClassModel.Note();
        public SeatManage.ClassModel.Note Note
        {
            get { return note; }
            set { note = value; }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox s = sender as TextBox;
            if (s != null)
            {
                s.Visibility = System.Windows.Visibility.Collapsed;
                tbkNote.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid g = VisualTreeHelper.GetParent(this) as Grid;
            if (g != null)
            {
                g.Children.Remove(this);
            }
            e.Handled = true;
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtNote.Visibility = System.Windows.Visibility.Visible;
            tbkNote.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
