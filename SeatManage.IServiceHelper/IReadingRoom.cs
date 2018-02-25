using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ServiceHelper
{
    public interface IReadingRoom 
    {
        /// <summary>
        /// 获取所有阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        string GetAllReadingRoomBaseInfo();
        /// <summary>
        /// 获取所有阅览室座位使用信息（使用数/座位总数）
        /// </summary>
        /// <returns></returns>
        string GetAllRoomSeatUsedInfo();
        /// <summary>
        /// 根据编号获取阅览室的设置信息
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        string GetReadingRoomSetInfoByRoomNum(string roomNum);
        /// <summary>
        /// 根据阅览室编号获取阅览室的座位信息
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        string GetSeatsLayoutByRoomNum(string roomNum);
        /// <summary>
        /// 根据阅览室编号获取当前座位使用情况的布局图（包括预约）。
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        string GetSeatsUsedInfoByRoomNum(string roomNum);
        /// <summary>
        /// 获取可被预约的座位布局设置
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        //string GetCanBespeakSeatsLayout(string roomNum);
        /// <summary>
        /// 获取座位预约信息
        /// </summary>
        /// <param name="roomNum"></param>
        ///  <param name="date">日期</param>
        /// <returns></returns>
        string GetSeatsBespeakInfoByRoomNum(string roomNum,string date);
         
 
    }
}
