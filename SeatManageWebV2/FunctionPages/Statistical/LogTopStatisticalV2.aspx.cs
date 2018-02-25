using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManageWebV2.Code;

namespace SeatManageWebV2.FunctionPages.Statistical
{
    public partial class LogTopStatisticalV2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            DataTable table = new DataTable();
            this.Session["StartDate"] = dpStartDate.SelectedDate.Value;
            this.Session["EndDate"] = dpEndDate.SelectedDate.Value;
            this.Session["SelectReaderType"] = ddlreadertype.SelectedValue;
            this.Session["SelectLogType"] = ddllogtype.SelectedValue;
            TopListGrid.Title = ddllogtype.SelectedText + "（" + ddlreadertype.SelectedText + "）";
            switch ((Code.ReadingRoomStatistics.TopReaderType)int.Parse(ddlreadertype.SelectedValue))
            {
                case Code.ReadingRoomStatistics.TopReaderType.OnlyReader:
                    TopListGrid.Columns[1].Hidden = false;
                    TopListGrid.Columns[2].Hidden = false;
                    TopListGrid.Columns[3].Hidden = false;
                    TopListGrid.Columns[4].Hidden = false;
                    break;
                case Code.ReadingRoomStatistics.TopReaderType.ReaderDept:
                    TopListGrid.Columns[1].Hidden = true;
                    TopListGrid.Columns[2].Hidden = true;
                    TopListGrid.Columns[3].Hidden = true;
                    TopListGrid.Columns[4].Hidden = false;
                    break;
                case Code.ReadingRoomStatistics.TopReaderType.ReaderType:
                    TopListGrid.Columns[1].Hidden = true;
                    TopListGrid.Columns[2].Hidden = true;
                    TopListGrid.Columns[3].Hidden = false;
                    TopListGrid.Columns[4].Hidden = true;
                    break;
            }
            switch((Code.ReadingRoomStatistics.TopLogType)int.Parse(ddllogtype.SelectedValue))
            {
                case ReadingRoomStatistics.TopLogType.SeatTime:
                    table = Code.LogTopQuery.GetSeatTimeTop(dpStartDate.SelectedDate.Value, dpEndDate.SelectedDate.Value, int.Parse(ddlreadertype.SelectedValue), int.Parse(ddltopnum.SelectedValue));
                    break;
                case ReadingRoomStatistics.TopLogType.EnterOutLog:
                    table = Code.LogTopQuery.GetSeatCountTop(dpStartDate.SelectedDate.Value, dpEndDate.SelectedDate.Value, int.Parse(ddlreadertype.SelectedValue), int.Parse(ddltopnum.SelectedValue));
                    break;
                case ReadingRoomStatistics.TopLogType.Blastlist:
                    table = Code.LogTopQuery.GetBlacklistTop(dpStartDate.SelectedDate.Value, dpEndDate.SelectedDate.Value, int.Parse(ddlreadertype.SelectedValue), int.Parse(ddltopnum.SelectedValue));
                    break;
                case ReadingRoomStatistics.TopLogType.ViolateDiscipline:
                    table = Code.LogTopQuery.GetViolateDisciplineTop(dpStartDate.SelectedDate.Value, dpEndDate.SelectedDate.Value, int.Parse(ddlreadertype.SelectedValue), int.Parse(ddltopnum.SelectedValue));
                    break;
            }
            string sortField = TopListGrid.Columns[TopListGrid.SortColumnIndex].SortField;
            string sortDirection = TopListGrid.SortDirection;
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            Dictionary<string, DataTable> ddt = new Dictionary<string, DataTable>();
            ddt.Add("排行榜", table);
            this.Session["DataTables"] = ddt;
            TopListGrid.DataSource = TableView;
            TopListGrid.DataBind();
        }
        protected void btn2_Click(object sender, EventArgs e)
        {
            ToExcel();
        }

        private void ToExcel()
        {
            if (Session["DataTables"] == null)
            {
                this.RegisterStartupScript("无结果", "<script>alert('请先进行统计！');</script>");
                return;
            }

            Dictionary<string, DataTable> dataTables = Session["DataTables"] as Dictionary<string, DataTable>;
            if (dataTables == null)
            {
                this.RegisterStartupScript("无结果", "<script>alert('请先进行统计！');</script>");
                return;
            }
            DataToExcel dte = new DataToExcel();
            dte.DataGridViewToExcel(dataTables);
        }
        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TopListGrid_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            TopListGrid.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TopListGrid_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            TopListGrid.SortDirection = e.SortDirection;
            TopListGrid.SortColumnIndex = e.ColumnIndex;
            Thread myThread = new Thread(new ThreadStart(BindGrid));
            myThread.Start();
            //BindGrid();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Thread myThread = new Thread(new ThreadStart(BindGrid));
            //myThread.Start();
            BindGrid();
        }
    }
}