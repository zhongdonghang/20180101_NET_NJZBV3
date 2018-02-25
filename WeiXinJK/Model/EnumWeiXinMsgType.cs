using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinJK.Model
{
    public enum EnumWeiXinMsgType
    {
        /// <summary>
        /// 未知
        /// </summary>
        None,
        /// <summary>
        /// 文本消息
        /// </summary>
        Text,
        /// <summary>
        /// 图片消息
        /// </summary>
        Image,
        /// <summary>
        /// 声音消息
        /// </summary>
        Voice,
        /// <summary>
        /// 视频消息
        /// </summary>
        Video,
        /// <summary>
        /// 地理位置
        /// </summary>
        Location,
        /// <summary>
        /// 链接消息
        /// </summary>
        Link,
        /// <summary>
        /// 事件消息
        /// </summary>
        Event,
        /// <summary>
        /// 多客服消息
        /// </summary>
        transfer_customer_service
    }
    public enum EnumWeiXinEvent
    {
        /// <summary>
        /// 未知
        /// </summary>
        None,
        /// <summary>
        /// 关注  
        /// </summary>
        subscribe,
        /// <summary>
        /// 取消关注
        /// </summary>
        unsubscribe,
        /// <summary>
        /// 点击事件
        /// </summary>
        CLICK,
        /// <summary>
        /// 点击事件连接
        /// </summary>
        VIEW
    }


    /// <summary>
    /// 菜单按钮的Key值
    /// </summary>
    public enum EnumMenuKey
    {
        None = -1,
        /// <summary>
        /// 绑定微信Id0
        /// </summary>
        BindWeiXinId = 0,
        /// <summary>
        /// 获取预约记录1
        /// </summary>
        GetBespeakLog = 1,
        /// <summary>
        /// 获取阅览室座位使用状态2
        /// </summary>
        GetRoomUsedState = 2,
        /// <summary>
        /// 设置暂离3
        /// </summary>
        ShortLeave = 3,
        /// <summary>
        /// 释放座位4
        /// </summary>
        FreeSeat = 4,
        /// <summary>
        /// 预约座位5
        /// </summary>
        ReserveSeat = 5,
        /// <summary>
        /// 黑名单6
        /// </summary>
        BlackList = 6,
        /// <summary>
        /// 获取读者座位以及状态7
        /// </summary>
        GetReaderState = 7,
        /// <summary>
        /// 获取规则8
        /// </summary>
        GetRules = 8,
        /// <summary>
        /// 获取我的信息9
        /// </summary>
        GetMyInfo = 9,
        /// <summary>
        /// 预约服务10
        /// </summary>
        ReservationService = 10,
        /// <summary>
        /// 天气11
        /// </summary>
        Weather = 11,
        /// <summary>
        /// 问题反馈12
        /// </summary>
        Feedback = 12,
        /// <summary>
        /// 每周新闻13
        /// </summary>
        Press = 13,
        /// <summary>
        /// 客服14
        /// </summary>
        Service = 14,
        /// <summary>
        /// 取消等待
        /// </summary>
        CancelWait = 15,
        /// <summary>
        /// 取消预约
        /// </summary>
        CancelBesapeak = 16,
        /// <summary>
        /// 获取使用记录
        /// </summary>
        GetUsageLog = 17
    }
    /// <summary>
    /// 消息回复类型枚举
    /// </summary>
    public enum EnumReplyMsgType
    {
        None = -1,
        /// <summary>
        /// 关注回复消息
        /// </summary>
        Subscribe = 0,
        /// <summary>
        /// 非法文字回复。
        /// </summary>
        TextAutoReply = 1,
        /// <summary>
        /// 非法图片回复
        /// </summary>
        ImgAutoReply = 2,
        /// <summary>
        /// 非法语音回复
        /// </summary>
        VoiceAutoReply = 3,
        /// <summary>
        /// 非法视频回复
        /// </summary>
        VideoAutoReply = 4,
        /// <summary>
        /// 非法地理位置回复
        /// </summary>
        LocationAutoReply = 5,
        /// <summary>
        /// 链接消息
        /// </summary>
        LinkAutoReply = 6,
    }
}
