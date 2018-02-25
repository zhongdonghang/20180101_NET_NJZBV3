/********************************
 * 作者：王昊天
 * 创建时间：2013-6-5
 * 说明：角色的菜单操作
 * 修改人：
 * 修改时间：
 * ******************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.IWCFService;
using SeatManage.WcfAccessProxy;
using System.ServiceModel;
using SeatManage.SeatManageComm;
namespace SeatManage.Bll
{
    public class SysMenu
    {
        /// <summary>
        /// 根据用户ID获取菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.SysMenuInfo> GetUserMenus(string LoginID)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetUserMenus(LoginID);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取菜单失败：" + ex.Message);
                return new List<SysMenuInfo>();

            }
           
        }
        /// <summary>
        /// 根据权限获取菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.SysMenuInfo> GetUserRoleMenus(string RoleID)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetUserRoleMenus(RoleID);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取菜单失败：" + ex.Message);
                return new List<SysMenuInfo>();

            }
           
        }
        /// <summary>
        /// 根据MenuID获取菜单
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.SysMenuInfo> GetMenusList(string menuId)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetMenusListByMenuId(menuId);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("获取菜单失败：" + ex.Message);
                return new List<SysMenuInfo>();
            }
           
        }
        /// <summary>
        /// 获取全部菜单
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.SysMenuInfo> GetMenusList()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetMenusList();
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("获取菜单失败：" + ex.Message);
                return new List<SysMenuInfo>();
            }
            
        }
        /// <summary>
        /// 添加一个菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns>成功返回true</returns>
        public static bool AddMenus(SeatManage.ClassModel.SysMenuInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddMenus(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加菜单失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns>成功返回true</returns>
        public static bool UpdateMenus(SeatManage.ClassModel.SysMenuInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateMenus(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("更新菜单失败：" + ex.Message);
                return false;
            }
            
        }

        /// <summary>
        /// 删除菜单，注意会连表删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns>成功返回true</returns>
        public static bool DeleteMenus(SeatManage.ClassModel.SysMenuInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteMenus(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("删除菜单失败：" + ex.Message);
                return false;
            }
        }
    }
}
