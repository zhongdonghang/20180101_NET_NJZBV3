using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.ServiceProxy
{
    /// <summary>
    /// 
    /// </summary>
    public class SchoolMainWindow
    {
        /// <summary>
        /// 获取学校信息的操作。
        /// </summary>
        /// <returns></returns>
        public static List<AMS.Model.AMS_ProvinceSchoolInfo> GetSchoolList()
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.GetSchoolList();
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }

            //AMS.Model.AMS_Device device1 = new Model.AMS_Device() { Id = 1, Number = "20203301", Flag = true };
            //AMS.Model.AMS_Device device2 = new Model.AMS_Device() { Id = 2, Number = "20203302", Flag = true };
            //AMS.Model.AMS_Device device3 = new Model.AMS_Device() { Id = 3, Number = "20203303", Flag = true };
            //AMS.Model.AMS_Device device4 = new Model.AMS_Device() { Id = 4, Number = "20203304", Flag = true };
            //AMS.Model.AMS_Campus campus1 = new Model.AMS_Campus() { Id = 1, Number = "202033", Name = "厦门校区", Address = "厦门市思明区普陀山旁边" };
            //campus1.Device.Add(device1);
            //campus1.Device.Add(device2);
            //AMS.Model.AMS_Campus campus2 = new Model.AMS_Campus() { Id = 2, Number = "202034", Name = "泉州校区", Address = "泉州市未名区" };
            //campus2.Device.Add(device3);
            //AMS.Model.AMS_Campus campus3 = new Model.AMS_Campus() { Id = 3, Number = "202033", Name = "丁家桥" };

            //AMS.Model.AMS_School school = new Model.AMS_School() { Id = 1, Number = "202001", Name = "厦门大学" };
            //school.Campus.Add(campus1);
            //school.Campus.Add(campus2);
            //AMS.Model.AMS_School school2 = new Model.AMS_School() { Id = 2, Number = "202001", Name = "东南大学" };

            //List<Model.AMS_School> list = new List<Model.AMS_School>();
            //List<AMS.Model.AMS_ProvinceSchoolInfo> provinceSchools = new List<Model.AMS_ProvinceSchoolInfo>();
            //AMS.Model.AMS_ProvinceSchoolInfo provinceSchool = new Model.AMS_ProvinceSchoolInfo() { ProvinceName = "福建省", ID = 1 };
            //list.Add(school);
            //list.Add(school2);
            //provinceSchool.Schools = list;
            //provinceSchools.Add(provinceSchool);
            //return provinceSchools;
        }
        /// <summary>
        /// 添加学校
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AddSchoolInfo(AMS.Model.AMS_School model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.AddSchoolInfo(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 更新学校信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string UpdateSchoolInfo(AMS.Model.AMS_School model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.UpdateSchoolInfo(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 删除学校信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string DeleteSchoolInfo(AMS.Model.AMS_School model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.DeleteSchoolInfo(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 添加校区信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AddCampusInfo(AMS.Model.AMS_Campus model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.AddCampusInfo(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }

        /// <summary>
        /// 更新校区信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string UpdateCampusInfo(AMS.Model.AMS_Campus model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.UpdateCampusInfo(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }

        /// <summary>
        /// 删除校区信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string DeleteCampusInfo(AMS.Model.AMS_Campus model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.DeleteCampusInfo(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AddDevice(AMS.Model.AMS_Device model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.AddDevice(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string UpdateDevice(AMS.Model.AMS_Device model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.UpdateDevice(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string DeleteDevice(AMS.Model.AMS_Device model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.DeleteDevice(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }

    }
}
