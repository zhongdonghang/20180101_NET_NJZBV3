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
        AMS.DAL.AMS_TitleAd Dal_TitleAd = new DAL.AMS_TitleAd();
        /// <summary>
        /// 获取冠名广告信息
        /// </summary>
        /// <returns></returns>
        public List<Model.AMS_TitleAd> GetTitleAdList()
        {
            try
            {
                List<AMS.Model.AMS_TitleAd> modellist = new List<Model.AMS_TitleAd>();
                DataSet ds = Dal_TitleAd.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_TitleAd(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {

                throw ex;
            }   
        }
        /// <summary>
        /// 添加冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddNewTitleAd(Model.AMS_TitleAd model)
        {
            try
            {
                Model.AMS_TitleAd Admodel = Dal_TitleAd.GetModel(model.Name);
                if (Admodel != null)
                {
                    return "该冠名已存在!";
                }
                if (Dal_TitleAd.Add(model) == 0)
                {
                    return "添加硬广信息失败!";
                }
                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        /// <summary>
        /// 修改冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateTitleAd(Model.AMS_TitleAd model)
        {
            try
            {
                Model.AMS_TitleAd Admodel = Dal_TitleAd.GetModel(model.Name);
                if (Admodel != null && model.Id != Admodel.Id)
                {
                    return "该冠名已存在！";
                }
                if (!Dal_TitleAd.Update(model))
                {
                    return "更新失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteTitleAd(Model.AMS_TitleAd model)
        {
            try
            {
                if (!Dal_TitleAd.Delete(model.Id))
                {
                    return "删除冠名失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private AMS.Model.AMS_TitleAd DataRowToAMS_TitleAd(DataRow dr)
        {
            AMS.Model.AMS_TitleAd model = new Model.AMS_TitleAd();
            model.Id = int.Parse(dr["Id"].ToString());
            model.Name = dr["Name"].ToString();
            model.EffectDate = Convert.ToDateTime(dr["EffectDate"]);
            model.EndDate = Convert.ToDateTime(dr["EndDate"]);
            model.AdContent = dr["AdContent"].ToString();
            model.Num = dr["Num"].ToString();
            model.CustomerId = Convert.ToInt32(dr["CustomerId"]);
            return model;
        }


        public Model.AMS_TitleAd GetTitleAdByNum(int Num)
        {
            try
            {
                AMS.Model.AMS_TitleAd modellist = new Model.AMS_TitleAd();
                string sql = string.Format(" ID={0}", Num);
                DataSet ds = Dal_TitleAd.GetList(sql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist=DataRowToAMS_TitleAd(ds.Tables[0].Rows[i]);
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
