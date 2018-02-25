using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model.Enum;

namespace AMS.Model
{
    public class AMS_IssureList
    {
        private int _ID = -1;
        /// <summary>
        /// 命令ID
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private IsureCommandType _CommandType = IsureCommandType.None;
        /// <summary>
        /// 命令类型
        /// </summary>
        public IsureCommandType CommandType
        {
            get { return _CommandType; }
            set { _CommandType = value; }
        }
        private int _CommandID = -1;
        /// <summary>
        /// 命令id
        /// </summary>
        public int CommandID
        {
            get { return _CommandID; }
            set { _CommandID = value; }
        }
        private int _SchoolID = -1;
        /// <summary>
        /// 学校编号
        /// </summary>
        public int SchoolID
        {
            get { return _SchoolID; }
            set { _SchoolID = value; }
        }
        private string _SchoolName = "";
        /// <summary>
        /// 学校名字
        /// </summary>
        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }
        private AMS.Model.Enum.AdType _AdvertType = AdType.None;
        /// <summary>
        /// 广告类型
        /// </summary>
        public AMS.Model.Enum.AdType AdvertType
        {
            get { return _AdvertType; }
            set { _AdvertType = value; }
        }
        private string _AdInfo = "";
        /// <summary>
        /// 广告消息
        /// </summary>
        public string AdInfo
        {
            get { return _AdInfo; }
            set { _AdInfo = value; }
        }
        private DateTime _SubmitTime = new DateTime();
        /// <summary>
        /// 下发时间
        /// </summary>
        public DateTime SubmitTime
        {
            get { return _SubmitTime; }
            set { _SubmitTime = value; }
        }
        private DateTime _GetTime =  DateTime.Parse("2000-1-1");
        /// <summary>
        /// 获取时间
        /// </summary>
        public DateTime GetTime
        {
            get { return _GetTime; }
            set { _GetTime = value; }
        }
        private DateTime _CompleteTime = DateTime.Parse("2000-1-1");
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompleteTime
        {
            get { return _CompleteTime; }
            set { _CompleteTime = value; }
        }
        private int _OperatorID = -1;
        /// <summary>
        /// 操作者
        /// </summary>
        public int OperatorID
        {
            get { return _OperatorID; }
            set { _OperatorID = value; }
        }
        private string _OperatorName = "";
        /// <summary>
        /// 下发人
        /// </summary>
        public string OperatorName
        {
            get { return _OperatorName; }
            set { _OperatorName = value; }
        }
        private int _Flag = -1;
        /// <summary>
        /// 操作状态
        /// </summary>
        public int Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
    }
}
