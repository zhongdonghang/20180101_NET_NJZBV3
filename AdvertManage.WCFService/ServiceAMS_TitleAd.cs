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
        DAL.AMS_TitleAdDal titleAdDal = new DAL.AMS_TitleAdDal();
        /// <summary>
        /// 根据Id获取优惠券信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public AdvertManage.Model.AMS_TitleAdModel GetTitleAdById(int id)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Id={0}", id);
            try
            {
                DataSet ds = titleAdDal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    AdvertManage.Model.AMS_TitleAdModel model = DataRowToAMS_TitleAdModel(ds.Tables[0].Rows[0]);
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 私有方法
        AdvertManage.Model.AMS_TitleAdModel DataRowToAMS_TitleAdModel(DataRow dr)
        {
            AdvertManage.Model.AMS_TitleAdModel model = new AdvertManage.Model.AMS_TitleAdModel();

            if (dr["Id"] != null && dr["Id"].ToString() != "")
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr["EffectDate"] != null && dr["EffectDate"].ToString() != "")
            {
                model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            }
            if (dr["EndDate"] != null && dr["EndDate"].ToString() != "")
            {
                model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            }
            if (dr["AdContent"] != null && dr["AdContent"].ToString() != "")
            {
                model.AdContent = dr["AdContent"].ToString();
            }
            return model;

        }
        #endregion


        public List<Model.AMS_TitleAdModel> GetTitleAd()
        {
            try
            {
                List<Model.AMS_TitleAdModel> modelList = new List<Model.AMS_TitleAdModel>();
                DataSet ds = titleAdDal.GetList(null, null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AdvertManage.Model.AMS_TitleAdModel model = DataRowToAMS_TitleAdModel(ds.Tables[0].Rows[i]);
                    modelList.Add(model);
                }
                return modelList;
            }
            catch
                (Exception ex)
            {
                throw ex;
            }

        }

        public Model.Enum.HandleResult AddTitleAd(Model.AMS_TitleAdModel model)
        {
            try
            {
                if (titleAdDal.Add(model) > 0)
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        public Model.Enum.HandleResult UpdateTitleAd(Model.AMS_TitleAdModel model)
        {
            try
            {
                if (titleAdDal.Update(model) )
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        public Model.Enum.HandleResult DeleteTitleAd(int id)
        {
            try
            {
                if (titleAdDal.Delete(id) )
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
