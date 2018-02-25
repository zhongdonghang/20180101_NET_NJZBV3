using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SeatManage.EnumType;

namespace SeatManageWebV2.FunctionPages.ReaderLog
{
    public partial class SelectViolateDiscipline : BasePage
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
                BindDDLReadingRoom();
                dpStartDate.SelectedDate = DateTime.Now.AddDays(-7);
                dpEndDate.SelectedDate = DateTime.Now;
                BindGrid();
            }
        }
        /// <summary>
        /// 绑定管理的阅览室下拉列表
        /// </summary>
        private void BindDDLReadingRoom()
        {
            List<SeatManage.ClassModel.ReadingRoomInfo> roomlist = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, null, null);
            roomlist.Insert(0, new SeatManage.ClassModel.ReadingRoomInfo() { Name = "所有阅览室", No = "" });
            ddlReadingRoom.DataTextField = "Name";
            ddlReadingRoom.DataValueField = "No";
            ddlReadingRoom.DataSource = roomlist;
            ddlReadingRoom.DataBind();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string sortField = VRGrid.Columns[VRGrid.SortColumnIndex].SortField;
            string sortDirection = VRGrid.SortDirection;
            DateTime starttime = dpStartDate.SelectedDate.Value;

            DateTime endtime = dpEndDate.SelectedDate.Value;
            if (!string.IsNullOrEmpty(dpStartDate.Text) && !string.IsNullOrEmpty(dpEndDate.Text) && starttime >= endtime)
            {
                FineUI.Alert.Show("结束日期必须大于等于开始日期");
                return;
            }
            DataTable table = GetUserInfoDateTable(starttime.ToString(), endtime.AddHours(23).AddMinutes(59).ToString());
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            VRGrid.DataSource = TableView;
            VRGrid.DataBind();
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
            dt.Columns.Add("ReadingRoom", typeof(string));
            dt.Columns.Add("Seat", typeof(string));
            dt.Columns.Add("LogStatus", typeof(string));
            dt.Columns.Add("BlacklistStatus", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            List<SeatManage.ClassModel.ViolationRecordsLogInfo> VRlist = SeatManage.Bll.T_SM_ViolateDiscipline.GetViolationRecords(
                this.LoginId,
                ddlReadingRoom.SelectedValue,
                starttime,
                endtime,
                (SeatManage.EnumType.LogStatus)int.Parse(ddllogstatus.SelectedValue),
                (SeatManage.EnumType.LogStatus)int.Parse(ddlblacklist.SelectedValue));
            foreach (SeatManage.ClassModel.ViolationRecordsLogInfo vrinfo in VRlist)
            {

                DataRow dr = dt.NewRow();
                dr["ID"] = vrinfo.ID;
                dr["CardNo"] = vrinfo.CardNo;
                dr["ReaderName"] = vrinfo.ReaderName;
                dr["AddTime"] = vrinfo.EnterOutTime;
                dr["ReadingRoom"] = vrinfo.ReadingRoomName;
                dr["Seat"] = vrinfo.SeatID;
                if (vrinfo.Flag == LogStatus.Valid)
                {
                    dr["LogStatus"] = "有效记录";
                }
                else
                {
                    dr["LogStatus"] = "失效记录";
                }
                if (vrinfo.BlacklistID != "-1")
                {
                    dr["BlacklistStatus"] = "已加入黑名单";
                }
                else
                {
                    dr["BlacklistStatus"] = "未处理";
                }
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
        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
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
        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VRGrid_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {

        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VRGrid_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
        }
    }
}