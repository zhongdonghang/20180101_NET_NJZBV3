using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class WeiXinBaseMsg
    {
        public WeiXinBaseMsg(string cardNum, string schoolNum)
        {
            this._cardNum = cardNum;
            this._schoolNum = schoolNum;
        }
        string _cardNum;
        string _readerName;
        string _remark;
        string _msgHeader;
        string _schoolNum;

        public string SchoolNum
        {
            get { return _schoolNum; }
            set { _schoolNum = value; }
        }
        /// <summary>
        /// 读者学号
        /// </summary>
        public string CardNum
        {
            get { return _cardNum; }
            set { _cardNum = value; }
        }
        /// <summary>
        /// 读者名字
        /// </summary>
        public string ReaderName
        {
            get { return _readerName; }
            set { _readerName = value; }
        }/// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 消息头
        /// </summary>
        public string MsgHeader
        {
            get { return _msgHeader; }
            set { _msgHeader = value; }
        }
    }
    /// <summary>
    /// 微信选座提醒消息
    /// </summary>
    [Serializable]
    public class WeiXinChooseSeatMsg : WeiXinBaseMsg
    {
        public WeiXinChooseSeatMsg(string cardNum, string schoolNum)
            : base(cardNum, schoolNum)
        { }
        string _seatNum;
        string _chooseTime;
        private string _readingRoomName;
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNum
        {
            get { return _seatNum; }
            set { _seatNum = value; }
        }
        /// <summary>
        /// 选座时间
        /// </summary>
        public string ChooseTime
        {
            get { return _chooseTime; }
            set { _chooseTime = value; }
        }

        /// <summary>
        /// 阅览室名字
        /// </summary>
        public string ReadingRoomName
        {
            get { return _readingRoomName; }
            set { _readingRoomName = value; }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("{0}\n\n", this.MsgHeader);
            str.AppendFormat("地点：{0}\n", ReadingRoomName);
            str.AppendFormat("座位号：{0}\n", SeatNum);
            str.AppendFormat("选座时间：{0}\n\n", ChooseTime);
            str.AppendFormat(Remark);
            return str.ToString();
        }
    }
    /// <summary>
    /// 预约消息
    /// </summary>
    [Serializable]
    public class WeiXinBespeakMsg : WeiXinChooseSeatMsg
    {
        public WeiXinBespeakMsg(string cardNum, string schoolNum)
            : base(cardNum, schoolNum)
        { }
        /// <summary>
        /// 签到时间
        /// </summary>
        public string SigninTimeBegin
        {
            get;
            set;
        }
        /// <summary>
        /// 签到结束时间
        /// </summary>
        public string SigninTimeEnd
        {
            get;
            set;
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("{0}\n\n", this.MsgHeader);
            str.AppendFormat("地点：{0}\n", ReadingRoomName);
            str.AppendFormat("座位号：{0}\n", SeatNum);
            str.AppendFormat("预约时间：{0}\n", ChooseTime);
            str.AppendFormat("确认时间：{0}至{1}\n\n", SigninTimeBegin, SigninTimeEnd);
            str.AppendFormat(Remark);
            return str.ToString();
        }
    }
    /// <summary>
    /// 违规提醒
    /// </summary>
    [Serializable]
    public class WeiXinViolationMsg : WeiXinBaseMsg
    {
        public WeiXinViolationMsg(string cardNum, string schoolNum)
            : base(cardNum, schoolNum)
        { }
        string _chooseTime;
        private string _readingRoomName;
        /// <summary>
        /// 阅览室名字
        /// </summary>
        public string ReadingRoomName
        {
            get { return _readingRoomName; }
            set { _readingRoomName = value; }
        } /// <summary>
        /// 选座时间
        /// </summary>
        public string ViolationTime
        {
            get { return _chooseTime; }
            set { _chooseTime = value; }
        }
        /// <summary>
        /// 违规事项
        /// </summary>
        public string ViolationItem
        {
            get;
            set;
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("{0}\n\n", this.MsgHeader);
            str.AppendFormat("地点：{0}\n", this.ReadingRoomName);
            str.AppendFormat("时间：{0}\n", this.ViolationTime);
            str.AppendFormat("事项：{0}\n\n", this.ViolationItem);
            str.Append(this.Remark);
            return str.ToString();
        }
    }
}
