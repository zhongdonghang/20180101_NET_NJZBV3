using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using SeatManage.ClassModel;

namespace WcfServiceForSeatManage
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public partial class SeatManageDateService : ISeatManageService
    { 

        #region 获取服务器时间
        public DateTime GetServerDateTime()
        {
            return DateTime.Now;
        }
        #endregion 

    }
}
