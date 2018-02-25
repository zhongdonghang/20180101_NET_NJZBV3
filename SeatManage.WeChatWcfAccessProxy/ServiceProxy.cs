using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using SeatManage.IWeChatWcfService;

namespace SeatManage.WeChatWcfChannel
{
    public class ServiceProxy
    {
        private static string weChatEndpointAddress;// = ConfigurationManager.ConnectionStrings["EndpointAddress"].ConnectionString;
        /// <summary>
        /// 创建WCF连接
        /// </summary>
        /// <returns></returns>
        public static IWeChatService CreateChannelSeatManageService()
        {
            if (string.IsNullOrEmpty(weChatEndpointAddress))
            {
                if (ConfigurationManager.ConnectionStrings["WeChatEndpointAddress"] != null)
                {
                    weChatEndpointAddress = SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["WeChatEndpointAddress"].ConnectionString);
                }
                else
                {
                    throw new Exception("未设置远程服务连接字符串");
                }
            }

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.CloseTimeout = new TimeSpan(0, 10, 0);
            string endPointAddress = weChatEndpointAddress + "WeChatService/";

            ChannelFactory<IWeChatService> proxy = new ChannelFactory<IWeChatService>(binding, new EndpointAddress(endPointAddress));
            foreach (OperationDescription op in proxy.Endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
            IWeChatService obj = proxy.CreateChannel();

            return obj;
        }
        /// <summary>
        /// 创建WCF连接
        /// </summary>
        /// <param name="endpointAddress">连接地址</param>
        /// <returns></returns>
        public static IWeChatService CreateChannelSeatManageService(string endpointAddress)
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;

            string endPointAddress = endpointAddress + "WeChatService/";
            ChannelFactory<IWeChatService> proxy = new ChannelFactory<IWeChatService>(binding, new EndpointAddress(endPointAddress));
            foreach (OperationDescription op in proxy.Endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
            IWeChatService obj = proxy.CreateChannel();
            return obj;
        }
    }
}
