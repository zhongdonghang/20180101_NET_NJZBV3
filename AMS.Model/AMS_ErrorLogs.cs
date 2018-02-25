using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_ErrorLogs:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_ErrorLogs
	{
		public AMS_ErrorLogs()
		{}
		#region Model
		private int _id;
		private string _remark;
		private DateTime? _submittime;
		/// <summary>
		/// 错误日志编号
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 处理时间
		/// </summary>
		public DateTime? SubmitTime
		{
			set{ _submittime=value;}
			get{return _submittime;}
		}
		#endregion Model

	}
}

