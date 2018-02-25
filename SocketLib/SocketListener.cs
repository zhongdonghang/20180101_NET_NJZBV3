using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketLib
{
    public sealed class SocketListener : IDisposable
    {
        static string className = "SocketListener";
        /// <summary>
        /// 缓冲区
        /// </summary>
        private BufferManager bufferManager;
        /// <summary>
        /// 服务器端Socket
        /// </summary>
        private Socket listenSocket;
        /// <summary>
        /// 服务同步锁
        /// </summary>
        private static Mutex mutex = new Mutex();
        /// <summary>
        /// 当前连接数
        /// </summary>
        private Int32 numConnections;
        /// <summary>
        /// 最大并发量
        /// </summary>
        private Int32 numConcurrence;
        /// <summary>
        /// 服务器状态
        /// </summary>
        private ServerState serverstate;
        /// <summary>
        /// 读取写入字节
        /// </summary>
        private const Int32 opsToPreAlloc = 1;
        /// <summary>
        /// Socket连接池
        /// </summary>
        private SocketAsyncEventArgsPool readWritePool;
        /// <summary>
        /// 并发控制信号量
        /// </summary>
        private Semaphore semaphoreAcceptedClients;
        /// <summary>
        /// 通信协议
        /// </summary>
        // private RequestHandler handler;

        /// <summary>
        /// 心跳包计时器
        /// </summary>
        System.Timers.Timer heartbeatTimer = null;
        /// <summary>
        /// 接收到信息时的事件委托
        /// </summary>
        /// <param name="info"></param>
        public delegate void ReceiveMsgHandler(string uid, SocketMsgData.SocketMsgBase e);

        /// <summary>
        /// 发送信息完成后的委托
        /// </summary>
        /// <param name="successorfalse"></param>
        public delegate void SendCompletedHandler(string uid, string exception);

        /// <summary>
        /// 获取常连接列表的委托
        /// </summary>
        /// <returns></returns>
        public delegate Dictionary<string, string> DelGetKeepAliveLink();
        /// <summary>
        /// 接收到信息时的事件 
        /// </summary>
        public event ReceiveMsgHandler OnMsgReceived;
        /// <summary>
        /// 发送信息完成后的事件
        /// </summary>
        public event SendCompletedHandler OnSended;

        /// <summary>
        /// 与客户端的连接已建立事件
        /// </summary>
        public event EventHandler<TcpClientConnectedEventArgs> OnClientConnected;
        /// <summary>
        /// 与客户端的连接已断开事件
        /// </summary>
        public event EventHandler<TcpClientDisconnectedEventArgs> OnClientDisconnected;
        /// <summary>
        /// 获取常连接的委托
        /// </summary>
        public DelGetKeepAliveLink GetKeepAliveLink;

        /// <summary>
        /// 获取当前的并发数
        /// </summary>
        public Int32 NumConnections
        {
            get { return this.numConnections; }
        }
        /// <summary>
        /// 最大并发数
        /// </summary>
        public Int32 MaxConcurrence
        {
            get { return this.numConcurrence; }
        }
        /// <summary>
        /// 返回服务器状态
        /// </summary>
        public ServerState State
        {
            get
            {
                return serverstate;
            }
        }
        /// <summary>
        /// 获取当前在线用户的UID
        /// </summary>
        public List<string> OnlineUID
        {
            get { return readWritePool.OnlineUID; }
        }

        /// <summary>
        /// 初始化服务器端
        /// </summary>
        /// <param name="numConcurrence">并发的连接数量(1000以上,35000以下)</param>
        /// <param name="receiveBufferSize">每一个收发缓冲区的大小(32768)</param>
        public SocketListener(Int32 numConcurrence, Int32 receiveBufferSize)
        {
            try
            {
                serverstate = ServerState.Initialing;
                this.numConnections = 0;
                this.numConcurrence = numConcurrence;
                this.bufferManager = new BufferManager(receiveBufferSize * numConcurrence * opsToPreAlloc, receiveBufferSize);
                this.readWritePool = new SocketAsyncEventArgsPool(numConcurrence);
                this.semaphoreAcceptedClients = new Semaphore(numConcurrence, numConcurrence);
                //handler = new RequestHandler();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("类名称：{0},方法名：{1},异常原因：{2}", className, "SocketListener", ex.Message));
            }
        }
        /// <summary>
        /// 服务端初始化
        /// </summary>
        public void Init()
        {
            this.bufferManager.InitBuffer();
            SocketAsyncEventArgsWithId readWriteEventArgWithId;
            for (Int32 i = 0; i < this.numConcurrence; i++)
            {
                readWriteEventArgWithId = new SocketAsyncEventArgsWithId();
                readWriteEventArgWithId.ReceiveSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceiveCompleted);
                readWriteEventArgWithId.SendSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);

                //只给接收的SocketAsyncEventArgs设置缓冲区
                this.bufferManager.SetBuffer(readWriteEventArgWithId.ReceiveSAEA);
                this.readWritePool.Push(readWriteEventArgWithId);
            }

            this.heartbeatTimer = new System.Timers.Timer();
            this.heartbeatTimer.Interval = 40000;
            this.heartbeatTimer.Elapsed += new System.Timers.ElapsedEventHandler(heartbeatTimer_Elapsed);
            serverstate = ServerState.Inited;
        }

        void heartbeatTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string[] uidList = this.OnlineUID.ToArray();
            foreach (string uid in uidList)
            {
                if (this.readWritePool.BusyPoolContains(uid))
                {
                    this.readWritePool.busypool[uid].UserOnLineCounter++;
                    if (this.readWritePool.busypool[uid].UserOnLineCounter >= 3)
                    {
                        CloseClientSocket(uid);
                    }
                }
            }

        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="data">端口号</param>
        public void Start(Int32 data)
        {
            Int32 port = (Int32)data;
            IPAddress[] addresslist = Dns.GetHostEntry(Environment.MachineName).AddressList;
            IPEndPoint localEndPoint = new IPEndPoint(addresslist[addresslist.Length - 1], port);
            this.listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            if (localEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
            {
                this.listenSocket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
                this.listenSocket.Bind(new IPEndPoint(IPAddress.IPv6Any, localEndPoint.Port));
            }
            else
            {
                Console.WriteLine("开始侦听：");
                for (int i = 0; i < addresslist.Length; i++)
                {
                    if (addresslist[i].IsIPv6LinkLocal)
                    {
                        continue;
                    }
                    Console.WriteLine(string.Format("    {0}:{1}", addresslist[i].ToString(), port));
                }
                this.listenSocket.Bind(new IPEndPoint(IPAddress.Any, localEndPoint.Port));//侦听所有活动的IP端口
            }
            this.listenSocket.Listen(100);
            this.StartAccept(null);
            serverstate = ServerState.Running;
            heartbeatTimer.Start();
            mutex.WaitOne();
        }


        /// <summary>
        /// 发送信息,发送失败，重试五次
        /// </summary>
        /// <param name="uid">要发送的用户的uid</param>
        /// <param name="msg">消息体</param>
        public void Send(string uid, byte[] msg)
        {
            SocketAsyncEventArgsWithId socketWithId = readWritePool.FindByUID(uid);
            if (socketWithId == null)
            {
                //说明用户已经断开  
                //100   发送成功
                //200   发送失败
                //300   用户不在线
                //其它  表示异常的信息
                if (OnSended != null)
                {
                    OnSended(uid, "300");
                }
                Console.WriteLine("{0:M} {1:t}：{2}", DateTime.Now, DateTime.Now, "用户不在线");
            }
            else
            {
                MySocketAsyncEventArgs e = socketWithId.SendSAEA;
                if (e.SocketError == SocketError.Success)
                {
                    int i = 0;
                    try
                    {
                        byte[] arrayLength = BitConverter.GetBytes(msg.Length);
                        List<byte> listByte = new List<byte>(arrayLength.Length + msg.Length);
                        listByte.AddRange(arrayLength);
                        listByte.AddRange(msg);
                        byte[] sendBuffer = listByte.ToArray();
                        e.SetBuffer(sendBuffer, 0, sendBuffer.Length);
                        Boolean willRaiseEvent = (e.UserToken as Socket).SendAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessSend(e);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (i <= 5)
                        {
                            i++;
                            //如果发送出现异常就延迟0.01秒再发
                            Thread.Sleep(10);
                            Send(uid, msg);
                        }
                        else
                        {
                            if (OnSended != null)
                            {
                                OnSended(uid, ex.ToString());
                            }
                        }
                    }
                }
                else
                {
                    if (OnSended != null)
                    {
                        OnSended(uid, "200");
                    }
                    this.CloseClientSocket(((MySocketAsyncEventArgs)e).UID);
                }
            }
        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        public void Stop()
        {
            if (listenSocket != null)
                listenSocket.Close();
            listenSocket = null;
            Dispose();
            mutex.ReleaseMutex();
            serverstate = ServerState.Stoped;
        }


        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            try
            {
                if (acceptEventArg == null)
                {
                    acceptEventArg = new SocketAsyncEventArgs();
                    acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
                }
                else
                    acceptEventArg.AcceptSocket = null;
                this.semaphoreAcceptedClients.WaitOne();
                Boolean willRaiseEvent = this.listenSocket.AcceptAsync(acceptEventArg);
                Console.WriteLine("{0:M} {1:t}：开始侦听新的连接……", DateTime.Now, DateTime.Now);
                if (!willRaiseEvent)
                {
                    this.ProcessAccept(acceptEventArg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnAcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            this.ProcessAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.LastOperation != SocketAsyncOperation.Accept)    //检查上一次操作是否是Accept，不是就返回
                {
                    return;
                }
                //if (e.BytesTransferred <= 0)    //检查发送的长度是否大于0,不是就返回
                //    return;
                string UID = e.AcceptSocket.RemoteEndPoint.ToString();   //根据IP获取用户的UID
                if (UID == string.Empty || UID == null || UID == "")
                    return;

                SocketAsyncEventArgsWithId readEventArgsWithId = null;
                if (readWritePool.BusyPoolContains(UID))    //判断现在的用户是否已经连接，避免同一用户开两个连接
                {
                    readEventArgsWithId = readWritePool.busypool[UID];
                }
                else
                {
                    lock (this.readWritePool)
                    {
                        readEventArgsWithId = this.readWritePool.Pop(UID);
                    }
                }
                readEventArgsWithId.ReceiveSAEA.UserToken = e.AcceptSocket;
                readEventArgsWithId.SendSAEA.UserToken = e.AcceptSocket;
                Interlocked.Increment(ref this.numConnections);
                lock (this.readWritePool)
                {
                    if (OnClientConnected != null)
                    {
                        //连接事件
                        OnClientConnected(this, new TcpClientConnectedEventArgs(UID));
                    }
                }

                Boolean willRaiseEvent = (readEventArgsWithId.ReceiveSAEA.UserToken as Socket).ReceiveAsync(readEventArgsWithId.ReceiveSAEA);
                if (!willRaiseEvent)
                    ProcessReceive(readEventArgsWithId.ReceiveSAEA);
            }
            catch (Exception EX)
            {
                throw EX;
            }
            finally
            {
                this.StartAccept(e);
            }
        }

        private void OnReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessReceive(e);
        }
        private void OnSendCompleted(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                ProcessSend(e);
            }
            catch (Exception ex)
            {

            }
        }
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            try
            {
                string uid = ((MySocketAsyncEventArgs)e).UID;//用户Id
                if (e.LastOperation != SocketAsyncOperation.Receive)
                    return;
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
                {
                    Int32 byteTransferred = e.BytesTransferred;
                    byte[] received = new byte[byteTransferred];
                    Buffer.BlockCopy(e.Buffer, e.Offset, received, 0, byteTransferred);
                    List<byte[]> msg = null;
                    //检查消息的准确性
                    lock (this.OnlineUID)
                    {
                        try
                        {
                            msg = this.readWritePool.busypool[uid].MsgHandler.GetActualObject(received);
                        }
                        catch (Exception ex)
                        {
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据消息头长度截取消息时遇到异常：{0}", ex.Message));
                            return;
                        }
                    }
                    foreach (byte[] m in msg)
                    {
                        SocketMsgData.SocketMsgBase smb = null;
                        try
                        {
                            smb = SeatManage.SeatManageComm.ByteSerializer.DeserializeByte<SocketMsgData.SocketMsgBase>(m);
                        }
                        catch (Exception ex)
                        {
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("消息反序列化失败：{0}", ex.Message));
                            return;
                        }
                        if (smb != null && smb.MsgType == SocketMsgData.TcpMsgDataType.Heartbeat)
                        {
                            if (this.readWritePool.BusyPoolContains(uid))
                            {
                                this.readWritePool.busypool[uid].UserOnLineCounter = 0;
                            }
                            else
                            {
                                SocketAsyncEventArgsWithId readEventArgsWithId = null;
                                lock (this.readWritePool)
                                {
                                    readEventArgsWithId = this.readWritePool.Pop(uid);
                                }

                                readEventArgsWithId.ReceiveSAEA.UserToken = e.AcceptSocket;
                                readEventArgsWithId.SendSAEA.UserToken = e.AcceptSocket;
                                Interlocked.Increment(ref this.numConnections);
                                lock (this.readWritePool)
                                {
                                    if (OnClientConnected != null)
                                    {
                                        //连接事件
                                        OnClientConnected(this, new TcpClientConnectedEventArgs(uid));
                                    }
                                }
                            }


                            //else
                            //{
                            //    this.readWritePool.Pop(uid);
                            //    if (OnMsgReceived != null)
                            //    {
                            //        smb.MsgType = SocketMsgData.TcpMsgDataType.ClientToken;
                            //        OnMsgReceived(((MySocketAsyncEventArgs)e).UID, smb);
                            //    }
                            //}

                            Console.WriteLine("接收到{0}的心跳包", ((MySocketAsyncEventArgs)e).UID);
                        }
                        else if (OnMsgReceived != null)
                        {
                            OnMsgReceived(((MySocketAsyncEventArgs)e).UID, smb);
                        }
                    }
                    //可以在这里设一个停顿来实现间隔时间段监听，这里的停顿是单个用户间的监听间隔
                    //发送一个异步接受请求，并获取请求是否为成功
                    Boolean willRaiseEvent = (e.UserToken as Socket).ReceiveAsync(e);
                    if (!willRaiseEvent)
                        ProcessReceive(e);

                }
                else
                {
                    this.CloseClientSocket(((MySocketAsyncEventArgs)e).UID);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("Socket服务处理接收到的消息遇到异常：{0}", ex.Message));
            }

        }
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.LastOperation != SocketAsyncOperation.Send)
                    return;
                if (e.BytesTransferred > 0)
                {
                    if (e.SocketError == SocketError.Success)
                        if (OnSended != null)
                        {
                            OnSended(((MySocketAsyncEventArgs)e).UID, "100");
                        }
                        else if (OnSended != null)
                        {
                            OnSended(((MySocketAsyncEventArgs)e).UID, "200");
                        }
                }
                else
                    this.CloseClientSocket(((MySocketAsyncEventArgs)e).UID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseClientSocket(string uid)
        {
            if (uid == string.Empty || uid == "")
                return;
            SocketAsyncEventArgsWithId saeaw = readWritePool.FindByUID(uid);
            if (saeaw == null)
                return;
            Socket r = saeaw.ReceiveSAEA.UserToken as Socket;
            Socket s = saeaw.SendSAEA.UserToken as Socket;
            try
            {
                r.Shutdown(SocketShutdown.Both);
                r.Close();
                Interlocked.Decrement(ref this.numConnections);
                this.readWritePool.Push(saeaw);
                this.semaphoreAcceptedClients.Release();
                lock (this.readWritePool)
                {
                    if (OnClientDisconnected != null)
                    {
                        //关闭连接事件
                        OnClientDisconnected(this, new TcpClientDisconnectedEventArgs(uid));
                    }
                }
            }
            catch (Exception)
            {
                //客户端已经关闭
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            bufferManager.Dispose();
            bufferManager = null;
            readWritePool.Dispose();
            readWritePool = null;
        }

        #endregion
    }
    public enum ServerState { Initialing, Inited, Ready, Running, Stoped }

    public class TcpClientConnectedEventArgs : EventArgs
    {
        private string uid = string.Empty;

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }
        /// <summary>
        /// 与客户端的连接已建立事件参数
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        public TcpClientConnectedEventArgs(string uid)
        {
            if (string.IsNullOrEmpty(uid))
                throw new ArgumentNullException("tcpClient");

            this.uid = uid;
        }
    }

    public class TcpClientDisconnectedEventArgs : EventArgs
    {
        string uid = string.Empty;

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }
        /// <summary>
        /// 与客户端的连接已建立事件参数
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        public TcpClientDisconnectedEventArgs(string uid)
        {
            if (string.IsNullOrEmpty(uid))
                throw new ArgumentNullException("tcpClient");

            this.uid = uid;
        }
    }
}
