using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SeatManage.ClassModel;
using SocketMsgData;
using System.Threading;

namespace SMS.BespeakServerProxy
{
    /// <summary>
    /// 客户端访问学校预约方法的代理
    /// </summary>
    public class BespeakServerProxy
    {
        SeatManage.ClassModel.ServerIp ip = new SeatManage.ClassModel.ServerIp(ConfigurationManager.ConnectionStrings["ip"].ConnectionString);
        private SocketRequest request = null;
        private SocketResponse response = null;
        private bool isError = false;
        private string schoolNum;
        SocketLib.SocketClient client;
        private  AutoResetEvent autoConnectEvent = new AutoResetEvent(false);
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

        public BespeakServerProxy(string schoolNum)
        {
            this.schoolNum = schoolNum;
            client = new SocketLib.SocketClient(ip.IP, ip.Port);
            client.OnMsgReceived += new SocketLib.SocketClient.ReceiveMsgHandler(client_OnMsgReceived);
        }

        void client_OnServerDisconnected(object sender, SocketLib.TcpServerDisconnectedEventArgs e)
        {
            Console.WriteLine("与服务器断开连接");
        }

        void client_OnMsgReceived(byte[] info)
        {
            try
            {
                response = SeatManage.SeatManageComm.ByteSerializer.DeserializeByte<SocketResponse>(info);
                autoConnectEvent.Set();
            }
            catch (Exception ex)
            {
                isError = true;
            }
        }

        public SeatManage.ClassModel.ReaderInfo CheckAndGetReaderInfo(UserInfo user)
        {
            try
            {
                lock (this.client)
                {
                    client.Connect();//连接
                    request = new SocketRequest();
                    request.MethodName = "CheckAndGetReaderInfo";
                    request.Parameters.Add(user);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("CheckAndGetReaderInfo") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as ReaderInfo;
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

        public List<ReadingRoomInfo> GetCanBespeakReaderRoomInfo(DateTime bespeakDate)
        {
            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetCanBespeakReaderRoomInfo";
                    request.Parameters.Add(bespeakDate);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;

                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetCanBespeakReaderRoomInfo") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as List<ReadingRoomInfo>;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public List<Seat> GetBookSeatList(DateTime bespeakDate, string RoomId)
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetBookSeatList";
                    request.Parameters.Add(bespeakDate);
                    request.Parameters.Add(RoomId);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetBookSeatList") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as List<Seat>;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public string SubmitBespeakInfo(BespeakLogInfo bespeakInfo)
        {
            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "SubmitBespeakInfo";
                    request.Parameters.Add(bespeakInfo);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                   if (request != null)
                    {

                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);

                    }
                    autoConnectEvent.WaitOne(20000);
                     if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("SubmitBespeakInfo") && string.IsNullOrEmpty(response.ErrorMsg))
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public ReaderInfo GetReaderInfoByCardNo(string cardNo)
        {
            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetReaderInfoByCardNo";
                    request.Parameters.Add(cardNo);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;

                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetReaderInfoByCardNo") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as ReaderInfo;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public ReaderInfo GetReaderInfoByCardNofalse(string cardNo)
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetReaderInfoByCardNofalse";
                    request.Parameters.Add(cardNo);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetReaderInfoByCardNofalse") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as ReaderInfo;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public List<ViolationRecordsLogInfo> GetViolateDiscipline(string cardNo, string readingRoomID, int queryDate)
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetViolateDiscipline";
                    request.Parameters.Add(cardNo);
                    request.Parameters.Add(readingRoomID);
                    request.Parameters.Add(queryDate);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetViolateDiscipline") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as List<ViolationRecordsLogInfo>;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public List<EnterOutLogInfo> GetEnterOutLogs(string cardNo, string readingRoomID, int queryDate)
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetEnterOutLogs";
                    request.Parameters.Add(cardNo);
                    request.Parameters.Add(readingRoomID);
                    request.Parameters.Add(queryDate);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetEnterOutLogs") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as List<EnterOutLogInfo>;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public List<BespeakLogInfo> GetBookLogs(string cardNo, string readingRoomID, int queryDate)
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetBookLogs";
                    request.Parameters.Add(cardNo);
                    request.Parameters.Add(readingRoomID);
                    request.Parameters.Add(queryDate);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetBookLogs") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as List<BespeakLogInfo>;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public bool UpdateBookLogsState(int bookNo)
        {
            // AsyncTcpClient.AsyncTcpClient client = new AsyncTcpClient.AsyncTcpClient(ip.IP, ip.Port);
            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "UpdateBookLogsState";
                    request.Parameters.Add(bookNo);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("UpdateBookLogsState") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return (bool)response.Result;
                    }
                    else if (!string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        throw new Exception(response.ErrorMsg);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public List<BlackListInfo> GetBlackList(string cardNo, int queryDate)
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetBlackList";
                    request.Parameters.Add(cardNo);
                    request.Parameters.Add(queryDate);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetBlackList") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as List<BlackListInfo>;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public List<ReadingRoomInfo> GetAllReadingRoomInfo()
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetAllReadingRoomInfo";
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetAllReadingRoomInfo") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as List<ReadingRoomInfo>;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public string SetShortLeave(string cardNo)
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "SetShortLeave";
                    request.Parameters.Add(cardNo);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("SetShortLeave") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result.ToString();
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
                client.Disconnect();
                request = null;
                response = null;

            }
        }

        public string FreeSeat(string cardNo)
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "FreeSeat";
                    request.Parameters.Add(cardNo);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("FreeSeat") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result.ToString();
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
               // SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                throw ex; 
            }
            finally
            {
                client.Disconnect();
                request = null;
                response = null;

            }
        }

        public ReaderInfo GetReaderInfo(string cardNo)
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetReaderInfo";
                    request.Parameters.Add(cardNo);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetReaderInfo") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as ReaderInfo;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

        public Dictionary<string, ReadingRoomSeatUsedState_Ex> GetAllRoomSeatUsedState()
        {

            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetAllRoomSeatUsedState";
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetAllRoomSeatUsedState") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as Dictionary<string, ReadingRoomSeatUsedState_Ex>;
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
                client.Disconnect();
                request = null;
                response = null;

            }
        }

