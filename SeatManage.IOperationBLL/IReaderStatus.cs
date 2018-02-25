using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SeatManage.IOperationBLL
{
    partial interface ISeatOperationService : IExceptionService
    {
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        [OperationContract]
        string GetReaderInfo(string studentNo);
        /// <summary>
        /// 获取用户当前状态
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        [OperationContract]
        string GetReaderNowStatus(string studentNo);
        /// <summary>
        /// 验证是否允许进入阅览室
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        [OperationContract]
        string CheckEnterRoom(string studentNo, string roomNo);
        
        [OperationContract]
        string CheckReaderOperation(string studentNo,EnumType.EnterOutLogType operationType);
    }
}
