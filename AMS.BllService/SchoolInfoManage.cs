using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.IBllService;
using System.Data;
using AMS.Model;

namespace AMS.BllService
{
    public partial class AdvertManageBllService : IAdvertManageBllService
    {
        AMS.DAL.AMS_School dal_School = new DAL.AMS_School();
        AMS.DAL.AMS_Campus dal_campus = new DAL.AMS_Campus();
        AMS.DAL.AMS_Device dal_device = new DAL.AMS_Device();
        public List<Model.AMS_ProvinceSchoolInfo> GetSchoolList()
        {
            try
            {
                List<AMS_ProvinceSchoolInfo> modelList = new List<AMS_ProvinceSchoolInfo>();
                List<AMS_Province> pList = new List<AMS_Province>();
                List<AMS_School> schoolList = new List<AMS_School>();
                List<AMS_Campus> campusList = new List<AMS_Campus>();
                List<AMS_Device> deviceList = new List<AMS_Device>();
                //获取设备信息
                DataSet deviceDs = dal_device.GetList("");
                for (int i = 0; i < deviceDs.Tables[0].Rows.Count; i++)
                {
                    AMS_Device model = DataRowToDeviceModel(deviceDs.Tables[0].Rows[i]);
                    deviceList.Add(model);
                }
                //获取校区信息
                DataSet campusDs = dal_campus.GetList("");
                for (int i = 0; i < campusDs.Tables[0].Rows.Count; i++)
                {
                    AMS_Campus model = DataRowToCampus(campusDs.Tables[0].Rows[i]);
                    //遍历设备列表中所有的设备，判断所属校区Id是否为当前处理的校区Id，
                    //如果是就添加到校区的设备列表中，并从当前列表中移除
                    for (int j = 0; j < deviceList.Count; )
                    {
                        AMS_Device d = deviceList[j];
                        if (d.CampusId == model.Id)
                        {
                            model.Device.Add(d);
                            deviceList.RemoveAt(j);//如果该项符合条件，则删除，j不增加
                        }
                        else
                        {//如果该项不符合条件，则j+1
                            j++;
                        }
                    }
                    campusList.Add(model);
                }
                //获取学校信息,并把校区添加到对应的学校中
                DataSet schoolDs = dal_School.GetList("");
                for (int i = 0; i < schoolDs.Tables[0].Rows.Count; i++)
                {
                    AMS_School schoolModel = DataRowToSchoolModel(schoolDs.Tables[0].Rows[i]);
                    for (int j = 0; j < campusList.Count; )
                    {
                        AMS_Campus c = campusList[j];
                        if (c.SchoolId == schoolModel.Id)
                        {
                            schoolModel.Campus.Add(c);
                            campusList.RemoveAt(j);
                        }
                        else
                        {
                            j++;
                        }
                    }
                    schoolList.Add(schoolModel);
                }

                //获取省份信息
                DataSet provinceDs = dal_Province.GetList(null);
                for (int i = 0; i < provinceDs.Tables[0].Rows.Count; i++)
                {
                    AMS_Province pModel = DataRowToAMS_ProvinceModel(provinceDs.Tables[0].Rows[i]);//转换省份信息。 
                    AMS_ProvinceSchoolInfo psi = new AMS_ProvinceSchoolInfo();
                    psi.ID = pModel.ID;
                    psi.ProvinceName = pModel.ProvinceName;
                    psi.Remark = pModel.Remark;
                    for (int j = 0; j < schoolList.Count; )
                    {
                        AMS_School s = schoolList[j];
                        if (s.ProvinceID == psi.ID)
                        {
                            psi.Schools.Add(s);
                            schoolList.RemoveAt(j);
                        }
                        else
                        {
                            j++;
                        }
                    }
                    modelList.Add(psi);
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加学校信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddSchoolInfo(Model.AMS_School model)
        {
            try
            {
                AMS_School school = dal_School.GetModel(string.Format(" Number='{0}' ", model.Number));
                if (school != null)
                {
                    return "重复的学校编号";
                }
                else
                {
                    if (dal_School.Add(model))
                    {
                        return "";
                    }
                    else
                    {
                        return "未知原因，添加失败";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新学校信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateSchoolInfo(Model.AMS_School model)
        {
            try
            {
                AMS_School school = dal_School.GetModel(string.Format(" Number='{0}' ", model.Number));
                if (school != null && school.Id != model.Id)
                {
                    return "学校编号重复";
                }
                else
                {
                    if (dal_School.Update(model))
                    {
                        return "";
                    }
                    else
                    {
                        return "未知原因，添加失败";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除学校信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string DeleteSchoolInfo(Model.AMS_School model)
        {
            try
            {
                if (dal_School.Delete(model.Id))
                {
                    return "";
                }
                else
                {
                    return "未知原因，删除失败";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加校区信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddCampusInfo(Model.AMS_Campus model)
        {
            try
            {
                AMS_Campus campus = dal_campus.GetModel(string.Format(" Number='{0}'", model.Number));
                if (campus != null)
                {
                    return "添加失败，校区编号重复";
                }
                else
                {
                    dal_campus.Add(model);
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("添加学校信息失败：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 更新校区信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateCampusInfo(Model.AMS_Campus model)
        {
            try
            {
                AMS_Campus campus = dal_campus.GetModel(model.Id);
                if (campus == null)
                {
                    return "更新失败，校区信息不存在";
                }
                else
                {
                    dal_campus.Update(model);
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("校区信息更新失败：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 删除校区信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string DeleteCampusInfo(Model.AMS_Campus model)
        {
            try
            {
                dal_campus.Delete(model.Id);
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("添加学校信息失败：{0}", ex.Message));
            }
        }

        public string AddDevice(Model.AMS_Device model)
        {
            try
            {
                AMS_Device device = dal_device.GetModel(string.Format(" Number='{0}'", model.Number));
                if (device != null)
                {
                    return "添加失败，设备编号已存在";
                }
                else
                {
                    dal_device.Add(model);
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("添加设备信息失败：{0}", ex.Message));
            }
        }

        public string UpdateDevice(Model.AMS_Device model)
        {
            try
            {
                dal_device.Update(model);
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("添加设备信息失败：{0}", ex.Message));
            }
        }

        public string DeleteDevice(Model.AMS_Device model)
        {
            try
            {
                dal_device.Delete(model.Id);
                return "";

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("添加设备信息失败：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 私有方法
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private AMS_School DataRowToSchoolModel(DataRow dataRow)
        {
            try
            {
                AMS_School model = new AMS_School();
                model.Id = int.Parse(dataRow["Id"].ToString().Trim());
                model.Number = dataRow["Number"].ToString().Trim();
                model.Name = dataRow["Name"].ToString().Trim();
                model.ProvinceID = int.Parse(dataRow["ProvinceID"].ToString().Trim());
                model.CardInfo = dataRow["CardInfo"].ToString().Trim();
                model.ConnectionString = dataRow["ConnectionString"].ToString().Trim();
                model.Describe = dataRow["Describe"].ToString().Trim();
                model.DTUip = dataRow["DTUip"].ToString().Trim();
                model.ExecuteProgress = dataRow["ExecuteProgress"].ToString().Trim();
                model.InstallDate = dataRow["InstallDate"].ToString().Trim();
                model.InstallMan = dataRow["InstallMan"].ToString().Trim();
                model.InterfaceInfo = dataRow["InterfaceInfo"].ToString().Trim();
                model.IsSeatBespeak = SeatManage.SeatManageComm.SeatComm.ConvertToBoolen(dataRow["IsSeatBespeak"].ToString().Trim());
                model.LinkAddress = dataRow["LinkAddress"].ToString().Trim();
                model.LinkMan = dataRow["LinkMan"].ToString().Trim();
                model.IsSeatBespeak = dataRow["Flag"].ToString() == "1" ? true : false;
                model.AppOpen = dataRow["AppOpen"].ToString() == "1";
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 设备对象行转换为model
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private AMS_Device DataRowToDeviceModel(DataRow dataRow)
        {
            try
            {
                string tempDate = "";
                AMS_Device model = new AMS_Device();
                model.CampusId = int.Parse(dataRow["CampusId"].ToString().Trim());
                model.CaputrePath = dataRow["CaputrePath"].ToString().Trim();

                tempDate = dataRow["CaputreTime"].ToString().Trim();
                if (!string.IsNullOrEmpty(tempDate))
                {
                    model.CaputreTime = DateTime.Parse(tempDate);
                }
                model.Describe = dataRow["Describe"].ToString().Trim();
                tempDate = dataRow["DeviceType"].ToString().Trim();
                if (!string.IsNullOrEmpty(tempDate))
                {
                    model.DeviceType = int.Parse(tempDate);
                }
                model.Flag = SeatManage.SeatManageComm.SeatComm.ConvertToBoolen(dataRow["Flag"].ToString().Trim());
                model.Id = int.Parse(dataRow["Id"].ToString().Trim());
                model.IsDel = SeatManage.SeatManageComm.SeatComm.ConvertToBoolen(dataRow["IsDel"].ToString().Trim());
                model.Number = dataRow["Number"].ToString().Trim();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// DataRow转换为校区
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private AMS_Campus DataRowToCampus(DataRow dataRow)
        {
            try
            {
                AMS_Campus model = new AMS_Campus();
                model.Address = dataRow["Address"].ToString().Trim();
                model.Describe = dataRow["Describe"].ToString().Trim();
                model.Id = int.Parse(dataRow["Id"].ToString().Trim());
                model.Name = dataRow["Name"].ToString().Trim();
                model.Number = dataRow["Number"].ToString().Trim();
                model.SchoolId = int.Parse(dataRow["SchoolId"].ToString().Trim());
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
