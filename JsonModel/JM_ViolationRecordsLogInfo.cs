using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
   public class JM_ViolationRecordsLog
    { 
        string _CardNo = "";
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        } 
        string _SeatID = "";
        /// <summary>
        /// 座位编号
        /// </summary>
        public string SeatID
        {
            get { return _SeatID; }
            set { _SeatID = value; }
        }
        string _ReadingRoomID = "";
        /// <summary>
        /// 阅览室ID
        /// </summary>
        public string ReadingRoomID
        {
            get { return _ReadingRoomID; }
            set { _ReadingRoomID = value; }
        }
        string _ReadingRoomName = "";
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; }
        }
        
        string _EnterOutTime = "";
        /// <summary>
        /// 违规时间
        /// </summary>
        public string EnterOutTime
        {
            get { return _EnterOutTime; }
            set { _EnterOutTime = value; }
        }
        string _ViolateType = "";
        /// <summary>
        /// 标注
        /// </summary>
        public string Remark
        {
            get { return _ViolateType; }
            set { _ViolateType = value; }
        }
       
       
        
    }
}
