using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Win32;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        /// <summary>
        /// 获取注册表值
        /// </summary>
        /// <returns></returns>
        public SeatManage.ClassModel.RegistryKey GetRegistryKey()
        {
            SeatManage.ClassModel.RegistryKey model = new SeatManage.ClassModel.RegistryKey();
            RegistryKey lm = Registry.CurrentUser;
            //对应HKEY_LOCAL_MACHINE基项分支
            RegistryKey software = lm.OpenSubKey("SOFTWARE", false);
            RegistryKey juneberry = software.OpenSubKey("Juneberry", false);
            if (juneberry != null)
            {
                RegistryKey interfaceKey = juneberry.OpenSubKey("InterfaceKey", false);
                if (interfaceKey != null)
                {
                    if (interfaceKey.GetValue("JuneberryReadingRoomInterfaceKey") != null)
                    {
                        model.RegistryList["JuneberryReadingRoomInterfaceKey"] = interfaceKey.GetValue("JuneberryReadingRoomInterfaceKey").ToString();
                    }
                    if (interfaceKey.GetValue("JuneberryAccessInterfaceKey") != null)
                    {
                        model.RegistryList["JuneberryAccessInterfaceKey"] = interfaceKey.GetValue("JuneberryAccessInterfaceKey").ToString();
                    }
                    if (interfaceKey.GetValue("JuneberryMediaReleaseKey") != null)
                    {
                        model.RegistryList["JuneberryMediaReleaseKey"] = interfaceKey.GetValue("JuneberryMediaReleaseKey").ToString();
                    }
                }
            }
            lm.Close();
            return model;
        }
        /// <summary>
        /// 保存注册表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveRegistryKey(SeatManage.ClassModel.RegistryKey model)
        {
            RegistryKey lm = Registry.CurrentUser;
            //对应HKEY_LOCAL_MACHINE基项分支
            RegistryKey software = lm.OpenSubKey("SOFTWARE", true);
            RegistryKey juneberry = software.OpenSubKey("Juneberry", true);
            if (juneberry == null)
            {
                juneberry = software.CreateSubKey("Juneberry");
            }
            RegistryKey interfaceKey = juneberry.OpenSubKey("InterfaceKey", true);
            if (interfaceKey == null)
            {
                interfaceKey = juneberry.CreateSubKey("InterfaceKey");
            }
            interfaceKey.SetValue("JuneberryReadingRoomInterfaceKey", model.RegistryList["JuneberryReadingRoomInterfaceKey"]);
            interfaceKey.SetValue("JuneberryAccessInterfaceKey", model.RegistryList["JuneberryAccessInterfaceKey"]);
            interfaceKey.SetValue("JuneberryMediaReleaseKey", model.RegistryList["JuneberryMediaReleaseKey"]);
            
            lm.Close();
            return true;
        }
        /// <summary>
        /// 获取学校编号
        /// </summary>
        /// <returns></returns>
        public string GetSchoolNum()
        {
            return ConfigurationManager.AppSettings["SchoolNo"];
        }
        /// <summary>
        /// 验证查询接口授权
        /// </summary>
        /// <returns></returns>
        public bool ReadingRoomInterfaceIsAuthorize()
        {
            string schoolNum = GetSchoolNum();
            if (string.IsNullOrEmpty(schoolNum))
            {
                return false;
            }
            SeatManage.ClassModel.RegistryKey reg = GetRegistryKey();
            if (string.IsNullOrEmpty(reg.RegistryList["JuneberryReadingRoomInterfaceKey"]))
            {
                return false;
            }
            else
            {
                string pass = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(new List<string>() { schoolNum, "JuneberryReadingRoomInterfaceKey" });
                if (reg.RegistryList["JuneberryReadingRoomInterfaceKey"] == pass.Substring(0, 8) + "-" + pass.Substring(8, 8) + "-" + pass.Substring(16, 8) + "-" + pass.Substring(24, 8))
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
        /// 验证门禁接口授权
        /// </summary>
        /// <returns></returns>
        public bool AccessInterfaceIsAuthorize()
        {
            string schoolNum = GetSchoolNum();
            if (string.IsNullOrEmpty(schoolNum))
            {
                return false;
            }
            SeatManage.ClassModel.RegistryKey reg = GetRegistryKey();
            if (string.IsNullOrEmpty(reg.RegistryList["JuneberryAccessInterfaceKey"]))
            {
                return false;
            }
            else
            {
                string pass = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(new List<string>() { schoolNum, "JuneberryAccessInterfaceKey" });
                if (reg.RegistryList["JuneberryAccessInterfaceKey"] == pass.Substring(0, 8) + "-" + pass.Substring(8, 8) + "-" + pass.Substring(16, 8) + "-" + pass.Substring(24, 8))
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
        /// 验证下发工具授权
        /// </summary>
        /// <returns></returns>
        public bool MediaReleaseIsAuthorize()
        {
            string schoolNum = GetSchoolNum();
            if (string.IsNullOrEmpty(schoolNum))
            {
                return false;
            }
            SeatManage.ClassModel.RegistryKey reg = GetRegistryKey();
            if (string.IsNullOrEmpty(reg.RegistryList["JuneberryMediaReleaseKey"]))
            {
                return false;
            }
            else
            {
                string pass = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(new List<string>() { schoolNum, "JuneberryMediaReleaseKey" });
                if (reg.RegistryList["JuneberryMediaReleaseKey"] == pass.Substring(0, 8) + "-" + pass.Substring(8, 8) + "-" + pass.Substring(16, 8) + "-" + pass.Substring(24, 8))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
