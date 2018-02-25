using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_Device:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_Device
	{
		public AMS_Device()
		{}
		#region Model
		private int _id=-1;
		private string _number="";
		private int _devicetype=0;
		private int _campusid=-1;
		private bool? _isdel= false;
		private bool? _flag= true;
		private string _describe="";
		private string _caputrepath="";
		private DateTime? _caputretime;
		/// <summary>
		/// 设备ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 设备编号
		/// </summary>
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 设备类型
		/// </summary>
		public int DeviceType
		{
			set{ _devicetype=value;}
			get{return _devicetype;}
		}
		/// <summary>
		/// 校区ID
		/// </summary>
		public int CampusId
		{
			set{ _campusid=value;}
			get{return _campusid;}
		}
		/// <summary>
		/// 设备是否撤销
		/// </summary>
		public bool? IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		/// <summary>
		/// 设备是否开启
		/// </summary>
		public bool? Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 设备描述
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
		/// <summary>
		/// 截图地址
		/// </summary>
		public string CaputrePath
		{
			set{ _caputrepath=value;}
			get{return _caputrepath;}
		}
		/// <summary>
		/// 截图时间
		/// </summary>
		public DateTime? CaputreTime
		{
			set{ _caputretime=value;}
			get{return _caputretime;}
		}
		#endregion Model

	}
}

