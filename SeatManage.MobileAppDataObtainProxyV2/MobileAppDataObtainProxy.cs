using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketMsgData;

namespace SeatManage.MobileAppDataObtainProxy
{
    /// <summary>
    /// 移动App数据获取访问代理。
    /// 方法执行请求，并把
    /// </summary>
    public class MobileAppDataObtainProxy : IMobileAppDataObtianProxy
    {
        SeatManage.ClassModel.ServerIp ip = new SeatManage.ClassModel.ServerIp(SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["AppServiceEndpointAddress"].ConnectionString));
        private SocketRequest request = null;
        private SocketResponse response = null;

        private bool isError = false;
        /// <summary>
        /// 学校编号，请求数据时要带上此参数，以判断请求发送到哪个学校。
        /// </summary>
        private string schoolNum;
        /// <summary>
        /// socket客户端，用于与服务器端通信
        /// </summary>
        SocketLib.SocketClient client;
        /// <summary>
        /// 线程同步信号量，发送请求后，线程处于等待状态；待接收到返回数据时，发送信号让线程继续执行。
        /// </summary>
        private AutoResetEvent autoConnectEvent = new AutoResetEvent(false);
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
        public MobileAppDataObtainProxy(string schoolNum)
        {
            this.schoolNum = schoolNum;
            client = new SocketLib.SocketClient(ip.IP, ip.Port);
            client.OnMsgReceived += new SocketLib.SocketClient.ReceiveMsgHandler(client_OnMsgReceived);
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
                    SocketConnect(0);

                    request.SubSystem = TcpSeatManageSubSystem.AndroidApp;
                    request.Target = schoolNum;
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
            request = new SocketRequest();
            request.RequestMethodType = "GetUserInfo";
            request.Parameters.Add(studentNo);
            return CallService(request);
        }
        /// <summary>
        /// 获取用户当前的在座状态
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        public string GetUserNowState(string studentNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetUserNowState";
            request.Parameters.Add(studentNo);
            return CallService(request);
        }
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string CheckUser(string loginId, string password)
        {
            request = new SocketRequest();
            request.RequestMethodType = "CheckUser";
            request.Parameters.Add(loginId);
            request.Parameters.Add(password);
            return CallService(request);
        }
        /// <summary>
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        public string GetAllRoomInfo()
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetAllRoomInfo";
            return CallService(request);
        }
        /// <summary>
        /// 获取全部阅览室的当前的使用状态（在座人数，是否开馆等）
        /// </summary>
        /// <returns></returns>
        public string GetAllRoomNowState()
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetAllRoomNowState";
            return CallService(request);
        }

        /// <summary>
        /// 根据阅览室编号获取但个阅览室开闭状态
        /// </summary>
        /// <returns></returns>
        public string GetSingleRoomOpenState(string roomNo, string datetime)
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetSingleRoomOpenState";
            request.Parameters.Add(roomNo);
            request.Parameters.Add(datetime);
            return CallService(request);
        }

        /// <summary>
        /// 获取阅览室布局图
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        public string GetRoomSeatLayout(string roomNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetRoomSeatLayout";
            request.Parameters.Add(roomNo);
            return CallService(request);
        }
        /// <summary>
        /// 获取阅览室可预约的座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        public string GetRoomBesapeakState(string roomNo, string bespeakTime)
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetRoomBesapeakState";
            request.Parameters.Add(roomNo);
            request.Parameters.Add(bespeakTime);
            return CallService(request);
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
            request = new SocketRequest();
            request.RequestMethodType = "GetEnterOutLog";
            request.Parameters.Add(studentNo);
            request.Parameters.Add(pageIndex);
            request.Parameters.Add(pageSize);
            return CallService(request);
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
            request = new SocketRequest();
            request.RequestMethodType = "GetBesapsekLog";
            request.Parameters.Add(studentNo);
            request.Parameters.Add(pageIndex);
            request.Parameters.Add(pageSize);
            return CallService(request);
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
            request = new SocketRequest();
            request.RequestMethodType = "GetBlacklist";
            request.Parameters.Add(studentNo);
            request.Parameters.Add(pageIndex);
            request.Parameters.Add(pageSize);
            return CallService(request);
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
            request = new SocketRequest();
            request.RequestMethodType = "GetViolationLog";
            request.Parameters.Add(studentNo);
            request.Parameters.Add(pageIndex);
            request.Parameters.Add(pageSize);
            return CallService(request);
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
            request = new SocketRequest();
            request.RequestMethodType = "GetOftenSeat";
            request.Parameters.Add(studentNo);
            request.Parameters.Add(seatCount);
            request.Parameters.Add(dayCount);
            return CallService(request);
        }
        /// <summary>
        /// 获取随机座位
        /// </summary>
        /// <param name="roomNo">如果为空则随机全部阅览室</param>
        /// <returns></returns>
        public string GetRandomSeat(string roomNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetRandomSeat";
            request.Parameters.Add(roomNo);
            return CallService(request);
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
            request = new SocketRequest();
            request.RequestMethodType = "SubmitBesapeskSeat";
            request.Parameters.Add(seatNo);
            request.Parameters.Add(roomNo);
            request.Parameters.Add(studentNo);
            request.Parameters.Add(besapeakTime);
            request.Parameters.Add(isNowBesapeak);
            return CallService(request);
        }
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="bespeakId">预约记录的ID</param>
        /// <returns></returns>
        public string CancelBesapeak(int bespeakId)
        {
            request = new SocketRequest();
            request.RequestMethodType = "CancelBesapeak";
            request.Parameters.Add(bespeakId);
            return CallService(request);
        }
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="bespeakDate">预约时间</param>
        /// <returns></returns>
        public string CancelBesapeakByCardNo(string cardNo, string bespeakDate)
        {
            request = new SocketRequest();
            request.RequestMethodType = "CancelBesapeakByCardNo";
            request.Parameters.Add(cardNo);
            request.Parameters.Add(bespeakDate);
            return CallService(request);
        }
        /// <summary>
        /// 座位暂离
        /// </summary>
        /// <param name="studentNo">读者学号</param>
        /// <returns></returns>
        public string ShortLeave(string studentNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "ShortLeave";
            request.Parameters.Add(studentNo);
            return CallService(request);
        }
        /// <summary>
        /// 释放座位
        /// </summary>
        /// <param name="studentNo">读者学号</param>
        /// <returns></returns>
        public string ReleaseSeat(string studentNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "ReleaseSeat";
            request.Parameters.Add(studentNo);
            return CallService(request);
        }
        /// <summary>
        /// 取消等待座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string CancelWait(string studentNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "CancelWait";
            request.Parameters.Add(studentNo);
            return CallService(request);
        }
        /// <summary>
        /// 扫描选座终端二维码操作
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        public string QRcodeOperation(string codeStr, string studentNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "QRcodeOperation";
            request.Parameters.Add(codeStr);
            request.Parameters.Add(studentNo);
            return CallService(request);
        }
        /// <summary>
        /// 扫描座位二维码
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        public string QRcodeSeatInfo(string codeStr)
        {
            request = new SocketRequest();
            request.RequestMethodType = "QRcodeSeatInfo";
            request.Parameters.Add(codeStr);
            return CallService(request);
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
            request = new SocketRequest();
            request.RequestMethodType = "ChangeSeat";
            request.Parameters.Add(seatNo);
            request.Parameters.Add(roomNo);
            request.Parameters.Add(cardNo);
            return CallService(request);
        }

        /// <summary>
        /// 获取登录读者详细信息
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string GetUserInfo_WeiXin(string studentNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetUserInfo_WeiXin";
            request.Parameters.Add(studentNo);
            return CallService(request);
        }

        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string ComeBack(string studentNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "ComeBack";
            request.Parameters.Add(studentNo);
            return CallService(request);
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
            request = new SocketRequest();
            request.RequestMethodType = "SelectSeat";
            request.Parameters.Add(studentNo);
            request.Parameters.Add(seatNo);
            request.Parameters.Add(roomNo);
            return CallService(request);
        }

        /// <summary>
        /// 座位续时
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string DelayTime(string studentNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "DelayTime";
            request.Parameters.Add(studentNo);
            return CallService(request);
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
            request = new SocketRequest();
            request.RequestMethodType = "WaitSeat";
            request.Parameters.Add(studentNo_A);
            request.Parameters.Add(studentNo_B);
            request.Parameters.Add(seatNo);
            return CallService(request);
        }

        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="besapeakNo"></param>
        /// <returns></returns>
        public string ConfirmSeat(string besapeakNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "ConfirmSeat";
            request.Parameters.Add(besapeakNo);
            return CallService(request);
        }

        /// <summary>
        /// 获取可预约的阅览室列表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetCanBespeakRoomInfo(string date)
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetCanBespeakRoomInfo";
            request.Parameters.Add(date);
            return CallService(request);
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
            request = new SocketRequest();
            request.RequestMethodType = "GetSeatBespeakInfo";
            request.Parameters.Add(seatNo);
            request.Parameters.Add(roomNo);
            request.Parameters.Add(bespeakTime);
            return CallService(request);
        }


        public string GetUserNowStateV2(string studentNo, bool isCheckCode)
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetUserNowStateV2";
            request.Parameters.Add(studentNo);
            request.Parameters.Add(isCheckCode);
            return CallService(request);
        }


        public string GetSeatNowStatus(string seatNo, string roomNo, string studentNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetSeatNowStatus";
            request.Parameters.Add(seatNo);
            request.Parameters.Add(roomNo);
            request.Parameters.Add(studentNo);
            return CallService(request);
        }


        public string CheckSeat(string studentNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "CheckSeat";
            request.Parameters.Add(studentNo);
            return CallService(request);
        }


        public string GetMessageSeatStatus(string seatNo, string roomNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetMessageSeatStatus";
            request.Parameters.Add(seatNo);
            request.Parameters.Add(roomNo);
            return CallService(request);
        }

        public string SelectSeatByMessager(string studentNo, string seatNo, string roomNo)
        {
            request = new SocketRequest();
            request.RequestMethodType = "SelectSeatByMessager";
            request.Parameters.Add(studentNo);
            request.Parameters.Add(seatNo);
            request.Parameters.Add(roomNo);
            return CallService(request);
        }


        public string GetLibraryNowState()
        {
            request = new SocketRequest();
            request.RequestMethodType = "GetLibraryNowState";
            return CallService(request);
        }


        public string GetCanBespeakRoom()
        {
            throw new NotImplementedException();
        }

        public string GetBespeakDate(string roomNo)
        {
            throw new NotImplementedException();
        }
    }
}
