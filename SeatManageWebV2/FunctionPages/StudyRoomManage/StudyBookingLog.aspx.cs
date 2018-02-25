using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SeatManage.EnumType;
using FineUI;

namespace SeatManageWebV2.FunctionPages.StudyRoomManage
{
    public partial class StudyBookingLog : BasePage
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
                DateTime nowDate = SeatManage.Bll.ServiceDateTime.Now;
                dpStartDate.SelectedDate = nowDate;
                dpEndDate.SelectedDate = nowDate.AddDays(7);
                GridBindDate();
            }
        }
        /// <summary>
        /// 页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridStudyLog_PageIndexChange(object sender, GridPageEventArgs e)
        {
            gridStudyLog.PageIndex = e.NewPageIndex;
        }

        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridStudyLog_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            LinkButtonField lbf = gridStudyLog.FindColumn("StudyWatch") as LinkButtonField;
            DataRowView row = e.DataItem as DataRowView;

            string logStatus = row[8].ToString();
            //   string status = row["BsepeakState"].ToString();

            if (logStatus == "等待审核")
            {
                lbf.Enabled = true;
                lbf.ToolTip = "审核申请";
                lbf.Icon = FineUI.Icon.PageEdit;
                lbf.OnClientClick = WindowEdit.GetShowReference(string.Format("StudyBookingLogCheck.aspx?id={0}&flag=edit", row[0].ToString()), "审核申请");

            }
            else
            {
                lbf.Enabled = true;
                lbf.ToolTip = "查看申请";
                lbf.Icon = FineUI.Icon.Zoom;
                lbf.OnClientClick = WindowEdit.GetShowReference(string.Format("StudyBookingLogCheck.aspx?id={0}", row[0].ToString()), "查看申请");
            }

        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridStudyLog_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            //if (e.CommandName == "ActionDeleteStudyLog")
            //{
            //    int id = int.Parse(gridStudyLog.Rows[e.RowIndex].DataKeys[0].ToString());
            //    SeatManage.ClassModel.StudyBookingLog StudyModel = SeatManage.Bll.StudyRoomOperation.GetStudyBookingLogByID(id);
            //    if (StudyModel.CheckState == CheckStatus.Adopt || StudyModel.CheckState == CheckStatus.Cancel)
            //    {
            //        FineUI.Alert.Show("操作失败，状态无效");
            //        GridBindDate();
            //    }
            //    StudyModel.CheckState = CheckStatus.Cancel;
            //    StudyModel.CheckTime = SeatManage.Bll.ServiceDateTime.Now;
            //    StudyModel.Remark = "读者取消此处审核";
            //    if (SeatManage.Bll.StudyRoomOperation.UpdateStudyBookingLog(StudyModel))
            //    {
            //        FineUI.Alert.Show("取消成功");
            //        GridBindDate();
            //    }
            //    else
            //    {
            //        FineUI.Alert.Show("操作失败");
            //        GridBindDate();
            //    }
            //}
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridStudyLog_Sort(object sender, GridSortEventArgs e)
        {
            gridStudyLog.SortDirection = e.SortDirection;
            gridStudyLog.SortColumnIndex = e.ColumnIndex;
            GridBindDate();
        }

        private void GridBindDate()
        {
            string cardNo = this.LoginId;
            if (string.IsNullOrEmpty(cardNo))
            {
                cardNo = this.LoginId;
            }
            CheckStatus status = CheckStatus.None;
            if (ddlStudyState.SelectedItem != null)
            {
                status = (CheckStatus)int.Parse(ddlStudyState.SelectedItem.Value);
            }
            DateTime startDate = dpStartDate.SelectedDate.Value;
            DateTime endDate = dpEndDate.SelectedDate.Value;
            List<CheckStatus> statusList = new List<CheckStatus>();
            statusList.Add(status);

            List<SeatManage.ClassModel.StudyBookingLog> modelList = SeatManage.Bll.StudyRoomOperation.GetStudyBookingLogList(null, null, startDate.Date, endDate.Date.AddDays(1).AddSeconds(-1), statusList);

            DataTable dt = new DataTable();
            DataColumn StudyID = new DataColumn("StudyID", typeof(string));
            DataColumn StudyRoomName = new DataColumn("StudyRoomName", typeof(string));
            DataColumn StudyRoomNo = new DataColumn("StudyRoomNo", typeof(string));
            DataColumn CardNo = new DataColumn("CardNo", typeof(string));
            DataColumn MeetingName = new DataColumn("MeetingName", typeof(string));
            DataColumn SubmitTime = new DataColumn("SubmitTime", typeof(DateTime));
            DataColumn BespeakTime = new DataColumn("BespeakTime", typeof(DateTime));
            DataColumn CheckTime = new DataColumn("CheckTime", typeof(DateTime));
            DataColumn CheckState = new DataColumn("CheckStatus", typeof(string));
            DataColumn Remark = new DataColumn("Remark", typeof(string));
            dt.Columns.Add(StudyID);
            dt.Columns.Add(StudyRoomName);
            dt.Columns.Add(StudyRoomNo);
            dt.Columns.Add(CardNo);
            dt.Columns.Add(MeetingName);
            dt.Columns.Add(SubmitTime);
            dt.Columns.Add(BespeakTime);
            dt.Columns.Add(CheckTime);
            dt.Columns.Add(CheckState);
            dt.Columns.Add(Remark);
            foreach (SeatManage.ClassModel.StudyBookingLog list in modelList)
            {
                DataRow row = dt.NewRow();
                row["StudyID"] = list.StudyID;
                row["StudyRoomName"] = list.StudyRoomName;
                row["StudyRoomNo"] = list.StudyRoomNo;
                row["CardNo"] = list.CardNo;
                row["MeetingName"] = list.Application.MeetingName;
                row["SubmitTime"] = list.SubmitTime;
                if (list.CheckTime == null)
                {
                    list.CheckTime = new DateTime();
                }
                row["CheckTime"] = list.CheckTime;
                row["BespeakTime"] = list.BespeakTime;
                switch (list.CheckState)
                {
                    case SeatManage.EnumType.CheckStatus.Adopt: row["CheckStatus"] = "通过审核"; break;
                    case SeatManage.EnumType.CheckStatus.Cancel: row["CheckStatus"] = "取消审核"; break;
                    case SeatManage.EnumType.CheckStatus.Checking: row["CheckStatus"] = "等待审核"; break;
                    case SeatManage.EnumType.CheckStatus.Failure: row["CheckStatus"] = "未通过"; break;
                }
                row["Remark"] = list.Remark;
                dt.Rows.Add(row);
            }


            string sortField = gridStudyLog.Columns[gridStudyLog.SortColumnIndex].SortField;
            string sortDirection = gridStudyLog.SortDirection;
            DataView TableView = dt.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            gridStudyLog.DataSource = TableView;
            gridStudyLog.DataBind();
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            GridBindDate();
        }
        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            GridBindDate();
        }
    }
}