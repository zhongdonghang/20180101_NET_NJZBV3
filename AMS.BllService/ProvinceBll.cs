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
        //NewDal
        AMS.DAL.AMS_Province dal_Province = new DAL.AMS_Province();
        /// <summary>
        /// 获取地区信息
        /// </summary>
        /// <returns></returns>
        public List<AMS.Model.AMS_Province> GetProvineInfo()
        {
            try
            {
                List<AMS.Model.AMS_Province> modellist = new List<Model.AMS_Province>();
                DataSet ds = dal_Province.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_ProvinceModel(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddNewProvine(AMS.Model.AMS_Province model)
        {
            try
            {
                AMS.Model.AMS_Province sameModel = dal_Province.GetModel(model.ProvinceName);
                if (sameModel != null)
                {
                    return "该地区名称已存在！";
                }
                if (dal_Province.Add(model) == 0)
                {
                    return "添加地区信息失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 修改地区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateProvine(AMS.Model.AMS_Province model)
        {
            try
            {
                AMS.Model.AMS_Province sameModel = dal_Province.GetModel(model.ProvinceName);
                if (sameModel != null && sameModel.ID != model.ID)
                {
                    return "该地区名称已存在！";
                }
                if (!dal_Province.Update(model))
                {
                    return "修改地区信息失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 删除地区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string DeleteProvine(AMS.Model.AMS_Province model)
        {
            try
            {
                if (!dal_Province.Delete(model.ID))
                {
                    return "删除地区信息失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 数据行转model
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private AMS.Model.AMS_Province DataRowToAMS_ProvinceModel(DataRow dr)
        {
            AMS.Model.AMS_Province model = new Model.AMS_Province();
            model.ID = int.Parse(dr["ID"].ToString());
            model.ProvinceName = dr["ProvinceName"].ToString();
            model.Remark = dr["Remark"].ToString();
            return model;
        }
    }
}
