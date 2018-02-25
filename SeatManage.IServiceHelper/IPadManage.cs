using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ServiceHelper
{
    public interface IPadManage
    {
        /// <summary>
        /// 根据账号密码进行管理员登录验证
        /// </summary>
        /// <param name="loginId">登录账号</param>
        /// <param name="password">登录密码</param>
        /// <returns>返回验证结果、用户信息</returns>
        string AdminLogin(string loginId, string password);

        /// <summary>
        /// 根据loginId获取管理的阅览室
        /// </summary>
        /// <param name="loginId">登录账号</param>
        /// <returns>返回登录管理员所管理的阅览室及阅览室信息</returns>
        string GetManagerPotencyReadingRoom(string loginId);

        /// <summary>
        /// 根据阅览室编号获取阅览室座位使用信息
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns>返回阅览室座位使用情况</returns>
        string GetRoomSeats(string roomNum, string seatState);

        /// <summary>
        /// 管理员对座位执行操作（暂离、取消暂离、释放座位、计时、加入黑名单）
        /// </summary>
        /// <param name="seatNo">座位号</param>
        /// <param name="operateType">操作类型</param>
        /// <param name="Operator">操作者类型</param>
        /// <returns></returns>
        string SeatOperation(string seatNo, string operateType, string loginId);
    }
}
