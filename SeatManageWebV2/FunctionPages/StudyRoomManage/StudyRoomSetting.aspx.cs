using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2.FunctionPages.StudyRoomManage
{
    public partial class StudyRoomSetting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["no"]))
                {
                    DataBind();
                }
                else
                {
                    FineUI.PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("设置获取错误！");
                }
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBind()
        {
            SeatManage.ClassModel.StudyRoomInfo room = SeatManage.Bll.StudyRoomOperation.GetSingleStudyRoonInfo(Request.QueryString["no"]);
            if (room == null)
            {
                room = new SeatManage.ClassModel.StudyRoomInfo();
            }
            SeatManage.ClassModel.StudyRoomSetting roomSet = room.Setting;
            if (roomSet == null)
            {
                roomSet = new SeatManage.ClassModel.StudyRoomSetting();
            }
            chkUseStudyRoom.Checked = roomSet.IsUseStudyRoom;
            txtApplicationInfo.Text = roomSet.ApplicationInfo;
            txtFacilities.Text = roomSet.FacilitiesRenmark;
            txtPrecautions.Text = roomSet.Precautions;
            txtOpenTime_H.Text = roomSet.OpenTime.ToShortTimeString().Split(':')[0];
            txtOpenTime_M.Text = roomSet.OpenTime.ToShortTimeString().Split(':')[1];
            txtEndTime_H.Text = roomSet.CloseTime.ToShortTimeString().Split(':')[0];
            txtEndTime_M.Text = roomSet.CloseTime.ToShortTimeString().Split(':')[1];
            txtMaxTime.Text = roomSet.MaxBookTime.ToString();
            txtCanUse.Text = roomSet.CanUseFacilities;
        }

        protected void Submit_OnClick(object sender, EventArgs e)
        {
            SeatManage.ClassModel.StudyRoomInfo room = SeatManage.Bll.StudyRoomOperation.GetSingleStudyRoonInfo(Request.QueryString["no"]);
            if (room == null)
            {
                FineUI.Alert.Show("设置保存失败！");
                return;
            }
            SeatManage.ClassModel.StudyRoomSetting roomSet = room.Setting;
            if (roomSet == null)
            {
                roomSet = new SeatManage.ClassModel.StudyRoomSetting();
            }
            roomSet.IsUseStudyRoom = chkUseStudyRoom.Checked;
            roomSet.ApplicationInfo = txtApplicationInfo.Text;
            roomSet.FacilitiesRenmark = txtFacilities.Text;
            roomSet.Precautions = txtPrecautions.Text;
            roomSet.CanUseFacilities = txtCanUse.Text;
            int maxTime = 0;
            if (int.TryParse(txtMaxTime.Text, out maxTime))
            {
                if (maxTime > 0)
                {
                    roomSet.MaxBookTime = maxTime;
                }
                else
                {
                    FineUI.Alert.Show("最大使用时长要大于0！");
                    return;
                }
            }
            else
            {
                FineUI.Alert.Show("最大使用时长格式错误！");
                return;
            }

            DateTime oTime = new DateTime();
            if (DateTime.TryParse(txtOpenTime_H.Text + ":" + txtOpenTime_M.Text, out oTime))
            {
                roomSet.OpenTime = oTime;
            }
            else
            {
                FineUI.Alert.Show("开放时间格式错误！");
                return;
            }

            DateTime eTime = new DateTime();
            if (DateTime.TryParse(txtEndTime_H.Text + ":" + txtEndTime_M.Text, out eTime))
            {
                roomSet.CloseTime = eTime;
            }
            else
            {
                FineUI.Alert.Show("关闭时间格式错误！");
                return;
            }
            if (roomSet.CloseTime <= roomSet.OpenTime)
            {
                FineUI.Alert.Show("开放时间不能大于关闭时间！");
                return;
            }
            try
            {
                room.Setting = roomSet;
                if (SeatManage.Bll.StudyRoomOperation.UpdateStudyRoom(room))
                {
                    FineUI.PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("保存成功！");
                }
                else
                {
                    FineUI.Alert.Show("保存失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                FineUI.Alert.Show("保存失败！");
                return;
            }
        }

    }
}