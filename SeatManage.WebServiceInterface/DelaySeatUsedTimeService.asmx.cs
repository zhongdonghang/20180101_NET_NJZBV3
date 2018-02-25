using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SeatManage.JsonModel;

namespace SeatManage.WebServiceInterface
{
    /// <summary>
    /// DelaySeatUsedTimeService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://WebServiceInterface.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class DelaySeatUsedTimeService : System.Web.Services.WebService ,SeatManage.ServiceHelper.IDelaySeatUsedTime
    {
        public MySoapHeader myHeader = new MySoapHeader();
        SeatManage.ServiceHelper.ServiceHelper server = new SeatManage.ServiceHelper.ServiceHelper();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetDelaySet(string roomNum)
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
      
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string SubmitDelayResult(string cardNo)
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
