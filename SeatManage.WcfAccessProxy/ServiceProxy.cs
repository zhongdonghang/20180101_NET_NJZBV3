using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ServiceModel;
using SeatManage.IWCFService;
using IWCFService.TransportService;
using System.ServiceModel.Description;
namespace SeatManage.WcfAccessProxy
{
    public class ServiceProxy
    {
        private static string readerOperateEndpointAddress;// = ConfigurationManager.ConnectionStrings["EndpointAddress"].ConnectionString;
        public static ISeatManageService CreateChannelSeatManageService()
        {
            if (ConfigurationManager.ConnectionStrings["EndpointAddress"] != null)
            {
                readerOperateEndpointAddress = SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["EndpointAddress"].ConnectionString);
            }
            else
            {
                 throw new Exception("未设置远程服务连接字符串");
            }
            //Random r = new Random();

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.CloseTimeout = new TimeSpan(0, 10, 0);
            string endPointAddress = readerOperateEndpointAddress + "SeatManageDateService/";

            //string endPointAddress = readerOperateEndpointAddress + "SeatManageDateService_"+r.Next(1,4)+"/";

            ChannelFactory<ISeatManageService> proxy = new ChannelFactory<ISeatManageService>(binding, new EndpointAddress(endPointAddress));
            foreach (OperationDescription op in proxy.Endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
            ISeatManageService obj = proxy.CreateChannel();
            
            return obj;
        }

        public static ISeatManageService CreateChannelSeatManageService(string readerOperateEndpointAddress)
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.CloseTimeout = new TimeSpan(0, 5, 0);
            binding.OpenTimeout = new TimeSpan(0, 5, 0);

            string endPointAddress = readerOperateEndpointAddress + "SeatManageDateService/";
            ChannelFactory<ISeatManageService> proxy = new ChannelFactory<ISeatManageService>(binding, new EndpointAddress(endPointAddress));
            foreach (OperationDescription op in proxy.Endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
            ISeatManageService obj = proxy.CreateChannel();
            return obj;
        }
        /// <summary>
        /// 构造文件传输的服务地址
        /// </summary>
        /// <returns></returns>
        public static IFileTransportService CreateChannelFileTransportService()
        {
            if (ConfigurationManager.ConnectionStrings["EndpointAddress"] != null)
            {
                readerOperateEndpointAddress = SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["EndpointAddress"].ConnectionString);
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
            foreach (OperationDescription op in proxy.Endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
            IFileTransportService obj = proxy.CreateChannel();
            return obj;
        }

    }
}
