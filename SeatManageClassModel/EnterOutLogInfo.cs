using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 进出记录实体
    /// </summary>
    [Serializable]
    public class EnterOutLogInfo
    {
        private string _EnterOutLogID = "";
        /// <summary>
        /// 进出记录ID
        /// </summary>
        public string EnterOutLogID
        {
            get { return _EnterOutLogID; }
            set { _EnterOutLogID = value; }
        }
        private DateTime _MarkTime = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 标注时间
        /// </summary>
        public DateTime MarkTime
        {
            get { return _MarkTime; }
            set { _MarkTime = value; }
        }
        private string _ReadingRoomNo;
        /// <summary>
        /// 房间编号
        /// </summary>
        public string ReadingRoomNo
        {
            get { return _ReadingRoomNo; }
            set { _ReadingRoomNo = value; }
        }

        private string cardNo = null;
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get
            {
                return cardNo;
            }
            set
            {
                cardNo = value;
            }
        }

        private string readerName = "";
        /// <summary>
        /// 读者姓名
        /// </summary>
        public string ReaderName
        {
            get { return readerName; }
            set { readerName = value; }
        }
        private string _ReadingRoomName = "";
        /// <summary>
        /// 房间名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; }
        }
        private string enterOutLogNo = null;
        /// <summary>
        /// 流水号
        /// </summary>
        public string EnterOutLogNo
        {
            get
            {
                return enterOutLogNo;
            }
            set
            {
                enterOutLogNo = value;
            }
        }

        private EnumType.EnterOutLogType enterOutState;
        /// <summary>
        /// 进出记录状态
        /// </summary>
        public EnumType.EnterOutLogType EnterOutState
        {
            get
            {
                return enterOutState;
            }
            set
            {
                enterOutState = value;
            }
        }

        private DateTime enterOutTime;
        /// <summary>
        /// 进出时间
        /// </summary>
        public DateTime EnterOutTime
        {
            get
            {
                return enterOutTime;
            }
            set
            {
                enterOutTime = value;
            }
        }

        private EnumType.LogStatus _EnterOutType = LogStatus.Valid;
        /// <summary>
        /// 记录有效标识
        /// </summary>
        public EnumType.LogStatus EnterOutType
        {
            get { return _EnterOutType; }
            set { _EnterOutType = value; }
        }
        private string _TerminalNum = "";
        /// <summary>
        /// 终端编号
        /// </summary>
        public string TerminalNum
        {
            get { return _TerminalNum; }
            set { _TerminalNum = value; }
        }

        private EnumType.Operation flag;
        /// <summary>
        /// 操作者表示
        /// </summary>
        public Operation Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        private string remark="无";
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }

        private string seatNo;
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get
            {
                return seatNo;
            }
            set
            {
                seatNo = value;
            }
        }

        private string shortSeatNo;
        /// <summary>
        /// 短座位号
        /// </summary>
        public string ShortSeatNo
        {
            get { return shortSeatNo; }
            set { shortSeatNo = value; }
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
