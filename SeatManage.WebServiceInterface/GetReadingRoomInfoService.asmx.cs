using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SeatManage.JsonModel;

namespace SeatManage.WebServiceInterface
{
    /// <summary>
    /// GetReadingRoomInfoService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://WebServiceInterface.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class GetReadingRoomInfoService : System.Web.Services.WebService, SeatManage.ServiceHelper.IReadingRoom
    {
        public MySoapHeader myHeader = new MySoapHeader();
        SeatManage.ServiceHelper.ServiceHelper server = new SeatManage.ServiceHelper.ServiceHelper();
        AuthorizeCheck.AuthorizeCheck verify = new AuthorizeCheck.AuthorizeCheck();

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
        /// <summary>
        /// 获取全部阅览室信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetAllReadingRoomBaseInfo()
        {
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            { 
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetAllReadingRoomBaseInfo();
        }
        /// <summary>
        /// 根据阅览室编号
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetReadingRoomSetInfoByRoomNum(string roomNum)
        {
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetReadingRoomSetInfoByRoomNum(roomNum);
        }
        /// <summary>
        /// 获取座位布局图
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetSeatsLayoutByRoomNum(string roomNum)
        {
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetSeatsLayoutByRoomNum(roomNum);
        }
        /// <summary>
        /// 获取阅览室座位使用情况
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetSeatsUsedInfoByRoomNum(string roomNum)
        {
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetSeatsUsedInfoByRoomNum(roomNum);
        }
        ///// <summary>
        ///// 获取能够预约的座位
        ///// </summary>
        ///// <param name="roomNum"></param>
        ///// <returns></returns>
        //[WebMethod]
        //[System.Web.Services.Protocols.SoapHeader("myHeader")]
        //public string GetCanBespeakSeatsLayout(string roomNum)
        //{
        //    IAuthorizeVerify verify = new WebAuthorizeVerify();
        //    if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
        //    {
        //        JM_HandleResult result = new JM_HandleResult();
        //        result.Result = false;
        //        result.Msg = "权限验证失败!";
        //        return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
        //    }
        //    return server.GetCanBespeakSeatsLayout(roomNum);
        //}
        /// <summary>
        /// 根据阅览室编号获取当天可预约的座位信息
        /// </summary>
        /// <param name="roomNum"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetSeatsBespeakInfoByRoomNum(string roomNum, string date)
        {
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetSeatsBespeakInfoByRoomNum(roomNum, date);
        }

        /// <summary>
        /// 获取阅览室座位使用信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public string GetAllRoomSeatUsedInfo()
        {
            if (!verify.Verify(myHeader.UserName, myHeader.PassWord, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "权限验证失败!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            return server.GetAllRoomSeatUsedInfo();
        }
    }
}
