using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.EnumType;
using System.Data;
using FineUI;

namespace SeatManageWebV2.FunctionPages.ReaderLog
{
    public partial class SelectBlacklist : BasePage
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
                dpStartDate.SelectedDate = DateTime.Now.AddDays(-7);
                dpEndDate.SelectedDate = DateTime.Now;
                BindGrid();
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string sortField = BlacklistGrid.Columns[BlacklistGrid.SortColumnIndex].SortField;
            string sortDirection = BlacklistGrid.SortDirection;
            DateTime starttime = dpStartDate.SelectedDate.Value;

            DateTime endtime = dpEndDate.SelectedDate.Value;

            if (!string.IsNullOrEmpty(dpStartDate.Text) && !string.IsNullOrEmpty(dpEndDate.Text) && starttime >= endtime)
            {
                FineUI.Alert.Show("结束日期必须大于等于开始日期");
                return;
            }
            DataTable table = GetUserInfoDateTable(starttime.ToString(), string.Format("{0} {1}", endtime.ToShortDateString(), "23:59:59"));
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            BlacklistGrid.DataSource = TableView;
            BlacklistGrid.DataBind();
        }
        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BlacklistGrid_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            LinkButtonField lbfshow = BlacklistGrid.FindColumn("BlacklistInfo") as LinkButtonField;
            DataRowView row = e.DataItem as DataRowView;
            string status = row[5].ToString();
            lbfshow.OnClientClick = WindowEdit.GetShowReference("../LogManage/BlacklistInfo.aspx?id=" + row[0].ToString() + "", "黑名单详情");
        }
        protected void BlacklistGrid_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetUserInfoDateTable(string starttime, string endtime)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("AddTime", typeof(string));
            dt.Columns.Add("LeaveTime", typeof(string));
            dt.Columns.Add("LogStatus", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            List<SeatManage.ClassModel.BlackListInfo> Blistlistlist = SeatManage.Bll.T_SM_Blacklist.GetAllBlackListInfo(
                this.LoginId,
                (SeatManage.EnumType.LogStatus)int.Parse(ddllogstatus.SelectedValue),
                starttime,
                endtime);
            foreach (SeatManage.ClassModel.BlackListInfo bllist in Blistlistlist)
            {

                DataRow dr = dt.NewRow();
                dr["ID"] = bllist.ID;
                dr["CardNo"] = bllist.CardNo;
                dr["ReaderName"] = bllist.ReaderName;
                dr["AddTime"] = bllist.AddTime;
                dr["LeaveTime"] = bllist.OutTime;
                if (bllist.BlacklistState == LogStatus.Valid)
                {
                    dr["LogStatus"] = "处罚中";
                }
                else
                {
                    dr["LogStatus"] = "已过期";
                }
                dr["Remark"] = bllist.ReMark;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BlacklistGrid_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            BlacklistGrid.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BlacklistGrid_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            BlacklistGrid.SortDirection = e.SortDirection;
            BlacklistGrid.SortColumnIndex = e.ColumnIndex;
            BindGrid();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}