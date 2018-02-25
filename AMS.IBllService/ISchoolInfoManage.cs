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
        ///获取学校信息|
        /// * 返回的List中Item结构为树形结构 省份->学校->校区->设备|
        /// * 如果不包含子项（如：学校不包含校区）则子项设置为空的list，不能把list设置为null。|
        /// * 如果没查到相关信息则返回空的list。
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_ProvinceSchoolInfo> GetSchoolList();
        /// <summary>
        /// 添加学校信息
        /// * 直接把参数中的学校model添加到数据库中。|
        ///     异常处理：
        ///     * 如果添加时候出现逻辑异常，比如：编号已存在，则返回一个字符串提示信息。
        ///     * 如果操作数据库时出现异常,如：数据库连接失败，throw一个异常到客户端。
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string AddSchoolInfo(AMS.Model.AMS_School model);
        /// <summary>
        /// 更新学校信息
        /// * 把数据库中Id与参数Id相对应的数据更新为参数中的数据。
        ///     异常处理：
        ///     * 如果添加时候出现逻辑异常，比如：编号已存在，则返回一个字符串提示信息。
        ///     * 如果操作数据库时出现异常,如：数据库连接失败，throw一个异常到客户端。
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateSchoolInfo(AMS.Model.AMS_School model);
        /// <summary>
        /// 删除model中的数据
        ///   * 把数据库中Id与参数Id相对应的数据删除。
        ///     异常处理：
        ///     * 如果添加时候出现逻辑异常，比如：编号已存在，则返回一个字符串提示信息。
        ///     * 如果操作数据库时出现异常,如：数据库连接失败，throw一个异常到客户端。  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string DeleteSchoolInfo(AMS.Model.AMS_School model);
        /// <summary>
        /// 添加校区信息
        /// * 把参数model中的数据添加到数据库中
        ///     异常处理：
        ///     * 如果添加时候出现逻辑异常，比如：编号已存在，则返回一个字符串提示信息。
        ///     * 如果操作数据库时出现异常,如：数据库连接失败，throw一个异常到客户端。   
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string AddCampusInfo(AMS.Model.AMS_Campus model);

        /// <summary>
        /// 更新校区信息
        /// *把参数中的信息更新到数据库中
        ///     异常处理：
        ///     * 如果添加时候出现逻辑异常，比如：编号已存在，则返回一个字符串提示信息。
        ///     * 如果操作数据库时出现异常,如：数据库连接失败，throw一个异常到客户端。  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateCampusInfo(AMS.Model.AMS_Campus model);

        /// <summary>
        /// 删除校区信息
        /// *把参数中的校区删除
        ///     异常处理：
        ///     * 如果添加时候出现逻辑异常，比如：编号已存在，则返回一个字符串提示信息。
        ///     * 如果操作数据库时出现异常,如：数据库连接失败，throw一个异常到客户端。  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string DeleteCampusInfo(AMS.Model.AMS_Campus model);
        /// <summary>
        /// 添加设备
        /// * 把参数中的设备信息添加到数据库中
        ///     异常处理：
        ///     * 如果添加时候出现逻辑异常，比如：编号已存在，则返回一个字符串提示信息。
        ///     * 如果操作数据库时出现异常,如：数据库连接失败，throw一个异常到客户端。  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string AddDevice(AMS.Model.AMS_Device model);
        /// <summary>
        /// 更新设备信息
        /// * 把参数中的设备信息更新到数据库中
        ///     异常处理：
        ///     * 如果添加时候出现逻辑异常，比如：编号已存在，则返回一个字符串提示信息。
        ///     * 如果操作数据库时出现异常,如：数据库连接失败，throw一个异常到客户端。  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateDevice(AMS.Model.AMS_Device model);

        /// <summary>
        /// 删除设备信息
        ///     异常处理：
        ///     * 如果添加时候出现逻辑异常，比如：编号已存在，则返回一个字符串提示信息。
        ///     * 如果操作数据库时出现异常,如：数据库连接失败，throw一个异常到客户端。  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string DeleteDevice(AMS.Model.AMS_Device model);
    }
}
