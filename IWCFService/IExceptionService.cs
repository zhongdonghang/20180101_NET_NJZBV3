using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    [ServiceContract]
    public interface IExceptionService
    {
        
        [OperationContract(IsOneWay = true)]
        void ThrowExceptionOneWay();

        [OperationContract]
        [FaultContract(typeof(InvalidOperationException))]
        void ThrowMessageFault();

        [OperationContract()]
        [FaultContract(typeof(InvalidOperationException))]
        void ThrowFaultEcxeption();
    }
}
