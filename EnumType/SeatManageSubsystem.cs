using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
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
        /// 优惠券图片
        /// </summary>
        SlipCustomer = 6,
        /// <summary>
        /// 座位凭条上的图片
        /// </summary>
        SeatSlip = 7,
        /// <summary>
        /// 共享文件
        /// </summary>
        SharingFile = 8,
        /// <summary>
        /// 共享文件
        /// </summary>
        HardAd = 9,


        /// <summary>
        /// 播放列表
        /// </summary>
        PlaylistAd = 10,
        /// <summary>
        /// 弹窗广告（图片）
        /// </summary>
        PopAd = 11,
        /// <summary>
        /// 座位凭条广告
        /// </summary>
        PrintReceiptAd = 12,
        /// <summary>
        /// 推广广告（原硬广）
        /// </summary>
        PromotionAd = 13,
        /// <summary>
        /// 读者推广广告
        /// </summary>
        ReaderAd = 14,
        /// <summary>
        /// 学校通知
        /// </summary>
        SchoolNotice = 15,
        /// <summary>
        /// 优惠券
        /// </summary>
        SlipCustomerAd = 16,
        /// <summary>
        /// 冠名（弹窗上部）
        /// </summary>
        TitleAd = 17,
        /// <summary>
        /// 使用指南
        /// </summary>
        UserGuide = 18,
        /// <summary>
        /// 进出记录
        /// </summary>
        EnterOutLog = 19,
        /// <summary>
        /// 预约记录
        /// </summary>
        BespeakLog = 20,
        /// <summary>
        /// 违规记录
        /// </summary>
        ViolateDiscipline = 21,
        /// <summary>
        /// 黑名单记录
        /// </summary>
        Blistlist = 22,
        /// <summary>
        /// 读者信息
        /// </summary>
        ReaderInfo = 23,
    }
}
