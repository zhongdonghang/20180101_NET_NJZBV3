using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using AMS.Model;
using SeatManage.AppJsonModel;
using SeatManage.SeatManageComm;
using SocketMsgData;

namespace SeatManage.MobileAppDataObtainProxy
{
    public class MobileAppDataWCFProxy : IMobileAppDataObtianProxy
    {

        //   SeatManage.ClassModel.ServerIp ip = new SeatManage.ClassModel.ServerIp(SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["AppServiceEndpointAddress"].ConnectionString));
        SeatManage.ClassModel.ServerIp ip = new SeatManage.ClassModel.ServerIp(SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["AppServiceEndpointAddress"].ConnectionString));
        private SocketRequest request = null;
        private SocketResponse response = null;
        private bool isError = false;
        /// <summary>
        /// socket客户端，用于与服务器端通信
        /// </summary>
        SocketLib.SocketClient client;
        /// <summary>
        /// 线程同步信号量，发送请求后，线程处于等待状态；待接收到返回数据时，发送信号让线程继续执行。
        /// </summary>
        private AutoResetEvent autoConnectEvent = new AutoResetEvent(false);

        /// <summary>
        /// 学校编号，请求数据时要带上此参数，以判断请求发送到哪个学校。
        /// </summary>
        private AMS_School school;

        public MobileAppDataWCFProxy(string schoolNum)
        {
            string test = SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["AppServiceEndpointAddress"].ConnectionString);
           // ip.IP = "180.96.23.214";

