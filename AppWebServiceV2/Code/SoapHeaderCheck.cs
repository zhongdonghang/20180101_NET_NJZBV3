using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SeatManage.MobileAppDataObtainProxy;
using SeatManage.SeatManageComm;
using SeatManage.AppJsonModel;

namespace AppWebService.Code
{
    public class SoapHeaderCheck
    {
        private string soapUserName = ConfigurationManager.AppSettings["soapHeaderUserName"];

        private string soapPwd = ConfigurationManager.AppSettings["soapHeaderPwd"];
        /// <summary>
        /// 获取学校列表
        /// </summary>
        /// <returns></returns>
        public string GetSchoolList()
        {
            try
            {
                List<AMS.Model.AMS_School> schools = AMS.ServiceProxy.AMS_SchoolProxy.GetAllSchool();
                List<AJM_School> ajmSchools = (from school in schools where school.AppOpen select new AJM_School() { SchoolNo = school.Number, SchoolName = school.Name }).ToList();
                AJM_HandleResult result = new AJM_HandleResult();
                result.Msg = JSONSerializer.Serialize(ajmSchools);
                result.Result = true;
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Msg = "获取开放手机APP的学校列表失败！";
                result.Result = false;
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 验证客户端连接权限
        /// </summary>
        /// <param name="myHeader"></param>
        /// <param name="isCheckSchool"></param>
        /// <returns></returns>
        public bool CheckSoapHeader(MySoapHeader myHeader, bool isCheckSchool)
        {
          return true;
            if (!myHeader.UserName.Equals(soapUserName) || !MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                return false;
            }
            if (!isCheckSchool)
            {
                return true;
            }
            AMS.Model.AMS_School school = AMS.ServiceProxy.AMS_SchoolProxy.GetSchoolInfoByNum(myHeader.SchoolNum);
            return school != null && school.AppOpen;
        }
    }
}