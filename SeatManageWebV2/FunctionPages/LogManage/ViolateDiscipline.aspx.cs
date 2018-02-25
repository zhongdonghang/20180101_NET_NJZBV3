using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SeatManage.EnumType;
using FineUI;

namespace SeatManageWebV2.FunctionPages.LogManage
{
    public partial class ViolateDiscipline : BasePage
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
                BindDDLReadingRoom();
                dpStartDate.SelectedDate = DateTime.Now.AddDays(-7);
                dpEndDate.SelectedDate = DateTime.Now;
                btnAdd.OnClientClick = WindowEdit.GetShowReference("AddViolateDiscipline.aspx", "添加违规记录");
                btnDeleteBySearch.OnClientClick = WindowEdit.GetShowReference("DelViolateDisciplineBySearch.aspx", "按条件删除违规记录");
            }
        }
        /// <summary>
        /// 绑定管理的阅览室下拉列表
        /// </summary>
        private void BindDDLReadingRoom()
        {
            List<SeatManage.ClassModel.ReadingRoomInfo> roomlist = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null);
            roomlist.Insert(0, new SeatManage.ClassModel.ReadingRoomInfo() { Name = "所有阅览室", No = "" });
            ddlReadingRoom.DataTextField = "Name";
            ddlReadingRoom.DataValueField = "No";
            ddlReadingRoom.DataSource = roomlist;
            ddlReadingRoom.DataBind();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string sortField = VRGrid.Columns[VRGrid.SortColumnIndex].SortField;
            string sortDirection = VRGrid.SortDirection;
            DateTime starttime = dpStartDate.SelectedDate.Value;

            DateTime endtime = dpEndDate.SelectedDate.Value.AddHours(23).AddMinutes(59);
            if (!string.IsNullOrEmpty(dpStartDate.Text) && !string.IsNullOrEmpty(dpEndDate.Text) && starttime >= endtime)
            {
                FineUI.Alert.Show("结束日期必须大于等于开始日期");
                return;
            }
            DataTable table = GetUserInfoDateTable(starttime.ToString(), endtime.ToString());
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            VRGrid.DataSource = TableView;
            VRGrid.DataBind();
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetUserInfoDateTable(string starttime, string endtime)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("AddTime", typeof(DateTime));
            dt.Columns.Add("ReadingRoom", typeof(string));
            dt.Columns.Add("Seat", typeof(string));
            dt.Columns.Add("LogStatus", typeof(string));
            dt.Columns.Add("BlacklistStatus", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            List<SeatManage.ClassModel.ViolationRecordsLogInfo> VRlist = new List<SeatManage.ClassModel.ViolationRecordsLogInfo>();
            if (chkSearchMH.Checked == false)
            {
                VRlist = SeatManage.Bll.T_SM_ViolateDiscipline.GetViolationRecords(
                txtNum.Text.Trim(),
                ddlReadingRoom.SelectedValue,
                starttime,
                endtime,
                (SeatManage.EnumType.LogStatus)int.Parse(ddllogstatus.SelectedValue),
                (SeatManage.EnumType.LogStatus)int.Parse(ddlblacklist.SelectedValue),
                (SeatManage.EnumType.ViolationRecordsType)int.Parse(ddlVrType.SelectedValue));
            }
            else
            {
                VRlist = SeatManage.Bll.T_SM_ViolateDiscipline.GetViolationRecords_ByFuzzySearch(
                    txtNum.Text.Trim(),
                    ddlReadingRoom.SelectedValue,
                    starttime,
                    endtime,
                    (SeatManage.EnumType.LogStatus)int.Parse(ddllogstatus.SelectedValue),
                    (SeatManage.EnumType.LogStatus)int.Parse(ddlblacklist.SelectedValue),
                    (SeatManage.EnumType.ViolationRecordsType)int.Parse(ddlVrType.SelectedValue));
            }

            foreach (SeatManage.ClassModel.ViolationRecordsLogInfo vrinfo in VRlist)
            {

                DataRow dr = dt.NewRow();
                dr["ID"] = vrinfo.ID;
                dr["CardNo"] = vrinfo.CardNo;
                dr["ReaderName"] = vrinfo.ReaderName;
                dr["AddTime"] = vrinfo.EnterOutTime;
                dr["ReadingRoom"] = vrinfo.ReadingRoomName;
                dr["Seat"] = vrinfo.SeatID;
                if (vrinfo.Flag == LogStatus.Valid)
                {
                    dr["LogStatus"] = "有效记录";
                }
                else
                {
                    dr["LogStatus"] = "失效记录";
                }
                if (vrinfo.BlacklistID != "-1")
                {
                    dr["BlacklistStatus"] = "已加入黑名单";
                }
                else
                {
                    dr["BlacklistStatus"] = "未处理";
                }
                dr["Remark"] = vrinfo.Remark;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VRGrid_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            VRGrid.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VRGrid_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            VRGrid.SortDirection = e.SortDirection;
            VRGrid.SortColumnIndex = e.ColumnIndex;
            BindGrid();
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VRGrid_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "ActionDelete")
            {
                SeatManage.ClassModel.ViolationRecordsLogInfo vrinfo = SeatManage.Bll.T_SM_ViolateDiscipline.GetViolationRecords(VRGrid.Rows[e.RowIndex].DataKeys[0].ToString());
                if (vrinfo != null)
                {
                    vrinfo.Flag = LogStatus.Fail;
                    if (!SeatManage.Bll.T_SM_ViolateDiscipline.UpdateViolationRecords(vrinfo))
                    {
                        FineUI.Alert.ShowInTop("删除失败！");
                    }
                    //SeatManage.ClassModel.ReaderNoticeInfo rni = new SeatManage.ClassModel.ReaderNoticeInfo();
                    //rni.CardNo = vrinfo.CardNo;
                    //rni.AddTime = SeatManage.Bll.ServiceDateTime.Now;
                    //rni.Note = string.Format("{0}记录的违规，{1}，过期", vrinfo.EnterOutTime, vrinfo.Remark);
                    //SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(rni);
                }
                else
                {
                    FineUI.Alert.ShowInTop("删除失败！");
                }
                BindGrid();
            }
        }
        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 按条件删除违规记录窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowDelBySearch_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindGrid();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectDelete_Click(object sender, EventArgs e)
        {
            DateTime nowDT = SeatManage.Bll.ServiceDateTime.Now;
            int[] selectindex = VRGrid.SelectedRowIndexArray;
            if (selectindex.Length > 0)
            {
                foreach (int index in selectindex)
                {
                    SeatManage.ClassModel.ViolationRecordsLogInfo vrinfo = SeatManage.Bll.T_SM_ViolateDiscipline.GetViolationRecords(VRGrid.Rows[index].DataKeys[0].ToString());
                    if (vrinfo != null)
                    {
                        vrinfo.Flag = LogStatus.Fail;
                        if (!SeatManage.Bll.T_SM_ViolateDiscipline.UpdateViolationRecords(vrinfo))
                        {
                            FineUI.Alert.ShowInTop("删除失败！");
                            return;
                        }
                    //    SeatManage.ClassModel.ReaderNoticeInfo rni = new SeatManage.ClassModel.ReaderNoticeInfo();
                    //    rni.CardNo = vrinfo.CardNo;
                    //    rni.AddTime = nowDT;
                    //    rni.Note = string.Format("{0}记录的违规，{1}，过期", vrinfo.EnterOutTime, vrinfo.Remark);
                    //    SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(rni);
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("删除失败！");
                    }
                }
                FineUI.Alert.ShowInTop("删除完成！");
                BindGrid();
            }
            else
            {
                FineUI.Alert.ShowInTop("请先选中需要删除的记录！");
            }
        }
        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VRGrid_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            LinkButtonField lbf = VRGrid.FindColumn("VRdelete") as LinkButtonField;
            DataRowView row = e.DataItem as DataRowView;
            string status = row[6].ToString();
            if (status == "失效记录")
            {
                lbf.Enabled = false;
                lbf.Icon = FineUI.Icon.None;
                lbf.ToolTip = "此记录已失效";
            }
            else
            {
                lbf.Enabled = true;
                lbf.Icon = FineUI.Icon.Delete;
                lbf.ToolTip = "删除此条记录";
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}