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
using WPF_Seat.Code;

namespace WPF_Seat.MyControl
{
    /// <summary>
    /// NoteElement.xaml 的交互逻辑
    /// </summary>
    public partial class NoteElement : UserControl
    {
        public NoteElement()
        {
            InitializeComponent();
            this.DataContext = this;
            SetBinding(noteElement, BackgroundProperty, "NoteType", new ConvertNoteImage());
        }
        private void SetBinding(FrameworkElement obj, DependencyProperty p, string path, IValueConverter valueConverter)
        {
            Binding b = new Binding();
            b.Source = this;
            b.Converter = valueConverter;
            b.Path = new PropertyPath(path);
            b.Mode = BindingMode.OneWay;
            obj.SetBinding(p, b);
        }
        public string Notes
        {
            get { return (string)GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Notes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotesProperty =
            DependencyProperty.Register("Notes", typeof(string), typeof(NoteElement));

        public SeatManage.EnumType.OrnamentType NoteType
        {
            get { return (SeatManage.EnumType.OrnamentType)GetValue(NoteTypeProperty); }
            set { SetValue(NoteTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeatState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteTypeProperty =
            DependencyProperty.Register("NoteType", typeof(SeatManage.EnumType.OrnamentType), typeof(NoteElement));

    }
}
