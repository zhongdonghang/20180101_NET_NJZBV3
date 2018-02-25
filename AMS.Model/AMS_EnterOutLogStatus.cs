using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_EnterOutLogStatus:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_EnterOutLogStatus
	{
		public AMS_EnterOutLogStatus()
		{}
		#region Model
		private int _id;
		private string _schoolid;
		private string _enteroutlogno;
		private int? _lastenteroutid;
		private string _cardno;
		private string _seatno;
		private string _readingroomno;
		private int? _selectseatmode;
		private int? _leavemodel;
		private DateTime _selectseattime;
		private DateTime _leaveseattime;
		private int? _seattime;
		private int? _shortleavecount;
		private int? _continuetimecount;
		private int? _alloperationcount;
		private int? _adminoperationcount;
		private int? _readeroperationcount;
		private int? _otheroperationcount;
		private int? _serveroperationcount;
		private bool _isviolation;
		/// <summary>
		/// 进出记录ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 学校ID
		/// </summary>
		public string SchoolId
		{
			set{ _schoolid=value;}
			get{return _schoolid;}
		}
		/// <summary>
		/// 进出记录编号
		/// </summary>
		public string EnterOutLogNo
		{
			set{ _enteroutlogno=value;}
			get{return _enteroutlogno;}
		}
		/// <summary>
		/// 最后的进出记录ID
		/// </summary>
		public int? LastEnterOutID
		{
			set{ _lastenteroutid=value;}
			get{return _lastenteroutid;}
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
		/// 座位号
		/// </summary>
		public string SeatNo
		{
			set{ _seatno=value;}
			get{return _seatno;}
		}
		/// <summary>
		/// 阅览室编号
		/// </summary>
		public string ReadingRoomNo
		{
			set{ _readingroomno=value;}
			get{return _readingroomno;}
		}
		/// <summary>
		/// 选座操作类型
		/// </summary>
		public int? SelectSeatMode
		{
			set{ _selectseatmode=value;}
			get{return _selectseatmode;}
		}
		/// <summary>
		/// 离开方式
		/// </summary>
		public int? LeaveModel
		{
			set{ _leavemodel=value;}
			get{return _leavemodel;}
		}
		/// <summary>
		/// 选座时间
		/// </summary>
		public DateTime SelectSeatTime
		{
			set{ _selectseattime=value;}
			get{return _selectseattime;}
		}
		/// <summary>
		/// 离座时间
		/// </summary>
		public DateTime LeaveSeatTime
		{
			set{ _leaveseattime=value;}
			get{return _leaveseattime;}
		}
		/// <summary>
		/// 选座次数
		/// </summary>
		public int? SeatTime
		{
			set{ _seattime=value;}
			get{return _seattime;}
		}
		/// <summary>
		/// 暂离次数
		/// </summary>
		public int? ShortLeaveCount
		{
			set{ _shortleavecount=value;}
			get{return _shortleavecount;}
		}
		/// <summary>
		/// 续时次数
		/// </summary>
		public int? ContinueTimeCount
		{
			set{ _continuetimecount=value;}
			get{return _continuetimecount;}
		}
		/// <summary>
		/// 全操作次数
		/// </summary>
		public int? AllOperationCount
		{
			set{ _alloperationcount=value;}
			get{return _alloperationcount;}
		}
		/// <summary>
		/// 管理员操作次数
		/// </summary>
		public int? AdminOperationCount
		{
			set{ _adminoperationcount=value;}
			get{return _adminoperationcount;}
		}
		/// <summary>
		/// 读者操作次数
		/// </summary>
		public int? ReaderOperationCount
		{
			set{ _readeroperationcount=value;}
			get{return _readeroperationcount;}
		}
		/// <summary>
		/// 其他操作次数
		/// </summary>
		public int? OtherOperationCount
		{
			set{ _otheroperationcount=value;}
			get{return _otheroperationcount;}
		}
		/// <summary>
		/// 服务操作次数
		/// </summary>
		public int? ServerOperationCount
		{
			set{ _serveroperationcount=value;}
			get{return _serveroperationcount;}
		}
		/// <summary>
		/// 是否违规
		/// </summary>
		public bool IsViolation
		{
			set{ _isviolation=value;}
			get{return _isviolation;}
		}
		#endregion Model

	}
}

