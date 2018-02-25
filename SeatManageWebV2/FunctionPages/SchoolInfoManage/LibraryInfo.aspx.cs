using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SchoolInfoManage
{
    public partial class LibraryInfo : BasePage
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
                btnAddLibrary.OnClientClick = WindowEdit.GetShowReference("LibraryEdit.aspx?flag=add", "添加图书馆");
                BindGrid();
            }
             
        }
        private void BindGrid()
        {
            string sortField = LibraryGrid.Columns[LibraryGrid.SortColumnIndex].SortField;
            string sortDirection = LibraryGrid.SortDirection;
            DataTable table = GetSchoolInfoDateTable();
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            LibraryGrid.DataSource = TableView;
            LibraryGrid.DataBind();
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetSchoolInfoDateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LibraryNo", typeof(string));
            dt.Columns.Add("LibraryName", typeof(string));
            dt.Columns.Add("SchoolNo", typeof(string));
            dt.Columns.Add("SchoolName", typeof(string));
            List<SeatManage.ClassModel.LibraryInfo> librarylist = SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, null, null);
            foreach (SeatManage.ClassModel.LibraryInfo library in librarylist)
            {
                DataRow dr = dt.NewRow();
                dr["LibraryNo"] = library.No;
                dr["LibraryName"] = library.Name;
                dr["SchoolNo"] = library.School.No;
                dr["SchoolName"] = library.School.Name;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 行命令时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LibraryGrid_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            //if (e.CommandName == "ActionDelete")
            //{
            //    SeatManage.ClassModel.LibraryInfo library = new SeatManage.ClassModel.LibraryInfo();
            //    library.No = LibraryGrid.Rows[e.RowIndex].DataKeys[0].ToString();
            //    if (!SeatManage.Bll.T_SM_Library.DeleteLibrary(library))
            //    {
            //        FineUI.Alert.ShowInTop("删除失败！");
            //    }
            //    else
            //    {
            //        FineUI.Alert.ShowInTop("删除完成！");
            //        BindGrid();
            //    }
            //}
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LibraryGrid_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            LibraryGrid.SortDirection = e.SortDirection;
            LibraryGrid.SortColumnIndex = e.ColumnIndex;
            BindGrid();
        }
        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LibraryGrid_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            LibraryGrid.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LibraryGrid_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            LinkButtonField lbf = LibraryGrid.FindColumn("Libraryedit") as LinkButtonField;
            DataRowView row = e.DataItem as DataRowView;
            string librarylno = row[0].ToString();
            lbf.OnClientClick = WindowEdit.GetShowReference("libraryEdit.aspx?flag=edit&id=" + librarylno + "", "图书馆编辑");

            LinkButtonField lbfx = LibraryGrid.FindColumn("Librarydelete") as LinkButtonField;
            DataRowView rowx = e.DataItem as DataRowView;
            string librarylnox = rowx[0].ToString();
            lbfx.OnClientClick = WindowDelete.GetShowReference("../SystemSet/DeletePassword.aspx?Type=Library&id=" + librarylno + "", "图书馆删除");
        }
        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}