using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using FineUI;

namespace SeatManageWebV2.FunctionPages.LogManage
{
    public partial class DelViolateDisciplineBySearch : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "ViolateDiscipline.aspx" && pageName != "FormSYS.aspx")
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
                BindDDLReadingRoom();
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

        protected void btnDel_Click(object sender, EventArgs e)
        {
            int count = SeatManage.Bll.T_SM_ViolateDiscipline.DelBySearch(dpStartDate.Text, dpEndDate.Text, ddlReadingRoom.SelectedValue);
            if (count != -1)
            {
                StringBuilder warn = new StringBuilder();
                if (count == 0)
                {
                    warn.Append("查询时间内没有有效违规记录");
                }
                else
                {
                    warn.Append("删除成功，共删除" + count + "条违规记录");
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop(warn.ToString());
            }
            else
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("删除失败");
            }

        }
    }
}