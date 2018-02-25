using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.IPocketBespeak
{
    public interface ISelectSeat
    {
        /// <summary>
        /// 获取阅览室列表
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        List<SeatManage.ClassModel.ReadingRoomInfo> GetReadingRoomUsingUsingState(AMS.Model.AMS_School school);
        /// <summary>
        /// 获取阅览室座位
        /// </summary>
        /// <param name="school"></param>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        List<SeatManage.ClassModel.Seat> GetReadingRoomSeatList(AMS.Model.AMS_School school, string RoomId);
        /// <summary>
        /// 选座
        /// </summary>
        /// <param name="school"></param>
        /// <param name="cardNo"></param>
        /// <param name="seatNum"></param>
        /// <param name="readingRoomNum"></param>
        /// <returns></returns>
        string SubmitSeat(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum);
    }
}
