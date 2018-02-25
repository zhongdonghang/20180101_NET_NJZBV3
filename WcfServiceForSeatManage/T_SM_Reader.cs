using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using SeatManage.DAL;
using System.Data.SqlClient;
using System.Data;
using SeatManage.EnumType;
using SeatManage.ISystemTerminal.ILoginValidate;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        T_SM_Reader t_sm_reader_Dal = new T_SM_Reader();
        /// <summary>
        /// 终端获取读者的当前的记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ReaderInfo GetReaderSeatState(string cardNo)
        {
            ReaderInfo readerInfo = GetReader(cardNo, false);
            if (readerInfo == null)
            {
                readerInfo = new ReaderInfo();
                readerInfo.CardNo = cardNo;
            }
            try
            {
                readerInfo.NoticeInfo = GetReaderNoticeByCardNoStatus(cardNo, LogStatus.Valid);
                List<WaitSeatLogInfo> waitSeatlogs = GetWaitLogList(cardNo, null, null, null, new List<EnterOutLogType> { EnterOutLogType.Waiting });
                if (waitSeatlogs.Count > 0)
                {
                    readerInfo.WaitSeatLog = waitSeatlogs[0];
                    readerInfo.WaitSeatLog.EnterOutLog = GetEnterOutLogInfoById(readerInfo.WaitSeatLog.EnterOutLogID);
                }
                else
                {
                    readerInfo.EnterOutLog = GetEnterOutLogInfoByCardNo(cardNo);
                    readerInfo.BlacklistLog = GetBlacklistInfo(cardNo);
                    if (readerInfo.EnterOutLog == null || readerInfo.EnterOutLog.EnterOutState == EnterOutLogType.Leave)
                    {
                        readerInfo.BespeakLog = GetBespeakLogInfo(cardNo, GetServerDateTime());
                    }
                }
                readerInfo.AtReadingRoom = getRoomInfoByReader(readerInfo);
                if (readerInfo.AtReadingRoom != null && readerInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.Used && readerInfo.EnterOutLog != null && !string.IsNullOrEmpty(readerInfo.EnterOutLog.EnterOutLogNo))
                {
                    readerInfo.CanContinuedTime = GetCanContinuedTimeByEOLNoRNo(readerInfo.EnterOutLog.EnterOutLogNo, readerInfo.AtReadingRoom);
                    readerInfo.ContinuedTimeCount = GetContinuedTimeCountByEOLNo(readerInfo.EnterOutLog.EnterOutLogNo);
                }
                return readerInfo;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取读者相关信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="queryLog"></param>
        /// <returns></returns>
        public ReaderInfo GetReader(string cardNo, bool queryLog)
        {
            if (string.IsNullOrEmpty(cardNo))
            {
                return null;
            }
            SeatManage.ClassModel.ReaderInfo readerInfo = new ReaderInfo(); //创建一个读者实体
            string strWhere = " [T_SM_Reader].[cardNo]=@cardNo";

            SqlParameter[] parameters =  { 
                                                 new SqlParameter("@cardNo",cardNo)
                                             };


            try
            {
                DataSet dsReader = t_sm_reader_Dal.GetList(strWhere, parameters);//获取读者的信息。
                readerInfo.CardNo = cardNo;
                if (dsReader.Tables[0].Rows.Count > 0)
                {
                    DataRowToReaderInfo(dsReader.Tables[0].Rows[0], ref readerInfo);
                }
                strWhere = string.Format(" CardNo='{0}' and BlacklistState={1}", cardNo, (int)LogStatus.Valid);//黑名单查询条件
                if (queryLog)
                {
                    readerInfo.BlacklistLog = GetBlacklistInfo(cardNo);
                    readerInfo.EnterOutLog = GetEnterOutLogInfoByCardNo(cardNo);
                    readerInfo.BespeakLog = GetBespeakLogInfo(cardNo, GetServerDateTime());
                    readerInfo.NoticeInfo = GetReaderNoticeByCardNoStatus(cardNo, LogStatus.Valid);
                    readerInfo.CanContinuedTime = GetCanContinuedTime(cardNo);
                    readerInfo.ContinuedTimeCount = GetContinuedTimeCount(cardNo);
                    readerInfo.PecketWebSetting = GetPecketBookWebSetting();
                    //TODO:读者的等待记录
                    List<EnterOutLogType> logType = new List<EnterOutLogType>();
                    logType.Add(EnterOutLogType.Waiting);
                    List<WaitSeatLogInfo> waitSeatlogs = GetWaitLogList(cardNo, null, null, null, logType);
                    if (waitSeatlogs.Count > 0)
                    {
                        readerInfo.WaitSeatLog = waitSeatlogs[0];
                    }
                    else
                    {
                        readerInfo.WaitSeatLog = null;
                    } 
                    readerInfo.AtReadingRoom = getRoomInfoByReader(readerInfo);
                }
                else if (dsReader.Tables[0].Rows.Count ==0)
                {//如果不查询记录，说明查询读者 信息，结果集中不存在，则返回null
                    return null;
                }
            }
            catch (Exception ex)
            {
                //TODO:记录错误日志
                return null;
            }

            return readerInfo;
        }
        /// <summary>
        /// 根据卡片物理ID获取学生信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public ReaderInfo GetReaderByCardId(string cardId)
        {
            if (string.IsNullOrEmpty(cardId))
            {
                return null;
            }
            SeatManage.ClassModel.ReaderInfo readerInfo = new ReaderInfo(); //创建一个读者实体
            string strWhere = " [T_SM_Reader].[cardId]=@cardId";

            SqlParameter[] parameters =  { 
                                                 new SqlParameter("@cardId",cardId)
                                             };


            try
            {
                DataSet dsReader = t_sm_reader_Dal.GetList(strWhere, parameters);//获取读者的信息。
                readerInfo.CardID = cardId;
                if (dsReader.Tables[0].Rows.Count > 0)
                {
                    DataRowToReaderInfo(dsReader.Tables[0].Rows[0], ref readerInfo);
                    return readerInfo;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取读者的选座次数
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        public int GetReaderChooseSeatTimes(string cardNo, int minutes)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" cardNo =@cardNo and (EnterOutState={0} or EnterOutState={1}) and datediff(mi,EnterOutTime,getdate())<@minutes", (int)SeatManage.EnumType.EnterOutLogType.SelectSeat, (int)SeatManage.EnumType.EnterOutLogType.ReselectSeat));
            SqlParameter[] parameters = { 
                                            new SqlParameter("@cardNo",cardNo),
                                            new SqlParameter("@minutes",minutes)
                                        };

            DataSet ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), parameters);
            return ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 获取读者的选座次数
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        public int GetReaderChooseSeatTimesByReadingRoom(string cardNo, int minutes,string roomNo)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" cardNo =@cardNo and ReadingRoomNo=@ReadingRoomNo and (EnterOutState={0} or EnterOutState={1}) and datediff(mi,EnterOutTime,getdate())<@minutes", (int)SeatManage.EnumType.EnterOutLogType.SelectSeat, (int)SeatManage.EnumType.EnterOutLogType.ReselectSeat));
            SqlParameter[] parameters = { 
                                            new SqlParameter("@cardNo",cardNo),
                                            new SqlParameter("@minutes",minutes),
                                            new SqlParameter("@ReadingRoomNo",roomNo)
                                        };

            DataSet ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), parameters);
            return ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 获取读者类型
        /// </summary>
        /// <returns></returns>
        public List<string> GetReaderType()
        {
            try
            {
                DataSet ds = t_sm_reader_Dal.GetReaderType();
                List<string> list = new List<string>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(dr[0].ToString());
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 情况读者信息
        /// </summary>
        public void ClearReaderInfo()
        {
            try
            {
                t_sm_reader_Dal.Delete();
            }
            catch
            {
                throw;
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public SeatManage.EnumType.HandleResult UpdateReaderInfo(ReaderInfo model)
        {
            try
            {
                if (t_sm_reader_Dal.Update(model))
                {
                    return HandleResult.Successed;
                }
                else
                {
                    return HandleResult.Failed;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据读者学号更新读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public SeatManage.EnumType.HandleResult UpdateReaderInfoByCardId(ReaderInfo model)
       {
           try
           {
               if (t_sm_reader_Dal.UpdateByCardId(model))
               {
                   return HandleResult.Successed;
               }
               else
               {
                   return HandleResult.Failed;
               }
           }
           catch
           {
               throw;
           }
       }
        /// <summary>
        /// 添加一条读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult Add(ReaderInfo model)
        {
            try
            {
                if (t_sm_reader_Dal.Add(model))
                {
                    return HandleResult.Successed;
                }
                else
                {
                    return HandleResult.Failed;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult DeleteReaderByCardNo(ReaderInfo model)
        {
            try
            {
                if (t_sm_reader_Dal.DeleteList(model.CardNo))
                {
                    return HandleResult.Successed;
                }
                else
                {
                    return HandleResult.Failed;
                }
            }
            catch
            {
                throw;
            }
        }

        #region 私有方法
        /// <summary>
        /// 通过学生信息获取读者所在的阅览室，如果读者当前状态为不为在做，预约，等待等状态，则返回null
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private ReadingRoomInfo getRoomInfoByReader(ReaderInfo reader)
        {
            string roomNo = "";
            if (reader == null)
            {
                return null;
            }
            if (reader.EnterOutLog != null && reader.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
            {
                roomNo = reader.EnterOutLog.ReadingRoomNo;
            }
            else if (reader.WaitSeatLog != null && reader.WaitSeatLog.WaitingState == EnterOutLogType.Waiting)
            {
                roomNo = reader.WaitSeatLog.ReadingRoomNo;
            }
            else if (reader.BespeakLog.Count > 0 && reader.BespeakLog[0].BsepeakState == BookingStatus.Waiting)
            {
                roomNo = reader.BespeakLog[0].ReadingRoomNo;
            }
            
            if (!string.IsNullOrEmpty(roomNo))
            {
                List<string> roomNums = new List<string>();
                roomNums.Add(roomNo);
                List<ReadingRoomInfo> roomInfo = GetReadingRoomInfo(roomNums);
                if (roomInfo.Count > 0)
                {
                    return roomInfo[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private ReaderInfo DataRowToReaderInfo(DataRow dr, ref ReaderInfo reader)
        {
            reader.CardID = dr["cardId"].ToString();
            reader.CardNo = dr["cardNo"].ToString();
            reader.Name = dr["ReaderName"].ToString();
            reader.ReaderType = dr["ReaderTypeName"].ToString();
            reader.Sex = dr["Sex"].ToString();
            reader.Dept = dr["ReaderDeptName"].ToString();
            reader.Flag = dr["Flag"].ToString();
            return reader;
        }



        #endregion

        /// <summary>
        /// 根据卡列号从源读者库获取读者信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public ReaderInfo GetReaderByCardIdFromSource(string cardId)
        {
            SeatManage.ISystemTerminal.IStuLibSync.IReaderSource loginValidate = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("IReaderSource") as SeatManage.ISystemTerminal.IStuLibSync.IReaderSource; //SeatManage.InterfaceFactory.SystemTerminalFactory.CreateLoginValidate();
            ReaderInfo reader = loginValidate.GetSourceReaderInfoByCardId(cardId);
            return reader;
        }
        /// <summary>
        /// 根据学号从源读者信息库获取读者信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ReaderInfo GetReaderByCardNoFromSource(string cardNo)
        {
            SeatManage.ISystemTerminal.IStuLibSync.IReaderSource loginValidate = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("IReaderSource") as SeatManage.ISystemTerminal.IStuLibSync.IReaderSource; //SeatManage.InterfaceFactory.SystemTerminalFactory.CreateLoginValidate();
            ReaderInfo reader = loginValidate.GetSourceReaderInfoByCardNo(cardNo);
            return reader;
        }
        /// <summary>
        /// 批量添加读者信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public HandleResult AddLotSize(List<ReaderInfo> modelList)
        {
            try
            {
                foreach (SeatManage.ClassModel.ReaderInfo model in modelList)
                {
                    t_sm_reader_Dal.Add(model);
                }
                return HandleResult.Successed;
            }
            catch
            {
                throw;
            }
        }
    }
}
