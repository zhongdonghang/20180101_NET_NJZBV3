using System;
namespace AMS.Model
{
	/// <summary>
	/// View_PlayList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_PlayList
	{
		public View_PlayList()
		{}
		#region Model
		private int _id;
		private string _number;
		private string _playlistname;
		private DateTime? _releasedate;
		private DateTime? _effectdate;
		private DateTime? _enddate;
		private string _describe;
		private string _playlist;
		private string _operatorloginid;
		private string _operatorpwd;
		private string _operatorbranchname;
		private string _operatorname;
		private string _operatorremark;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PlayListName
		{
			set{ _playlistname=value;}
			get{return _playlistname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReleaseDate
		{
			set{ _releasedate=value;}
			get{return _releasedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EffectDate
		{
			set{ _effectdate=value;}
			get{return _effectdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PlayList
		{
			set{ _playlist=value;}
			get{return _playlist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorLoginId
		{
			set{ _operatorloginid=value;}
			get{return _operatorloginid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorPwd
		{
			set{ _operatorpwd=value;}
			get{return _operatorpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorBranchName
		{
			set{ _operatorbranchname=value;}
			get{return _operatorbranchname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorName
		{
			set{ _operatorname=value;}
			get{return _operatorname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorRemark
		{
			set{ _operatorremark=value;}
			get{return _operatorremark;}
		}
		#endregion Model

	}
}

