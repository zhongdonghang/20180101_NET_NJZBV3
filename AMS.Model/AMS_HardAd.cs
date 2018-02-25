using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_HardAd:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_HardAd
	{
		public AMS_HardAd()
		{}
		#region Model
		private int _id;
		private string _name;
		private int? _operator;
		private int _customerid;
		private string _number;
		private DateTime? _effectdate;
		private DateTime? _enddate;
		private byte[] _adimage;
		private string _describe;
        private string _OperatorName;

        /// <summary>
        /// 操作者
        /// </summary>
        public string OperatorName
        {
            get { return _OperatorName; }
            set { _OperatorName = value; }
        }
		/// <summary>
		/// 硬广ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 硬广名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		/// 客户ID
		/// </summary>
		public int CustomerId
		{
			set{ _customerid=value;}
			get{return _customerid;}
		}
		/// <summary>
		/// 硬广编号
		/// </summary>
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 硬广发布时间
		/// </summary>
		public DateTime? EffectDate
		{
			set{ _effectdate=value;}
			get{return _effectdate;}
		}
		/// <summary>
		/// 硬广结束时间
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 硬广图片
		/// </summary>
		public byte[] AdImage
		{
			set{ _adimage=value;}
			get{return _adimage;}
		}
		/// <summary>
		/// 硬广描述
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
		#endregion Model

	}
}

