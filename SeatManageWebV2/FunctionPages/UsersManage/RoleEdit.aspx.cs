using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.UsersManage
{
    public partial class RoleEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string roleId = Request.QueryString["roleId"];
            string roleName = Server.UrlDecode(Request.QueryString["roleName"]);
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "RoleManage.aspx" && pageName != "FormSYS.aspx")
                    {
                        WriteLogs(url);
                        Response.Write("请通过正确方式访问网站！");
                        Response.End();
                        return;
                    }
                }
                else
                {
                    WriteLogs(HttpContext.Current.Request.Url.AbsoluteUri);
                    Response.Write("请通过正确方式访问网站！");
                    Response.End();
                    return;
                }
                LoadData();
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["flag"] == "edit")
                    {
                        txtRoleName.Text = roleName;
                        BindCheckedBox(roleId);
                    }
                }
            }
        }
        /// <summary>
        /// 绑定菜单
        /// </summary>
        private void LoadData()
        {
            List<SeatManage.ClassModel.SysMenuInfo> listSysMenu = SeatManage.Bll.SysMenu.GetMenusList();
            if (listSysMenu != null)
            {
                foreach (SeatManage.ClassModel.SysMenuInfo list in listSysMenu)
                {
                    FineUI.TreeNode node = new FineUI.TreeNode();
                    node.NodeID = list.MenuID.ToString();
                    node.Target = list.MainNum;
                    node.Text = list.MenuName;
                    node.Expanded = true;
                    node.EnableCheckBox = true;
                    node.AutoPostBack = true;
                    treeMenu.Nodes.Add(node);
                    foreach (SeatManage.ClassModel.SysMenuInfo listChild in list.ChildMenu)
                    {
                        FineUI.TreeNode nodeChild = new FineUI.TreeNode();
                        nodeChild.NodeID = listChild.MenuID.ToString();
                        nodeChild.Text = listChild.MenuName;
                        nodeChild.Target = list.MainNum;
                        nodeChild.Expanded = true;
                        nodeChild.EnableCheckBox = true;
                        nodeChild.AutoPostBack = true;
                        node.Nodes.Add(nodeChild);
                    }
                }
            }
        }
        #region 复选框选择事件
        protected void TreeMenu_NodeCheck(object sender, TreeCheckEventArgs e)
        {
            if (!e.Node.Leaf)
            {
                CheckTreeNode(e.Node.Nodes, e.Checked);
            }
            else
            {
                FineUI.Tree tree = e.Node.TreeInstance;
                foreach (FineUI.TreeNode node in tree.Nodes)
                {
                    if (node.Target == e.Node.Target)
                    {
                        node.Checked = true;
                    }
                }
            }
        }
        private void CheckTreeNode(FineUI.TreeNodeCollection nodes, bool isChecked)
        {
            foreach (FineUI.TreeNode node in nodes)
            {
                node.Checked = isChecked;
                if (!node.Leaf)
                {
                    CheckTreeNode(node.Nodes, isChecked);
                }
            }
        }
        #endregion

        /// <summary>
        /// 绑定已有权限复选框 设置checked为true
        /// </summary>
        protected void BindCheckedBox(string roleId)
        {
            List<SeatManage.ClassModel.SysMenuInfo> listHaveMenu = SeatManage.Bll.SysMenu.GetUserRoleMenus(roleId);
            if (listHaveMenu != null)
            {
                foreach (FineUI.TreeNode node in treeMenu.Nodes)
                {
                    foreach (SeatManage.ClassModel.SysMenuInfo list in listHaveMenu)
                    {
                        if (node.NodeID == list.MenuID.ToString())
                        {
                            node.Checked = true;
                            foreach (FineUI.TreeNode nodeChild in node.Nodes)
                            {
                                foreach (SeatManage.ClassModel.SysMenuInfo listChild in list.ChildMenu)
                                {
                                    if (nodeChild.NodeID == listChild.MenuID.ToString())
                                    {
                                        nodeChild.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取选中菜单list 
        /// </summary>
        /// <returns></returns>
        protected List<SeatManage.ClassModel.SysMenuInfo> GetCheckedMenu()
        {
            List<SeatManage.ClassModel.SysMenuInfo> listMenu = new List<SeatManage.ClassModel.SysMenuInfo>();
            foreach (FineUI.TreeNode node in treeMenu.Nodes)
            {
                if (node.Checked == true)
                {
                    SeatManage.ClassModel.SysMenuInfo modelSysMenu = new SeatManage.ClassModel.SysMenuInfo();
                    modelSysMenu.MenuID = Convert.ToInt32(node.NodeID);
                    listMenu.Add(modelSysMenu);
                    foreach (FineUI.TreeNode nodeChild in node.Nodes)
                    {
                        if (nodeChild.Checked == true)
                        {
                            SeatManage.ClassModel.SysMenuInfo modelSysMenuChild = new SeatManage.ClassModel.SysMenuInfo();
                            modelSysMenuChild.MenuID = Convert.ToInt32(nodeChild.NodeID);
                            listMenu.Add(modelSysMenuChild);
                        }
                    }
                }

            }
            return listMenu;
        }

        //提交按钮点击事件
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string flag = Request.QueryString["flag"];
            string roleName = txtRoleName.Text;
            SeatManage.Bll.SysRolesDic bllSysRoleDic = new SeatManage.Bll.SysRolesDic();
            SeatManage.ClassModel.SysRolesDicInfo ModelSysRolesDicInfo = new SeatManage.ClassModel.SysRolesDicInfo();
            List<SeatManage.ClassModel.SysMenuInfo> listMenu = new List<SeatManage.ClassModel.SysMenuInfo>();
            ModelSysRolesDicInfo.RoleName = roleName;
            listMenu = GetCheckedMenu();
            ModelSysRolesDicInfo.RoleMenu = listMenu;
            switch (flag)
            {
                //新增菜单
                case "add":
                    if (string.IsNullOrEmpty(bllSysRoleDic.AddNewRole(ModelSysRolesDicInfo)))
                    {
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                        FineUI.Alert.ShowInTop("添加成功！");
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("添加失败！");
                    }
                    break;
                //修改菜单
                case "edit":
                    ModelSysRolesDicInfo.RoleID = Request.QueryString["roleId"];
                    string result = bllSysRoleDic.UpdateRole(ModelSysRolesDicInfo);
                    if (string.IsNullOrEmpty(result))
                    {
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                        FineUI.Alert.ShowInTop("修改成功！");
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("修改失败！" + result);
                    }
                    break;
                default:
                    FineUI.Alert.ShowInTop("未执行任何操作");
                    break;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtRoleName.Text = "";
            treeMenu.Nodes.Clear();
            LoadData();
            BindCheckedBox(Request.QueryString["roleId"]);
        }
    }
}