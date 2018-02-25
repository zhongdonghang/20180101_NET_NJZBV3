using System;
namespace AMS.Model
{
	/// <summary>
	/// View_EnterOutLog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_EnterOutLog
	{
		public View_EnterOutLog()
		{}
		#region Model
		private int _id;
		private string _cardno;
		private string _enteroutno;
		private int? _enteroutstate;
		private string _terminalnum;
		private string _readingroomnum;
		private string _seatno;
		private int? _operator;
		private DateTime? _enterouttime;
		private int? _enterouttype;
		private string _remark;
		private string _schoolname;
		private string _schoolnum;
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
		public string CardNo
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EnterOutNo
		{
			set{ _enteroutno=value;}
			get{return _enteroutno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EnterOutState
		{
			set{ _enteroutstate=value;}
			get{return _enteroutstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TerminalNum
		{
			set{ _terminalnum=value;}
			get{return _terminalnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReadingRoomNum
		{
			set{ _readingroomnum=value;}
			get{return _readingroomnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SeatNo
		{
			set{ _seatno=value;}
			get{return _seatno;}
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
		public DateTime? EnterOutTime
		{
			set{ _enterouttime=value;}
			get{return _enterouttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EnterOutType
		{
			set{ _enterouttype=value;}
			get{return _enterouttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
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

