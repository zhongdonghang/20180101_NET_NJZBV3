using System;
namespace AMS.Model
{
	/// <summary>
	/// View_PrintTemplate:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_PrintTemplate
	{
		public View_PrintTemplate()
		{}
		#region Model
		private string _customername;
		private string _name;
		private string _number;
		private string _template;
		private DateTime? _effectdate;
		private DateTime? _enddate;
		private string _describe;
		private string _customerno;
		private string _customerlinkway;
		private string _customerdescribe;
		private string _operatorremark;
		private string _operatorname;
		private string _operatorbranchname;
		private string _operatorpwd;
		private string _operatorloginid;
		/// <summary>
		/// 
		/// </summary>
		public string CustomerName
		{
			set{ _customername=value;}
			get{return _customername;}
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
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Template
		{
			set{ _template=value;}
			get{return _template;}
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
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
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
		public string OperatorRemark
		{
			set{ _operatorremark=value;}
			get{return _operatorremark;}
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
		public string OperatorBranchName
		{
			set{ _operatorbranchname=value;}
			get{return _operatorbranchname;}
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
		public string OperatorLoginId
		{
			set{ _operatorloginid=value;}
			get{return _operatorloginid;}
		}
		#endregion Model

	}
}

