using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SeatManageWebV2.FunctionPages.Statistical
{
    public partial class RoomSeatUseInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                startdate.Value = DateTime.Now.AddYears(-1).AddDays(-1).ToShortDateString();
                enddate.Value = DateTime.Now.AddDays(-1).ToShortDateString();
                BindLibrary();
                if (!string.IsNullOrEmpty(ddlLibrary.SelectedItem.Value))
                {
                    ReadingRoomBinding();
                }
            }
        }
        protected void ddlLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlLibrary.SelectedItem.Value))
            {
                ReadingRoomBinding();
            }
        }
        protected void btn1_Click(object sender, EventArgs e)
        {
            ChartDataBinding();
        }
        private void ChartDataBinding()
        {
            if (DateTime.Parse(enddate.Value).Date >= DateTime.Now.Date)
            {
                this.RegisterStartupScript("日期错误", "<script>alert('只能查询当天之前的数据！');</script>");
                return;
            }
            if (DateTime.Parse(startdate.Value).Date > DateTime.Parse(enddate.Value).Date)
            {
                this.RegisterStartupScript("日期错误", "<script>alert('开始时间必须小于结束时间！');</script>");
                return;
            }
            SeatManageWebV2.Code.ReadingRoomStatistics rrsta=new Code.ReadingRoomStatistics();
            List<string> roomList = new List<string>();
            foreach (System.Web.UI.WebControls.ListItem li in cblreadingroom.Items)
            {
                if (li.Selected)
                {
                    roomList.Add(li.Value);
                }
            }

            List<SeatManage.ClassModel.ReadingRoomInfo> rooms = new List<SeatManage.ClassModel.ReadingRoomInfo>();
            if (roomList.Count == 0)
            {
                List<string> libno = new List<string>();
                libno.Add(ddlLibrary.SelectedItem.Value);
                rooms = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, libno, null);
            }
            else
            {
                rooms = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(roomList, null, null);
            }

            SeatManage.ClassModel.ReadingRoomUsageStatistics roomUsage = rrsta.StatisticsSelectAndLeave(rooms, DateTime.Parse(startdate.Value), DateTime.Parse(enddate.Value));

            Chart1.Series.Clear();
            System.Web.UI.DataVisualization.Charting.Series sers = new System.Web.UI.DataVisualization.Charting.Series();
            sers.Name = "EnterOutCount";
            sers.Points.DataBindXY(roomUsage.SeatSelect.DefaultView, "SelectType", roomUsage.SeatSelect.DefaultView, "Count");
            sers.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
            sers.ChartArea = "ChartArea1";
            sers.IsValueShownAsLabel = true;
            sers.YValuesPerPoint = 4;
            Chart1.Series.Add(sers);

            Chart2.Series.Clear();
            System.Web.UI.DataVisualization.Charting.Series serl = new System.Web.UI.DataVisualization.Charting.Series();
            serl.Name = "EnterOutCount";
            serl.Points.DataBindXY(roomUsage.SeatLeave.DefaultView, "LeaveType", roomUsage.SeatLeave.DefaultView, "Count");
            serl.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
            serl.ChartArea = "ChartArea1";
            serl.IsValueShownAsLabel = true;
            serl.YValuesPerPoint = 4;
            Chart2.Series.Add(serl);

            Chart3.Series.Clear();
            System.Web.UI.DataVisualization.Charting.Series sert = new System.Web.UI.DataVisualization.Charting.Series();
            sert.Name = "EnterOutCount";
            sert.Points.DataBindXY(roomUsage.SeatTime.DefaultView, "SeatTime", roomUsage.SeatTime.DefaultView, "Count");
            sert.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
            sert.ChartArea = "ChartArea1";
            sert.IsValueShownAsLabel = true;
            sert.YValuesPerPoint = 4;
            Chart3.Series.Add(sert);

        }
        private void BindLibrary()
        {
            List<SeatManage.ClassModel.LibraryInfo> libList = SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, null, null);
            if (libList != null)
            {
                ddlLibrary.DataTextField = "Name";
                ddlLibrary.DataValueField = "No";
                ddlLibrary.DataSource = libList;
                ddlLibrary.DataBind();
            }
        }
        private void ReadingRoomBinding()
        {
            List<string> libno = new List<string>();
            libno.Add(ddlLibrary.SelectedItem.Value);
            List<SeatManage.ClassModel.ReadingRoomInfo> rooms = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, libno, null);
            cblreadingroom.Items.Clear();
            if (rooms != null)
            {
                
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
                {
                    System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem();
                    string name = room.Name;
                    byte[] strl = System.Text.Encoding.Default.GetBytes(name);
                    for (int i = 0; i < 20 - strl.Length; i++)
                    {
                        name += "&nbsp;";
                    }
                    li.Text = name;
                    li.Value = room.No;
                    cblreadingroom.Items.Add(li);
                }
            }
        }
    }
}