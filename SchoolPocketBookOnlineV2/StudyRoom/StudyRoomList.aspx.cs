using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

namespace SchoolPocketBookOnlineV2.StudyRoom
{
    public partial class StudyRoomList : BasePage
    {
        public string roomUsedMessage = "";
        SeatManage.IPocketBespeak.IBookStudyRoom handler = new SeatManage.PocketBespeak.PecketBespeak_BookStudyRoom();
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
            string imageWebUrl = ConfigurationManager.AppSettings["imageUrl"];
            strRoomMessage.Append("<ul data-role='listview' data-divider-theme='d' data-inset='true'><li data-role='list-divider' role='heading'>研习间列表</li>");
            List<SeatManage.ClassModel.StudyRoomInfo> rooms = handler.GetStudyRoomList(this.UserSchoolInfo);// bll.GetReadingRoomSeatUsingState(roomNums, Session["SchoolConnectionString"].ToString());
            foreach (SeatManage.ClassModel.StudyRoomInfo room in rooms)
            {
                StringBuilder str=new StringBuilder();
                str.Append("<li data-theme='c'>{0} {1}");
                str.Append("<ul><li>编号：{2}</li>");
                str.Append("<li>名称：{3}</li>");
                str.Append("<li><div>");
                str.Append("<img src='{7}' height=\"225px\" width=\"300px\" />");
                str.Append("</div></li>");
                str.Append("<li>申请说明：<br/>{4}</li>");
                str.Append("<li>设备描述：<br/>{5}</li>");
                str.Append("<li>注意事项：<br/>{6}</li>");
                str.Append("<li>");
                str.Append("<div style=\"height:35px; text-align: center\">");
                str.Append("<div class=\"ui-block-a\" style=\"width: 50%; text-align: center\">");
                str.Append("<input data-inline=\"true\" value=\"返回\" data-mini=\"true\" type=\"button\" onclick=\"javascript:history.go(-1);\" />");
                str.Append("</div>");
                str.Append("<div class=\"ui-block-b\" style=\"width: 50%; text-align: center\">");
                if (room.Setting.IsUseStudyRoom)
                {
                    str.Append("<input data-inline=\"true\" value=\"申请\" data-mini=\"true\" type=\"button\" onclick=\"location.href='BookStudyRoom.aspx?No={0}&ID=-1'\" />");
                }
                else
                {
                    str.Append("<input data-inline=\"true\" value=\"申请\" data-mini=\"true\" type=\"button\" disabled=\"disabled\" onclick=\"location.href='BookStudyRoom.aspx?No={0}&ID=-1'\" />");
                }
                str.Append("</div>");
                str.Append("</div>");
                str.Append("</li>");
                str.Append("</ul></li>");
                   


                

                strRoomMessage.Append(string.Format(str.ToString(), room.StudyRoomNo, room.StudyRoomName, room.StudyRoomNo, room.StudyRoomName, room.Setting.ApplicationInfo.Replace("\r\n", "<br/>"), room.Setting.FacilitiesRenmark.Replace("\r\n", "<br/>"), room.Setting.Precautions.Replace("\r\n", "<br/>"), imageWebUrl + "StudyImage/" + room.RoomImage));
            }
            strRoomMessage.Append("</ul>");
            roomUsedMessage = strRoomMessage.ToString();
        }
    }
}