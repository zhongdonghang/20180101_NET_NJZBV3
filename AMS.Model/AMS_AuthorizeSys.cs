using System;
namespace AMS.Model
{
	/// <summary>
	/// AMS_AuthorizeSys:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AMS_AuthorizeSys
	{
		public AMS_AuthorizeSys()
		{}
		#region Model
		private int _id;
		private int _schoolid;
		private bool _authorizestatus;
		private string _interfacetype;
		private DateTime? _effecttime;
		private DateTime? _endtime;
		private string _authorizecode;
		private bool? _iscomorsch;
		private string _describe;
		/// <summary>
		/// 授权ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 学校Id
		/// </summary>
		public int SchoolId
		{
			set{ _schoolid=value;}
			get{return _schoolid;}
		}
		/// <summary>
		/// 是否授权
		/// </summary>
		public bool AuthorizeStatus
		{
			set{ _authorizestatus=value;}
			get{return _authorizestatus;}
		}
		/// <summary>
		/// 接口类型
		/// </summary>
		public string InterfaceType
		{
			set{ _interfacetype=value;}
			get{return _interfacetype;}
		}
		/// <summary>
		/// 授权生效时间
		/// </summary>
		public DateTime? EffectTime
		{
			set{ _effecttime=value;}
			get{return _effecttime;}
		}
		/// <summary>
		/// 授权失效时间
		/// </summary>
		public DateTime? EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 授权码
		/// </summary>
		public string AuthorizeCode
		{
			set{ _authorizecode=value;}
			get{return _authorizecode;}
		}
		/// <summary>
		/// 判断授权对象
		/// </summary>
		public bool? IsComOrSch
		{
			set{ _iscomorsch=value;}
			get{return _iscomorsch;}
		}
		/// <summary>
		/// 授权描述
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
		#endregion Model

	}
}

