using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    /// <summary>
    /// 黑名单信息
    /// </summary>
    public class JM_Blacklist
    {
        string _ID = "";
        /// <summary>
        /// 黑名单序号
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string _CardNo = "";
        /// <summary>
        /// 读者学号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }
  
         
       
        string _AddTime;
        /// <summary>
        /// 加入时间
        /// </summary>
        public string AddTime
        {
            get { return _AddTime; }
            set { _AddTime = value; }
        }
        string _OutTime  ;
        /// <summary>
        /// 离开时间
        /// </summary>
        public string OutTime
        {
            get { return _OutTime; }
            set { _OutTime = value; }
        }
        string _ReMark = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string ReMark
        {
            get { return _ReMark; }
            set { _ReMark = value; }
        }
        bool isValid;
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }
       string  _OutBlacklistMode ;
        /// <summary>
        /// 离开黑名单方式:备注形式。如：7天后自动离开/需要管理员处理
        /// </summary>
       public string OutBlacklistMode
        {
            get { return _OutBlacklistMode; }
            set { _OutBlacklistMode = value; }
        }
    }
}
