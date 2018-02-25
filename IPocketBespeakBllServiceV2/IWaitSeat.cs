using System.Collections.Generic;
using System.ServiceModel;
using SeatManage.ClassModel;

namespace SeatManage.IPocketBespeakBllServiceV2
{
    public partial interface IPocketBespeakBllService
    {
        /// <summary>
        /// 获取可以等待座位的阅览室
        /// </summary>
        /// <param name="school">学校</param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.ReadingRoomInfo> GetCanWaitSeatRoomInfo();

        /// <summary>
        /// 获取可以被等待的座位。
        /// </summary>
        /// <param name="school">学校</param>
        /// <param name="RoomId">想要等待座位的阅览室Id</param>
        /// <returns></returns>
        [OperationContract]
        List<Seat> GetWaitSeatList(string RoomId);
        /// <summary>
        /// 提交等待信息,返回提示消息
        /// </summary>
        /// <param name="bespeakInfo"></param>
        /// <returns></returns>
        [OperationContract]
        string SubmitWaitInfo(SeatManage.ClassModel.WaitSeatLogInfo waitInfo);
        /// <summary>
        /// 判断师傅可以等待座位
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsCanWaitSeat(string CardNo, string roomNo);
        /// <summary>
        /// 取消等待
        /// </summary>
        /// <param name="waitInfo"></param>
        /// <returns></returns>
        [OperationContract]
        string CancelWait(SeatManage.ClassModel.WaitSeatLogInfo waitInfo);
    }
}
