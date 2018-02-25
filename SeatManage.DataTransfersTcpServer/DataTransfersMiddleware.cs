using System;
using System.Collections.Generic;
using SeatManage.SeatManageComm;
using SocketLib;
using SocketMsgData;

namespace SeatManage.Middleware
{
    /// <summary>
    /// 用于定义学校连接或者断开链接的事件
    /// </summary>
    /// <param name="schoolNum"></param>
    /// <param name="ip"></param>
    public delegate void SocketConnectionHandler(string schoolNum, string ip);
    /// <summary>
    /// 定义接收到消息的处理事件
    /// </summary>
    /// <param name="msg"></param>
    public delegate void SocketMsgHandler(SocketMsgBase msg);
    /// <summary>
    /// 启动Socket服务，用于客户端链接，以及转发不同客户端直接通信的中间件。
    /// </summary>
    public class DataTransfersMiddleware : IService.IService
    {
        SocketListener server;
        Dictionary<string, string> schoolNums = new Dictionary<string, string>();
        /// <summary>
        /// 客户端连接（包括学校）
        /// </summary>
        public event EventHandler OnClientConnectioned;
        /// <summary>
        /// 学校已连接
        /// </summary>
        public event SocketConnectionHandler OnSchoolConnectionHandler;
        /// <summary>
        /// 学校断开连接
        /// </summary>
        public event SocketConnectionHandler OnSchoolDisConnectionHandler;
        /// <summary>
        /// 接收到消息
        /// </summary>
        public event SocketMsgHandler OnReceivedMsg;

        public void Start()
        {
            try
            {
                server = new SocketListener(20000, 16384);//最大并发量35000，缓冲区16kb
                server.GetKeepAliveLink = GetSchoolInfo;
                server.OnClientConnected += server_OnClientConnected;
                server.OnClientDisconnected += server_OnClientDisconnected;
                server.OnMsgReceived += new SocketListener.ReceiveMsgHandler(server_OnMsgReceived);

                server.Init();
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("socket服务初始化失败：{0},异常来自：{1}", ex.Message, ex.Source));
            }

            server.Start(12306);
        }

        public void Stop()
        {
            server.Stop();
            schoolNums.Clear();
            server = null;
        }

        public void Dispose()
        {
            Stop();
        }

        public Dictionary<string, string> GetSchoolInfo()
        {
            return schoolNums;
        }
        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void server_OnClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            SocketListener s = sender as SocketListener;
            Console.WriteLine("{0:M} {1:t}：有新的连接，当前连接数{2}，终结点：{3}", DateTime.Now, DateTime.Now, s.NumConnections, e.Uid);
        }
        void server_OnMsgReceived(string uid, SocketMsgBase e)
        {
            try
            {
                SocketMsgBase msg = e;
                if (msg.SubSystem == TcpSeatManageSubSystem.SocketClient && msg.MsgType == TcpMsgDataType.ClientToken)
                {
                    if (schoolNums.ContainsKey(msg.Sender))
                    {
                        schoolNums[msg.Sender] = uid;
                    }
                    else
                    {
                        schoolNums.Add(msg.Sender, uid);
                    }
                    Console.WriteLine("{0:M} {1:t}：学校{2}已建立连接", DateTime.Now, DateTime.Now, msg.Sender);
                    // SeatManage.SeatManageComm.WriteLog.Write(schoolLinkLog, string.Format("学校{0}已连接,Ip地址：{1}", msg.Sender, uid));
                    if (OnSchoolConnectionHandler != null)
                    {
                        OnSchoolConnectionHandler(msg.Sender, uid);
                    }
                }
                else
                {
                    if (OnReceivedMsg != null)
                    {
                        OnReceivedMsg(msg);
                    }
                    switch (msg.MsgType)
                    {
                        case TcpMsgDataType.Relay:
                            RelaySocketMsg(uid, msg);
                            break;
                        case TcpMsgDataType.MsgPush:
                            PubshMsg(msg);
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("消息处理失败：{0},异常来自：{1}", ex.Message, ex.Source));
            }
        }
        /// <summary>
        /// 转发socket消息 ，
        /// </summary>
        /// <param name="uid">请求的目标ID。</param>
        /// <param name="msg">如果msg类型为Request，即客户端请求学校，需要转发给学校。否则为学校响应客户端的请求。</param>
        private void RelaySocketMsg(string uid, SocketMsgBase msg)
        {
            try
            {
                msg.Sender = uid;
                if (schoolNums.ContainsKey(msg.Target))
                {
                    server.Send(schoolNums[msg.Target], ByteSerializer.ObjectToByte(msg));
                    Console.WriteLine("{0:M} {1:t}：转发给目标{2},地址为：{3}", DateTime.Now, DateTime.Now, msg.Target, schoolNums[msg.Target]);
                }
                else
                {
                    if (msg is SocketRequest)
                    {
                        Console.WriteLine("{0:M} {1:t}：请求的目标{2}不存在", DateTime.Now, DateTime.Now, msg.Target);
                        SocketResponse response = new SocketResponse();
                        response.SubSystem = msg.SubSystem;
                        response.MethodName = msg.MethodName;
                        response.Sender = response.Target;
                        response.Target = response.Sender;
                        response.ErrorMsg = "学校没有连接";
                        server.Send(uid, ByteSerializer.ObjectToByte(response));
                    }
                    else if (msg is SocketResponse)
                    {
                        // Console.WriteLine("消息回复给{0}", msg.Target);
                        Console.WriteLine("{0:M} {1:t}：消息回复给{2}", DateTime.Now, DateTime.Now, msg.Target);
                        server.Send(msg.Target, ByteSerializer.ObjectToByte(msg));
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("RelaySocketMsg执行遇到异常：{0},异常来自：{1}", ex.Message, ex.Source));
            }
        }

        void server_OnClientDisconnected(object sender, TcpClientDisconnectedEventArgs e)
        {
            try
            {
                foreach (string k in schoolNums.Keys)
                {
                    if (schoolNums[k] == e.Uid)
                    {
                        lock (schoolNums)
                        {
                            schoolNums.Remove(k);
                            //SeatManage.SeatManageComm.WriteLog.Write(schoolLinkLog, string.Format("学校{0}已断开连接，Ip地址：{0}", k, e.Uid));
                            if (OnSchoolDisConnectionHandler != null)
                            {
                                OnSchoolDisConnectionHandler(k, e.Uid);
                            }
                        }
                        break;
                    }
                }
                SocketListener s = sender as SocketListener;
                Console.WriteLine("{0:M} {1:t}：客户端断开连接，当前连接数{2}", DateTime.Now, DateTime.Now, s.NumConnections);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("断开连接处理遇到异常：{0}", ex.Message));
            }
        }

        private AppMessagePush.AppServiceMsgPush appPush = new AppMessagePush.AppServiceMsgPush();
        /// <summary>
        /// 推送消息处理
        /// </summary>
        /// <param name="msg"></param>
        private void PubshMsg(SocketMsgBase msg)
        {
            try
            {
                if (msg is SocketRequest)
                {
                    SocketRequest srMsg = msg as SocketRequest;
                    appPush.MsgPush((string) srMsg.Parameters[0]);
                    Console.WriteLine("{0:M} {1:t}：推送用户消息：{2}", DateTime.Now, DateTime.Now, (string)srMsg.Parameters[0]);
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("PubshMsg执行遇到异常：{0},异常来自：{1}", ex.Message, ex.Source));
            }
        }
    }
}
