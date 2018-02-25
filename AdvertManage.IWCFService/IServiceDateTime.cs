using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.IWCFService
{
    public partial interface IAdvertManageService
    {
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
         [OperationContract]
        DateTime GetServerDateTime();
    }
}
