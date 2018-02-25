using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace SocketLib
{
    /// <summary>
    /// 简单的Socket客户端连接，
    /// 可以进行连接可发送消息
    /// </summary>
    public class SimpleSocketClient
    {
        SocketLib.SocketClient client;
        private readonly object locker = new object(); 
        private static AutoResetEvent autoConnectEvent = new AutoResetEvent(false);
        public SimpleSocketClient(string ip, int port)
        {
            client = new SocketLib.SocketClient(ip, port);
            client.OnServerConnected += new EventHandler<SocketLib.TcpServerConnectedEventArgs>(client_OnServerConnected);
        }
        void client_OnServerConnected(object sender, SocketLib.TcpServerConnectedEventArgs e)
        {
            Thread.Sleep(5000);
            client.Send(byteDatas);
            autoConnectEvent.Set();
        }
        byte[] byteDatas;
        public void Send(object data)
        {
            try
            {
                //BinaryFormatter formatter = new BinaryFormatter();
                //MemoryStream rems = new MemoryStream();
                //formatter.Serialize(rems, data);
                byteDatas = SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(data);
                //byteDatas =  data as byte[];
                client.Connect();
                autoConnectEvent.WaitOne(2000);
                client.Disconnect();
            }
            catch
            {
                try
                {
                    Thread.Sleep(5000);
                    client.Connect();
                    autoConnectEvent.WaitOne(2000);
                    client.Disconnect();
                }
                catch
                {
                    return;
                }

            }

        }
    }
}
