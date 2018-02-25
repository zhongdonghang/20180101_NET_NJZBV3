using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SeatManageWebV2.Code;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SeatBespeak
{
    public partial class BespeakSeat : BasePage
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
                BindLibaray();
                dpStartDate.SelectedDate = DateTime.Now.AddDays(1);
                dpStartDate.MinDate = DateTime.Now.AddDays(1);

            }
            BindingGrid();
        }
        /// <summary>
        /// 绑定图书馆下拉列表
        /// </summary>
        protected void BindLibaray()
        {
            List<SeatManage.ClassModel.LibraryInfo> listLibrary = new List<SeatManage.ClassModel.LibraryInfo>();
            listLibrary = SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, null, null);
            if (listLibrary != null)
            {
                ddlLibrary.DataTextField = "Name";
                ddlLibrary.DataValueField = "No";
                ddlLibrary.DataSource = listLibrary;
                ddlLibrary.DataBind();
            }
        }
        protected void btnnewdate_OnClick(object sender, EventArgs e)
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
            DateTime selectedDate = this.dpStartDate.SelectedDate.Value;
            DateTime nowDate = SeatManage.Bll.ServiceDateTime.Now;
            DataRowView row = e.DataItem as DataRowView;
            string roomSet = row[5].ToString();
            LinkButtonField lbf = gridRoomList.FindColumn("seatUsedView") as LinkButtonField;
            if (string.IsNullOrEmpty(roomSet))
            {
                lbf.Icon = Icon.BulletCross;
                lbf.ToolTip = "该阅览室没有配置";
                lbf.EnablePostBack = false;
                lbf.Enabled = false;
                return;
            }
            if (selectedDate.CompareTo(nowDate) <= 0)
            {
                //选择日期小于或者等于当前日期 
                lbf.Icon = Icon.BulletCross;
                lbf.ToolTip = "日期选择错误";
                lbf.EnablePostBack = false;
                lbf.Enabled = false;
                return;
            }
            try
            {
                SeatManage.ClassModel.ReadingRoomSetting set = new SeatManage.ClassModel.ReadingRoomSetting(roomSet);

                if (!set.SeatBespeak.Used || set.RoomOpenSet.UninterruptibleModel)
                {
                    lbf.Icon = Icon.BulletCross;
                    lbf.ToolTip = "阅览室没有开放预约";
                    lbf.EnablePostBack = false;
                    lbf.Enabled = false;
                    return;
                }
                if (!dateBespeak(set.SeatBespeak, nowDate))
                {
                    lbf.Icon = Icon.BulletCross;
                    lbf.ToolTip = "该日期座位不能预约";
                    lbf.EnablePostBack = false;
                    lbf.Enabled = false;
                    return;
                }
                if (!timeCanBespeak(set.SeatBespeak, nowDate))
                {
                    lbf.Icon = Icon.BulletCross;
                    lbf.ToolTip = string.Format("预约时间为：{0}到{1}", set.SeatBespeak.CanBespeatTimeSpace.BeginTime, set.SeatBespeak.CanBespeatTimeSpace.EndTime);
                    lbf.EnablePostBack = false;
                    lbf.Enabled = false;
                    return;
                }

                int canBespeakAmount = int.Parse(row[4].ToString());
                if (canBespeakAmount <= 0)
                {
                    lbf.Icon = Icon.BulletCross;
                    lbf.ToolTip = "座位已被全部预约";
                    lbf.Enabled = false;
                }
                else
                {
                    lbf.Icon = Icon.Zoom;
                    lbf.ToolTip = "预约座位";
                    lbf.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                FineUI.Alert.Show(string.Format("阅览室设置不正确：{0}", ex.Message));
            }
            string roomNum = row[0].ToString();
            lbf.EnablePostBack = false;
            lbf.OnClientClick = WindowEdit.GetShowReference(string.Format("BespeakSeatLayout.aspx?roomId={0}&date={1}", roomNum, this.dpStartDate.SelectedDate.Value.ToBinary()), "座位视图");

        }


        /// <summary>
        /// 判断选择的日期是否可以预约，false为不可预约
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        private bool dateBespeak(SeatManage.ClassModel.SeatBespeakSet set, DateTime nowDate)
        {

            DateTime selectedDate = this.dpStartDate.SelectedDate.Value;
            TimeSpan span = selectedDate.Date - nowDate.Date;
            //判断当天是否大于选择的日期
            if (span.Days > set.BespeakBeforeDays)
            {
                return false;
            }
            for (int i = 0; i < set.NoBespeakDates.Count; i++)
            {
                try
                {
                    DateTime sDate = DateTime.Parse(this.dpStartDate.SelectedDate.Value.Month.ToString() + "-" + this.dpStartDate.SelectedDate.Value.Day.ToString());
                    DateTime beginDate = DateTime.Parse(set.NoBespeakDates[i].BeginTime);
                    DateTime endDate = DateTime.Parse(set.NoBespeakDates[i].EndTime);
                    if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(beginDate, endDate, sDate) || sDate.CompareTo(beginDate) == 0 || sDate.CompareTo(endDate) == 0)
                    {//如果当前时间符合某个不可预约的规则，则直接返回false，不可预约
                        return false;
                    }
                }
                catch
                {//日期转换遇到异常，则忽略 
                }
            }
            return true;
        }
        /// <summary>
        /// 判断当前时间是否可以预约
        /// </summary>
        /// <param name="set"></param>
        /// <param name="nowDate"></param>
        /// <returns></returns>
        private bool timeCanBespeak(SeatManage.ClassModel.SeatBespeakSet set, DateTime nowDate)
        {
            try
            {
                DateTime beginTime = DateTime.Parse(string.Format("{0} {1}", nowDate.ToShortDateString(), set.CanBespeatTimeSpace.BeginTime));
                DateTime endTime = DateTime.Parse(string.Format("{0} {1}", nowDate.ToShortDateString(), set.CanBespeatTimeSpace.EndTime));
                if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(beginTime, endTime, nowDate))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        void BindingGrid()
        {
            string libno = ddlLibrary.SelectedValue;
            DateTime date = this.dpStartDate.SelectedDate.Value;
            DataTable dt = LogQueryHelper.BespeakRoomList(this.dpStartDate.SelectedDate.Value, libno);
            string sortField = gridRoomList.Columns[gridRoomList.SortColumnIndex].SortField;
            string sortDirection = gridRoomList.SortDirection;
            DataView TableView = dt.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            gridRoomList.DataSource = TableView;
            gridRoomList.DataBind();

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
    }
}