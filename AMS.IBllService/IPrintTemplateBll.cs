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
        /// 获取全部打印模板
        /// *异常处理，catch后向上抛出
        /// </summary>
        /// <returns>返回打印模板的list</returns>
        [OperationContract]
        List<AMS.Model.AMS_PrintTemplate> GetPrintTemplateList();

        /// <summary>
        /// 添加新的打印模板
        /// *异常处理，catch捕获后return异常信息、
        /// *自定义异常：编号不能重复
        /// </summary>
        /// <param name="model">打印模板的model</param>
        /// <returns>成功返回null或""值，失败返回错误信息</returns>
        [OperationContract]
        string AddNewPrintTemplate(AMS.Model.AMS_PrintTemplate model);

        /// <summary>
        /// 更新打印模板
        /// *异常处理，catch捕获后return异常信息、
        /// *自定义异常：除了当前模板编号不能和其它打印模板重复
        /// </summary>
        /// <param name="model">打印模板的model，ID不能修改</param>
        /// <returns>成功返回null或""值，失败返回错误信息</returns>
        [OperationContract]
        string UpdatePrintTemplate(AMS.Model.AMS_PrintTemplate model);

        /// <summary>
        /// 删除打印模板
        /// *异常处理，catch捕获后return异常信息
        /// </summary>
        /// <param name="model">需要删除的打印模板的model，除了ID之外，其他属性可以为空</param>
        /// <returns>成功返回null或""值，失败返回错误信息</returns>
        [OperationContract]
        string DeletePrintTemplate(AMS.Model.AMS_PrintTemplate model);

        [OperationContract]
        AMS.Model.AMS_PrintTemplate GetPrintTemplateByNum(int Num);
    }
}
