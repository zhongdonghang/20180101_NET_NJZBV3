using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvertManage.Model
{
    /// <summary>
    /// 校区
    /// </summary>
    [Serializable]
    public class AMS_CampusModel
    {
        private int _id;
		private string _number;
		private int _schoolid;
		private string _name;
		private string _describe;
		private string _schoolnum;
		private string _schoolname;
		private string _schooldtuip;
		private string _schooldescribe;
		private string _schoolconnectionstring;
        #region 属性
        /// <summary>
		/// 校区Id
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
		/// 所在学校Id
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
		/// 学校编号
		/// </summary>
		public string SchoolNum
		{
			set{ _schoolnum=value;}
			get{return _schoolnum;}
		}
		/// <summary>
		/// 学校名称
		/// </summary>
		public string SchoolName
		{
			set{ _schoolname=value;}
			get{return _schoolname;}
		}
		/// <summary>
		/// 学校数据中转服务Ip
		/// </summary>
		public string SchoolDTUIp
		{
			set{ _schooldtuip=value;}
			get{return _schooldtuip;}
		}
		/// <summary>
		/// 学校描述
		/// </summary>
		public string SchoolDescribe
		{
			set{ _schooldescribe=value;}
			get{return _schooldescribe;}
		}
		/// <summary>
		/// 学校服务连接字符串
		/// </summary>
		public string SchoolConnectionString
		{
			set{ _schoolconnectionstring=value;}
			get{return _schoolconnectionstring;}
		}
		#endregion Model
    }
}
