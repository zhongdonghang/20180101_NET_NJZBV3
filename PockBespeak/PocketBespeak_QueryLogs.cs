using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IPocketBespeak;
using System.ServiceModel;

namespace SeatManage.PocketBespeak
{
    public class PocketBespeak_QueryLogs : IQueryLogs
    {
        /// <summary>
        /// 获取读者违规信息
        /// </summary>
        /// <param name="school"></param>
        /// <param name="cardNo"></param>
        /// <param name="readingRoomID"></param>
        /// <param name="queryDays"></param>
        /// <returns></returns>
        public List<ClassModel.ViolationRecordsLogInfo> GetViolateDiscipline(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDays)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetViolateDiscipline(cardNo, readingRoomID, queryDays);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = pocketBespeak as ICommunicationObject;
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
        /// 获取进出记录
        /// </summary>
        /// <param name="school"></param>
        /// <param name="cardNo"></param>
        /// <param name="readingRoomID"></param>
        /// <param name="queryDays"></param>
        /// <returns></returns>
        public List<ClassModel.EnterOutLogInfo> GetEnterOutLogs(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDays)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetEnterOutLogs(cardNo, readingRoomID, queryDays);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = pocketBespeak as ICommunicationObject;
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
        /// 过去预约记录
        /// </summary>
        /// <param name="school"></param>
        /// <param name="cardNo"></param>
        /// <param name="readingRoomID"></param>
        /// <param name="queryDays"></param>
        /// <returns></returns>
        public List<ClassModel.BespeakLogInfo> GetBookLogs(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDays)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetBookLogs(cardNo, readingRoomID, queryDays);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = pocketBespeak as ICommunicationObject;
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
        /// 更新预约状态
        /// </summary>
        /// <param name="school"></param>
        /// <param name="bookNo"></param>
        /// <returns></returns>
        public bool UpdateBookLogsState(AMS.Model.AMS_School school, int bookNo)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.UpdateBookLogsState(bookNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = pocketBespeak as ICommunicationObject;
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
        /// 获取黑名单信息
        /// </summary>
        /// <param name="school"></param>
        /// <param name="cardNo"></param>
        /// <param name="queryDays"></param>
        /// <returns></returns>
        public List<ClassModel.BlackListInfo> GetBlackList(AMS.Model.AMS_School school, string cardNo, int queryDays)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetBlackList(cardNo, queryDays);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = pocketBespeak as ICommunicationObject;
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


        public List<ClassModel.ReadingRoomInfo> GetAllReadingRoomInfo(AMS.Model.AMS_School school)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetAllReadingRoomInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = pocketBespeak as ICommunicationObject;
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
