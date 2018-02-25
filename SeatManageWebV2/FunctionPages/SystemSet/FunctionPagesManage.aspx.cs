using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.Data;

namespace SeatManageWebV2.FunctionPages.SystemSet
{
    public partial class FunctionPagesManage : BasePage
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
                BindFunctionPagesGrid();
                btnAddMenu.OnClientClick = WindowEdit.GetShowReference("FunctionPageEdit.aspx?flag=add", "添加功能页");//为btnAddMenu新增点击事件
            }
        }
        //窗体关闭事件
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindFunctionPagesGrid();
        }
        /// <summary>
        /// 绑定功能页Grid
        /// </summary>
        protected void BindFunctionPagesGrid()
        {
            List<SeatManage.ClassModel.SysFuncDicInfo> listSysFuncDic = new List<SeatManage.ClassModel.SysFuncDicInfo>();
            SeatManage.Bll.SysFuncDic bllSysFuncDic = new SeatManage.Bll.SysFuncDic();
            listSysFuncDic = bllSysFuncDic.GetFuncPage(null, null);
            string sortField = GridFunctionPages.Columns[GridFunctionPages.SortColumnIndex].SortField;
            string sortDirection = GridFunctionPages.SortDirection;
            DataTable dt = new DataTable();
            DataColumn ModSeq = new DataColumn("ModSeq", typeof(string));
            DataColumn MCaption = new DataColumn("MCaption", typeof(string));
            DataColumn MenuLink = new DataColumn("MenuLink", typeof(string));
            DataColumn OrderSeq = new DataColumn("OrderSeq", typeof(string));
            dt.Columns.Add(ModSeq);
            dt.Columns.Add(MCaption);
            dt.Columns.Add(MenuLink);
            dt.Columns.Add(OrderSeq);
            foreach (SeatManage.ClassModel.SysFuncDicInfo list in listSysFuncDic)
            {
                DataRow row = dt.NewRow();
                row["ModSeq"] = list.No;
                row["MCaption"] = list.Name;
                row["MenuLink"] = list.PageUrl;
                row["OrderSeq"] = list.Order;
                dt.Rows.Add(row);
            }
            DataView view = dt.DefaultView;
            view.Sort = String.Format("{0} {1}", sortField, sortDirection);
            GridFunctionPages.DataSource = view;
            GridFunctionPages.DataBind();
        }

        protected void GridFunctionPages_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            GridFunctionPages.SortDirection = e.SortDirection;
            GridFunctionPages.SortColumnIndex = e.ColumnIndex;
            BindFunctionPagesGrid();
        }

        //删除功能页按钮点击事件
        protected void btnDel_Click(object sender, EventArgs e)
        {
            FineUI.CheckBoxField chkFild = (FineUI.CheckBoxField)GridFunctionPages.FindColumn("CheckBoxField1");
            SeatManage.ClassModel.SysFuncDicInfo modelSysFuncDicInfo = new SeatManage.ClassModel.SysFuncDicInfo();
            SeatManage.Bll.SysFuncDic bllSysFuncDic = new SeatManage.Bll.SysFuncDic();
            int selectCount = GridFunctionPages.SelectedRowIndexArray.Length;
            if (selectCount > 0)
            {
                for (int i = 0; i < selectCount; i++)
                {
                    int rowIndex = GridFunctionPages.SelectedRowIndexArray[i];
                    FineUI.GridRow row = GridFunctionPages.Rows[rowIndex] as FineUI.GridRow;
                    modelSysFuncDicInfo.No = row.DataKeys[0].ToString();
                    if (bllSysFuncDic.DeleteFuncPage(modelSysFuncDicInfo))
                    {
                        FineUI.Alert.ShowInTop("删除成功！");

                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("删除失败！");
                    }
                }
                BindFunctionPagesGrid();
            }
        }

        protected void GridFunctionPages_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ActionDelete")
            {
                SeatManage.ClassModel.SysFuncDicInfo modelSysFuncDicInfo = new SeatManage.ClassModel.SysFuncDicInfo();
                SeatManage.Bll.SysFuncDic bllSysFuncDic = new SeatManage.Bll.SysFuncDic();
                modelSysFuncDicInfo.No = GridFunctionPages.Rows[e.RowIndex].DataKeys[0].ToString();
                if (!bllSysFuncDic.DeleteFuncPage(modelSysFuncDicInfo))
                {
                    FineUI.Alert.ShowInTop("删除失败！");
                }
                else
                {
                    FineUI.Alert.ShowInTop("删除成功！");
                    BindFunctionPagesGrid();
                }
            }
        }

    }
}