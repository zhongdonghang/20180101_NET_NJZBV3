using SeatManage.ClassModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace WCFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 800; i++)
            {
                Thread showTimeThread = new Thread(A);
                showTimeThread.Start();
                Console.WriteLine("启动第" + i + "个线程");
                System.Threading.Thread.Sleep(500);
            }
        }

        public static void A()
        {
            int i = 1;
            System.Threading.Thread.Sleep(5000);
            while (true)
            {
                SeatManage.IWCFService.ISeatManageService seatService = null;
                try
                {
                    seatService = SeatManage.WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService("net.tcp://localhost:8201/");
                    List<ReadingRoomInfo> list = seatService.GetReadingRoomInfo(null);
                    Console.WriteLine(DateTime.Now.ToString()+" 第" + i + "次：" + list.Count());
                   //DateTime list = seatService.GetServerDateTime();
                   //Console.WriteLine("第" + i + "次：" + list.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("错误：" + ex.Message);
                }
                finally
                {
                    if (seatService != null)
                    {
                        ICommunicationObject ICommObjectService = seatService as ICommunicationObject;
                        try
                        {
                            if (ICommObjectService.State == CommunicationState.Faulted)
                            {
                                ICommObjectService.Abort();
                            }
                            else
                            {
                                ICommObjectService.Close();
                            }
                        }
                        catch
                        {
                            ICommObjectService.Abort();
                        }
                        
                    }
                    i++;
                }
            }
        }
    }
}
