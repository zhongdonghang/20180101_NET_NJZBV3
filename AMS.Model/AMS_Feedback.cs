using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_Feedback:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_Feedback
	{
		public AMS_Feedback()
		{}
		#region Model
		private int _id;
		private string _cardno;
		private string _schoolid;
		private string _remark;
		private DateTime? _submittime;
		/// <summary>
		/// 反馈ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
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
		/// 学校ID
		/// </summary>
		public string SchoolId
		{
			set{ _schoolid=value;}
			get{return _schoolid;}
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
		/// 提交时间
		/// </summary>
		public DateTime? SubmitTime
		{
			set{ _submittime=value;}
			get{return _submittime;}
		}
		#endregion Model

	}
}

