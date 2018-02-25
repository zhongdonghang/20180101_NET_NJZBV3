/*******************************************
 * 作者：王昊天
 * 创建时间：2013-6-3
 * 说明：功能菜单
 * 修改人：
 * 修改日期：
 * *****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 根据用户ID获取菜单
        /// </summary>
        /// <param name="LoginID"></param>
        /// <returns></returns>
        [OperationContract]
        List<SysMenuInfo> GetUserMenus(string LoginID);
        /// <summary>
        /// 获取角色功能菜单
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <returns></returns>
        [OperationContract]
        List<SysMenuInfo> GetUserRoleMenus(string RoleID);
        /// <summary>
        /// 获取功能菜单
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SysMenuInfo> GetMenusList();
        /// <summary>
        /// 根据MenuID获取功能菜单
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SysMenuInfo> GetMenusListByMenuId(string menuId);
        /// <summary>
        /// 修改功能菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMenus(SysMenuInfo model);
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMenus(SysMenuInfo model);
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMenus(SysMenuInfo model);
    }
}
