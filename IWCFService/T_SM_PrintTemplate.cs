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
        /// 获取打印模板
        /// </summary>
        /// <returns></returns>
         [OperationContract]
        string GetPrintTemplate(string CardNo);
        /// <summary>
        /// 添加打印模板
        /// </summary>
        /// <returns></returns>
         [OperationContract]
         EnumType.HandleResult AddPrintTemplate(SeatManage.ClassModel.AMS_PrintTemplateModel model);
        /// <summary>
        /// 获取过期的打印模板
        /// </summary>
        /// <returns></returns>
         [OperationContract]
         List<SeatManage.ClassModel.AMS_PrintTemplateModel> GetPrintTemplateOverTime();
        /// <summary>
        /// 删除打印模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
         EnumType.HandleResult DeletePrintTemplate(SeatManage.ClassModel.AMS_PrintTemplateModel model);

        /// <summary>
        /// 查询NUM是否存在
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.AMS_PrintTemplateModel GetPrintTemplateByNum(string Num);

        /// <summary>
        /// 修改打印模板
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult UpdatePrintTemplate(SeatManage.ClassModel.AMS_PrintTemplateModel model);
    }
}
