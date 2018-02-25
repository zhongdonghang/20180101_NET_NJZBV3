﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace SeatManage.Bll
{
    public class T_SM_PrintTemplate
    {
        /// <summary>
        /// 获取打印模板
        /// </summary>
        /// <returns></returns>
        public static string GetPrintTemplate(string CardNo)
        {

            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.GetPrintTemplate(CardNo);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取打印模板失败：" + ex.Message);
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
        /// 获取打印模板
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.AMS_PrintTemplateModel> GetPrintTemplateOverTime()
        {

            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.GetPrintTemplateOverTime();
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取过期打印模板失败：" + ex.Message);
                return new List<ClassModel.AMS_PrintTemplateModel>();
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
        public static EnumType.HandleResult AddPrintTemplate(SeatManage.ClassModel.AMS_PrintTemplateModel model)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.AddPrintTemplate(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加打印模板失败：" + ex.Message);
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
        public static EnumType.HandleResult DeletePrintTemplate(SeatManage.ClassModel.AMS_PrintTemplateModel model)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.DeletePrintTemplate(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除打印模板失败：" + ex.Message);
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

        public static AMS_PrintTemplateModel GetPrintTemplateByNum(string Num)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.GetPrintTemplateByNum(Num);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除打印模板失败：" + ex.Message);
                return null ;
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

        public static HandleResult UpdatePrintTemplate(AMS_PrintTemplateModel model)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool error = false;
            try
            {
                return seatService.UpdatePrintTemplate(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加打印模板失败：" + ex.Message);
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
