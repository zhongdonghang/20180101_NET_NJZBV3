using System;
namespace AMS.Model
{
	/// <summary>
	/// View_SlipCustomer:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_SlipCustomer
	{
		public View_SlipCustomer()
		{}
		#region Model
		private string _customerno;
		private string _companyname;
		private string _customerlinkway;
		private string _customerdescribe;
		private int _id;
		private string _number;
		private string _slipname;
		private string _imageurl;
		private string _sliptemplate;
		private string _couponsxml;
		private string _operatorlonginid;
		private string _operatorpwd;
		private string _operatorbranchname;
		private string _operatorname;
		private string _operatorremark;
		private string _slipcustomerdescribe;
		private bool? _isprint;
		private int? _type;
		private DateTime? _enddate;
		private DateTime? _effectdate;
		private string _campusnum;
		private string _customerimage;
		/// <summary>
		/// 
		/// </summary>
		public string CustomerNo
		{
			set{ _customerno=value;}
			get{return _customerno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CompanyName
		{
			set{ _companyname=value;}
			get{return _companyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomerLinkWay
		{
			set{ _customerlinkway=value;}
			get{return _customerlinkway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomerDescribe
		{
			set{ _customerdescribe=value;}
			get{return _customerdescribe;}
		}
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
		public string SlipName
		{
			set{ _slipname=value;}
			get{return _slipname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
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
		public string CouponsXml
		{
			set{ _couponsxml=value;}
			get{return _couponsxml;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorLonginId
		{
			set{ _operatorlonginid=value;}
			get{return _operatorlonginid;}
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
		/// <summary>
		/// 
		/// </summary>
		public string SlipCustomerDescribe
		{
			set{ _slipcustomerdescribe=value;}
			get{return _slipcustomerdescribe;}
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
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EffectDate
		{
			set{ _effectdate=value;}
			get{return _effectdate;}
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
		public string CustomerImage
		{
			set{ _customerimage=value;}
			get{return _customerimage;}
		}
		#endregion Model

	}
}

