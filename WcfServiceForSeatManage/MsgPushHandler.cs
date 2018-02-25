using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SeatManage.ClassModel;
using SeatManage.EnumType;
//using SeatManage.JsonModel;
using SeatManage.SeatManageComm;

namespace MsgPushCenter
{
    /// <summary>
    /// 消息推送中心
    /// </summary>
    public class MsgPushHandler
    {
        WcfServiceForSeatManage.SeatManageDateService dataService = new WcfServiceForSeatManage.SeatManageDateService();
        SeatManage.ClassModel.MsgPostSet msgPostSet = null;
        static MsgPushHandler handler = null;
        static Object obj = new object();
        private MsgPushHandler()
        {
            msgPostSet = dataService.GetMsgPostSet();
        }
        public static MsgPushHandler GetInstance()
        {
            if (handler == null)
            {
                lock (obj)
                {
                    if (handler == null)
                    {
                        handler = new MsgPushHandler();
                    }
                }
            }
            return handler;
        }
        ///// <summary>
        ///// 推送消息
        ///// </summary>
        ///// <param name="msg"></param>
        //public void PushMsg(Object msgObj)
        //{
        //    try
        //    {
        //        Console.WriteLine("执行消息推送");
        //        SeatNotice msg = msgObj as SeatNotice;
        //        if (msg != null)
        //        {
        //            pushMsg(msg);
        //            return;
        //        }
        //        SeatManage.JsonModel.JM_NotifyEvent notifyEvent = msgObj as SeatManage.JsonModel.JM_NotifyEvent;
        //        if (notifyEvent != null)
        //        {
        //            for (int i = 0; i < msgPostSet.PostItems.Count; i++)
        //            {
        //                if (msgPostSet.PostItems[i].UserName == "juneberry")//只推送自己app上。
        //                {
        //                    HttpRequest.Post(string.Format("{0}?appid={1}", msgPostSet.PostItems[i].PostUrl, msgPostSet.PostItems[i].AppID), SeatManage.SeatManageComm.JSONSerializer.Serialize(notifyEvent));
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("消息推送遇到异常：" + ex.Message);

        //    }
        //}
        /// <summary>
        /// 推送消息（app上的通知栏）
        /// </summary>
        /// <param name="msg"></param>
        //private void pushMsg(SeatNotice msg)
        //{
        //    bool isPush = false;
        //    if (msgPostSet != null)
        //    {
        //        switch (msg.Type)
        //        {
        //            case NoticeType.ManagerSetShortLeaveWarning:
        //                if (msgPostSet.IsPushManagerSetShortLeaveWarning)
        //                { isPush = true; }
        //                break;

        //            case NoticeType.OtherSetShortLeaveWarning:
        //                if (msgPostSet.IsPushOtherSetShortLeaveWarning)
        //                { isPush = true; }
        //                break;

        //            case NoticeType.ShortLeaveTimeEndWarning:
        //                if (msgPostSet.IsPushShortLeaveTimeEndWarning)
        //                { isPush = true; }
        //                break;

        //            case NoticeType.ShortLeaveTimeEndBeforeWarning:
        //                if (msgPostSet.IsPushShortLeaveTimeEndBeforeWarning)
        //                { isPush = true; }
        //                break;

        //            case NoticeType.ManagerFreeSetWarning:
        //                if (msgPostSet.IsPushManagerFreeSetWarning)
        //                { isPush = true; }
        //                break;

        //            case NoticeType.AddBlacklistWarning:
        //                if (msgPostSet.IsPushAddBlacklistWarning)
        //                { isPush = true; }
        //                break;

        //            case NoticeType.DeleteBlacklistWarning:
        //                if (msgPostSet.IsPushDeleteBlacklistWarning)
        //                { isPush = true; }
        //                break;

        //            case NoticeType.SeatUsedTimeEnd:
        //                if (msgPostSet.IsPushSeatUsedTimeEnd)
        //                { isPush = true; }
        //                break;

        //            case NoticeType.SeatUsedTimeEndBefore:
        //                if (msgPostSet.IsPushSeatUsedTimeEndBefore)
        //                { isPush = true; }
        //                break;

        //            case NoticeType.BespeakExpiration:
        //                if (msgPostSet.IsPushBespeakExpiration)
        //                { isPush = true; }
        //                break;
        //            case NoticeType.BespeakExpirationBefore:
        //                if (msgPostSet.IsPushBespeakExpirationBefore)
        //                { isPush = true; }
        //                break;
        //            case NoticeType.RoomSeatCrampedWarning:
        //                if (msgPostSet.IsPushRoomSeatCrampedWarning)
        //                { isPush = true; }
        //                break;
        //            case NoticeType.ViolationWarning:
        //                if (msgPostSet.IsPushViolationWarning)
        //                { isPush = true; }
        //                break;
        //            case NoticeType.WaitSeatFail:
        //                if (msgPostSet.IsPushWaitSeatFail)
        //                { isPush = true; }
        //                break;
        //            case NoticeType.WaitSeatSuccess:
        //                if (msgPostSet.IsPushWaitSeatSuccess)
        //                { isPush = true; }
        //                break;
        //            case NoticeType.RecoverSeat:
        //                if (msgPostSet.IsPushRecoverSeat)
        //                { isPush = true; }
        //                break;
        //        }
        //        if (isPush)
        //        {
        //            for (int i = 0; i < msgPostSet.PostItems.Count; i++)
        //            {
        //                try
        //                {
        //                    SeatManage.JsonModel.JM_SeatNotice notice = new SeatManage.JsonModel.JM_SeatNotice(msg.CardNo, msg.SchoolNum);
        //                    notice.Context = msg.Context;
        //                    notice.Title = NoticeTypeValue.valueOf(msg.Type);
        //                    notice.NotifyType = SeatNotifyType.Msg;
        //                    HttpRequest.Post(string.Format("{0}?appid={1}", msgPostSet.PostItems[i].PostUrl, msgPostSet.PostItems[i].AppID), SeatManage.SeatManageComm.JSONSerializer.Serialize(notice));
        //                }
        //                catch (Exception ex)
        //                {

        //                }
        //            }
        //        }
        //    }
        //}



    }
}
