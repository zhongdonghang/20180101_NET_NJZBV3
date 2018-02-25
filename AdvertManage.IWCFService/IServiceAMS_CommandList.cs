using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.IWCFService
{
    public partial interface IAdvertManageService
    {
        /// <summary>
        /// 根据学校编号获取有效的命令列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AdvertManage.Model.AMS_CommandListModel> GetCommandListBySchoolNum(string schoolNum);
        /// <summary>
        /// 操作结果更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.Enum.HandleResult UpdateFinishFlag(AdvertManage.Model.AMS_CommandListModel model);
        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.Enum.HandleResult AddAMS_CommandList(AdvertManage.Model.AMS_CommandListModel model);
        /// <summary>
        /// 根据参数值获取命令列表
        /// </summary>
        /// <param name="schoolId">学校Id</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="handleResult">学校Id</param>
        /// <returns></returns>
        [OperationContract]
        List<AdvertManage.Model.AMS_CommandListModel> GetCommandListByCondition(int schoolId,Model.Enum.CommandType commandType,Model.Enum.CommandHandleResult handleResult);
    }
}
