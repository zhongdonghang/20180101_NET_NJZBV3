using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatBespeakException;
using System.ServiceModel;

namespace SeatManage.PocketBespeak
{
    public class PocketBespeak_Login : SeatManage.IPocketBespeak.ILogin
    {
        /// <summary>
        /// 获取本地学校信息
        /// </summary>
        /// <returns></returns>
        public List<AMS.Model.AMS_School> GetAllSchoolFromLocal()
        {
            return AMS.ServiceProxy.AMS_SchoolProxy.GetAllSchool();
        }

        /// <summary>
        /// 获取单个学校信息
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public AMS.Model.AMS_School GetSingleSchoolInfo(string schoolId)
        {
            return AMS.ServiceProxy.AMS_SchoolProxy.GetSchoolById(int.Parse(schoolId));
        }

        /// <summary>
        /// 从学校获取读者 信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        public ClassModel.ReaderInfo CheckAndGetReaderInfo(ClassModel.UserInfo user, AMS.Model.AMS_School school)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.CheckAndGetReaderInfo(user);
            }
            catch (LoginFailed ex)
            {
                throw ex;
            }
            catch (ReaderHandlerFailed ex)
            {
                throw ex;
            }
            catch (EndpointNotFoundException ex)
            {
                throw new RemoteServiceLinkFailed();
            }
            catch (CommunicationException ex)
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
        ///  获取读者信息，并查询读者当前记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        public ClassModel.ReaderInfo GetReaderInfoByCardNo(string cardNo, AMS.Model.AMS_School school)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetReaderInfoByCardNo(cardNo);
            }
            catch (LoginFailed ex)
            {
                throw ex;
            }
            catch (ReaderHandlerFailed ex)
            {
                throw ex;
            }
            catch (EndpointNotFoundException ex)
            {
                throw new RemoteServiceLinkFailed();
            }
            catch (CommunicationException ex)
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
        ///  获取读者信息，并查询读者信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        public ClassModel.ReaderInfo GetReaderInfoByCardNofalse(string cardNo, AMS.Model.AMS_School school)
        {
            SeatManage.IPocketBespeakBllService.IPocketBespeakBllService pocketBespeak = BespeakServiceConnProxy.BespeakServiceConnProxy.CreateChannelPocketBespeakBllService(school.ConnectionString);
            try
            {
                return pocketBespeak.GetReaderInfoByCardNo(cardNo);
            }
            catch (LoginFailed ex)
            {
                throw ex;
            }
            catch (ReaderHandlerFailed ex)
            {
                throw ex;
            }
            catch (EndpointNotFoundException ex)
            {
                throw new RemoteServiceLinkFailed();
            }
            catch (CommunicationException ex)
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
    }
}
