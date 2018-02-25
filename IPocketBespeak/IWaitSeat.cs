using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;

namespace SeatManage.IPocketBespeak
{
    public interface IWaitSeat
    {
        /// <summary>
        /// 获取可以等待座位的阅览室
        /// </summary>
        /// <param name="school">学校</param>
        /// <returns></returns>
        List<SeatManage.ClassModel.ReadingRoomInfo> GetCanWaitSeatRoomInfo(AMS.Model.AMS_School school);

        /// <summary>
        /// 获取可以被等待的座位。
        /// </summary>
        /// <param name="school">学校</param>
        /// <param name="RoomId">想要等待座位的阅览室Id</param>
        /// <returns></returns>
        List<Seat> GetWaitSeatList(AMS.Model.AMS_School school, string RoomId);
        /// <summary>
        /// 提交等待信息,返回提示消息
        /// </summary>
        /// <param name="bespeakInfo"></param>
        /// <returns></returns>
        string SubmitWaitInfo(AMS.Model.AMS_School school, SeatManage.ClassModel.WaitSeatLogInfo waitInfo);
        /// <summary>
        /// 判断是否可能等待座位
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        bool IsCanWaitSeat(AMS.Model.AMS_School school, string CardNo, string roomNo);
        /// <summary>
        /// 取消等待
        /// </summary>
        /// <param name="waitInfo"></param>
        /// <returns></returns>
        string CancelWait(AMS.Model.AMS_School school, SeatManage.ClassModel.WaitSeatLogInfo waitInfo);
    }
}
