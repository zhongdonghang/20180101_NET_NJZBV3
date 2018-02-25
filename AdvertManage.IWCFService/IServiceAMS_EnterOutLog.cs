using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.Model;
using AdvertManage.Model.Enum;
using System.ServiceModel;

namespace AdvertManage.IWCFService
{
    public partial interface IAdvertManageService
    {
        /// <summary>
        /// 插入进出记录列表
        /// </summary>
        /// <param name="logList"></param>
        [OperationContract]
        HandleResult AddEnterOutLogList(List<AMS_EnterOutLog> logList);
     
         
    }
}
