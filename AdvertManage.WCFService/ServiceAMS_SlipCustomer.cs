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
        DAL.AMS_SlipCustomerDal slipCustomerDal = new DAL.AMS_SlipCustomerDal();
        /// <summary>
        /// 根据Id获取优惠券信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
       public AdvertManage.Model.AMS_SlipCustomerModel GetSlipCustomerById(int id)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Id={0}", id);
            try
            {
                DataSet ds = slipCustomerDal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    AdvertManage.Model.AMS_SlipCustomerModel model = DataRowToAMS_SlipCustomerModel(ds.Tables[0].Rows[0]);
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据优惠券编号获取优惠券信息
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
       public AdvertManage.Model.AMS_SlipCustomerModel GetSlipCustomerByNum(string num)
       {
           StringBuilder strWhere = new StringBuilder();
           strWhere.AppendFormat(" Number='{0}'", num);
           try
           {
               DataSet ds = slipCustomerDal.GetList(strWhere.ToString(), null);
               if (ds.Tables[0].Rows.Count > 0)
               {
                   AdvertManage.Model.AMS_SlipCustomerModel model = DataRowToAMS_SlipCustomerModel(ds.Tables[0].Rows[0]);
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
        AdvertManage.Model.AMS_SlipCustomerModel DataRowToAMS_SlipCustomerModel(DataRow dr)
        {
              AdvertManage.Model.AMS_SlipCustomerModel model = new Model.AMS_SlipCustomerModel();
                if (dr["Id"] != null && dr["Id"].ToString() != "")
                {
                    model.Id = int.Parse(dr["Id"].ToString());
                }
                if (dr["Number"] != null && dr["Number"].ToString() != "")
                {
                    model.Number = dr["Number"].ToString();
                }
                if (dr["ImageUrl"] != null && dr["ImageUrl"].ToString() != "")
                {
                    model.ImageUrl = dr["ImageUrl"].ToString();
                }
                if (dr["SlipTemplate"] != null && dr["SlipTemplate"].ToString() != "")
                {
                    model.SlipTemplate = dr["SlipTemplate"].ToString();
                }
                if (dr["CustomerImage"] != null && dr["CustomerImage"].ToString() != "")
                {
                    model.CustomerImage = dr["CustomerImage"].ToString();
                }
                if (dr["CampusNum"] != null && dr["CampusNum"].ToString() != "")
                {
                    model.CampusNum = dr["CampusNum"].ToString();
                }
                if (dr["EffectDate"] != null && dr["EffectDate"].ToString() != "")
                {
                    model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
                }
                if (dr["EndDate"] != null && dr["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
                }
                if (dr["Type"] != null && dr["Type"].ToString() != "")
                {
                    model.Type = dr["Type"].ToString();
                }
                if (dr["IsPrint"] != null && dr["IsPrint"].ToString() != "")
                {
                    model.IsPrint = bool.Parse(dr["IsPrint"].ToString());
                }
                return model; 
        }
        #endregion


        public List<Model.AMS_SlipCustomerModel> GetSlipCustomerList()
        {
            try
            {
                List<Model.AMS_SlipCustomerModel> modelList = new List<Model.AMS_SlipCustomerModel>();
                DataSet ds = slipCustomerDal.GetList(null, null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToAMS_SlipCustomerModel(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新优惠券信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult UpdateSlipCustomer(Model.AMS_SlipCustomerModel model)
        {
            try
            {
                if (slipCustomerDal.Update(model))
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

        public Model.Enum.HandleResult AddSlipCustomer(Model.AMS_SlipCustomerModel model)
        {
            try
            {
                if (slipCustomerDal.Add(model)>0)
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

        public Model.Enum.HandleResult DeleteSlipCustomer(int id)
        {
            try
            {
                if (slipCustomerDal.Delete(id) )
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
