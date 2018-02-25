using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.Bll;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;
using System.Xml;

namespace SeatService.Service
{
    public class WatchOperate
    {
        private List<ReadingRoomInfo> _rooms;
        private static RegulationRulesSetting _regulationRulesSetting;
        private Dictionary<string, ReadingRoomInfo> _roomsDic;
        /// <summary>
        /// 构造函数
        /// </summary>
        public WatchOperate()
        {
            GetSetting();
        }
        public void GetSetting()
        {
            try
            {
                //获取全部阅览室信息和黑名单设置
                _rooms = GetReadingRoomList();
                _regulationRulesSetting = T_SM_SystemSet.GetRegulationRulesSetting();
                _roomsDic = new Dictionary<string, ReadingRoomInfo>();
                for (int i = 0; i < _rooms.Count; i++)
                {
                    _roomsDic.Add(_rooms[i].No, _rooms[i]);
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("获取全部阅览室信息和黑名单设置遇到错误：{0}", e.Message));
            }
        }
        /// <summary>
        /// 服务启动清除异常处理
        /// </summary>
        public void ServiceStartOperate()
        {
            try
            {
                foreach (ReadingRoomInfo rri in _rooms)
                {
                    if (rri.Setting != null)
                    {
                        OpenReadingRoom(rri);
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write("服务启动清除异常处理失败：" + e.Message);
            }
        }

        /// <summary>
        /// 执行阅览室开闭馆处理
        /// </summary>
        /// <param name="RoomsStatus">阅览室状态</param>
        public void OpenCloseReadingRoom()
        {
            try
            {
                //遍历所有阅览室
                foreach (ReadingRoomInfo rri in _rooms)
                {
                    if (rri.Setting != null)
                    {
                        try
                        {
                            NowReadingRoomState nrrs = new NowReadingRoomState(rri);
                            //获取阅览室状态
                            ReadingRoomOpenCloseLogInfo rroc = new ReadingRoomOpenCloseLogInfo();
                            List<ReadingRoomOpenCloseLogInfo> rrocl = new List<ReadingRoomOpenCloseLogInfo>();
                            try
                            {
                                rrocl = T_SM_RROpenCloseLog.GetReadingRoomOClog(rri.No, LogStatus.Valid, null, null);
                            }
                            catch (Exception e)
                            {
                                WriteLog.Write(e.Message);
                                break;
                            }
                            if (rrocl.Count > 0)
                            {
                                rroc = rrocl[0];
                            }
                            //如果没有记录默认是闭馆
                            else
                            {
                                rroc.OpenCloseState = ReadingRoomStatus.Close;
                                rroc.ReadingRoomNo = rri.No;
                                rroc.OperateNo = SeatComm.RndNum();
                                rroc.OperateTime = ServiceDateTime.Now;
                                rroc.Logstatus = LogStatus.Valid;
                            }
                            int new_id = 0;
                            //如果启用24小时模式
                            if (rri.Setting.RoomOpenSet.UninterruptibleModel)
                            {
                                if (rroc.OpenCloseState == ReadingRoomStatus.Close)
                                {
                                    rroc.OpenCloseState = ReadingRoomStatus.Open;
                                    rroc.OperateTime = ServiceDateTime.Now;
                                    T_SM_RROpenCloseLog.AddNewReadingRoomOClog(rroc, ref new_id);
                                    WriteLog.Write(string.Format("{0},关闭", rri.Name));
                                }
                            }
                            else
                            {
                                //判断状态
                                if (rroc.OpenCloseState != nrrs.RoomOpenState)
                                {
                                    if (nrrs.RoomOpenState == ReadingRoomStatus.Open)
                                    {
                                        rroc.OpenCloseState = ReadingRoomStatus.Open;
                                        rroc.OperateTime = ServiceDateTime.Now;
                                        rroc.OperateNo = SeatComm.RndNum();
                                        T_SM_RROpenCloseLog.AddNewReadingRoomOClog(rroc, ref new_id);
                                        WriteLog.Write(string.Format("{0},开启", rri.Name));
                                    }
                                    else if (nrrs.RoomOpenState == ReadingRoomStatus.Close)
                                    {
                                        CloseReadingRoom(rri);
                                        rroc.OpenCloseState = ReadingRoomStatus.Close;
                                        rroc.OperateTime = ServiceDateTime.Now;
                                        T_SM_RROpenCloseLog.AddNewReadingRoomOClog(rroc, ref new_id);
                                        WriteLog.Write(string.Format("{0},关闭", rri.Name));
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            WriteLog.Write(e.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("执行阅览室开闭馆处理遇到错误：{0}", ex.Message));
            }
        }

        #region 超时处理

        /// <summary>
        /// 处理暂离和在座超时
        /// </summary>
        /// <param name="readingRooms"></param>
        public void OverTimeOperate()
        {
            //遍历所有阅览室
            foreach (ReadingRoomInfo rri in _rooms)
            {
                DateTime NowDateTime = ServiceDateTime.Now;
                if (rri.Setting != null)
                {
                    DateTime seatOutTime = rri.Setting.RoomOpenSet.NowOpenTime(NowDateTime);
                    if (rri.Setting.SeatUsedTimeLimit.Used && rri.Setting.SeatUsedTimeLimit.Mode == "Fixed")
                    {
                        for (int i = 0; i < rri.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                        {
                            if (NowDateTime > rri.Setting.SeatUsedTimeLimit.FixedTimes[i])
                            {
                                if (rri.Setting.SeatUsedTimeLimit.IsCanContinuedTime)
                                {
                                    seatOutTime = rri.Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-rri.Setting.SeatUsedTimeLimit.CanDelayTime);
                                }
                                else
                                {
                                    seatOutTime = rri.Setting.SeatUsedTimeLimit.FixedTimes[i];
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    NowReadingRoomState nrrs = new NowReadingRoomState(rri);
                    List<EnterOutLogType> typeList = new List<EnterOutLogType>();
                    typeList.Add(EnterOutLogType.BookingConfirmation);
                    typeList.Add(EnterOutLogType.ComeBack);
                    typeList.Add(EnterOutLogType.SelectSeat);
                    typeList.Add(EnterOutLogType.ReselectSeat);
                    typeList.Add(EnterOutLogType.WaitingSuccess);
                    typeList.Add(EnterOutLogType.ShortLeave);
                    typeList.Add(EnterOutLogType.ContinuedTime);
                    try
                    {
                        //获取当前的进出记录
                        List<EnterOutLogInfo> eolList = T_SM_EnterOutLog.GetEnterOutLogByStatus(null, rri.No, null, typeList, LogStatus.Valid, NowDateTime.ToShortDateString(), null);
                        foreach (EnterOutLogInfo eol in eolList)
                        {
                            switch (eol.EnterOutState)
                            {
                                case EnterOutLogType.SelectSeat:
                                case EnterOutLogType.ReselectSeat:
                                case EnterOutLogType.WaitingSuccess:
                                case EnterOutLogType.BookingConfirmation:
                                    try
                                    {
                                        if (rri.Setting.SeatUsedTimeLimit.Used)
                                        {
                                            //判断是否在座超时
                                            if (rri.Setting.SeatUsedTimeLimit.Mode == "Free" && (eol.EnterOutTime.AddMinutes(rri.Setting.SeatUsedTimeLimit.UsedTimeLength) < NowDateTime))
                                            {
                                                SeatOverTimeOperator(rri.Setting, eol, NowDateTime);
                                            }
                                            else if (rri.Setting.SeatUsedTimeLimit.Mode == "Fixed" && eol.EnterOutTime < seatOutTime)
                                            {
                                                SeatOverTimeOperator(rri.Setting, eol, NowDateTime);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLog.Write(string.Format("在座超时处理遇到异常{0}", ex.Message));
                                    }
                                    break;
                                case EnterOutLogType.ContinuedTime:
                                    try
                                    {
                                        //判断是否续时超时
                                        if (rri.Setting.SeatUsedTimeLimit.Used)
                                        {
                                            if (rri.Setting.SeatUsedTimeLimit.Mode == "Free" && eol.EnterOutTime.AddMinutes(rri.Setting.SeatUsedTimeLimit.DelayTimeLength) < NowDateTime)
                                            {
                                                SeatOverTimeOperator(rri.Setting, eol, NowDateTime);
                                            }
                                            else if (rri.Setting.SeatUsedTimeLimit.Mode == "Fixed" && eol.EnterOutTime < seatOutTime)
                                            {
                                                SeatOverTimeOperator(rri.Setting, eol, NowDateTime);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLog.Write(string.Format("续时超时处理遇到异常{0}", ex.Message));
                                    } break;
                                case EnterOutLogType.ComeBack:
                                    {
                                        //操作最后一条选座或续时的记录
                                        EnterOutLogInfo neweol = GetLastNoSeatTimeLog(eol);
                                        if (neweol != null)
                                        {
                                            eol.EnterOutTime = neweol.EnterOutTime;
                                            eol.EnterOutState = neweol.EnterOutState;
                                            if (eol.EnterOutState == EnterOutLogType.ContinuedTime)
                                            {
                                                goto case EnterOutLogType.ContinuedTime;
                                            }
                                            else
                                            {
                                                goto case EnterOutLogType.BookingConfirmation;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                case EnterOutLogType.ShortLeave:
                                    {
                                        if (eol.Flag == Operation.OtherReader && rri.Setting.NoManagement.Used && (eol.EnterOutTime.AddMinutes(NowReadingRoomState.GetSeatHoldTime(rri.Setting.SeatHoldTime, eol.EnterOutTime)) < NowDateTime))
                                        {
                                            //判断座位等待处理
                                            List<EnterOutLogType> type = new List<EnterOutLogType>();
                                            type.Add(EnterOutLogType.Waiting);
                                            List<WaitSeatLogInfo> wslilist = T_SM_SeatWaiting.GetWaitSeatList(null, eol.EnterOutLogID, null, null, type);
                                            if (wslilist.Count > 0)
                                            {
                                                WaitSeatOperate(rri, eol, wslilist, NowDateTime);
                                            }
                                        }
                                        else
                                        {
                                            if (eol.Flag == Operation.Admin)
                                            {
                                                if (rri.Setting.AdminShortLeave.IsUsed && (eol.EnterOutTime.AddMinutes(rri.Setting.AdminShortLeave.HoldTimeLength) < NowDateTime))
                                                {
                                                    ShortLeaveOverTimeOperator(eol, rri.Setting, NowDateTime);
                                                }
                                                else if (!rri.Setting.AdminShortLeave.IsUsed && (eol.EnterOutTime.AddMinutes(NowReadingRoomState.GetSeatHoldTime(rri.Setting.SeatHoldTime, eol.EnterOutTime)) < NowDateTime))
                                                {
                                                    ShortLeaveOverTimeOperator(eol, rri.Setting, NowDateTime);
                                                }
                                            }
                                            else if (eol.EnterOutTime.AddMinutes(NowReadingRoomState.GetSeatHoldTime(rri.Setting.SeatHoldTime, eol.EnterOutTime)) < NowDateTime)
                                            {//读者自己刷卡释放座位或者在座超时释放座位。
                                                ShortLeaveOverTimeOperator(eol, rri.Setting, NowDateTime);
                                            }
                                        }
                                    } break;
                            }
                        }
                        //如果阅览室开放预约
                        if (rri.Setting.SeatBespeak.Used)
                        {
                            try
                            {
                                //预约超时处理
                                //List<BookingStatus> bstype = new List<BookingStatus>();
                                //bstype.Add(BookingStatus.Waiting);
                                //List<BespeakLogInfo> blilist = T_SM_SeatBespeak.GetBespeakList(null, rri.No, NowDateTime, 0, bstype);
                                List<BespeakLogInfo> blilist = T_SM_SeatBespeak.GetNotCheckedBespeakLogInfo(new List<string>() { rri.No }, NowDateTime);
                                if (blilist.Count > 0)
                                {
                                    foreach (BespeakLogInfo bli in blilist)
                                    {
                                        if (rri.Setting.SeatBespeak.NowDayBespeak && bli.BsepeakTime.Date == DateTime.Now.Date)
                                        {
                                            if (bli.BsepeakTime.AddMinutes(rri.Setting.SeatBespeak.SeatKeepTime) < NowDateTime)
                                            {
                                                BookingOverTime(rri.Setting, NowDateTime, bli);
                                            }
                                        }
                                        //如果预约超时则更改预约状态
                                        else
                                        {
                                            if (bli.BsepeakTime.AddMinutes(int.Parse(rri.Setting.SeatBespeak.ConfirmTime.EndTime)) < NowDateTime)
                                            {
                                                BookingOverTime(rri.Setting, NowDateTime, bli);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("预约超时处理遇到异常{0}", ex.Message));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        WriteLog.Write(string.Format("获取当前的进出记录失败{0}", e.Message));
                    }
                }
            }
        }


        /// <summary>
        /// 处理锁定超时
        /// </summary>
        public void LockOverTime()
        {
            try
            {
                DateTime NowDateTime = ServiceDateTime.Now;
                //获取全部的锁定座位
                List<Seat> SeatList = T_SM_Seat.GetSeatListByRoomNum(null, true);
                if (SeatList != null)
                {
                    foreach (Seat s in SeatList)
                    {
                        if (s.LockedTime.AddMinutes(1.0) < NowDateTime)
                        {
                            T_SM_Seat.UnLockSeat(s.SeatNo);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(e.Message);
            }
        }
        /// <summary>
        /// 违规记录超时处理
        /// </summary>
        public void ViolationRecordsOverTime()
        {
            try
            {
                DateTime NowDateTime = ServiceDateTime.Now;
                //违规记录超时处理
                List<ViolationRecordsLogInfo> listvr = T_SM_ViolateDiscipline.GetViolationRecordsLog(null, null);
                if (listvr.Count > 0)
                {
                    foreach (ViolationRecordsLogInfo vrli in listvr)
                    {
                        if (_roomsDic[vrli.ReadingRoomID].Setting.BlackListSetting.Used && DateTime.Parse(vrli.EnterOutTime).AddDays(_roomsDic[vrli.ReadingRoomID].Setting.BlackListSetting.ViolateFailDays) < NowDateTime)
                        {
                            vrli.Flag = LogStatus.Fail;
                            T_SM_ViolateDiscipline.UpdateViolationRecords(vrli);
                            WriteLog.Write(string.Format("读者{0}，违规记录过期", vrli.CardNo));
                            AddNotice(vrli.CardNo, string.Format("{0}记录的违规，{1}，过期", vrli.EnterOutTime, vrli.Remark));
                        }
                        else if (_regulationRulesSetting.BlacklistSet.Used && DateTime.Parse(vrli.EnterOutTime).AddDays(_regulationRulesSetting.BlacklistSet.ViolateFailDays) < NowDateTime)
                        {
                            vrli.Flag = LogStatus.Fail;
                            T_SM_ViolateDiscipline.UpdateViolationRecords(vrli);
                            WriteLog.Write(string.Format("读者{0}，违规记录过期", vrli.CardNo));
                            AddNotice(vrli.CardNo, string.Format("{0}记录的违规，{1}，过期", vrli.EnterOutTime, vrli.Remark));
                        }
                        else if (!_regulationRulesSetting.BlacklistSet.Used && !_roomsDic[vrli.ReadingRoomID].Setting.BlackListSetting.Used)
                        {
                            vrli.Flag = LogStatus.Fail;
                            T_SM_ViolateDiscipline.UpdateViolationRecords(vrli);
                            WriteLog.Write(string.Format("读者{0}，违规记录过期", vrli.CardNo));
                            AddNotice(vrli.CardNo, string.Format("{0}记录的违规，{1}，过期", vrli.EnterOutTime, vrli.Remark));
                        }

                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("违规记录超时处理遇到异常：{0}", e.Message));
            }
        }
        /// <summary>
        /// 清除黑名单
        /// </summary>
        /// <param name="readingRooms"></param>
        public void BlacklistOverTime()
        {
            try
            {
                DateTime NowDateTime = ServiceDateTime.Now;
                List<BlackListInfo> bllist = SeatManage.Bll.T_SM_Blacklist.GetBlackListInfo(null);
                if (bllist != null)
                {
                    foreach (BlackListInfo bli in bllist)
                    {
                        if ((bli.OutTime < NowDateTime) && (bli.OutBlacklistMode == LeaveBlacklistMode.AutomaticMode))
                        {
                            bli.BlacklistState = LogStatus.Fail;
                            T_SM_Blacklist.UpdateBlackList(bli);
                            WriteLog.Write("读者" + bli.CardNo + "，处罚结束，离开黑名单");

                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("黑名单处理遇到异常：{0}", e.Message));
            }
        }
        /// <summary>
        /// 清除过期的媒体文件
        /// </summary>
        public void MediaOverDate()
        {
            //TODO:清除过期的媒体文件
            //清除过期的播放列表
            try
            {
                List<SeatManage.ClassModel.AMS_PlayList> overTimePlaylistList = SeatManage.Bll.AMS_PlayList.GetPlayListOverTime(LogStatus.Fail);
                foreach (SeatManage.ClassModel.AMS_PlayList playerlist in overTimePlaylistList)
                {
                    if (playerlist.PlayListNo == "默认播放列表")
                    {
                        continue;
                    }
                    foreach (SeatManage.ClassModel.AMS_VideoItem videofile in playerlist.VideoFiles)
                    {
                        if (!SeatManage.Bll.FileOperate.FileDelete(videofile.Name, SeatManageSubsystem.MediaFiles))
                        {
                            WriteLog.Write(string.Format("删除媒体文件{0}失败", videofile.Name));
                        }
                    }
                    if (SeatManage.Bll.AMS_PlayList.DeletePlaylist(playerlist) == SeatManage.EnumType.HandleResult.Failed)
                    {
                        WriteLog.Write(string.Format("删除播放列表{0}失败", playerlist.PlayListNo));
                    }
                    else
                    {
                        WriteLog.Write(string.Format("删除过期的播放列表{0}", playerlist.PlayListNo));
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("清除过期的播放列表遇到异常：{0}", e.Message));
            }
            //清除过期的优惠券
            try
            {
                List<SeatManage.ClassModel.AMS_SlipCustomer> overTimeSlipList = SeatManage.Bll.AMS_SlipCustomer.GetSlipCustomerListOverTime(LogStatus.Fail);
                foreach (SeatManage.ClassModel.AMS_SlipCustomer slip in overTimeSlipList)
                {
                    if (!SeatManage.Bll.FileOperate.FileDelete(slip.ImageName, SeatManageSubsystem.SlipCustomer))
                    {
                        WriteLog.Write(string.Format("删除优惠券{0},优惠图片失败", slip.No));
                    }
                    if (!SeatManage.Bll.FileOperate.FileDelete(slip.CustomerLogo, SeatManageSubsystem.SlipCustomer))
                    {
                        WriteLog.Write(string.Format("删除优惠券{0},Logo图片失败", slip.No));
                    }
                    string printLogo = GetLogoFileName(slip.SlipTemplate);
                    if (printLogo != "")
                    {
                        if (!SeatManage.Bll.FileOperate.FileDelete(printLogo, SeatManageSubsystem.SlipCustomer))
                        {
                            WriteLog.Write(string.Format("删除优惠券{0},打印Logo图片失败", slip.No));
                        }
                    }
                    if (SeatManage.Bll.AMS_SlipCustomer.DeleteSlipCustomer(slip) == SeatManage.EnumType.HandleResult.Failed)
                    {
                        WriteLog.Write(string.Format("删除优惠券{0}失败", slip.No));
                    }
                    else
                    {
                        WriteLog.Write(string.Format("删除过期优惠券{0}", slip.No));
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("清除过期的优惠券遇到异常：{0}", e.Message));
            }
            //清除过期的硬广
            try
            {
                List<SeatManage.ClassModel.HardAdvertInfo> hardAdList = SeatManage.Bll.AMS_HardAd.GetHardAdvertOvertime();
                foreach (SeatManage.ClassModel.HardAdvertInfo hardad in hardAdList)
                {
                    if (SeatManage.Bll.AMS_HardAd.DeleteHardAdvert(hardad) == SeatManage.EnumType.HandleResult.Failed)
                    {
                        WriteLog.Write(string.Format("删除硬广{0}失败", hardad.HardAdvertNo));
                    }
                    else
                    {
                        WriteLog.Write(string.Format("删除过期硬广{0}", hardad.HardAdvertNo));
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("清除过期的硬广遇到异常：{0}", e.Message));
            }
            //清除过期的冠名广告
            try
            {
                List<SeatManage.ClassModel.TitleAdvertInfo> overTimeTitleAdvertList = SeatManage.Bll.AMS_TitleAd.GetTitleAdvertOverTime();
                foreach (SeatManage.ClassModel.TitleAdvertInfo titlead in overTimeTitleAdvertList)
                {
                    if (SeatManage.Bll.AMS_TitleAd.DeleteTitleAdvert(titlead) == SeatManage.EnumType.HandleResult.Failed)
                    {
                        WriteLog.Write(string.Format("删除冠名广告\"{0}\"失败", titlead.TitleAdvert));
                    }
                    else
                    {
                        WriteLog.Write(string.Format("删除过期冠名广告\"{0}\"", titlead.TitleAdvert));
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("清除过期的冠名广告遇到异常：{0}", e.Message));
            }
            //清除过期的打印模板
            try
            {
                List<SeatManage.ClassModel.AMS_PrintTemplateModel> overTimePrintTemplateList = SeatManage.Bll.T_SM_PrintTemplate.GetPrintTemplateOverTime();
                foreach (SeatManage.ClassModel.AMS_PrintTemplateModel printTemplate in overTimePrintTemplateList)
                {
                    if (printTemplate.Describe == "默认母板")
                    {
                        continue;
                    }
                    if (SeatManage.Bll.T_SM_PrintTemplate.DeletePrintTemplate(printTemplate) == SeatManage.EnumType.HandleResult.Failed)
                    {
                        WriteLog.Write(string.Format("删除打印模板{0}失败", printTemplate.Id));
                    }
                    else
                    {
                        WriteLog.Write(string.Format("删除过期打印模板{0}", printTemplate.Id));
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("清除过期的打印模板遇到异常：{0}", e.Message));
            }
            //清除过期的滚动广告
            //try
            //{
            //    List<SeatManage.ClassModel.RollTitlesInfo> overTimeRollTitleList = SeatManage.Bll.AMS_RollTitles.GetOverTimeRollTitle();
            //    foreach (SeatManage.ClassModel.RollTitlesInfo rollTitle in overTimeRollTitleList)
            //    {
            //        if (SeatManage.Bll.AMS_RollTitles.DeleteRollTitles(rollTitle.Num) == SeatManage.EnumType.HandleResult.Failed)
            //        {
            //            WriteLog.Write(string.Format("删除滚动广告{0}失败", rollTitle.Num));
            //        }
            //        else
            //        {
            //            WriteLog.Write(string.Format("删除滚动广告模板{0}", rollTitle.Num));
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    WriteLog.Write(string.Format("清除过期的滚动广告遇到异常：{0}", e.Message));
            //}
        }
        /// <summary>
        /// 删除过期广告
        /// </summary>
        public void AdvertOverTime()
        {
            try
            {
                string error = "";
                List<AMS_Advertisement> modelList = SeatManage.Bll.AdvertisementOperation.GetAdList(true, AdType.None);
                foreach (AMS_Advertisement model in modelList)
                {
                    if (model.Type == AdType.SchoolNotice)
                    {
                        continue;
                    }
                    model.ImageFilePath = AMS_Advertisement.GetDownloadFile(model.AdContent);
                    foreach (string file in model.ImageFilePath)
                    {
                        if (!FileOperate.FileDelete(file, (SeatManage.EnumType.SeatManageSubsystem)System.Enum.Parse(typeof(SeatManage.EnumType.SeatManageSubsystem), model.Type.ToString())))
                        {
                            WriteLog.Write(string.Format("删除过期广告处理遇到异常：文件{0}删除失败", file));
                        }
                    }
                    error = SeatManage.Bll.AdvertisementOperation.DeleteAdModel(model);
                    if (!string.IsNullOrEmpty(SeatManage.Bll.AdvertisementOperation.DeleteAdModel(model)))
                    {
                        WriteLog.Write(string.Format("删除过期广告处理遇到异常：{0}", error));
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("删除过期广告处理遇到异常：{0}", e.Message));
            }
        }
        /// <summary>
        /// 同步读者信息
        /// </summary>
        public void SysnceReaderInfo(ref bool IsUpdate)
        {
            try
            {
                StuLibSyncSetting SyncSet = T_SM_SystemSet.GetStuLibSync();
                if (SyncSet != null)
                {
                    if (SyncSet.SyncMode == StudentSyncMode.OptionalSync)
                    {
                        if (DateTime.Compare(DateTime.Parse(ServiceDateTime.Now.ToShortDateString() + " " + SyncSet.SyncTime), ServiceDateTime.Now) < 0)
                        {
                            if (IsUpdate)//如果为True 执行更新
                            {
                                SeatManage.ISystemTerminal.IStuLibSync.IStuLibSync StuLibSync;
                                try
                                {
                                    StuLibSync = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("IStuLibSync") as SeatManage.ISystemTerminal.IStuLibSync.IStuLibSync;
                                }
                                catch
                                {
                                    IsUpdate = true;
                                    throw;
                                }
                                StuLibSync.StuLibSyncSet = SyncSet;
                                //同步开始执行事件
                                StuLibSync.Syncing += new SeatManage.ISystemTerminal.IStuLibSync.EventHandleSync(StuLibSync_Syncing);
                                //同步结束事件
                                StuLibSync.Synced += new SeatManage.ISystemTerminal.IStuLibSync.EventHandleSync(StuLibSync_Synced);
                                IsUpdate = false;
                                StuLibSync.Sync();
                            }
                        }
                        else  //如果时间过了，重新变为true；
                        {
                            IsUpdate = true;
                        }
                        //  TODO:记录同步结果
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("读者信息同步遇到异常：{0}", e.Message));
            }
        }

        int addAmount = 0;
        int updateAmount = 0;
        int filedAmount = 0;
        void StuLibSync_Synced(object sender, SeatManage.ISystemTerminal.IStuLibSync.SyncPercentEventArgs e)
        {
            if (
            e.State == SeatManage.ISystemTerminal.IStuLibSync.SyncState.Fail)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("同步失败。"));
            }
            SeatManage.SeatManageComm.WriteLog.Write(string.Format("同步完成，新增了{0}条，更新了{1}条，失败{0}条", addAmount, updateAmount, filedAmount));
            addAmount = 0;
            updateAmount = 0;
            filedAmount = 0;
        }

        void StuLibSync_Syncing(object sender, SeatManage.ISystemTerminal.IStuLibSync.SyncPercentEventArgs e)
        {
            if (e.Type == SeatManage.ISystemTerminal.IStuLibSync.SyncType.Add)
            {
                if (e.State == SeatManage.ISystemTerminal.IStuLibSync.SyncState.Success)
                {
                    addAmount += 1;
                }
                else
                {
                    filedAmount += 1;
                }
            }
            else if (e.Type == SeatManage.ISystemTerminal.IStuLibSync.SyncType.Update)
            {
                if (e.State == SeatManage.ISystemTerminal.IStuLibSync.SyncState.Success)
                {
                    updateAmount += 1;
                }
                else
                {
                    filedAmount += 1;
                }
            }
        }
        /// <summary>
        /// 统计进出记录
        /// </summary>
        /// <param name="IsStatistics"></param>
        public void EnterOutStatistics(ref bool IsStatistics)
        {
            if (DateTime.Compare(DateTime.Parse(ServiceDateTime.Now.ToShortDateString() + " 4:00:00"), ServiceDateTime.Now) < 0)
            {
                if (IsStatistics)
                {
                    Service.Code.EnterOutLogStatistics statistics = new Code.EnterOutLogStatistics();
                    try
                    {
                        WriteLog.Write(statistics.StartStatistics());
                        IsStatistics = false;
                    }
                    catch (Exception ex)
                    {
                        WriteLog.Write(ex.Message);
                    }
                }
            }
            else
            {
                IsStatistics = true;
            }
        }
        #endregion


        #region 私有方法
        /// <summary>
        /// 获取全部的阅览室信息
        /// </summary>
        /// <returns></returns>
        private List<ReadingRoomInfo> GetReadingRoomList()
        {
            try
            {
                return T_SM_ReadingRoom.GetReadingRooms(null, null, null);
            }
            catch (Exception e)
            {
                WriteLog.Write(e.Message);
                return null;
            }
        }
        /// <summary>
        /// 开馆处理
        /// </summary>
        /// <param name="readingRooms"></param>
        private void OpenReadingRoom(ReadingRoomInfo room)
        {
            DateTime NowDateTime = ServiceDateTime.Now;
            //添加记录状态
            List<EnterOutLogType> typeList = new List<EnterOutLogType>();
            typeList.Add(EnterOutLogType.BookingConfirmation);
            typeList.Add(EnterOutLogType.ComeBack);
            typeList.Add(EnterOutLogType.SelectSeat);
            typeList.Add(EnterOutLogType.ReselectSeat);
            typeList.Add(EnterOutLogType.WaitingSuccess);
            typeList.Add(EnterOutLogType.ShortLeave);
            typeList.Add(EnterOutLogType.ContinuedTime);
            try
            {
                //获取昨天的进出记录
                List<EnterOutLogInfo> eolList = T_SM_EnterOutLog.GetEnterOutLogByStatus(null, room.No, null, typeList, LogStatus.Valid, "1900-1-1", ServiceDateTime.Now.ToShortDateString());
                foreach (EnterOutLogInfo eol in eolList)
                {
                    if ((eol.EnterOutState == EnterOutLogType.ShortLeave) && (eol.Flag == Operation.Admin || eol.Flag == Operation.OtherReader))
                    {
                        //获取昨天的等待记录
                        List<EnterOutLogType> wslogtype = new List<EnterOutLogType>();
                        wslogtype.Add(EnterOutLogType.Waiting);
                        List<WaitSeatLogInfo> wsllist = T_SM_SeatWaiting.GetWaitSeatList(null, eol.EnterOutLogID, null, null, wslogtype);
                        if (wsllist.Count > 0)
                        {
                            wsllist[0].WaitingState = EnterOutLogType.WaitingCancel;
                            wsllist[0].StatsChangeTime = NowDateTime;
                            T_SM_SeatWaiting.UpdateWaitLog(wsllist[0]);
                        }
                    }
                    eol.EnterOutState = EnterOutLogType.Leave;
                    eol.EnterOutTime = NowDateTime;
                    eol.Flag = Operation.Service;
                    eol.Remark = string.Format("在{0}，{1}号座位，闭馆释放座位", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - room.Setting.SeatNumAmount));
                    int logdi = 0;
                    EnterOutOperate.AddEnterOutLog(eol, ref logdi);
                    WriteLog.Write(string.Format("读者{0}，{1}", eol.CardNo, eol.Remark));
                }
                //预约记录处理
                List<BookingStatus> bstype = new List<BookingStatus>();
                bstype.Add(BookingStatus.Waiting);
                TimeSpan span = NowDateTime - DateTime.Parse("2010-1-1");
                List<BespeakLogInfo> blilist = T_SM_SeatBespeak.GetBespeakList(null, room.No, NowDateTime.AddDays(-1), span.Days, bstype);
                if (blilist.Count > 0)
                {
                    foreach (BespeakLogInfo bli in blilist)
                    {
                        //如果预约超时则更改预约状态
                        if (bli.BsepeakTime.Date < NowDateTime.Date)
                        {
                            bli.CancelTime = bli.BsepeakTime.AddMinutes(int.Parse(room.Setting.SeatBespeak.ConfirmTime.EndTime));
                            bli.CancelPerson = Operation.Service;
                            bli.BsepeakState = BookingStatus.Cencaled;
                            bli.Remark = string.Format("读者，在{0}，{1}号座位，预约，闭馆取消预约", bli.ReadingRoomName, bli.SeatNo.Substring(bli.SeatNo.Length - room.Setting.SeatNumAmount));
                            T_SM_SeatBespeak.UpdateBespeakList(bli);
                            WriteLog.Write(string.Format("读者{0}，{1}", bli.CardNo, bli.Remark));
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 闭馆处理
        /// </summary>
        /// <param name="readingRooms"></param>
        private void CloseReadingRoom(ReadingRoomInfo room)
        {
            DateTime NowDateTime = ServiceDateTime.Now;
            //添加记录状态
            List<EnterOutLogType> typeList = new List<EnterOutLogType>();
            NowReadingRoomState nrrs = new NowReadingRoomState(room);
            typeList.Add(EnterOutLogType.BookingConfirmation);
            typeList.Add(EnterOutLogType.ComeBack);
            typeList.Add(EnterOutLogType.SelectSeat);
            typeList.Add(EnterOutLogType.ReselectSeat);
            typeList.Add(EnterOutLogType.WaitingSuccess);
            typeList.Add(EnterOutLogType.ShortLeave);
            typeList.Add(EnterOutLogType.ContinuedTime);
            try
            {
                //获取今天的进出记录
                List<EnterOutLogInfo> eolList = T_SM_EnterOutLog.GetEnterOutLogByStatus(null, room.No, null, typeList, LogStatus.Valid, NowDateTime.ToShortDateString(), null);
                foreach (EnterOutLogInfo eol in eolList)
                {
                    if ((eol.EnterOutState == EnterOutLogType.ShortLeave) && (eol.Flag == Operation.Admin || eol.Flag == Operation.OtherReader))
                    {
                        //获取昨天的等待记录
                        List<EnterOutLogType> wslogtype = new List<EnterOutLogType>();
                        wslogtype.Add(EnterOutLogType.Waiting);
                        List<WaitSeatLogInfo> wsllist = T_SM_SeatWaiting.GetWaitSeatList(null, eol.EnterOutLogID, null, null, wslogtype);
                        if (wsllist.Count > 0)
                        {
                            wsllist[0].WaitingState = EnterOutLogType.WaitingCancel;
                            wsllist[0].StatsChangeTime = NowDateTime;
                            T_SM_SeatWaiting.UpdateWaitLog(wsllist[0]);
                        }
                    }
                    eol.EnterOutState = EnterOutLogType.Leave;
                    eol.EnterOutTime = NowDateTime;
                    eol.Flag = Operation.Service;
                    eol.Remark = string.Format("在{0}，{1}号座位，闭馆释放座位", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - room.Setting.SeatNumAmount));
                    int logdi = 0;
                    EnterOutOperate.AddEnterOutLog(eol, ref logdi);
                    WriteLog.Write(string.Format("读者{0}，{1}", eol.CardNo, eol.Remark));
                }
                //预约记录处理
                /*
                 *闭馆为什么还要处理预约记录？一旦预约记录有超时就应该及时处理。
                 * 
                 */
                //List<BookingStatus> bstype = new List<BookingStatus>();
                //bstype.Add(BookingStatus.Waiting);
                //List<BespeakLogInfo> blilist = T_SM_SeatBespeak.GetBespeakList(null, room.No, NowDateTime, 0, bstype);
                //if (blilist.Count > 0)
                //{
                //    foreach (BespeakLogInfo bli in blilist)
                //    {
                //        //如果预约超时则更改预约状态
                //        bli.CancelTime = NowDateTime;
                //        bli.CancelPerson = Operation.Service;
                //        bli.BsepeakState = BookingStatus.Cencaled;
                //        bli.Remark = string.Format("读者，在{0}，{1}号座位，预约，闭馆取消预约", bli.ReadingRoomName, bli.SeatNo.Substring(bli.SeatNo.Length - room.Setting.SeatNumAmount));
                //        T_SM_SeatBespeak.UpdateBespeakList(bli);
                //        WriteLog.Write(string.Format("读者{0}，{1}", bli.CardNo, bli.Remark));
                //    }
                //}
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 预约超时
        /// </summary>
        /// <param name="rri">阅览室信息</param>
        /// <param name="NowDateTime">时间</param>
        /// <param name="bli">预约记录</param>
        private static void BookingOverTime(ReadingRoomSetting roomSetting, DateTime NowDateTime, BespeakLogInfo bli)
        {
            try
            {
                bli.CancelTime = NowDateTime;
                bli.CancelPerson = Operation.Service;
                bli.BsepeakState = BookingStatus.Cencaled;
                bli.Remark = string.Format("在{1}，{2}号座位，预约超时", bli.CardNo, bli.ReadingRoomName, bli.SeatNo.Substring(bli.SeatNo.Length - roomSetting.SeatNumAmount));
                T_SM_SeatBespeak.UpdateBespeakList(bli);
                WriteLog.Write(string.Format("读者{0}，{1}", bli.CardNo, bli.Remark));
                if (roomSetting.IsRecordViolate)
                {

                    AddViolationRecordByBookLog(bli, ViolationRecordsType.BookingTimeOut, string.Format("读者在{0}，{1}号座位，预约超时", bli.ReadingRoomName, bli.SeatNo.Substring(bli.SeatNo.Length - roomSetting.SeatNumAmount)), roomSetting, NowDateTime);
                    ReaderNoticeInfo notice = new ReaderNoticeInfo();
                    notice.CardNo = bli.CardNo;
                    notice.Type = NoticeType.BespeakExpiration;
                    notice.Note = "预约的座位因超时已被取消。";
                    T_SM_ReaderNotice.AddReaderNotice(notice);
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 在座超时处理
        /// </summary>
        /// <param name="leavetype">处理类型</param>
        /// <param name="eol">进出记录</param>
        private static void SeatOverTimeOperator(ReadingRoomSetting roomsetting, EnterOutLogInfo eol, DateTime NowDateTime)
        {
            try
            {
                int logdi = 0;
                //在座超时处理
                if (roomsetting.SeatUsedTimeLimit.OverTimeHandle == EnterOutLogType.Leave)
                {
                    eol.EnterOutState = EnterOutLogType.Leave;
                    eol.EnterOutTime = NowDateTime;
                    eol.Flag = Operation.Service;
                    eol.Remark = string.Format("在{0}，{1}号座位，在座超时，监控服务释放座位", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - roomsetting.SeatNumAmount));
                    EnterOutOperate.AddEnterOutLog(eol, ref logdi);
                    WriteLog.Write(string.Format("读者{0}，{1}", eol.CardNo, eol.Remark));
                    //违规处理
                    if (roomsetting.IsRecordViolate)
                    {
                        AddViolationRecordByEnterOutLog(eol, ViolationRecordsType.SeatOutTime, string.Format("读者在{0}，{1}号座位，在座超时", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - roomsetting.SeatNumAmount)), roomsetting, NowDateTime);
                    }
                }
                else if (roomsetting.SeatUsedTimeLimit.OverTimeHandle == EnterOutLogType.ShortLeave)
                {
                    eol.EnterOutState = EnterOutLogType.ShortLeave;
                    eol.EnterOutTime = NowDateTime;
                    eol.Flag = Operation.Service;
                    eol.Remark = string.Format("在{0}，{1}号座位，在座超时，监控服务设置暂离", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - roomsetting.SeatNumAmount));
                    EnterOutOperate.AddEnterOutLog(eol, ref logdi);
                    WriteLog.Write(string.Format("读者{0}，{1}", eol.CardNo, eol.Remark));
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 暂离超时操作
        /// </summary>
        /// <param name="enterOutlog">进出记录</param>
        /// <param name="roomset">阅览室设置</param>
        private static void ShortLeaveOverTimeOperator(EnterOutLogInfo enterOutlog, ReadingRoomSetting readingRoomsetting, DateTime NowDateTime)
        {
            try
            {
                string vrremark = "";
                ViolationRecordsType vrtypt = ViolationRecordsType.ShortLeaveOutTime;
                enterOutlog.EnterOutTime = NowDateTime;
                enterOutlog.EnterOutState = EnterOutLogType.Leave;
                switch (enterOutlog.Flag)
                {//TODO:记录管理员信息
                    case Operation.Admin:
                        enterOutlog.Remark = string.Format("在{0}，{1}号座位，被管理员设置暂离，暂离超时，被监控服务释放座位", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomsetting.SeatNumAmount));
                        vrremark = string.Format("读者在{0}，{1}号座位，被管理员设置暂离，暂离超时", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomsetting.SeatNumAmount));
                        vrtypt = ViolationRecordsType.ShortLeaveByAdminOutTime;
                        break;
                    case Operation.OtherReader:
                        enterOutlog.Remark = string.Format("在{0}，{1}号座位，被其他读者设置暂离，暂离超时，被监控服务释放座位", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomsetting.SeatNumAmount));
                        vrremark = string.Format("读者在{0}，{1}号座位，被其他读者设置暂离，暂离超时", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomsetting.SeatNumAmount));
                        vrtypt = ViolationRecordsType.ShortLeaveByReaderOutTime;
                        break;
                    case Operation.Reader:
                        enterOutlog.Remark = string.Format("在{0}，{1}号座位，暂离超时，被监控服务释放座位", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomsetting.SeatNumAmount));
                        vrremark = string.Format("读者在{0}，{1}号座位，暂离超时", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomsetting.SeatNumAmount));
                        vrtypt = ViolationRecordsType.ShortLeaveOutTime;
                        break;
                    case Operation.Service:
                        enterOutlog.Remark = string.Format("在{0}，{1}号座位，在座超时，监控服务设置暂离，暂离超时，被监控服务释放座位", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomsetting.SeatNumAmount));
                        vrremark = string.Format("读者在{0}，{1}号座位，在座超时，监控服务设置暂离，暂离超时", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomsetting.SeatNumAmount));
                        vrtypt = ViolationRecordsType.SeatOutTime;
                        break;
                    default:
                        enterOutlog.Remark = string.Format("在{0}，{1}号座位，暂离超时，被监控服务释放座位", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomsetting.SeatNumAmount));
                        vrremark = string.Format("读者在{0}，{1}号座位，暂离超时", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomsetting.SeatNumAmount));
                        vrtypt = ViolationRecordsType.ShortLeaveOutTime;
                        break;
                }
                ReaderNoticeInfo notice = new ReaderNoticeInfo();
                notice.CardNo = enterOutlog.CardNo;
                if (enterOutlog.Flag == Operation.Service)//暂离记录为监控服务操作。
                {
                    notice.Type = NoticeType.SeatUsedTimeEnd;
                    notice.Note = "在座超时，座位已经被释放，如果还需继续使用座位，请重新选座";
                }
                else
                {
                    notice.Type = NoticeType.SeatUsedTimeEnd;
                    notice.Note = "暂离超时，座位已经被释放，如果还需继续使用座位，请重新选座";
                }
                T_SM_ReaderNotice.AddReaderNotice(notice);
                enterOutlog.Flag = Operation.Service;
                int logid = 0;
                EnterOutOperate.AddEnterOutLog(enterOutlog, ref logid);
                WriteLog.Write(string.Format("读者{0}，{1}", enterOutlog.CardNo, enterOutlog.Remark));
                if (readingRoomsetting.IsRecordViolate)
                {
                    AddViolationRecordByEnterOutLog(enterOutlog, vrtypt, vrremark, readingRoomsetting, NowDateTime);
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 添加违规记录
        /// </summary>
        /// <param name="eol">进出记录</param>
        /// <param name="type">违规类型</param>
        /// <param name="note">提示信息</param>
        private static void AddViolationRecordByEnterOutLog(EnterOutLogInfo eol, ViolationRecordsType type, string note, ReadingRoomSetting roomsetting, DateTime NowDateTime)
        {
            try
            {
                if (roomsetting.BlackListSetting.Used)
                {
                    if (roomsetting.BlackListSetting.ViolateRoule[type])
                    {
                        ViolationRecordsLogInfo vrli = new ViolationRecordsLogInfo();
                        vrli.CardNo = eol.CardNo;
                        vrli.SeatID = eol.SeatNo;
                        if (vrli.SeatID.Length > roomsetting.SeatNumAmount)
                        {
                            vrli.SeatID = vrli.SeatID.Substring(vrli.SeatID.Length - roomsetting.SeatNumAmount);
                        }
                        vrli.ReadingRoomID = eol.ReadingRoomNo;
                        vrli.EnterOutTime = NowDateTime.ToString();
                        vrli.EnterFlag = type;
                        vrli.Remark = note;
                        T_SM_ViolateDiscipline.AddViolationRecords(vrli);
                        WriteLog.Write(string.Format("读者{0}，{1}，记录违规", vrli.CardNo, vrli.Remark));
                    }
                }
                else if (_regulationRulesSetting.BlacklistSet.Used && _regulationRulesSetting.BlacklistSet.ViolateRoule[type])
                {
                    ViolationRecordsLogInfo vrli = new ViolationRecordsLogInfo();
                    vrli.CardNo = eol.CardNo;
                    vrli.SeatID = eol.SeatNo;
                    if (vrli.SeatID.Length > roomsetting.SeatNumAmount)
                    {
                        vrli.SeatID = vrli.SeatID.Substring(vrli.SeatID.Length - roomsetting.SeatNumAmount);
                    }
                    vrli.ReadingRoomID = eol.ReadingRoomNo;
                    vrli.EnterOutTime = NowDateTime.ToString();
                    vrli.EnterFlag = type;
                    vrli.Remark = note;
                    T_SM_ViolateDiscipline.AddViolationRecords(vrli);
                    WriteLog.Write(string.Format("读者{0}，{1}，记录违规", vrli.CardNo, vrli.Remark));
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据预约记录添加违规记录
        /// </summary>
        /// <param name="blilog">预约记录</param>
        /// <param name="type">违规类型</param>
        /// <param name="note">提示信息</param>
        /// <param name="blacklistset"></param>
        private static void AddViolationRecordByBookLog(BespeakLogInfo blilog, ViolationRecordsType type, string note, ReadingRoomSetting roomsetting, DateTime NowDateTime)
        {
            try
            {
                if (roomsetting.BlackListSetting.Used)
                {
                    if (roomsetting.BlackListSetting.ViolateRoule[type])
                    {
                        ViolationRecordsLogInfo vrli = new ViolationRecordsLogInfo();
                        vrli.CardNo = blilog.CardNo;
                        vrli.SeatID = blilog.SeatNo;
                        if (vrli.SeatID.Length > roomsetting.SeatNumAmount)
                        {
                            vrli.SeatID = vrli.SeatID.Substring(vrli.SeatID.Length - roomsetting.SeatNumAmount);
                        }
                        vrli.ReadingRoomID = blilog.ReadingRoomNo;
                        vrli.EnterOutTime = NowDateTime.ToString();
                        vrli.EnterFlag = type;
                        vrli.Remark = note;
                        T_SM_ViolateDiscipline.AddViolationRecords(vrli);
                        WriteLog.Write(string.Format("读者{0}，{1}，记录违规", vrli.CardNo, vrli.Remark));
                    }
                }
                else if (_regulationRulesSetting.BlacklistSet.Used && _regulationRulesSetting.BlacklistSet.ViolateRoule[type])
                {
                    ViolationRecordsLogInfo vrli = new ViolationRecordsLogInfo();
                    vrli.CardNo = blilog.CardNo;
                    vrli.SeatID = blilog.SeatNo;
                    if (vrli.SeatID.Length > roomsetting.SeatNumAmount)
                    {
                        vrli.SeatID = vrli.SeatID.Substring(vrli.SeatID.Length - roomsetting.SeatNumAmount);
                    }
                    vrli.ReadingRoomID = blilog.ReadingRoomNo;
                    vrli.EnterOutTime = NowDateTime.ToString();
                    vrli.EnterFlag = type;
                    vrli.Remark = note;
                    T_SM_ViolateDiscipline.AddViolationRecords(vrli);
                    WriteLog.Write(string.Format("读者{0}，{1}，记录违规", vrli.CardNo, vrli.Remark));
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 添加读者消息提示
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="Note">消息内容</param>
        private static void AddNotice(string cardNo, string Note)
        {
            try
            {
                ReaderNoticeInfo rni = new ReaderNoticeInfo();
                rni.CardNo = cardNo;
                rni.Note = Note;
                T_SM_ReaderNotice.AddReaderNotice(rni);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 返回最后一条计时记录
        /// </summary>
        /// <param name="eol">进出记录</param>
        private static EnterOutLogInfo GetLastNoSeatTimeLog(EnterOutLogInfo eol)
        {
            try
            {
                List<EnterOutLogType> eoltypeList = new List<EnterOutLogType>();
                eoltypeList.Add(EnterOutLogType.BookingConfirmation);
                eoltypeList.Add(EnterOutLogType.SelectSeat);
                eoltypeList.Add(EnterOutLogType.ReselectSeat);
                eoltypeList.Add(EnterOutLogType.WaitingSuccess);
                eoltypeList.Add(EnterOutLogType.ContinuedTime);
                List<EnterOutLogInfo> lasteol = T_SM_EnterOutLog.GetEnterOutLogByNo(eol.EnterOutLogNo, eoltypeList, 1);
                if (lasteol.Count > 0)
                {
                    return lasteol[0];
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
        /// <summary>
        /// 座位等待处理
        /// </summary>
        /// <param name="readingRoom">阅览室</param>
        /// <param name="enterOutLog">进出记录</param>
        /// <param name="WaitSeatLoglist">等待记录列表</param>
        private static void WaitSeatOperate(ReadingRoomInfo readingRoom, EnterOutLogInfo enterOutLog, List<WaitSeatLogInfo> WaitSeatLoglist, DateTime NowDateTime)
        {
            try
            {
                WaitSeatLoglist[0].WaitingState = EnterOutLogType.WaitingSuccess;
                WaitSeatLoglist[0].StatsChangeTime = NowDateTime;
                T_SM_SeatWaiting.UpdateWaitLog(WaitSeatLoglist[0]);
                ReaderNoticeInfo notice = new ReaderNoticeInfo();

                //释放原读者座位
                int logid = 0;
                enterOutLog.Flag = Operation.Service;
                enterOutLog.EnterOutState = EnterOutLogType.Leave;
                enterOutLog.EnterOutTime = NowDateTime;
                enterOutLog.Remark = string.Format("在{0}，{1}号座位，被其他读者设置暂离，暂离超时，被监控服务释放座位", enterOutLog.ReadingRoomName, enterOutLog.SeatNo.Substring(enterOutLog.SeatNo.Length - readingRoom.Setting.SeatNumAmount));
                EnterOutOperate.AddEnterOutLog(enterOutLog, ref logid);
                notice.CardNo = enterOutLog.CardNo;
                notice.Type = NoticeType.ShortLeaveTimeEndWarning;
                notice.Note = "暂离超时，座位已被释放。";
                T_SM_ReaderNotice.AddReaderNotice(notice);
                WriteLog.Write(string.Format("读者{0}，{1}", enterOutLog.CardNo, enterOutLog.Remark));
                //等待读者入座
                EnterOutLogInfo new_eol = new EnterOutLogInfo();
                new_eol.CardNo = WaitSeatLoglist[0].CardNo;
                new_eol.EnterOutLogNo = SeatComm.RndNum();
                new_eol.EnterOutState = EnterOutLogType.WaitingSuccess;
                new_eol.EnterOutType = LogStatus.Valid;
                new_eol.ReadingRoomNo = WaitSeatLoglist[0].ReadingRoomNo;
                new_eol.Flag = Operation.Service;
                new_eol.SeatNo = enterOutLog.SeatNo;
                new_eol.Remark = string.Format("在{0}，{1}号座位，等待成功，自动入座", enterOutLog.ReadingRoomName, enterOutLog.SeatNo.Substring(enterOutLog.SeatNo.Length - readingRoom.Setting.SeatNumAmount));
                EnterOutOperate.AddEnterOutLog(new_eol, ref logid);
                notice.CardNo = enterOutLog.CardNo;
                notice.Type = NoticeType.WaitSeatSuccess;
                notice.Note = "您等待的座位已经分配给您。";
                T_SM_ReaderNotice.AddReaderNotice(notice);
                WriteLog.Write(string.Format("读者{0}，{1}", new_eol.CardNo, new_eol.Remark));
                if (readingRoom.Setting.IsRecordViolate)
                {
                    AddViolationRecordByEnterOutLog(enterOutLog, ViolationRecordsType.ShortLeaveByReaderOutTime, string.Format("读者在{0}，{1}号座位，被其他读者设置暂离，暂离超时", enterOutLog.ReadingRoomName, enterOutLog.SeatNo.Substring(enterOutLog.SeatNo.Length - readingRoom.Setting.SeatNumAmount)), readingRoom.Setting, NowDateTime);
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取打印模板中的图片名称
        /// </summary>
        /// <returns></returns>
        private static string GetLogoFileName(string printXML)
        {
            string filename = "";
            if (!string.IsNullOrEmpty(printXML))
            {
                XmlDocument Templatedoc = new XmlDocument();
                Templatedoc.LoadXml(printXML);
                XmlElement Templateroot = Templatedoc.DocumentElement;
                XmlNodeList Templatexnlist = ((XmlNode)Templateroot).ChildNodes;
                for (int i = 0; i < Templatexnlist.Count; i++)
                {
                    if (Templatexnlist[i].Name == "Pic" && Templatexnlist[i].InnerText != "南京智佰闻欣logo.png")
                    {
                        filename = Templatexnlist[i].InnerText;
                    }
                }
            }
            return filename;
        }
        #endregion

    }
}
