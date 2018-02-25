using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        #region IExceptionService 成员

        public void ThrowExceptionOneWay()
        {
            throw new NotImplementedException();
        }

        public void ThrowMessageFault()
        {
            //InvalidOperationException error = new InvalidOperationException("");
            //MessageFault mfault=MessageFault.CreateFault(new FaultCode("Server",new FaultCode(String.Format("Server.{0}",error))));
            throw new NotImplementedException();
        }

        public void ThrowFaultEcxeption()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
