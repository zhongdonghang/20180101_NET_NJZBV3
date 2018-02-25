using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_BlacklistLog
    {
        string _id = "";
        string _studentNo = "";
        string _addTime; 
        string _outTime;
        string _reMark = ""; 
        bool _isValid;  
        string _outBlacklistMode;
        /// <summary>
        /// 黑名单序号
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 读者学号
        /// </summary>
        public string StudentNo
        {
            get { return _studentNo; }
            set { _studentNo = value; }
        }
        /// <summary>
        /// 加入黑名单时间
        /// </summary>
        public string AddTime
        {
            get { return _addTime; }
            set { _addTime = value; }
        }
        /// <summary>
        /// 离开黑名单时间
        /// </summary>
        public string OutTime
        {
            get { return _outTime; }
            set { _outTime = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string ReMark
        {
            get { return _reMark; }
            set { _reMark = value; }
        }
        /// <summary>
        /// 记录是否有效
        /// </summary>
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }
        /// <summary>
        /// 离开黑名单方式:备注形式。如：7天后自动离开/需要管理员处理
        /// </summary>
        public string OutBlacklistMode
        {
            get { return _outBlacklistMode; }
            set { _outBlacklistMode = value; }
        }
    }
}
