using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using SeatManageWebV2.Code;
using SeatManage.EnumType;
using System.Data;
using SeatManage.ClassModel;

namespace SeatManageWebV2.FunctionPages.LogManage
{
    public partial class BespeakLog : BasePage
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
                dpStartDate.SelectedDate = SeatManage.Bll.ServiceDateTime.Now;
                dpEndDate.SelectedDate = dpStartDate.SelectedDate.Value;
                BindDDLReadingRoom();
                BindDDLBespeakStatus();
            }
        }

        /// <summary>
        /// 页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridBespeakLog_PageIndexChange(object sender, GridPageEventArgs e)
        {
            gridBespeakLog.PageIndex = e.NewPageIndex;
        }

        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridBespeakLog_RowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            LinkButtonField lbf = gridBespeakLog.FindColumn("bespeakOpreate") as LinkButtonField;
            DataRowView row = e.DataItem as DataRowView;

            BookingStatus logStatus = (BookingStatus)int.Parse(row[10].ToString());

            if (logStatus == BookingStatus.Waiting)
            {
                lbf.Enabled = true;
                lbf.ToolTip = "取消预约";
                lbf.Icon = FineUI.Icon.Delete;
            }
            else
            {
                lbf.Enabled = false;
                lbf.ToolTip = "不可操作";
                lbf.Icon = FineUI.Icon.None;
            }
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridBespeakLog_Sort(object sender, GridSortEventArgs e)
        {
            gridBespeakLog.SortDirection = e.SortDirection;
            gridBespeakLog.SortColumnIndex = e.ColumnIndex;
            GridBindDate();
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            GridBindDate();
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridBespeakLog_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "ActionDeleteBespeakLog")
            {
                int id = int.Parse(gridBespeakLog.Rows[e.RowIndex].DataKeys[0].ToString());
                SeatManage.ClassModel.BespeakLogInfo bespeakModel = SeatManage.Bll.T_SM_SeatBespeak.GetBespeaklogById(id);
                if (bespeakModel.BsepeakState != BookingStatus.Waiting)
                {
                    FineUI.Alert.Show("操作失败，状态无效");
                    GridBindDate();
                }
                bespeakModel.BsepeakState = BookingStatus.Cencaled;
                bespeakModel.CancelPerson = Operation.Admin;
                bespeakModel.CancelTime = SeatManage.Bll.ServiceDateTime.Now;
                bespeakModel.Remark = "被管理员" + this.LoginId + "取消预约";
                if (SeatManage.Bll.T_SM_SeatBespeak.UpdateBespeakList(bespeakModel) > 0)
                {

                    //SeatManage.ClassModel.ReaderNoticeInfo rni = new SeatManage.ClassModel.ReaderNoticeInfo();
                    //rni.CardNo = bespeakModel.CardNo;
                    //rni.AddTime = bespeakModel.CancelTime;
                    //rni.Note = bespeakModel.Remark;
                    //SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(rni);

                    //PushMsgInfo msg = new PushMsgInfo();
                    //msg.Title = "您好，您的预约已被取消";
                    //msg.MsgType =  MsgPushType.AdminOperation;
                    //msg.StudentNum = bespeakModel.CardNo;
                    //msg.Message = bespeakModel.Remark;
                    //SeatManage.Bll.T_SM_ReaderNotice.SendPushMsg(msg);

                    FineUI.Alert.Show("取消成功");
                    GridBindDate();
                }
                else
                {
                    FineUI.Alert.Show("操作失败");
                    GridBindDate();
                }
            }
        }

        private void GridBindDate()
        {
            string cardNo = txtCardNo.Text;
            string roomNum = ddlReadingRoom.SelectedItem.Value;
            BookingStatus status = (BookingStatus)int.Parse(ddlBespeakState.SelectedItem.Value);
            DateTime startDate = dpStartDate.SelectedDate.Value;
            DateTime endDate = dpEndDate.SelectedDate.Value;
            List<BookingStatus> statusList = new List<BookingStatus>();
            if (status == BookingStatus.None)
            {
                statusList.Add(BookingStatus.Cencaled);
                statusList.Add(BookingStatus.Confinmed);
                statusList.Add(BookingStatus.Waiting);
            }
            else
            {
                statusList.Add(status);
            }
            DataTable dt = new DataTable();
            if (chkSearchMH.Checked == false)
            {
                dt = LogQueryHelper.BespeakLogQuery(cardNo, roomNum, statusList, startDate, endDate.AddHours(23).AddMinutes(59));
            }
            else
            {
                dt = LogQueryHelper.BespeakLogQuery_ByFuzzySearch(cardNo, roomNum, statusList, startDate, endDate.AddHours(23).AddMinutes(59));
            }
            string sortField = gridBespeakLog.Columns[gridBespeakLog.SortColumnIndex].SortField;
            string sortDirection = gridBespeakLog.SortDirection;
            DataView TableView = dt.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            gridBespeakLog.DataSource = TableView;
            gridBespeakLog.DataBind();
        }


        /// <summary>
        /// 绑定管理的阅览室下拉列表
        /// </summary>
        private void BindDDLReadingRoom()
        {
            SeatManage.ClassModel.ManagerPotency potency = SeatManage.Bll.T_SM_ManagerPotency.GetManangePotencyByLoginID(this.LoginId);
            if (potency != null)
            {
                potency.RightRoomList.Insert(0, new SeatManage.ClassModel.ReadingRoomInfo() { Name = "所有阅览室", No = "" });
                ddlReadingRoom.DataTextField = "Name";
                ddlReadingRoom.DataValueField = "No";
                ddlReadingRoom.DataSource = potency.RightRoomList;
                ddlReadingRoom.DataBind();
            }
        }
        /// <summary>
        /// 绑定预约状态
        /// </summary>
        private void BindDDLBespeakStatus()
        {
            List<BespeakEnumKey_Value> list = new List<BespeakEnumKey_Value>();
            list.Add(new BespeakEnumKey_Value() { BespeakState = SeatManage.SeatManageComm.SeatComm.ConvertBookingStatus(SeatManage.EnumType.BookingStatus.None), Value = ((int)SeatManage.EnumType.BookingStatus.None).ToString() });

            list.Add(new BespeakEnumKey_Value() { BespeakState = SeatManage.SeatManageComm.SeatComm.ConvertBookingStatus(SeatManage.EnumType.BookingStatus.Waiting), Value = ((int)SeatManage.EnumType.BookingStatus.Waiting).ToString() });
            list.Add(new BespeakEnumKey_Value() { BespeakState = SeatManage.SeatManageComm.SeatComm.ConvertBookingStatus(SeatManage.EnumType.BookingStatus.Confinmed), Value = ((int)SeatManage.EnumType.BookingStatus.Confinmed).ToString() });
            list.Add(new BespeakEnumKey_Value() { BespeakState = SeatManage.SeatManageComm.SeatComm.ConvertBookingStatus(SeatManage.EnumType.BookingStatus.Cencaled), Value = ((int)SeatManage.EnumType.BookingStatus.Cencaled).ToString() });
            ddlBespeakState.DataTextField = "BespeakState";
            ddlBespeakState.DataValueField = "Value";
            ddlBespeakState.DataSource = list;
            ddlBespeakState.DataBind();
            ddlBespeakState.SelectedIndex = 1;
        }
    }
}