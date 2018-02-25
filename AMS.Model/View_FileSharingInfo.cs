using System;
namespace AMS.Model
{
	/// <summary>
	/// View_FileSharingInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_FileSharingInfo
	{
		public View_FileSharingInfo()
		{}
		#region Model
		private int? _id;
		private string _name;
		private string _remark;
		private int? _filetype;
		private int? _upmanid;
		private string _filepath;
		private int? _downloadcount;
		private int _userid;
		private string _loginid;
		private string _username;
		private string _branchname;
		/// <summary>
		/// 
		/// </summary>
		public int? Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FileType
		{
			set{ _filetype=value;}
			get{return _filetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UpManID
		{
			set{ _upmanid=value;}
			get{return _upmanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FilePath
		{
			set{ _filepath=value;}
			get{return _filepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DownLoadCount
		{
			set{ _downloadcount=value;}
			get{return _downloadcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LoginId
		{
			set{ _loginid=value;}
			get{return _loginid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BranchName
		{
			set{ _branchname=value;}
			get{return _branchname;}
		}
		#endregion Model

	}
}

