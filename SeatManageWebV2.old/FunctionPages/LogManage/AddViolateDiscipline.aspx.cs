using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using SeatManage.Bll;
using SeatManage.EnumType;
using FineUI;

namespace SeatManageWebV2.FunctionPages.LogManage
{
    public partial class AddViolateDiscipline : BasePage
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
            List<SeatManage.ClassModel.ReadingRoomInfo> roomlist = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, null, null);
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
            SeatManage.ClassModel.RegulationRulesSetting regulationRulesSetting = T_SM_SystemSet.GetRegulationRulesSetting();
            string CardNo = txtCardno.Text;
            string SeatNo = txtseatno.Text;
            string seatnoremark = "";
            if (!string.IsNullOrEmpty(SeatNo))
            {
                seatnoremark = SeatNo + "号座位，";
            }
            string Remark = txtRemark.Text;
            ViolationRecordsType Type = (ViolationRecordsType)int.Parse(ddltype.SelectedValue);
            string ReadingRoomNo = ddlroom.SelectedValue;
            ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(ReadingRoomNo);
            if (room.Setting.IsRecordViolate)
            {
                ViolationRecordsLogInfo vrli = new ViolationRecordsLogInfo();
                vrli.CardNo = CardNo;
                vrli.SeatID = SeatNo;
                vrli.ReadingRoomID = ReadingRoomNo;
                vrli.EnterOutTime = ServiceDateTime.Now.ToString();
                vrli.EnterFlag = Type;
                vrli.Remark = string.Format("在{0}，{1}被管理员{2}，手动记录违规，备注{3}", room.Name, seatnoremark, this.LoginId, Remark);
                if (T_SM_ViolateDiscipline.AddViolationRecords(vrli))
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
        /// <summary>
        /// 添加读者消息提示
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="Note">消息内容</param>
        private static void AddNotice(string cardNo, string Note)
        {
            ReaderNoticeInfo rni = new ReaderNoticeInfo();
            rni.CardNo = cardNo;
            rni.Note = Note;
            T_SM_ReaderNotice.AddReaderNotice(rni);
        }
    }
}