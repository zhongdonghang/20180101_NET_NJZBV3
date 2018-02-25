using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.Data;

namespace SeatManageWebV2.FunctionPages.ReaderLog
{
    public partial class SelectNoticeLog : BasePage
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
            }
            BindGrid();
        }

        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NoticeGrid_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            NoticeGrid.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NoticeGrid_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            NoticeGrid.SortDirection = e.SortDirection;
            NoticeGrid.SortColumnIndex = e.ColumnIndex;
            BindGrid();
        }

        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NoticeGrid_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            //LinkButtonField lbf = NoticeGrid.FindColumn("noticeContent") as LinkButtonField;
            //DataRowView row = e.DataItem as DataRowView;

        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string sortField = NoticeGrid.Columns[NoticeGrid.SortColumnIndex].SortField;
            string sortDirection = NoticeGrid.SortDirection;
            DataTable table = Code.LogQueryHelper.ReaderNoticeList(this.LoginId);
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            NoticeGrid.DataSource = TableView;
            NoticeGrid.DataBind();
        }
    }
}