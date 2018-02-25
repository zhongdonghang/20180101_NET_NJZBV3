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
        public void BeapeakLogOperating()
        {
            try
            {
                DateTime nowDateTime = ServiceDateTime.Now;
                //预约超时处理
                List<BespeakLogInfo> blilist = T_SM_SeatBespeak.GetNotCheckedBespeakLogInfo(null, nowDateTime);
                if (blilist.Count <= 0)
                {
                    return;
                }
                foreach (BespeakLogInfo bli in blilist)
                {
                    if (roomList[bli.ReadingRoomNo].Setting.SeatBespeak.NowDayBespeak && bli.BsepeakTime.Date == bli.SubmitTime.Date)
                    {
                        if (bli.BsepeakTime.AddMinutes(roomList[bli.ReadingRoomNo].Setting.SeatBespeak.SeatKeepTime) < nowDateTime)
                        {
                            BookingOverTime(roomList[bli.ReadingRoomNo].Setting, nowDateTime, bli);
                        }
                    }
                    //如果预约超时则更改预约状态
                    else
                    {
                        if (bli.BsepeakTime.AddMinutes(int.Parse(roomList[bli.ReadingRoomNo].Setting.SeatBespeak.ConfirmTime.EndTime)) < nowDateTime)
                        {
                            BookingOverTime(roomList[bli.ReadingRoomNo].Setting, nowDateTime, bli);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：预约超时处理遇到异常{0}", ex.Message));
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
                bli.Remark = string.Format("在{0}，{1}号座位，预约超时", bli.ReadingRoomName, bli.SeatNo.Substring(bli.SeatNo.Length - roomSetting.SeatNumAmount));
                T_SM_SeatBespeak.UpdateBespeakList(bli);
                WriteLog.Write(string.Format("读者{0}，{1}", bli.CardNo, bli.Remark));
                if (roomSetting.IsRecordViolate)
                {
                    AddViolationRecordByBookLog(bli, ViolationRecordsType.BookingTimeOut, string.Format("读者在{0}，{1}号座位，预约超时", bli.ReadingRoomName, bli.SeatNo.Substring(bli.SeatNo.Length - roomSetting.SeatNumAmount)), roomSetting, NowDateTime);
                    //ReaderNoticeInfo notice = new ReaderNoticeInfo();
                    //notice.CardNo = bli.CardNo;
                    //notice.Type = NoticeType.BespeakExpiration;
                    //notice.Note = "预约的座位因超时已被取消。";
                    //T_SM_ReaderNotice.AddReaderNotice(notice);
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：处理读者预约超时发生错误：" + ex.Message));
            }
        }
    }
}
