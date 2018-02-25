using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

namespace SeatManageWebV2.FunctionPages.Statistical
{
    public partial class ReadingRoomUsageInfo : BasePage
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
        protected void ddlReaderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(ddlReaderType.SelectedItem.Value))
            //{
            //    ReadingRoomBinding();
            //}
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
            Code.ReadingRoomStatistics rrsta=new Code.ReadingRoomStatistics();
            List<string> roomList = new List<string>();
            foreach (System.Web.UI.WebControls.ListItem li in cblreadingroom.Items)
            {
                if (li.Selected)
                {
                    roomList.Add(li.Value);
                }
            }
            if (roomList.Count == 0)
            {
                foreach (System.Web.UI.WebControls.ListItem li in cblreadingroom.Items)
                {
                    roomList.Add(li.Value);
                }
            }
            for (int i = 0; i < roomList.Count; i++)
            {
                if (i % 2 == 0)
                {
                    HtmlTableRow tr = new HtmlTableRow();
                    tr.Align = "center";
                    HtmlTableCell td = new HtmlTableCell();
                    tr.Cells.Add(td);
                    td = new HtmlTableCell();
                    tr.Cells.Add(td);
                    roomtable.Rows.Add(tr);
                }
                roomtable.Rows[i / 2].Cells[i % 2].Controls.Add(rrsta.GetReadingRoomUsageInfo(roomList[i],ddlReaderType.SelectedItem.Value, DateTime.Parse(startdate.Value), DateTime.Parse(enddate.Value)));
            }

            //创建表格

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
            SeatManage.Bll.T_SM_Reader readerbll = new SeatManage.Bll.T_SM_Reader();
            List<string> readertype = readerbll.GetReaderType();
            if (readertype != null)
            {
                foreach (string type in readertype)
                {
                    ddlReaderType.Items.Add(new ListItem(type, type));
                }
            }
            ddlReaderType.Items.Insert(0, new ListItem("全部类型", ""));
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