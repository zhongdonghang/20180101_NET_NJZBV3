using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.IBllService;
using System.Data;

namespace AMS.BllService
{
    public partial class AdvertManageBllService : IAdvertManageBllService
    {
        DAL.AMS_PrintTemplate dal_PrintTemplate = new DAL.AMS_PrintTemplate();
        DAL.View_PrintTemplate dal_PrintTemplateView = new DAL.View_PrintTemplate();
        public List<Model.AMS_PrintTemplate> GetPrintTemplateList()
        {
            try
            {
                List<Model.AMS_PrintTemplate> modellist = new List<Model.AMS_PrintTemplate>();
                DataSet ds = dal_PrintTemplateView.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_PrintTemplateModel(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AddNewPrintTemplate(Model.AMS_PrintTemplate model)
        {
            try
            {
                AMS.Model.AMS_PrintTemplate sameModel = dal_PrintTemplate.GetModel(model.Number);
                if (sameModel != null)
                {
                    return "模板编号重复！";
                }
                if (dal_PrintTemplate.Add(model) == 0)
                {
                    return "添加打印模板失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdatePrintTemplate(Model.AMS_PrintTemplate model)
        {
            try
            {
                AMS.Model.AMS_PrintTemplate sameModel = dal_PrintTemplate.GetModel(model.Number);
                if (sameModel != null && sameModel.Id != model.Id)
                {
                    return "模板编号重复！";
                }
                if (!dal_PrintTemplate.Update(model))
                {
                    return "修改打印模板失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeletePrintTemplate(Model.AMS_PrintTemplate model)
        {
            try
            {
                if (!dal_PrintTemplate.Delete(model.Id))
                {
                    return "删除打印模板失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private AMS.Model.AMS_PrintTemplate DataRowToAMS_PrintTemplateModel(DataRow dr)
        {
            //CustomerName,Name,Number,Template,EffectDate,EndDate,Describe,CustomerNo,
            //CustomerLinkWay,CustomerDescribe
            //OperatorRemark,OperatorName,OperatorBranchName,OperatorPwd,OperatorLoginId
            AMS.Model.AMS_PrintTemplate model = new Model.AMS_PrintTemplate();
            model.Id = int.Parse(dr["ID"].ToString());
            if (dr.Table.Columns.Contains("OperatorName"))
            {
                model.OperatorName = dr["OperatorName"].ToString();
            }
            model.CustomerId = int.Parse(dr["CustomerId"].ToString());
            model.CustomerName = dr["CustomerName"].ToString();
            model.Describe = dr["Describe"].ToString();
            model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            model.Name = dr["Name"].ToString();
            model.Number = dr["Number"].ToString();
            model.Template = dr["Template"].ToString();
            return model;
        }


        public AMS.Model.AMS_PrintTemplate GetPrintTemplateByNum(int Num)
        {
            try
            {
                Model.AMS_PrintTemplate modellist = new Model.AMS_PrintTemplate();
                string sql = string.Format(" ID={0}",Num);
                DataSet ds = dal_PrintTemplateView.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist = DataRowToAMS_PrintTemplateModel(ds.Tables[0].Rows[i]);
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
