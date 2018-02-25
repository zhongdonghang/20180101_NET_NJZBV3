using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_EnterOutLog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_EnterOutLog
	{
		public AMS_EnterOutLog()
		{}
		#region Model
		private int _id;
		private int _schoolid;
		private string _cardno;
		private string _enteroutno;
		private int? _enteroutstate;
		private string _terminalnum;
		private string _readingroomnum;
		private string _seatno;
		private DateTime? _enterouttime;
		private int? _operator;
		private int? _enterouttype;
		private string _remark;
		/// <summary>
		/// 进出记录ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 学校ID
		/// </summary>
		public int Schoolid
		{
			set{ _schoolid=value;}
			get{return _schoolid;}
		}
		/// <summary>
		/// 卡号
		/// </summary>
		public string CardNo
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 进出记录编号
		/// </summary>
		public string EnterOutNo
		{
			set{ _enteroutno=value;}
			get{return _enteroutno;}
		}
		/// <summary>
		/// 进出状态
		/// </summary>
		public int? EnterOutState
		{
			set{ _enteroutstate=value;}
			get{return _enteroutstate;}
		}
		/// <summary>
		/// 终端编号
		/// </summary>
		public string TerminalNum
		{
			set{ _terminalnum=value;}
			get{return _terminalnum;}
		}
		/// <summary>
		/// 阅览室编号
		/// </summary>
		public string ReadingRoomNum
		{
			set{ _readingroomnum=value;}
			get{return _readingroomnum;}
		}
		/// <summary>
		/// 座位号
		/// </summary>
		public string SeatNo
		{
			set{ _seatno=value;}
			get{return _seatno;}
		}
		/// <summary>
		/// 进出时间
		/// </summary>
		public DateTime? EnterOutTime
		{
			set{ _enterouttime=value;}
			get{return _enterouttime;}
		}
		/// <summary>
		/// 操作人
		/// </summary>
		public int? Operator
		{
			set{ _operator=value;}
			get{return _operator;}
		}
		/// <summary>
		/// 进出类型
		/// </summary>
		public int? EnterOutType
		{
			set{ _enterouttype=value;}
			get{return _enterouttype;}
		}
		/// <summary>
		/// 进出记录描述
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

