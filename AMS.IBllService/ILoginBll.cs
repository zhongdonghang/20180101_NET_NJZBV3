using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using AMS.Model;

namespace AMS.IBllService
{
    [ServiceContract]
    public partial interface IAdvertManageBllService
    {
        [OperationContract]
        AMS_UserInfo Login(string loginName, string loginPassword);
    }
}
