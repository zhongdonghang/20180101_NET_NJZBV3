using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model.Enum
{
    /// <summary>
    /// 座位管理系统子系统
    /// </summary>
    public enum SeatManageSubsystem
    {
        /// <summary>
        /// 
        /// </summary>
        None = -1,
        /// <summary>
        /// 选座终端
        /// </summary>
        SeatClient = 0,
        /// <summary>
        /// 播放器
        /// </summary>
        Mediaplayer = 1,
        /// <summary>
        /// 后台管理系统
        /// </summary>
        SeatManageWeb = 2,
        /// <summary>
        /// 监控服务
        /// </summary>
        SeatService = 3,
        /// <summary>
        /// 媒体文件
        /// </summary>
        MediaFiles = 4,
        /// <summary>
        /// 截图
        /// </summary>
        Caputre = 5,
        /// <summary>
        /// 凭条图片
        /// </summary>
        SlipCustomer = 6,
        /// <summary>
        /// 打印模板图片
        /// </summary>
        PrintTemplateImage = 7,
        /// <summary>
        /// 共享文件
        /// </summary>
        SharingFile = 8,
    }
}
