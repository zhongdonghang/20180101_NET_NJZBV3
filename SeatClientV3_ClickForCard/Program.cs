using System;
using System.Collections.Generic;
using System.Windows;
using SeatClientV3;
using System.Configuration;
using System.Threading;
using SeatClientV3.WindowObject;

namespace SeatClientV3
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            AppLoadingWindowObject.GetInstance().Window.CheckConfigConnection(false);
            if (AppLoadingWindowObject.GetInstance().Window.viewModel.InitializeState == SeatManage.EnumType.HandleResult.Failed)
            {
                return;
            }
            //KeyboardWindowObject.GetInstance();
            //LeaveWindowObject.GetInstance();
            //PopupWindowsObject.GetInstance();
            //ReaderNoteWindowObject.GetInstance();
            //ReadingRoomWindowObject.GetInstance();
            //RecordTheQueryWindowObject.GetInstance();
            //RoomSeatWindowObject.GetInstance();
            //UsuallySeatWindowObject.GetInstance();
            App app = new App();
            app.InitializeComponent();
            app.Run();

        }
    }
}