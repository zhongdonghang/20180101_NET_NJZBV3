/******************************************
 * 作者：罗晨阳
 * 创建时间：2013-6-5
 * 说明：功能菜单操作
 * 修改人：
 * 修改时间：
 * ******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SystemSet
{
    public partial class MenuManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!OpVerifiction())
                {
                    Response.Write("请使用正常方式访问网站！");
                    Response.End();
                }
                BindSysMenu();
                btnAddMenu.OnClientClick = WindowEdit.GetShowReference("MenuEdit.aspx?flag=add", "添加菜单");//为btnAddMenu新增点击事件
            }
        }
        /// <summary>
        /// 绑定系统所有菜单到gird
        /// </summary>
        private void BindSysMenu()
        {
            GridSysMenu.DataSource = GetSysMenu();
            GridSysMenu.DataBind();
        }
        /// <summary>
        /// 获取当前系统所有菜单datatable
        /// </summary>
        /// <returns></returns>
        private DataTable GetSysMenu()
        {
            List<SeatManage.ClassModel.SysMenuInfo> listSysMenu = new List<SeatManage.ClassModel.SysMenuInfo>();
            listSysMenu = SeatManage.Bll.SysMenu.GetMenusList();
            DataTable dt = new DataTable();
            DataColumn MenuID = new DataColumn("MenuID", typeof(int));
            DataColumn Mainnum = new DataColumn("Mainnum", typeof(string));
            DataColumn ModSeq = new DataColumn("ModSeq", typeof(string));
            DataColumn ItemSeq = new DataColumn("ItemSeq", typeof(string));
            DataColumn Mcaption = new DataColumn("Mcaption", typeof(string));
            DataColumn MenuLv = new DataColumn("MenuLv", typeof(int));
            dt.Columns.Add(MenuID);
            dt.Columns.Add(Mainnum);
            dt.Columns.Add(ModSeq);
            dt.Columns.Add(ItemSeq);
            dt.Columns.Add(Mcaption);
            dt.Columns.Add(MenuLv);

            foreach (SeatManage.ClassModel.SysMenuInfo list in listSysMenu)
            {
                DataRow row = dt.NewRow();
                row["MenuID"] = list.MenuID;
                row["Mainnum"] = list.MainNum;
                row["ModSeq"] = "一级菜单";
                row["ItemSeq"] = list.Index;
                row["Mcaption"] = list.MenuName;
                row["MenuLv"] = list.MenuLv;
                dt.Rows.Add(row);
                foreach (SeatManage.ClassModel.SysMenuInfo listChild in list.ChildMenu)
                {
                    DataRow rowChild = dt.NewRow();
                    rowChild["MenuID"] = listChild.MenuID;
                    rowChild["Mainnum"] = listChild.MainNum;
                    rowChild["ModSeq"] = listChild.FuncPageNum;
                    rowChild["ItemSeq"] = listChild.Index;
                    rowChild["Mcaption"] = listChild.MenuName;
                    rowChild["MenuLv"] = listChild.MenuLv;
                    dt.Rows.Add(rowChild);
                }
            }
            return dt;
        }

        //窗体关闭事件
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindSysMenu();
        }
        //删除菜单按钮点击事件
        protected void btnDel_Click(object sender, EventArgs e)
        {
            FineUI.CheckBoxField chkFild = (FineUI.CheckBoxField)GridSysMenu.FindColumn("CheckBoxField1");
            SeatManage.ClassModel.SysMenuInfo modelSysMenuInfo = new SeatManage.ClassModel.SysMenuInfo();
            int selectCount = GridSysMenu.SelectedRowIndexArray.Length;
            if (selectCount > 0)
            {
                for (int i = 0; i < selectCount; i++)
                {
                    int rowIndex = GridSysMenu.SelectedRowIndexArray[i];
                    FineUI.GridRow row = GridSysMenu.Rows[rowIndex] as FineUI.GridRow;
                    modelSysMenuInfo.MenuID = int.Parse(row.DataKeys[0].ToString());
                    if (row.DataKeys[1].ToString() == "一级菜单")
                    {
                        List<SeatManage.ClassModel.SysMenuInfo> listSysMenu = SeatManage.Bll.SysMenu.GetMenusList();
                        foreach (SeatManage.ClassModel.SysMenuInfo selectmenu in listSysMenu)
                        {
                            if (selectmenu.MenuID == modelSysMenuInfo.MenuID)
                            {
                                modelSysMenuInfo = selectmenu;
                                break;
                            }
                        }
                        foreach (SeatManage.ClassModel.SysMenuInfo childmenu in modelSysMenuInfo.ChildMenu)
                        {
                            if (!SeatManage.Bll.SysMenu.DeleteMenus(childmenu))
                            {
                                FineUI.Alert.ShowInTop("删除子菜单失败！");
                                BindSysMenu();
                                return;
                            }
                        }
                        if (SeatManage.Bll.SysMenu.DeleteMenus(modelSysMenuInfo))
                        {
                            FineUI.Alert.ShowInTop("删除成功！");
                            BindSysMenu();

                        }
                        else
                        {
                            FineUI.Alert.ShowInTop("删除失败！");
                            BindSysMenu();
                        }
                    }
                    else
                    {
                        if (SeatManage.Bll.SysMenu.DeleteMenus(modelSysMenuInfo))
                        {
                            FineUI.Alert.ShowInTop("删除成功！");

                        }
                        else
                        {
                            FineUI.Alert.ShowInTop("删除失败！");
                        }
                    }

                }
                BindSysMenu();
            }
        }

        protected void GridSysMenu_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ActionDelete")
            {
                SeatManage.ClassModel.SysMenuInfo modelSysMenuInfo = new SeatManage.ClassModel.SysMenuInfo();
                modelSysMenuInfo.MenuID = Convert.ToInt32(GridSysMenu.Rows[e.RowIndex].DataKeys[0]);
                if (GridSysMenu.Rows[e.RowIndex].DataKeys[1].ToString() == "一级菜单")
                {
                    List<SeatManage.ClassModel.SysMenuInfo> listSysMenu = SeatManage.Bll.SysMenu.GetMenusList();
                    foreach (SeatManage.ClassModel.SysMenuInfo selectmenu in listSysMenu)
                    {
                        if (selectmenu.MenuID == modelSysMenuInfo.MenuID)
                        {
                            modelSysMenuInfo = selectmenu;
                            break;
                        }
                    }
                    foreach (SeatManage.ClassModel.SysMenuInfo childmenu in modelSysMenuInfo.ChildMenu)
                    {
                        if (!SeatManage.Bll.SysMenu.DeleteMenus(childmenu))
                        {
                            FineUI.Alert.ShowInTop("删除子菜单失败！");
                            BindSysMenu();
                            return;
                        }
                    }
                    if (SeatManage.Bll.SysMenu.DeleteMenus(modelSysMenuInfo))
                    {
                        FineUI.Alert.ShowInTop("删除成功！");
                        BindSysMenu();
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("删除失败！");
                        BindSysMenu();
                    }
                }
                else
                {
                    if (SeatManage.Bll.SysMenu.DeleteMenus(modelSysMenuInfo))
                    {
                        FineUI.Alert.ShowInTop("删除成功！");
                        BindSysMenu();
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("删除失败！");
                        BindSysMenu();
                    }
                }
            }
        }
    }
}