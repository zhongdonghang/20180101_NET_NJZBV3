using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IPocketBespeak;
using System.ServiceModel;
using SeatBespeakException;
using SeatManage.ClassModel;

namespace SeatManage.PocketBespeak
{
    public class PocketBespeak_MainFunctionPageBll : IMainFunctionPageBll, IMainFunctionPage_Ex
    {
        /// <summary>
        /// 暂离操作
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public string SetShortLeave(AMS.Model.AMS_School school, ClassModel.ReaderInfo reader)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.SetShortLeave(reader.CardNo);
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
        ///释放座位
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public string FreeSeat(AMS.Model.AMS_School school, ClassModel.ReaderInfo reader)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.FreeSeat(reader.CardNo);
            }
            catch (ReaderHandlerFailed ex)
            {
                throw ex;
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
        /// 获取读者信息
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public ClassModel.ReaderInfo GetReaderInfo(AMS.Model.AMS_School school, ClassModel.ReaderInfo reader)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetReaderInfo(reader.CardNo);
            }
            catch (ReaderHandlerFailed ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RemoteServiceLinkFailed();
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
        /// 获取阅览室状态
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public Dictionary<string, ReadingRoomSeatUsedState_Ex> GetAllRoomSeatUsedState(AMS.Model.AMS_School school)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetAllRoomSeatUsedState();
            }
            catch (ReaderHandlerFailed ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RemoteServiceLinkFailed();
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
        /// 获取读者当前状态
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns></returns> 
        public SeatManage.ClassModel.ReaderInfo GetReaderInfo(AMS.Model.AMS_School school, string cardNo)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetReaderInfo(cardNo);
            }
            catch (ReaderHandlerFailed ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RemoteServiceLinkFailed();
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
        /// 续时
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public string DelaySeatUsedTime(AMS.Model.AMS_School school, ReaderInfo reader)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.DelaySeatUsedTime(reader);
            }
            catch (ReaderHandlerFailed ex)
            {
                throw ex;
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


        public string ReaderComeBack(AMS.Model.AMS_School school, ReaderInfo reader)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.ReaderComeBack(reader);
            }
            catch (ReaderHandlerFailed ex)
            {
                throw ex;
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


        public SeatBookUsingInfo GetSeatUsingInfo(AMS.Model.AMS_School school, string readingRoomNo, string SeatNo)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetSeatBookUsingStatus(SeatNo, readingRoomNo);
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


        public string ChangeSeat(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.ChangeSeat(cardNo, seatNum, readingRoomNum);
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


        public string SelectSeat(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.SelectSeat(cardNo, seatNum, readingRoomNum);
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
