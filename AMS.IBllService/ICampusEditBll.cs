using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService
    {
        /// <summary>
        /// 获取校区信息
        /// </summary>
        /// <param name="schoolNo">学校编号，Null为全部学校</param>
        /// <param name="fullInfo">是否获取全部信息</param>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_Campus> GetCampusInfo(string schoolNo, bool fullInfo);
        /// <summary>
        /// 获取单个校区信息
        /// </summary>
        /// <param name="campusNo">校区编号</param>
        /// <param name="fullInfo">是否获取全部信息</param>
        /// <returns></returns>
        [OperationContract]
        AMS.Model.AMS_Campus GetSingleCampusInfo(string campusNo, bool fullInfo);
        /// <summary>
        /// 添加校区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        AMS.Model.Enum.HandleResult AddNewCampus(AMS.Model.AMS_Campus model);
        /// <summary>
        /// 更新校区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        AMS.Model.Enum.HandleResult UpdateCampus(AMS.Model.AMS_Campus model);
        /// <summary>
        /// 删除校区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        AMS.Model.Enum.HandleResult DeleteCampus(AMS.Model.AMS_Campus model);
    }
}
