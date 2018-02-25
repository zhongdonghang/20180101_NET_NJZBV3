using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SeatManage.JsonModel;

namespace SeatManage.WebServiceInterface
{
    /// <summary>
    /// GetReaderInfoService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://WebServiceInterface.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class GetReaderInfoService : System.Web.Services.WebService ,SeatManage.ServiceHelper.IReader
    {
        SeatManage.ServiceHelper.ServiceHelper server = new SeatManage.ServiceHelper.ServiceHelper(); 
        public MySoapHeader myHeader = new MySoapHeader();
        AuthorizeCheck.AuthorizeCheck verify = new AuthorizeCheck.AuthorizeCheck();
        

        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 根据读者卡号获取读者信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetBaseReaderInfoByCardId(string cardId)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetBaseReaderInfoByCardId(cardId);
        }
        /// <summary>
        /// 根据读者学号获取读者信息
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
            return server.GetBaseReaderInfo(cardNum);
        }
        /// <summary>
        /// 获取读者实时记录
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
            return server.GetReaderActualTimeRecord(cardNum, getItemsParameter);
        }
        /// <summary>
        /// 获取读者的预约记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetReaderBespeakRecord(string cardNum, int pageIndex, int pageSize)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetReaderBespeakRecord(cardNum,  pageIndex,  pageSize);
        }
        /// <summary>
        /// 获取读者的选座记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")] 
        public string GetReaderChooseSeatRecord(string cardNum, int pageIndex,int pageSize)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetReaderChooseSeatRecord(cardNum, pageIndex, pageSize);
        }
        /// <summary>
        /// 获取读者的黑名单记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetReaderBlacklistRecord(string cardNum, int pageIndex, int pageSize)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetReaderBlacklistRecord(cardNum, pageIndex,pageSize);
        }
        /// <summary>
        /// 验证用户的账号
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")] 
        public string GetReaderAccount(string cardNum,string password )
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetReaderAccount(cardNum, password);
        }


        /// <summary>
        /// 获取读者的违规记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")] 
        public string GetViolateDiscipline(string cardNum, int pageIndex, int pageSize)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetViolateDiscipline(cardNum, pageIndex, pageSize);
        }
    }
}
