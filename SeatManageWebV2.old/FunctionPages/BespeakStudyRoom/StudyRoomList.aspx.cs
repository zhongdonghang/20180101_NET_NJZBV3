using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FineUI;

namespace SeatManageWebV2.FunctionPages.BespeakStudyRoom
{
    public partial class StudyRoomList : BasePage
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
            }
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
            LinkButtonField lbf = gridRoomList.FindColumn("appTable") as LinkButtonField;
            string roomNum = row[0].ToString().Trim();


            bool IsUseStudyRoom = bool.Parse(row[7].ToString());
            if (IsUseStudyRoom)
            {
                lbf.IconUrl = "/Images/Hand.png";
                lbf.EnablePostBack = false;
                lbf.Enabled = true;
                lbf.OnClientClick = WindowEdit.GetShowReference(string.Format("BeskpeakStudyRoom.aspx?roomNo={0}", roomNum), "申请表");
            }
            else
            {
                lbf.IconUrl = "/Images/HandDisable.png";
                lbf.ToolTip = "研习间暂未开放";
                lbf.EnablePostBack = false;
                lbf.Enabled = false;
            }

            divremark.InnerHtml = row[2].ToString();
            //divRemaFacilitiesRenmarkrk.InnerHtml = row[3].ToString();
            //divPrecautions.InnerHtml = row[4].ToString();
            //divApplicationInfo.InnerHtml = row[5].ToString(); 
            //remark = row[4].ToString().Replace("\r\n", "<br/>");
        }

        void BindingGrid()
        {
            List<SeatManage.ClassModel.StudyRoomInfo> listStudyRoom = SeatManage.Bll.StudyRoomOperation.GetStudyRoonInfoList(null);
            DataTable dt = new DataTable();
            DataColumn StudyRoomNo = new DataColumn("StudyRoomNo", typeof(string));
            DataColumn StudyRoomName = new DataColumn("StudyRoomName", typeof(string));
            DataColumn Remark = new DataColumn("Remark", typeof(string));
            DataColumn FacilitiesRenmark = new DataColumn("FacilitiesRenmark", typeof(string));
            DataColumn Precautions = new DataColumn("Precautions", typeof(string));
            DataColumn ApplicationInfo = new DataColumn("ApplicationInfo", typeof(string));
            DataColumn StudyImage = new DataColumn("StudyImage", typeof(string));
            DataColumn IsUseStudyRoom = new DataColumn("IsUseStudyRoom", typeof(bool));

            dt.Columns.Add(StudyRoomNo);
            dt.Columns.Add(StudyRoomName);
            dt.Columns.Add(Remark);
            dt.Columns.Add(FacilitiesRenmark);
            dt.Columns.Add(Precautions);
            dt.Columns.Add(ApplicationInfo);
            dt.Columns.Add(StudyImage);
            dt.Columns.Add(IsUseStudyRoom);
            foreach (SeatManage.ClassModel.StudyRoomInfo list in listStudyRoom)
            {
                DataRow row = dt.NewRow();

                row["StudyRoomName"] = list.StudyRoomName;
                row["StudyRoomNo"] = list.StudyRoomNo;
                if (!string.IsNullOrEmpty(list.Remark))
                {
                    row["Remark"] = list.Remark.Replace("\r\n", "<br/>").Replace(" ", "&nbsp;");
                }
                if (!string.IsNullOrEmpty(list.Setting.FacilitiesRenmark))
                {
                    row["FacilitiesRenmark"] = list.Setting.FacilitiesRenmark.Replace("\r\n", "<br/>").Replace(" ", "&nbsp;");
                }
                if (!string.IsNullOrEmpty(list.Setting.Precautions))
                {
                    row["Precautions"] = list.Setting.Precautions.Replace("\r\n", "<br/>").Replace(" ", "&nbsp;");
                }
                if (!string.IsNullOrEmpty(list.Setting.ApplicationInfo))
                {
                    row["ApplicationInfo"] = list.Setting.ApplicationInfo.Replace("\r\n", "<br/>").Replace(" ", "&nbsp;");
                }
                row["StudyImage"] = "~/StudyImage/" + list.RoomImage;
                if (list.Setting.IsUseStudyRoom)
                {
                    row["IsUseStudyRoom"] = true;
                }
                else
                {
                    row["IsUseStudyRoom"] = false;
                }
                dt.Rows.Add(row);
            }
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