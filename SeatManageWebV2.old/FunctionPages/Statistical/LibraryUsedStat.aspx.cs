using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SeatManageWebV2.FunctionPages.Statistical
{
    public partial class LibraryUsedStat : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLibrary();
                if (ddlLibrary.Items.Count > 0)
                {
                    LibDataBinding();
                }
            }

        }

        protected void ddlLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(ddlLibrary.SelectedItem.Value))
            {
                LibDataBinding();
            }
        }

        private void LibDataBinding()
        {
            librarySeatUsedInfo.Titles["titleName"].Text = string.Format("{0}{1}座位使用情况", DateTime.Now.ToLongDateString(), ddlLibrary.SelectedItem.Text);
            DataTable dt = SeatManageWebV2.Code.LogQueryHelper.LibrarySeatInfo(ddlLibrary.SelectedItem.Value);
            DataView dv = dt.DefaultView;
            librarySeatUsedInfo.ChartAreas[0].Area3DStyle.Enable3D = true;
            librarySeatUsedInfo.ChartAreas[0].Area3DStyle.Inclination = 15;
            librarySeatUsedInfo.ChartAreas[0].Area3DStyle.Rotation = 15;
            librarySeatUsedInfo.ChartAreas[0].Area3DStyle.PointDepth = 25;
            librarySeatUsedInfo.ChartAreas[0].Area3DStyle.WallWidth = 0;
            librarySeatUsedInfo.ChartAreas[0].Area3DStyle.IsClustered = true;
            librarySeatUsedInfo.ChartAreas[0].AxisX.Interval = 1;
            librarySeatUsedInfo.ChartAreas[0].AxisX.LabelStyle.Angle = 50;
            librarySeatUsedInfo.ChartAreas[0].BackColor = System.Drawing.Color.WhiteSmoke;
            librarySeatUsedInfo.Series["SeatUsedAmount"].Points.DataBindXY(dv, "ReadingRoomName", dv, "SeatUsedAmount");
            librarySeatUsedInfo.Series["LeisureSeat"].Points.DataBindXY(dv, "ReadingRoomName", dv, "LeisureSeat");
            //librarySeatUsedInfo.Series["PersonTimes"].Points.DataBindXY(dv, "ReadingRoomName", dv, "PersonTimes");
        }

        /// <summary>
        /// 绑定阅览室信息
        /// </summary>
        private void BindLibrary()
        {
            List<SeatManage.ClassModel.LibraryInfo> libList = SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, null, null);
            if (libList != null)
            {
                ddlLibrary.Items.Clear();
                //libList.Insert(0, new SeatManage.ClassModel.LibraryInfo() { Name = "请选择", No = "" });
                ddlLibrary.DataTextField = "Name";
                ddlLibrary.DataValueField = "No";
                ddlLibrary.DataSource = libList;
                ddlLibrary.DataBind();
            }
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            LibDataBinding();
        }
    }
}