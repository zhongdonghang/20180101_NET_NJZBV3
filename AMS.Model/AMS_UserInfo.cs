using System;
namespace AMS.Model
{
    /// <summary>
    /// AMS_UserInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AMS_UserInfo
    {
        public AMS_UserInfo()
        { }
        #region Model
        private int _id = -1;
        private string _loginid = "";
        private string _userpwd = "";
        private string _branchname = "";
        private string _username = "";
        private string _remark = "";
        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string LoginId
        {
            set { _loginid = value; }
            get { return _loginid; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd
        {
            set { _userpwd = value; }
            get { return _userpwd; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string BranchName
        {
            set { _branchname = value; }
            get { return _branchname; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}

