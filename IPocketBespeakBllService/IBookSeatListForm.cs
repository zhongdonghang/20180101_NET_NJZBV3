using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;
namespace SeatManage.IPocketBespeakBllService
{
    public partial interface IPocketBespeakBllService
    {
        /// <summary>
        /// 获取指定日期能够被预约的座位
        /// </summary>
        /// <param name="school">学校</param>
        /// <param name="bespeatDate">要预约的日期</param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.ReadingRoomInfo> GetCanBespeakReaderRoomInfo(DateTime bespeakDate);
        /// <summary>
        /// 获取当天可预约的阅览室
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.ReadingRoomInfo> GetCanBespeakNowDayRoomInfo();
        /// <summary>
        /// 获取指定日期可被预约的座位。
        /// </summary>
        /// <param name="school">学校</param>
        /// <param name="bespeakDate">想要预约的日期</param>
        /// <param name="RoomId">想要预约的阅览室Id</param>
        /// <returns></returns>
        [OperationContract]
        List<Seat> GetBookSeatList(DateTime bespeakDate, string RoomId);
        /// <summary>
        /// 获取当前可预约座位列表
        /// </summary>
        /// <param name="bespeakDate"></param>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Seat> GetNowBookSeatList(string RoomId);
        /// <summary>
        /// 提交预约信息,返回确认信息
        /// </summary>
        /// <param name="bespeakInfo"></param>
        /// <returns></returns>
        [OperationContract]
        string SubmitBespeakInfo(SeatManage.ClassModel.BespeakLogInfo bespeakInfo);
        /// <summary>
        /// 提交当天预约记录
        /// </summary>
        /// <param name="bespeakInfo"></param>
        /// <returns></returns>
        [OperationContract]
        string SubmitNowDayBespeakInfo(SeatManage.ClassModel.BespeakLogInfo bespeakInfo);
        /// <summary>
        /// 获取读者扫描二维码的座位信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="seatNum">座位号</param>
        /// <param name="readingRoomNum">阅览室编号</param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel GetScanCodeSeatInfo(string cardNo, string seatNum, string readingRoomNum);
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="seatNum">要更换的座位号</param>
        /// <param name="readingRoomNum">阅览室编号</param>
        /// <returns></returns>
        [OperationContract]
        string ChangeSeat(string cardNo, string seatNum, string readingRoomNum);
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="bookNo"></param>
        /// <returns></returns>
        [OperationContract]
        string ConfigSeat(int bookNo);
        /// <summary>
        /// 获取座位使用信息
        /// </summary>
        /// <param name="seatNum"></param>
        /// <param name="readingRoomNum"></param>
        /// <returns></returns>
        [OperationContract]
        SeatBookUsingInfo GetSeatBookUsingStatus(string seatNum, string readingRoomNum);

    }
}
