using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManageWebV2.Code;
using System.Data;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SeatMonitor
{
    public partial class MonitorGraphMode : BasePage
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
                BindingGrid();
            }
        }

        protected void btnbinding_OnClick(object sender, EventArgs e)
        {
            BindingGrid();
        }
        protected void gridRoomList_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            gridRoomList.PageIndex = e.NewPageIndex;
        }

        protected void gridRoomList_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            gridRoomList.SortDirection = e.SortDirection;
            gridRoomList.SortColumnIndex = e.ColumnIndex;
            BindingGrid();
        }

        protected void gridRoomList_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            LinkButtonField lbf = gridRoomList.FindColumn("seatUsedView") as LinkButtonField;
            DataRowView row = e.DataItem as DataRowView;
            string roomNum = row[0].ToString();
            lbf.OnClientClick = WindowEdit.GetShowReference("SeatGraph.aspx?roomId=" + roomNum + "", "座位视图");

            LinkButtonField lbfReaderList = gridRoomList.FindColumn("seatReaderList") as LinkButtonField;
            lbfReaderList.OnClientClick = windowSeatUsedList.GetShowReference("MonitorListMode.aspx?roomId=" + roomNum + "", "在座读者列表");
        }

        protected void gridRoomList_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {

        }

        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindingGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        void BindingGrid()
        {
            DataTable dt = LogQueryHelper.GetMonitorGraphReadingRoomList(this.LoginId);
            string sortField = gridRoomList.Columns[gridRoomList.SortColumnIndex].SortField;
            string sortDirection = gridRoomList.SortDirection;
            DataView TableView = dt.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            gridRoomList.DataSource = TableView;
            gridRoomList.DataBind();
        }

    }
}