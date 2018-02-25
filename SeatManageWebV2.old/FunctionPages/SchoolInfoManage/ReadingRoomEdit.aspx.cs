using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SchoolInfoManage
{
    public partial class ReadingRoomEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "ReadingRoomInfo.aspx" && pageName != "FormSYS.aspx")
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
            if (!IsPostBack)
            {
                BindLibaray();

            }
            if (Request.QueryString["flag"] == "edit")
            {
                if (!IsPostBack)
                {
                    EditReadRoomShow();
                }
            }
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

        protected void ddlLibrary_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindArea();
        }
        /// <summary>
        /// 绑定区域列表
        /// </summary>
        protected void BindArea()
        {
            List<SeatManage.ClassModel.LibraryInfo> listLibrary = new List<SeatManage.ClassModel.LibraryInfo>();
            listLibrary = SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null,ddlLibrary.SelectedValue, null);
            if (listLibrary.Count > 0)
            {
                ddlArea.DataTextField = "AreaName";
                ddlArea.DataValueField = "AreaNo";
                ddlArea.DataSource = listLibrary[0].AreaList;
                ddlArea.DataBind();
            }
        }
        /// <summary>
        /// 显示阅览室信息
        /// </summary>
        protected void EditReadRoomShow()
        {
            string no = Request.QueryString["rrid"];
            SeatManage.ClassModel.ReadingRoomInfo modelReadRoom = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(no);
            if (modelReadRoom != null)
            {
                txtReadRoomName.Text = modelReadRoom.Name;
                txtReadRoomNo.Text = modelReadRoom.No;
                txtReadRoomNo.Readonly = true;
                txtReadRoomNo.Enabled = false;
                ddlLibrary.SelectedValue = modelReadRoom.Libaray.No;
                BindArea();
                ddlArea.SelectedValue = modelReadRoom.Area.AreaNo.ToString();
            }
        }

        //提交按钮点击事件
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string flag = Request.QueryString["flag"];
            string no = Request.QueryString["rrid"];
            SeatManage.ClassModel.ReadingRoomInfo modelReadingRoom = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(no);
            if (modelReadingRoom == null)
            {
                modelReadingRoom = new SeatManage.ClassModel.ReadingRoomInfo();
                modelReadingRoom.SeatList = new SeatManage.ClassModel.SeatLayout();
                modelReadingRoom.Setting = new SeatManage.ClassModel.ReadingRoomSetting();
            }
            string readRoomNo = txtReadRoomNo.Text;
            string readRoomName = txtReadRoomName.Text;
            string libraryNo = ddlLibrary.SelectedValue;
            string AreaName = ddlArea.SelectedText;

            modelReadingRoom.No = readRoomNo;
            modelReadingRoom.Name = readRoomName;
            modelReadingRoom.Libaray.No = libraryNo;
            modelReadingRoom.Area.AreaName = AreaName;
            {
                switch (flag)
                {
                    case "add":
                        if (SeatManage.Bll.T_SM_ReadingRoom.ReadingRoomIsExists(readRoomNo))
                        {
                            FineUI.Alert.ShowInTop("阅览室编号重复！");
                            return;
                        }
                        if (SeatManage.Bll.T_SM_ReadingRoom.AddNewReadingRoom(modelReadingRoom))
                        {
                            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                            FineUI.Alert.ShowInTop("添加成功！");
                        }
                        else
                        {
                            FineUI.Alert.ShowInTop("添加失败！");
                        }
                        break;
                    case "edit":
                        if (SeatManage.Bll.T_SM_ReadingRoom.UpdateReadingRoom(modelReadingRoom))
                        {
                            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                            FineUI.Alert.ShowInTop("修改成功！");
                        }
                        else
                        {
                            FineUI.Alert.ShowInTop("修改失败！");
                        }
                        break;
                    default:
                        FineUI.Alert.ShowInTop("未执行任何操作");
                        break;
                }
            }
        }
        ////重置按钮点击事件
        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["flag"] == "edit")
        //    {
        //        EditReadRoomShow();
        //    }
        //    else
        //    {
        //        txtReadRoomName.Text = "";
        //        txtReadRoomNo.Text = "";
        //        ddlLibrary.SelectedIndex = 1;
        //    }
        //}
    }
}