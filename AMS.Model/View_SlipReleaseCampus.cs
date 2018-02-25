using System;
namespace AMS.Model
{
	/// <summary>
	/// View_SlipReleaseCampus:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_SlipReleaseCampus
	{
		public View_SlipReleaseCampus()
		{}
		#region Model
		private int _id;
		private string _schoolnum;
		private string _schoolname;
		private int _campusid;
		private string _campusnum;
		private string _campusname;
		private int _slipcustomerid;
		private string _slipcustomernum;
		private string _sliptemplate;
		private bool? _isprint;
		private int? _sliptype;
		private DateTime? _slipenddate;
		private DateTime? _slipeffectdate;
		private string _customerimage;
		private string _slipimageurl;
		private string _slipname;
		private string _couponsxml;
		private string _slipcustomerdescribe;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
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
		public string SlipTemplate
		{
			set{ _sliptemplate=value;}
			get{return _sliptemplate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool? IsPrint
		{
			set{ _isprint=value;}
			get{return _isprint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SlipType
		{
			set{ _sliptype=value;}
			get{return _sliptype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SlipEndDate
		{
			set{ _slipenddate=value;}
			get{return _slipenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SlipEffectDate
		{
			set{ _slipeffectdate=value;}
			get{return _slipeffectdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomerImage
		{
			set{ _customerimage=value;}
			get{return _customerimage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SlipImageUrl
		{
			set{ _slipimageurl=value;}
			get{return _slipimageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SlipName
		{
			set{ _slipname=value;}
			get{return _slipname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CouponsXml
		{
			set{ _couponsxml=value;}
			get{return _couponsxml;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SlipCustomerDescribe
		{
			set{ _slipcustomerdescribe=value;}
			get{return _slipcustomerdescribe;}
		}
		#endregion Model

	}
}