        public string DelaySeatUsedTime(ReaderInfo reader)
        {
            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "DelaySeatUsedTime";
                    request.Parameters.Add(reader);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("DelaySeatUsedTime") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result.ToString();
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }
        /// <summary>
        /// 读者扫码获取座位信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="seatNum"></param>
        /// <param name="readingRoomNum"></param>
        /// <returns></returns>
        public SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel GetScanCodeSeatInfo(string cardNo, string seatNum, string readingRoomNum)
        { 
            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "GetScanCodeSeatInfo";
                    request.Parameters.Add(cardNo);
                    request.Parameters.Add(seatNum);
                    request.Parameters.Add(readingRoomNum);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("GetScanCodeSeatInfo") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result as SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel;
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }
        /// <summary>
        /// 读者更换座位
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="seatNum"></param>
        /// <param name="readingRoomNum"></param>
        /// <returns></returns>
        public string ChangeSeat(string cardNo, string seatNum, string readingRoomNum)
        {
            try
            {
                lock (this.client)
                {
                    client.Connect();
                    request = new SocketRequest();
                    request.MethodName = "ChangeSeat";
                    request.Parameters.Add(cardNo);
                    request.Parameters.Add(seatNum);
                    request.Parameters.Add(readingRoomNum);
                    request.SubSystem = TcpSeatManageSubSystem.WeiXinSeatBespeak;
                    request.Target = schoolNum;
                    request.MsgType = TcpMsgDataType.Relay;
                    if (request != null)
                    {
                        client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                        Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.MethodName);
                    }
                    autoConnectEvent.WaitOne(20000);
                    if (response == null)
                    {
                        throw new Exception("连接超时。");
                    }
                    else if (!isError && response.MethodName.Equals("ChangeSeat") && string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        return response.Result.ToString();
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
                client.Disconnect();
                request = null;
                response = null;
            }
        }

    }
}
