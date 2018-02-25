using System;
using System.Collections.Generic;
namespace AMS.Model
{
	/// <summary>
	/// AMS_Campus:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_Campus
	{
		public AMS_Campus()
		{}
		#region Model
		private int _id;
		private string _number="";
		private int _schoolid=-1;
		private string _name="";
		private string _describe="";
        private List<AMS_Device> _Device = new List<AMS_Device>();
        /// <summary>
        /// 设备列表
        /// </summary>
        public List<AMS_Device> Device
        {
            get { return _Device; }
            set { _Device = value; }
        }
		/// <summary>
		/// 校区ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 校区编号
		/// </summary>
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
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
		/// 校区名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 校区描述
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
        /// <summary>
        /// 校区地址
        /// </summary>
        public string Address
        {
            get;
            set;
        }
		#endregion Model

	}
}

