using System.Collections.Generic;
using System.ServiceModel;

namespace SeatManage.IPocketBespeakBllServiceV2
{
    public partial interface IPocketBespeakBllService
    {
        /// <summary>
        /// 获取阅览室列表
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.ReadingRoomInfo> GetReadingRoomUsingUsingState();
        /// <summary>
        /// 获取阅览室座位
        /// </summary>
        /// <param name="school"></param>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.Seat> GetReadingRoomSeatList(string RoomId);
        /// <summary>
        /// 选座
        /// </summary>
        /// <param name="school"></param>
        /// <param name="cardNo"></param>
        /// <param name="seatNum"></param>
        /// <param name="readingRoomNum"></param>
        /// <returns></returns>
        [OperationContract]
        string SubmitSeat(string cardNo, string seatNum, string readingRoomNum);
    }
}
