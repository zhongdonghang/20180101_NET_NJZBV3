using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LastSeatClient.MyUserControl;
using LastSeatClient.ViewModel;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;

namespace LastSeatClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private ViewModel.VM_W_MainWindow viewModel = new ViewModel.VM_W_MainWindow();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.GetRoomList();
            BindingReadingRoom();
            ShowTimeRun();
        }
        /// <summary>
        /// 绑定阅览室
        /// </summary>
        private void BindingReadingRoom()
        {
            int ucWidth = 245;
            int ucHeight = 175;
            double bgWidth = viewModel.WindowWidth - 20;
            double bgHeight = can_bg.ActualHeight;

            int rowCount = (int)bgHeight / ucHeight;
            int colCount = (int)bgWidth / ucWidth;

            double freeHeight = 0;
            double freeWidth = 0;

            while (true)
            {
                if ((colCount + 1) * 10 > bgWidth - colCount * ucWidth)
                {
                    colCount--;
                }
                else
                {
                    freeWidth = (bgWidth - colCount * ucWidth) / (colCount + 1);
                    break;
                }
            }
            while (true)
            {
                if ((rowCount + 1) * 10 > bgHeight - rowCount * ucHeight)
                {
                    rowCount--;
                }
                else
                {
                    freeHeight = (bgHeight - rowCount * ucHeight) / (rowCount + 1);
                    break;
                }
            }


            List<ViewModel.VM_UC_RoomStatus> statusList = new List<ViewModel.VM_UC_RoomStatus>(viewModel.StateList.Values);
            int oneCanvasCount = rowCount * colCount;
            int bgCanvascount = (statusList.Count / oneCanvasCount) + (statusList.Count % oneCanvasCount == 0 ? 0 : 1);


            for (int k = 0; k < bgCanvascount; k++)
            {
                Canvas canvasBG = new Canvas();
                canvasBG.Width = bgWidth;
                canvasBG.Height = bgHeight;
                for (int i = k * oneCanvasCount; i < statusList.Count && i < oneCanvasCount * (k + 1); i++)
                {
                    MyUserControl.UC_RoomStatus uc_Room = new MyUserControl.UC_RoomStatus(statusList[i]);
                    uc_Room.Height = ucHeight;
                    uc_Room.Width = ucWidth;
                    Canvas.SetLeft(uc_Room, freeWidth + i % colCount * (ucWidth + freeWidth));
                    Canvas.SetTop(uc_Room, freeHeight + (i - (i / oneCanvasCount) * oneCanvasCount) / colCount * (ucHeight + freeHeight));
                    canvasBG.Children.Add(uc_Room);
                }
                if (canvasBG.Children.Count == 0)
                {
                    break;
                }
                Canvas.SetLeft(canvasBG, 0);
                Canvas.SetTop(canvasBG, -bgHeight);
                can_bg.Children.Add(canvasBG);
            }

            UC_LibStatus uc_lib = new UC_LibStatus(viewModel.LibStatus);
            uc_lib.Margin = new Thickness(0,20,20,0);
            uc_lib.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            uc_lib.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            uc_lib.Height = 140;
            uc_lib.Width = 360;
            g_bg.Children.Add(uc_lib);

        }
        protected Storyboard Storyboard;
        protected DoubleAnimation doubleAnimation0;
        protected DoubleAnimation doubleAnimation1;
        private void CanvasMove()
        {
            Canvas oldCan = (Canvas)can_bg.Children[0];
            Storyboard = new Storyboard();

            doubleAnimation0 = new DoubleAnimation(-can_bg.ActualHeight, new Duration(TimeSpan.FromSeconds(0.5)));
            Storyboard.SetTarget(doubleAnimation0, can_bg.Children[0]);
            Storyboard.SetTargetProperty(doubleAnimation0, new PropertyPath("(Canvas.Top)"));
            Storyboard.Children.Add(doubleAnimation0);

            doubleAnimation1 = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.5)));
            Storyboard.SetTarget(doubleAnimation1, can_bg.Children[1]);
            Storyboard.SetTargetProperty(doubleAnimation1, new PropertyPath("(Canvas.Top)"));
            Storyboard.Children.Add(doubleAnimation1);



            if (!Resources.Contains("rectAnimation"))
            {
                Resources.Add("rectAnimation", Storyboard);
            }
            //动画播放
            Storyboard.Begin();
            can_bg.Children.RemoveAt(0);
            Canvas.SetTop(oldCan, -can_bg.ActualHeight);
            Canvas.SetLeft(oldCan, 0);
            can_bg.Children.Add(oldCan);

        }

        private TimeLoop timeGetStatus;
        private Thread showGetStatus;

        /// <summary>
        /// 时间开始
        /// </summary>
        public void ShowTimeRun()
        {
            timeGetStatus = new TimeLoop(10000);
            timeGetStatus.TimeTo += timeDateTimeShow_TimeTo;
            showGetStatus = new Thread(timeGetStatus.TimeStrat);
            showGetStatus.Start();

        }
        //一秒执行
        void timeDateTimeShow_TimeTo(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                CanvasMove();
            }));
        }
    }
}
