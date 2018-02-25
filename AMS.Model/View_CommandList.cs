using System;
using AMS.Model.Enum;
namespace AMS.Model
{
	/// <summary>
	/// View_CommandList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_CommandList
	{
		public View_CommandList()
		{}
		#region Model
		private int _id;
        private CommandType _command;
		private int? _commandid;
		private DateTime _releasetime;
		private DateTime? _finishtime;
		private int? _finishflag;
		private string _schoolnum;
		private string _schoolname;
		private string _schoolconnectionstring;
		private string _schooldescribe;
		private string _schooldtuip;
		private string _schoollinkman;
		private string _schooladdress;
		private int _schoolprovince;
		private string _schoolcardinfo;
		private string _schoolinterfaceinfo;
		private string _operatorloginid;
		private string _operatorpwd;
		private string _operatorbranchname;
		private string _operatorname;
		private string _operatorremark;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
        public CommandType Command
		{
			set{ _command=value;}
			get{return _command;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommandId
		{
			set{ _commandid=value;}
			get{return _commandid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ReleaseTime
		{
			set{ _releasetime=value;}
			get{return _releasetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FinishTime
		{
			set{ _finishtime=value;}
			get{return _finishtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FinishFlag
		{
			set{ _finishflag=value;}
			get{return _finishflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolNum
		{
			set{ _schoolnum=value;}
			get{return _schoolnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolName
		{
			set{ _schoolname=value;}
			get{return _schoolname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolConnectionString
		{
			set{ _schoolconnectionstring=value;}
			get{return _schoolconnectionstring;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolDescribe
		{
			set{ _schooldescribe=value;}
			get{return _schooldescribe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolDTUip
		{
			set{ _schooldtuip=value;}
			get{return _schooldtuip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolLinkMan
		{
			set{ _schoollinkman=value;}
			get{return _schoollinkman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolAddress
		{
			set{ _schooladdress=value;}
			get{return _schooladdress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SchoolProvince
		{
			set{ _schoolprovince=value;}
			get{return _schoolprovince;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolCardInfo
		{
			set{ _schoolcardinfo=value;}
			get{return _schoolcardinfo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolInterfaceInfo
		{
			set{ _schoolinterfaceinfo=value;}
			get{return _schoolinterfaceinfo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorLoginId
		{
			set{ _operatorloginid=value;}
			get{return _operatorloginid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorPwd
		{
			set{ _operatorpwd=value;}
			get{return _operatorpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorBranchName
		{
			set{ _operatorbranchname=value;}
			get{return _operatorbranchname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorName
		{
			set{ _operatorname=value;}
			get{return _operatorname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorRemark
		{
			set{ _operatorremark=value;}
			get{return _operatorremark;}
		}
		#endregion Model

	}
}

