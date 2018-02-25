using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
namespace SeatManage.IPocketBespeak
{
    public interface IMainFunctionPage_Ex
    {
        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        string ReaderComeBack(AMS.Model.AMS_School school, ReaderInfo reader);
        /// <summary>
        /// 获取座位信息
        /// </summary>
        /// <param name="school"></param>
        /// <param name="readingRoomNo"></param>
        /// <param name="SeatNo"></param>
        /// <returns></returns>
        SeatBookUsingInfo GetSeatUsingInfo(AMS.Model.AMS_School school, string readingRoomNo, string SeatNo);
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="school"></param>
        /// <param name="cardNo"></param>
        /// <param name="seatNum"></param>
        /// <param name="readingRoomNum"></param>
        /// <returns></returns>
        string ChangeSeat(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum);
        /// <summary>
        /// 选择座位
        /// </summary>
        /// <param name="school"></param>
        /// <param name="cardNo"></param>
        /// <param name="seatNum"></param>
        /// <param name="readingRoomNum"></param>
        /// <returns></returns>
        string SelectSeat(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum);
    }
}
