using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_AdCustomer:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_AdCustomer
	{
		public AMS_AdCustomer()
		{}
		#region Model
		private int _id;
		private string _customerno;
		private string _companyname;
		private string _linkway;
		private string _describe;
		/// <summary>
		/// 客户ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 客户编号
		/// </summary>
		public string CustomerNo
		{
			set{ _customerno=value;}
			get{return _customerno;}
		}
		/// <summary>
		/// 客户名称
		/// </summary>
		public string CompanyName
		{
			set{ _companyname=value;}
			get{return _companyname;}
		}
		/// <summary>
		/// 客户联系方式
		/// </summary>
		public string LinkWay
		{
			set{ _linkway=value;}
			get{return _linkway;}
		}
		/// <summary>
		/// 客户描述
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
		#endregion Model

	}
}

