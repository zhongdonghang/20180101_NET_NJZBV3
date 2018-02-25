using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using AuthorizeVerify;
using SeatManage.ClassModel;

namespace SeatManage.Bll
{
    public class SystemAuthorizationOperation
    {
        /// <summary>
        /// 获取功能授权信息（Host服务根目录）
        /// </summary>
        /// <returns></returns>
        public static SystemAuthorization GetSystemAuthorization()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetSystemAuthorization();
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("获取功能授权失败：" + ex.Message);
                throw new Exception("获取功能授权失败：" + ex.Message);
                // return new TerminalInfo();
            }
            
        }
        /// <summary>
        /// 保存服务授权文件到本地(Host服务根目录)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SaveSystemAuthorization(SystemAuthorization model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.SaveSystemAuthorization(model);
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("保存授权文件失败：" + ex.Message);
                throw new Exception("保存授权文件失败：" + ex.Message);
            }
            
        }
    }
}
