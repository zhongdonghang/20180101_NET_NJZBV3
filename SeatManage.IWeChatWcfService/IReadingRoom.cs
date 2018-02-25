using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.IWeChatWcfService
{
    public partial interface IWeChatService
    {
        /// <summary>
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetAllRoomInfo();
        /// <summary>
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetCanBespeakRoomInfo(string date);
        /// <summary>
        /// 获取可预约的阅览室
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetCanBespeakRoom();
        /// <summary>
        /// 根据阅览室获取可预约的日期
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [OperationContract]
        string GetBespeakDate(string roomNo);
        /// <summary>
        /// 获取全部阅览室的当前的使用状态（在座人数，是否开馆等）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetAllRoomNowState();
        /// <summary>
        /// 根据阅览室编号获取但个阅览室当前开闭状态
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetSingleRoomOpenState(string roomNo, string datetime);
        /// <summary>
        /// 获取阅览室布局图
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        [OperationContract]
        string GetRoomSeatLayout(string roomNo);
        /// <summary>
        /// 获取阅览室可预约的座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="bespeakTime"></param>
        /// <returns></returns>
        [OperationContract]
        string GetRoomBesapeakState(string roomNo, string bespeakTime);
        /// <summary>
        /// 获取全部图书馆的使用情况
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetLibraryNowState();
    }
}
