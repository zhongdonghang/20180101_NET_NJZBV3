using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 添加冠名信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult AddRollTitles(SeatManage.ClassModel.RollTitlesInfo model);
        /// <summary>
        /// 查询有效实例转换成string
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetModelStr();
        /// <summary>
        /// 根据NUM查询数据是否存在
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.RollTitlesInfo GetModelByNum(string Num);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult UpdateRollTitles(SeatManage.ClassModel.RollTitlesInfo model);
        /// <summary>
        /// 获取过期的广告
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ClassModel.RollTitlesInfo> GetOverTimeRollTitleList();
        /// <summary>
        /// 删除过期的滚动广告
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult DeleteRollTitle(string Num);

    }
}
