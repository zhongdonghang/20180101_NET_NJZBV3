using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.JsonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebServiceJsonTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_ActualTimeRecordParam());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_ActualTimeRecords());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_AdminShortLeaveSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_BespeakLog());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_Blacklist());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_BlacklistSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_EnterOutLog());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_HandleResult());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_LimitReaderEnterSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_Node());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_OpenBespeakReadingRoom());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_POSRestrict());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_ReadingRoom());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_ReadingRoomBlacklistSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_RoomOpenClosePlan());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_RoomOpenTimeSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_RoomSeatUsedState());
            //SeatManage.JsonModel.JM_RoomSet jm_roomSet = new SeatManage.JsonModel.JM_RoomSet();
            //SeatManage.ClassModel.ReadingRoomSetting roomSet = new SeatManage.ClassModel.ReadingRoomSetting("<?xml version=\"1.0\" encoding=\"utf-8\"?><rootNode><seatChooseMethod chooseMethod=\"2\" usedAdvancedSet=\"1\"><chooseMethodPlan dayOfWeek=\"0\" used=\"0\" /><chooseMethodPlan dayOfWeek=\"1\" used=\"1\"><planOption chooseMethod=\"2\" beginTime=\"8:00\" endTime=\"10:00\" /></chooseMethodPlan><chooseMethodPlan dayOfWeek=\"2\" used=\"0\" /><chooseMethodPlan dayOfWeek=\"3\" used=\"0\" /><chooseMethodPlan dayOfWeek=\"4\" used=\"0\" /><chooseMethodPlan dayOfWeek=\"5\" used=\"0\" /><chooseMethodPlan dayOfWeek=\"6\" used=\"0\" /></seatChooseMethod><seatHoldTime defaultHoldTimeLength=\"30\" usedAdvancedSet=\"1\"><option holdTimeLength=\"60\" beginTime=\"11:00\" endTime=\"12:00\" used=\"1\" /><option holdTimeLength=\"60\" beginTime=\"17:00\" endTime=\"18:00\" used=\"1\" /></seatHoldTime><seatNumAmount Amount=\"3\" /><roomOpenSet beginTime=\"8:00\" endTime=\"22:50\" openBeforeTimeLength=\"30\" closeBeforeTimeLength=\"30\" usedAdvancedSet=\"1\" UninterruptibleModel=\"0\"><roomOpenPlan used=\"0\" dayOfWeek=\"0\" /><roomOpenPlan used=\"0\" dayOfWeek=\"1\" /><roomOpenPlan used=\"1\" dayOfWeek=\"2\"><opens beginTime=\"8:00\" endTime=\"23:00\" /></roomOpenPlan><roomOpenPlan used=\"0\" dayOfWeek=\"3\" /><roomOpenPlan used=\"0\" dayOfWeek=\"4\" /><roomOpenPlan used=\"0\" dayOfWeek=\"5\" /><roomOpenPlan used=\"0\" dayOfWeek=\"6\" /></roomOpenSet><noManagement used=\"1\" operatingInterval=\"30\" /><usedBlacklistLimit used=\"1\" /><isRecordViolate used=\"1\" /><blacklist used=\"0\" violateTimes=\"3\" limitDays=\"7\" leaveBlacklist=\"0\" ViolateFailDays=\"60\"><violateType used=\"1\" typeValue=\"0\" /><violateType used=\"1\" typeValue=\"1\" /><violateType used=\"0\" typeValue=\"2\" /><violateType used=\"1\" typeValue=\"4\" /><violateType used=\"1\" typeValue=\"5\" /><violateType used=\"0\" typeValue=\"6\" /><violateType used=\"0\" typeValue=\"3\" /><violateType used=\"0\" typeValue=\"7\" /></blacklist><seatUsedTimeLimit used=\"1\" usedTimeLength=\"30\" overTimeHandle=\"0\" IsCanContinuedTimes=\"1\" delayTimeLength=\"30\" continuedTimes=\"3\" CanDelayTime=\"10\" Mode=\"Fixed\"><FixedTimes><Plan Time=\"14:00:00\" /><Plan Time=\"18:00:00\" /></FixedTimes></seatUsedTimeLimit><seatBespeakSet bespeakBeforeDays=\"5\" BespeakSeatCount=\"5\" allowDelayTime=\"1\" allowShortLeave=\"1\" allowLeave=\"1\" NowDayBespeak=\"1\" SeatKeepTime=\"20\" confirmBeforeTime=\"10\" confirmEndTime=\"20\" used=\"1\" BespeatWithOnSeat=\"1\" canBespeakBeginTime=\"0:00\" canBespeakEndTime=\"23:59\" SpecifiedBespeak=\"1\" SelectBespeakSeat=\"1\" SpecifiedTime=\"0\"><bespeakArea bespeakType=\"1\" scale=\"0.3\"><noBespeakDates beginDate=\"2015/1/1\" endDate=\"2015/2/2\" /></bespeakArea><SpecifiedTimeList><TimeItem Time=\"10:00\" /><TimeItem Time=\"12:00\" /><TimeItem Time=\"14:00\" /><TimeItem Time=\"16:00\" /><TimeItem Time=\"18:00\" /></SpecifiedTimeList></seatBespeakSet><limitReaderEnter readerTypes=\"离退休教工;专科;自考生;教工;交换留学生\" canEnter=\"0\" used=\"0\" /><adminSetShortLeave IsUsed=\"0\" seatholeTime=\"20\" /><POSRestrict IsUsed=\"1\" Minutes=\"10\" Times=\"3\" /></rootNode>");
            //jm_roomSet.BlackListSet.IsLimitBlacklist = roomSet.UsedBlacklistLimit;
            //jm_roomSet.BlackListSet.IsViolation = roomSet.IsRecordViolate;
            //jm_roomSet.BlackListSet.Setting.AutoLeaveDays = roomSet.BlackListSetting.LimitDays;
            //jm_roomSet.BlackListSet.Setting.IsUsed = roomSet.BlackListSetting.Used;
            //jm_roomSet.BlackListSet.Setting.IsUseViolationBookingTimeOut = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.BookingTimeOut];
            //jm_roomSet.BlackListSet.Setting.IsUseViolationLeaveByAdmin = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.LeaveByAdmin];
            //jm_roomSet.BlackListSet.Setting.IsUseViolationSeatOutTime = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.SeatOutTime];
            //jm_roomSet.BlackListSet.Setting.IsUseViolationShortLeaveByAdminOutTime = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.ShortLeaveByAdminOutTime];
            //jm_roomSet.BlackListSet.Setting.IsUseViolationShortLeaveByReaderOutTime = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.ShortLeaveByReaderOutTime];
            //jm_roomSet.BlackListSet.Setting.IsUseViolationShortLeaveOutTime = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.ShortLeaveByServiceOutTime];
            //jm_roomSet.BlackListSet.Setting.LeaveBlacklistModel = roomSet.BlackListSetting.LeaveBlacklist.ToString();
            //jm_roomSet.BlackListSet.Setting.ViolateCountWithEnterBlacklist = roomSet.BlackListSetting.ViolateTimes;
            //jm_roomSet.BlackListSet.Setting.ViolateFailDays = roomSet.BlackListSetting.ViolateFailDays;
            //jm_roomSet.LimitReaderEnterSet.CanEnter = roomSet.LimitReaderEnter.CanEnter;
            //jm_roomSet.LimitReaderEnterSet.ReaderTypes = roomSet.LimitReaderEnter.ReaderTypes;
            //jm_roomSet.LimitReaderEnterSet.Used = roomSet.LimitReaderEnter.Used;
            //jm_roomSet.PosRestrict.IsUsed = roomSet.PosTimes.IsUsed;
            //jm_roomSet.PosRestrict.Minutes = roomSet.PosTimes.Minutes;
            //jm_roomSet.PosRestrict.Times = roomSet.PosTimes.Times;
            //jm_roomSet.RoomOCPlanSet.AdvancedOpenClosePlan = new List<JM_RoomOpenClosePlan>();
            //foreach (KeyValuePair<DayOfWeek, SeatManage.ClassModel.RoomOpenPlanSet> day in roomSet.RoomOpenSet.RoomOpenPlan)
            //{
            //    JM_RoomOpenClosePlan plan = new JM_RoomOpenClosePlan();
            //    plan.Day = day.Key.ToString();
            //    plan.IsUsed = day.Value.Used;
            //    plan.OpenCloseTimeSpan = new List<JM_TimeSpan>();
            //    foreach (TimeSpace ts in day.Value.OpenTime)
            //    {
            //        JM_TimeSpan jm_ts = new JM_TimeSpan();
            //        jm_ts.StartTime = ts.BeginTime;
            //        jm_ts.EndTime = ts.EndTime;
            //        plan.OpenCloseTimeSpan.Add(jm_ts);
            //    }
            //    jm_roomSet.RoomOCPlanSet.AdvancedOpenClosePlan.Add(plan);
            //}
            //jm_roomSet.RoomOCPlanSet.CloseBeforeTimeLength = int.Parse(roomSet.RoomOpenSet.CloseBeforeTimeLength.ToString().Split('.')[0]);
            //jm_roomSet.RoomOCPlanSet.DefaultCloseTime = roomSet.RoomOpenSet.DefaultOpenTime.EndTime;
            //jm_roomSet.RoomOCPlanSet.DefaultOpenTime = roomSet.RoomOpenSet.DefaultOpenTime.BeginTime;
            //jm_roomSet.RoomOCPlanSet.IsUsed24HourModel = roomSet.RoomOpenSet.UninterruptibleModel;
            //jm_roomSet.RoomOCPlanSet.IsUsedAdvancedModel = roomSet.RoomOpenSet.UsedAdvancedSet;
            //jm_roomSet.RoomOCPlanSet.OpenBeforeTimeLength = int.Parse(roomSet.RoomOpenSet.OpenBeforeTimeLength.ToString().Split('.')[0]);
            //jm_roomSet.SeatBespeakSet.CanBespeakSeatCount = roomSet.SeatBespeak.BespeakSeatCount;
            //jm_roomSet.SeatBespeakSet.CanBespeakTimeSpan = new JM_TimeSpan();
            //jm_roomSet.SeatBespeakSet.CanBespeakTimeSpan.StartTime = roomSet.SeatBespeak.CanBespeatTimeSpace.BeginTime;
            //jm_roomSet.SeatBespeakSet.CanBespeakTimeSpan.EndTime = roomSet.SeatBespeak.CanBespeatTimeSpace.EndTime;
            //jm_roomSet.SeatBespeakSet.CanNotBespeakDate = new List<JM_TimeSpan>();
            //foreach (TimeSpace ts in roomSet.SeatBespeak.NoBespeakDates)
            //{
            //    JM_TimeSpan jm_ts = new JM_TimeSpan();
            //    jm_ts.StartTime = ts.BeginTime;
            //    jm_ts.EndTime = ts.EndTime;
            //    jm_roomSet.SeatBespeakSet.CanNotBespeakDate.Add(jm_ts);
            //}
            //jm_roomSet.SeatBespeakSet.IsCanBespeakWithOnSeat = roomSet.SeatBespeak.BespeatWithOnSeat;
            //jm_roomSet.SeatBespeakSet.IsCanSelectBespeakSeat = roomSet.SeatBespeak.SelectBespeakSeat;
            //jm_roomSet.SeatBespeakSet.IsSpecifiedTime = roomSet.SeatBespeak.SpecifiedTime;
            //jm_roomSet.SeatBespeakSet.IsUsedMultiSpanBespeak = roomSet.SeatBespeak.SpecifiedBespeak;
            //jm_roomSet.SeatBespeakSet.IsUsedNowDaySeatBespeak = roomSet.SeatBespeak.NowDayBespeak;
            //jm_roomSet.SeatBespeakSet.IsUsedSeatBespeak = roomSet.SeatBespeak.Used;
            //jm_roomSet.SeatBespeakSet.NowDaySeatKeepTimeLength = int.Parse(roomSet.SeatBespeak.SeatKeepTime.ToString().Split('.')[0]);
            //jm_roomSet.SeatBespeakSet.SigninTimeSpan = new JM_TimeSpan();
            //jm_roomSet.SeatBespeakSet.SigninTimeSpan.StartTime = roomSet.SeatBespeak.ConfirmTime.BeginTime;
            //jm_roomSet.SeatBespeakSet.SigninTimeSpan.EndTime = roomSet.SeatBespeak.ConfirmTime.EndTime;
            //jm_roomSet.SeatBespeakSet.SpecifiedTimeList = new List<string>();
            //foreach (DateTime item in roomSet.SeatBespeak.SpecifiedTimeList)
            //{
            //    jm_roomSet.SeatBespeakSet.SpecifiedTimeList.Add(item.ToShortTimeString());
            //}
            //jm_roomSet.SeatChooseMethod.DefaultChooseMethod = roomSet.SeatChooseMethod.DefaultChooseMethod.ToString();
            //jm_roomSet.SeatChooseMethod.UsedAdvancedSet = roomSet.SeatChooseMethod.UsedAdvancedSet;
            //jm_roomSet.SeatChooseMethod.AdvancedSet = new List<JM_SeatChooseMethodPlan>();
            //foreach (KeyValuePair<DayOfWeek, SeatChooseMethodPlan> item in roomSet.SeatChooseMethod.AdvancedSelectSeatMode)
            //{
            //    JM_SeatChooseMethodPlan jm_plan = new JM_SeatChooseMethodPlan();
            //    jm_plan.Day = item.Key.ToString();
            //    jm_plan.IsUsed = item.Value.Used;
            //    jm_plan.DayPlan = new List<JM_SeatChooseMethodAdvancedSet>();
            //    foreach (SeatChooseMethodOption option in item.Value.PlanOption)
            //    {
            //        JM_SeatChooseMethodAdvancedSet s = new JM_SeatChooseMethodAdvancedSet();
            //        s.ChooseMethod = option.ChooseMethod.ToString();
            //        s.ChooseMethodTimeSpan = new JM_TimeSpan();
            //        s.ChooseMethodTimeSpan.StartTime = option.UsedTime.BeginTime;
            //        s.ChooseMethodTimeSpan.EndTime = option.UsedTime.EndTime;
            //        jm_plan.DayPlan.Add(s);
            //    }
            //    jm_roomSet.SeatChooseMethod.AdvancedSet.Add(jm_plan);
            //}
            //jm_roomSet.SeatShortLeaveSet.DefaultHoldTimeLength = roomSet.SeatHoldTime.DefaultHoldTimeLength;
            //jm_roomSet.SeatShortLeaveSet.AdminSet = new JM_AdminShortLeaveSet();
            //jm_roomSet.SeatShortLeaveSet.AdminSet.HoldTimeLength = roomSet.AdminShortLeave.HoldTimeLength;
            //jm_roomSet.SeatShortLeaveSet.AdminSet.IsUsed = roomSet.AdminShortLeave.IsUsed;
            //jm_roomSet.SeatShortLeaveSet.AdvancedSet = new List<JM_ShortLeavePlan>();
            //foreach (SeatHoldTimeOption item in roomSet.SeatHoldTime.AdvancedSeatHoldTime)
            //{
            //    JM_ShortLeavePlan plan = new JM_ShortLeavePlan();
            //    plan.ChooseMethodTimeSpan = new JM_TimeSpan();
            //    plan.ChooseMethodTimeSpan.StartTime = item.UsedTime.BeginTime;
            //    plan.ChooseMethodTimeSpan.EndTime = item.UsedTime.EndTime;
            //    plan.HoldTimeLength = item.HoldTimeLength;
            //    plan.IsUsed = item.Used;
            //    jm_roomSet.SeatShortLeaveSet.AdvancedSet.Add(plan);
            //}
            //jm_roomSet.SeatUsedTimeSet.CanContinuedTimes = roomSet.SeatUsedTimeLimit.ContinuedTimes;
            //jm_roomSet.SeatUsedTimeSet.DelayLastTimeLength = int.Parse(roomSet.SeatUsedTimeLimit.CanDelayTime.ToString().Split('.')[0]);
            //jm_roomSet.SeatUsedTimeSet.DelayTimeLength = int.Parse(roomSet.SeatUsedTimeLimit.DelayTimeLength.ToString().Split('.')[0]);
            //jm_roomSet.SeatUsedTimeSet.FixedTimes = new List<string>();
            //foreach (DateTime dt in roomSet.SeatUsedTimeLimit.FixedTimes)
            //{
            //    jm_roomSet.SeatUsedTimeSet.FixedTimes.Add(dt.ToShortTimeString());
            //}
            //jm_roomSet.SeatUsedTimeSet.HoldTimeModel = roomSet.SeatUsedTimeLimit.Mode;
            //jm_roomSet.SeatUsedTimeSet.IsUsed = roomSet.SeatUsedTimeLimit.Used;
            //jm_roomSet.SeatUsedTimeSet.IsUsedContinueTime = roomSet.SeatUsedTimeLimit.IsCanContinuedTime;
            //jm_roomSet.SeatUsedTimeSet.OverTimeHandle = roomSet.SeatUsedTimeLimit.OverTimeHandle.ToString();
            //jm_roomSet.SeatUsedTimeSet.SeatHoldTimeLength = int.Parse(roomSet.SeatUsedTimeLimit.UsedTimeLength.ToString().Split('.')[0]);
            //jm_roomSet.SeatWaitSet.IsUsed = roomSet.NoManagement.Used;
            //jm_roomSet.SeatWaitSet.OperatingTimeInterval = int.Parse(roomSet.NoManagement.OperatingInterval.ToString().Split('.')[0]);
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_roomSet);
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_ScanResult());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_SchoolModel());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_Seat());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_SeatBespeakSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_SeatChooseMethodAdvancedSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_SeatChooseMethodPlan());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_SeatChooseMethodSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_SeatLayout());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_SeatUsedTimeLimitSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_SeatWaitSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_ShortLeavePlan());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_ShortLeaveSet());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_TimeSpan());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_User());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_ViolationRecordsLog());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_WaitSeatLog());
            //richTextBox1.Text += "\n" + SeatManage.SeatManageComm.JSONSerializer.Serialize(new List<SeatManage.JsonModel.JM_EnterOutLog>() { new SeatManage.JsonModel.JM_EnterOutLog(), new SeatManage.JsonModel.JM_EnterOutLog() ,new SeatManage.JsonModel.JM_EnterOutLog()});

        }
    }
}
