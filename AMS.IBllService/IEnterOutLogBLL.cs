using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using AMS.Model;
using AMS.Model.Enum;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService
    {
        /// <summary>
        /// 插入进出记录列表
        /// </summary>
        /// <param name="logList"></param>
        [OperationContract]
        HandleResult AddEnterOutLogList(List<AMS_EnterOutLog> logList);
    }
}
