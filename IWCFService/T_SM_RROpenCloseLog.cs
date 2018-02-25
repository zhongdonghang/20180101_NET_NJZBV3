/************************************
 * 作者：王昊天
 * 创建时间：2013-5-22
 * 说明：开闭阅览室计划功能接口
 * 修改人：
 * 修改时间：
 * **********************************/
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
        /// <summary>
        /// 获取阅览室开闭馆计划，条件为null获取全部
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="logstatus">记录状态</param>
        /// <param name="beginDate">记录起始时间</param>
        /// <param name="endDate">记录结束时间</param>
        /// <returns></returns>
        [OperationContract]
        List<ReadingRoomOpenCloseLogInfo> GetReadingRoomOCLog(string roomNum, LogStatus logstatus, string beginDate, string endDate);
        /// <summary>
        /// 增加开闭馆记录
        /// </summary>
        /// <param name="model">开闭馆记录</param>
        /// <returns></returns>
        [OperationContract]
        int AddReadingRoomOClog(ReadingRoomOpenCloseLogInfo model, ref int newid);
    }
}
