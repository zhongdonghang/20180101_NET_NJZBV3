using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    public class MsgPostSet
    {

       private bool _IsPushManagerSetShortLeaveWarning = true;
       private bool _IsPushShortLeaveTimeEndWarning = false;
       private bool _IsPushShortLeaveTimeEndBeforeWarning = true;
       private bool _IsPushManagerFreeSetWarning = true;
       private bool _IsPushAddBlacklistWarning = true;
       private bool _IsPushDeleteBlacklistWarning = false;
       private bool _IsPushSeatUsedTimeEnd = false;
       private bool _IsPushSeatUsedTimeEndBefore = true;
       private bool _IsPushBespeakExpiration = false;
       private bool _IsPushBespeakExpirationBefore = true;
       private bool _IsPushRoomSeatCrampedWarning = true;
       private bool _IsPushViolationWarning = false;
       private bool _IsPushRecoverSeat = false;
       private bool _IsPushWaitSeatSuccess = true;
       private bool _IsPushWaitSeatFail = true;
        /// <summary>
        /// 是否推送等待座位失败的消息
        /// </summary>
        public bool IsPushWaitSeatFail
        {
            get { return _IsPushWaitSeatFail; }
            set { _IsPushWaitSeatFail = value; }
        }

        /// <summary>
        /// 是否推送等待座位成功的消息
        /// </summary>
        public bool IsPushWaitSeatSuccess
        {
            get { return _IsPushWaitSeatSuccess; }
            set { _IsPushWaitSeatSuccess = value; }
        }

        /// <summary>
        /// 是否推送座位恢复消息
        /// </summary> 
        public bool IsPushRecoverSeat
        {
            get { return _IsPushRecoverSeat; }
            set { _IsPushRecoverSeat = value; }
        }
        /// <summary>
        /// 是否推送违规提醒
        /// </summary>
        public bool IsPushViolationWarning
        {
            get { return _IsPushViolationWarning; }
            set { _IsPushViolationWarning = value; }
        }
        /// <summary>
        /// 是否推送管理员设置读者暂离
        /// </summary>
        public bool IsPushManagerSetShortLeaveWarning
        {
            get { return _IsPushManagerSetShortLeaveWarning; }
            set { _IsPushManagerSetShortLeaveWarning = value; }
        }

        bool _IsPushOtherSetShortLeaveWarning = true;
        /// <summary>
        /// 是否推送被其他读者设置暂离
        /// </summary>
        public bool IsPushOtherSetShortLeaveWarning
        {
            get { return _IsPushOtherSetShortLeaveWarning; }
            set { _IsPushOtherSetShortLeaveWarning = value; }
        }

        /// <summary>
        /// 是否推送暂离到时间提醒
        /// </summary>
        public bool IsPushShortLeaveTimeEndWarning
        {
            get { return _IsPushShortLeaveTimeEndWarning; }
            set { _IsPushShortLeaveTimeEndWarning = value; }
        }
        /// <summary>
        /// 是否推送暂离到时间之前提醒
        /// </summary>
        public bool IsPushShortLeaveTimeEndBeforeWarning
        {
            get { return _IsPushShortLeaveTimeEndBeforeWarning; }
            set { _IsPushShortLeaveTimeEndBeforeWarning = value; }
        }

        /// <summary>
        /// 是否推送管理员释放座位提醒
        /// </summary>
        public bool IsPushManagerFreeSetWarning
        {
            get { return _IsPushManagerFreeSetWarning; }
            set { _IsPushManagerFreeSetWarning = value; }
        }
        /// <summary>
        /// 是否推送被加入黑名单提醒
        /// </summary>
        public bool IsPushAddBlacklistWarning
        {
            get { return _IsPushAddBlacklistWarning; }
            set { _IsPushAddBlacklistWarning = value; }
        }
        /// <summary>
        /// 是否推送删除黑名单的提醒
        /// </summary>
        public bool IsPushDeleteBlacklistWarning
        {
            get { return _IsPushDeleteBlacklistWarning; }
            set { _IsPushDeleteBlacklistWarning = value; }
        }
        /// <summary>
        /// 是否推送座位超时提醒
        /// </summary>
        public bool IsPushSeatUsedTimeEnd
        {
            get { return _IsPushSeatUsedTimeEnd; }
            set { _IsPushSeatUsedTimeEnd = value; }
        }

        /// <summary>
        /// 是否推送超时之前提醒
        /// </summary>
        public bool IsPushSeatUsedTimeEndBefore
        {
            get { return _IsPushSeatUsedTimeEndBefore; }
            set { _IsPushSeatUsedTimeEndBefore = value; }
        }
        /// <summary>
        /// 是否推送预约到期
        /// </summary>
        public bool IsPushBespeakExpiration
        {
            get { return _IsPushBespeakExpiration; }
            set { _IsPushBespeakExpiration = value; }
        }
        /// <summary>
        /// 是否推送预约到期之前提醒
        /// </summary>
        public bool IsPushBespeakExpirationBefore
        {
            get { return _IsPushBespeakExpirationBefore; }
            set { _IsPushBespeakExpirationBefore = value; }
        }
        /// <summary>
        /// 是否推送座位紧缺提醒
        /// </summary>
        public bool IsPushRoomSeatCrampedWarning
        {
            get { return _IsPushRoomSeatCrampedWarning; }
            set { _IsPushRoomSeatCrampedWarning = value; }
        }


        List<MsgPostItem> _PostItems = new List<MsgPostItem>();
        /// <summary>
        /// 是否推送消息推送的地址列表
        /// </summary>
        public List<MsgPostItem> PostItems
        {
            get { return _PostItems; }
            set { _PostItems = value; }
        }
        /// <summary>
        /// 对象转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SeatManageComm.JSONSerializer.Serialize(this);
        }

        public static MsgPostSet Parse(string strObject)
        {
            return SeatManageComm.JSONSerializer.Deserialize<MsgPostSet>(strObject);
        }
    }

    public class MsgPostItem
    {
        private string postUrl;
        /// <summary>
        /// 请求的Url
        /// </summary>
        public string PostUrl
        {
            get { return postUrl; }
            set { postUrl = value; }
        }
        private string appID;
        /// <summary>
        /// 数据有效验证的ID
        /// </summary>
        public string AppID
        {
            get { return appID; }
            set { appID = value; }
        }
         private string userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

    }
}
