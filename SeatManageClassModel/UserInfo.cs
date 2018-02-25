using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    [Serializable]
    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserInfo
    {
        private string _loginId = "";
        /// <summary>
        /// 登录ID
        /// </summary>
        public string LoginId
        {
            get
            { return _loginId; }
            set
            { _loginId = value; }
        }

        private string _userName = " ";
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get
            { return _userName; }
            set
            { _userName = value; }
        }

        private SeatManage.EnumType.UserType _UserType;
        /// <summary>
        /// 用户类型
        /// </summary>
        public EnumType.UserType UserType
        {
            get
            { return _UserType; }
            set { _UserType = value; }
        }

        private string _password = "";
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password
        {
            get
            { return _password; }
            set
            { _password = value; }
        }
        private string _LockIPAdress = "0.0.0.0";
        /// <summary>
        /// 锁定的IP
        /// </summary>
        public string LockIPAdress
        {
            get { return _LockIPAdress; }
            set { _LockIPAdress = value; }
        }
        private LogStatus _IsUsing = LogStatus.Valid;
        /// <summary>
        /// 是否启用
        /// </summary>
        public LogStatus IsUsing
        {
            get { return _IsUsing; }
            set { _IsUsing = value; }
        }
        private string _Remark = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        private List<int> _ReloID = new List<int>();
        /// <summary>
        /// 角色ID
        /// </summary>
        public List<int> ReloID
        {
            get { return _ReloID; }
            set { _ReloID = value; }
        }

        private bool _IsAdmin = false;
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }
        List<SysMenuInfo> _UserMenus = new List<SysMenuInfo>();
        /// <summary>
        /// 用户的功能菜单
        /// </summary>
        public List<SysMenuInfo> UserMenus
        {
            get { return _UserMenus; }
            set { _UserMenus = value; }
        }

        private ManagerPotency _UserRoomRight = new ManagerPotency();
        /// <summary>
        /// 读者的管理阅览室的权限
        /// </summary>
        public ManagerPotency UserRoomRight
        {
            get { return _UserRoomRight; }
            set { _UserRoomRight = value; }
        }

    }
}
