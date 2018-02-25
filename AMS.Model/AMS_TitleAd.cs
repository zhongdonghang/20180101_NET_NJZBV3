using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_TitleAd:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_TitleAd
	{
		public AMS_TitleAd()
		{}
		#region Model
		private int _id;
		private string _name;
		private int? _operator;
		private int _customerid;
		private DateTime? _effectdate;
		private DateTime? _enddate;
		private string _adcontent;
        private string _operatorname;
        private string _num;

        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _num; }
            set { _num = value; }
        }

        /// <summary>
        /// 操作者
        /// </summary>
        public string Operatorname
        {
            get { return _operatorname; }
            set { _operatorname = value; }
        }
		/// <summary>
		/// 冠名广告ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 冠名广告名称
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
		/// 发布日期
		/// </summary>
		public DateTime? EffectDate
		{
			set{ _effectdate=value;}
			get{return _effectdate;}
		}
		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 广告内容
		/// </summary>
		public string AdContent
		{
			set{ _adcontent=value;}
			get{return _adcontent;}
		}
		#endregion Model

	}
}

