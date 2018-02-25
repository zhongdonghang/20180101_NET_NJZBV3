using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeiXinJK.Model;

namespace WeiXinJK
{
    public  interface IWeiXinAdvertService
    { 

        /// <summary>
        /// 将微信信息发送给指定用户
        /// </summary>
        /// <param name="weixinID"></param>
        /// <returns></returns>
        string SendTxtMessage(string weixinID,string message);

        /// <summary>
        /// 将图文信息发送给指定用户
        /// </summary>
        /// <param name="weixinID"></param>
        /// <returns></returns>
        string SendArticleMessage(string weixinID, List<AMS.Model.WeiXinAdvertse> article);

        /// <summary>
        /// 发送模版提醒消息
        /// </summary>
        /// <param name="weixinId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        string SendTxtMessage(string jsonMsg);

        /// <summary>
        /// 将图文信息发送给所有微信用户
        /// </summary>
        /// <returns></returns>
        string SendArticleToAll(List<string> article);

        /// <summary>
        /// 将文字信息发送给所有微信用户
        /// </summary>
        /// <returns></returns>
        string SendTxtToALL(string message);

        /// <summary>
        /// 获取accesstoken以发送主动信息
        /// </summary>
        /// <returns></returns>
         string GetAccessToken();

        /// <summary>
        /// 根据json建立自定义菜单
        /// </summary>
        /// <returns></returns>
         string BuildUpMenu(string json);
        /// <summary>
        /// 实时天气
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
         string GetCityWeather(long cityid);
    }
}
