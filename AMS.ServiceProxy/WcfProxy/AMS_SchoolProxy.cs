using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.ServiceProxy
{
    public class AMS_SchoolProxy
    {
        /// <summary>
        /// 获取所有学校信息
        /// </summary>
        /// <returns></returns>
        public static List<AMS.Model.AMS_School> GetAllSchool()
        {
            return ICallBackInfoService.GetSchoolList();
        }

        public static AMS.Model.AMS_School GetSchoolById(int id)
        { 
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try
            {
                return bllService.GetSchoolInfoById(id);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校编号获取设备列表遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 根据编号获取学校信息
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <returns></returns>
        public static AMS.Model.AMS_School GetSchoolInfoByNum(string schoolNum)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try 
            {
                return bllService.GetSchoolInfoByNum(schoolNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校编号获取设备列表遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
