using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_Seat : JM_Element
    {
        private bool havePower = false;
        private string _ReadingRoomNum = "";
        private string _SeatUsedState = "";
        private string shortseatNo = "";
        private bool canBeBook = false;
        private string seatNo = "";
        private bool isSuspended = false;
        private string seatState = "";
        private string _ReadingRoomName = "";
        private string _Remark;

        private string _userName = "";
        private string _userCardNo = "";
        private string _markTime = "";
        private string _beginUsedTime = "";
        
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; }
        }
        /// <summary>
        /// 座位当前状态
        /// </summary>
        public string SeatState
        {
            get { return seatState; }
            set { seatState = value; }
        }
        /// <summary>
        /// 所在阅览室编号
        /// </summary>
        public string ReadingRoomNum
        {
            get { return _ReadingRoomNum; }
            set { _ReadingRoomNum = value; }
        }
        /// <summary>
        /// 是否有电源
        /// </summary>
        public bool HavePower
        {
            get
            { return havePower; }
            set
            { havePower = value; }
        }
        /// <summary>
        /// 使用状态
        /// </summary>
        public string SeatUsedState
        {
            get { return _SeatUsedState; }
            set { _SeatUsedState = value; }
        }

        /// <summary>
        /// 显示的座位号
        /// </summary>
        public string ShortSeatNo
        {
            get
            { return shortseatNo; }
            set
            { shortseatNo = value; }
        }
        /// <summary>
        /// 完整的座位号
        /// </summary>
        public string SeatNo
        {
            get { return seatNo; }
            set { seatNo = value; }
        }

        /// <summary>
        /// 是否提供预约
        /// </summary>
        public bool CanBeBespeak
        {
            get { return canBeBook; }
            set { canBeBook = value; }
        }

        /// <summary>
        /// 是否暂停使用
        /// </summary>
        public bool IsSuspended
        {
            get { return isSuspended; }
            set { isSuspended = value; }
        }

        /// <summary>
        /// 座位开始使用时间
        /// </summary>
        public string BeginUsedTime
        {
            get { return _beginUsedTime; }
            set { _beginUsedTime = value; }
        }
        /// <summary>
        /// 座位开始计时时间
        /// </summary>
        public string MarkTime
        {
            get { return _markTime; }
            set { _markTime = value; }
        }
        /// <summary>
        /// 使用座位读者学号
        /// </summary>
        public string UserCardNo
        {
            get { return _userCardNo; }
            set { _userCardNo = value; }
        }
        /// <summary>
        /// 使用座位读者姓名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
    }
}
