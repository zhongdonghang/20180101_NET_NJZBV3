using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SeatManageWebV2.Code;
using FineUI;
using SeatManage.ClassModel;
namespace SeatManageWebV2.FunctionPages.LogManage
{
    public partial class EnterOutLog : BasePage
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
                DateTime serviceDate = SeatManage.Bll.ServiceDateTime.Now;
                dpStartDate.SelectedDate = serviceDate;
                dpEndDate.SelectedDate = serviceDate;
            }
        }

        private void EnterOutLogListBinding()
        {
            string num = txtNum.Text;
            string roomNum = ddlReadingRoom.SelectedItem.Value;
            DateTime startDate = dpStartDate.SelectedDate.Value;
            if (startDate.Date < SeatManage.Bll.ServiceDateTime.Now.AddDays(-30).Date)
            {
                FineUI.Alert.Show("最多可以查询30天前的数据");
                return;
            }// DateTime.Parse(string.Format("{0} {1}", dpStartDate.Text, " 0:00:00"));
            DateTime endDate = dpEndDate.SelectedDate.Value; //DateTime.Parse(string.Format("{0} {1}", dpEndDate.Text, " 23:59:59"));
            EnumEnterOutLogQueryMethod method = EnumEnterOutLogQueryMethod.None;
            DataTable dt = null;
            if (ddlQueryMethod.SelectedValue == "cardNo")
            {
                method = EnumEnterOutLogQueryMethod.CardNo;
            }
            else if (ddlQueryMethod.SelectedValue == "seatNum")
            {
                method = EnumEnterOutLogQueryMethod.SeatNum;
            }
            if (chkSearchMH.Checked == false)
            {
                dt = LogQueryHelper.GetEnterOutLogDataSet(num, roomNum, method, startDate, endDate.AddHours(23).AddMinutes(59));
            }
            else
            {
                dt = LogQueryHelper.GetEnterOutLogDataSet_ByFuzzySearch(num, roomNum, method, startDate, endDate.AddHours(23).AddMinutes(59));
            }
            if (dt != null)
            {
                string sortField = enterOutLogList.Columns[enterOutLogList.SortColumnIndex].SortField;
                string sortDirection = enterOutLogList.SortDirection;
                DataView TableView = dt.DefaultView;
                TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
                enterOutLogList.DataSource = TableView;
                enterOutLogList.DataBind();
            }
            else
            {
                FineUI.Alert.Show("没有查询到信息");
            }
        }

        protected void ddlQueryMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlQueryMethod.SelectedValue == "cardNo")
            {
                chkSearchMH.Enabled = true;
            }
            else if (ddlQueryMethod.SelectedValue == "seatNum")
            {
                chkSearchMH.Enabled = false;
            }
        }

        protected void enterOutLogList_PageIndexChange(object sender, GridPageEventArgs e)
        {
            enterOutLogList.PageIndex = e.NewPageIndex;
        }

        protected void enterOutLogList_Sort(object sender, GridSortEventArgs e)
        {
            enterOutLogList.SortDirection = e.SortDirection;
            enterOutLogList.SortColumnIndex = e.ColumnIndex;
            EnterOutLogListBinding();
        }

        protected void enterOutLogList_RowDataBound(object sender, GridRowEventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EnterOutLogListBinding();
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
    }
}