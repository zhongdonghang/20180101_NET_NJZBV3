using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;

namespace SeatManage.Bll
{
    public  class WeiXinMessSender
    {
        /// <summary>
        /// 发送微信信息
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="message"></param>
        public static void SendWeiXinMessage(ReaderInfo reader,string message)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                seatService.SendWeiXinMessage(reader, message);
            }
            catch (Exception ex)
            {
             
                SeatManageComm.WriteLog.Write("发送微信信息失败：" + ex.Message);
                
            }

        }
    }
}
