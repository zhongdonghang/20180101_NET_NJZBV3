using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.BLL
{
    /// <summary>
    ///  
    /// </summary>
   public class AMS_CampusBLL
    {
        /// <summary>
        /// 根据Id获取校区信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
      public static Model.AMS_CampusModel GetCampusInfoByID(int id)
       {
           IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
           bool error = false;
           try
           {
               return advertService.GetCampusInfoByID(id);
           }
           catch (Exception ex)
           {
               error = true;
               SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据Id获取校区信息失败，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
               throw ex;
           }
           finally
           {
               ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
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
        /// 根据编号获取校区信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns> 
      public static Model.AMS_CampusModel GetCampusInfoByNum(string number)
      { 
        IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
           bool error = false;
           try
           {
               return advertService.GetCampusInfoByNum(number);
           }
           catch (Exception ex)
           {
               error = true;
               SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据编号获取校区信息失败，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
               throw ex;
           }
           finally
           {
               ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
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
        /// 根据学校编号获取校区信息
        /// </summary>
        /// <param name="schoolNumber"></param>
        /// <returns></returns> 
      public static List<Model.AMS_CampusModel> GetCampusInfoListBySchoolNum(string schoolNumber)
      {
          IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
          bool error = false;
          try
          {
              return advertService.GetCampusInfoListBySchoolNum(schoolNumber);
          }
          catch (Exception ex)
          {
              error = true;
              SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校编号获取校区信息失败，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
              throw ex;
          }
          finally
          {
              ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
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
        /// 根据学校Id获取学校信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
      public static  List<Model.AMS_CampusModel> GetCampusInfoListBySchoolId(int id)
      {
          IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
          bool error = false;
          try
          {
              return advertService.GetCampusInfoListBySchoolId(id);
          }
          catch (Exception ex)
          {
              error = true;
              SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校Id获取学校信息失败，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
              throw ex;
          }
          finally
          {
              ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
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
      public static Model.Enum.HandleResult AddCampus(Model.AMS_CampusModel model)
      {
          IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
          bool error = false;
          try
          {
              return advertService.AddCampus(model);
          }
          catch (Exception ex)
          {
              error = true;
              SeatManage.SeatManageComm.WriteLog.Write(string.Format("添加校区信息失败，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
              throw ex;
          }
          finally
          {
              ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
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
        /// <param name="id"></param>
        /// <returns></returns> 
      public static Model.Enum.HandleResult DeleteCampus(int id)
      {
          IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
          bool error = false;
          try
          {
              return advertService.DeleteCampus(id);
          }
          catch (Exception ex)
          {
              error = true;
              SeatManage.SeatManageComm.WriteLog.Write(string.Format("删除校区信息失败，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
              throw ex;
          }
          finally
          {
              ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
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
      public static Model.Enum.HandleResult UpdateCampus(Model.AMS_CampusModel model)
      {
          IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
          bool error = false;
          try
          {
              return advertService.UpdateCampus(model);
          }
          catch (Exception ex)
          {
              error = true;
              SeatManage.SeatManageComm.WriteLog.Write(string.Format("更新校区信息失败，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
              throw ex;
          }
          finally
          {
              ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
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
