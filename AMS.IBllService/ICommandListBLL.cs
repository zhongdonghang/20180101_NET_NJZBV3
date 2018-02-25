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
        /// 根据学校编号获取有效的命令列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Model.AMS_CommandList> GetCommandListBySchoolNum(string schoolNum);
        /// <summary>
        /// 操作结果更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult UpdateFinishFlag(Model.AMS_CommandList model);
        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult AddAMS_CommandList(Model.AMS_CommandList model);
        /// <summary>
        /// 根据参数值获取命令列表
        /// </summary>
        /// <param name="schoolId">学校Id</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="handleResult">学校Id</param>
        /// <returns></returns>
        [OperationContract]
        List<Model.AMS_CommandList> GetCommandListByCondition(int schoolId, Model.Enum.CommandType commandType, Model.Enum.CommandHandleResult handleResult);
    }
}
