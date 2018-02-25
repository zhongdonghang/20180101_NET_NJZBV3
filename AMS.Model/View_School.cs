using System;
namespace AMS.Model
{
	/// <summary>
	/// View_School:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_School
	{
		public View_School()
		{}
		#region Model
		private int _id;
		private string _number;
		private string _name;
		private string _dtuip;
		private string _describe;
		private string _connectionstring;
		private string _linkman;
		private string _linkaddress;
		private string _cardinfo;
		private string _interfaceinfo;
		private string _provincename;
		private string _provinceremark;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DTUip
		{
			set{ _dtuip=value;}
			get{return _dtuip;}
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
		public string ConnectionString
		{
			set{ _connectionstring=value;}
			get{return _connectionstring;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LinkMan
		{
			set{ _linkman=value;}
			get{return _linkman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LinkAddress
		{
			set{ _linkaddress=value;}
			get{return _linkaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CardInfo
		{
			set{ _cardinfo=value;}
			get{return _cardinfo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InterfaceInfo
		{
			set{ _interfaceinfo=value;}
			get{return _interfaceinfo;}
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
		public string ProvinceRemark
		{
			set{ _provinceremark=value;}
			get{return _provinceremark;}
		}
		#endregion Model

	}
}

