/************************************************
 * 作者：王昊天
 * 创建日期：2013-6-4
 * 说明：管理员阅览室权限操作
 * 修改人：
 * 修改时间：
 * **********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        #region 管理员阅览室权限
        /// <summary>
        /// 根据用户获取对应权限的阅览室
        /// </summary>
        /// <param name="LoginID"></param>
        /// <returns></returns>
        [OperationContract]
        ManagerPotency GetManagerPotencyByLoginID(string LoginID);
        /// <summary>
        /// 对用户权限的修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateManagerPotency(ManagerPotency model);
        #endregion
    }
}
