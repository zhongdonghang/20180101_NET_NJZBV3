using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.LogManage
{
    public partial class BlackListDelete : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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
                    }
                }
                else
                {
                    WriteLogs(HttpContext.Current.Request.Url.AbsoluteUri);
                    Response.Write("请通过正确方式访问网站！");
                    Response.End();
                    return;
                }
                dpEndDate.SelectedDate = DateTime.Now.Date;
                dpStartDate.SelectedDate = DateTime.Now.AddDays(-30).Date;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string starttime = dpStartDate.Text + " 0:00:00";
            string endtime = dpEndDate.Text + " 23:59:59";
            List<SeatManage.ClassModel.BlackListInfo> Blistlistlist = SeatManage.Bll.T_SM_Blacklist.GetAllBlackListInfo(null, SeatManage.EnumType.LogStatus.Valid, starttime, endtime);
            foreach (SeatManage.ClassModel.BlackListInfo blinfo in Blistlistlist)
            {
                blinfo.BlacklistState = SeatManage.EnumType.LogStatus.Fail;
                if (SeatManage.Bll.T_SM_Blacklist.UpdateBlackList(blinfo) == 0)
                {
                    FineUI.Alert.ShowInTop("删除失败！");
                    return;
                }
                else
                {
                    SeatManage.ClassModel.ReaderNoticeInfo rni = new SeatManage.ClassModel.ReaderNoticeInfo();
                    rni.CardNo = blinfo.CardNo;
                    rni.Note = "被管理员手动移除黑名单";
                    if (SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(rni) == 0)
                    {
                        FineUI.Alert.ShowInTop("添加消息失败！");
                        return;
                    }
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            FineUI.Alert.ShowInTop("删除完成！");
        }
    }
}