using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    /// <summary>
    /// 冠名广告
    /// </summary>
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取有效的冠名信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.TitleAdvertInfo GetTitleAdvertInfo();
        /// <summary>
        /// 获取过期的冠名广告
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.TitleAdvertInfo> GetTitleAdOverTime();
        /// <summary>
        /// 添加冠名信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult AddTitleAdvert(SeatManage.ClassModel.TitleAdvertInfo model);
        /// <summary>
        /// 删除冠名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult DeleteTitleAdvert(SeatManage.ClassModel.TitleAdvertInfo model);
        /// <summary>
        /// 判断Num是否存在
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.TitleAdvertInfo GetTitleModel(string Num);

        /// <summary>
        /// 修改冠名信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult UpdateTitleAdvert(SeatManage.ClassModel.TitleAdvertInfo model);
    }
}
