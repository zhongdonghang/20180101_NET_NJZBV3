using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.BLL
{
    public class AMS_FeedbackBLL
    {
        /// <summary>
        /// 插入反馈意见
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="remark">反馈意见</param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public static Model.Enum.HandleResult AddFeedback(Model.AMS_FeedbackModel model)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.AddFeedback(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("添加反馈意见遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 将抛出异常信息存入数据库
        /// </summary>
        /// <param name="error"></param>
        public void AddErrorMessage(string error)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            try
            {
                advertService.AddErrorMessage(error);
            }
            catch
            {
                throw;
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
        /// 网站访问量
        /// </summary>
        /// <param name="id"></param>
        public void SetPageView(string id)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            try
            {
                advertService.SetPageView(id);
            }
            catch
            {
                throw;
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
