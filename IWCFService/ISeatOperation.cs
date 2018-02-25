using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        [OperationContract]
        string SeatShortLeave(string cardNo, string clientNo, string remark);

        [OperationContract]
        string SeatLeave(string cardNo, string clientNo, string remark);
    }
}
