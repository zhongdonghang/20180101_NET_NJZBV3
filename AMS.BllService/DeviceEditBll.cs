using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.IBllService;
using System.Data;
using AMS.Model.Enum;

namespace AMS.BllService
{
    public partial class AdvertManageBllService : IAdvertManageBllService
    {

        AMS.DAL.AMS_Device DeDAL = new DAL.AMS_Device();
        AMS.DAL.View_Device VDal = new DAL.View_Device();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public List<Model.AMS_Device> GeDeviceModelBySchoolNum(string schoolNum, bool flag)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                List<Model.AMS_Device> list = new List<Model.AMS_Device>();
                if (!string.IsNullOrEmpty(schoolNum))
                {
                    strWhere.AppendFormat(" SchoolNumber='{0}' ", schoolNum);
                    if (flag)
                    {
                        strWhere.AppendFormat(" and Flag='{0}'", flag);
                    }
                    DataSet ds = VDal.GetList(strWhere.ToString());
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Model.AMS_Device model = DataRowToAMS_DeviceModel(ds.Tables[0].Rows[i]);
                        list.Add(model);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult UpdateDeviceModel(Model.AMS_Device model)
        {
            try
            {
                if (DeDAL.Update(model))
                {
                    return HandleResult.Successed;
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

        public Model.Enum.HandleResult UpdateDeviceStatus(string DeviceNo, DateTime stateUpdateTime)
        {
            Model.AMS_Device model = GetDevicebyNo(DeviceNo);
            if (model == null)
            {
                return HandleResult.Failed;
            }
            model.CaputreTime = stateUpdateTime;
            return UpdateDeviceModel(model);
        }

        public Model.Enum.HandleResult AddDeviceModel(Model.AMS_Device model)
        {
            try
            {
                if (DeDAL.Add(model) > 0)
                {
                    return HandleResult.Successed;
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

        public List<Model.AMS_Device> GeDeviceModelByCampusNum(string campusNum)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                DataSet ds = new DataSet();
                List<Model.AMS_Device> list = new List<Model.AMS_Device>();
                if (!string.IsNullOrEmpty(campusNum))
                {
                    strWhere.AppendFormat(" CampusNumber='{0}'  ", campusNum);
                    ds = DeDAL.GetList(strWhere.ToString());
                }
                else
                {
                    ds = DeDAL.GetList(strWhere.ToString());
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Model.AMS_Device model = DataRowToAMS_DeviceModel(ds.Tables[0].Rows[i]);
                    list.Add(model);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Model.AMS_Device GetDevicebyNo(string No)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                if (!string.IsNullOrEmpty(No))
                {
                    strWhere.Append(string.Format("Number='{0}'", No));
                }
                DataSet ds = DeDAL.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToAMS_DeviceModel(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Model.AMS_Device GetDevicebyID(int id)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                if (id > 0)
                {
                    strWhere.Append(string.Format("Id={0}", id));
                }
                DataSet ds = DeDAL.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToAMS_DeviceModel(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region 私有方法
        Model.AMS_Device DataRowToAMS_DeviceModel(DataRow dr)
        {
            Model.AMS_Device model = new Model.AMS_Device();

            if (dr["Id"] != null && dr["Id"].ToString() != "")
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr["Number"] != null && dr["Number"].ToString() != "")
            {
                model.Number = dr["Number"].ToString();
            }
            if (dr["CampusId"] != null && dr["CampusId"].ToString() != "")
            {
                model.CampusId = int.Parse(dr["CampusId"].ToString());
            }
            if (dr["IsDel"] != null && dr["IsDel"].ToString() != "")
            {
                if ((dr["IsDel"].ToString() == "1") || (dr["IsDel"].ToString().ToLower() == "true"))
                {
                    model.IsDel = true;
                }
                else
                {
                    model.IsDel = false;
                }
            }
            if (dr["Flag"] != null && dr["Flag"].ToString() != "")
            {
                if ((dr["Flag"].ToString() == "1") || (dr["Flag"].ToString().ToLower() == "true"))
                {
                    model.Flag = true;
                }
                else
                {
                    model.Flag = false;
                }
            }
            if (dr["Describe"] != null && dr["Describe"].ToString() != "")
            {
                model.Describe = dr["Describe"].ToString();
            }
            if (dr["CaputrePath"] != null && dr["CaputrePath"].ToString() != "")
            {
                model.CaputrePath = dr["CaputrePath"].ToString();
            }
            if (dr["CaputreTime"] != null && dr["CaputreTime"].ToString() != "")
            {
                model.CaputreTime = DateTime.Parse(dr["CaputreTime"].ToString());
            }
            return model;
        }
        #endregion
    }
}
