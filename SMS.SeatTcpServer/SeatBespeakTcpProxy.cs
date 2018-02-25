using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Net.Sockets;
namespace SMS.SeatTcpServer
{
    /// <summary>
    /// 学校的客户端代理服务
    /// </summary>
    public class SeatBespeakTcpProxy : IService.IService
    {
        SocketLib.SocketClient client;
        SeatManage.ClassModel.ServerIp ip = new SeatManage.ClassModel.ServerIp(ConfigurationManager.ConnectionStrings["ip"].ConnectionString);
        string schoolNum = ConfigurationManager.AppSettings["SchoolNo"];
        SeatManage.IPocketBespeakBllService.IPocketBespeakBllService bespakBll;
        public SeatBespeakTcpProxy()
        {
            try
            {
                bespakBll = new SeatManage.PocketBespeakBllService.PocketBespeakBllService();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void client_OnServerDisconnected(object sender, SocketLib.TcpServerDisconnectedEventArgs e)
        {
            Connect();
        }

        void client_OnServerConnected(object sender, SocketLib.TcpServerConnectedEventArgs e)
        {
            SocketMsgData.SocketMsgBase baseMsg = new SocketMsgData.SocketMsgBase();
            baseMsg.LinkType = "1";
            baseMsg.MsgType = SocketMsgData.TcpMsgDataType.ClientToken;
            baseMsg.Sender = schoolNum;
            baseMsg.SubSystem = SocketMsgData.TcpSeatManageSubSystem.SocketClient;
            client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(baseMsg));
        }

        void client_OnSended(bool successorfalse)
        {
            //throw new NotImplementedException();
        }

        void client_OnMsgReceived(byte[] info)
        {
            SocketMsgData.SocketRequest msg = SeatManage.SeatManageComm.ByteSerializer.DeserializeByte<SocketMsgData.SocketMsgBase>(info) as SocketMsgData.SocketRequest;
            if (msg != null)
            {
                Console.WriteLine("子系统{0}请求执行{1}方法", msg.SubSystem.ToString(), msg.MethodName);
                SocketMsgData.SocketResponse response = new SocketMsgData.SocketResponse();
                switch (msg.MethodName)
                {
                    case "CheckAndGetReaderInfo":
                        try
                        {
                            SeatManage.ClassModel.UserInfo user = msg.Parameters[0] as SeatManage.ClassModel.UserInfo;
                            SeatManage.ClassModel.ReaderInfo reader = bespakBll.CheckAndGetReaderInfo(user);
                            response.Result = reader;
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "DelaySeatUsedTime;":
                        try
                        {
                            SeatManage.ClassModel.ReaderInfo reader = msg.Parameters[0] as SeatManage.ClassModel.ReaderInfo;
                            string result = bespakBll.DelaySeatUsedTime(reader);
                            response.Result = result;
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "FreeSeat":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            string result = bespakBll.FreeSeat(cardNo);
                            response.Result = result;
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetAllReadingRoomInfo":
                        try
                        {
                            List<SeatManage.ClassModel.ReadingRoomInfo> rooms = bespakBll.GetAllReadingRoomInfo();
                            response.Result = rooms;
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetAllRoomSeatUsedState":
                        try
                        {
                            Dictionary<string, SeatManage.ClassModel.ReadingRoomSeatUsedState_Ex> roomState = bespakBll.GetAllRoomSeatUsedState();
                            response.Result = roomState;
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetBlackList":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            int queryDays = int.Parse(msg.Parameters[1].ToString());
                            List<SeatManage.ClassModel.BlackListInfo> result = bespakBll.GetBlackList(cardNo, queryDays);
                            response.Result = result;
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetBookLogs":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            string roomNum = msg.Parameters[1] == null ? null : msg.Parameters[1].ToString();
                            int queryDays = int.Parse(msg.Parameters[2].ToString());
                            List<SeatManage.ClassModel.BespeakLogInfo> result = bespakBll.GetBookLogs(cardNo, roomNum, queryDays);
                            response.Result = result;
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetBookSeatList":
                        try
                        {
                            DateTime dt = (DateTime)msg.Parameters[0];
                            string roomNum = msg.Parameters[1] == null ? null : msg.Parameters[1].ToString();
                            List<SeatManage.ClassModel.Seat> result = bespakBll.GetBookSeatList(dt, roomNum);
                            response.Result = result;
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetCanBespeakReaderRoomInfo":
                        try
                        {
                            DateTime dt = (DateTime)msg.Parameters[0];
                            response.Result = bespakBll.GetCanBespeakReaderRoomInfo(dt);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetEnterOutLogs":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            string roomNum = msg.Parameters[1] == null ? null : msg.Parameters[1].ToString();
                            int queryDate = (int)msg.Parameters[2];
                            response.Result = bespakBll.GetEnterOutLogs(cardNo, roomNum, queryDate);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetReaderInfo":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            response.Result = bespakBll.GetReaderInfo(cardNo);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetReaderInfoByCardNo":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            response.Result = bespakBll.GetReaderInfoByCardNo(cardNo);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetReaderInfoByCardNofalse":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            response.Result = bespakBll.GetReaderInfoByCardNofalse(cardNo);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetViolateDiscipline":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            string roomNum = msg.Parameters[1] == null ? null : msg.Parameters[1].ToString();
                            int queryDate = (int)msg.Parameters[2];
                            response.Result = bespakBll.GetViolateDiscipline(cardNo, roomNum, queryDate);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "SetShortLeave":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            response.Result = bespakBll.SetShortLeave(cardNo);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "SubmitBespeakInfo":
                        try
                        {
                            SeatManage.ClassModel.BespeakLogInfo bespeak = (SeatManage.ClassModel.BespeakLogInfo)msg.Parameters[0];
                            response.Result = bespakBll.SubmitBespeakInfo(bespeak);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "UpdateBookLogsState":
                        try
                        {
                            int bespeak = (int)msg.Parameters[0];
                            response.Result = bespakBll.UpdateBookLogsState(bespeak);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "GetScanCodeSeatInfo":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            string seatNum = msg.Parameters[1].ToString();
                            string readingRoomNum = msg.Parameters[2].ToString();
                            response.Result = bespakBll.GetScanCodeSeatInfo(cardNo, seatNum, readingRoomNum);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;
                    case "ChangeSeat":
                        try
                        {
                            string cardNo = msg.Parameters[0].ToString();
                            string seatNum = msg.Parameters[1].ToString();
                            string readingRoomNum = msg.Parameters[2].ToString();
                            response.Result = bespakBll.ChangeSeat(cardNo, seatNum, readingRoomNum);
                        }
                        catch (Exception ex)
                        {
                            response.ErrorMsg = ex.Message;
                        }
                        break;

                }
                response.MethodName = msg.MethodName;
                response.Sender = msg.Target;
                response.SubSystem = msg.SubSystem;
                response.Target = msg.Sender;
                response.MsgType = SocketMsgData.TcpMsgDataType.Relay;
                client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(response));
                Console.WriteLine("执行成功，已返回结果");
            }
        }
        public void Connect()
        {
            try
            {
                Console.WriteLine("正在连接到服务器……");
                client.Connect(true);
                Console.WriteLine("连接成功");
                SeatManage.SeatManageComm.WriteLog.Write("已与服务器连接，等待终端请求。");
            }
            catch (SocketException ex)
            {
                Console.WriteLine("连接失败，10s后重连…");
                Thread.Sleep(10000);
                Connect();
            }
        }
        Thread connectThread;
        public void Start()
        {
            client = new SocketLib.SocketClient(ip.IP, ip.Port);
            client.OnMsgReceived += new SocketLib.SocketClient.ReceiveMsgHandler(client_OnMsgReceived);
            client.OnSended += new SocketLib.SocketClient.SendCompleted(client_OnSended);
            client.OnServerConnected += new EventHandler<SocketLib.TcpServerConnectedEventArgs>(client_OnServerConnected);
            client.OnServerDisconnected += new EventHandler<SocketLib.TcpServerDisconnectedEventArgs>(client_OnServerDisconnected); 
            connectThread = new Thread(new ThreadStart(Connect));
            connectThread.Start();
            //Connect();
        }

        public void Stop()
        {
            client.Disconnect();
            client.Dispose();
        }

        public override string ToString()
        {
            return "Socket座位预约服务";
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
