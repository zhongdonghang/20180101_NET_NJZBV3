using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Web.Services;
using System.Web.Services.Protocols;
using AMS.Model;
using AMS.ServiceProxy;
using SeatManage.AppJsonModel;
using SeatManage.MobileAppDataObtainProxy;
using SeatManage.SeatManageComm;

namespace AppWebService
{
    /// <summary>
    /// MobileAppWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://juneberry.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class MobileAppWebService : WebService
    {
        /// <summary>
        /// zdh测试，稍后删掉
        /// </summary>
        public MobileAppWebService()
        {
            myHeader.UserName = "zdh";
            myHeader.PassWord = "zdh";
            myHeader.SchoolNum = "20171203";
        }


        public MySoapHeader myHeader = new MySoapHeader();
        Code.SoapHeaderCheck headerCheck = new Code.SoapHeaderCheck();
        /// <summary>
        /// 获取用户的基本信息
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetUserInfo(string studentNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
               // IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy("20171203");
                string r = obtainProxy.GetUserInfo(studentNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取用户当前的在座状态
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetUserNowState(string studentNo, bool isCheckCode)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetUserNowStateV2(studentNo, isCheckCode);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string CheckUser(string loginId, string password)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.CheckUser(loginId, password);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetAllRoomInfo()
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetAllRoomInfo();
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取全部阅览室的当前的使用状态（在座人数，是否开馆等）
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetAllRoomNowState()
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetAllRoomNowState();
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }

        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetSingleRoomOpenState(string roomNo, string datetime)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetSingleRoomOpenState(roomNo, datetime);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }

        /// <summary>
        /// 获取阅览室布局图
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetRoomSeatLayout(string roomNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetRoomSeatLayout(roomNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取阅览室可预约的座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetRoomBesapeakState(string roomNo, string bespeakTime)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetRoomBesapeakState(roomNo, bespeakTime);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取进出记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]

        public string GetEnterOutLog(string studentNo, int pageIndex, int pageSize)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetEnterOutLog(studentNo, pageIndex, pageSize);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取预约记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetBesapsekLog(string studentNo, int pageIndex, int pageSize)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetBesapsekLog(studentNo, pageIndex, pageSize);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取黑名单记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetBlacklist(string studentNo, int pageIndex, int pageSize)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetBlacklist(studentNo, pageIndex, pageSize);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取违规记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetViolationLog(string studentNo, int pageIndex, int pageSize)
        {

            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetViolationLog(studentNo, pageIndex, pageSize);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取常用座位
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="seatCount">获取的座位数目</param>
        /// <param name="dayCount">统计的天数</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetOftenSeat(string studentNo, int seatCount, int dayCount)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetOftenSeat(studentNo, seatCount, dayCount);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取随机座位
        /// </summary>
        /// <param name="roomNo">如果为空则随机全部阅览室</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetRandomSeat(string roomNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetRandomSeat(roomNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 预约提交
        /// </summary>
        /// <param name="seatNo">座位编号（9位）</param>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="studentNo">学生学号</param>
        /// <param name="besapeakTime">预约的时间（立即预约次处值无效可为空）</param>
        /// <param name="isNowBesapeak">是否是立即预约</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string SubmitBesapeskSeat(string seatNo, string roomNo, string studentNo, string besapeakTime, bool isNowBesapeak)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.SubmitBesapeskSeat(seatNo, roomNo, studentNo, besapeakTime, isNowBesapeak);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="bespeakId">预约记录的ID</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string CancelBesapeak(int bespeakId)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.CancelBesapeak(bespeakId);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 座位暂离
        /// </summary>
        /// <param name="studentNo">读者学号</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string ShortLeave(string studentNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.ShortLeave(studentNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 释放座位
        /// </summary>
        /// <param name="studentNo">读者学号</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string ReleaseSeat(string studentNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.ReleaseSeat(studentNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 取消等待座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string CancelWait(string studentNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.CancelWait(studentNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 扫描选座终端二维码操作
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string QRcodeOperation(string codeStr, string studentNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.QRcodeOperation(codeStr, studentNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 扫描座位二维码
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string QRcodeSeatInfo(string codeStr)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.QRcodeSeatInfo(codeStr);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取学校列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetSchoolList()
        {
            if (headerCheck.CheckSoapHeader(myHeader, false))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = headerCheck.GetSchoolList();
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 根据学号取消预约记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="bespeakDate"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string CancelBesapeakByCardNo(string cardNo, string bespeakDate)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.CancelBesapeakByCardNo(cardNo, bespeakDate);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string ChangeSeat(string seatNo, string roomNo, string cardNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.ChangeSeat(seatNo, roomNo, cardNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取读者信息（包括设置信息）
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetUserInfo_WeiXin(string studentNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetUserInfo_WeiXin(studentNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string ComeBack(string studentNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.ComeBack(studentNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 选座
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string SelectSeat(string studentNo, string seatNo, string roomNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.SelectSeat(studentNo, seatNo, roomNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 续时
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string DelayTime(string studentNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.DelayTime(studentNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 等待座位
        /// </summary>
        /// <param name="studentNo_A"></param>
        /// <param name="studentNo_B"></param>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string WaitSeat(string studentNo_A, string studentNo_B, string seatNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.WaitSeat(studentNo_A, studentNo_B, seatNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="besapeakNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string ConfirmSeat(string besapeakNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.ConfirmSeat(besapeakNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取可预约的阅览室列表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetCanBespeakRoomInfo(string date)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetCanBespeakRoomInfo(date);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取座位预约信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="bespeakTime"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetSeatBespeakInfo(string seatNo, string roomNo, string bespeakTime)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetSeatBespeakInfo(seatNo, roomNo, bespeakTime);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取座位信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetSeatNowStatus(string seatNo, string roomNo, string studentNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetSeatNowStatus(seatNo, roomNo, studentNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string CheckSeat(string studentNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.CheckSeat(studentNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取管理员座位管理信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetMessageSeatStatus(string seatNo, string roomNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetMessageSeatStatus(seatNo, roomNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }

        /// <summary>
        /// 分配座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string SelectSeatByMessager(string studentNo, string seatNo, string roomNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.SelectSeatByMessager(studentNo, seatNo, roomNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }

        /// <summary>
        /// 获取当前图书馆使用情况
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetLibraryNowState()
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetLibraryNowState();
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取开放预约的阅览室
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetCanBespeakRoom()
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetCanBespeakRoom();
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 获取阅览室可预约的日期
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetBespeakDate(string roomNo)
        {
            if (headerCheck.CheckSoapHeader(myHeader, true))
            {
                IMobileAppDataObtianProxy obtainProxy = new MobileAppDataWCFProxy(myHeader.SchoolNum);
                string r = obtainProxy.GetBespeakDate(roomNo);
                obtainProxy.Dispose();
                return r;
            }
            AJM_HandleResult result = new AJM_HandleResult();
            result.Result = false;
            result.Msg = "权限验证失败!";
            return JSONSerializer.Serialize(result);
        }
    }
}
