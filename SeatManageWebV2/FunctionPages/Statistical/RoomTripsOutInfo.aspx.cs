using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.Excel;
using SeatManageWebV2.Code;
using DataTable = System.Data.DataTable;

namespace SeatManageWebV2.FunctionPages.Statistical
{
    public partial class RoomTripsOutInfo : BasePage
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
        protected void btn2_Click(object sender, EventArgs e)
        {
            ToExcel();
        }

        private void ToExcel()
        {
            if (Session["DataTables"] == null)
            {
                this.RegisterStartupScript("无结果", "<script>alert('请先进行统计！');</script>");
                return;
            }

            Dictionary<string, DataTable> dataTables = Session["DataTables"] as Dictionary<string, DataTable>;
            if (dataTables == null)
            {
                this.RegisterStartupScript("无结果", "<script>alert('请先进行统计！');</script>");
                return;
            }

            try
            {
                DataToExcel dte = new DataToExcel();
                dte.DataGridViewToExcel(dataTables);
            }
            catch (Exception ex )
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.ToString());
            }

        }
        private void ChartDataBinding()
        {
            if (DateTime.Parse(enddate.Value).Date >= DateTime.Now.Date)
            {
                RegisterStartupScript("日期错误", "<script>alert('只能查询当天之前的数据！');</script>");
                return;
            }
            if (DateTime.Parse(startdate.Value).Date > DateTime.Parse(enddate.Value).Date)
            {
                RegisterStartupScript("日期错误", "<script>alert('开始时间必须小于结束时间！');</script>");
                return;
            }
            Dictionary<string, DataTable> dataTables = new Dictionary<string, DataTable>();
            ReadingRoomStatistics rrsta = new ReadingRoomStatistics();
            List<string> roomList = new List<string>();
            foreach (ListItem li in cblreadingroom.Items)
            {
                if (li.Selected)
                {
                    roomList.Add(li.Value);
                }
            }
            librarySeatUsedInfo.ChartAreas[0].AxisX.Interval = 1;
            librarySeatUsedInfo.ChartAreas[0].AxisX.Title = "时";
            librarySeatUsedInfo.ChartAreas[0].AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            librarySeatUsedInfo.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            librarySeatUsedInfo.ChartAreas[0].AxisY.Title = "人次";
            librarySeatUsedInfo.ChartAreas[0].AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            librarySeatUsedInfo.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            librarySeatUsedInfo.ChartAreas[0].BackColor = System.Drawing.Color.WhiteSmoke;
            librarySeatUsedInfo.Series.Clear();
            if (roomList.Count == 0)
            {
                List<string> libno = new List<string>();
                libno.Add(ddlLibrary.SelectedItem.Value);
                DataTable dt = rrsta.StatisticsHoursLibData(libno, DateTime.Parse(startdate.Value), DateTime.Parse(enddate.Value));
                DataView dv = dt.DefaultView;

                dataTables.Add(ddlLibrary.SelectedItem.Text, dt);
                if (cbselect.Checked)
                {

                    System.Web.UI.DataVisualization.Charting.Series ser = new System.Web.UI.DataVisualization.Charting.Series();
                    ser.Name = "EnterCount";
                    ser.LegendText = "入座人次";
                    ser.Points.DataBindXY(dv, "Hour", dv, "EnterCount");
                    ser.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
                    ser.ChartArea = "ChartArea1";
                    ser.IsValueShownAsLabel = true;
                    ser.YValuesPerPoint = 4;
                    librarySeatUsedInfo.Series.Add(ser);
                }
                if (cbleave.Checked)
                {
                    System.Web.UI.DataVisualization.Charting.Series serx = new System.Web.UI.DataVisualization.Charting.Series();
                    serx.Name = "LeaveCount";
                    serx.LegendText = "离开人次";
                    serx.Points.DataBindXY(dv, "Hour", dv, "OutCount");
                    serx.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
                    serx.ChartArea = "ChartArea1";
                    serx.IsValueShownAsLabel = true;
                    serx.YValuesPerPoint = 4;
                    librarySeatUsedInfo.Series.Add(serx);
                }
                if (cbonseat.Checked)
                {
                    System.Web.UI.DataVisualization.Charting.Series serx = new System.Web.UI.DataVisualization.Charting.Series();
                    serx.Name = "SeatCount";
                    serx.LegendText = "在座人数";
                    serx.Points.DataBindXY(dv, "Hour", dv, "SeatCount");
                    serx.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
                    serx.ChartArea = "ChartArea1";
                    serx.IsValueShownAsLabel = true;
                    serx.YValuesPerPoint = 4;
                    librarySeatUsedInfo.Series.Add(serx);
                }

            }
            else
            {
                List<SeatManage.ClassModel.ReadingRoomInfo> rooms = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(roomList);
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
                {
                    SeatManage.ClassModel.ReadingRoomUsageStatistics roomUsage = rrsta.StatisticsHoursRoomData(room, DateTime.Parse(startdate.Value), DateTime.Parse(enddate.Value));
                    DataTable dt = roomUsage.StatisticsData;
                    DataView dv = dt.DefaultView;
                    dataTables.Add(room.Name, dt);
                    if (cbselect.Checked)
                    {

                        System.Web.UI.DataVisualization.Charting.Series ser = new System.Web.UI.DataVisualization.Charting.Series();
                        ser.Name = room.No + "EnterCount";
                        ser.LegendText = room.Name + "入座人次";
                        ser.Points.DataBindXY(dv, "Hour", dv, "EnterCount");
                        ser.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
                        ser.ChartArea = "ChartArea1";
                        ser.IsValueShownAsLabel = true;
                        ser.YValuesPerPoint = 4;
                        librarySeatUsedInfo.Series.Add(ser);
                    }
                    if (cbleave.Checked)
                    {
                        System.Web.UI.DataVisualization.Charting.Series serx = new System.Web.UI.DataVisualization.Charting.Series();
                        serx.Name = room.No + "LeaveCount";
                        serx.LegendText = room.Name + "离开人次";
                        serx.Points.DataBindXY(dv, "Hour", dv, "OutCount");
                        serx.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
                        serx.ChartArea = "ChartArea1";
                        serx.IsValueShownAsLabel = true;
                        serx.YValuesPerPoint = 4;
                        librarySeatUsedInfo.Series.Add(serx);
                    }
                    if (cbonseat.Checked)
                    {
                        System.Web.UI.DataVisualization.Charting.Series serx = new System.Web.UI.DataVisualization.Charting.Series();
                        serx.Name = room.No + "SeatCount";
                        serx.LegendText = room.Name + "在座人数";
                        serx.Points.DataBindXY(dv, "Hour", dv, "SeatCount");
                        serx.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
                        serx.ChartArea = "ChartArea1";
                        serx.IsValueShownAsLabel = true;
                        serx.YValuesPerPoint = 4;
                        librarySeatUsedInfo.Series.Add(serx);
                    }
                }
            }
            Session["DataTables"] = dataTables;

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
                    ListItem li = new ListItem();
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