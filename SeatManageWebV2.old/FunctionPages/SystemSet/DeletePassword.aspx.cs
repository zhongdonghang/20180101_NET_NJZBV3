using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SystemSet
{
    public partial class DeletePassword : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "ReadingRoomInfo.aspx" && pageName != "FormSYS.aspx" && pageName != "SchoolInfo.aspx" && pageName != "LibraryInfo.aspx")
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
            }
        }
        /// <summary>
        /// 提交设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Type"] != null && Request.QueryString["id"] != null)
            {
                if (!string.IsNullOrEmpty(txtpw1.Text) && !string.IsNullOrEmpty(txtpw2.Text))
                {
                    //密码是Juneberry_NJZBWX
                    if (txtpw1.Text == txtpw2.Text && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(txtpw2.Text) == "88C5884397D51468FA04ACFA46483AE4")
                    {
                        FineUI.Alert.Show("验证成功！");
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHidePostBackReference());
                        switch (Request.QueryString["Type"])
                        {
                            case "School":
                                SeatManage.ClassModel.School school = new SeatManage.ClassModel.School();
                                school.No = Request.QueryString["id"];
                                if (!SeatManage.Bll.T_SM_School.DeleteSchool(school))
                                {
                                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                                    FineUI.Alert.ShowInTop("删除失败！");
                                }
                                else
                                {
                                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                                    FineUI.Alert.ShowInTop("删除完成！");
                                } break;
                            case "Library":
                                SeatManage.ClassModel.LibraryInfo library = new SeatManage.ClassModel.LibraryInfo();
                                library.No = Request.QueryString["id"];
                                if (!SeatManage.Bll.T_SM_Library.DeleteLibrary(library))
                                {
                                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                                    FineUI.Alert.ShowInTop("删除失败！");
                                }
                                else
                                {
                                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                                    FineUI.Alert.ShowInTop("删除完成！");
                                }
                                break;
                            case "ReadingRoom":
                                SeatManage.ClassModel.ReadingRoomInfo room = new SeatManage.ClassModel.ReadingRoomInfo();
                                room.No = Request.QueryString["id"];
                                if (!SeatManage.Bll.T_SM_ReadingRoom.DeleteReadingRoom(room))
                                {
                                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                                    FineUI.Alert.ShowInTop("删除失败！");
                                }
                                else
                                {
                                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                                    FineUI.Alert.ShowInTop("删除完成！");
                                }
                                break;
                        }
                    }
                    else
                    {
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                        FineUI.Alert.ShowInTop("密码错误！");
                    }
                }
                else
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("密码不能为空！");
                    return;
                }
            }
            else
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("加载页面失败，请重新打开");
            }
        }
    }
}