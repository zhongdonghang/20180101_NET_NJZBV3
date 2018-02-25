using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.IWCFService;
using System.Data;
using System.Data.SqlClient;
using AdvertManage.Model.Enum;

namespace AdvertManage.WCFService
{
    public partial class AdvertManageService : IAdvertManageService
    {
        AdvertManage.DAL.AMS_DeviceDal deviceModel = new DAL.AMS_DeviceDal();
        /// <summary>
        /// 根据学校编号获取设备列表
        /// 参数flag为数据中转服务需要获取和更新的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public List<AdvertManage.Model.AMS_DeviceModel> GeDeviceModelBySchoolNum(string schoolNum, bool flag)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                List<AdvertManage.Model.AMS_DeviceModel> list = new List<Model.AMS_DeviceModel>();
                if (!string.IsNullOrEmpty(schoolNum))
                {
                    strWhere.AppendFormat(" SchoolNumber='{0}' ",schoolNum);
                    if (flag)
                    {
                        strWhere.Append(" and Flag=@Flag");
                    }
                    SqlParameter[] parameters = {
                                            new SqlParameter("@Flag",SqlDbType.Bit), 
                                            };
                    parameters[0].Value = flag;
                    DataSet ds = deviceModel.GetList(strWhere.ToString(), parameters);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Model.AMS_DeviceModel model = DataRowToAMS_DeviceModel(ds.Tables[0].Rows[i]);
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
        /// 更新设备处理结果
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public HandleResult UpdateDeviceModel(AdvertManage.Model.AMS_DeviceModel model)
        {
            try
            {
                if (deviceModel.Update(model))
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


        public List<Model.AMS_DeviceModel> GeDeviceModelByCampusNum(string campusNum)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                DataSet ds = new DataSet();
                List<AdvertManage.Model.AMS_DeviceModel> list = new List<Model.AMS_DeviceModel>();
                if (!string.IsNullOrEmpty(campusNum))
                {
                    strWhere.Append(" CampusNumber=@CampusNumber  ");
                    SqlParameter[] parameters = {
                                            new SqlParameter("@CampusNumber",SqlDbType.NVarChar), 
                                            };
                    parameters[0].Value = campusNum;
                    ds = deviceModel.GetList(strWhere.ToString(), parameters);
                }
                else
                {
                    ds = deviceModel.GetList(strWhere.ToString(), null);
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Model.AMS_DeviceModel model = DataRowToAMS_DeviceModel(ds.Tables[0].Rows[i]);
                    list.Add(model);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HandleResult AddDeviceModel(Model.AMS_DeviceModel model)
        {
            try
            {
                if (deviceModel.Add(model) > 0)
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
        /// <summary>
        /// 根据设备编号获取设备信息
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public AdvertManage.Model.AMS_DeviceModel GetDevicebyNo(string no)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                if (!string.IsNullOrEmpty(no))
                {
                    strWhere.Append(string.Format("Number='{0}'", no));
                }
                DataSet ds = deviceModel.GetList(strWhere.ToString(), null);
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
        /// <summary>
        /// 根据设备id获取设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AdvertManage.Model.AMS_DeviceModel GetDevicebyid(int id)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                if (id > 0)
                {
                    strWhere.Append(string.Format("Id='{0}'", id));
                }
                DataSet ds = deviceModel.GetList(strWhere.ToString(), null);
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
        /// <summary>
        /// 更新设备状态
        /// </summary>
        /// <param name="DeviceNo"></param>
        /// <param name="stateUpdateTime"></param>
        /// <returns></returns>
       public  AdvertManage.Model.Enum.HandleResult UpdateDeviceStatus(string DeviceNo, DateTime stateUpdateTime)
        {
            AdvertManage.Model.AMS_DeviceModel model = GetDevicebyNo(DeviceNo);
            if (model == null)
            {
                return HandleResult.Failed;
            }
            model.CaputreTime = stateUpdateTime;
            return UpdateDeviceModel(model);
        }

        #region 私有方法
        AdvertManage.Model.AMS_DeviceModel DataRowToAMS_DeviceModel(DataRow dr)
        {
            AdvertManage.Model.AMS_DeviceModel model = new Model.AMS_DeviceModel();

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
            if (dr["SchoolId"] != null && dr["SchoolId"].ToString() != "")
            {
                model.SchoolId = int.Parse(dr["SchoolId"].ToString());
            }
            if (dr["SchoolNumber"] != null && dr["SchoolNumber"].ToString() != "")
            {
                model.SchoolNumber = dr["SchoolNumber"].ToString();
            }
            if (dr["SchooName"] != null && dr["SchooName"].ToString() != "")
            {
                model.SchooName = dr["SchooName"].ToString();
            }
            if (dr["CampusName"] != null && dr["CampusName"].ToString() != "")
            {
                model.CampusName = dr["CampusName"].ToString();
            }
            if (dr["CampusNumber"] != null && dr["CampusNumber"].ToString() != "")
            {
                model.CampusNumber = dr["CampusNumber"].ToString();
            }
            return model;
        }
        #endregion



    }
}
