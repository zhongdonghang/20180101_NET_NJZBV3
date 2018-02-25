/***********************************************
 * 作者：王昊天
 * 创建时间：2013-6-5
 * 说明：角色类
 * 
 * 
 * *********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class SysRolesDicInfo
    {
        private string _RoleID = "";
        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        private string _RoleName = "";
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        private bool _IsLock = false;
        /// <summary>
        /// 是否锁定角色
        /// </summary>
        public bool IsLock
        {
            get { return _IsLock; }
            set { _IsLock = value; }
        }
        private List<SysMenuInfo> _RoleMenu = new List<SysMenuInfo>();
        /// <summary>
        /// 角色的功能权限
        /// </summary>
        public List<SysMenuInfo> RoleMenu
        {
            get { return _RoleMenu; }
            set { _RoleMenu = value; }
        }
    }
}
