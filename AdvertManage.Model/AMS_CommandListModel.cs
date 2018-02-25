using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.Model.Enum;

namespace AdvertManage.Model
{
    /// <summary>
    /// 下发命令
    /// </summary>
    [Serializable]
    public class AMS_CommandListModel
    {
        #region Model
        private int _id;
        private int _schoolid;
        private Enum.CommandType _command;
        private int? _commandid;
        private DateTime _releasetime;
        private DateTime? _finishtime;
        private CommandHandleResult _finishflag;
        private string _schoolnum;
        private string _schoolname;
        private string _schoolconnectionstring;
        private string _schooldescribe;
        private string _schooldtuip;
        /// <summary>
        /// 下发命令的Id
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 下发到学校的Id
        /// </summary>
        public int SchoolId
        {
            set { _schoolid = value; }
            get { return _schoolid; }
        }
        /// <summary>
        /// 命令类型
        /// </summary>
        public Enum.CommandType Command
        {
            set { _command = value; }
            get { return _command; }
        }
        /// <summary>
        /// 命令中操作需要的Id
        /// </summary>
        public int? CommandId
        {
            set { _commandid = value; }
            get { return _commandid; }
        }
        /// <summary>
        /// 命令发布时间
        /// </summary>
        public DateTime ReleaseTime
        {
            set { _releasetime = value; }
            get { return _releasetime; }
        }
        /// <summary>
        /// 命令完成时间
        /// </summary>
        public DateTime? FinishTime
        {
            set { _finishtime = value; }
            get { return _finishtime; }
        }
        /// <summary>
        /// 命令完成标识
        /// </summary>
        public CommandHandleResult FinishFlag
        {
            set { _finishflag = value; }
            get { return _finishflag; }
        }
        /// <summary>
        /// 命令下发到学校的编号
        /// </summary>
        public string SchoolNum
        {
            set { _schoolnum = value; }
            get { return _schoolnum; }
        }
        /// <summary>
        /// 命令下发到学校的名称
        /// </summary>
        public string SchoolName
        {
            set { _schoolname = value; }
            get { return _schoolname; }
        }
        /// <summary>
        /// 命令下发到学校的链接远程服务链接字符串
        /// </summary>
        public string SchoolConnectionString
        {
            set { _schoolconnectionstring = value; }
            get { return _schoolconnectionstring; }
        }
        /// <summary>
        /// 命令下发到学校的描述
        /// </summary>
        public string SchoolDescribe
        {
            set { _schooldescribe = value; }
            get { return _schooldescribe; }
        }
        /// <summary>
        /// 命令下发到学校的数据中转服务器Ip
        /// </summary>
        public string SchoolDTUip
        {
            set { _schooldtuip = value; }
            get { return _schooldtuip; }
        }
        #endregion Model
    }
}
