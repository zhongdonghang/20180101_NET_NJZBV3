using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.Data;
using SeatManageWebV2.Code;

namespace SeatManageWebV2.FunctionPages.SeatBespeak
{
    public partial class BespeakNowDaySeat : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!OpVerifiction())
                {
                    Response.Write("请使用正常方式访问网站！");
                    Response.End();
                    return;
                }
                BindLibaray();
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

            try
            {
                SeatManage.ClassModel.ReadingRoomSetting set = new SeatManage.ClassModel.ReadingRoomSetting(roomSet);
                if (Code.NowReadingRoomState.ReadingRoomOpenState(set.RoomOpenSet, nowDate) == SeatManage.EnumType.ReadingRoomStatus.Close)
                {
                    lbf.Icon = Icon.BulletCross;
                    lbf.ToolTip = "阅览室没有开放";
                    lbf.EnablePostBack = false;
                    lbf.Enabled = false;
                    return;
                }
                if (!set.SeatBespeak.Used || !set.SeatBespeak.NowDayBespeak)
                {
                    lbf.Icon = Icon.BulletCross;
                    lbf.ToolTip = "阅览室没有开放预约";
                    lbf.EnablePostBack = false;
                    lbf.Enabled = false;
                    return;
                }

                int canBespeakAmount = int.Parse(row[4].ToString());
                if (canBespeakAmount <= 0)
                {
                    lbf.Icon = Icon.BulletCross;
                    lbf.ToolTip = "没有空余座位";
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

            string roomNum = row[0].ToString().Trim();
            lbf.EnablePostBack = false;

            //加密
            string urlParameters = SeatManage.SeatManageComm.AESAlgorithm.DESEncode(string.Format("roomNo={0}", roomNum));
            lbf.OnClientClick = WindowEdit.GetShowReference("BespeakNowDaySeatLayout.aspx?Param=" + urlParameters, "座位视图");
            //原版
            //lbf.OnClientClick = WindowEdit.GetShowReference(string.Format("BespeakNowDaySeatLayout.aspx?roomId={0}", roomNum), "座位视图");

        }

        void BindingGrid()
        {
            string libno = ddlLibrary.SelectedValue;
            DataTable dt = LogQueryHelper.NowDayBespeakRoomInfo(libno);
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