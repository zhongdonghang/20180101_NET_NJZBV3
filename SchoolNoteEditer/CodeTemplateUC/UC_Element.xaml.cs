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

namespace SchoolNoteEditer.UC
{
    /// <summary>
    /// UC_Element.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Element : UserControl
    {
        public UC_Element()
        {
            InitializeComponent();
            viewModel.CanvalPointChange += viewModel_CanvalPointChange;
            this.DataContext = viewModel;
        }
        /// <summary>
        /// 改变坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void viewModel_CanvalPointChange(object sender, EventArgs e)
        {
            Canvas.SetTop(this, viewModel.ElementTop);
            Canvas.SetLeft(this, viewModel.ElementLeft);
        }
        public ViewModel.VM_UC_Element viewModel = new ViewModel.VM_UC_Element();
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Canvas c = VisualTreeHelper.GetParent(this) as Canvas;
            if (c != null)
            {
                c.Children.Remove(this);
            }
            e.Handled = true;
        }
    }
}
