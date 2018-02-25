using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using SeatManage.ClassModel;
using System.ServiceModel;
using SeatManage.EnumType;

namespace SeatManage.IWCFService
{ 
    public partial interface ISeatManageService : IExceptionService
    {
        #region 等待记录相关操作
        /// <summary>
        /// 获取等待记录
        /// </summary>
        /// <param name="cardNo">读者学号</param>
        /// <param name="SaetNo">座位号</param>
        /// <param name="enterOutLogNo">进出记录编号</param>
        /// <param name="logStatus">记录状态</param>
        /// <returns></returns>
        [OperationContract]
        List<WaitSeatLogInfo> GetWaitLogList(string cardNo, string enterOutLogId,string begindate,string enddate, List<EnterOutLogType> logType);
        /// <summary>
        /// 根据学号获取读者最后一条等待记录。
        /// 如果房间号为空，则不根据房间号查询
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <returns></returns>
        [OperationContract]
        WaitSeatLogInfo GetListWaitLogByCardNo(string cardNo,string roomNum); 
        /// <summary>
        /// 增加一条等待记录
        /// </summary>
        /// <param name="model">等待记录</param>
        /// <returns></returns>
        [OperationContract]
        int AddWaitLog(WaitSeatLogInfo model);
        /// <summary>
        /// 修改一条等待记录
        /// </summary>
        /// <param name="model">等待记录</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateWaitLog(WaitSeatLogInfo model);
        #endregion
    }
}
