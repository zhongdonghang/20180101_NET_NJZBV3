using System;
namespace AMS.Model
{
	/// <summary>
	/// SlipReleaseCampus:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SlipReleaseCampus
	{
		public SlipReleaseCampus()
		{}
		#region Model
		private int _id;
		private int _slipcustomerid;
		private int _campusid;
		/// <summary>
		/// 优惠券下发校区ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 优惠券ID
		/// </summary>
		public int SlipCustomerId
		{
			set{ _slipcustomerid=value;}
			get{return _slipcustomerid;}
		}
		/// <summary>
		/// 校区ID
		/// </summary>
		public int CampusId
		{
			set{ _campusid=value;}
			get{return _campusid;}
		}
		#endregion Model

	}
}

