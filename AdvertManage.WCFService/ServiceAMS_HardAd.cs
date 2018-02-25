using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.IWCFService;
using System.Data;
using AdvertManage.Model;
using System.Data.SqlClient;
using AdvertManage.Model.Enum;

namespace AdvertManage.WCFService
{
    public partial class AdvertManageService : IAdvertManageService
    {
        DAL.AMS_HardAdDal hardAdDal = new DAL.AMS_HardAdDal();
        /// <summary>
        /// 根据Id获取播放列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public AdvertManage.Model.AMS_HardAdModel GetHardAdById(int id)
        {
            Model.AMS_HardAdModel model = null;
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" id=@id");
            SqlParameter[] parameters = {
                                        new SqlParameter("@id",SqlDbType.Int)
                                      };
            parameters[0].Value = id;
            DataSet ds = hardAdDal.GetList(strWhere.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model = DataRowToAMS_HardAdModel(ds.Tables[0].Rows[0]);
            }
            return model;
        }
        public AMS_HardAdModel GetHardAdByNum(string number)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" Number=@Number");
            SqlParameter[] parameters = {
                                        new SqlParameter("@Number",SqlDbType.NVarChar)
                                      };
            parameters[0].Value = number;
            DataSet ds = hardAdDal.GetList(strWhere.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Model.AMS_HardAdModel model = DataRowToAMS_HardAdModel(ds.Tables[0].Rows[0]);
                return
                    model;
            }
            return null;
        }

        public Model.Enum.HandleResult AddHardAd(AMS_HardAdModel model)
        {
            try
            {
                if (hardAdDal.Add(model) > 0)
                {
                    return AdvertManage.Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Model.Enum.HandleResult UpdateHardAd(AMS_HardAdModel model)
        {
            try
            {
                if (hardAdDal.Update(model))
                {
                    return AdvertManage.Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Model.Enum.HandleResult DeleteHardAd(int id)
        {
            try
            {
                if (hardAdDal.Delete(id))
                {
                    return AdvertManage.Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AMS_HardAdModel> GetHardAdList()
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                List<AdvertManage.Model.AMS_HardAdModel> list = new List<Model.AMS_HardAdModel>();

                DataSet ds = hardAdDal.GetList(null, null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Model.AMS_HardAdModel model = DataRowToAMS_HardAdModel(ds.Tables[0].Rows[i]);
                    list.Add(model);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region
        AdvertManage.Model.AMS_HardAdModel DataRowToAMS_HardAdModel(DataRow dr)
        {
            AdvertManage.Model.AMS_HardAdModel model = new AMS_HardAdModel();
            if (dr["ID"] != null && dr["ID"].ToString() != "")
            {
                model.ID = int.Parse(dr["ID"].ToString());
            }
            if (dr["Number"] != null && dr["Number"].ToString() != "")
            {
                model.Number = dr["Number"].ToString();
            }
            if (dr["EffectDate"] != null && dr["EffectDate"].ToString() != "")
            {
                model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            }
            if (dr["EndDate"] != null && dr["EndDate"].ToString() != "")
            {
                model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            }
            if (dr["AdImage"] != null && dr["AdImage"].ToString() != "")
            {
                model.AdImage = (byte[])dr["AdImage"];
            }
            return model;
        }
        #endregion





    }
}
