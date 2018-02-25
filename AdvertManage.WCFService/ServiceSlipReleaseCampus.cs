using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.IWCFService;
using System.Data;
using AdvertManage.Model;

namespace AdvertManage.WCFService
{
    public partial class AdvertManageService : IAdvertManageService
    {
        AdvertManage.DAL.SlipReleaseCampus slipRelease_dal = new DAL.SlipReleaseCampus();
        /// <summary>
        /// 添加发布信息
        /// </summary>
        /// <param name="slipId"></param>
        /// <param name="CampusId"></param>
        /// <returns></returns>
        public int AddSlipRelease(int slipId, int CampusId)
        {
            try
            {
                return slipRelease_dal.Add(CampusId, slipId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据学校id查询
        /// </summary>
        /// <param name="schoolid"></param>
        /// <returns></returns>
        public List<AMS_SlipCustomerModel> GetSlipReleaseListBySchoolId(int schoolid)
        {
            List<AMS_SlipCustomerModel> list = new List<AMS_SlipCustomerModel>();
            StringBuilder strWhere = new StringBuilder();
            if (schoolid > 0)
            {
                strWhere.AppendFormat(" SchoolId='{0}'", schoolid);
            }
            try
            {
                DataSet ds = slipRelease_dal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        list.Add(DataRowToAMS_SlipCustomerViewModel(ds.Tables[0].Rows[i]));
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据学校ID查询
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<AMS_SlipCustomerModel> GetSlipReleaseListBySchoolNum(string num)
        {
            List<AMS_SlipCustomerModel> list = new List<AMS_SlipCustomerModel>();
            StringBuilder strWhere = new StringBuilder();
            if (string.IsNullOrEmpty(num))
            {
                strWhere.AppendFormat(" SchoolNum='{0}'", num);
            }
            try
            {
                DataSet ds = slipRelease_dal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        list.Add(DataRowToAMS_SlipCustomerViewModel(ds.Tables[0].Rows[i]));
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据校区id查询
        /// </summary>
        /// <param name="campusid"></param>
        /// <returns></returns>
        public List<AMS_SlipCustomerModel> GetSlipReleaseListByCampusId(int campusid)
        {
            List<AMS_SlipCustomerModel> list = new List<AMS_SlipCustomerModel>();
            StringBuilder strWhere = new StringBuilder();
            if (campusid > 0)
            {
                strWhere.AppendFormat(" CampusId='{0}'", campusid);
            }
            try
            {
                DataSet ds = slipRelease_dal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        list.Add(DataRowToAMS_SlipCustomerViewModel(ds.Tables[0].Rows[i]));
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据校区编号查询
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<AMS_SlipCustomerModel> GetSlipReleaseListByCampusNum(string num)
        {
            List<AMS_SlipCustomerModel> list = new List<AMS_SlipCustomerModel>();
            StringBuilder strWhere = new StringBuilder();
            if (string.IsNullOrEmpty(num))
            {
                strWhere.AppendFormat(" CampusNum='{0}'", num);
            }
            try
            {
                DataSet ds = slipRelease_dal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        list.Add(DataRowToAMS_SlipCustomerViewModel(ds.Tables[0].Rows[i]));
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据Id查询下发记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  AMS_SlipCustomerModel  GetSlipReleaseListById(int id)
        { 
            StringBuilder strWhere = new StringBuilder();
            if (id > 0)
            {
                strWhere.AppendFormat(" Id='{0}'", id);
            }
            try
            {
                DataSet ds = slipRelease_dal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                     
                    AMS_SlipCustomerModel model = DataRowToAMS_SlipCustomerViewModel(ds.Tables[0].Rows[0]);
                    return model;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        #region 私有方法
        AdvertManage.Model.AMS_SlipCustomerModel DataRowToAMS_SlipCustomerViewModel(DataRow dr)
        {
            AdvertManage.Model.AMS_SlipCustomerModel model = new Model.AMS_SlipCustomerModel();
            if (dr["SlipCustomerId"] != null && dr["SlipCustomerId"].ToString() != "")
            {
                model.Id = int.Parse(dr["SlipCustomerId"].ToString());
            }
            if (dr["SlipCustomerNum"] != null && dr["SlipCustomerNum"].ToString() != "")
            {
                model.Number = dr["SlipCustomerNum"].ToString();
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
    }
}
