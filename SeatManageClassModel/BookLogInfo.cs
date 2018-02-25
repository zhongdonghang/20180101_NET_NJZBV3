using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    [Serializable]
    /// <summary>
    /// 预约记录实体
    /// </summary>
    public class BespeakLogInfo
    {
        private string _BsepeaklogID;

        private DateTime _BsepeakTime = DateTime.Parse("1900-1-1");
        private string _ReaderName = ""; 
        private EnumType.BookingStatus _BsepeakState;
        private Operation _CancelPerson = Operation.None;
        private DateTime _CancelTime = DateTime.Parse("1900-1-1");
        private string _CardNo = "";
        private string _ReadingRoomNo = "";
        private string _SeatNo = "";
        private DateTime _SubmitTime = DateTime.Parse("1900-1-1");
        private string _ReadingRoomName = "";
        private string _remark = "";
        private string shortSeatNum = "";
        private string _FlagKey = "";
        /// <summary>
        /// 加密码
        /// </summary>
        public string FlagKey
        {
            get { return _FlagKey; }
            set { _FlagKey = value; }
        }
        /// <summary>
        /// 短座位号
        /// </summary>
        public string ShortSeatNum
        {
            get { return shortSeatNum; }
            set { shortSeatNum = value; }
        }
        /// <summary>
        /// Id
        /// </summary>
        public string BsepeaklogID
        {
            get { return _BsepeaklogID; }
            set { _BsepeaklogID = value; }
        }
        /// <summary>
        /// 读者姓名
        /// </summary>
        public string ReaderName
        {
            get { return _ReaderName; }
            set { _ReaderName = value; }
        }
        /// <summary>
        /// 预约开始时间
        /// </summary>
        public DateTime BsepeakTime
        {
            get
            {
                return _BsepeakTime;
            }
            set
            {
                _BsepeakTime = value;
            }
        }

        /// <summary>
        /// 预约状态
        /// </summary>
        public EnumType.BookingStatus BsepeakState
        {
            get
            {
                return _BsepeakState;
            }
            set
            {
                _BsepeakState = value;
            }
        }

        /// <summary>
        /// 取消人
        /// </summary>
        public Operation CancelPerson
        {
            get
            {
                return _CancelPerson;
            }
            set
            {
                _CancelPerson = value;
            }
        }

        /// <summary>
        /// 取消时间
        /// </summary>
        public DateTime CancelTime
        {
            get
            {
                return _CancelTime;
            }
            set
            {
                _CancelTime = value;
            }
        }

        /// <summary>
        /// 预约人的卡号
        /// </summary>
        public string CardNo
        {
            get
            {
                return _CardNo;
            }
            set
            {
                _CardNo = value;
            }
        }

        /// <summary>
        /// 预约的阅览室Id
        /// </summary>
        public string ReadingRoomNo
        {
            get
            {
                return _ReadingRoomNo;
            }
            set
            {
                _ReadingRoomNo = value;
            }
        }

        /// <summary>
        /// 预约阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; }
        }

        /// <summary>
        /// 预约的座位号
        /// </summary>
        public string SeatNo
        {
            get
            {
                return _SeatNo;
            }
            set
            {
                _SeatNo = value;
            }
        }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmitTime
        {
            get
            {
                return _SubmitTime;
            }
            set
            {
                _SubmitTime = value;
            }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        private string _TypeName;
        /// <summary>
        /// 读者类型
        /// </summary>
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        private string _DeptName;
        /// <summary>
        /// 读者院系
        /// </summary>
        public string DeptName
        {
            get { return _DeptName; }
            set { _DeptName = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        private string _Sex;
    }
}
