using System;
using System.Collections.Generic;
namespace AMS.Model
{
    /// <summary>
    /// AMS_School:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AMS_School
    {
        public AMS_School()
        { }
        #region Model
        private int _id;
        private string _number;
        private string _name;
        private string _dtuip;
        private string _describe;
        private string _connectionstring;
        private string _linkman;
        private string _linkaddress; 
        private int _provinceid=-1;
        private string _provinceName; 
        private string _cardinfo;
        private List<AMS_Campus> _Campus = new List<AMS_Campus>();
        private string _interfaceinfo;
        private string executeProgress;
        private string installDate;
        private string _InstallMan;
        private bool _IsSeatBespeak;
        private bool _appOpen;
        /// <summary>
        /// 座位是否启用预约
        /// </summary>
        public bool IsSeatBespeak
        {
            get { return _IsSeatBespeak; }
            set { _IsSeatBespeak = value; }
        }
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName
        {
            get { return _provinceName; }
            set { _provinceName = value; }
        }
        /// <summary>
        /// 安装人
        /// </summary>
        public string InstallMan
        {
            get { return _InstallMan; }
            set { _InstallMan = value; }
        }
        /// <summary>
        /// 安装进度
        /// </summary>
        public string ExecuteProgress
        {
            get { return executeProgress; }
            set { executeProgress = value; }
        }
        /// <summary>
        /// 安装日期
        /// </summary>
        public string InstallDate
        {
            get { return installDate; }
            set { installDate = value; }
        }
        /// <summary>
        /// 学校ID
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 学校编号
        /// </summary>
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 外网IP
        /// </summary>
        public string DTUip
        {
            set { _dtuip = value; }
            get { return _dtuip; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }
        /// <summary>
        /// 连接字段
        /// </summary>
        public string ConnectionString
        {
            set { _connectionstring = value; }
            get { return _connectionstring; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan
        {
            set { _linkman = value; }
            get { return _linkman; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string LinkAddress
        {
            set { _linkaddress = value; }
            get { return _linkaddress; }
        }
        /// <summary>
        /// 省份ID
        /// </summary>
        public int ProvinceID
        {
            set { _provinceid = value; }
            get { return _provinceid; }
        }
        /// <summary>
        /// 卡信息
        /// </summary>
        public string CardInfo
        {
            set { _cardinfo = value; }
            get { return _cardinfo; }
        }
        /// <summary>
        /// 接口信息
        /// </summary>
        public string InterfaceInfo
        {
            set { _interfaceinfo = value; }
            get { return _interfaceinfo; }
        }
        public List<AMS_Campus> Campus
        {
            get { return _Campus; }
            set { _Campus = value; }
        }
        /// <summary>
        /// 是否开放App
        /// </summary>
        public bool AppOpen
        {
            get { return _appOpen; }
            set { _appOpen = value; }
        }

        #endregion Model

    }
}