            client = new SocketLib.SocketClient(ip.IP, ip.Port);
            client.OnMsgReceived += new SocketLib.SocketClient.ReceiveMsgHandler(client_OnMsgReceived);
            school = AMS.ServiceProxy.AMS_SchoolProxy.GetSchoolInfoByNum(schoolNum);
            if (school == null)
            {
                school=new AMS_School();
            }
        }
        /// <summary>
        /// 关闭连接并释放资源
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            client.Disconnect();
        }
        /// <summary>
        /// 接收到回复，通知线程继续执行。
        /// </summary>
        /// <param name="info"></param>
        private void client_OnMsgReceived(byte[] info)
        {
            try
            {
                response = SeatManage.SeatManageComm.ByteSerializer.DeserializeByte<SocketResponse>(info);
                autoConnectEvent.Set();
            }
            catch (Exception ex)
            {
                autoConnectEvent.Set();
                isError = true;
            }
        }
        /// <summary>
        /// 连接到socket服务器。链接出错会重试。错误超过三次，则抛出异常。
        /// </summary>
        /// <param name="connectTimes"></param>
        /// <returns></returns>
        private bool SocketConnect(int connectTimes)
        {
            try
            {
                
                return client.Connect();//连接
            }
            catch (Exception ex)
            {
                if (connectTimes > 3)
                {
                    throw ex;
                }
                else
                {
                    connectTimes++;
                    Thread.Sleep(200);
                    return SocketConnect(connectTimes);
                }
            }
        }
        /// <summary>
        /// 发送请求的数据包
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string CallService(SocketRequest request)
        {
            try
            {
                lock (this.client)
                {
                    string test = SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["AppServiceEndpointAddress"].ConnectionString);


                    SocketConnect(0);

                    request.SubSystem = TcpSeatManageSubSystem.AndroidApp;
                    request.Target = school.Number;
                    request.MsgType = TcpMsgDataType.Relay;
                    client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                    Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.RequestMethodType);
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as string;
                    }
                    else if (!string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        throw new Exception(response.ErrorMsg);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                request = null;
                response = null;
                client.Disconnect();
            }
        }



        /// <summary>
        /// 获取用户的基本信息
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        public string GetUserInfo(string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReaderProxy.GetUserInfo(studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetUserInfo";
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReaderProxy.GetUserInfo(studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取用户当前的在座状态
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        public string GetUserNowState(string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReaderProxy.GetUserNowState(studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetUserNowState";
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReaderProxy.GetUserNowState(studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetUserNowStateV2(string studentNo, bool isCheckCode)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReaderProxy.GetUserNowStateV2(studentNo, isCheckCode, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetUserNowStateV2";
                request.Parameters.Add(studentNo);
                request.Parameters.Add(isCheckCode);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReaderProxy.GetUserNowStateV2(studentNo, isCheckCode, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string CheckUser(string loginId, string password)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReaderProxy.CheckUser(loginId, password, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "CheckUser";
                request.Parameters.Add(loginId);
                request.Parameters.Add(password);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReaderProxy.CheckUser(loginId, password, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        public string GetAllRoomInfo()
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReadingRoomProxy.GetAllRoomInfo(school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetAllRoomInfo";
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReadingRoomProxy.GetAllRoomInfo(school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取全部阅览室的当前的使用状态（在座人数，是否开馆等）
        /// </summary>
        /// <returns></returns>
        public string GetAllRoomNowState()
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetAllRoomNowState";
                return CallService(request);
            }
            try
            {
                return WeChatWcfProxy.ReadingRoomProxy.GetAllRoomNowState(school.ConnectionString);

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReadingRoomProxy.GetAllRoomNowState(school.ConnectionString);

                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 根据阅览室编号获取但个阅览室开闭状态
        /// </summary>
        /// <returns></returns>
        public string GetSingleRoomOpenState(string roomNo, string datetime)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReadingRoomProxy.GetSingleRoomOpenState(roomNo, datetime, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetSingleRoomOpenState";
                request.Parameters.Add(roomNo);
                request.Parameters.Add(datetime);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReadingRoomProxy.GetSingleRoomOpenState(roomNo, datetime, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取阅览室布局图
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        public string GetRoomSeatLayout(string roomNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReadingRoomProxy.GetRoomSeatLayout(roomNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetRoomSeatLayout";
                request.Parameters.Add(roomNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReadingRoomProxy.GetRoomSeatLayout(roomNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取阅览室可预约的座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        public string GetRoomBesapeakState(string roomNo, string bespeakTime)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReadingRoomProxy.GetRoomBesapeakState(roomNo, bespeakTime, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetRoomBesapeakState";
                request.Parameters.Add(roomNo);
                request.Parameters.Add(bespeakTime);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReadingRoomProxy.GetRoomBesapeakState(roomNo, bespeakTime, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取进出记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        public string GetEnterOutLog(string studentNo, int pageIndex, int pageSize)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.RecordsProxy.GetEnterOutLog(studentNo, pageIndex, pageSize, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetEnterOutLog";
                request.Parameters.Add(studentNo);
                request.Parameters.Add(pageIndex);
                request.Parameters.Add(pageSize);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.RecordsProxy.GetEnterOutLog(studentNo, pageIndex, pageSize, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取预约记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        public string GetBesapsekLog(string studentNo, int pageIndex, int pageSize)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.RecordsProxy.GetBesapsekLog(studentNo, pageIndex, pageSize, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetBesapsekLog";
                request.Parameters.Add(studentNo);
                request.Parameters.Add(pageIndex);
                request.Parameters.Add(pageSize);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.RecordsProxy.GetBesapsekLog(studentNo, pageIndex, pageSize, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取黑名单记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        public string GetBlacklist(string studentNo, int pageIndex, int pageSize)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.RecordsProxy.GetBlacklist(studentNo, pageIndex, pageSize, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetBlacklist";
                request.Parameters.Add(studentNo);
                request.Parameters.Add(pageIndex);
                request.Parameters.Add(pageSize);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.RecordsProxy.GetBlacklist(studentNo, pageIndex, pageSize, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取违规记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        public string GetViolationLog(string studentNo, int pageIndex, int pageSize)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.RecordsProxy.GetViolationLog(studentNo, pageIndex, pageSize, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetViolationLog";
                request.Parameters.Add(studentNo);
                request.Parameters.Add(pageIndex);
                request.Parameters.Add(pageSize);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.RecordsProxy.GetViolationLog(studentNo, pageIndex, pageSize, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取常用座位
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="seatCount">获取的座位数目</param>
        /// <param name="dayCount">统计的天数</param>
        /// <returns></returns>
        public string GetOftenSeat(string studentNo, int seatCount, int dayCount)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatProxy.GetOftenSeat(studentNo, seatCount, dayCount, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetOftenSeat";
                request.Parameters.Add(studentNo);
                request.Parameters.Add(seatCount);
                request.Parameters.Add(dayCount);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatProxy.GetOftenSeat(studentNo, seatCount, dayCount, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取随机座位
        /// </summary>
        /// <param name="roomNo">如果为空则随机全部阅览室</param>
        /// <returns></returns>
        public string GetRandomSeat(string roomNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatProxy.GetRandomSeat(roomNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetRandomSeat";
                request.Parameters.Add(roomNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatProxy.GetRandomSeat(roomNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="seatNo">座位编号（9位）</param>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="studentNo">学生学号</param>
        /// <param name="besapeakTime">预约的时间（立即预约次处值无效可为空）</param>
        /// <param name="isNowBesapeak">是否是立即预约</param>
        /// <returns></returns>
        public string SubmitBesapeskSeat(string seatNo, string roomNo, string studentNo, string besapeakTime, bool isNowBesapeak)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.SubmitBesapeskSeat(seatNo, roomNo, studentNo, besapeakTime, isNowBesapeak, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "SubmitBesapeskSeat";
                request.Parameters.Add(seatNo);
                request.Parameters.Add(roomNo);
                request.Parameters.Add(studentNo);
                request.Parameters.Add(besapeakTime);
                request.Parameters.Add(isNowBesapeak);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.SubmitBesapeskSeat(seatNo, roomNo, studentNo, besapeakTime, isNowBesapeak, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="bespeakId">预约记录的ID</param>
        /// <returns></returns>
        public string CancelBesapeak(int bespeakId)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.CancelBesapeak(bespeakId, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "CancelBesapeak";
                request.Parameters.Add(bespeakId);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.CancelBesapeak(bespeakId, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="bespeakDate">预约时间</param>
        /// <returns></returns>
        public string CancelBesapeakByCardNo(string cardNo, string bespeakDate)
        {
            //throw new NotImplementedException();
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.CancelBespeakLogByCardNo(cardNo, bespeakDate, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "CancelBesapeakByCardNo";
                request.Parameters.Add(cardNo);
                request.Parameters.Add(bespeakDate);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.CancelBespeakLogByCardNo(cardNo, bespeakDate, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 座位暂离
        /// </summary>
        /// <param name="studentNo">读者学号</param>
        /// <returns></returns>
        public string ShortLeave(string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.ShortLeave(studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "ShortLeave";
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.ShortLeave(studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 释放座位
        /// </summary>
        /// <param name="studentNo">读者学号</param>
        /// <returns></returns>
        public string ReleaseSeat(string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.ReleaseSeat(studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "ReleaseSeat";
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.ReleaseSeat(studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 取消等待座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string CancelWait(string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.CancelWait(studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "CancelWait";
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.CancelWait(studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 扫描选座终端二维码操作
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        public string QRcodeOperation(string codeStr, string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.QRCodeProxy.QRcodeOperation(codeStr, studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "QRcodeOperation";
                request.Parameters.Add(codeStr);
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.QRCodeProxy.QRcodeOperation(codeStr, studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 扫描座位二维码
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        public string QRcodeSeatInfo(string codeStr)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.QRCodeProxy.QRcodeSeatInfo(codeStr, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "QRcodeSeatInfo";
                request.Parameters.Add(codeStr);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.QRCodeProxy.QRcodeSeatInfo(codeStr, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public string ChangeSeat(string seatNo, string roomNo, string cardNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.ChangeSeat(seatNo, roomNo, cardNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "ChangeSeat";
                request.Parameters.Add(seatNo);
                request.Parameters.Add(roomNo);
                request.Parameters.Add(cardNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.ChangeSeat(seatNo, roomNo, cardNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取登录读者详细信息
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string GetUserInfo_WeiXin(string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReaderProxy.GetUserInfo_WeiXin(studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetUserInfo_WeiXin";
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReaderProxy.GetUserInfo_WeiXin(studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string ComeBack(string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.ComeBack(studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "ComeBack";
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.ComeBack(studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 选择座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public string SelectSeat(string studentNo, string seatNo, string roomNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.SelectSeat(studentNo, seatNo, roomNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "SelectSeat";
                request.Parameters.Add(studentNo);
                request.Parameters.Add(seatNo);
                request.Parameters.Add(roomNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.SelectSeat(studentNo, seatNo, roomNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 座位续时
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string DelayTime(string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.DelayTime(studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "DelayTime";
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.DelayTime(studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 等待座位
        /// </summary>
        /// <param name="studentNo_A"></param>
        /// <param name="studentNo_B"></param>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public string WaitSeat(string studentNo_A, string studentNo_B, string seatNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.WaitSeat(studentNo_A, studentNo_B, seatNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "WaitSeat";
                request.Parameters.Add(studentNo_A);
                request.Parameters.Add(studentNo_B);
                request.Parameters.Add(seatNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.WaitSeat(studentNo_A, studentNo_B, seatNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="besapeakNo"></param>
        /// <returns></returns>
        public string ConfirmSeat(string besapeakNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {            
                return WeChatWcfProxy.SeatOperationProxy.ConfirmSeat(besapeakNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "ConfirmSeat";
                request.Parameters.Add(besapeakNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.ConfirmSeat(besapeakNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取可预约的阅览室列表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetCanBespeakRoomInfo(string date)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReadingRoomProxy.GetCanBespeakRoomInfo(date, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetCanBespeakRoomInfo";
                request.Parameters.Add(date);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReadingRoomProxy.GetCanBespeakRoomInfo(date, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取预约座位的信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="bespeakTime"></param>
        /// <returns></returns>
        public string GetSeatBespeakInfo(string seatNo, string roomNo, string bespeakTime)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetSeatBespeakInfo";
                request.Parameters.Add(seatNo);
                request.Parameters.Add(roomNo);
                request.Parameters.Add(bespeakTime);
                return CallService(request);
            }
            try
            {
                return WeChatWcfProxy.SeatProxy.GetSeatBespeakInfo(seatNo, roomNo, bespeakTime, school.ConnectionString);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatProxy.GetSeatBespeakInfo(seatNo, roomNo, bespeakTime, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取座位当前状态
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string GetSeatNowStatus(string seatNo, string roomNo, string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatProxy.GetSeatNowStatus(seatNo, roomNo, studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetSeatNowStatus";
                request.Parameters.Add(seatNo);
                request.Parameters.Add(roomNo);
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatProxy.GetSeatNowStatus(seatNo, roomNo, studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string CheckSeat(string studentNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.CheckSeat(studentNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "CheckSeat";
                request.Parameters.Add(studentNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.CheckSeat(studentNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取座位状态
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public string GetMessageSeatStatus(string seatNo, string roomNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatProxy.GetMessageSeatStatus(seatNo, roomNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetMessageSeatStatus";
                request.Parameters.Add(seatNo);
                request.Parameters.Add(roomNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatProxy.GetMessageSeatStatus(seatNo, roomNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 管理员选座
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public string SelectSeatByMessager(string studentNo, string seatNo, string roomNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.SeatOperationProxy.SelectSeatByMessager(studentNo, seatNo, roomNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "SelectSeatByMessager";
                request.Parameters.Add(studentNo);
                request.Parameters.Add(seatNo);
                request.Parameters.Add(roomNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.SeatOperationProxy.SelectSeatByMessager(studentNo, seatNo, roomNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取阅览室当前状态
        /// </summary>
        /// <returns></returns>
        public string GetLibraryNowState()
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReadingRoomProxy.GetLibraryNowState(school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetLibraryNowState";
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReadingRoomProxy.GetLibraryNowState(school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取开放预约的阅览室
        /// </summary>
        /// <returns></returns>
        public string GetCanBespeakRoom()
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReadingRoomProxy.GetCanBespeakRoom(school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetCanBespeakRoom";
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReadingRoomProxy.GetCanBespeakRoom(school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取阅览室可预约的日期
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public string GetBespeakDate(string roomNo)
        {
            if (school.IsSeatBespeak && !string.IsNullOrEmpty(school.ConnectionString))
            {
                return WeChatWcfProxy.ReadingRoomProxy.GetBespeakDate(roomNo, school.ConnectionString);
            }
            try
            {
                request = new SocketRequest();
                request.RequestMethodType = "GetBespeakDate";
                request.Parameters.Add(roomNo);
                return CallService(request);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(school.ConnectionString))
                {
                    return WeChatWcfProxy.ReadingRoomProxy.GetBespeakDate(roomNo, school.ConnectionString);
                }
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "获取数据失败!";
                return JSONSerializer.Serialize(result);
            }
        }

    }
}
