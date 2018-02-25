using System;
using System.Collections.Generic;
using System.Text;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;

namespace WeiXinPocketBookOnline.ReadingRoomInfos
{
    public partial class ReadingRoomState : BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        public string roomUsedMessage = "";
        readonly WeiXinService.IWeiXinService weiXinService = new WeiXinService.WeiXinServiceHepler();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            BindDataList();

            string cmd = Request.Form["subCmd"];
            if (IsPostBack)
            {
                switch (cmd)
                {
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomList"></param>
        private void BindDataList()
        {
            StringBuilder strRoomMessage = new StringBuilder();

            strRoomMessage.Append("<ul data-role='listview' data-divider-theme='d' data-inset='true'><li data-role='list-divider' role='heading'>阅览室座位使用情况</li>");
            List<AJM_ReadingRoomState> dic = weiXinService.GetAllRoomNowState(UserSchoolInfo.SchoolNo);
            foreach (AJM_ReadingRoomState seatUsedState in dic)
            {
                //此方法暂时注释
                strRoomMessage.Append(string.Format("<li data-theme='c'>{0}:剩余{1}个座位  <ul>" +
                    "<li>总座位：{2}</li>" +
                    "<li>在座：{3}</li>" +
                    "<li>预约：{4}</li>" +
                    "<li>空闲：{5}</li>" +
                    "<li>" +
                    "<div data-role='button' style='width:90px; height=50px' data-mini='true' class='ui-btn-left' onclick='javascript:history.go(-1);'>返回 </div></li></ul></li>",
                    seatUsedState.RoomName, seatUsedState.SeatAmount_Last, seatUsedState.SeatAmount_All, seatUsedState.SeatAmount_Used, seatUsedState.SeatAmount_Bespeak, seatUsedState.SeatAmount_Last));
            }

            strRoomMessage.Append("</ul>");
            roomUsedMessage = strRoomMessage.ToString();
        }
    }
}