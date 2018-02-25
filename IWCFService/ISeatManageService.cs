using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    [ServiceContract]
    public partial interface ISeatManageService :  IExceptionService
    {
        /// <summary>
        /// 查询服务器时间
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DateTime GetServerDateTime();  
    }
}
