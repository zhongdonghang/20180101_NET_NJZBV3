using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SeatManageWebV2.FunctionPages.StudyRoomManage
{
    public partial class StudyRoomManage : BasePage
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
                BindStudyRoom();
                btnAddStudyRoom.OnClientClick = WindowEdit.GetShowReference("StudyRoomEdit.aspx?flag=add", "添加功能页");//为btnAddStudyRoom新增点击事件
            }
        }

        private void BindStudyRoom()
        {
            List<SeatManage.ClassModel.StudyRoomInfo> listStudyRoom = SeatManage.Bll.StudyRoomOperation.GetStudyRoonInfoList(null);
            DataTable dt = new DataTable();
            DataColumn StudyRoomNo = new DataColumn("StudyRoomNo", typeof(string));
            DataColumn StudyRoomName = new DataColumn("StudyRoomName", typeof(string));
            DataColumn Remark = new DataColumn("Remark", typeof(string));
            dt.Columns.Add(StudyRoomNo);
            dt.Columns.Add(StudyRoomName);
            dt.Columns.Add(Remark);
            foreach (SeatManage.ClassModel.StudyRoomInfo list in listStudyRoom)
            {
                DataRow row = dt.NewRow();
                row["StudyRoomName"] = list.StudyRoomName;
                row["StudyRoomNo"] = list.StudyRoomNo;
                row["Remark"] = list.Remark;
                dt.Rows.Add(row);
            }
            string sortField = GridStudyRoom.Columns[GridStudyRoom.SortColumnIndex].SortField;
            string sortDirection = GridStudyRoom.SortDirection;
            DataView TableView = dt.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            GridStudyRoom.DataSource = TableView;
            GridStudyRoom.DataBind();
        }
        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridStudyRoom_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            //FineUI.LinkButtonField lbfx = GridStudyRoom.FindColumn("StudyRoomdelete") as FineUI.LinkButtonField;
            //DataRowView rowx = e.DataItem as DataRowView;
            //string roomnox = rowx[0].ToString();
            //lbfx.OnClientClick = WindowDelete.GetShowReference("../SystemSet/DeletePassword.aspx?Type=StudyRoom&id=" + roomnox + "", "阅览室删除");
        }

        //窗体关闭事件
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindStudyRoom();
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
        protected void GridStudyRoom_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            GridStudyRoom.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridStudyRoom_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            GridStudyRoom.SortDirection = e.SortDirection;
            GridStudyRoom.SortColumnIndex = e.ColumnIndex;
            BindStudyRoom();
        }
        protected void GridStudyRoom_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "ActionDelete")
            {
                SeatManage.ClassModel.StudyRoomInfo room = new SeatManage.ClassModel.StudyRoomInfo();
                room.StudyRoomNo = GridStudyRoom.Rows[e.RowIndex].DataKeys[0].ToString();
                if (!SeatManage.Bll.StudyRoomOperation.DeleteStudyRoom(room))
                {
                    FineUI.Alert.ShowInTop("删除失败！");
                }
                else
                {
                    FineUI.Alert.ShowInTop("删除完成！");
                    BindStudyRoom();
                }
            }
        }
    }
}