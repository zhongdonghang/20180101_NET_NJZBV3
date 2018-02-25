using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IPocketBespeakBllService;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
namespace BespeakServiceConnProxy
{
    public class BespeakServiceConnProxy
    {
        private static string pocketBespeakBllServiceEndpointAddress;// = ConfigurationManager.ConnectionStrings["EndpointAddress"].ConnectionString;
        public static IPocketBespeakBllService CreateChannelPocketBespeakBllService(string connStr)
        {
            

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.CloseTimeout = new TimeSpan(0, 10, 0);
            string endPointAddress = connStr + "PocketBespeakBllService/";
            ChannelFactory<IPocketBespeakBllService> proxy = new ChannelFactory<IPocketBespeakBllService>(binding, new EndpointAddress(endPointAddress));
            foreach (OperationDescription op in proxy.Endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
            IPocketBespeakBllService obj = proxy.CreateChannel();

            return obj;
        }
    }
}
