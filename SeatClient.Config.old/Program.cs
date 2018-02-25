using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SeatManage.SeatClient.Config;

namespace SeatClient.Config
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SeatManageConfigTool());
            //AppSkin app = new AppSkin();
            //app.ShowDialog();
            //if (app.CheckResult)
            //{
            //    Application.Run(new Form1());

            //}
            //else
            //{
            //    Application.Exit();
            //}
        }
    }
}
