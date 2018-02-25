using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.WcfAccessProxy;
using System.ServiceModel;
using SeatManage.SeatManageComm;
namespace SeatManage.Bll
{
    /// <summary>
    /// 获取服务器的时间。
    /// </summary>
    public class ServiceDateTime
    {
        public static DateTime Now
        {
            get
            {
                return DateTime.Now;
                return serviceDatetime();
            }
        }
        /// <summary>
        /// 获取目标服务器时间
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DateTime serviceDateTime(string connectionString)
        {
            IWCFService.ISeatManageService service = SeatManage.WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService(connectionString);
            bool error = false;
            try
            {
                return service.GetServerDateTime();
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write(string.Format("获取服务器时间出错：{0}", ex.Message));
                throw ex;
            }
           
        }
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        private static DateTime serviceDatetime()
        {
            IWCFService.ISeatManageService service = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return service.GetServerDateTime();
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write(string.Format("获取服务器时间出错：{0}", ex.Message));
                //return DateTime.Now;
                throw ex;
            }
            

        }

    }
}
