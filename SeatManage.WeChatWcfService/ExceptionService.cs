using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWeChatWcfService;

namespace SeatManage.WeChatWcfService
{
    public partial class WeChatService : IWeChatService
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
