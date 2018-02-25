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
        /// <summary>
        /// 根据读者信息发送消息
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="message"></param>
        [OperationContract]
        void SendWeiXinMessage(ReaderInfo reader, string message);
    }
}
