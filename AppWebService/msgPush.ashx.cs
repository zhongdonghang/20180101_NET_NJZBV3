using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using AMS.Model;
using BaiduPush;

namespace AppWebService
{
    /// <summary>
    /// msgPush 的摘要说明
    /// </summary>
    public class msgPush : IHttpHandler
    {
        HttpContext context = null;
        const string api_key = "sFdanscl9XqB90B44S5ZYUNK";
        const string secret_key = "HBLmHIaVKSqXY01cbnYflEjI3ZX3INUj";
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            context.Response.ContentType = "text/plain";
            if (this.context.Request.HttpMethod == "POST")
            {
                try
                {
                    string json = getPostData();
                    SeatManage.JsonModel.BaseSeatNotify basemsg = SeatManage.SeatManageComm.JSONSerializer.Deserialize<SeatManage.JsonModel.BaseSeatNotify>(json);
                    if (basemsg.NotifyType == SeatManage.JsonModel.SeatNotifyType.Msg)
                    {
                        SeatManage.JsonModel.JM_SeatNotice noticeNodel = SeatManage.SeatManageComm.JSONSerializer.Deserialize<SeatManage.JsonModel.JM_SeatNotice>(json);
                        pushMsg(noticeNodel);
                    }
                    else
                    {
                        SeatManage.JsonModel.JM_NotifyEvent notifyEvent = SeatManage.SeatManageComm.JSONSerializer.Deserialize<SeatManage.JsonModel.JM_NotifyEvent>(json);
                        pushEvent(notifyEvent);
                    }
                }
                catch (Exception ex)
                { SeatManage.SeatManageComm.WriteLog.Write("消息出错：" + ex.Message); }
                finally
                {
                    context.Response.End();
                }
            }
        }

        /// <summary>
        /// 推送事件（穿透消息）
        /// </summary>
        /// <param name="notifyEvent"></param>
        private void pushEvent(SeatManage.JsonModel.JM_NotifyEvent notifyEvent)
        {
            try
            {
                AppUserInfo app_user = AMS.ServiceProxy.App_UserInfoProxy.GetAppUserInfoByCardNoAndSchoolNum(notifyEvent.CardNo, notifyEvent.SchoolNum);
                if (app_user == null)
                {  
                    return;
                }
                if (notifyEvent == null)
                {  
                    return;
                } 
                PushOptions pOpts = new PushOptions(app_user.ChannelId, 0,SeatManage.SeatManageComm.JSONSerializer.Serialize(notifyEvent), null, api_key, getTimestamp(), null, Device_Type.Android);

                BaiduPush.BaiduPush push = new BaiduPush.BaiduPush("POST", secret_key);
                string pushResult = push.PushSingleDevice(pOpts);
              //  SeatManage.SeatManageComm.WriteLog.Write("消息推送结果：" + pushResult);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void pushMsg(SeatManage.JsonModel.JM_SeatNotice notice)
        {
            AppUserInfo app_user = AMS.ServiceProxy.App_UserInfoProxy.GetAppUserInfoByCardNoAndSchoolNum(notice.CardNo, notice.SchoolNum);
            //TODO:根据学号获取channelId。
            BaiduPushNotification notification = new BaiduPushNotification();
            notification.title = notice.Title;
            notification.description = notice.Context;
            notification.notification_builder_id = 0;
            notification.notification_basic_style = 7;
            notification.custom_content = new Custom_content();
            string msg = notification.getJsonString();

            PushOptions pOpts = new PushOptions(app_user.ChannelId, 1, msg, null, api_key, getTimestamp(), null, Device_Type.Android);

            BaiduPush.BaiduPush push = new BaiduPush.BaiduPush("POST", secret_key);
            push.PushSingleDevice(pOpts);


        }
        private uint getTimestamp()
        {
            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            uint unixTime = (uint)ts.TotalSeconds;
            return unixTime;
        }



        private static string getPostData()
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            return Encoding.UTF8.GetString(b);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}