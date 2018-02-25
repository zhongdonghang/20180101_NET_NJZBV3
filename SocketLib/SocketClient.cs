using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;

namespace SocketLib
{
    public class SocketClient : IDisposable
    {
        /// <summary>
        /// 客户端连接Socket
        /// </summary>
        private Socket clientSocket;
        /// <summary>
        /// 连接状态
        /// </summary>
        private Boolean connected = false;

        public Boolean Connected
        {
            get { return clientSocket.Connected; }
        }
        /// <summary>
        /// 链接是否为常连接
        /// </summary>
        bool isConstantConnect;
        /// <summary>
        /// 连接点
        /// </summary>
        private IPEndPoint hostEndPoint;
        /// <summary>
        /// 连接信号量
        /// </summary>
        private static AutoResetEvent autoConnectEvent = new AutoResetEvent(false);
        /// <summary>
        /// 接受到数据时的委托
        /// </summary>
        /// <param name="info"></param>
        public delegate void ReceiveMsgHandler(byte[] info);
        /// <summary>
        /// 接收到数据时调用的事件
        /// </summary>
        public event ReceiveMsgHandler OnMsgReceived;
        /// <summary>
        /// 发送信息完成的委托
        /// </summary>
        /// <param name="successorfalse"></param>
        public delegate void SendCompleted(bool successorfalse);
        /// <summary>
        /// 发送信息完成的事件
        /// </summary>
        public event SendCompleted OnSended;
        /// <summary>
        /// 监听接收的SocketAsyncEventArgs
        /// </summary>
        private SocketAsyncEventArgs listenerSocketAsyncEventArgs;
        private RequestHandler handle = new RequestHandler();

        /// <summary>
        /// 与服务器的连接已建立事件
        /// </summary>
        public event EventHandler<TcpServerConnectedEventArgs> OnServerConnected;
        /// <summary>
        /// 与服务器的连接已断开事件
        /// </summary>
        public event EventHandler<TcpServerDisconnectedEventArgs> OnServerDisconnected;
        /// <summary>
        /// 与服务器的连接发生异常事件
        /// </summary>
        public event EventHandler<TcpServerExceptionOccurredEventArgs> OnServerExceptionOccurred;
        IPAddress[] addressList;
        /// <summary>
        /// 心跳包计时器
        /// </summary>
        System.Timers.Timer heartbeatTimer = null;
        /// <summary>
        /// 心跳包次数
        /// </summary>
        private int heartbeatcount = 0;

        public IPAddress[] AddressList
        {
            get { return addressList; }
        }
        private int port;

        public int Port
        {
            get { return port; }
        }
        private string clientNo = "";
        public string ClientNo
        {
            get { return clientNo; }
            set { clientNo = value; }
        }



        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteEP">远端服务器终结点</param>
        public SocketClient(IPEndPoint remoteEP)
            : this(new[] { remoteEP.Address }, remoteEP.Port)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteEP">远端服务器终结点</param>
        /// <param name="localEP">本地客户端终结点</param>
        public SocketClient(IPEndPoint remoteEP, IPEndPoint localEP)
            : this(new[] { remoteEP.Address }, remoteEP.Port, localEP)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteIPAddress">远端服务器IP地址</param>
        /// <param name="remotePort">远端服务器端口</param>
        public SocketClient(IPAddress remoteIPAddress, int remotePort)
            : this(new[] { remoteIPAddress }, remotePort)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteIPAddress">远端服务器IP地址</param>
        /// <param name="remotePort">远端服务器端口</param>
        /// <param name="localEP">本地客户端终结点</param>
        public SocketClient(
          IPAddress remoteIPAddress, int remotePort, IPEndPoint localEP)
            : this(new[] { remoteIPAddress }, remotePort, localEP)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteHostName">远端服务器主机名</param>
        /// <param name="remotePort">远端服务器端口</param>
        public SocketClient(string remoteHostName, int remotePort)
            : this(Dns.GetHostAddresses(remoteHostName), remotePort)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteHostName">远端服务器主机名</param>
        /// <param name="remotePort">远端服务器端口</param>
        /// <param name="localEP">本地客户端终结点</param>
        public SocketClient(
          string remoteHostName, int remotePort, IPEndPoint localEP)
            : this(Dns.GetHostAddresses(remoteHostName), remotePort, localEP)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteIPAddresses">远端服务器IP地址列表</param>
        /// <param name="remotePort">远端服务器端口</param>
        public SocketClient(IPAddress[] remoteIPAddresses, int remotePort)
            : this(remoteIPAddresses, remotePort, null)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteIPAddresses">远端服务器IP地址列表</param>
        /// <param name="remotePort">远端服务器端口</param>
        /// <param name="localEP">本地客户端终结点</param>
        public SocketClient(IPAddress[] remoteIPAddresses, int remotePort, IPEndPoint localEP)
        {
            this.addressList = remoteIPAddresses;
            this.hostEndPoint = new IPEndPoint(addressList[addressList.Length - 1], remotePort);
            this.port = remotePort;

            this.heartbeatTimer = new System.Timers.Timer();
            this.heartbeatTimer.Interval = 40000;//心跳包40s发送一次。
            this.heartbeatTimer.Elapsed += new System.Timers.ElapsedEventHandler(heartbeatTimer_Elapsed);
        }

