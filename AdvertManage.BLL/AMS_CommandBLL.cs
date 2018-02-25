using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.BLL
{
   public class AMS_CommandBLL
    {
       /// <summary>
       /// 根据学校编号获取有效的下发记录
       /// </summary>
       /// <param name="schoolNum"></param>
       /// <returns></returns>
       public static List<AdvertManage.Model.AMS_CommandListModel> GetCommandListBySchoolNum(string schoolNum)
       {
           IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
           bool error = false;
           try
           {
               return advertService.GetCommandListBySchoolNum(schoolNum);
           }
           catch (Exception ex)
           {
               error = true;
               SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据Id获取下发记录失败，异常来自：{0}；信息：{1}",ex.Source, ex.Message));
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
       /// 更新命令完成结果
       /// </summary>
       /// <param name="id">命令Id</param>
       /// <returns></returns> 
       public static AdvertManage.Model.Enum.HandleResult UpdateFinishFlag(AdvertManage.Model.AMS_CommandListModel model)
       {
           IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
           bool error = false;
           try
           {
               return advertService.UpdateFinishFlag(model);
           }
           catch (Exception ex)
           {
               error = true;
               SeatManage.SeatManageComm.WriteLog.Write(string.Format("命令完成结果更新失败 ，异常来自：{0}；信息：{1}",ex.Source, ex.Message));
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
      /// 添加命令
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      public static AdvertManage.Model.Enum.HandleResult AddAMS_CommandList(AdvertManage.Model.AMS_CommandListModel model)
      {
          IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
          bool error = false;
          try
          {
              return advertService.AddAMS_CommandList(model);
          }
          catch (Exception ex)
          {
              error = true;
              SeatManage.SeatManageComm.WriteLog.Write(string.Format("添加下发命令失败 ，异常来自：{0}；信息：{1}",ex.Source, ex.Message));
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
       /// 根据条件获取相关记录
       /// </summary>
       /// <param name="schoolId"></param>
       /// <param name="commandType"></param>
       /// <param name="handleResult"></param>
       /// <returns></returns>
      public static List<AdvertManage.Model.AMS_CommandListModel> GetCommandListByCondition(int schoolId, Model.Enum.CommandType commandType, Model.Enum.CommandHandleResult handleResult)
      {
          IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
          bool error = false;
          try
          {
              return advertService.GetCommandListByCondition(schoolId, commandType, handleResult);
          }
          catch (Exception ex)
          {
              error = true;
              SeatManage.SeatManageComm.WriteLog.Write(string.Format("添加下发命令失败 ，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
