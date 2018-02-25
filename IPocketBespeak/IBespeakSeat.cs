using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
namespace SeatManage.IPocketBespeak
{
    public interface IBespeakSeatListForm
    {
        /// <summary>
        /// 获取指定日期开放预约的阅览室
        /// </summary>
        /// <param name="school">学校</param>
        /// <param name="bespeatDate">要预约的日期</param>
        /// <returns></returns>
        List<SeatManage.ClassModel.ReadingRoomInfo> GetCanBespeakReaderRoomInfo(AMS.Model.AMS_School school, DateTime bespeakDate);
        /// <summary>
        /// 获取指定日期开放预约的阅览室
        /// </summary>
        /// <param name="school">学校</param>
        /// <param name="bespeatDate">要预约的日期</param>
        /// <returns></returns>
        List<SeatManage.ClassModel.ReadingRoomInfo> GetCanBespeakNowDayRoomInfo(AMS.Model.AMS_School school);
        /// <summary>
        /// 获取指定日期可被预约的座位。
        /// </summary>
        /// <param name="school">学校</param>
        /// <param name="bespeakDate">想要预约的日期</param>
        /// <param name="RoomId">想要预约的阅览室Id</param>
        /// <returns></returns>
        List<Seat> GetBookSeatList(AMS.Model.AMS_School school, DateTime bespeakDate, string RoomId);
        /// <summary>
        /// 获取当天可预约座位列表
        /// </summary>
        /// <param name="school"></param>
        /// <param name="bespeakDate"></param>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        List<Seat> GetNowDayBookSeatList(AMS.Model.AMS_School school, string RoomId);
        /// <summary>
        /// 提交预约信息,返回提示消息
        /// </summary>
        /// <param name="bespeakInfo"></param>
        /// <returns></returns>
        string SubmitBespeakInfo(AMS.Model.AMS_School school, SeatManage.ClassModel.BespeakLogInfo bespeakInfo);
        /// <summary>
        /// 提交当天预约记录
        /// </summary>
        /// <param name="school"></param>
        /// <param name="bespeakInfo"></param>
        /// <returns></returns>
        string SubmitNowDayBespeakInfo(AMS.Model.AMS_School school, SeatManage.ClassModel.BespeakLogInfo bespeakInfo);
        /// <summary>
        /// 获取座位信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="seatNum">座位号</param>
        /// <param name="readingRoomNum">阅览室编号</param>
        /// <returns></returns>
        SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel GetScanCodeSeatInfo(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum);
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="seatNum">要更换的座位号</param>
        /// <param name="readingRoomNum">阅览室编号</param>
        /// <returns></returns>
        string ChangeSeat(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum);
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="school"></param>
        /// <param name="bookNo"></param>
        /// <returns></returns>
        string ConfrimSeat(AMS.Model.AMS_School school, int bookNo);
    }
}
