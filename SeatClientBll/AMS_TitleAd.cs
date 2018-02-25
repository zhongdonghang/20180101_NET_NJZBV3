using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;

namespace SeatManage.Bll
{
    public class AMS_TitleAd
    {
        /// <summary>
        ///获取当前生效的冠名广告
        /// </summary>
        /// <returns></returns>
        public static SeatManage.ClassModel.TitleAdvertInfo GetTitleAdvertInfo()
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.GetTitleAdvertInfo();
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取当前生效的冠名广告失败：" + ex.Message);
                return null;
            }
            finally
            {
                ICommunicationObject ICommObjectService = seatService as ICommunicationObject;
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
        ///获取过期的冠名广告
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.TitleAdvertInfo> GetTitleAdvertOverTime()
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.GetTitleAdOverTime();
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取过期的冠名广告失败：" + ex.Message);
                return new List<ClassModel.TitleAdvertInfo>();
            }
            finally
            {
                ICommunicationObject ICommObjectService = seatService as ICommunicationObject;
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
        /// 添加冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult AddTitleAdvert(SeatManage.ClassModel.TitleAdvertInfo model)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.AddTitleAdvert(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加冠名广告失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            finally
            {
                ICommunicationObject ICommObjectService = seatService as ICommunicationObject;
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
        /// 删除冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult DeleteTitleAdvert(SeatManage.ClassModel.TitleAdvertInfo model)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.DeleteTitleAdvert(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除冠名广告失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            finally
            {
                ICommunicationObject ICommObjectService = seatService as ICommunicationObject;
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

        public static TitleAdvertInfo GetTitleModel(string Num)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.GetTitleModel(Num);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除冠名广告失败：" + ex.Message);
                return null;
            }
            finally
            {
                ICommunicationObject ICommObjectService = seatService as ICommunicationObject;
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

        public static SeatManage.EnumType.HandleResult UpdateTitleAdvert(TitleAdvertInfo model)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.UpdateTitleAdvert(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加冠名广告失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            finally
            {
                ICommunicationObject ICommObjectService = seatService as ICommunicationObject;
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
