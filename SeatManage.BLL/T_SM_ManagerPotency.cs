/*****************************************
 * 作者：王昊天
 * 创建时间：2013-6-4
 * 说明：用户权限管理的BLL
 * 修改人：
 * 修改时间：
 * **************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class T_SM_ManagerPotency
    {
        /// <summary>
        /// 根据LoginID获取用户对应的阅览室编号
        /// </summary>
        /// <param name="LoginID"></param>
        /// <returns></returns>
        public static ManagerPotency GetManangePotencyByLoginID(string LoginID)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetManagerPotencyByLoginID(LoginID);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取用户权限失败：" + ex.Message);
                return null;
            }
           
        }
    }
}
