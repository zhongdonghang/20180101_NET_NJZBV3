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
        /// 添加命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="schoolIDList"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddCommand(AMS.Model.AMS_IssureList command, List<int> schoolIDList);

        /// <summary>
        /// 查询命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="schoolID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_IssureList> GetCommandState(AMS.Model.Enum.IsureCommandType commandType, int schoolID, AMS.Model.Enum.CommandHandleResult state);

        /// <summary>
        /// 查询学校的命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="schoolID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_IssureList> GetSchoolCommand(string schoolNo);
        /// <summary>
        /// 删除命令
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteCommand(AMS.Model.AMS_IssureList model);
        /// <summary>
        /// 修改命令
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCommand(Model.AMS_IssureList model);
    }
}
