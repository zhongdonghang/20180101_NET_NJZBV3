using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SeatManage.JsonModel;

namespace SeatManage.WebServiceInterface
{
    /// <summary>
    /// ChooseSeatService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://WebServiceInterface.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ChooseSeatService : System.Web.Services.WebService, SeatManage.ServiceHelper.IChooseSeat
    {
        public MySoapHeader myHeader = new MySoapHeader();
        SeatManage.ServiceHelper.ServiceHelper server = new SeatManage.ServiceHelper.ServiceHelper();


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 获取所有阅览室基础信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetAllReadingRoomBaseInfo()
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据编号获取阅览室的设置信息
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetReadingRoomSetInfoByRoomNum(string roomNum)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取读者基本信息
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetBaseReaderInfo(string cardNum)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取读者黑名单记录
        /// </summary>
        /// <param name="cardNum">学号</param>
        /// <param name="beforeDays">指定天数</param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetReaderBlacklistRecord(string cardNum, string beforeDays)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取读者的实时记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="getItemsParameter"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetReaderActualTimeRecord(string cardNum, string getItemsParameter)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取读者指定天数内的选择座位的记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="beforeDays">指定天数</param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetReaderChooseSeatRecord(string cardNum, string beforeDays)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }



        /// <summary>
        /// 获取座位使用状态布局图
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetSeatsUsedInfoByRoomNum(string roomNum)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }

        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string VerifyCanDoIt(string cardNo, string roomNo)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }

        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string SeatLock(string seatNum)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }

        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string SeatUnLock(string seatNum)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }

        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string SubmitChooseResult(string cardNum, string seatNum, string roomNum)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            throw new NotImplementedException();
        }
    }
}
