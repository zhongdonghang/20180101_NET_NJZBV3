using System;
using System.Collections.Generic;
using System.Windows;
using SeatClientV3;

namespace SeatClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            AppLoadingWindow loadWindow = new AppLoadingWindow();
            loadWindow.ShowDialog();
            if (loadWindow.viewModel.InitializeState == SeatManage.EnumType.HandleResult.Failed)
            {
                return;
            }
            App app = new App();
            app.InitializeComponent();
            app.Run();
            
        }
    }
}