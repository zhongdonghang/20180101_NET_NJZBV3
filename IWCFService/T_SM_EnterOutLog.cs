using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;
namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        #region 进出记录的相关操作
        /// <summary>
        /// 根据学号获取有效的进出记录
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        EnterOutLogInfo GetEnterOutLogInfoByCardNo(string cardNo);
        /// <summary>
        /// 根据记录ID查询有效进出记录
        /// </summary>
        /// <param name="enterOutLogID"></param>
        /// <returns></returns>
        [OperationContract]
        EnterOutLogInfo GetEnterOutLogInfoById(int enterOutLogID);
        /// <summary>
        /// 根据座位号获取有效记录
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        [OperationContract]
        EnterOutLogInfo GetEnterOutLogInfoBySeatNum(string seatNo);
        /// <summary>
        /// 根据参数查询进出记录，不需要的直接传空值
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roonNum">阅览室编号</param>
        ///  <param name="seatNo">座位号</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        [OperationContract]
        List<EnterOutLogInfo> GetEnterOutLogs(string cardNo, string roomNum, string seatNo, string beginDate, string endDate);
        /// <summary>
        /// 根据参数模糊查询进出记录，不需要的直接传空值
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roonNum">阅览室编号</param>
        ///  <param name="seatNo">座位号</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        [OperationContract]
        List<EnterOutLogInfo> GetEnterOutLogs_ByFuzzySearch(string cardNo, string roomNum, string seatNo, string beginDate, string endDate);
        /// <summary>
        /// 根据进出记录编号查找对应的进出记录
        /// </summary>
        /// <param name="EnterOutNo">进出记录编号</param>
        /// <param name="logType">记录类型</param>
        /// <param name="top">最后数目</param>
        /// <returns></returns>
        [OperationContract]
        List<EnterOutLogInfo> GetEnterOutLogsByNo(string EnterOutNo, List<EnterOutLogType> logType, int top);
        /// <summary>
        /// 根据记录状态和类型获取进出记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="seatNo">座位号</param>
        /// <param name="logType">记录类型（多选）</param>
        /// <param name="logStatus">记录状态</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        [OperationContract]
        List<EnterOutLogInfo> GetEnterOutLogsByStatus(string cardNo, string roomNum, string seatNo, List<EnterOutLogType> logType, LogStatus logStatus, string beginDate, string endDate);
        /// <summary>
        /// 添加新记录 通过存储过程来执行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newLogId">新记录的Id</param>
        [OperationContract]
        HandleResult AddEnterOutLogInfo(EnterOutLogInfo model, ref int newLogId);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        [OperationContract]
        int DelEnterOutLogInfo(string cardNo, string roomNum, string beginDate, string endDate);

        /// <summary>
        /// 获取阅览室中正在使用的座位 
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        [OperationContract]
        List<EnterOutLogInfo> GetUsingSeatEnterOutLogInfo(string roomNum);
        /// <summary>
        /// 获取读者续时次数
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        [OperationContract]
        int GetContinuedTimeCount(string CardNo);
        /// <summary>
        /// 获取读者可以续时的时间
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        [OperationContract]
        DateTime GetCanContinuedTime(string CardNo);
        /// <summary>
        /// 获取Id大于参数值的进出记录
        /// </summary>
        /// <param name="id">大于的ID</param>
        /// <param name="logcount">查询数目-1为不限制</param>
        /// <returns></returns>
        [OperationContract]
        List<EnterOutLogInfo> GetEnterOutLogInfoGreaterThanId(int id,bool isorder);
        /// <summary>
        /// 根据进出记录编号插入计时时间
        /// </summary>
        /// <param name="enterOutLigNo"></param>
        /// <param name="markTime"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMarkTime(string enterOutLigNo, DateTime markTime);
        /// <summary>
        /// 获取进出记录状态有预约和等待座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        [OperationContract]
        EnterOutLogInfo GetEnterOutLogInfoWithBookWaitBySeatNum(string seatNo);
        /// <summary>
        /// 延长座位使用时间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string DelaySeatUsedTime(ReaderInfo model);
        /// <summary>
        /// 获取使用中的座位
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        [OperationContract]
        List<EnterOutLogInfo> GetRoomUsingSeatEnterOutLogInfo(List<string> roomNum);
        #endregion

    }
}
