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
        /// 获取全部的系统版本信息列表
        /// *异常处理：直接向上层抛出
        /// </summary>
        /// <returns>获取成功返回List</returns>
        [OperationContract]
        List<AMS.Model.ProgramUpgrade> GetProgramUpgradeList();
        /// <summary>
        /// 根据系统类型获取系统版本信息
        /// *异常处理：直接向上层抛出
        /// </summary>
        /// <param name="programType">系统类型</param>
        /// <returns>获取成功返回List</returns>
        [OperationContract]
        AMS.Model.ProgramUpgrade GetProgramInfoByProgramType(Model.Enum.SeatManageSubsystem programType);
        /// <summary>
        /// 新增新版本系统
        /// *异常处理：直接在catch中return异常信息，自定义异常直接return自定义的错误
        /// *自定义异常：系统版本号不能其他同类型系统版本号相同
        /// </summary>
        /// <param name="model">下发命令的model</param>
        /// <returns>成功返回null或""值，失败返回失败信息</returns>
        [OperationContract]
        string AddNewProgramUpgrade(AMS.Model.ProgramUpgrade model);
        /// <summary>
        /// 更新系统版本
        /// *异常处理：直接在catch中return异常信息，自定义异常直接return自定义的错误
        /// *自定义异常：系统版本号不能其他同类型系统版本号相同
        /// </summary>
        /// <param name="model">下发命令的model</param>
        /// <returns>成功返回null或""值，失败返回失败信息</returns>
        [OperationContract]
        string UpdateProgramUpgrade(AMS.Model.ProgramUpgrade model);
        /// <summary>
        /// 删除系统版本
        /// *异常处理：直接在catch中return异常信息，自定义异常直接return自定义的错误
        /// </summary>
        /// <param name="model">下发命令的model</param>
        /// <returns>成功返回null或""值，失败返回失败信息</returns>
        [OperationContract]
        string DeleteProgramUpgrade(AMS.Model.ProgramUpgrade model);

        [OperationContract]
        Model.ProgramUpgrade GetProgramUpgradeByID (int id);
    }
}
