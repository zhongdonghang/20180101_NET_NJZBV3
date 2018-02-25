using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Configuration;
using IWCFService.TransportService;
using AdvertManage.IWCFService;
using System.ServiceModel.Description;

namespace AdvertManage.WcfAccessProxy
{
    public class AMS_ServiceProxy
    {
        private static string readerOperateEndpointAddress;// = ConfigurationManager.ConnectionStrings["EndpointAddress"].ConnectionString;
        static ChannelFactory<IAdvertManageService> advertManageServiceProxy = null;
        public static IAdvertManageService CreateChannelAdvertManageService()
        {
            if (advertManageServiceProxy == null)
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
                binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                binding.MaxReceivedMessageSize = int.MaxValue;
                binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                string endPointAddress = readerOperateEndpointAddress + "AdvertManageService/";
                 advertManageServiceProxy = new ChannelFactory<IAdvertManageService>(binding, new EndpointAddress(endPointAddress));
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
            IAdvertManageService obj = advertManageServiceProxy.CreateChannel(); 
            return obj;
        }

        public static IAdvertManageService CreateChannelAdvertManageService(string readerOperateEndpointAddress)
        {
            NetTcpBinding binding = new NetTcpBinding();
            
            binding.Security.Mode = SecurityMode.None;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue; 
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue; 
            string endPointAddress = readerOperateEndpointAddress + "AdvertManageService/"; 
            ChannelFactory<IAdvertManageService> proxy = new ChannelFactory<IAdvertManageService>(binding, new EndpointAddress(endPointAddress));
            foreach (OperationDescription op in proxy.Endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
            IAdvertManageService obj = proxy.CreateChannel();
            return obj;
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
