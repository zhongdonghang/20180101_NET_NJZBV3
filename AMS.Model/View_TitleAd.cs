using System;
namespace AMS.Model
{
	/// <summary>
	/// View_TitleAd:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_TitleAd
	{
		public View_TitleAd()
		{}
		#region Model
		private string _name;
		private DateTime? _effectdate;
		private DateTime? _enddate;
		private string _adcontent;
		private string _companyname;
		private string _adcustomerno;
		private string _adcustomerlinkway;
		private string _adcustomerdes;
		private string _operatorbranchname;
		private string _operatorname;
		private string _operatorloginid;
		private string _operatorpwd;
		private string _operatorremark;
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
		public DateTime? EffectDate
		{
			set{ _effectdate=value;}
			get{return _effectdate;}
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
		public string AdContent
		{
			set{ _adcontent=value;}
			get{return _adcontent;}
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
		public string AdCustomerNo
		{
			set{ _adcustomerno=value;}
			get{return _adcustomerno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdCustomerLinkWay
		{
			set{ _adcustomerlinkway=value;}
			get{return _adcustomerlinkway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdCustomerDes
		{
			set{ _adcustomerdes=value;}
			get{return _adcustomerdes;}
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
		public string OperatorRemark
		{
			set{ _operatorremark=value;}
			get{return _operatorremark;}
		}
		#endregion Model

	}
}

