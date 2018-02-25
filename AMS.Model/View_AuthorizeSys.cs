using System;
namespace AMS.Model
{
	/// <summary>
	/// View_AuthorizeSys:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_AuthorizeSys
	{
		public View_AuthorizeSys()
		{}
		#region Model
		private bool _authorizestatus;
		private int _id;
		private string _interfacetype;
		private DateTime? _effecttime;
		private DateTime? _endtime;
		private string _authorizecode;
		private string _describe;
		private bool? _iscomorsch;
		private string _schoolnumber;
		private string _schoolname;
		private string _schooldtuip;
		private string _schooldescribe;
		private string _schoolcon;
		private string _schoollinkman;
		/// <summary>
		/// 
		/// </summary>
		public bool AuthorizeStatus
		{
			set{ _authorizestatus=value;}
			get{return _authorizestatus;}
		}
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
		public string InterfaceType
		{
			set{ _interfacetype=value;}
			get{return _interfacetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EffectTime
		{
			set{ _effecttime=value;}
			get{return _effecttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AuthorizeCode
		{
			set{ _authorizecode=value;}
			get{return _authorizecode;}
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
		public bool? IsComOrSch
		{
			set{ _iscomorsch=value;}
			get{return _iscomorsch;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolNumber
		{
			set{ _schoolnumber=value;}
			get{return _schoolnumber;}
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
		public string SchoolDTUip
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
		public string SchoolCon
		{
			set{ _schoolcon=value;}
			get{return _schoolcon;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SchoolLinkMan
		{
			set{ _schoollinkman=value;}
			get{return _schoollinkman;}
		}
		#endregion Model

	}
}

