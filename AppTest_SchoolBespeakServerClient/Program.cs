using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.SeatTcpServer;
using SMS.BespeakServerProxy;
using System.Drawing;
using System.Drawing.Imaging;

namespace AppTest_SchoolBespeakServerClient
{
    class Program
    { 
        static SeatManage.MobileAppService.MobileAppService proxy ;
        static void Main(string[] args)
        {  
            while (true)
            {
                try
                {
                    proxy = new SeatManage.MobileAppService.MobileAppService();
                    proxy.Start();
                    Console.Read();
                     
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
