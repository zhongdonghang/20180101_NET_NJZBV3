using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using SeatManage.ClassModel;

namespace SchoolPocketBookWeb
{
    public class BasePage : Page
    {
        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
        public ReaderInfo LoginUserInfo
        {
            get
            {
                Session["userInfo"] = (ReaderInfo)Session["userInfo"];
                return (ReaderInfo)Session["userInfo"];
            }
            set
            {
                Session["userInfo"] = value;
            }
        }
        ///// <summary>
        ///// 当前用户所在的学校
        ///// </summary>
        //public AMS_School UserSchoolInfo
        //{
        //    get
        //    {
        //        Session["schoolInfo"] = (AMS_School)Session["schoolInfo"];
        //        return (AMS_School)Session["schoolInfo"];
        //    }
        //    set
        //    {
        //        Session["schoolInfo"] = value;
        //    }
        //}
        /// <summary>
        /// 存放当前提供预约的阅览室列表
        /// </summary>
        public Dictionary<string, ReadingRoomInfo> ReadingRoomList
        {
            get
            {
                Session["roomList"] = (Dictionary<string, ReadingRoomInfo>)Session["roomList"];
                return (Dictionary<string, ReadingRoomInfo>)Session["roomList"];
            }
            set
            {
                Session["roomList"] = value;
            }
        }

        public string LoginUrl()
        {
            if (ConfigurationManager.AppSettings["redirectLoginPage"] != null)
            {
                return ConfigurationManager.AppSettings["redirectLoginPage"];
            }
            return "../Login.aspx";
        }

        public string LogoutUrl()
        {
            if (ConfigurationManager.AppSettings["LogOutUrl"] != null)
            {
                return ConfigurationManager.AppSettings["LogOutUrl"];
            }
            return "../Login.aspx";
        }
    }
}