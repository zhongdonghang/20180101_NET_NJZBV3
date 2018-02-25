using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.IWCFService;
using System.Data;

namespace AdvertManage.WCFService
{
    public partial class AdvertManageService : IAdvertManageService
    {
        DAL.AMS_PrintTemplateDal printTemplateDal = new DAL.AMS_PrintTemplateDal();
        /// <summary>
        /// 根据Id获取播放列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public AdvertManage.Model.AMS_PrintTemplateModel GetPrintTemplateById(int id)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Id={0}", id);
            try
            {
                DataSet ds = printTemplateDal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    AdvertManage.Model.AMS_PrintTemplateModel model = DataRowToAMS_PrintTemplateModel(ds.Tables[0].Rows[0]); 
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region
        AdvertManage.Model.AMS_PrintTemplateModel DataRowToAMS_PrintTemplateModel(DataRow dr)
        {
            AdvertManage.Model.AMS_PrintTemplateModel model = new AdvertManage.Model.AMS_PrintTemplateModel();
            if (dr["Id"] != null && dr["Id"].ToString() != "")
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr["Template"] != null && dr["Template"].ToString() != "")
            {
                model.Template = dr["Template"].ToString();
            }
            if (dr["EffectDate"] != null && dr["EffectDate"].ToString() != "")
            {
                model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            }
            if (dr["EndDate"] != null && dr["EndDate"].ToString() != "")
            {
                model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            }
            if (dr["Describe"] != null && dr["Describe"].ToString() != "")
            {
                model.Describe = dr["Describe"].ToString();
            }
            return model;

        }
        #endregion


      

        public List<Model.AMS_PrintTemplateModel> GetPrintTemplateList()
        { 
            try
            {
                List<Model.AMS_PrintTemplateModel> printTemplateList = new List<Model.AMS_PrintTemplateModel>();
                DataSet ds = printTemplateDal.GetList(null, null);
                for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
                {
                    AdvertManage.Model.AMS_PrintTemplateModel model = DataRowToAMS_PrintTemplateModel(ds.Tables[0].Rows[i]);
                    printTemplateList.Add(model);
                }
                return printTemplateList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Model.Enum.HandleResult UpdatePrintTemplate(Model.AMS_PrintTemplateModel model)
        {
            try
            {
                if (printTemplateDal.Update(model))
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加打印凭条模版
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult AddPrintTemplateList(Model.AMS_PrintTemplateModel model)
        {
            try
            {
                if (printTemplateDal.Add(model))
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除打印模版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult DeletePrintTemplate(int id)
        {
            try
            {
                if (printTemplateDal.Delete(id))
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
