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
        /// 添加滚动文字
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string AddRollTitles(Model.AMS_RollTitles model);
        /// <summary>
        /// 修改滚动文字
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateModel(Model.AMS_RollTitles model);
        /// <summary>
        /// 删除单个实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        string DelModel(int id);
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="StrWhere"></param>
        /// <returns></returns>
        [OperationContract]
        List<Model.AMS_RollTitles> GetList();

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="StrWhere"></param>
        /// <returns></returns>
        [OperationContract]
        Model.AMS_RollTitles GetModelNum(int  Num);
    }
}
