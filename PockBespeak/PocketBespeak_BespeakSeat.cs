using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatBespeakException;

namespace SeatManage.PocketBespeak
{
    public class PocketBespeak_BespeakSeat : SeatManage.IPocketBespeak.IBespeakSeatListForm
    {
        /// <summary>
        /// 获取可被预约的阅览室
        /// </summary>
        /// <param name="school"></param>
        /// <param name="bespeakDate"></param>
        /// <returns></returns>
        public List<SeatManage.ClassModel.ReadingRoomInfo> GetCanBespeakReaderRoomInfo(AMS.Model.AMS_School school, DateTime bespeakDate)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetCanBespeakReaderRoomInfo(bespeakDate);
            }
            catch (Exception ex)
            {
                throw new RemoteServiceLinkFailed(ex.Message);
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


        public List<ClassModel.ReadingRoomInfo> GetCanBespeakNowDayRoomInfo(AMS.Model.AMS_School school)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetCanBespeakNowDayRoomInfo();
            }
            catch (Exception ex)
            {
                throw new RemoteServiceLinkFailed(ex.Message);
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

        public List<ClassModel.Seat> GetBookSeatList(AMS.Model.AMS_School school, DateTime bespeakDate, string RoomId)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetBookSeatList(bespeakDate, RoomId);
            }
            catch (Exception ex)
            {
                throw new RemoteServiceLinkFailed(ex.Message);
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

        public string SubmitBespeakInfo(AMS.Model.AMS_School school, ClassModel.BespeakLogInfo bespeakInfo)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.SubmitBespeakInfo(bespeakInfo);
            }
            catch (Exception ex)
            {
                throw new RemoteServiceLinkFailed(ex.Message);
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
        /// 获取座位信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="seatNum">座位号</param>
        /// <param name="readingRoomNum">阅览室编号</param>
        /// <returns></returns>
        public ClassModel.BespeakSeatModel.ScanCodeViewModel GetScanCodeSeatInfo(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="seatNum">要更换的座位号</param>
        /// <param name="readingRoomNum">阅览室编号</param>
        /// <returns></returns>
        public string ChangeSeat(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum)
        {
            throw new NotImplementedException();
        }







        public List<ClassModel.Seat> GetNowDayBookSeatList(AMS.Model.AMS_School school, string RoomId)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetNowBookSeatList(RoomId);
            }
            catch (Exception ex)
            {
                throw new RemoteServiceLinkFailed(ex.Message);
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

        public string SubmitNowDayBespeakInfo(AMS.Model.AMS_School school, ClassModel.BespeakLogInfo bespeakInfo)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.SubmitNowDayBespeakInfo(bespeakInfo);
            }
            catch (Exception ex)
            {
                throw new RemoteServiceLinkFailed(ex.Message);
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


        public string ConfrimSeat(AMS.Model.AMS_School school, int bookNo)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.ConfigSeat(bookNo);
            }
            catch (Exception ex)
            {
                throw new RemoteServiceLinkFailed(ex.Message);
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
