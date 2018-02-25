/********************************
 * 作者：王昊天
 * 创建时间：2013-6-5
 * 说明：角色的操作，权限的的赋予
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
    public class SysRolesDic
    {
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="RoleID">根据角色ID查询，输入null不根据此条件查询</param>
        /// <param name="RoleName">根据角色名称查询，输入null不根据此条件查询</param>
        /// <returns></returns>
        public static List<SysRolesDicInfo> GetRoleList(string RoleID, string RoleName)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetRolesInfo(RoleID, RoleName);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("获取角色信息失败：" + ex.Message);
                return new List<SysRolesDicInfo>();
            }
            
        }
        /// <summary>
        /// 增加新的角色
        /// </summary>
        /// <param name="model">增加角色，并且添加菜单权限</param>
        /// <returns>操作正常返回空值，失败返回错误提示</returns>
        public string AddNewRole(SysRolesDicInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddRole(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加角色失败：" + ex.Message);
                return ex.Message;
            }
           
        }
        /// <summary>
        ///更新角色
        /// </summary>
        /// <param name="model">更新角色，并且修改菜单权限</param>
        /// <returns>操作正常返回空值，失败返回错误提示</returns>
        public string UpdateRole(SysRolesDicInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateRole(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("更新角色：" + ex.Message);
                return ex.Message;
            }
           
        }
        /// <summary>
        /// 删除角色，连表删除，会连同权限一起删除
        /// </summary>
        /// <param name="model">删除的角色</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool DeleteRole(SysRolesDicInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteRole(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("删除角色错误：" + ex.Message);
                return false;
            }
            
        }
    }
}
