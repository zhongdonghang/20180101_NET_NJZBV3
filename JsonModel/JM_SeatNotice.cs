using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_SeatNotice : BaseSeatNotify
    {
        public JM_SeatNotice(string cardNo, string schoolNum)
        {
            this.CardNo = cardNo;
            this.SchoolNum = schoolNum;
        }

        public JM_SeatNotice()
        {
        }


        string title;
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        string context;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Context
        {
            get { return context; }
            set { context = value; }
        }
    }

    /// <summary>
    /// 事件通知。推送给app，触发对应的事件。如：读者座位状态改变，app获取最新的读者状态。
    /// </summary>
    public class JM_NotifyEvent : BaseSeatNotify
    { 
        /// <summary>
        /// 座位状态发生变化的事件。
        /// </summary>
        public const string SEATSTATECHANGED = "SeatStateChanged";

        private string _Event;
        /// <summary>
        /// 事件，内容通过NotifyEventEnum.toString()进行获取标识。
        /// </summary>
        public string Event
        {
            get { return _Event; }
            set { _Event = value; }
        }
        private string _obj;
        /// <summary>
        /// 事件参数
        /// </summary>
        public string Obj
        {
            get { return _obj; }
            set { _obj = value; }
        }
        public JM_NotifyEvent(string cardNo, string schoolNum)
        {
            this.CardNo = cardNo;
            this.SchoolNum = schoolNum;
        }

        public JM_NotifyEvent()
        { }

    }

    public class BaseSeatNotify
    {
        private SeatNotifyType _NotifyType;
        string cardNo;
        /// <summary>
        /// 学生学号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        string schoolNum;
        /// <summary>
        /// 学校编号，用于标识用户所在学校
        /// </summary>
        public string SchoolNum
        {
            get { return schoolNum; }
            set { schoolNum = value; }
        }
        public SeatNotifyType NotifyType
        {
            get { return _NotifyType; }
            set { _NotifyType = value; }
        }
    }
    public enum SeatNotifyType
    {
        /// <summary>
        /// 如果是事件，则向app发送穿透消息。
        /// </summary>
        Event,
        Msg
    }

}
