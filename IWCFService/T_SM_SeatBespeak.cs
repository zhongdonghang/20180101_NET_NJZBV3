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

        #region 预约信息的相关操作
        /// <summary>
        /// 查询某天有效的预约记录,时间为空，则查询所有有效的预约记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        List<BespeakLogInfo> GetBespeakLogInfo(string cardNo, DateTime date);
        /// <summary>
        /// 根据座位号查询有效的预约记录
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        List<BespeakLogInfo> GetBespeakLogInfoBySeatNo(string seatNo, DateTime date);

        /// <summary>
        /// 根据Id获取预约记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        BespeakLogInfo GetBespeaklogById(int id);
        /// <summary>
        /// 获取当天已经被预约的座位布局图
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        SeatLayout GetBeseakSeatLayout(string roomNum, DateTime date);
        /// <summary>
        /// 根据阅览室编号获取提供预约的座位布局图
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        [OperationContract]
        SeatLayout GetBeseakSeatSettingLayout(string roomNum);
        /// <summary>
        /// 获取阅览室中座位预约状态
        /// </summary>
        /// <param name="roomNums"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, ReadingRoomSeatBespeakState> GetRoomBespeakSeatState(List<string> roomNums, DateTime date);
        /// <summary>
        /// 获取全部的记录，为空就是不设条件查询
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="date">查询当前日期的记录</param>
        /// <param name="status">预约状态</param>
        /// <returns></returns>
        [OperationContract]
        List<BespeakLogInfo> GetBespeakLogs(string cardNo, string roomNum, DateTime endDate, int spanDays, List<BookingStatus> status);
        /// <summary>
        /// 通过学号模糊查询获取全部的记录，为空就是不设条件查询
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="date">查询当前日期的记录</param>
        /// <param name="status">预约状态</param>
        /// <returns></returns>
        [OperationContract]
        List<BespeakLogInfo> GetBespeakLogs_ByFuzzySearch(string cardNo, string roomNum, DateTime endDate, int spanDays, List<BookingStatus> status);
        /// <summary>
        /// 获取所有预约记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        [OperationContract]
        List<BespeakLogInfo> GetBespeakLogInfos(string cardNo, string roomNum);
        /// <summary>
        /// 添加新的预约记录
        /// </summary>
        /// <param name="bespeakLog"></param>
        /// <returns></returns>
        [OperationContract]
        HandleResult AddBespeakLogInfo(BespeakLogInfo bespeakLog);
        /// <summary>
        /// 删除预约记录
        /// </summary>
        /// <param name="bespeakLog"></param>
        /// <returns></returns>
        [OperationContract]
        int UpdateBespeakLogInfo(BespeakLogInfo bespeakLog);
        /// <summary>
        /// 预约记录查询
        /// </summary>
        /// <param name="roomNum"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        List<BespeakLogInfo> GetBespeakLogInfoByRoomsNum(List<string> roomNum, DateTime date);
        /// <summary>
        /// 获取日期前未签到的记录
        /// </summary>
        /// <param name="roomNum"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        List<BespeakLogInfo> GetNotCheckedBespeakLogInfo(List<string> roomNum, DateTime date);
        #endregion
    }
}
