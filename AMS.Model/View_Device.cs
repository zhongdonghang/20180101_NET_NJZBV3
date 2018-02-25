using System;
namespace AMS.Model
{
	/// <summary>
	/// View_Device:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_Device
	{
		public View_Device()
		{}
		#region Model
		private string _number;
		private bool? _isdel;
		private bool? _flag;
		private string _describe;
		private string _caputrepath;
		private DateTime? _caputretime;
		private string _schoolnumber;
		private string _schooname;
		private string _campusname;
		private string _campusnumber;
		private int _devicetype;
		private string _campusdescribe;
		private string _provincename;
		private int _id;
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
		public bool? IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool? Flag
		{
			set{ _flag=value;}
			get{return _flag;}
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
		public string CaputrePath
		{
			set{ _caputrepath=value;}
			get{return _caputrepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CaputreTime
		{
			set{ _caputretime=value;}
			get{return _caputretime;}
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
		public string SchooName
		{
			set{ _schooname=value;}
			get{return _schooname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CampusName
		{
			set{ _campusname=value;}
			get{return _campusname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CampusNumber
		{
			set{ _campusnumber=value;}
			get{return _campusnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DeviceType
		{
			set{ _devicetype=value;}
			get{return _devicetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CampusDescribe
		{
			set{ _campusdescribe=value;}
			get{return _campusdescribe;}
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
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		#endregion Model

	}
}

