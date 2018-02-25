using System;
namespace AMS.Model
{
	/// <summary>
	/// View_Campus:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_Campus
	{
		public View_Campus()
		{}
		#region Model
		private int _id;
		private string _number;
		private int _schoolid;
		private string _name;
		private string _describe;
		private string _schoolnum;
		private string _schoolname;
		private string _schooldtuip;
		private string _schooldescribe;
		private string _schoolconnectionstring;
		private string _schoollinkman;
		private string _schoolcardinfo;
		private string _schoolinterfaceinfo;
		private string _schooladdress;
		private string _provincename;
		private string _provinceremark;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SchoolId
		{
			set{ _schoolid=value;}
			get{return _schoolid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
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
		public string SchoolDTUIp
		{
			set{ _schooldtuip=value;}
			get{return _schooldtuip;}
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
		public string SchoolConnectionString
		{
			set{ _schoolconnectionstring=value;}
			get{return _schoolconnectionstring;}
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
		public string SchoolAddress
		{
			set{ _schooladdress=value;}
			get{return _schooladdress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProvinceName
		{
			set{ _provincename=value;}
			get{return _provincename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProvinceRemark
		{
			set{ _provinceremark=value;}
			get{return _provinceremark;}
		}
		#endregion Model

	}
}

