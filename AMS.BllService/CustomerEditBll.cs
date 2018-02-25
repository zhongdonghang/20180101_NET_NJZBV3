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
        AMS.DAL.AMS_AdCustomer Dal_Customer = new DAL.AMS_AdCustomer();
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <returns></returns>
        public List<Model.AMS_AdCustomer> GetAdCustomerList()
        {
            try
            {
                List<AMS.Model.AMS_AdCustomer> modellist = new List<Model.AMS_AdCustomer>();
                DataSet ds = Dal_Customer.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_AdCustomer(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddNewCustomer(Model.AMS_AdCustomer model)
        {
            try
            {
                Model.AMS_AdCustomer sameModel = Dal_Customer.GetModel(model.CompanyName);
                if (sameModel != null)
                {
                    return "该客户已存在!";
                }
                if (Dal_Customer.Add(model) == 0)
                {
                    return "添加信息失败!";
                }
                return null;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateCustomer(Model.AMS_AdCustomer model)
        {
            try
            {
                Model.AMS_AdCustomer sameModel = Dal_Customer.GetModel(model.CompanyName);
                if (sameModel != null && sameModel.ID != model.ID)
                {
                    return "该客户已存在！";
                }
                if (!Dal_Customer.Update(model))
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
        /// 删除客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string DeleteCustomer(Model.AMS_AdCustomer model)
        {
            try
            {
                if (!Dal_Customer.Delete(model.ID))
                {
                    return "删除客户失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private AMS.Model.AMS_AdCustomer DataRowToAMS_AdCustomer(DataRow dr)
        {
            AMS.Model.AMS_AdCustomer model = new Model.AMS_AdCustomer();
            model.ID = int.Parse(dr["ID"].ToString());
            model.CustomerNo = dr["CustomerNo"].ToString();
            model.CompanyName = dr["CompanyName"].ToString();
            model.LinkWay = dr["LinkWay"].ToString();
            model.Describe = dr["Describe"].ToString();
            return model;
        }
    }
}
