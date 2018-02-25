using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;
using System.Data.SqlClient;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        SeatManage.DAL.SlipCustomer slipCustomerDal = new SeatManage.DAL.SlipCustomer();
        /// <summary>
        /// 获取有效期内的优惠券客户
        /// </summary>
        /// <returns></returns>
        public List<SeatManage.ClassModel.AMS_SlipCustomer> GetSlipCustomerList(string campusNum)
        {
            List<SeatManage.ClassModel.AMS_SlipCustomer> list = new List<SeatManage.ClassModel.AMS_SlipCustomer>();
            string strWhere = "";
            if (string.IsNullOrEmpty(campusNum))
            {
                strWhere = string.Format("datediff(day,EffectDate,@NowDate)>=0 and datediff(day,EndDate,@NowDate)<=0 ", campusNum);
            }
            else
            {
                strWhere = string.Format("CampusNum='{0}' and datediff(day,EffectDate,@NowDate)>=0 and datediff(day,EndDate,@NowDate)<=0 ", campusNum);
            }
            SqlParameter[] parameters = { 
                                            new SqlParameter("@NowDate",SqlDbType.DateTime)
                                        };
            parameters[0].Value = GetServerDateTime();
            try
            {
                DataSet ds = slipCustomerDal.GetList(strWhere, parameters);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SeatManage.ClassModel.AMS_SlipCustomer model = DataRowToSlipCustomer(ds.Tables[0].Rows[i]);
                    list.Add(model);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新优惠券客户
        /// </summary>
        /// <param name="slipCustomer"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult UpdateSlipCustomer(SeatManage.ClassModel.AMS_SlipCustomer slipCustomer)
        {
            try
            {
                bool i = slipCustomerDal.Update(slipCustomer);
                if (i)
                {
                    return SeatManage.EnumType.HandleResult.Successed;
                }
                else
                {
                    return SeatManage.EnumType.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加一个优惠券客户。
        /// </summary>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult AddSlipCustomer(SeatManage.ClassModel.AMS_SlipCustomer slipCustomer)
        {
            try
            {
                bool i = slipCustomerDal.Add(slipCustomer);
                if (i)
                {
                    return SeatManage.EnumType.HandleResult.Successed;
                }
                else
                {
                    return SeatManage.EnumType.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据编号获取优惠券信息
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public SeatManage.ClassModel.AMS_SlipCustomer GetSlipCustomerByNum(string num)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" No='{0}'", num);
            DataSet ds = slipCustomerDal.GetList(strWhere.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToSlipCustomer(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        /// <summary>
        /// 删除优惠券
        /// </summary>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult DeleteSlipCustomer(SeatManage.ClassModel.AMS_SlipCustomer model)
        {
            try
            {
                bool i = slipCustomerDal.Delete(model.Id);
                if (i)
                {
                    return SeatManage.EnumType.HandleResult.Successed;
                }
                else
                {
                    return SeatManage.EnumType.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加打印次数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult AddSlipCustomerPrintCount(string num)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.AppendFormat(" No='{0}'", num);
                DataSet ds = slipCustomerDal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SeatManage.ClassModel.AMS_SlipCustomer slip = DataRowToSlipCustomer(ds.Tables[0].Rows[0]);
                    slip.PrintAmount++;
                    return UpdateSlipCustomer(slip);

                }
                else
                {
                    return SeatManage.EnumType.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加优惠券查看次数
        /// </summary>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult AddSlipCustomerLookCount(string num)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.AppendFormat(" No='{0}'", num);
                DataSet ds = slipCustomerDal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SeatManage.ClassModel.AMS_SlipCustomer slip = DataRowToSlipCustomer(ds.Tables[0].Rows[0]);
                    slip.LookOverAmount++;
                    return UpdateSlipCustomer(slip);

                }
                else
                {
                    return SeatManage.EnumType.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取过期的优惠券客户信息
        /// </summary>
        /// <returns></returns>
        public List<SeatManage.ClassModel.AMS_SlipCustomer> GetSlipCustomerListOverTime(SeatManage.EnumType.LogStatus status)
        {
            List<SeatManage.ClassModel.AMS_SlipCustomer> list = new List<SeatManage.ClassModel.AMS_SlipCustomer>();
            string strWhere = "";
            if (status == SeatManage.EnumType.LogStatus.Fail)
            {
                strWhere = " datediff(day,EndDate,@NowDate)>0 ";
            }
            SqlParameter[] parameters = { 
                                            new SqlParameter("@NowDate",SqlDbType.DateTime)
                                        };
            parameters[0].Value = GetServerDateTime();
            try
            {
                DataSet ds = slipCustomerDal.GetList(strWhere, parameters);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SeatManage.ClassModel.AMS_SlipCustomer model = DataRowToSlipCustomer(ds.Tables[0].Rows[i]);
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
        private SeatManage.ClassModel.AMS_SlipCustomer DataRowToSlipCustomer(DataRow dr)
        {
            SeatManage.ClassModel.AMS_SlipCustomer model = new SeatManage.ClassModel.AMS_SlipCustomer();
            model.Id = int.Parse(dr["Id"].ToString());
            model.CampusNum = dr["CampusNum"].ToString();
            model.CustomerLogo = dr["CustomerImage"].ToString();
            model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            model.ImageName = dr["ImageUrl"].ToString();
            model.Name = dr["Name"].ToString();
            model.No = dr["No"].ToString();
            model.PrintAmount = int.Parse(dr["PrintAmount"].ToString());
            model.LookOverAmount = int.Parse(dr["LookOverAmount"].ToString());
            model.SlipTemplate = dr["SlipTemplate"].ToString();
            model.Type = int.Parse(dr["Type"].ToString());
            model.Num = dr["Num"].ToString();
            if (dr["IsPrint"] != null && !string.IsNullOrEmpty(dr["IsPrint"].ToString()))
            {
                model.IsPrint = bool.Parse(dr["IsPrint"].ToString());
            }
            return model;
        }
        #endregion
    }


}
