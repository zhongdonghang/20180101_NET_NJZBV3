using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    public enum MsgPushType
    {
        /// <summary>
        /// 空值
        /// </summary>
        None = -1,
        /// <summary>
        /// 用户操作
        /// </summary>
        UserOperation = 0,
        /// <summary>
        /// 管理员操作
        /// </summary>
        AdminOperation = 1,
        /// <summary>
        /// 其他用户操作
        /// </summary>
        OtherUser = 2,
        /// <summary>
        /// 违规/进入黑名单通知
        /// </summary>
        EnterVrBlack = 3,
        /// <summary>
        /// 取消违规/离开黑名单
        /// </summary>
        LeaveVrBlack = 4,
        /// <summary>
        /// 超时通知
        /// </summary>
        TimeOut = 5,
        /// <summary>
        /// 到时通知
        /// </summary>
        ToTime = 6,
        /// <summary>
        /// 记录违规
        /// </summary>
        EnterVR=7,
        /// <summary>
        /// 记录黑名单
        /// </summary>
        EnterBlack=8,
    }
}
