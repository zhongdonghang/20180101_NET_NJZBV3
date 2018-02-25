using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SeatManage.SeatManageComm
{
    public class CookiesManager
    {
        #region Cookies成员变量
        /// <summary>
        /// 登录名
        /// </summary>
        public static readonly string LoginID = "LoginId";

        /// <summary>
        /// 登录密码
        /// </summary>
        public static readonly string Password = "Password";

        /// <summary>
        /// 学校数据库链接地址
        /// </summary>
        public static readonly string ConnectionString = "ConnectionString";

        /// <summary>
        /// 学校id cookies
        /// </summary>
        public static readonly string SchoolId = "SchoolId";

        #endregion

        #region Session成员变量
        /// <summary>
        /// 学号
        /// </summary>
        public static readonly string CardNo = "CardNo";
        /// <summary>
        /// 姓名
        /// </summary>
        public static readonly string Name = "Name";
        /// <summary>
        /// 学校链接字符串
        /// </summary>
        public static readonly string SchoolConnectionString = "SchoolConnectionString";
        /// <summary>
        /// 学校id session
        /// </summary>
        public static readonly string SessionSchoolId = "SessionSchoolId";
        #endregion

        #region Cookies操作方法
        /// <summary>
        /// 添加cookies
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="passWord"></param>
        /// <param name="connectionString"></param>
        public static void SetCookies(string loginId, string passWord, string schoolId)
        {
            try
            {
                HttpCookie userInfo = new HttpCookie("userInfo");
                StringCipherCls sc = new StringCipherCls();
                sc.StringCipher(ref loginId, sc.Key);
                sc.StringCipher(ref passWord, sc.Key);
                sc.StringCipher(ref schoolId, sc.Key);
                userInfo.Values[LoginID] = loginId;
                userInfo.Values[Password] = passWord;
                userInfo.Values[SchoolId] = schoolId;
                userInfo.Expires = DateTime.MaxValue;
                System.Web.HttpContext.Current.Response.Cookies.Add(userInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 手持设备添加cookies
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="passWord"></param>
        /// <param name="connectionString"></param>
        public static void SetPadCookies(string loginId, string passWord)
        {
            try
            {
                HttpCookie userInfo = new HttpCookie("userInfo");
                StringCipherCls sc = new StringCipherCls();
                sc.StringCipher(ref loginId, sc.Key);
                sc.StringCipher(ref passWord, sc.Key);
                userInfo.Values[LoginID] = loginId;
                userInfo.Values[Password] = passWord;
                userInfo.Expires = DateTime.MaxValue;
                System.Web.HttpContext.Current.Response.Cookies.Add(userInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 读取指定的cookies
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetCookiesValue(string strName)
        {
            HttpCookie userInfo = System.Web.HttpContext.Current.Request.Cookies["userInfo"];
            StringCipherCls sc = new StringCipherCls();
            if (userInfo != null)
            {
                string cookiesValue = userInfo.Values[strName].ToString();
                sc.StringDecipher(ref cookiesValue, sc.Key);
                return cookiesValue;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 删除cookies
        /// </summary>
        /// <param name="strName"></param>
        public static void RemoveCookies(string strName)
        {
            try
            {
                HttpCookie userInfo = new HttpCookie(strName);
                userInfo.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(userInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断指定的cookies是否存在
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static bool ExisCookies(string strName)
        {
            try
            {
                return !GetCookiesValue(strName).Equals(null);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 获取zhidingcookies与内容作对比
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool CompareCookies(string strName, string strValue)
        {
            try
            {
                StringCipherCls sc = new StringCipherCls();
                sc.StringCipher(ref strValue, sc.Key);
                return GetCookiesValue(strName).Equals(strValue);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Session操作方法
        #endregion

        private static int refreshNum = 0;
        /// <summary>
        /// 刷新次数
        /// </summary>
        public static int RefreshNum
        {
            get { return CookiesManager.refreshNum; }
            set { CookiesManager.refreshNum = value; }
        }


    }
}
