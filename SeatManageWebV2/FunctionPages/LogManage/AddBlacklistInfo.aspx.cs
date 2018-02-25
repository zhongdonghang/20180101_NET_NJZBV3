using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.LogManage
{
    public partial class AddBlacklistInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "Blacklist.aspx" && pageName != "FormSYS.aspx")
                    {
                        WriteLogs(url);
                        Response.Write("请通过正确方式访问网站！");
                        Response.End();
                        return;
                        //FineUI.Alert.ShowInTop("请通过正确方式访问网站！");
                        //btnSubmit.Enabled = false;
                        //PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    }
                }
                else
                {
                    WriteLogs(HttpContext.Current.Request.Url.AbsoluteUri);
                    Response.Write("请通过正确方式访问网站！");
                    Response.End();
                    return;
                }
                BindDDLReadingRoom();
                dpEndDate.SelectedDate = DateTime.Now.AddDays(7);
            }
        }


        /// <summary>
        /// 绑定管理的阅览室下拉列表
        /// </summary>
        private void BindDDLReadingRoom()
        {
            List<SeatManage.ClassModel.ReadingRoomInfo> roomlist = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null);
            ddlroom.DataTextField = "Name";
            ddlroom.DataValueField = "No";
            ddlroom.DataSource = roomlist;
            ddlroom.DataBind();
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.RegulationRulesSetting regulationRulesSetting = SeatManage.Bll.T_SM_SystemSet.GetRegulationRulesSetting();
            string CardNo = txtCardno.Text;
            string Remark = txtRemark.Text;
            string ReadingRoomNo = ddlroom.SelectedValue;
            SeatManage.ClassModel.ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(ReadingRoomNo);
            SeatManage.ClassModel.BlackListInfo bli = new SeatManage.ClassModel.BlackListInfo();
            bli.CardNo = CardNo;
            bli.AddTime = SeatManage.Bll.ServiceDateTime.Now;
            if (ddlleaveMode.SelectedValue == "0")
            {
                if (dpEndDate.SelectedDate.Value != null && dpEndDate.SelectedDate.Value < bli.AddTime.Date)
                {
                    FineUI.Alert.Show("请输入不小于今天的日期！");
                }
                bli.OutTime = DateTime.Parse(dpEndDate.SelectedDate.Value.ToShortDateString() + " 23:59:59");
            }
            bli.OutBlacklistMode = (SeatManage.EnumType.LeaveBlacklistMode)int.Parse(ddlleaveMode.SelectedValue);
            if (bli.OutBlacklistMode == SeatManage.EnumType.LeaveBlacklistMode.ManuallyMode)
            {
                bli.ReMark = string.Format("被管理员{0}加入手动加入黑名单，管理员手动移除黑名单，备注：{1}", this.LoginId, Remark);
            }
            else
            {
                bli.ReMark = string.Format("被管理员{0}加入手动加入黑名单，记录黑名单{1}天，备注：{2}", this.LoginId, (bli.OutTime - bli.AddTime).Days, Remark);
            }
            bli.ReadingRoomID = ReadingRoomNo;
            int blackId = 0;
            if (cbIsAllRR.Checked)
            {
                int roomCount = 0;
                List<SeatManage.ClassModel.ReadingRoomInfo> roomlist = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null);
                foreach (SeatManage.ClassModel.ReadingRoomInfo roominfo in roomlist)
                {
                    if (roominfo.Setting.BlackListSetting.Used)
                    {
                        bli.ReadingRoomID = roominfo.No;
                        if (!(SeatManage.Bll.T_SM_Blacklist.AddBlackList(bli) > 0))
                        {
                            FineUI.Alert.Show("添加失败！");
                            return;
                        }
                        else
                        {
                            roomCount++;
                        }
                    }
                }
                if (roomCount == 0)
                {
                    blackId = SeatManage.Bll.T_SM_Blacklist.AddBlackList(bli);
                } 
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.Show("添加成功！");
            }
            else
            {
                blackId = SeatManage.Bll.T_SM_Blacklist.AddBlackList(bli);
                if (blackId > 0)
                { 
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.Show("添加成功！");
                }
                else
                {
                    FineUI.Alert.Show("添加失败！");
                }
            }
        }
        ///// <summary>
        ///// 添加读者消息提示
        ///// </summary>
        ///// <param name="cardNo">卡号</param>
        ///// <param name="Note">消息内容</param>
        //private static void AddNotice(string cardNo, string Note)
        //{
        //    SeatManage.ClassModel.ReaderNoticeInfo rni = new SeatManage.ClassModel.ReaderNoticeInfo();
        //    rni.CardNo = cardNo;
        //    rni.Note = Note;
        //    SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(rni);
        //}
        protected void ddlleaveMode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlleaveMode.SelectedValue == "0")
            {
                dpEndDate.Enabled = true;
            }
            else
            {
                dpEndDate.Enabled = false;
            }
        }
        protected void cbIsAllRR_OnCheckedChanged(object sender, EventArgs e)
        {
            if (cbIsAllRR.Checked)
            {
                ddlroom.Enabled = false;
            }
            else
            {
                ddlroom.Enabled = true;
            }
        }
    }
}