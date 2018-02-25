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
        /// 根据Id获取打印模版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract] 
        AdvertManage.Model.AMS_PrintTemplateModel GetPrintTemplateById(int id);
   
        /// <summary>
        /// 获取所有的打印模板
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AdvertManage.Model.AMS_PrintTemplateModel> GetPrintTemplateList();
        /// <summary>
        /// 更新打印模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
       Model.Enum.HandleResult UpdatePrintTemplate (AdvertManage.Model.AMS_PrintTemplateModel model);
        /// <summary>
        /// 添加打印模版
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult AddPrintTemplateList(AdvertManage.Model.AMS_PrintTemplateModel model);
        /// <summary>
        /// 删除打印模版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         [OperationContract]
        Model.Enum.HandleResult DeletePrintTemplate(int id);
    }
}
