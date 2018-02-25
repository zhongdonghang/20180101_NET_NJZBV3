using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.JsonModel;
using SeatManage.SeatManageComm;

namespace AppTest_SocketTransfersServer
{
    class Program
    {
        static SeatManage.Middleware.DataTransfersMiddleware server;
        static void Main(string[] args)
        {
            try
            {
                JM_SeatNotice notice = new JM_SeatNotice("20150331", "20140714");
                notice.Title =SeatManage.EnumType.NoticeTypeValue.valueOf( SeatManage.EnumType.NoticeType.ManagerSetShortLeaveWarning); 
                  notice.Context = "您的座位被管理员设置为暂时离开。";
                HttpRequest.Post("http://localhost:61812/msgPush.ashx", SeatManage.SeatManageComm.JSONSerializer.Serialize(notice));
                //server = new SeatManage.Middleware.DataTransfersMiddleware();
               // server.Start();
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
