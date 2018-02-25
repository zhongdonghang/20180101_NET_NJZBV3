using System;
namespace AMS.Model
{
	/// <summary>
	/// FileSharingInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FileSharingInfo
	{
		public FileSharingInfo()
		{}
		#region Model
		private int _id;
		private string _name="";
		private string _remark="";
		private int? _filetype;
		private int? _upmanid;
		private string _filepath="";
		private int? _downloadcount;
        private string _Size="0";
        private string _UpMan;

        public string UpMan
        {
            get { return _UpMan; }
            set { _UpMan = value; }
        }

        public string Size
        {
            get { return _Size; }
            set { _Size = value; }
        }
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
		#endregion Model

	}
}