        void heartbeatTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            heartbeatTimer.Stop();
            SocketMsgData.SocketMsgBase heardMsg = new SocketMsgData.SocketMsgBase();
            heardMsg.MsgType = SocketMsgData.TcpMsgDataType.Heartbeat;
            heardMsg.Sender = ClientNo;
            byte[] b = SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(heardMsg);
            try
            {
                heartbeatcount++;
                Send(b);
                Console.WriteLine("发送心跳包...");
                if (heartbeatcount>=1000)
                {
                    heartbeatcount = 0;
                    Disconnect();
                    this.connected = false;
                    if (OnServerDisconnected != null)
                    {
                        OnServerDisconnected(this, new TcpServerDisconnectedEventArgs(this.addressList, this.port));
                        SeatManage.SeatManageComm.WriteLog.Write("发送心跳包出现异常，触发断开事件");
                    }
                }
            }
            catch (Exception ex)
            {
                //if (OnServerDisconnected != null)
                //{
                //    OnServerDisconnected(this, new TcpServerDisconnectedEventArgs(this.addressList, this.port));
                //}
                //Connect();
                this.connected = false;
                if (OnServerDisconnected != null)
                {
                    OnServerDisconnected(this, new TcpServerDisconnectedEventArgs(this.addressList, this.port));
                    SeatManage.SeatManageComm.WriteLog.Write("发送心跳包出现异常，触发断开事件");
                }
                else
                {
                    SeatManage.SeatManageComm.WriteLog.Write("发送心跳包出现异常，断开事件为null");
                }
            }
            finally
            {
                heartbeatTimer.Start();
            }
        }
        ///// <summary>
        ///// 初始化客户端
        ///// </summary>
        ///// <param name="hostName">服务端地址{IP地址}</param>
        ///// <param name="port">端口号</param>
        //public SocketClient(String hostName, Int32 port)
        //{
        //    try
        //    {
        //        IPHostEntry host = Dns.GetHostEntry(hostName);
        //        addressList = host.AddressList;
        //        // this.clientSocket = new Socket(this.hostEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 连接服务端,默认为非常连接
        /// </summary>
        public bool Connect()
        {
            try
            {
                return Connect(false);
            }
            catch
            {
                throw;
            }

        }
        private readonly object locker = new object();  
        /// <summary>
        /// 连接。。
        /// </summary>
        /// <param name="constantConnection">值为true链接为常连接，为false则是短连接。</param>
        /// <returns></returns>
        public bool Connect(bool constantConnection)
        {
            try
            {
                isConstantConnect = constantConnection;//链接方式保存
                lock (locker)
                {
                    this.clientSocket = new Socket(this.hostEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    if (!this.connected)
                    {
                        SocketAsyncEventArgs connectArgs = new SocketAsyncEventArgs();
                        connectArgs.UserToken = this.clientSocket;
                        connectArgs.RemoteEndPoint = this.hostEndPoint;
                        connectArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnConnect);
                        clientSocket.ConnectAsync(connectArgs);

                        //等待连接结果
                        autoConnectEvent.WaitOne();
                        SocketError errorCode = connectArgs.SocketError;
                        if (errorCode == SocketError.Success)
                        {
                            listenerSocketAsyncEventArgs = new SocketAsyncEventArgs();
                            byte[] receiveBuffer = new byte[32768];
                            listenerSocketAsyncEventArgs.UserToken = clientSocket;
                            listenerSocketAsyncEventArgs.SetBuffer(receiveBuffer, 0, receiveBuffer.Length);
                            listenerSocketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceive);
                            (listenerSocketAsyncEventArgs.UserToken as Socket).ReceiveAsync(listenerSocketAsyncEventArgs);//开始侦听接收
                            if (constantConnection)//如果是常连接，启动心跳包
                            {
                                heartbeatTimer.Start();//启动心跳包。
                            }
                            if (OnServerConnected != null)
                            {
                                OnServerConnected(this, new TcpServerConnectedEventArgs(AddressList, Port));
                            }
                            return true;
                        }
                        else
                            this.clientSocket.Close(500);
                        throw new SocketException((Int32)errorCode);
                    }
                    return this.connected;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="message"></param>
        public void Send(byte[] message)
        {
            try
            {
                byte[] arrayLength = BitConverter.GetBytes(message.Length);
                List<byte> listByte = new List<byte>(arrayLength.Length + message.Length);
                listByte.AddRange(arrayLength);
                listByte.AddRange(message);
                Byte[] sendBuffer = listByte.ToArray();
                SendHandler(sendBuffer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        private void SendHandler(byte[] sendBuffer)
        {
            if (this.connected)
            {
                SocketAsyncEventArgs senderSocketAsyncEventArgs = new SocketAsyncEventArgs();
                senderSocketAsyncEventArgs.UserToken = this.clientSocket;
                senderSocketAsyncEventArgs.SetBuffer(sendBuffer, 0, sendBuffer.Length);
                senderSocketAsyncEventArgs.RemoteEndPoint = this.hostEndPoint;
                senderSocketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSend);
                lock (this.clientSocket)
                {
                    clientSocket.SendAsync(senderSocketAsyncEventArgs);
                }
            }
            else
            {
                heartbeatTimer.Stop();
                throw new SocketException((Int32)SocketError.NotConnected);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            if (this.connected)
            {
                lock (this.clientSocket)
                {
                    if (isConstantConnect)
                    {
                        heartbeatTimer.Stop();
                    }
                    this.connected = false;
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }
            // clientSocket.Close();
            //clientSocket.Disconnect(true);
        }


        /// <summary>
        /// 连接的完成方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnect(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                autoConnectEvent.Set();
                this.connected = (e.SocketError == SocketError.Success);
            }
            catch (Exception ex)
            {
                Console.WriteLine("连接失败：" + ex.Message);
            }
        }
        static object obj = new object();

        /// <summary>
        /// 接收的完成方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceive(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
                {
                    byte[] received = new byte[e.BytesTransferred];
                    Buffer.BlockCopy(e.Buffer, e.Offset, received, 0, e.BytesTransferred);
                    lock (obj)
                    {
                        List<byte[]> msg = handle.GetActualObject(received);
                        //Listen(); 
                        foreach (byte[] m in msg)
                        {
                            if (OnMsgReceived != null)
                            {
                                OnMsgReceived(m);
                            }
                        }
                    }
                    lock (this.clientSocket)
                    {
                        (listenerSocketAsyncEventArgs.UserToken as Socket).ReceiveAsync(listenerSocketAsyncEventArgs);//开始侦听接收
                    }
                }
                else
                {
                    this.connected = false;
                    this.heartbeatTimer.Stop();
                    if (OnServerDisconnected != null)
                    {
                        OnServerDisconnected(this, new TcpServerDisconnectedEventArgs(this.addressList, this.port));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("接收失败：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 发送的完成方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSend(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {//发送成功
                if (OnSended != null)
                {
                    OnSended(true);
                }
            }
            else
            {//发送失败
                if (OnSended != null)
                {
                    OnSended(false);
                }
                heartbeatTimer.Stop();
                this.ProcessError(e);
            }
        }
        /// <summary>
        /// 处理错误
        /// </summary>
        /// <param name="e"></param>
        private void ProcessError(SocketAsyncEventArgs e)
        {
            Socket s = e.UserToken as Socket;
            if (s.Connected)
            {
                try
                {
                    s.Shutdown(SocketShutdown.Both);
                }
                catch (Exception)
                {
                    //client already closed
                }
                finally
                {
                    if (s.Connected)
                    {
                        s.Close();
                    }
                }
            }
            throw new SocketException((Int32)e.SocketError);
        }

        #region IDisposable Members
        public void Dispose()
        {
            Disconnect();
            heartbeatTimer.Dispose();

            // autoConnectEvent.Close();
            //if (this.clientSocket.Connected)
            //{
            //    this.clientSocket.Close(500);
            //}
        }
        #endregion
    }


    /// <summary>
    /// 与服务器的连接已建立事件参数
    /// </summary>
    public class TcpServerConnectedEventArgs : EventArgs
    {
        /// <summary>
        /// 与服务器的连接已建立事件参数
        /// </summary>
        /// <param name="ipAddresses">服务器IP地址列表</param>
        /// <param name="port">服务器端口</param>
        public TcpServerConnectedEventArgs(IPAddress[] ipAddresses, int port)
        {
            if (ipAddresses == null)
                throw new ArgumentNullException("ipAddresses");

            this.Addresses = ipAddresses;
            this.Port = port;
        }

        /// <summary>
        /// 服务器IP地址列表
        /// </summary>
        public IPAddress[] Addresses { get; private set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string s = string.Empty;
            foreach (var item in Addresses)
            {
                s = s + item.ToString() + ',';
            }
            s = s.TrimEnd(',');
            s = s + ":" + Port.ToString(CultureInfo.InvariantCulture);

            return s;
        }
    }

    /// <summary>
    /// 与服务器的连接已断开事件参数
    /// </summary>
    public class TcpServerDisconnectedEventArgs : EventArgs
    {
        /// <summary>
        /// 与服务器的连接已断开事件参数
        /// </summary>
        /// <param name="ipAddresses">服务器IP地址列表</param>
        /// <param name="port">服务器端口</param>
        public TcpServerDisconnectedEventArgs(IPAddress[] ipAddresses, int port)
        {
            if (ipAddresses == null)
                throw new ArgumentNullException("ipAddresses");

            this.Addresses = ipAddresses;
            this.Port = port;
        }

        /// <summary>
        /// 服务器IP地址列表
        /// </summary>
        public IPAddress[] Addresses { get; private set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string s = string.Empty;
            foreach (var item in Addresses)
            {
                s = s + item.ToString() + ',';
            }
            s = s.TrimEnd(',');
            s = s + ":" + Port.ToString(CultureInfo.InvariantCulture);

            return s;
        }
    }

    /// <summary>
    /// 与服务器的连接发生异常事件参数
    /// </summary>
    public class TcpServerExceptionOccurredEventArgs : EventArgs
    {
        /// <summary>
        /// 与服务器的连接发生异常事件参数
        /// </summary>
        /// <param name="ipAddresses">服务器IP地址列表</param>
        /// <param name="port">服务器端口</param>
        /// <param name="innerException">内部异常</param>
        public TcpServerExceptionOccurredEventArgs(
          IPAddress[] ipAddresses, int port, Exception innerException)
        {
            if (ipAddresses == null)
                throw new ArgumentNullException("ipAddresses");

            this.Addresses = ipAddresses;
            this.Port = port;
            this.Exception = innerException;
        }

        /// <summary>
        /// 服务器IP地址列表
        /// </summary>
        public IPAddress[] Addresses { get; private set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// 内部异常
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string s = string.Empty;
            foreach (var item in Addresses)
            {
                s = s + item.ToString() + ',';
            }
            s = s.TrimEnd(',');
            s = s + ":" + Port.ToString(CultureInfo.InvariantCulture);

            return s;
        }
    }
}