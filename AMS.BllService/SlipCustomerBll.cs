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
        DAL.View_SlipCustomer dal_SlipCustomerView = new DAL.View_SlipCustomer();
        DAL.AMS_SlipCustomer dal_SlipCustomer = new DAL.AMS_SlipCustomer();
        DAL.View_SlipReleaseCampus dal_SlipReleaseCampusView = new DAL.View_SlipReleaseCampus();
        public List<Model.AMS_SlipCustomer> GetSlipCustomerList()
        {
            try
            {
                List<Model.AMS_SlipCustomer> modellist = new List<Model.AMS_SlipCustomer>();
                DataSet ds = dal_SlipCustomerView.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_SlipCustomerModel(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AddNewSlipCustomer(Model.AMS_SlipCustomer model)
        {
            try
            {
                Model.AMS_SlipCustomer sameModel = dal_SlipCustomer.GetModel(model.Number);
                if (sameModel != null)
                {
                    return "优惠券编号重复！";
                }
                if (dal_SlipCustomer.Add(model) == 0)
                {
                    return "优惠券添加失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateSlipCustomer(Model.AMS_SlipCustomer model)
        {
            try
            {
                Model.AMS_SlipCustomer sameModel = dal_SlipCustomer.GetModel(model.Number);
                if (sameModel != null && sameModel.Id != model.Id)
                {
                    return "优惠券编号重复！";
                }
                if (!dal_SlipCustomer.Update(model))
                {
                    return "优惠券修改失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteSlipCustomer(Model.AMS_SlipCustomer model)
        {
            try
            {
                if (!dal_SlipCustomer.Delete(model.Id))
                {
                    return "优惠券删除失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// model转换
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private AMS.Model.AMS_SlipCustomer DataRowToAMS_SlipCustomerModel(DataRow dr)
        {
            //CustomerNo,CompanyName,CustomerLinkWay,CustomerDescribe,Id,Number,SlipName,ImageUrl
            //SlipTemplate,CouponsXml,OperatorLonginId,OperatorPwd,OperatorBranchName
            //OperatorName,OperatorRemark,SlipCustomerDescribe,IsPrint,Type,EndDate
            //EffectDate,CampusNum,CustomerImage
            AMS.Model.AMS_SlipCustomer model = new Model.AMS_SlipCustomer();
            model.Id = int.Parse(dr["Id"].ToString());
            model.Number = dr["Number"].ToString();
            if (dr.Table.Columns.Contains("CompanyName"))
            {
                model.CustomerName = dr["CompanyName"].ToString();
            }
            model.CustomerId = Convert.ToInt32(dr["CustomerId"]);
            model.CustomerImage = dr["CustomerImage"].ToString();
            model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            model.ImageUrl = dr["ImageUrl"].ToString();
            if (dr["IsPrint"].ToString() == "0")
            {
                model.IsPrint = false;
            }
            else
            {
                model.IsPrint = true;
            }
            model.SlipName = dr["SlipName"].ToString();
            model.SlipTemplate = dr["SlipTemplate"].ToString();
            if (dr.Table.Columns.Contains("OperatorName"))
            {
                model.OperatorName = dr["OperatorName"].ToString();
            }
            model.CampusNum = dr["CampusNum"].ToString();
            return model;
        }
        private AMS.Model.AMS_SlipCustomer DataRowToAMS_SlipReleaseCampusModel(DataRow dr)
        {
            //CustomerNo,CompanyName,CustomerLinkWay,CustomerDescribe,Id,Number,SlipName,ImageUrl
            //SlipTemplate,CouponsXml,OperatorLonginId,OperatorPwd,OperatorBranchName
            //OperatorName,OperatorRemark,SlipCustomerDescribe,IsPrint,Type,EndDate
            //EffectDate,CampusNum,CustomerImage
            AMS.Model.AMS_SlipCustomer model = new Model.AMS_SlipCustomer();
            model.Id = int.Parse(dr["Id"].ToString());
            model.Number = dr["SlipCustomerNum"].ToString();
            if (dr.Table.Columns.Contains("CompanyName"))
            {
                model.CustomerName = dr["CompanyName"].ToString();
            }
            model.CustomerImage = dr["CustomerImage"].ToString();
            model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            model.ImageUrl = dr["ImageUrl"].ToString();
            if (dr["IsPrint"].ToString() == "0")
            {
                model.IsPrint = false;
            }
            else
            {
                model.IsPrint = true;
            }
            model.SlipName = dr["SlipName"].ToString();
            model.SlipTemplate = dr["SlipTemplate"].ToString();
            if (dr.Table.Columns.Contains("OperatorName"))
            {
                model.OperatorName = dr["OperatorName"].ToString();
            }
            model.CampusNum = dr["CampusNum"].ToString();
            return model;
        }

        public Model.AMS_SlipCustomer GetSlipCustomerByNum(string Num)
        {
            try
            {
                Model.AMS_SlipCustomer modellist = new Model.AMS_SlipCustomer();
                string sql = string.Format(" Number='{0}'",Num);
                DataSet ds = dal_SlipCustomerView.GetList(sql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist=DataRowToAMS_SlipCustomerModel(ds.Tables[0].Rows[i]);
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Model.AMS_SlipCustomer GetSlipCustomerById(int ID)
        {
            try
            {
                Model.AMS_SlipCustomer modellist = new Model.AMS_SlipCustomer();
                string sql = string.Format(" ID={0}", ID);
                DataSet ds = dal_SlipReleaseCampusView.GetList(sql);
                if (ds.Tables[0].Rows.Count < 1)
                {
                    return null;
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist = DataRowToAMS_SlipReleaseCampusModel(ds.Tables[0].Rows[i]);
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
