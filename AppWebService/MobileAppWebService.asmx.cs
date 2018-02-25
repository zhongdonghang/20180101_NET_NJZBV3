using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using AMS.Model;
using AMS.ServiceProxy;
using SeatManage.JsonModel;

namespace AppWebService
{
    /// <summary>
    /// MobileAppWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://juneberry.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class MobileAppWebService : System.Web.Services.WebService, SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy
    {
        public MySoapHeader myHeader = new MySoapHeader();
        private string soapUserName = ConfigurationManager.AppSettings["soapHeaderUserName"];
        private string soapPwd = ConfigurationManager.AppSettings["soapHeaderPwd"];
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string CancelBespeakLog(int bespeakId, string remark)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.CancelBespeakLog(bespeakId, remark);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            } 
        }
        [WebMethod]
        [SoapHeader("myHeader")]

        public string ChangeSeat(string cardNo, string seatNo, string readingRoom)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.ChangeSeat(cardNo, seatNo, readingRoom);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string FreeSeat(string cardNo)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.FreeSeat(cardNo);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
           
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetAllRoomSeatUsedInfo()
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetAllRoomSeatUsedInfo();
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetOpenBespeakRooms(string strDate)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetOpenBespeakRooms(strDate);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
         
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetReaderActualTimeRecord(string cardNum, string getItemsParameter)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetReaderActualTimeRecord(cardNum, getItemsParameter);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
           
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetReaderBespeakRecord(string cardNum, int pageIndex, int pageSize)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetReaderBespeakRecord(cardNum, pageIndex, pageSize);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
          
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetReaderBlacklistRecord(string cardNum, int pageIndex, int pageSize)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetReaderBlacklistRecord(cardNum, pageIndex, pageSize);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
           
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetReaderChooseSeatRecord(string cardNum, int pageIndex, int pageSize)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {

                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetReaderChooseSeatRecord(cardNum, pageIndex, pageSize);
                obtainProxy.Dispose();
                return r;
            }

            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }

        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetReaderAccount(string cardNum, string password)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {

                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetReaderAccount(cardNum, password);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }

        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetScanCodeSeatInfo(string scanResult, string cardNo)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetScanCodeSeatInfo(scanResult, cardNo);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
           
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetSeatsBespeakInfoByRoomNum(string roomNum, string date)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetSeatsBespeakInfoByRoomNum(roomNum, date);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }

           
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetViolateDiscipline(string cardNum, int pageIndex, int pageSize)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {

                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetViolateDiscipline(cardNum, pageIndex, pageSize);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }


        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string ShortLeave(string cardNo)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {

                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.ShortLeave(cardNo);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }

        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string SubmitBespeakInfo(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.SubmitBespeakInfo(cardNo, roomNum, seatNum, bespeakDatetime, remark);
                obtainProxy.Dispose();
                return r;
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }

            
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string SubmitBespeakInfoCustomTime(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {

                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.SubmitBespeakInfoCustomTime(cardNo, roomNum, seatNum, bespeakDatetime, remark);
                obtainProxy.Dispose();
                return r; 
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetBespeakSeatSchoolList()
        {
            List<SeatManage.JsonModel.JM_SchoolModel> jm_schools = new List<SeatManage.JsonModel.JM_SchoolModel>();
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                List<AMS.Model.AMS_School> schoolList =  ICallBackInfoService.GetSchoolList();
                for (int i = 0; i < schoolList.Count; i++)
                {
                    if (schoolList[i].IsSeatBespeak)
                    {
                        SeatManage.JsonModel.JM_SchoolModel jm_school = new SeatManage.JsonModel.JM_SchoolModel();
                        jm_school.SchoolId = schoolList[i].Id.ToString();
                        jm_school.SchoolName = schoolList[i].Name;
                        jm_school.SchoolNum = schoolList[i].ConnectionString;
                        jm_schools.Add(jm_school);
                    }
                }
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_schools);
            }
            else
            {
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_HandleResult() { Result = false, Msg = "没有权限！" });
            }
        }

        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetReaderNotice(string cardNum, int pageIndex, int pageSize)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            { 
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetReaderNotice(cardNum, pageIndex, pageSize);
                obtainProxy.Dispose();
                return r; 
            }
            else
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 通过卡号和学号编号获取用于给用户推送消息的app基础信息。包括channelId、userId。
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNum"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetAppUserInfoByCardNoAndSchoolNum(string cardNo, string schoolNum)
        { 
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                AppUserInfo app_userInfo = App_UserInfoProxy.GetAppUserInfoByCardNoAndSchoolNum(cardNo, schoolNum);
                if (app_userInfo != null)
                {
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(app_userInfo);

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_HandleResult() { Result = false, Msg = "没有权限！" });
            }
        }

        /// <summary>
        /// 绑定app的用户信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="schoolId">学号Id</param>
        /// <param name="channelId">app channelId</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string BindAppUserInfo(string cardNo, string schoolId, string channelId, string userId)
        {
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                AppUserInfo user = new AppUserInfo();
                user.CardNo = cardNo;
                user.SchoolId = int.Parse(schoolId);
                user.ChannelId = channelId;
                user.UserId = userId;
                bool result = App_UserInfoProxy.BindAppUserInfo(user);
                if (result)
                { 
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_HandleResult() { Result = true, Msg = "操作成功！" });
                }
                else
                {
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_HandleResult() { Result = false, Msg = "操作失败！" });
                }
            }
            else
            {
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_HandleResult() { Result = false, Msg = "没有权限！" });
            }
        }


        /// <summary>
        /// 获取读者座位状态
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetReaderSeatState(string cardNum)
        {
             if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                SeatManage.MobileAppDataObtainProxy.IMobileAppDataObtianProxy obtainProxy = new SeatManage.MobileAppDataObtainProxy.MobileAppDataObtainProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetReaderSeatState(cardNum);
                obtainProxy.Dispose();
                return r; 
            }
             else
             {
                 return SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_HandleResult() { Result = false, Msg = "没有权限！" });
             }
        }
    }
}
