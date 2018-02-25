using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FineUI;
using SeatManage.EnumType;

namespace SeatManageWebV2.FunctionPages.LogManage
{
    public partial class Blacklist : BasePage
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
                dpStartDate.SelectedDate = DateTime.Now.AddDays(-7);
                dpEndDate.SelectedDate = DateTime.Now;
                btnDeleteBlackList.OnClientClick = WindowBLdelete.GetShowReference("BlackListDelete.aspx", "批量删除黑名单");
                btnAddBl.OnClientClick = WindowBLAdd.GetShowReference("AddBlacklistInfo.aspx", "添加黑名单");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string sortField = BlacklistGrid.Columns[BlacklistGrid.SortColumnIndex].SortField;
            string sortDirection = BlacklistGrid.SortDirection;
            string starttime = "";
            if (!string.IsNullOrEmpty(dpStartDate.Text))
            {
                starttime = dpStartDate.Text + " 0:00:00";
            }
            string endtime = "";
            if (!string.IsNullOrEmpty(dpEndDate.Text))
            {
                endtime = dpEndDate.Text + " 23:59:59";
            }
            if (!string.IsNullOrEmpty(dpStartDate.Text) && !string.IsNullOrEmpty(dpEndDate.Text) && DateTime.Parse(starttime) >= DateTime.Parse(endtime))
            {
                FineUI.Alert.Show("结束日期必须大于等于开始日期");
                return;
            }
            DataTable table = GetUserInfoDateTable(starttime, endtime);
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            BlacklistGrid.DataSource = TableView;
            BlacklistGrid.DataBind();
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
            dt.Columns.Add("LeaveTime", typeof(DateTime));
            dt.Columns.Add("LeaveMode", typeof(string));
            dt.Columns.Add("LogStatus", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            List<SeatManage.ClassModel.BlackListInfo> Blistlistlist = new List<SeatManage.ClassModel.BlackListInfo>();
            if (chkSearchMH.Checked == false)
            {
                Blistlistlist = SeatManage.Bll.T_SM_Blacklist.GetAllBlackListInfo(
                    txtNum.Text.Trim(),
                    (SeatManage.EnumType.LogStatus)int.Parse(ddllogstatus.SelectedValue),
                    starttime,
                    endtime);
            }
            else
            {
                Blistlistlist = SeatManage.Bll.T_SM_Blacklist.GetAllBlackListInfo_ByFuzzySearch(
                    txtNum.Text.Trim(),
                    (SeatManage.EnumType.LogStatus)int.Parse(ddllogstatus.SelectedValue),
                    starttime,
                    endtime);
            }
            foreach (SeatManage.ClassModel.BlackListInfo bllist in Blistlistlist)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = bllist.ID;
                dr["CardNo"] = bllist.CardNo;
                dr["ReaderName"] = bllist.ReaderName;
                dr["AddTime"] = bllist.AddTime;
                dr["LeaveTime"] = bllist.OutTime;
                if (bllist.OutBlacklistMode == SeatManage.EnumType.LeaveBlacklistMode.AutomaticMode)
                {
                    dr["LeaveMode"] = "自动离开";
                }
                else
                {
                    dr["LeaveMode"] = "手动释放";
                }
                if (bllist.BlacklistState == LogStatus.Valid)
                {
                    dr["LogStatus"] = "处罚中";
                }
                else
                {
                    dr["LogStatus"] = "已过期";
                }
                dr["Remark"] = bllist.ReMark;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BlacklistGrid_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            BlacklistGrid.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BlacklistGrid_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            BlacklistGrid.SortDirection = e.SortDirection;
            BlacklistGrid.SortColumnIndex = e.ColumnIndex;
            BindGrid();
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BlacklistGrid_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "ActionDelete")
            {
                SeatManage.ClassModel.BlackListInfo blacklist = SeatManage.Bll.T_SM_Blacklist.GetBlistList(BlacklistGrid.Rows[e.RowIndex].DataKeys[0].ToString());
                if (blacklist != null)
                {
                    blacklist.BlacklistState = LogStatus.Fail;
                    if (SeatManage.Bll.T_SM_Blacklist.UpdateBlackList(blacklist) == 0)
                    {
                        FineUI.Alert.ShowInTop("删除失败！");
                    }
                    else
                    {
                        //SeatManage.ClassModel.ReaderNoticeInfo rni = new SeatManage.ClassModel.ReaderNoticeInfo();
                        //rni.CardNo = blacklist.CardNo;
                        //rni.Type = NoticeType.DeleteBlacklistWarning;
                        //rni.Note = "被管理员手动移除黑名单";
                        //if (SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(rni) > 0)
                        //{
                        FineUI.Alert.ShowInTop("删除完成！");
                        //}
                        //else
                        //{
                        //    FineUI.Alert.ShowInTop("添加消息失败！");
                        //}
                    }
                }
                else
                {
                    FineUI.Alert.ShowInTop("删除失败！");
                }
                BindGrid();
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectDelete_Click(object sender, EventArgs e)
        {
            int[] selectindex = BlacklistGrid.SelectedRowIndexArray;
            if (selectindex.Length > 0)
            {
                foreach (int index in selectindex)
                {
                    SeatManage.ClassModel.BlackListInfo blacklist = SeatManage.Bll.T_SM_Blacklist.GetBlistList(BlacklistGrid.Rows[index].DataKeys[0].ToString());
                    if (blacklist != null)
                    {
                        blacklist.BlacklistState = LogStatus.Fail;
                        if (SeatManage.Bll.T_SM_Blacklist.UpdateBlackList(blacklist) == 0)
                        {
                            FineUI.Alert.ShowInTop("删除失败！");
                            return;
                        }
                        //else
                        //{
                        //    SeatManage.ClassModel.ReaderNoticeInfo rni = new SeatManage.ClassModel.ReaderNoticeInfo();
                        //    rni.CardNo = blacklist.CardNo;
                        //    rni.Type = NoticeType.DeleteBlacklistWarning;
                        //    rni.Note = "被管理员手动移除黑名单";
                        //    if (SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(rni) == 0)
                        //    {
                        //        FineUI.Alert.ShowInTop("添加消息失败！");
                        //        return;
                        //    }
                        //}
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
        protected void BlacklistGrid_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            LinkButtonField lbf = BlacklistGrid.FindColumn("Blacklistdelete") as LinkButtonField;
            LinkButtonField lbfshow = BlacklistGrid.FindColumn("BlacklistInfo") as LinkButtonField;
            DataRowView row = e.DataItem as DataRowView;
            string status = row[6].ToString();
            lbfshow.OnClientClick = WindowEdit.GetShowReference("BlacklistInfo.aspx?id=" + row[0].ToString() + "", "黑名单详情");
            if (status == "已过期")
            {
                lbf.Enabled = false;
                lbf.Icon = FineUI.Icon.None;
                lbf.ToolTip = "此记录已过期";
            }
            else
            {
                lbf.Enabled = true;
                lbf.Icon = FineUI.Icon.Delete;
                lbf.ToolTip = "读者移出黑名单";
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
        protected void Window_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}