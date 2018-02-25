using System.Collections.Generic;
using SeatManage.AppJsonModel;

namespace WeiXinService
{
    public partial interface IWeiXinService
    {
        /// <summary>
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        List<AJM_ReadingRoom> GetAllRoomInfo(string schoolNo);

        /// <summary>
        /// 获取全部阅览室的当前的使用状态（在座人数，是否开馆等）
        /// </summary>
        /// <returns></returns>
        List<AJM_ReadingRoomState> GetAllRoomNowState(string schoolNo);

        /// <summary>
        /// 获取单个阅览室开闭状态
        /// </summary>
        /// <param name="roomNo"></param>
        /// <param name="datetime"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        AJM_ReadingRoomState GetSingleRoomOpenState(string roomNo, string datetime, string schoolNo);

        /// <summary>
        /// 获取阅览室布局图
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        AJM_SeatLayout GetRoomSeatLayout(string roomNo, string schoolNo);

        /// <summary>
        /// 获取阅览室可预约的座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="bespeakTime"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        List<AJM_BespeakSeat> GetRoomBesapeakSeat(string roomNo, string bespeakTime, string schoolNo);
        /// <summary>
        /// 获取可以预约的阅览室列表
        /// </summary>
        /// <param name="date"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        List<AJM_ReadingRoom> GetBesapeakRoomList(string date, string schoolNo);
    }
}
