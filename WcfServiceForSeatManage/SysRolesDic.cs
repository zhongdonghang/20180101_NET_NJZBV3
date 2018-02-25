/******************************************
 * 作者：王昊天
 * 创建时间：2013-6-5
 * 说明：角色操作
 * 修改人：
 * 修改时间：
 * ******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;
using System.Data.SqlClient; 
using SeatManage.ClassModel;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        SeatManage.DAL.SysRolesDic sysrolesdic_dal = new SeatManage.DAL.SysRolesDic();
        SeatManage.DAL.SysFuncRights sysfuncrights_dal = new SeatManage.DAL.SysFuncRights();

        /// <summary>
        /// 根据登录名获取权限ID
        /// </summary>
        /// <param name="LoginID"></param>
        /// <returns></returns>
        public List<int> GetRoleID(string LoginID)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(LoginID))
            {
                strWhere.Append(" LoginID='" + LoginID + "'");
            }
            List<int> list = new List<int>();
            try
            {
                DataSet ds = sysemprole_dal.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(int.Parse(dr["RoleID"].ToString()));
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="RolesID">角色ID，不输默认获取全部</param>
        /// <returns></returns>
        public List<SysRolesDicInfo> GetRolesInfo(string RolesID, string RoleName)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(RolesID))
            {
                strWhere.Append(" RoleID='" + RolesID + "'");
            }
            if (!string.IsNullOrEmpty(RoleName))
            {
                if (!string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" and RoleName='" + RoleName + "'");
                }
                else
                {
                    strWhere.Append(" RoleName='" + RoleName + "'");
                }
            }
            List<SysRolesDicInfo> list = new List<SysRolesDicInfo>();
            try
            {
                DataSet ds = sysrolesdic_dal.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(DataRowToSysRolesDicInfo(dr));
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddRole(SysRolesDicInfo model)
        {
            try
            {
                if (IsExistsSameRoleName(model, false))
                {
                    return "角色名称重复";
                }
                else
                {
                    int newid = sysrolesdic_dal.Add(model);
                    model.RoleID = newid.ToString();
                    if (newid > 0)
                    {
                        sysfuncrights_dal.DeleteFuncRights(model);
                        foreach (SysMenuInfo smi in model.RoleMenu)
                        {
                            sysfuncrights_dal.AddFuncRight(model.RoleID, smi.MenuID);
                            foreach (SysMenuInfo smic in smi.ChildMenu)
                            {
                                sysfuncrights_dal.AddFuncRight(model.RoleID, smic.MenuID);
                            }
                        }
                        return "";
                    }
                    else
                    {
                        return "操作失败";
                    }

                }
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateRole(SysRolesDicInfo model)
        {
            try
            {
                if (IsExistsSameRoleName(model, true))
                {
                    return "角色名称重复";
                }
                else
                {
                    if (sysrolesdic_dal.Update(model))
                    {
                        sysfuncrights_dal.DeleteFuncRights(model);
                        foreach (SysMenuInfo smi in model.RoleMenu)
                        {
                            sysfuncrights_dal.AddFuncRight(model.RoleID, smi.MenuID);
                            foreach (SysMenuInfo smic in smi.ChildMenu)
                            {
                                sysfuncrights_dal.AddFuncRight(model.RoleID, smic.MenuID);
                            }
                        }
                        return "";
                    }
                    else
                    {
                        return "操作失败";
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteRole(SysRolesDicInfo model)
        {
            try
            {
                return sysrolesdic_dal.Delete(model);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 数据行转成实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private SysRolesDicInfo DataRowToSysRolesDicInfo(DataRow dr)
        {
            SysRolesDicInfo srd = new SysRolesDicInfo();
            srd.RoleID = dr["RoleID"].ToString();
            srd.RoleName = dr["RoleName"].ToString();
            srd.IsLock = bool.Parse(dr["IsLock"].ToString());
            return srd;
        }
        /// <summary>
        /// 判断是否有重复的角色
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool IsExistsSameRoleName(SysRolesDicInfo model, bool mode)
        {
            StringBuilder strWhere = new StringBuilder();
            if (mode)
            {
                strWhere.Append(" RoleID<>'" + model.RoleID + "'");
            }
            if (model != null)
            {
                if (!string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" and RoleName='" + model.RoleName + "'");
                }
                else
                {
                    strWhere.Append(" RoleName='" + model.RoleName + "'");
                }
            }
            else
            {
                return false;
            }
            List<SysRolesDicInfo> list = new List<SysRolesDicInfo>();
            try
            {
                DataSet ds = sysrolesdic_dal.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
