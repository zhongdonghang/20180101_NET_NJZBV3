/******************************************
 * 作者：王昊天
 * 创建时间：2013-6-5
 * 说明：菜单操作
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

        SeatManage.DAL.SysMenu sysMenu_Dal = new SeatManage.DAL.SysMenu();
        /// <summary>
        /// 根据用户ID获取菜单
        /// </summary>
        /// <param name="LoginID"></param>
        /// <returns></returns>
        public List<SysMenuInfo> GetUserMenus(string LoginID)
        {
            try
            {
                List<int> rolelist = GetRoleID(LoginID);
                if (rolelist.Count > 0)
                {
                    StringBuilder strWhere = new StringBuilder();
                    for (int i = 0; i < rolelist.Count; i++)
                    {
                        if (i == 0)
                        {
                            strWhere.Append(string.Format(" RoleID in ('{0}'", rolelist[i]));
                        }
                        else if (i != rolelist.Count - 1)
                        {
                            strWhere.Append(string.Format(",'{0}'", rolelist[i]));
                            //continue;
                        }
                        if (i == rolelist.Count - 1)
                        {
                            strWhere.Append(string.Format(",'{0}' ) ", rolelist[i]));
                        }

                    }

                    DataSet ds = sysMenu_Dal.GetRoleMenuList(strWhere.ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<SysMenuInfo> lsm = new List<SysMenuInfo>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SysMenuInfo sm = DataRowViewToSysMenu(dr);
                            if (sm.MenuLv == 1)
                            {
                                bool newitem = true;
                                foreach (SysMenuInfo item in lsm)
                                {
                                    if (item.MenuID == sm.MenuID)
                                    {
                                        newitem = false;
                                        break;
                                    }
                                }
                                if (newitem)
                                {
                                    foreach (DataRow cdr in ds.Tables[0].Rows)
                                    {
                                        SysMenuInfo csm = DataRowViewToSysMenu(cdr);
                                        if (csm.MenuLv == 2 && csm.MainNum == sm.MainNum)
                                        {
                                            bool newchilditem=true;
                                            foreach (SysMenuInfo childitem in sm.ChildMenu)
                                            {
                                                if (childitem.MenuID == csm.MenuID)
                                                {
                                                    newchilditem = false;
                                                    break;
                                                }
                                            }
                                            if (newchilditem)
                                            {
                                                sm.ChildMenu.Add(csm);
                                            }
                                        }
                                    }
                                    lsm.Add(sm);
                                }
                            }
                        }
                        return lsm;
                    }
                    else
                    {
                         return new List<SysMenuInfo>();
                    }
                }
                else
                {
                    return new List<SysMenuInfo>();
                }
            }

            catch
            {
                throw;
            }


        }

        /// <summary>
        /// 获取角色功能菜单
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <returns></returns>
        public List<SysMenuInfo> GetUserRoleMenus(string RoleID)
        {
            if (string.IsNullOrEmpty(RoleID))
            {
                return null;
            }
            string strWhere = "RoleID='" + RoleID + "'";
            try
            {
                DataSet ds = sysMenu_Dal.GetRoleMenuList(strWhere);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<SysMenuInfo> lsm = new List<SysMenuInfo>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        SysMenuInfo sm = DataRowViewToSysMenu(dr);
                        if (sm.MenuLv == 1)
                        {
                            bool newitem = true;
                            foreach (SysMenuInfo item in lsm)
                            {
                                if (item.MenuID == sm.MenuID)
                                {
                                    newitem = false;
                                    break;
                                }
                            }
                            if (newitem)
                            {
                                foreach (DataRow cdr in ds.Tables[0].Rows)
                                {
                                    SysMenuInfo csm = DataRowViewToSysMenu(cdr);
                                    if (csm.MenuLv == 2 && csm.MainNum == sm.MainNum)
                                    {
                                        bool newchilditem = true;
                                        foreach (SysMenuInfo childitem in sm.ChildMenu)
                                        {
                                            if (childitem.MenuID == csm.MenuID)
                                            {
                                                newchilditem = false;
                                                break;
                                            }
                                        }
                                        if (newchilditem)
                                        {
                                            sm.ChildMenu.Add(csm);
                                        }
                                    }
                                }
                                lsm.Add(sm);
                            }
                        }
                    }
                    return lsm;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 根据MenuID获取功能菜单
        /// </summary>
        /// <returns></returns>
        public List<SysMenuInfo> GetMenusListByMenuId(string menuId)
        {
            string strWhere = " MenuID=" + menuId;
            try
            {
                DataSet ds = sysMenu_Dal.GetList(strWhere);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<SysMenuInfo> lsm = new List<SysMenuInfo>();
                    SysMenuInfo sm = DataRowTableToSysMenu(ds.Tables[0].Rows[0]);
                    lsm.Add(sm);
                    return lsm;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取功能菜单
        /// </summary>
        /// <returns></returns>
        public List<SysMenuInfo> GetMenusList()
        {
            try
            {
                DataSet ds = sysMenu_Dal.GetList(null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<SysMenuInfo> lsm = new List<SysMenuInfo>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        SysMenuInfo sm = DataRowTableToSysMenu(dr);
                        if (sm.MenuLv == 1)
                        {
                            foreach (DataRow cdr in ds.Tables[0].Rows)
                            {
                                SysMenuInfo csm = DataRowTableToSysMenu(cdr);
                                if (csm.MenuLv == 2 && csm.MainNum == sm.MainNum)
                                {
                                    sm.ChildMenu.Add(csm);
                                }
                            }
                            lsm.Add(sm);
                        }
                    }
                    return lsm;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 修改功能菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMenus(SysMenuInfo model)
        {
            try
            {
                return sysMenu_Dal.Update(model);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMenus(SysMenuInfo model)
        {
            try
            {
                return sysMenu_Dal.Add(model);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteMenus(SysMenuInfo model)
        {
            try
            {
                return sysMenu_Dal.delete(model);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 列转换成model
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private SysMenuInfo DataRowViewToSysMenu(DataRow dr)
        {
            SysMenuInfo sm = new SysMenuInfo();
            //ROLEID, ROLENAME, MenuLv, MCaption, Mainnum, ModSeq, MenuLink, MenuImagePath, ItemSeq
            sm.MenuID = int.Parse(dr["MenuID"].ToString());
            sm.MainNum = dr["Mainnum"].ToString();
            sm.MenuName = dr["MCaption"].ToString();
            sm.MenuLink = dr["MenuLink"].ToString();
            sm.FuncPageNum = dr["ModSeq"].ToString();
            sm.Index = int.Parse(dr["ItemSeq"].ToString());
            sm.MenuLv = int.Parse(dr["MenuLv"].ToString());
            sm.ImageUrl = dr["MenuImagePath"].ToString();
            return sm;
        }
        /// <summary>
        /// 列转换成moel
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private SysMenuInfo DataRowTableToSysMenu(DataRow dr)
        {
            SysMenuInfo sm = new SysMenuInfo();
            //ROLEID, ROLENAME, MenuLv, MCaption, Mainnum, ModSeq, MenuLink, MenuImagePath, ItemSeq
            sm.MenuID = int.Parse(dr["MenuID"].ToString());
            sm.MainNum = dr["Mainnum"].ToString();
            sm.MenuName = dr["MCaption"].ToString();
            sm.FuncPageNum = dr["ModSeq"].ToString();
            sm.Index = int.Parse(dr["ItemSeq"].ToString());
            sm.MenuLv = int.Parse(dr["MenuLv"].ToString());
            sm.ImageUrl = dr["MenuImagePath"].ToString();
            return sm;
        }
    }
}
