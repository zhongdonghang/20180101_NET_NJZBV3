using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using AMS.IBllService;
using System.Configuration;
using System.ServiceModel.Description;
using IWCFService.TransportService;

namespace AMS.ServiceConnectChannel
{
    public class AdvertManageBllServiceChannel
    { 
        private static string readerOperateEndpointAddress;// = ConfigurationManager.ConnectionStrings["EndpointAddress"].ConnectionString;
        static ChannelFactory<IAdvertManageBllService> advertManageServiceProxy = null;
        public static IAdvertManageBllService CreateServiceChannel()
        {
            try
            {
                if (advertManageServiceProxy == null)
                {
                    if (ConfigurationManager.ConnectionStrings["AdvertServiceEndpointAddress"] != null)
                    {
                        readerOperateEndpointAddress = SeatManage.SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["AdvertServiceEndpointAddress"].ConnectionString);
                        SeatManage.SeatManageComm.WriteLog.Write("链接字符串"+readerOperateEndpointAddress);
                    }
                    else
                    {
                        // throw new Exception("未设置远程服务连接字符串");
                    }

                    NetTcpBinding binding = new NetTcpBinding();
                    binding.Security.Mode = SecurityMode.None;
                    binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
                    binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                    binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                    binding.MaxReceivedMessageSize = int.MaxValue;
                    binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                    string endPointAddress = readerOperateEndpointAddress + "AdvertManageBllService/";
                    advertManageServiceProxy = new ChannelFactory<IAdvertManageBllService>(binding, new EndpointAddress(endPointAddress));
                    #region 设置ListItem最大值
                    foreach (OperationDescription op in advertManageServiceProxy.Endpoint.Contract.Operations)
                    {
                        DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                        if (dataContractBehavior != null)
                        {
                            dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                        }
                    }
                    #endregion

                }
                IAdvertManageBllService obj = advertManageServiceProxy.CreateChannel();
                return obj;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.ToString());
                throw ex;
            }
        }
        /// <summary>
        /// 构造文件传输的服务地址
        /// </summary>
        /// <returns></returns>
        public static IFileTransportService CreateChannelFileTransportService()
        {
            if (ConfigurationManager.ConnectionStrings["AdvertServiceEndpointAddress"] != null)
            {
                readerOperateEndpointAddress = SeatManage.SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["AdvertServiceEndpointAddress"].ConnectionString);
            }
            else
            {
                // throw new Exception("未设置远程服务连接字符串");
            }

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            string endPointAddress = readerOperateEndpointAddress + "TransportService/";
            ChannelFactory<IFileTransportService> proxy = new ChannelFactory<IFileTransportService>(binding, new EndpointAddress(endPointAddress));
            IFileTransportService obj = proxy.CreateChannel();
            return obj;
        }
    }
}
