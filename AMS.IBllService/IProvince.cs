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
        /// 获取省份列表
        /// *异常处理：向上抛出
        /// </summary>
        /// <returns>返回省份信息的model的list</returns>
        [OperationContract]
        List<AMS.Model.AMS_Province> GetProvineInfo();

        ///// <summary>
        ///// 获取单独信息
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[OperationContract]
        //AMS.Model.AMS_Province GetSingleProvineInfo(int id);

        /// <summary>
        /// 新增省份
        /// *异常处理：catch到后return异常信息
        /// *自定义异常，省份名称不能相同
        /// </summary>
        /// <param name="model">省份信息的model</param>
        /// <returns>执行成功返回Null或""失败返回错误或异常信息</returns>
        [OperationContract]
        string AddNewProvine(AMS.Model.AMS_Province model);


        /// <summary>
        /// 更新省份
        /// *异常处理：catch到后return异常信息
        /// </summary>
        /// <param name="model">省份的model</param>
        /// <returns>执行成功返回Null或""失败返回错误或异常信息</returns>
        [OperationContract]
        string UpdateProvine(AMS.Model.AMS_Province model);


        /// <summary>
        /// 删除省份
        /// *异常处理：catch到后return异常信息
        /// </summary>
        /// <param name="model">需要删除的省份的model，只需要ID，其他属性可以为空</param>
        /// <returns>执行成功返回Null或""失败返回错误或异常信息</returns>
        [OperationContract]
        string DeleteProvine(AMS.Model.AMS_Province model);
    }
}
