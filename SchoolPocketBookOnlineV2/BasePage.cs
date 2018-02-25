using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SchoolPocketBookOnlineV2
{
    public class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
        public SeatManage.ClassModel.ReaderInfo LoginUserInfo
        {
            get
            {
                Session["userInfo"] = (SeatManage.ClassModel.ReaderInfo)Session["userInfo"];
                return (SeatManage.ClassModel.ReaderInfo)Session["userInfo"];
            }
            set
            {
                Session["userInfo"] = value;
            }
        }
        /// <summary>
        /// 当前用户所在的学校
        /// </summary>
        public AMS.Model.AMS_School UserSchoolInfo
        {
            get
            {
                Session["schoolInfo"] = (AMS.Model.AMS_School)Session["schoolInfo"];
                return (AMS.Model.AMS_School)Session["schoolInfo"];
            }
            set
            {
                Session["schoolInfo"] = value;
            }
        }
        /// <summary>
        /// 存放当前提供预约的阅览室列表
        /// </summary>
        public Dictionary<string, SeatManage.ClassModel.ReadingRoomInfo> ReadingRoomList
        {
            get
            {
                Session["roomList"] = (Dictionary<string, SeatManage.ClassModel.ReadingRoomInfo>)Session["roomList"];
                return (Dictionary<string, SeatManage.ClassModel.ReadingRoomInfo>)Session["roomList"];
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