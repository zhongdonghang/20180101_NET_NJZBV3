using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SeatClientLeave
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

            AppSkin appSkin = new AppSkin();
            appSkin.ShowDialog();
            if (appSkin.InitializeState == SeatManage.EnumType.HandleResult.Failed)
            { 
                Application.Exit();
                return;
            }
            else
            {
                Application.Run(new MainForm());
            }
            
        }
    }
}
