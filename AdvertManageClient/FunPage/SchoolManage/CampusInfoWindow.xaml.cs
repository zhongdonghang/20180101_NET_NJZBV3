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
using System.Windows.Shapes;

namespace AdvertManageClient.FunPage.SchoolManage
{
    /// <summary>
    /// CampusInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CampusInfoWindow : Window
    {
        public CampusInfoWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return (bool)GetValue(IsSuccessProperty); }
            set { SetValue(IsSuccessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSuccess.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSuccessProperty =
            DependencyProperty.Register("IsSuccess", typeof(bool), typeof(CampusInfoWindow), new UIPropertyMetadata(false));



        /// <summary>
        /// 校区信息ViewModel
        /// </summary>
        public AMS.ViewModel.ViewModelCampusEditWindow ViewModelCampusWindow
        {
            get { return (AMS.ViewModel.ViewModelCampusEditWindow)GetValue(ViewModelCampusWindowProperty); }
            set { SetValue(ViewModelCampusWindowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModelCampusWindow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelCampusWindowProperty =
            DependencyProperty.Register("ViewModelCampusWindow", typeof(AMS.ViewModel.ViewModelCampusEditWindow), typeof(CampusInfoWindow), new UIPropertyMetadata(null));

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            IsSuccess = ViewModelCampusWindow.ButtomSubmit();
            if (IsSuccess)
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "操作成功！");
                mbw.ShowDialog();
                this.Close();
            }
        }

        public void DeleteCampusWindow()
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "该操作会删除该校区下的设备信息，您确定要删除该校区？");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                if (ViewModelCampusWindow.ButtomSubmit())
                {
                    mbw = new MessageBoxWindow();
                    mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "删除成功");
                    mbw.ShowDialog();
                }
            }
            //MessageBoxResult r = MessageBox.Show("该操作会删除该校区下的设备信息，您确定要删除该校区？", "删除", MessageBoxButton.OKCancel);
            //if (r == MessageBoxResult.OK)
            //{
            //    IsSuccess = ViewModelCampusWindow.ButtomSubmit();
            //    if (IsSuccess)
            //    {
            //        MessageBox.Show("操作成功。"); 
            //    }
            //}
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
