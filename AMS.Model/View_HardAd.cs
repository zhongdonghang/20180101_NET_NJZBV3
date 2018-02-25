using System;
namespace AMS.Model
{
	/// <summary>
	/// View_HardAd:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_HardAd
	{
		public View_HardAd()
		{}
		#region Model
		private string _companyname;
		private string _customerno;
		private string _customerlinkway;
		private string _customerdescribe;
		private string _name;
		private int? _operator;
		private string _number;
		private DateTime? _effectdate;
		private DateTime? _enddate;
		private byte[] _adimage;
		private string _hardaddes;
		private string _operatorloginid;
		private string _operatorpwd;
		private string _operatorbranchname;
		private string _operatorname;
		private string _operatorremark;
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
		public string CustomerNo
		{
			set{ _customerno=value;}
			get{return _customerno;}
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Operator
		{
			set{ _operator=value;}
			get{return _operator;}
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
		public byte[] AdImage
		{
			set{ _adimage=value;}
			get{return _adimage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HardAdDes
		{
			set{ _hardaddes=value;}
			get{return _hardaddes;}
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

