using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FineUI;

namespace SeatManageWebV2.FunctionPages.LogManage
{
    public partial class BlacklistInfo : BasePage
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
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                BindGrid();
            }
            else
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("信息获取失败！请重新打开！");
            }
        }
        private void BindGrid()
        {
            string sortField = VRGrid.Columns[VRGrid.SortColumnIndex].SortField;
            string sortDirection = VRGrid.SortDirection;
            DataTable table = GetUserInfoDateTable();
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            VRGrid.DataSource = TableView;
            VRGrid.DataBind();
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetUserInfoDateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("AddTime", typeof(DateTime));
            dt.Columns.Add("ReadingRoom", typeof(string));
            dt.Columns.Add("Seat", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            List<SeatManage.ClassModel.ViolationRecordsLogInfo> VRlist = SeatManage.Bll.T_SM_ViolateDiscipline.GetViolationRecordsByBlackliatID(Request.QueryString["id"]);
            foreach (SeatManage.ClassModel.ViolationRecordsLogInfo vrinfo in VRlist)
            {

                DataRow dr = dt.NewRow();
                dr["CardNo"] = vrinfo.CardNo;
                dr["ReaderName"] = vrinfo.ReaderName;
                dr["AddTime"] = vrinfo.EnterOutTime;
                dr["ReadingRoom"] = vrinfo.ReadingRoomName;
                dr["Seat"] = vrinfo.SeatID;
                dr["Remark"] = vrinfo.Remark;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VRGrid_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            VRGrid.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VRGrid_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            VRGrid.SortDirection = e.SortDirection;
            VRGrid.SortColumnIndex = e.ColumnIndex;
            BindGrid();
        }
    }
}