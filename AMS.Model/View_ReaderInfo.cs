using System;
namespace AMS.Model
{
	/// <summary>
	/// View_ReaderInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_ReaderInfo
	{
		public View_ReaderInfo()
		{}
		#region Model
		private string _cardno;
		private string _name;
		private string _sex;
		private string _dept;
		private string _type;
		private string _schoolname;
		private string _schoolnum;
		/// <summary>
		/// 
		/// </summary>
		public string CardNo
		{
			set{ _cardno=value;}
			get{return _cardno;}
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
		public string sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dept
		{
			set{ _dept=value;}
			get{return _dept;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string type
		{
			set{ _type=value;}
			get{return _type;}
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
		public string SchoolNum
		{
			set{ _schoolnum=value;}
			get{return _schoolnum;}
		}
		#endregion Model

	}
}

