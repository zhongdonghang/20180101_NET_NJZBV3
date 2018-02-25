using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatService.MonitorService.Code
{
    public partial class SeatDataOperation
    {
        /// <summary>
        /// 违规记录超时处理
        /// </summary>
        public void ViolationRecordsOperating()
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
                        if (roomList[vrli.ReadingRoomID].Setting.BlackListSetting.Used && DateTime.Parse(vrli.EnterOutTime).AddDays(roomList[vrli.ReadingRoomID].Setting.BlackListSetting.ViolateFailDays) < NowDateTime)
                        {
                            vrli.Flag = LogStatus.Fail;
                            T_SM_ViolateDiscipline.UpdateViolationRecords(vrli);

                            //ReaderNoticeInfo notice = new ReaderNoticeInfo();
                            //notice.CardNo = vrli.CardNo;
                            //notice.Type = NoticeType.DeleteViolation;
                            //notice.Note = string.Format("{0}记录的违规，{1}，过期", vrli.EnterOutTime, vrli.Remark);
                            //T_SM_ReaderNotice.AddReaderNotice(notice);
                        }
                        else if (regulationRulesSetting.BlacklistSet.Used && DateTime.Parse(vrli.EnterOutTime).AddDays(regulationRulesSetting.BlacklistSet.ViolateFailDays) < NowDateTime)
                        {
                            vrli.Flag = LogStatus.Fail;
                            T_SM_ViolateDiscipline.UpdateViolationRecords(vrli);
                            WriteLog.Write(string.Format("监控服务：读者{0}，违规记录过期", vrli.CardNo));

                            //ReaderNoticeInfo notice = new ReaderNoticeInfo();
                            //notice.CardNo = vrli.CardNo;
                            //notice.Type = NoticeType.DeleteViolation;
                            //notice.Note = string.Format("{0}记录的违规，{1}，过期", vrli.EnterOutTime, vrli.Remark);
                            //T_SM_ReaderNotice.AddReaderNotice(notice);
                        }
                        else if (!regulationRulesSetting.BlacklistSet.Used && !roomList[vrli.ReadingRoomID].Setting.BlackListSetting.Used)
                        {
                            vrli.Flag = LogStatus.Fail;
                            T_SM_ViolateDiscipline.UpdateViolationRecords(vrli);
                            WriteLog.Write(string.Format("监控服务：读者{0}，违规记录过期", vrli.CardNo));

                            //ReaderNoticeInfo notice = new ReaderNoticeInfo();
                            //notice.CardNo = vrli.CardNo;
                            //notice.Type = NoticeType.DeleteViolation;
                            //notice.Note = string.Format("{0}记录的违规，{1}，过期", vrli.EnterOutTime, vrli.Remark);
                            //T_SM_ReaderNotice.AddReaderNotice(notice);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("监控服务：违规记录超时处理遇到异常：{0}", e.Message));
            }
        }
        /// <summary>
        /// 添加违规记录
        /// </summary>
        /// <param name="eol">进出记录</param>
        /// <param name="type">违规类型</param>
        /// <param name="note">提示信息</param>
        private static void AddViolationRecordByEnterOutLog(EnterOutLogInfo eol, ViolationRecordsType type, string note, ReadingRoomSetting roomSetting, DateTime nowDateTime)
        {
            try
            {
                if (roomSetting.BlackListSetting.Used && !roomSetting.BlackListSetting.ViolateRoule[type])
                {
                    return;
                }
                if (!regulationRulesSetting.BlacklistSet.Used || !regulationRulesSetting.BlacklistSet.ViolateRoule[type])
                {
                    return;
                }
                ViolationRecordsLogInfo vrli = new ViolationRecordsLogInfo();
                vrli.CardNo = eol.CardNo;
                vrli.SeatID = eol.SeatNo;
                if (vrli.SeatID.Length > roomSetting.SeatNumAmount)
                {
                    vrli.SeatID = vrli.SeatID.Substring(vrli.SeatID.Length - roomSetting.SeatNumAmount);
                }
                vrli.ReadingRoomID = eol.ReadingRoomNo;
                vrli.EnterOutTime = nowDateTime.ToString();
                vrli.EnterFlag = type;
                vrli.Remark = note;
                T_SM_ViolateDiscipline.AddViolationRecords(vrli);
                WriteLog.Write(string.Format("监控服务：读者{0}，{1}，记录违规", vrli.CardNo, vrli.Remark));
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：添加违规记录发生错误：" + ex.Message));
            }
        }
        /// <summary>
        /// 根据预约记录添加违规记录
        /// </summary>
        /// <param name="blilog">预约记录</param>
        /// <param name="type">违规类型</param>
        /// <param name="note">提示信息</param>
        /// <param name="blacklistset"></param>
        private static void AddViolationRecordByBookLog(BespeakLogInfo blilog, ViolationRecordsType type, string note, ReadingRoomSetting roomSetting, DateTime nowDateTime)
        {
            try
            {
                if (roomSetting.BlackListSetting.Used && !roomSetting.BlackListSetting.ViolateRoule[type])
                {
                    return;
                }
                if (!regulationRulesSetting.BlacklistSet.Used || !regulationRulesSetting.BlacklistSet.ViolateRoule[type])
                {
                    return;
                }
                ViolationRecordsLogInfo vrli = new ViolationRecordsLogInfo();
                vrli.CardNo = blilog.CardNo;
                vrli.SeatID = blilog.SeatNo;
                if (vrli.SeatID.Length > roomSetting.SeatNumAmount)
                {
                    vrli.SeatID = vrli.SeatID.Substring(vrli.SeatID.Length - roomSetting.SeatNumAmount);
                }
                vrli.ReadingRoomID = blilog.ReadingRoomNo;
                vrli.EnterOutTime = nowDateTime.ToString();
                vrli.EnterFlag = type;
                vrli.Remark = note;
                T_SM_ViolateDiscipline.AddViolationRecords(vrli);
                WriteLog.Write(string.Format("监控服务：读者{0}，{1}，记录违规", vrli.CardNo, vrli.Remark));
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：添加违规记录发生错误：" + ex.Message));
            }
        }
        
    }
}
