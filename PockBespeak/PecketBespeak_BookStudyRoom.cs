using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatBespeakException;
using System.ServiceModel;

namespace SeatManage.PocketBespeak
{
    public class PecketBespeak_BookStudyRoom : SeatManage.IPocketBespeak.IBookStudyRoom
    {
        public List<ClassModel.StudyRoomInfo> GetStudyRoomList(AMS.Model.AMS_School school)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetStudyRoomList();
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

        public ClassModel.StudyRoomInfo GetStudyRoomInfo(AMS.Model.AMS_School school, string roomNo)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetStudyRoomInfo(roomNo);
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

        public string SubmitStudyLog(AMS.Model.AMS_School school, ClassModel.StudyBookingLog logModel)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.SubmitStudyLog(logModel);
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

        public List<ClassModel.StudyBookingLog> GetStudyLogList(AMS.Model.AMS_School school, string cardNo, int spanDay)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetStudyLogList(cardNo, spanDay);
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

        public ClassModel.StudyBookingLog GetStudyLog(AMS.Model.AMS_School school, int logID)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetStudyLog(logID);
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

        public string CancelStudyLog(AMS.Model.AMS_School school, ClassModel.StudyBookingLog logModel)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.CancelStudyLog(logModel);
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
