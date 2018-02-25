using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using SeatManageWebV2.Code;
using System.Data;

namespace SeatManageWebV2.FunctionPages.SeatMonitor
{
    public partial class MonitorListMode : BasePage
    {
        string roomId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            roomId = Request.QueryString["roomId"];
            if (!IsPostBack)
            {
                if (!OpVerifiction())
                {
                    Response.Write("请使用正常方式访问网站！");
                    Response.End();
                }
                GridBinding();
            }
        }

        /// <summary>
        /// 页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridReaderList_PageIndexChange(object sender, GridPageEventArgs e)
        {
            gridReaderList.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridReaderList_OnPreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            LinkButtonField lbf = gridReaderList.FindColumn("seatReaderList") as LinkButtonField;
            DataRowView row = e.DataItem as DataRowView;
            string seatNum = row[3].ToString();
            string shortNum = row[4].ToString();
            lbf.OnClientClick = WindowEdit.GetShowReference(string.Format("SeatHandle.aspx?seatNo={0}&seatShortNo={1}&used={2}", seatNum, shortNum, "1"), "座位视图");
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridReaderList_Sort(object sender, GridSortEventArgs e)
        {
            gridReaderList.SortDirection = e.SortDirection;
            gridReaderList.SortColumnIndex = e.ColumnIndex;
            GridBinding();
        }

        private void GridBinding()
        {
            DataTable dt = LogQueryHelper.UsingSeatReader(roomId);
            string sortField = gridReaderList.Columns[gridReaderList.SortColumnIndex].SortField;
            string sortDirection = gridReaderList.SortDirection;
            DataView TableView = dt.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            gridReaderList.DataSource = TableView;
            gridReaderList.DataBind();

        }
        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            GridBinding();
        }
    }
}