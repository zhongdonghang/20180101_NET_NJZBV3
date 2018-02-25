using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 查询服务器时间
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool SendMsg(ClassModel.PushMsgInfo model);
    }
}
