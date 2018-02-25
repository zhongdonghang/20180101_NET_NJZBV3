using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService
    {

        [OperationContract]
        List<AMS.Model.AMS_School> GetSchoolInfoList();

        [OperationContract]
        AMS.Model.AMS_School GetSchoolInfoById(int SchoolID);

        [OperationContract]
        AMS.Model.AMS_School GetSchoolInfoByNum(string schoolNum);
    }
}
