using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using SocketMsgData;
using SeatManage.ClassModel;
using WeiXinJK;
namespace AMS.TcpTransfersServer
{
    /// <summary>
    /// socket 业务逻辑服务,需要在appconfig 中添加节点：<add key="weiXinMsgSendUrl" value=""/>,并通过post请求
    /// </summary>
    public class TcpTransfersServer : IService.IService
    {
        SocketLib.SocketListener server;
        Dictionary<string, string> schoolNums = new Dictionary<string, string>();
        public TcpTransfersServer()
        {
            
        }

        static string schoolLinkLog = "学校连接日志";
        void server_OnMsgReceived(string uid, SocketMsgData.SocketMsgBase e)
        {
            try
            {
                SocketMsgData.SocketMsgBase msg = e;
                if (msg.SubSystem == SocketMsgData.TcpSeatManageSubSystem.SocketClient && msg.MsgType == SocketMsgData.TcpMsgDataType.ClientToken)
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
                    SeatManage.SeatManageComm.WriteLog.Write(schoolLinkLog, string.Format("学校{0}已连接,Ip地址：{1}", msg.Sender, uid));
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
                        case TcpMsgDataType.WeiXinNotice:
                            SocketMsgData.SocketRequest request = msg as SocketMsgData.SocketRequest;
                            weixinNoticeSend(request);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("消息处理失败：{0},异常来自：{1}", ex.Message, ex.Source));
            }
        }
        /// <summary>
        /// 微信消息发送接口
        /// </summary>
        /// <param name="msg"></param>
        private void weixinNoticeSend(SocketMsgData.SocketRequest msg)
        {
            try
            {
                Console.WriteLine("推送微信消息"); 
                //Console.WriteLine("{0:M} {1:t}：推送微信消息", DateTime.Now, DateTime.Now );  
                if (msg != null)
                {
                    try
                    {
                        SeatManage.ClassModel.WeiXinBaseMsg weixinMsg = msg.Parameters[0] as SeatManage.ClassModel.WeiXinBaseMsg;
                        string weixinId = AMS.ServiceProxy.WeiXinProxy.GetReaderWeixinIdByCardNo(weixinMsg.CardNum, weixinMsg.SchoolNum);

                        if (string.IsNullOrEmpty(weixinId))//如果微信Id为null，则程序终止
                        {
                            return;
                        }
                        WeiXinJK.Model.WeiXinJsonTxtMessage json = new WeiXinJK.Model.WeiXinJsonTxtMessage(weixinId, weixinMsg.ToString());

                        string weiXinMsgSendUrl = System.Configuration.ConfigurationManager.AppSettings["weiXinMsgSendUrl"];
                        string strjson = json.Getmess(); 
                        Console.WriteLine("{0:M} {1:t}：推送微信消息{2}", DateTime.Now, DateTime.Now,strjson);  
                        SeatManage.SeatManageComm.HttpRequest.Post(weiXinMsgSendUrl, strjson);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("weixinNoticeSend执行失败：{0},异常来自：{1}", ex.Message, ex.Source));
            }
        }
        public Dictionary<string, string> GetSchoolInfo()
        {
            return this.schoolNums;
        }
        /// <summary>
        /// 转发socket消息，即客户端发来的是请求学校服务器的消息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="msg"></param>
        private void RelaySocketMsg(string uid, SocketMsgData.SocketMsgBase msg)
        {
            try
            {
                msg.Sender = uid;
                if (schoolNums.ContainsKey(msg.Target))
                {
                    server.Send(schoolNums[msg.Target], SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(msg));
                    Console.WriteLine("{0:M} {1:t}：转发给目标{2},地址为：{3}", DateTime.Now, DateTime.Now, msg.Target, schoolNums[msg.Target]);  

                }
                else
                {
                    if (msg is SocketRequest)
                    { 
                        Console.WriteLine("{0:M} {1:t}：请求的目标{2}不存在", DateTime.Now, DateTime.Now, msg.Target );  
                        SocketResponse response = new SocketResponse();
                        response.SubSystem = msg.SubSystem;
                        response.MethodName = msg.MethodName;
                        response.Sender = response.Target;
                        response.Target = response.Sender;
                        response.ErrorMsg = "学校没有连接";
                        server.Send(uid, SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(response));
                    }
                    else if (msg is SocketResponse)
                    {
                        Console.WriteLine("消息回复给{0}", msg.Target);
                        Console.WriteLine("{0:M} {1:t}：消息回复给{2}", DateTime.Now, DateTime.Now, msg.Target); 
                        server.Send(msg.Target, SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(msg));
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("RelaySocketMsg执行遇到异常：{0},异常来自：{1}", ex.Message, ex.Source));
            }
        }

        void server_OnClientDisconnected(object sender, SocketLib.TcpClientDisconnectedEventArgs e)
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
                            SeatManage.SeatManageComm.WriteLog.Write(schoolLinkLog, string.Format("学校{0}已断开连接，Ip地址：{0}", k, e.Uid));
                            if (OnSchoolDisConnectionHandler != null)
                            {
                                OnSchoolDisConnectionHandler(k, e.Uid);
                            }
                        }
                        break;
                    }
                }
                SocketLib.SocketListener s = sender as SocketLib.SocketListener; 
                Console.WriteLine("{0:M} {1:t}：客户端断开连接，当前连接数{2}", DateTime.Now, DateTime.Now, s.NumConnections); 
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("断开连接处理遇到异常：{0}",ex.Message));
            }
        }
        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void server_OnClientConnected(object sender, SocketLib.TcpClientConnectedEventArgs e)
        {
            SocketLib.SocketListener s = sender as SocketLib.SocketListener; 
            Console.WriteLine("{0:M} {1:t}：有新的连接，当前连接数{2}，终结点：{3}", DateTime.Now, DateTime.Now, s.NumConnections,e.Uid); 
        }
        public void Start()
        {
            try
            {
                server = new SocketLib.SocketListener(20000, 16384);//最大并发量35000，缓冲区16kb
                server.GetKeepAliveLink = GetSchoolInfo;
                server.OnClientConnected += new EventHandler<SocketLib.TcpClientConnectedEventArgs>(server_OnClientConnected);
                server.OnClientDisconnected += new EventHandler<SocketLib.TcpClientDisconnectedEventArgs>(server_OnClientDisconnected);
                server.OnMsgReceived += new SocketLib.SocketListener.ReceiveMsgHandler(server_OnMsgReceived);
                //server.OnSended += new SocketLib.SocketListener.SendCompletedHandler(server_OnSended);
                server.Init();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("socket服务初始化失败：{0},异常来自：{1}", ex.Message, ex.Source));
            }

            server.Start(10010); 
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
        /// <summary>
        /// 并发数
        /// </summary>
        public int Concurrence
        {
            get
            {
                if (server != null)
                {
                    return server.NumConnections;
                }
                else
                {
                    return 0;
                }
            }
        }
        public override string ToString()
        {
            return "socket服务";
        }
        private int _MaxConcurrence;
        /// <summary>
        /// 最大并发数
        /// </summary>
        public int MaxConcurrence
        {
            get { return _MaxConcurrence; }
        }

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
    }

    public delegate void SocketConnectionHandler(string schoolNum, string ip);
    public delegate void SocketMsgHandler(SocketMsgData.SocketMsgBase msg);
}
