using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using System.Data;
using System.Data.SqlClient;
using SeatManage.DAL;
using SeatManage.EnumType;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        private T_SM_ViolateDiscipline violateDiscipline = new T_SM_ViolateDiscipline();
        #region 违规记录操作
        /// <summary>
        /// 根据黑名单id获取违规记录
        /// </summary>
        /// <param name="blacklistid"></param>
        /// <returns></returns>
        public List<ViolationRecordsLogInfo> GetViolationRecordsLogByblacklistID(string blacklistid)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(blacklistid))
            {
                strWhere.Append(string.Format(" BlacklistID = '{0}'", blacklistid));
            }
            DataSet ds = violateDiscipline.GetList(strWhere.ToString(), null);
            List<ViolationRecordsLogInfo> vrloglist = new List<ViolationRecordsLogInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    vrloglist.Add(DataRowToViolationRecordsLogInfo(ds.Tables[0].Rows[i]));
                }
            }
            return vrloglist;
        }
        /// <summary>
        /// 查询有效的违规单记录
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        public List<ViolationRecordsLogInfo> GetViolationRecordsLogInfo(string cardNo, string roomNums)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" Flag = '{0}'", (int)LogStatus.Valid));
            if (!string.IsNullOrEmpty(cardNo))
            {
                strWhere.Append(string.Format("and CardNo = '{0}'", cardNo));
            }
            if (!string.IsNullOrEmpty(roomNums))
            {
                strWhere.Append(string.Format("and ReadingRoomNo = '{0}'", roomNums));
            }
            DataSet ds = violateDiscipline.GetList(strWhere.ToString(), null);
            List<ViolationRecordsLogInfo> vrloglist = new List<ViolationRecordsLogInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    vrloglist.Add(DataRowToViolationRecordsLogInfo(ds.Tables[0].Rows[i]));
                }
            }
            return vrloglist;
        }
        /// <summary>
        /// 分页查询用户的违规记录。
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ViolationRecordsLogInfo> GetViolationRecordsLogsByPage(string cardNo, int pageIndex, int pageSize)
        {
            DataSet ds = violateDiscipline.GetList(cardNo,pageIndex,pageSize);
            List<ViolationRecordsLogInfo> vrloglist = new List<ViolationRecordsLogInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    vrloglist.Add(DataRowToViolationRecordsLogInfo(ds.Tables[0].Rows[i]));
                }
            }
            return vrloglist;
        }

        /// <summary>
        /// 根据条件查询违规记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roonNum"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<ViolationRecordsLogInfo> GetViolationRecordsLogs(string cardNo, string roomNums, string beginDate, string endDate, LogStatus logstatus, LogStatus blackliststatus)
        {
            StringBuilder strWhere = new StringBuilder();
            if (((int)logstatus) > -1)
            {
                strWhere.Append(string.Format(" Flag = '{0}'", (int)logstatus));
            }
            if (((int)blackliststatus) > -1)
            {
                if (blackliststatus == LogStatus.Valid)
                {
                    if (string.IsNullOrEmpty(strWhere.ToString()))
                    {

                        strWhere.Append(" BlacklistID > -1");
                    }
                    else
                    {
                        strWhere.Append(" and BlacklistID > -1");
                    }
                }
                else if (blackliststatus == LogStatus.Fail)
                {
                    if (string.IsNullOrEmpty(strWhere.ToString()))
                    {

                        strWhere.Append(" BlacklistID = -1");
                    }
                    else
                    {
                        strWhere.Append(" and BlacklistID = -1");
                    }
                }
            }
            if (!string.IsNullOrEmpty(cardNo))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" CardNo = '{0}'", cardNo));
                }
                else
                {
                    strWhere.Append(string.Format("and CardNo = '{0}'", cardNo));
                }
            }
            if (!string.IsNullOrEmpty(roomNums))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" ReadingRoomNo = '{0}'", roomNums));
                }
                else
                {
                    strWhere.Append(string.Format("and ReadingRoomNo = '{0}'", roomNums));
                }
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterOutTime >= '{0}'", beginDate));
                }
                else
                {
                    strWhere.Append(string.Format("and EnterOutTime >= '{0}'", beginDate));
                }
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterOutTime <= '{0}'", endDate));
                }
                else
                {
                    strWhere.Append(string.Format("and EnterOutTime <= '{0}'", endDate));
                }
            }
            DataSet ds = violateDiscipline.GetList(strWhere.ToString(), null);
            List<ViolationRecordsLogInfo> vrloglist = new List<ViolationRecordsLogInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    vrloglist.Add(DataRowToViolationRecordsLogInfo(ds.Tables[0].Rows[i]));
                }
            }
            return vrloglist;
        }
        /// <summary>
        /// 根据条件查询违规记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roonNum"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<ViolationRecordsLogInfo> GetViolationRecordsLogsByType(string cardNo, string roomNums, string beginDate, string endDate, LogStatus logstatus, LogStatus blackliststatus, ViolationRecordsType vrtype)
        {
            StringBuilder strWhere = new StringBuilder();
            if (((int)logstatus) > -1)
            {
                strWhere.Append(string.Format(" Flag = '{0}'", (int)logstatus));
            }
            if (((int)blackliststatus) > -1)
            {
                if (blackliststatus == LogStatus.Valid)
                {
                    if (string.IsNullOrEmpty(strWhere.ToString()))
                    {

                        strWhere.Append(" BlacklistID > -1");
                    }
                    else
                    {
                        strWhere.Append(" and BlacklistID > -1");
                    }
                }
                else if (blackliststatus == LogStatus.Fail)
                {
                    if (string.IsNullOrEmpty(strWhere.ToString()))
                    {

                        strWhere.Append(" BlacklistID = -1");
                    }
                    else
                    {
                        strWhere.Append(" and BlacklistID = -1");
                    }
                }
            }
            if (!string.IsNullOrEmpty(cardNo))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" CardNo = '{0}'", cardNo));
                }
                else
                {
                    strWhere.Append(string.Format("and CardNo = '{0}'", cardNo));
                }
            }
            if (!string.IsNullOrEmpty(roomNums))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" ReadingRoomNo = '{0}'", roomNums));
                }
                else
                {
                    strWhere.Append(string.Format("and ReadingRoomNo = '{0}'", roomNums));
                }
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterOutTime >= '{0}'", beginDate));
                }
                else
                {
                    strWhere.Append(string.Format("and EnterOutTime >= '{0}'", beginDate));
                }
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterOutTime <= '{0}'", endDate));
                }
                else
                {
                    strWhere.Append(string.Format("and EnterOutTime <= '{0}'", endDate));
                }
            }
            if (vrtype != SeatManage.EnumType.ViolationRecordsType.None)
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterFlag = '{0}'", (int)vrtype));
                }
                else
                {
                    strWhere.Append(string.Format("and EnterFlag = '{0}'", (int)vrtype));
                }
            }
            DataSet ds = violateDiscipline.GetList(strWhere.ToString(), null);
            List<ViolationRecordsLogInfo> vrloglist = new List<ViolationRecordsLogInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    vrloglist.Add(DataRowToViolationRecordsLogInfo(ds.Tables[0].Rows[i]));
                }
            }
            return vrloglist;
        }
        /// <summary>
        /// 根据条件、学号模糊查询违规记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roonNum"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<ViolationRecordsLogInfo> GetViolationRecordsLogsByType_ByFuzzySearch(string cardNo, string roomNums, string beginDate, string endDate, LogStatus logstatus, LogStatus blackliststatus, ViolationRecordsType vrtype)
        {
            StringBuilder strWhere = new StringBuilder();
            if (((int)logstatus) > -1)
            {
                strWhere.Append(string.Format(" Flag = '{0}'", (int)logstatus));
            }
            if (((int)blackliststatus) > -1)
            {
                if (blackliststatus == LogStatus.Valid)
                {
                    if (string.IsNullOrEmpty(strWhere.ToString()))
                    {

                        strWhere.Append(" BlacklistID > -1");
                    }
                    else
                    {
                        strWhere.Append(" and BlacklistID > -1");
                    }
                }
                else if (blackliststatus == LogStatus.Fail)
                {
                    if (string.IsNullOrEmpty(strWhere.ToString()))
                    {

                        strWhere.Append(" BlacklistID = -1");
                    }
                    else
                    {
                        strWhere.Append(" and BlacklistID = -1");
                    }
                }
            }
            if (!string.IsNullOrEmpty(cardNo))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" CardNo like '%{0}%'", cardNo));
                }
                else
                {
                    strWhere.Append(string.Format(" and CardNo like '%{0}%'", cardNo));
                }
            }
            if (!string.IsNullOrEmpty(roomNums))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" ReadingRoomNo = '{0}'", roomNums));
                }
                else
                {
                    strWhere.Append(string.Format(" and ReadingRoomNo = '{0}'", roomNums));
                }
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterOutTime >= '{0}'", beginDate));
                }
                else
                {
                    strWhere.Append(string.Format(" and EnterOutTime >= '{0}'", beginDate));
                }
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterOutTime <= '{0}'", endDate));
                }
                else
                {
                    strWhere.Append(string.Format(" and EnterOutTime <= '{0}'", endDate));
                }
            }
            if (vrtype != SeatManage.EnumType.ViolationRecordsType.None)
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterFlag = '{0}'", (int)vrtype));
                }
                else
                {
                    strWhere.Append(string.Format(" and EnterFlag = '{0}'", (int)vrtype));
                }
            }
            DataSet ds = violateDiscipline.GetList(strWhere.ToString(), null);
            List<ViolationRecordsLogInfo> vrloglist = new List<ViolationRecordsLogInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    vrloglist.Add(DataRowToViolationRecordsLogInfo(ds.Tables[0].Rows[i]));
                }
            }
            return vrloglist;
        }
        /// <summary>
        /// 添加违规记录
        /// </summary>
        /// <param name="blacklist"></param>
        public HandleResult AddViolationRecordsLog(ViolationRecordsLogInfo ViolationRecordsLog)
        {
            //添加违规记录
            bool result = violateDiscipline.Add(ViolationRecordsLog);
            //return HandleResult.Successed;
            #region 添加提醒，已被注销
            if (result)
            {
                List<string> roomlist = new List<string>();
                roomlist.Add(ViolationRecordsLog.ReadingRoomID);
                List<ReadingRoomInfo> roominfos = GetReadingRoomInfo(null);
                Dictionary<string, ReadingRoomSetting> roomSettings = new Dictionary<string, ReadingRoomSetting>();
                for (int i = 0; i < roominfos.Count; i++)
                {
                    roomSettings.Add(roominfos[i].No, roominfos[i].Setting);
                }
                if (roomSettings[ViolationRecordsLog.ReadingRoomID] != null && roomSettings[ViolationRecordsLog.ReadingRoomID].BlackListSetting.Used)
                {
                    List<ViolationRecordsLogInfo> violateRecords = GetViolationRecordsLogInfo(ViolationRecordsLog.CardNo, ViolationRecordsLog.ReadingRoomID);
                    //添加读者提醒
                    //ReaderNoticeInfo rni = new ReaderNoticeInfo();
                    //rni.Type = NoticeType.ViolationWarning;
                    //rni.IsRead = LogStatus.Valid;
                    //rni.CardNo = ViolationRecordsLog.CardNo;
                    //rni.Note = string.Format("{0}，还有{1}次违规，就进入黑名单", ViolationRecordsLog.Remark, roomSettings[ViolationRecordsLog.ReadingRoomID].BlackListSetting.ViolateTimes - violateRecords.Count);
                    //AddReaderNotice(rni);

                    PushMsgInfo msg = new PushMsgInfo();
                    msg.Title = "您好，您有一次违规";
                    msg.MsgType = MsgPushType.EnterVR;
                    msg.StudentNum = ViolationRecordsLog.CardNo;
                    msg.RoomName = roominfos.Find(u => u.No == ViolationRecordsLog.ReadingRoomID).Name;
                    msg.VrType = ViolationRecordsLog.EnterFlag;
                    msg.Message = string.Format("{0}，还有{1}次违规，就进入黑名单", ViolationRecordsLog.Remark, roomSettings[ViolationRecordsLog.ReadingRoomID].BlackListSetting.ViolateTimes - violateRecords.Count);
                    SendMsg(msg);


                    if (violateRecords.Count >= roomSettings[ViolationRecordsLog.ReadingRoomID].BlackListSetting.ViolateTimes)
                    {
                        BlackListInfo bli = new BlackListInfo();
                        bli.CardNo = ViolationRecordsLog.CardNo;
                        bli.ReadingRoomID = ViolationRecordsLog.ReadingRoomID;
                        bli.AddTime = GetServerDateTime();
                        bli.OutBlacklistMode = roomSettings[ViolationRecordsLog.ReadingRoomID].BlackListSetting.LeaveBlacklist;
                        if (bli.OutBlacklistMode == LeaveBlacklistMode.ManuallyMode)
                        {
                            bli.ReMark = string.Format("违规累计{0}次，被加入黑名单", roomSettings[ViolationRecordsLog.ReadingRoomID].BlackListSetting.ViolateTimes);
                        }
                        else
                        {
                            bli.OutTime = bli.AddTime.AddDays(roomSettings[ViolationRecordsLog.ReadingRoomID].BlackListSetting.LimitDays);
                            bli.ReMark = string.Format("违规累计{0}次，被加入黑名单{1}天", roomSettings[ViolationRecordsLog.ReadingRoomID].BlackListSetting.ViolateTimes, roomSettings[ViolationRecordsLog.ReadingRoomID].BlackListSetting.LimitDays);
                        }

                        int blackId = AddBlacklist(bli);
                        //修改黑名单涉及的违规记录
                        foreach (ViolationRecordsLogInfo vr in violateRecords)
                        {
                            vr.BlacklistID = blackId.ToString();
                            vr.Flag = LogStatus.Fail;
                            UpdateViolationRecordsLog(vr);
                        }
                        
                    }
                }
                else
                {
                    //判断黑名单 
                    RegulationRulesSetting set = GetRegulationRulesSetting();
                    List<ViolationRecordsLogInfo> violateRecords = GetViolationRecordsLogInfo(ViolationRecordsLog.CardNo, null);
                    for (int i = 0; i < violateRecords.Count; i++)
                    {
                        if (roomSettings[violateRecords[i].ReadingRoomID].BlackListSetting.Used)
                        {
                            violateRecords.RemoveAt(i);
                            i--;
                        }
                    }
                    //添加读者提醒
                    //ReaderNoticeInfo rni = new ReaderNoticeInfo();
                    //rni.Type = NoticeType.ViolationWarning;
                    //rni.IsRead = LogStatus.Valid;
                    //rni.CardNo = ViolationRecordsLog.CardNo;
                    //rni.Note = string.Format("{0}，还有{1}次违规，就进入黑名单", ViolationRecordsLog.Remark, roomSettings[ViolationRecordsLog.ReadingRoomID].BlackListSetting.ViolateTimes - violateRecords.Count);
                    //AddReaderNotice(rni);


                    PushMsgInfo msg = new PushMsgInfo();
                    msg.Title = "您好，您有一次违规";
                    msg.MsgType = MsgPushType.EnterVR;
                    msg.StudentNum = ViolationRecordsLog.CardNo;
                    msg.RoomName = roominfos.Find(u => u.No == ViolationRecordsLog.ReadingRoomID).Name;
                    msg.VrType = ViolationRecordsLog.EnterFlag;
                    msg.Message = string.Format("{0}，还有{1}次违规，就进入黑名单", ViolationRecordsLog.Remark, set.BlacklistSet.ViolateTimes - violateRecords.Count);
                    SendMsg(msg);
                    
                    if (violateRecords.Count >= set.BlacklistSet.ViolateTimes)
                    {
                        BlackListInfo bli = new BlackListInfo();
                        bli.CardNo = ViolationRecordsLog.CardNo;
                        bli.ReadingRoomID = ViolationRecordsLog.ReadingRoomID;
                        bli.AddTime = GetServerDateTime();
                        bli.OutTime = bli.AddTime.AddDays(set.BlacklistSet.LimitDays);
                        bli.OutBlacklistMode = set.BlacklistSet.LeaveBlacklist;
                        if (bli.OutBlacklistMode == LeaveBlacklistMode.ManuallyMode)
                        {
                            bli.ReMark = string.Format("违规累计{0}次，被加入黑名单", set.BlacklistSet.ViolateTimes);
                        }
                        else
                        {
                            bli.ReMark = string.Format("多次违规，被加入黑名单{1}天", set.BlacklistSet.ViolateTimes, set.BlacklistSet.LimitDays);
                        }

                        int blackId = AddBlacklist(bli);
                        //修改黑名单涉及的违规记录
                        foreach (ViolationRecordsLogInfo vr in violateRecords)
                        {
                            vr.BlacklistID = blackId.ToString();
                            vr.Flag = LogStatus.Fail;
                            UpdateViolationRecordsLog(vr);
                        }
                       
                    }
                }

                return HandleResult.Successed;
            }
            else
            {
                return HandleResult.Failed;
            }
            #endregion
        }
        /// <summary>
        /// 更新违规记录
        /// </summary>
        /// <param name="blacklist"></param>
        public HandleResult UpdateViolationRecordsLog(ViolationRecordsLogInfo ViolationRecordsLog)
        {
            bool result = violateDiscipline.Update(ViolationRecordsLog);
            if (result)
            {
                if (ViolationRecordsLog.Flag == LogStatus.Fail && ViolationRecordsLog.BlacklistID == "-1")
                {
                    PushMsgInfo msg = new PushMsgInfo();
                    msg.Title = "您好，您违规已失效";
                    msg.MsgType = MsgPushType.LeaveVrBlack;
                    msg.StudentNum = ViolationRecordsLog.CardNo;
                    msg.VrType = ViolationRecordsLog.EnterFlag;
                    msg.AddTime = DateTime.Parse(ViolationRecordsLog.EnterOutTime);
                    msg.LeaveDate = DateTime.Now;
                    string type = "";
                    switch (ViolationRecordsLog.EnterFlag)
                    {
                        case ViolationRecordsType.BookingTimeOut:
                            type = "预约超时";
                            break;
                        case ViolationRecordsType.CancelWaitByAdmin:
                            type = "被管理员取消等待";
                            break;
                        case ViolationRecordsType.LeaveByAdmin:
                            type = "被管理员释放座位";
                            break;
                        case ViolationRecordsType.LeaveNotReadCard:
                            type = "离开没有释放座位";
                            break;
                        case ViolationRecordsType.SeatOutTime:
                            type = "在座超时";
                            break;
                        case ViolationRecordsType.ShortLeaveByAdminOutTime:
                            type = "被管理员设置暂离超时";
                            break;
                        case ViolationRecordsType.ShortLeaveByReaderOutTime:
                            type = "被其他读者设置暂离超时";
                            break;
                        case ViolationRecordsType.ShortLeaveByServiceOutTime:
                            type = "暂离超时";
                            break;
                        case ViolationRecordsType.ShortLeaveOutTime:
                            type = "暂离超时";
                            break;
                    }
                    msg.Message = string.Format("您在{0}的违规：{1}，已经到期或被管理员删除，请遵守系统使用规则。", ViolationRecordsLog.EnterOutTime, type);
                    SendMsg(msg);
                }

                return HandleResult.Successed;
            }
            else
            {
                return HandleResult.Failed;
            }
        }
        /// <summary>
        /// 根据条件删除违规记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室号</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        public int DeleteViolationRecordsLog(string cardNo, string roomNum, string beginDate, string endDate)
        {
            throw new Exception();
        }
        /// <summary>
        /// 根据id查找违规记录
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ViolationRecordsLogInfo GetViolationRecordsLog(string ID)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" violateID ='" + ID + "'");
            try
            {
                DataSet ds = violateDiscipline.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToViolationRecordsLogInfo(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 私有方法
        private ViolationRecordsLogInfo DataRowToViolationRecordsLogInfo(DataRow dr)
        {
            //violateID,CardNo,SeatNo,ReadingRoomNo,ReadingRoomName,EnterFlag,EnterOutTime,BlacklistID,WarningState,Remark,Flag,ReaderName
            ViolationRecordsLogInfo model = new ViolationRecordsLogInfo();
            model.ID = dr["violateID"].ToString();
            model.CardNo = dr["CardNo"].ToString();
            model.ReaderName = dr["ReaderName"].ToString();
            model.TypeName = dr["ReaderTypeName"].ToString();
            model.DeptName = dr["ReaderDeptName"].ToString();
            model.Sex = dr["Sex"].ToString();
            model.SeatID = dr["SeatNo"].ToString();
            model.ReadingRoomID = dr["ReadingRoomNo"].ToString();
            model.ReadingRoomName = dr["ReadingRoomName"].ToString();
            model.EnterFlag = (ViolationRecordsType)int.Parse(dr["EnterFlag"].ToString());
            model.EnterOutTime = DateTime.Parse(dr["EnterOutTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            model.BlacklistID = dr["BlacklistID"].ToString();
            model.Remark = dr["Remark"].ToString();
            model.Flag = (LogStatus)int.Parse(dr["Flag"].ToString());
            return model;
        }
        #endregion
    }
}
