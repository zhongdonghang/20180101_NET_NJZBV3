using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SeatManageWebV2.FunctionPages.SchoolInfoManage
{
    public partial class ReadingRoomInfo : BasePage
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
                BindReadingRoom();
                btnAddReadRoom.OnClientClick = WindowEdit.GetShowReference("ReadingRoomEdit.aspx?flag=add", "添加功能页");//为btnAddReadRoom新增点击事件
            }
        }

        private void BindReadingRoom()
        {
            //获取阅览室列表
            List<SeatManage.ClassModel.ReadingRoomInfo> listReadingRoom = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null);
            DataTable dt = new DataTable();
            DataColumn ReadingRoomNo = new DataColumn("ReadingRoomNo", typeof(string));
            DataColumn ReadingRoomName = new DataColumn("ReadingRoomName", typeof(string));
            DataColumn LibraryName = new DataColumn("LibraryName", typeof(string));
            DataColumn SchoolName = new DataColumn("SchoolName", typeof(string));
            dt.Columns.Add(ReadingRoomNo);
            dt.Columns.Add(ReadingRoomName);
            dt.Columns.Add(LibraryName);
            dt.Columns.Add(SchoolName);
            foreach (SeatManage.ClassModel.ReadingRoomInfo list in listReadingRoom)
            {
                DataRow row = dt.NewRow();
                row["LibraryName"] = list.Libaray.Name;
                row["ReadingRoomName"] = list.Name;
                row["ReadingRoomNo"] = list.No;
                row["SchoolName"] = list.Libaray.School.Name;
                dt.Rows.Add(row);
            }
            string sortField = GridReadRoom.Columns[GridReadRoom.SortColumnIndex].SortField;
            string sortDirection = GridReadRoom.SortDirection;
            DataView TableView = dt.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            GridReadRoom.DataSource = TableView;
            GridReadRoom.DataBind();
        }
        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridReadRoom_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            FineUI.LinkButtonField lbfx = GridReadRoom.FindColumn("ReadingRoomdelete") as FineUI.LinkButtonField;
            DataRowView rowx = e.DataItem as DataRowView;
            string roomnox = rowx[0].ToString();
            lbfx.OnClientClick = WindowDelete.GetShowReference("../SystemSet/DeletePassword.aspx?Type=ReadingRoom&id=" + roomnox + "", "阅览室删除");
        }

        //窗体关闭事件
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindReadingRoom();
        }
        //窗体关闭事件
        protected void WindowSetting_Close(object sender, FineUI.WindowCloseEventArgs e)
        {

        }
        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridReadRoom_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            GridReadRoom.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridReadRoom_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            GridReadRoom.SortDirection = e.SortDirection;
            GridReadRoom.SortColumnIndex = e.ColumnIndex;
            BindReadingRoom();
        }
        protected void GridReadRoom_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            //if (e.CommandName == "ActionDelete")
            //{
            //    SeatManage.ClassModel.ReadingRoomInfo room = new SeatManage.ClassModel.ReadingRoomInfo();
            //    room.No = GridReadRoom.Rows[e.RowIndex].DataKeys[0].ToString();
            //    if (!SeatManage.Bll.T_SM_ReadingRoom.DeleteReadingRoom(room))
            //    {
            //        FineUI.Alert.ShowInTop("删除失败！");
            //    }
            //    else
            //    {
            //        FineUI.Alert.ShowInTop("删除完成！");
            //        BindReadingRoom();
            //    }
            //}
        }

    }
}