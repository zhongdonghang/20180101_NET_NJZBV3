using System;
namespace AMS.Model
{
	/// <summary>
	/// View_SlipPrintInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_SlipPrintInfo
	{
		public View_SlipPrintInfo()
		{}
		#region Model
		private int _slipcustomerid;
		private string _slipcustomernum;
		private DateTime? _date;
		private int? _printamount;
		private int? _lookoveramount;
		private string _schoolnumber;
		private string _schoolname;
		private int _campusid;
		private string _campusnum;
		private string _campusname;
		private string _slipname;
		/// <summary>
		/// 
		/// </summary>
		public int SlipCustomerId
		{
			set{ _slipcustomerid=value;}
			get{return _slipcustomerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SlipCustomerNum
		{
			set{ _slipcustomernum=value;}
			get{return _slipcustomernum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Date
		{
			set{ _date=value;}
			get{return _date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PrintAmount
		{
			set{ _printamount=value;}
			get{return _printamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LookOverAmount
		{
			set{ _lookoveramount=value;}
			get{return _lookoveramount;}
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
		public int CampusId
		{
			set{ _campusid=value;}
			get{return _campusid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CampusNum
		{
			set{ _campusnum=value;}
			get{return _campusnum;}
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
		public string SlipName
		{
			set{ _slipname=value;}
			get{return _slipname;}
		}
		#endregion Model

	}
}

