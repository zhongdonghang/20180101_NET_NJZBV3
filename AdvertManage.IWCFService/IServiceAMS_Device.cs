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
        /// 根据学校编号获取设备列表
        /// 参数flag为
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
       List< AdvertManage.Model.AMS_DeviceModel> GeDeviceModelBySchoolNum(string schoolNum,bool flag);
        /// <summary>
        /// 根据校区编号查看设备信息
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
          [OperationContract]
        List<AdvertManage.Model.AMS_DeviceModel> GeDeviceModelByCampusNum(string campusNum);
        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.Enum.HandleResult UpdateDeviceModel( AdvertManage.Model.AMS_DeviceModel model);
        
        /// <summary>
        /// 更新设备状态
        /// </summary>
        /// <param name="DeviceNo">设备编号</param>
        /// <param name="stateTime">状态更新时间</param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.Enum.HandleResult UpdateDeviceStatus(string DeviceNo,DateTime stateUpdateTime);

        /// <summary>
        /// 添加设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.Enum.HandleResult AddDeviceModel(AdvertManage.Model.AMS_DeviceModel model);
        /// <summary>
        /// 根据设备编号获取设备信息
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.AMS_DeviceModel GetDevicebyNo(string no);
        /// <summary>
        /// 根据设备id获取设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.AMS_DeviceModel GetDevicebyid(int id);
    }
}
