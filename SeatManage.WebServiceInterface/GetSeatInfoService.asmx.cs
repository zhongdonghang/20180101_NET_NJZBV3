using SeatManage.JsonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SeatManage.WebServiceInterface
{
    /// <summary>
    /// GetSeatInfoService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class GetSeatInfoService : System.Web.Services.WebService, SeatManage.ServiceHelper.ISeat
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
        /// 获取座位的预约记录
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetSeatBespeakInfo(string seatNum)
        {
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            
            return server.GetSeatBespeakInfo(seatNum);
        }
        /// <summary>
        /// 获取座位的使用情况
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetSeatUsage(string seatNum)
        {
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetSeatUsage(seatNum);
        }
        /// <summary>
        /// 二维码扫描结果处理
        /// </summary>
        /// <param name="scanResult"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetQRcodeInfo(string strQRcode)
        {
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetQRcodeInfo(strQRcode);
        }
    }
}
