using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_CommandList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_CommandList
	{
		public AMS_CommandList()
		{}
		#region Model
		private int _id;
		private int _schoolid;
        private AMS.Model.Enum.CommandType _command;
		private int? _operator;
		private int? _commandid;
		private DateTime _releasetime;
		private DateTime? _finishtime;
		private int? _finishflag=0;
		/// <summary>
		/// 下发命令ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 学校ID
		/// </summary>
		public int SchoolId
		{
			set{ _schoolid=value;}
			get{return _schoolid;}
		}
		/// <summary>
		/// 下发命令
		/// </summary>
        public AMS.Model.Enum.CommandType Command
		{
			set{ _command=value;}
			get{return _command;}
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
		/// 下发命令编号
		/// </summary>
		public int? CommandId
		{
			set{ _commandid=value;}
			get{return _commandid;}
		}
		/// <summary>
		/// 发布时间
		/// </summary>
		public DateTime ReleaseTime
		{
			set{ _releasetime=value;}
			get{return _releasetime;}
		}
		/// <summary>
		/// 完成时间
		/// </summary>
		public DateTime? FinishTime
		{
			set{ _finishtime=value;}
			get{return _finishtime;}
		}
		/// <summary>
		/// 下发状态
		/// </summary>
		public int? FinishFlag
		{
			set{ _finishflag=value;}
			get{return _finishflag;}
		}
		#endregion Model

	}
}

