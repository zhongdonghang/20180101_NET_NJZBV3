using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AMS.DataTransfer.Code
{

    /// <summary>
    /// 授权方式
    /// </summary>
    public enum EmpowerMode
    {
        None = -1,
        /// <summary>
        /// 本地
        /// </summary>
        Local = 0,
        /// <summary>
        /// 服务器授权
        /// </summary>
        Server = 1
    }
    /// <summary>
    /// 存储服务设置
    /// </summary>
    public class ServiceSet
    {
        /// <summary>
        /// 下载的文件缓存文件夹
        /// </summary>
        public static string TempFilePath
        {
            get { return string.Format(@"{0}\temp\", System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase); }
        }
        /// <summary>
        /// 学校编号
        /// </summary>
        public static string SchoolNums
        {
            get
            {
                return ConfigurationManager.AppSettings["SchoolNo"];
            }
        }
        /// <summary>
        /// 服务循环间隔时间
        /// </summary>
        public static string Interval
        {
            get
            {
                return ConfigurationManager.AppSettings["Interval"];
            }
        }
        /// <summary>
        /// 授权方式
        /// </summary>
        public static EmpowerMode Empower
        {
            get
            {
                if (ConfigurationManager.AppSettings["empower"] != null)
                {
                    return (EmpowerMode)int.Parse(ConfigurationManager.AppSettings["empower"]);
                }
                else
                {
                    return EmpowerMode.Server;
                }
            }
        }
        /// <summary>
        /// 是否在线
        /// </summary>
        public static bool IsOnline
        {
            get
            {
                if (ConfigurationManager.AppSettings["Online"] != null && ConfigurationManager.AppSettings["Online"] == "Y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///  记录上传时间
        /// </summary>
        public static string LogUploadTime
        {
            get
            {
                if (ConfigurationManager.AppSettings["LogUploadTime"] != null)
                {
                    return ConfigurationManager.AppSettings["LogUploadTime"];
                }
                else
                {
                    return "2:00";
                }
            }
        }
    }
}
