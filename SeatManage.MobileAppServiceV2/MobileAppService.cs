using System;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using SocketLib;
using SocketMsgData;

namespace SeatManage.MobileAppService
{
    /// <summary>
    /// 移动客户端app数据访问服务。
    /// 执行在学校服务器上，用于响应公司外网服务器上服务的请求。
    /// </summary>
    public class MobileAppService : IService.IService
    {
        public override string ToString()
        {
            return "座位管理系统微信客户端访问服务";
        }

        SocketClient client;
        ServerIp ip = new ServerIp(AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["AppServiceEndpointAddress"].ConnectionString));
        string schoolNum = ConfigurationManager.AppSettings["SchoolNo"];
        Thread connectThread;
        public void Start()
        {
            client = new SocketClient(ip.IP, ip.Port);
            client.ClientNo = schoolNum;
            client.OnMsgReceived += client_OnMsgReceived;
            //client.OnSended += new SocketLib.SocketClient.SendCompleted(client_OnSended);
            client.OnServerConnected += client_OnServerConnected;
            client.OnServerDisconnected += client_OnServerDisconnected;
            connectThread = new Thread(Connect);
            connectThread.Start();
        }

        private void client_OnServerDisconnected(object sender, TcpServerDisconnectedEventArgs e)
        {//与服务器连接出现断开，则重新链接
            Connect();
        }

        private void client_OnServerConnected(object sender, TcpServerConnectedEventArgs e)
        {
            //链接成功，向服务器发送学校编号。
            SocketMsgBase baseMsg = new SocketMsgBase();
            baseMsg.LinkType = "1";
            baseMsg.MsgType = TcpMsgDataType.ClientToken;
            baseMsg.Sender = schoolNum;
            baseMsg.SubSystem = TcpSeatManageSubSystem.SocketClient;
            client.Send(ByteSerializer.ObjectToByte(baseMsg));
        }





        public void Stop()
        {
            client.Disconnect();
            client.Dispose();
        }

        public void Dispose()
        {
            Stop();
        }

        public void Connect()
        {
            try
            {
                Console.WriteLine("正在连接到服务器……");
                client.Connect(true);
                Console.WriteLine("连接成功");
                WriteLog.Write("已与服务器连接，等待终端请求。");
            }
            catch (SocketException ex)
            {
                Console.WriteLine("连接失败，10s后重连…");
                Thread.Sleep(10000);
                Connect();
            }
        }
        /// <summary>
        /// 接收到请求的处理
        /// </summary>
        /// <param name="info"></param>
        private void client_OnMsgReceived(byte[] info)
        {
            SocketRequest requestMsg = ByteSerializer.DeserializeByte<SocketMsgBase>(info) as SocketRequest;
            if (requestMsg != null)
            {
                Console.WriteLine("子系统{0}请求执行{1}方法", requestMsg.SubSystem, requestMsg.RequestMethod);
                SocketResponse response = new SocketResponse();
                string result = string.Empty;
                try
                {
                    result = OperationFactory.Execute(requestMsg.Parameters, requestMsg.RequestMethodType);
                    response.Result = result;
                }
                catch (Exception ex)
                {
                    response.ErrorMsg = "执行遇到异常";
                }
                response.MethodName = requestMsg.MethodName;
                response.Sender = requestMsg.Target;
                response.SubSystem = requestMsg.SubSystem;
                response.Target = requestMsg.Sender;
                response.MsgType = TcpMsgDataType.Relay;

                client.Send(ByteSerializer.ObjectToByte(response));
                Console.WriteLine("执行成功，已返回结果");
            }
        }
    }
}
