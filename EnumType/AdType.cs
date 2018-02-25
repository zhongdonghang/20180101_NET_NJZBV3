using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum AdType
    {
        /// <summary>
        /// 未知类型
        /// </summary>
        None = -1,
        /// <summary>
        /// 播放列表
        /// </summary>
        PlaylistAd = 0,
        /// <summary>
        /// 弹窗广告（图片）
        /// </summary>
        PopAd = 1,
        /// <summary>
        /// 座位凭条广告
        /// </summary>
        PrintReceiptAd = 2,
        /// <summary>
        /// 推广广告（原硬广）
        /// </summary>
        PromotionAd = 3,
        /// <summary>
        /// 读者推广广告
        /// </summary>
        ReaderAd = 4,
        /// <summary>
        /// 学校通知
        /// </summary>
        SchoolNotice = 5,
        /// <summary>
        /// 优惠券
        /// </summary>
        SlipCustomerAd = 6,
        /// <summary>
        /// 冠名（弹窗上部）
        /// </summary>
        TitleAd = 7,
    }
}
