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
        SeatManage.ClassModel.ServerIp ip = new SeatManage.ClassModel.ServerIp(ConfigurationManager.ConnectionStrings["ip"].ConnectionString);
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
        public string CancelBespeakLog(int bespeakId, string remark)
        {
            //初始化请求包
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.CancelBespeakLog;
            request.Parameters.Add(bespeakId);
            request.Parameters.Add(remark); 
            return CallService(request);

        }

        public string ChangeSeat(string cardNo, string seatNo, string readingRoom)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.ChangeSeat;
            request.Parameters.Add(cardNo);
            request.Parameters.Add(seatNo);
            request.Parameters.Add(readingRoom); 
            return CallService(request);
            
        }

        public string FreeSeat(string cardNo)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.FreeSeat;
            request.Parameters.Add(cardNo); 
            return CallService(request);
            
        }

        public string GetAllRoomSeatUsedInfo()
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetAllRoomSeatUsedInfo; 
            return CallService(request);
            
        }

        public string GetOpenBespeakRooms(string strDate)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetOpenBespeakRooms;
            request.Parameters.Add(strDate); 
            return CallService(request);
        }

        public string GetReaderActualTimeRecord(string cardNum, string getItemsParameter)
        {

            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetReaderActualTimeRecord;
            request.Parameters.Add(cardNum);
            request.Parameters.Add(getItemsParameter); 
            return CallService(request);
             
        }

        public string GetReaderBespeakRecord(string cardNum, int pageIndex, int pageSize)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetReaderBespeakRecord;
            request.Parameters.Add(cardNum);
            request.Parameters.Add(pageIndex);
            request.Parameters.Add(pageSize); 
            return CallService(request);
             
        }

        public string GetReaderBlacklistRecord(string cardNum, int pageIndex, int pageSize)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetReaderBlacklistRecord;
            request.Parameters.Add(cardNum);
            request.Parameters.Add(pageIndex);
            request.Parameters.Add(pageSize); 
            return CallService(request);
            
        }

        public string GetReaderChooseSeatRecord(string cardNum, int pageIndex, int pageSize)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetReaderChooseSeatRecord;
            request.Parameters.Add(cardNum);
            request.Parameters.Add(pageIndex);
            request.Parameters.Add(pageSize); 
            return CallService(request);
             
        }

        public string GetReaderAccount(string cardNum, string password)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetReaderAccount;
            request.Parameters.Add(cardNum);
            request.Parameters.Add(password); 
            return CallService(request);
            
        }

        public string GetScanCodeSeatInfo(string scanResult, string cardNo)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetScanCodeSeatInfo;
            request.Parameters.Add(scanResult);
            request.Parameters.Add(cardNo); 
            return CallService(request);
             
        }

        public string GetSeatsBespeakInfoByRoomNum(string roomNum, string date)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetSeatsBespeakInfoByRoomNum;
            request.Parameters.Add(roomNum);
            request.Parameters.Add(date); 
            return CallService(request);
        }

        public string GetViolateDiscipline(string cardNum, int pageIndex, int pageSize)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetViolateDiscipline;
            request.Parameters.Add(cardNum);
            request.Parameters.Add(pageIndex);
            request.Parameters.Add(pageSize); 
            return CallService(request);
        }

        public string ShortLeave(string cardNo)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.ShortLeave;
            request.Parameters.Add(cardNo); 
            
            return CallService(request);
        }

        public string SubmitBespeakInfo(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.SubmitBespeakInfo;
            request.Parameters.Add(cardNo);
            request.Parameters.Add(roomNum);
            request.Parameters.Add(seatNum);
            request.Parameters.Add(bespeakDatetime);
            request.Parameters.Add(remark); 
            return CallService(request);
        }

        public string SubmitBespeakInfoCustomTime(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.SubmitBespeakInfoCustomTime;
            request.Parameters.Add(cardNo);
            request.Parameters.Add(roomNum);
            request.Parameters.Add(seatNum);
            request.Parameters.Add(bespeakDatetime);
            request.Parameters.Add(remark);
            
            return CallService(request);
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
                    Console.WriteLine("请求客户端{0}执行{1}方法已发送，等待返回结果…", request.Target, request.RequestMethod.ToString());
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
        /// 获取读者的消息提醒
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public string GetReaderNotice(string cardNum, int pageIndex, int pageSize)
        {
            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetReaderNotice;
            request.Parameters.Add(cardNum);
            request.Parameters.Add(pageIndex);
            request.Parameters.Add(pageSize); 

            return CallService(request);
        }


        public string GetReaderSeatState(string cardNum)
        {

            request = new SocketRequest();
            request.RequestMethod = RequestMethodEnum.GetReaderSeatState;
            request.Parameters.Add(cardNum);
            return CallService(request);
        }
    }
}
