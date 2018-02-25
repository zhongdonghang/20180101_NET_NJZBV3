using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.ServiceProxy
{
    public class ProjecVersionWindow
    {
        /// <summary>
        /// 获取系统版本记录信息的操作。
        /// </summary>
        /// <returns></returns>
        public static List<AMS.Model.ProgramUpgrade> GetProjectList()
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.GetProgramUpgradeList();
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
        /// 根据类型获取对应的程序信息
        /// </summary>
        /// <param name="programType"></param>
        /// <returns></returns>
        public static AMS.Model.ProgramUpgrade GetProgramInfoByProgramType(Model.Enum.SeatManageSubsystem programType)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.GetProgramInfoByProgramType(programType);
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
        public static Model.ProgramUpgrade GetProgramUpgradeByID(int id)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.GetProgramUpgradeByID(id);
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
