using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_SlipPrintInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_SlipPrintInfo
	{
		public AMS_SlipPrintInfo()
		{}
		#region Model
		private int _id;
		private int? _slipcustomerid;
		private int? _campusid;
		private DateTime? _date;
		private int? _printamount;
		private int? _lookoveramount;
		/// <summary>
		/// 优惠券打印ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 优惠券ID
		/// </summary>
		public int? SlipCustomerId
		{
			set{ _slipcustomerid=value;}
			get{return _slipcustomerid;}
		}
		/// <summary>
		/// 校区ID
		/// </summary>
		public int? CampusId
		{
			set{ _campusid=value;}
			get{return _campusid;}
		}
		/// <summary>
		/// 打印日期
		/// </summary>
		public DateTime? Date
		{
			set{ _date=value;}
			get{return _date;}
		}
		/// <summary>
		/// 打印总计
		/// </summary>
		public int? PrintAmount
		{
			set{ _printamount=value;}
			get{return _printamount;}
		}
		/// <summary>
		/// 翻看次数
		/// </summary>
		public int? LookOverAmount
		{
			set{ _lookoveramount=value;}
			get{return _lookoveramount;}
		}
		#endregion Model

	}
}

