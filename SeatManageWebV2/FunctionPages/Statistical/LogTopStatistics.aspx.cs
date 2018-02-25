using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using SeatManageWebV2.Code;

namespace SeatManageWebV2.FunctionPages.Statistical
{
    public partial class LogTopStatistics : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.Session["InsDate"] == null)
                {
                    dpEndDate.SelectedDate = DateTime.Now.Date.AddDays(-1);
                    dpStartDate.SelectedDate = DateTime.Now.AddYears(-1).AddDays(-1).Date;
                    this.Session["InsDate"] = "IsSetDate";
                }
                else
                {
                    dpStartDate.SelectedDate = DateTime.Parse(this.Session["StartDate"].ToString()).Date;
                    dpEndDate.SelectedDate = DateTime.Parse(this.Session["EndDate"].ToString()).Date;
                    ddlreadertype.SelectedValue = this.Session["SelectReaderType"].ToString();
                    ddllogtype.SelectedValue = this.Session["SelectLogType"].ToString();
                }
                if (this.Session["GridData"] != null)
                {
                    TopListGrid.DataSource = this.Session["GridData"] as DataView;
                    TopListGrid.DataBind();
                    //this.Session.Remove("GridData");
                }
                if (this.Session["Data_progress"] != null)
                {
                    Response.Write(this.Session["Data_progress"].ToString());
                    if (this.Session["Data_progress"].ToString() == "(4/4)查询完成，感谢您的耐心等待！")
                    {
                        this.Session.Remove("Data_progress");
                    }
                    Response.End();
                }
            }
        }
        void rrsta_Progress(string message)
        {
            this.Session["Data_progress"] = message;
        }
        Code.ReadingRoomStatistics rrsta;
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
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
            string sortField = TopListGrid.Columns[TopListGrid.SortColumnIndex].SortField;
            string sortDirection = TopListGrid.SortDirection;
            rrsta = new Code.ReadingRoomStatistics();
            rrsta.Progress += new Code.ReadingRoomStatistics.EventHanleProgress(rrsta_Progress);
            DataTable table = rrsta.LogTop((Code.ReadingRoomStatistics.TopLogType)int.Parse(ddllogtype.SelectedValue), (Code.ReadingRoomStatistics.TopReaderType)int.Parse(ddlreadertype.SelectedValue), dpStartDate.SelectedDate.Value.ToShortDateString(), dpEndDate.SelectedDate.Value.ToShortDateString());
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            this.Session["DataTables"] = table;
            this.Session["GridData"] = TableView;
            this.Session["Data_progress"] = "(4/4)查询完成，感谢您的耐心等待！";
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
            Thread myThread = new Thread(new ThreadStart(BindGrid));
            myThread.Start();
            //BindGrid();
        }
    }
}