using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatBespeakException;
using System.ServiceModel;

namespace SeatManage.PocketBespeak
{
    public class PocketBespeak_WaitSeat : SeatManage.IPocketBespeak.IWaitSeat
    {
        public List<ClassModel.ReadingRoomInfo> GetCanWaitSeatRoomInfo(AMS.Model.AMS_School school)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetCanWaitSeatRoomInfo();
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

        public List<ClassModel.Seat> GetWaitSeatList(AMS.Model.AMS_School school, string RoomId)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetWaitSeatList(RoomId);
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


        public string SubmitWaitInfo(AMS.Model.AMS_School school, ClassModel.WaitSeatLogInfo waitInfo)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.SubmitWaitInfo(waitInfo);
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


        public bool IsCanWaitSeat(AMS.Model.AMS_School school, string CardNo, string roomNo)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.IsCanWaitSeat(CardNo, roomNo);
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

        public string CancelWait(AMS.Model.AMS_School school, ClassModel.WaitSeatLogInfo waitInfo)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.CancelWait(waitInfo);
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
