using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SeatManageWebV2.FunctionPages.Statistical
{
    public partial class RoomTripsOutUseInfo : BasePage
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
                libraryEnterOutInfo.Visible = false;
                libraryAttendanceInfo.Visible = false;
                Span1.Visible = false;
                Span2.Visible = false;
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
            tl.InnerText = "阅览室进出人次统计（" + rbltype.SelectedItem.Text + "）";
            List<string> roomList = new List<string>();
            foreach (System.Web.UI.WebControls.ListItem li in cblreadingroom.Items)
            {
                if (li.Selected)
                {
                    roomList.Add(li.Value);
                }
            }
            librarySeatUsedInfo.ChartAreas[0].AxisX.Title = "时间";
            librarySeatUsedInfo.ChartAreas[0].AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            librarySeatUsedInfo.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            librarySeatUsedInfo.ChartAreas[0].AxisY.Title = "人次";
            librarySeatUsedInfo.ChartAreas[0].AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            librarySeatUsedInfo.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            librarySeatUsedInfo.ChartAreas[0].AxisX.LabelStyle.Angle = 50;
            switch ((SeatManage.ClassModel.StatisticsType)int.Parse(rbltype.SelectedValue))
            {
                case SeatManage.ClassModel.StatisticsType.DayOfWeek:
                case SeatManage.ClassModel.StatisticsType.DayOfMonth:
                case SeatManage.ClassModel.StatisticsType.MonthOfYear:
                    librarySeatUsedInfo.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                    break;
                case SeatManage.ClassModel.StatisticsType.DayOfYear:
                    librarySeatUsedInfo.ChartAreas[0].AxisX.LabelStyle.Interval = 20;
                    break;
                case SeatManage.ClassModel.StatisticsType.WeekOfYear:
                    librarySeatUsedInfo.ChartAreas[0].AxisX.LabelStyle.Interval = 4;
                    break;
            }
            DataTable dtx = new DataTable();
            dtx.Columns.Add("EnterOutCount", typeof(int));
            dtx.Columns.Add("AttendanceCount", typeof(string));
            dtx.Columns.Add("Room", typeof(string));
            librarySeatUsedInfo.ChartAreas[0].BackColor = System.Drawing.Color.WhiteSmoke;
            librarySeatUsedInfo.Series.Clear();
            if (roomList.Count == 0)
            {
                List<string> libno = new List<string>();
                libno.Add(ddlLibrary.SelectedItem.Value);
                SeatManage.ClassModel.ReadingRoomUsageStatistics libUsage = rrsta.StatisticsDayLibData(libno, DateTime.Parse(startdate.Value), DateTime.Parse(enddate.Value), ((SeatManage.ClassModel.StatisticsType)int.Parse(rbltype.SelectedValue)));
                DataTable dt = libUsage.StatisticsData;
                DataView dv = dt.DefaultView;
                if (rbleatype.SelectedValue == "EnterOutCount")
                {
                    System.Web.UI.DataVisualization.Charting.Series ser = new System.Web.UI.DataVisualization.Charting.Series();
                    ser.Name = "EnterOutCount";
                    ser.LegendText = "进出人次";
                    ser.Points.DataBindXY(dv, "Date", dv, "EnterOutCount");
                    ser.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
                    ser.ChartArea = "ChartArea1";
                    ser.IsValueShownAsLabel = true;
                    ser.YValuesPerPoint = 4;
                    librarySeatUsedInfo.Series.Add(ser);
                }
                else
                {
                    System.Web.UI.DataVisualization.Charting.Series ser = new System.Web.UI.DataVisualization.Charting.Series();
                    ser.Name = "AttendanceCount";
                    ser.LegendText = "上座率";
                    ser.Points.DataBindXY(dv, "Date", dv, "Attendance");
                    ser.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
                    ser.ChartArea = "ChartArea1";
                    ser.IsValueShownAsLabel = true;
                    ser.YValuesPerPoint = 4;
                    librarySeatUsedInfo.Series.Add(ser);
                }

                DataRow dr = dtx.NewRow();
                dr["Room"] = ddlLibrary.SelectedItem.Text;
                dr["EnterOutCount"] = libUsage.EnterOurCount;
                dr["AttendanceCount"] = (libUsage.Attendance * 100).ToString("0.00");
                dtx.Rows.Add(dr);
            }
            else
            {
                List<SeatManage.ClassModel.ReadingRoomInfo> rooms = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(roomList, null, null);
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
                {
                    SeatManage.ClassModel.ReadingRoomUsageStatistics roomUsage = rrsta.StatisticsDayRoomData(room, DateTime.Parse(startdate.Value), DateTime.Parse(enddate.Value), ((SeatManage.ClassModel.StatisticsType)int.Parse(rbltype.SelectedValue)));
                    DataTable dt = roomUsage.StatisticsData;
                    DataView dv = dt.DefaultView;
                    if (rbleatype.SelectedValue == "EnterOutCount")
                    {
                        System.Web.UI.DataVisualization.Charting.Series ser = new System.Web.UI.DataVisualization.Charting.Series();
                        ser.Name = room.Name + "EnterOutCount";
                        ser.LegendText = room.Name + "进出人次";
                        ser.Points.DataBindXY(dv, "Date", dv, "EnterOutCount");
                        ser.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
                        ser.ChartArea = "ChartArea1";
                        ser.IsValueShownAsLabel = true;
                        ser.YValuesPerPoint = 4;
                        librarySeatUsedInfo.Series.Add(ser);
                    }
                    else
                    {
                        System.Web.UI.DataVisualization.Charting.Series ser = new System.Web.UI.DataVisualization.Charting.Series();
                        ser.Name = room.Name + "AttendanceCount";
                        ser.LegendText = room.Name + "上座率";
                        ser.Points.DataBindXY(dv, "Date", dv, "Attendance");
                        ser.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
                        ser.ChartArea = "ChartArea1";
                        ser.IsValueShownAsLabel = true;
                        ser.YValuesPerPoint = 4;
                        librarySeatUsedInfo.Series.Add(ser);
                    }

                    DataRow dr = dtx.NewRow();
                    dr["Room"] = roomUsage.RoomInfo.Name;
                    dr["EnterOutCount"] = roomUsage.EnterOurCount;
                    dr["AttendanceCount"] = (roomUsage.Attendance * 100).ToString("0.00");
                    dtx.Rows.Add(dr);
                }
            }
            if (rbleatype.SelectedValue == "EnterOutCount")
            {
                Span1.Visible = false;
                Span2.Visible = true;
                libraryEnterOutInfo.Visible = true;
                libraryAttendanceInfo.Visible = false;

                libraryEnterOutInfo.ChartAreas[0].AxisX.Title = "阅览室";
                libraryEnterOutInfo.ChartAreas[0].AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
                libraryEnterOutInfo.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
                libraryEnterOutInfo.ChartAreas[0].AxisY.Title = "人次";
                libraryEnterOutInfo.ChartAreas[0].AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
                libraryEnterOutInfo.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
                libraryEnterOutInfo.ChartAreas[0].AxisX.LabelStyle.Angle = 50;
                libraryEnterOutInfo.ChartAreas[0].BackColor = System.Drawing.Color.WhiteSmoke;
                libraryEnterOutInfo.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                libraryEnterOutInfo.Series.Clear();

                System.Web.UI.DataVisualization.Charting.Series sereo = new System.Web.UI.DataVisualization.Charting.Series();
                sereo.Name = "EnterOutCount";
                sereo.LegendText = "进出人次";
                sereo.Points.DataBindXY(dtx.DefaultView, "Room", dtx.DefaultView, "EnterOutCount");
                sereo.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
                sereo.ChartArea = "ChartArea1";
                sereo["PointWidth"] = "0.1";
                sereo.IsValueShownAsLabel = true;
                sereo.YValuesPerPoint = 4;
                libraryEnterOutInfo.Series.Add(sereo);


            }
            else
            {
                libraryEnterOutInfo.Visible = false;
                libraryAttendanceInfo.Visible = true;
                Span1.Visible = true;
                Span2.Visible = false;

                libraryAttendanceInfo.ChartAreas[0].AxisX.Title = "阅览室";
                libraryAttendanceInfo.ChartAreas[0].AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
                libraryAttendanceInfo.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
                libraryAttendanceInfo.ChartAreas[0].AxisY.Title = "人次";
                libraryAttendanceInfo.ChartAreas[0].AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
                libraryAttendanceInfo.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
                libraryAttendanceInfo.ChartAreas[0].AxisX.LabelStyle.Angle = 50;
                libraryAttendanceInfo.ChartAreas[0].BackColor = System.Drawing.Color.WhiteSmoke;
                libraryAttendanceInfo.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                libraryAttendanceInfo.Series.Clear();

                System.Web.UI.DataVisualization.Charting.Series sera = new System.Web.UI.DataVisualization.Charting.Series();
                sera.Name = "EnterOutCount";
                sera.LegendText = "上座率（%）";
                sera.Points.DataBindXY(dtx.DefaultView, "Room", dtx.DefaultView, "AttendanceCount");
                sera.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
                sera.Color = System.Drawing.Color.IndianRed;
                sera.ChartArea = "ChartArea1";
                sera["PointWidth"] = "0.1";
                sera.IsValueShownAsLabel = true;
                sera.YValuesPerPoint = 4;
                libraryAttendanceInfo.Series.Add(sera);
            }
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