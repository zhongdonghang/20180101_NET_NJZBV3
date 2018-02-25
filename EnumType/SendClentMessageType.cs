using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    public enum SendClentMessageType
    {
        /// <summary>
        /// 空
        /// </summary>
        None = -1,
        /// <summary>
        /// 关闭
        /// </summary>
        Close = 0,
        /// <summary>
        /// 启动
        /// </summary>
        StartUp = 1,
        /// <summary>
        /// 隐藏
        /// </summary>
        Hide = 2,
        /// <summary>
        /// 显示
        /// </summary>
        Show = 3,
        /// <summary>
        /// 最大化
        /// </summary>
        Maximize = 4,
        /// <summary>
        /// 最小化
        /// </summary>
        Minimize = 5,
        /// <summary>
        /// 最小化到系统托盘
        /// </summary>
        SystemTray = 6,
        /// <summary>
        /// 恢复正常
        /// </summary>
        Normal = 7,
        /// <summary>
        /// 上移
        /// </summary>
        MoveUp = 6,
        /// <summary>
        /// 下移
        /// </summary>
        MoveDown = 9,
        /// <summary>
        /// 左移
        /// </summary>
        MoveLeft = 10,
        /// <summary>
        /// 右移
        /// </summary>
        MoveRight = 11,
        /// <summary>
        /// 增加尺寸
        /// </summary>
        PlusSize = 12,
        /// <summary>
        /// 减小尺寸
        /// </summary>
        MinusSize = 13,
        /// <summary>
        /// 锁定
        /// </summary>
        OnLock = 14,
        /// <summary>
        /// 刷新
        /// </summary>
        Refresh = 15,
        /// <summary>
        /// 屏幕截图
        /// </summary>
        ScreenShots = 16,
        /// <summary>
        /// 重启座位终端
        /// </summary>
        ReStartUpSeatClient=17,
    }
}
