using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SeatManage.JsonModel;

namespace SeatManage.WebServiceInterface
{
    /// <summary>
    /// IBespeakSeatService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://WebServiceInterface.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class BespeakSeatService : System.Web.Services.WebService
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
        public string GetOpenBespeakRooms(string strDate)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetOpenBespeakRooms(strDate);
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string SubmitBespeakInfo(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.SubmitBespeakInfo(cardNo, roomNum, seatNum, bespeakDatetime, remark);
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
            return server.GetBaseReaderInfo(cardNum);
        }
        /// <summary>
        /// 获取读者实时预约记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="getItemsParameter"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetReaderActualTimeBespeakRecord(string cardNum)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetReaderActualTimeBespeakRecord(cardNum);
        }
        /// <summary>
        /// 提交自定义时间的预约记录（需要验证学校是否购买了该功能模块）
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNum"></param>
        /// <param name="seatNum"></param>
        /// <param name="bespeakDatetime"></param>
        /// <returns></returns> 
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string SubmitBespeakInfoCustomTime(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.SubmitBespeakInfoCustomTime(cardNo, roomNum, seatNum, bespeakDatetime, remark);
        }


        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetReaderAccount(string cardNum, string pwd)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetReaderAccount(cardNum, pwd);
        }
        /// <summary>
        /// 取消预约记录
        /// </summary>
        /// <param name="bespeakId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string CancelBespeakLog(int bespeakId, string remark)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.CancelBespeakLog(bespeakId, remark);
        }
        /// <summary>
        /// 二维码扫描结果处理
        /// </summary>
        /// <param name="scanResult"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetScanCodeSeatInfo(string scanResult, string cardNo)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetScanCodeSeatInfo(scanResult, cardNo);
        }
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="seatNo">座位号</param>
        /// <param name="readingRoom">阅览室编号</param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string ChangeSeat(string cardNo, string seatNo, string readingRoomNum)
        {
            IAuthorizeVerify verify = new WebAuthorizeVerify();
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.ChangeSeat(cardNo, seatNo, "");
        }
    }
}
