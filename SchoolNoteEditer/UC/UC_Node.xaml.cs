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
using System.ComponentModel;

namespace SchoolNoteEditer.UC
{
    /// <summary>
    /// CtlNode.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Node : UserControl, INotifyPropertyChanged
    {
        public UC_Node()
        {
            InitializeComponent();
            this.DataContext = Note;

            bgimg.RenderTransformOrigin = new Point(0.5, 0.5);
            RotateTransform rotateTransform = new RotateTransform(0);
            bgimg.RenderTransform = rotateTransform;
        }

        private SeatManage.ClassModel.Note note = new SeatManage.ClassModel.Note();
        public SeatManage.ClassModel.Note Note
        {
            get { return note; }
            set
            {
                note = value;
                if (value.Type != SeatManage.EnumType.OrnamentType.None)
                {
                    bgimg.Source = new BitmapImage(new Uri("/Resources/" + value.Type.ToString() + ".png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    bgimg.Source = new BitmapImage(new Uri("/Resources/blank.png", UriKind.RelativeOrAbsolute)); ;
                }
                Changed("Note");
            }
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
            Canvas g = VisualTreeHelper.GetParent(this) as Canvas;
            if (g != null)
            {
                g.Children.Remove(this);
            }
            e.Handled = true;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                txtNote.Visibility = System.Windows.Visibility.Visible;
                tbkNote.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        
    }
}
