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
using System.Threading;

namespace AdvertManageClient
{
    /// <summary>
    /// ProgressBarWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressBarWindow : Window
    {
        public ProgressBarWindow(AMS.ViewModel.ViewModelProgressBar vm)
        {
            InitializeComponent();
            vm_Progress = vm;
            vm_Progress.ProgressFinish += new AMS.ViewModel.ViewModelProgressBar.ProgressBarClose(vm_Progress_ProgressFinish);
            this.DataContext = vm_Progress;
            //Thread thread = new Thread(new ThreadStart(refreshTime));
            //thread.Start();
        }

        void vm_Progress_ProgressFinish()
        {
            this.Close();
        }
        public AMS.ViewModel.ViewModelProgressBar vm_Progress
        {
            get { return (AMS.ViewModel.ViewModelProgressBar)GetValue(vm_ProgressProperty); }
            set { SetValue(vm_ProgressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for School.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty vm_ProgressProperty =
            DependencyProperty.Register("vm_Progress", typeof(AMS.ViewModel.ViewModelProgressBar), typeof(AMS.ViewModel.ViewModelProgressBar));

        //申明一个代理用于想UI更新时间
        //private delegate void DelegateSetCurrentTime();
        //private void refreshTime()
        //{
        //    //向UI界面更新时钟显示 
        //    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new DelegateSetCurrentTime(setCurrentTime));
        //}
        //private void setCurrentTime()
        //{
            
        //}
    }
}
