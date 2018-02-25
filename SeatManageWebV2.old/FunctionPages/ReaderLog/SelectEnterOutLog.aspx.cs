using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManageWebV2.Code;
using System.Data;
using FineUI;

namespace SeatManageWebV2.FunctionPages.ReaderLog
{
    public partial class SelectEnterOutLog : BasePage
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
                dpStartDate.SelectedDate = DateTime.Now.AddDays(-7);
                dpEndDate.SelectedDate = DateTime.Now;
            }
        }

        private void EnterOutLogListBinding()
        {
            string roomNum = ddlReadingRoom.SelectedItem.Value;
            DateTime startDate = dpStartDate.SelectedDate.Value;
            if (startDate.Date < SeatManage.Bll.ServiceDateTime.Now.AddDays(-30).Date)
            {
                FineUI.Alert.Show("最多可以查询30天前的数据");
                return;
            }// DateTime.Parse(string.Format("{0} {1}", dpStartDate.Text, " 0:00:00"));
            DateTime endDate = dpEndDate.SelectedDate.Value; //DateTime.Parse(string.Format("{0} {1}", dpEndDate.Text, " 23:59:59"));
            EnumEnterOutLogQueryMethod method = EnumEnterOutLogQueryMethod.CardNo;
            DataTable dt = null;

            dt = LogQueryHelper.GetEnterOutLogDataSet(this.LoginId, roomNum, method, startDate, endDate.AddHours(23).AddMinutes(59));
            if (dt != null)
            {
                string sortField = EnterOutGrid.Columns[EnterOutGrid.SortColumnIndex].SortField;
                string sortDirection = EnterOutGrid.SortDirection;
                DataView TableView = dt.DefaultView;
                TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
                EnterOutGrid.DataSource = TableView;
                EnterOutGrid.DataBind();
            }
            else
            {
                FineUI.Alert.Show("没有查询到信息");
            }
        }

        protected void EnterOutGrid_PageIndexChange(object sender, GridPageEventArgs e)
        {
            EnterOutGrid.PageIndex = e.NewPageIndex;
        }

        protected void EnterOutGrid_Sort(object sender, GridSortEventArgs e)
        {
            EnterOutGrid.SortDirection = e.SortDirection;
            EnterOutGrid.SortColumnIndex = e.ColumnIndex;
            EnterOutLogListBinding();
        }

        protected void EnterOutGrid_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
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
            List<SeatManage.ClassModel.ReadingRoomInfo> roomList = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, null, null);
            if (roomList.Count > 0)
            {
                roomList.Insert(0, new SeatManage.ClassModel.ReadingRoomInfo() { Name = "所有阅览室", No = "" });
                ddlReadingRoom.DataTextField = "Name";
                ddlReadingRoom.DataValueField = "No";
                ddlReadingRoom.DataSource = roomList;
                ddlReadingRoom.DataBind();
            }
        }



    }
}