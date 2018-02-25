using System;
using System.Xml;
namespace SeatManage.ClassModel
{
    /// <summary>
    /// T_SM_StudyBookingLog:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class StudyBookingLog
    {
        public StudyBookingLog()
        { }
        #region Model
        private int _studyid = -1;
        private string _cardno = "";
        private string _studyroomno = "";
        private DateTime _submittime = new DateTime();
        private DateTime _checktime = DateTime.Parse("2000-1-1");
        private DateTime _bespeaktime = new DateTime();
        private int _usetime = 0;
        private EnumType.CheckStatus _checkstate = EnumType.CheckStatus.None;
        private string _checklperson = "";
        private string _remark = "";
        private string _StudyRoomName = "";
        private StudyRoomSetting _roomSetting = new StudyRoomSetting();
        private string _ReaderName = "";
        /// <summary>
        /// 读者姓名
        /// </summary>
        public string ReaderName
        {
            get { return _ReaderName; }
            set { _ReaderName = value; }
        }
        /// <summary>
        /// 研习间设置
        /// </summary>
        public StudyRoomSetting RoomSetting
        {
            get { return _roomSetting; }
            set { _roomSetting = value; }
        }
        /// <summary>
        /// 研习间名称
        /// </summary>
        public string StudyRoomName
        {
            get { return _StudyRoomName; }
            set { _StudyRoomName = value; }
        }
        /// <summary>
        /// id
        /// </summary>
        public int StudyID
        {
            set { _studyid = value; }
            get { return _studyid; }
        }
        /// <summary>
        /// 读者学号
        /// </summary>
        public string CardNo
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 研习间编号
        /// </summary>
        public string StudyRoomNo
        {
            set { _studyroomno = value; }
            get { return _studyroomno; }
        }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmitTime
        {
            set { _submittime = value; }
            get { return _submittime; }
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime CheckTime
        {
            set { _checktime = value; }
            get { return _checktime; }
        }
        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime BespeakTime
        {
            set { _bespeaktime = value; }
            get { return _bespeaktime; }
        }
        /// <summary>
        /// 使用时长
        /// </summary>
        public int UseTime
        {
            set { _usetime = value; }
            get { return _usetime; }
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        public EnumType.CheckStatus CheckState
        {
            set { _checkstate = value; }
            get { return _checkstate; }
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ChecklPerson
        {
            set { _checklperson = value; }
            get { return _checklperson; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        private ApplicationTable _application = new ApplicationTable();
        /// <summary>
        /// 申请表
        /// </summary>
        public ApplicationTable Application
        {
            get { return _application; }
            set { _application = value; }
        }
        #endregion Model

    }
    /// <summary>
    /// 研习间申请表
    /// </summary>
    public class ApplicationTable
    {
        /// <summary>
        /// 空构造函数
        /// </summary>
        public ApplicationTable()
        { }
        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="xml"></param>
        public ApplicationTable(string xml)
        {
            try
            {
                ToModel(xml);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 输出XML
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            return ConvertToXml(this);
        }
        /// <summary>
        /// 转换为mode
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public ApplicationTable ToModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                this.ApplicantCardNo = node.Attributes["ApplicantCardNo"].Value;
                this.ApplicantDept = node.Attributes["ApplicantDept"].Value;
                this.ApplicantName = node.Attributes["ApplicantName"].Value;
                this.ApplicantPhoneNum = node.Attributes["ApplicantPhoneNum"].Value;
                this.ApplicantType = node.Attributes["ApplicantType"].Value;
                this.HeadPerson = node.Attributes["HeadPerson"].Value;
                this.HeadPersonPhoneNum = node.Attributes["HeadPersonPhoneNum"].Value;
                this.HeadPersonType = node.Attributes["HeadPersonType"].Value;
                this.MeetingName = node.Attributes["MeetingName"].Value;
                this.MembersCount = int.Parse(node.Attributes["MembersCount"].Value);
                this.UseReason = node.Attributes["UseReason"].Value;
                this.UseProjector = node.Attributes["UseProjector"].Value;
                if (node.Attributes["EmailAddress"] != null)
                {
                    this.EmailAddress = node.Attributes["EmailAddress"].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this;
        }
        /// <summary>
        /// 输出xml
        /// </summary>
        /// <param name="stting"></param>
        /// <returns></returns>
        private string ConvertToXml(ApplicationTable model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            root.SetAttribute("ApplicantCardNo", model.ApplicantCardNo);
            root.SetAttribute("ApplicantDept", model.ApplicantDept);
            root.SetAttribute("ApplicantName", model.ApplicantName);
            root.SetAttribute("ApplicantPhoneNum", model.ApplicantPhoneNum);
            root.SetAttribute("ApplicantType", model.ApplicantType);
            root.SetAttribute("HeadPerson", model.HeadPerson);
            root.SetAttribute("HeadPersonPhoneNum", model.HeadPersonPhoneNum);
            root.SetAttribute("HeadPersonType", model.HeadPersonType);
            root.SetAttribute("MeetingName", model.MeetingName);
            root.SetAttribute("MembersCount", model.MembersCount.ToString());
            root.SetAttribute("UseReason", model.UseReason);
            root.SetAttribute("UseProjector", model.UseProjector);
            root.SetAttribute("EmailAddress", model.EmailAddress);
            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }
        private string _ApplicantName = "";
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName
        {
            get { return _ApplicantName; }
            set { _ApplicantName = value; }
        }
        private string _ApplicantType = "";
        /// <summary>
        /// 申请人类别/职称
        /// </summary>
        public string ApplicantType
        {
            get { return _ApplicantType; }
            set { _ApplicantType = value; }
        }
        private string _ApplicantCardNo = "";
        /// <summary>
        /// 证件号码
        /// </summary>
        public string ApplicantCardNo
        {
            get { return _ApplicantCardNo; }
            set { _ApplicantCardNo = value; }
        }
        private string _ApplicantDept = "";
        /// <summary>
        /// 申请人单位
        /// </summary>
        public string ApplicantDept
        {
            get { return _ApplicantDept; }
            set { _ApplicantDept = value; }
        }
        private string _ApplicantPhoneNum = "";
        /// <summary>
        /// 申请人电话
        /// </summary>
        public string ApplicantPhoneNum
        {
            get { return _ApplicantPhoneNum; }
            set { _ApplicantPhoneNum = value; }
        }
        private string _UseReason = "";
        /// <summary>
        /// 使用原因
        /// </summary>
        public string UseReason
        {
            get { return _UseReason; }
            set { _UseReason = value; }
        }
        private string _MeetingName = "";
        /// <summary>
        /// 会议名称
        /// </summary>
        public string MeetingName
        {
            get { return _MeetingName; }
            set { _MeetingName = value; }
        }
        private int _MembersCount = 1;
        /// <summary>
        /// 参与人数
        /// </summary>
        public int MembersCount
        {
            get { return _MembersCount; }
            set { _MembersCount = value; }
        }
        private string _HeadPerson = "";
        /// <summary>
        /// 负责人
        /// </summary>
        public string HeadPerson
        {
            get { return _HeadPerson; }
            set { _HeadPerson = value; }
        }
        private string _HeadPersonType = "";
        /// <summary>
        /// 负责人类别/职称
        /// </summary>
        public string HeadPersonType
        {
            get { return _HeadPersonType; }
            set { _HeadPersonType = value; }
        }
        private string _HeadPersonPhoneNum = "";
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string HeadPersonPhoneNum
        {
            get { return _HeadPersonPhoneNum; }
            set { _HeadPersonPhoneNum = value; }
        }

        private string _UseProjector = "";
        /// <summary>
        /// 需要使用的设备
        /// </summary>
        public string UseProjector
        {
            get { return _UseProjector; }
            set { _UseProjector = value; }
        }

        private string _EmailAddress = "";
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
    }
}

