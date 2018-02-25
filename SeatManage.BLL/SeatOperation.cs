using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class SeatOperation
    {
        /// <summary>
        /// 设置暂离
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string SeatShortLeave(string cardNo, string client, string remark)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.SeatShortLeave(cardNo, client, remark);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("座位暂离失败：" + ex.Message);
                return "座位暂离失败" + ex.Message;
            }
         
        }
        /// <summary>
        /// 设置暂离
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string SeatLeave(string cardNo, string client, string remark)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.SeatLeave(cardNo, client, remark);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("释放座位失败：" + ex.Message);
                return "释放座位失败" + ex.Message;
            }
          
        }
    }
}
