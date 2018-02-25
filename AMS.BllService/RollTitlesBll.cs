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
        AMS.DAL.AMS_RollTitles dal = new DAL.AMS_RollTitles();
        public string AddRollTitles(Model.AMS_RollTitles model)
        {
            try
            {
                if (dal.GetModelByName(model.Name))
                {
                    return "此滚动文字已存在";
                }
                if (!dal.AddRollTitles(model))
                {
                    return "添加失败";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        public string UpdateModel(Model.AMS_RollTitles model)
        {
            try
            {
                if (!dal.UpdateRollTitles(model))
                {
                    return "修改失败";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DelModel(int id)
        {
            try
            {
                if (dal.DelRollTitles(id))
                {
                    return "删除成功";
                }
                return "删除失败";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        public List<Model.AMS_RollTitles> GetList()
        {
            try
            {
                List<Model.AMS_RollTitles> list = new List<Model.AMS_RollTitles>();
                DataSet ds = dal.GetModel(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(DataRowToAMS_RollTitles(ds.Tables[0].Rows[i]));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private AMS.Model.AMS_RollTitles DataRowToAMS_RollTitles(DataRow dr)
        {
            AMS.Model.AMS_RollTitles model = new Model.AMS_RollTitles
            {
               ID=int.Parse(dr["ID"].ToString()),
               CustomerId = int.Parse(dr["CustomerId"].ToString()),
               EffectDate = Convert.ToDateTime(dr["EffectDate"].ToString()),
               EndDate = Convert.ToDateTime(dr["EndDate"].ToString()),
               Name = dr["Name"].ToString(),
               Type = dr["Type"].ToString(),
               Num=dr["Num"].ToString()
            };
            return model;
        }


        public Model.AMS_RollTitles GetModelNum(int Num)
        {
            try
            {
                Model.AMS_RollTitles list = new Model.AMS_RollTitles();
                string sql = string.Format(" ID={0}",Num);
                DataSet ds = dal.GetModel(sql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list=DataRowToAMS_RollTitles(ds.Tables[0].Rows[i]);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
