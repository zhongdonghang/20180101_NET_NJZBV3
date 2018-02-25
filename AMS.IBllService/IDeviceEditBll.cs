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
        /// 根据学校编号获取设备列表
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <param name="flag">表示是否获取更新的</param>
        /// <returns></returns>
       [OperationContract]
        List<AMS.Model.AMS_Device> GeDeviceModelBySchoolNum(string schoolNum, bool flag);
        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        AMS.Model.Enum.HandleResult UpdateDeviceModel(AMS.Model.AMS_Device model);
        /// <summary>
        /// 更新设备状态
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <param name="stateUpdateTime"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        AMS.Model.Enum.HandleResult UpdateDeviceStatus(string deviceNo, DateTime stateUpdateTime);

        /// <summary>
        /// 添加设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        /// 
        [OperationContract]
       AMS.Model.Enum.HandleResult AddDeviceModel(AMS.Model.AMS_Device model);

        /// <summary>
        /// 根据校区编号查看设备信息
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <param name="flag"></param>
        /// <returns></returns> 
        /// 
        [OperationContract]
        List<AMS.Model.AMS_Device> GeDeviceModelByCampusNum(string campusNum);
        /// <summary>
        /// 根据设备编号查找设备
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        AMS.Model.AMS_Device GetDevicebyNo(string No);
        /// <summary>
        /// 根据设备id查找设备
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        AMS.Model.AMS_Device GetDevicebyID(int id);
    }
}
