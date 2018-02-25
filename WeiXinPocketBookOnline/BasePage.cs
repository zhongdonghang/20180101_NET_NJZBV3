using System.Collections.Generic;
using System.Configuration;
using SeatManage.AppJsonModel;

namespace WeiXinPocketBookOnline
{
    public class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
        public AJM_WeiXinUserInfo LoginUserInfo
        {
            get
            {
                Session["userInfo"] = (AJM_WeiXinUserInfo)Session["userInfo"];
                return (AJM_WeiXinUserInfo)Session["userInfo"];
            }
            set
            {
                Session["userInfo"] = value;
            }
        }
        /// <summary>
        /// 当前用户所在的学校
        /// </summary>
        public AJM_School UserSchoolInfo
        {
            get
            {
                Session["schoolInfo"] = (AJM_School)Session["schoolInfo"];
                return (AJM_School)Session["schoolInfo"];
            }
            set
            {
                Session["schoolInfo"] = value;
            }
        }
        /// <summary>
        /// 存放当前提供预约的阅览室列表
        /// </summary>
        public Dictionary<string, AJM_ReadingRoom> ReadingRoomList
        {
            get
            {
                Session["roomList"] = (Dictionary<string, AJM_ReadingRoom>)Session["roomList"];
                return (Dictionary<string, AJM_ReadingRoom>)Session["roomList"];
            }
            set
            {
                Session["roomList"] = value;
            }
        }

        /// <summary>
        /// 存放当前提供预约的阅览室列表
        /// </summary>
        public AJM_SeatBespeakInfo BespeakSeatInfo
        {
            get
            {
                Session["BespeakSeat"] = (AJM_SeatBespeakInfo)Session["BespeakSeat"];
                return (AJM_SeatBespeakInfo)Session["BespeakSeat"];
            }
            set
            {
                Session["BespeakSeat"] = value;
            }
        }


        public string LoginUrl()
        {
            if (ConfigurationManager.AppSettings["redirectLoginPage"] != null)
            {
                return ConfigurationManager.AppSettings["redirectLoginPage"];
            }
            else
            {
                return "../Login.aspx";
            }
        }
        public string LogoutUrl()
        {
            if (ConfigurationManager.AppSettings["LogOutUrl"] != null)
            {
                return ConfigurationManager.AppSettings["LogOutUrl"];
            }
            else
            {
                return "../Login.aspx";
            }
        }
    }
}