using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinJK
{
    /// <summary>
    /// 微信消息处理委托
    /// </summary>
    /// <param name="sender">事件发起者</param>
    /// <param name="arge">处理的消息结果，可以根据消息类型把消息强制转换为其子类</param>
    public delegate void WeixinMsgHandle(object sender,WeiXinJK.Model.WeiXinMessageBase arge);
    /// <summary>
    /// 微信事件处理委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arge"></param>
    public delegate void WeixinEventHandle(object sender, WeiXinJK.Model.WeixinEventMsg arge);
    public interface IWeixinService
    {
        /// <summary>
        /// 接收消息事件处理，可以根据消息参数的类型把
        /// </summary>
        event WeixinMsgHandle ReceiveMsgEvent;
        /// <summary>
        /// 菜单点击事件
        /// </summary>
        event WeixinEventHandle MenuClickEvent;
        /// <summary>
        /// 点击连接菜单触发事件。
        /// </summary>
        event WeixinEventHandle LinkMenuClickEvent;
        /// <summary>
        /// 用户关注事件
        /// </summary>
        event WeixinEventHandle SubscribeEvent;
        /// <summary>
        /// 用户取消关注事件
        /// </summary>
        event WeixinEventHandle UnSubscribeEvent;
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        bool CheckSignature(string signature, string timestamp, string nonce);
      
        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="msg"></param>
        void MessageHandle(string msg);

        /// <summary>
        /// 文本消息回复
        /// </summary>
        /// <param name="Txt"></param>
        /// <returns></returns>
        string SendWeiXinMsg(WeiXinJK.Model.WeiXinMessageBase age);
       
    }
}
