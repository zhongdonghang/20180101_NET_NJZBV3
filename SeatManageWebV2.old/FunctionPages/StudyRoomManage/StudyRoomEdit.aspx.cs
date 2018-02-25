using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.IO;

namespace SeatManageWebV2.FunctionPages.StudyRoomManage
{
    public partial class StudyRoomEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "StudyRoomManage.aspx" && pageName != "FormSYS.aspx")
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
            if (Request.QueryString["flag"] == "edit")
            {
                if (!IsPostBack)
                {
                    EditReadRoomShow();
                }
            }
        }


        /// <summary>
        /// 显示阅览室信息
        /// </summary>
        protected void EditReadRoomShow()
        {
            string no = Request.QueryString["srid"];
            SeatManage.ClassModel.StudyRoomInfo modelStudyRoom = SeatManage.Bll.StudyRoomOperation.GetSingleStudyRoonInfo(no);
            if (modelStudyRoom != null)
            {
                txtStudyRoomName.Text = modelStudyRoom.StudyRoomName;
                txtStudyRoomNo.Text = modelStudyRoom.StudyRoomNo;
                txtRemark.Text = modelStudyRoom.Remark;
                txtStudyRoomNo.Readonly = true;
                txtStudyRoomNo.Enabled = false;
                if (!string.IsNullOrEmpty(modelStudyRoom.RoomImage))
                {
                    imgRoomImage.ImageUrl = "~/StudyImage/" + modelStudyRoom.RoomImage;
                }
            }
        }

        protected void uploadImage_OnFileSelected(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadImage.PostedFile.FileName))
            {
                string imgPath = uploadImage.PostedFile.FileName;
                if (imgPath.Substring(imgPath.LastIndexOf(".") + 1).ToUpper() == "JPG" || imgPath.Substring(imgPath.LastIndexOf(".") + 1).ToUpper() == "PNG" || imgPath.Substring(imgPath.LastIndexOf(".") + 1).ToUpper() == "BMP")
                {
                    string path = Server.MapPath("/");
                    uploadImage.PostedFile.SaveAs(path + "/StudyImage/" + imgPath);
                    imgRoomImage.ImageUrl = "~/StudyImage/" + imgPath;

                }
            }
        }

        //提交按钮点击事件
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string flag = Request.QueryString["flag"];
            string no = Request.QueryString["srid"];
            SeatManage.ClassModel.StudyRoomInfo modelStudyRoom = SeatManage.Bll.StudyRoomOperation.GetSingleStudyRoonInfo(no);
            if (modelStudyRoom == null)
            {
                modelStudyRoom = new SeatManage.ClassModel.StudyRoomInfo();
                modelStudyRoom.Setting = new SeatManage.ClassModel.StudyRoomSetting();
            }
            string RoomNo = txtStudyRoomNo.Text;
            string RoomName = txtStudyRoomName.Text;
            string RoomRemark = txtRemark.Text;
            modelStudyRoom.StudyRoomNo = RoomNo;
            modelStudyRoom.StudyRoomName = RoomName;
            modelStudyRoom.Remark = RoomRemark;
            if (!string.IsNullOrEmpty(uploadImage.PostedFile.FileName))
            {
                modelStudyRoom.RoomImage = uploadImage.PostedFile.FileName;
            }
            {
                switch (flag)
                {
                    case "add":
                        if (SeatManage.Bll.StudyRoomOperation.GetSingleStudyRoonInfo(RoomNo) != null)
                        {
                            FineUI.Alert.ShowInTop("研习间编号重复！");
                            return;
                        }
                        if (SeatManage.Bll.StudyRoomOperation.AddNewStudyRoom(modelStudyRoom))
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
                        if (SeatManage.Bll.StudyRoomOperation.UpdateStudyRoom(modelStudyRoom))
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
    }
}