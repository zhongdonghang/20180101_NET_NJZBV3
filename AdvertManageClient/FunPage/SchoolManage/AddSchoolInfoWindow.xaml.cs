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
    /// SchoolInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SchoolInfoWindow : Window
    {
        public SchoolInfoWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public SchoolInfoWindow(AMS.ViewModel.Enum.HandleType handleType)
        {
            InitializeComponent();
            ViewModelSchoolEdit = new AMS.ViewModel.ViewModelSchoolEditWindow(handleType);
            this.DataContext = this;
        }

        public AMS.ViewModel.ViewModelSchoolEditWindow ViewModelSchoolEdit
        {
            get { return (AMS.ViewModel.ViewModelSchoolEditWindow)GetValue(ViewModelSchoolEditProperty); }
            set { SetValue(ViewModelSchoolEditProperty, value); }
        }


        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return (bool)GetValue(IsSuccessProperty); }
            set { SetValue(IsSuccessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSuccess.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSuccessProperty =
            DependencyProperty.Register("IsSuccess", typeof(bool), typeof(SchoolInfoWindow), new UIPropertyMetadata(null));



        // Using a DependencyProperty as the backing store for ViewModelSchoolEdit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelSchoolEditProperty =
            DependencyProperty.Register("ViewModelSchoolEdit", typeof(AMS.ViewModel.ViewModelSchoolEditWindow), typeof(SchoolInfoWindow), new UIPropertyMetadata(null));

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            IsSuccess = ViewModelSchoolEdit.Submit();
            if (IsSuccess)
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "操作成功！");
                mbw.ShowDialog();
                this.Close();
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void DeleteSchoolInfo()
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "该操作会删除该学校下的校区信息以及设备信息，您确定要删除该学校吗？");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                if(ViewModelSchoolEdit.Submit())
                {
                    mbw = new MessageBoxWindow();
                    mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "删除成功");
                    mbw.ShowDialog();
                }
            }
        }


    }
}
