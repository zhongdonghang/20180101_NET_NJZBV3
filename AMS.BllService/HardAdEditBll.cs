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
        AMS.DAL.AMS_HardAd Dal_HardAd = new DAL.AMS_HardAd();
        /// <summary>
        /// 获取硬广信息
        /// </summary>
        /// <returns></returns>
        public List<Model.AMS_HardAd> GetHardAdList()
        {
            try
            {
                List<AMS.Model.AMS_HardAd> modellist = new List<Model.AMS_HardAd>();
                DataSet ds = Dal_HardAd.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_HardAd(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {

                throw ex;
            }   
        }
        /// <summary>
        /// 添加硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddNewHardAd(Model.AMS_HardAd model)
        {
            try
            {
                Model.AMS_HardAd Admodel = Dal_HardAd.GetModel(model.Name);
                if (Admodel!=null)
                {
                    return "该硬广已存在!";
                }
                if (Dal_HardAd.Add(model) == 0)
                {
                    return "添加信息失败!";
                }
                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        /// <summary>
        /// 修改硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateHardAd(Model.AMS_HardAd model)
        {
            try
            {
                Model.AMS_HardAd Admodel = Dal_HardAd.GetModel(model.Name);
                if (Admodel != null && Admodel.ID != model.ID)
                {
                    return "该硬广已存在！";
                }
                if (!Dal_HardAd.Update(model))
                {
                    return "修改失败！";
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 删除硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string DeleteHardAd(Model.AMS_HardAd model)
        {
            try
            {
                if (!Dal_HardAd.Delete(model.ID))
                {
                    return "删除硬广失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// row to model
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private AMS.Model.AMS_HardAd DataRowToAMS_HardAd(DataRow dr)
        {
            AMS.Model.AMS_HardAd model = new Model.AMS_HardAd();
            model.ID = int.Parse(dr["ID"].ToString());
            model.CustomerId = int.Parse(dr["CustomerId"].ToString());
            model.Number = dr["Number"].ToString();
            model.Name = dr["Name"].ToString();
            model.EffectDate = Convert.ToDateTime(dr["EffectDate"].ToString());
            model.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
            model.AdImage = (byte[])(dr["AdImage"]);
            model.Describe = dr["Describe"].ToString();
            model.Operator = int.Parse(dr["Operator"].ToString());
            return model;
        }

        /// <summary>
        /// get model
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Model.AMS_HardAd GetHardAdModel(string name)
        {
            try
            {
                return Dal_HardAd.GetModel(name);
            }
            catch (Exception)
            {
                
                throw new Exception("出现异常");
            }
        }


        public Model.AMS_HardAd GetHardAdModelByNum(int Num)
        {
            try
            {
                return Dal_HardAd.GetModelByNum(Num);
            }
            catch (Exception)
            {

                throw new Exception("出现异常");
            }
        }
    }
}
