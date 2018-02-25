using System;
namespace AMS.Model
{
    /// <summary>
    /// AMS_CallBackErrorInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AMS_CallBackErrorInfo
    {
        public AMS_CallBackErrorInfo()
        { }
        #region Model
        private int _id;
        private int _schoolid;
        private string _fbperson;
        private int? _markmanid;
        private DateTime? _fbtime;
        private int? _solvemanid;
        private DateTime? _solvetime;
        private string _solveway;
        private int? _problemtype;
        private string _fbdescribe;
        private string _solveman;
        private string _markman;
        private string _schoolname;
        private string _problemame;
        private int _solvestatic=-1;



        /// <summary>
        /// 问题类型名称
        /// </summary>
        public string Problemame
        {
            get { return _problemame; }
            set { _problemame = value; }
        }


        /// <summary>
        /// 解决状态名称
        /// </summary>
        public int Solvestatic
        {
            get { return _solvestatic; }
            set { _solvestatic = value; }
        }


        /// <summary>
        /// 反馈信息ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 学校ID
        /// </summary>
        public int SchoolId
        {
            set { _schoolid = value; }
            get { return _schoolid; }
        }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string Schoolname
        {
            get { return _schoolname; }
            set { _schoolname = value; }
        }
        /// <summary>
        /// 记录人
        /// </summary>
        public string Markman
        {
            get { return _markman; }
            set { _markman = value; }
        }
        /// <summary>
        /// 解决者
        /// </summary>
        public string Solveman
        {
            get { return _solveman; }
            set { _solveman = value; }
        }
        /// <summary>
        /// 反馈人
        /// </summary>
        public string FbPerson
        {
            set { _fbperson = value; }
            get { return _fbperson; }
        }
        /// <summary>
        /// 记录人ID
        /// </summary>
        public int? MarkManID
        {
            set { _markmanid = value; }
            get { return _markmanid; }
        }
        /// <summary>
        /// 反馈时间
        /// </summary>
        public DateTime? FbTime
        {
            set { _fbtime = value; }
            get { return _fbtime; }
        }
        /// <summary>
        /// 解决人ID
        /// </summary>
        public int? SolveManID
        {
            set { _solvemanid = value; }
            get { return _solvemanid; }
        }

        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime? SolveTime
        {
            set { _solvetime = value; }
            get { return _solvetime; }
        }
        /// <summary>
        /// 解决方法
        /// </summary>
        public string SolveWay
        {
            set { _solveway = value; }
            get { return _solveway; }
        }
        /// <summary>
        /// 问题类型
        /// </summary>
        public int? ProblemType
        {
            set { _problemtype = value; }
            get { return _problemtype; }
        }
        /// <summary>
        /// 反馈描述
        /// </summary>
        public string FbDescribe
        {
            set { _fbdescribe = value; }
            get { return _fbdescribe; }
        }

        #endregion Model

    }
}

