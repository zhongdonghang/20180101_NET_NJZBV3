using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_RoomSet
    {
        JM_SeatChooseMethodSet seatChooseMethod = new JM_SeatChooseMethodSet();
        /// <summary>
        /// 选座方式设置
        /// </summary>
        public JM_SeatChooseMethodSet SeatChooseMethod
        {
            get { return seatChooseMethod; }
            set { seatChooseMethod = value; }
        }
        JM_ShortLeaveSet seatShortLeaveSet= new JM_ShortLeaveSet();
        /// <summary>
        /// 座位暂离设置
        /// </summary>
        public JM_ShortLeaveSet SeatShortLeaveSet
        {
            get { return seatShortLeaveSet; }
            set { seatShortLeaveSet = value; }
        }
        JM_RoomOpenTimeSet roomOCPlanSet = new JM_RoomOpenTimeSet();
        /// <summary>
        /// 阅览室开闭馆计划
        /// </summary>
        public JM_RoomOpenTimeSet RoomOCPlanSet
        {
            get { return roomOCPlanSet; }
            set { roomOCPlanSet = value; }
        }
        JM_SeatWaitSet seatWaitSet = new JM_SeatWaitSet();
        /// <summary>
        /// 座位等待设置
        /// </summary>
        public JM_SeatWaitSet SeatWaitSet
        {
            get { return seatWaitSet; }
            set { seatWaitSet = value; }
        }
        JM_ReadingRoomBlacklistSet blackListSet = new JM_ReadingRoomBlacklistSet();
        /// <summary>
        /// 黑名单设置
        /// </summary>
        public JM_ReadingRoomBlacklistSet BlackListSet
        {
            get { return blackListSet; }
            set { blackListSet = value; }
        }
        JM_SeatUsedTimeLimitSet seatUsedTimeSet = new JM_SeatUsedTimeLimitSet();
        /// <summary>
        /// 在座时长限制
        /// </summary>
        public JM_SeatUsedTimeLimitSet SeatUsedTimeSet
        {
            get { return seatUsedTimeSet; }
            set { seatUsedTimeSet = value; }
        }
        JM_SeatBespeakSet seatBespeakSet = new JM_SeatBespeakSet();
        /// <summary>
        /// 座位预约设置
        /// </summary>
        public JM_SeatBespeakSet SeatBespeakSet
        {
            get { return seatBespeakSet; }
            set { seatBespeakSet = value; }
        }
        private JM_LimitReaderEnterSet limitReaderEnterSet = new JM_LimitReaderEnterSet();
        /// <summary>
        /// 限制读者进入设置
        /// </summary>
        public JM_LimitReaderEnterSet LimitReaderEnterSet
        {
            get { return limitReaderEnterSet; }
            set { limitReaderEnterSet = value; }
        }
        JM_POSRestrict posRestrict = new JM_POSRestrict();
        /// <summary>
        /// 刷卡次数设置
        /// </summary>
        public JM_POSRestrict PosRestrict
        {
            get { return posRestrict; }
            set { posRestrict = value; }
        }
    }

    #region 选座模式设置
    /// <summary>
    /// 选座方式设置
    /// </summary>
    public class JM_SeatChooseMethodSet
    {
        string defaultChooseMethod;
        /// <summary>
        /// 默认选座模式
        /// </summary>
        public string DefaultChooseMethod
        {
            get { return defaultChooseMethod; }
            set { defaultChooseMethod = value; }
        }
        bool usedAdvancedSet;
        /// <summary>
        /// 是否启用高级选项
        /// </summary>
        public bool UsedAdvancedSet
        {
            get { return usedAdvancedSet; }
            set { usedAdvancedSet = value; }
        }
        List<JM_SeatChooseMethodPlan> advancedSet;
        /// <summary>
        /// 高级设置
        /// </summary>
        public List<JM_SeatChooseMethodPlan> AdvancedSet
        {
            get { return advancedSet; }
            set { advancedSet = value; }
        }
    }
    /// <summary>
    /// 选座模式时间计划
    /// </summary>
    public class JM_SeatChooseMethodPlan
    {
        string day;
        /// <summary>
        /// 日期（星期几）
        /// </summary>
        public string Day
        {
            get { return day; }
            set { day = value; }
        }
        bool isUsed;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }
        List<JM_SeatChooseMethodAdvancedSet> dayPlan;
        /// <summary>
        /// 选座模式计划
        /// </summary>
        public List<JM_SeatChooseMethodAdvancedSet> DayPlan
        {
            get { return dayPlan; }
            set { dayPlan = value; }
        }
    }
    /// <summary>
    /// 选座模式高级设置
    /// </summary>
    public class JM_SeatChooseMethodAdvancedSet
    {
        JM_TimeSpan chooseMethodTimeSpan;
        /// <summary>
        /// 计划时间段（时:分）
        /// </summary>
        public JM_TimeSpan ChooseMethodTimeSpan
        {
            get { return chooseMethodTimeSpan; }
            set { chooseMethodTimeSpan = value; }
        }
        string chooseMethod;
        /// <summary>
        /// 选座模式
        /// </summary>
        public string ChooseMethod
        {
            get { return chooseMethod; }
            set { chooseMethod = value; }
        }
    }
    #endregion

    #region 暂离设置
    /// <summary>
    /// 座位暂离设置
    /// </summary>
    public class JM_ShortLeaveSet
    {
        int defaultHoldTimeLength;
        /// <summary>
        /// 默认保留时长（分钟）
        /// </summary>
        public int DefaultHoldTimeLength
        {
            get { return defaultHoldTimeLength; }
            set { defaultHoldTimeLength = value; }
        }
        JM_AdminShortLeaveSet adminSet;
        /// <summary>
        /// 管理员暂离设置
        /// </summary>
        public JM_AdminShortLeaveSet AdminSet
        {
            get { return adminSet; }
            set { adminSet = value; }
        }
        List<JM_ShortLeavePlan> advancedSet;
        /// <summary>
        /// 高级设置
        /// </summary>
        public List<JM_ShortLeavePlan> AdvancedSet
        {
            get { return advancedSet; }
            set { advancedSet = value; }
        }
    }
    /// <summary>
    /// 管理员暂离设置
    /// </summary>
    public class JM_AdminShortLeaveSet
    {
        bool isUsed;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }
        int holdTimeLength;
        /// <summary>
        /// 保留时长（分钟）
        /// </summary>
        public int HoldTimeLength
        {
            get { return holdTimeLength; }
            set { holdTimeLength = value; }
        }
    }
    /// <summary>
    /// 座位暂离高级设置
    /// </summary>
    public class JM_ShortLeavePlan
    {
        int holdTimeLength;
        /// <summary>
        /// 保留时长（分钟）
        /// </summary>
        public int HoldTimeLength
        {
            get { return holdTimeLength; }
            set { holdTimeLength = value; }
        }
        JM_TimeSpan chooseMethodTimeSpan;
        /// <summary>
        /// 计划时间段（时:分）
        /// </summary>
        public JM_TimeSpan ChooseMethodTimeSpan
        {
            get { return chooseMethodTimeSpan; }
            set { chooseMethodTimeSpan = value; }
        }
        bool isUsed;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }
    }
    #endregion

    #region 开闭馆设置
    /// <summary>
    /// 阅览室开闭馆设置
    /// </summary>
    public class JM_RoomOpenTimeSet
    {
        int openBeforeTimeLength;
        /// <summary>
        /// 开馆预处理时长（分钟）
        /// </summary>
        public int OpenBeforeTimeLength
        {
            get { return openBeforeTimeLength; }
            set { openBeforeTimeLength = value; }
        }
        int closeBeforeTimeLength;
        /// <summary>
        /// 闭馆预处理时长（分钟）
        /// </summary>
        public int CloseBeforeTimeLength
        {
            get { return closeBeforeTimeLength; }
            set { closeBeforeTimeLength = value; }
        }
        string defaultOpenTime;
        /// <summary>
        /// 默认开馆时间（时:分）
        /// </summary>
        public string DefaultOpenTime
        {
            get { return defaultOpenTime; }
            set { defaultOpenTime = value; }
        }
        string defaultCloseTime;
        /// <summary>
        /// 默认闭馆时间
        /// </summary>
        public string DefaultCloseTime
        {
            get { return defaultCloseTime; }
            set { defaultCloseTime = value; }
        }

        bool isUsed24HourModel;
        /// <summary>
        /// 是否启用24小时不间断模式
        /// </summary>
        public bool IsUsed24HourModel
        {
            get { return isUsed24HourModel; }
            set { isUsed24HourModel = value; }
        }
        bool isUsedAdvancedModel;
        /// <summary>
        /// 是否启用高级设置
        /// </summary>
        public bool IsUsedAdvancedModel
        {
            get { return isUsedAdvancedModel; }
            set { isUsedAdvancedModel = value; }
        }
        List<JM_RoomOpenClosePlan> advancedOpenClosePlan;
        /// <summary>
        /// 高级开闭馆计划
        /// </summary>
        public List<JM_RoomOpenClosePlan> AdvancedOpenClosePlan
        {
            get { return advancedOpenClosePlan; }
            set { advancedOpenClosePlan = value; }
        }
    }
    /// <summary>
    /// 阅览室开闭馆计划
    /// </summary>
    public class JM_RoomOpenClosePlan
    {
        string day;
        /// <summary>
        /// 日期（星期几）
        /// </summary>
        public string Day
        {
            get { return day; }
            set { day = value; }
        }
        List<JM_TimeSpan> openCloseTimeSpan;
        /// <summary>
        /// 开闭馆时间段（时:分）
        /// </summary>
        public List<JM_TimeSpan> OpenCloseTimeSpan
        {
            get { return openCloseTimeSpan; }
            set { openCloseTimeSpan = value; }
        }
        bool isUsed;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }
    }
    #endregion

    #region 等待座位设置
    /// <summary>
    /// 座位等待设置
    /// </summary>
    public class JM_SeatWaitSet
    {
        bool isUsed;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }
        int operatingTimeInterval;
        /// <summary>
        /// 操作时间间隔（分钟）
        /// </summary>
        public int OperatingTimeInterval
        {
            get { return operatingTimeInterval; }
            set { operatingTimeInterval = value; }
        }
    }
    #endregion

    /// <summary>
    /// 黑名单及违规记录设置
    /// </summary>
    public class JM_ReadingRoomBlacklistSet
    {
        bool isLimitBlacklist;
        /// <summary>
        /// 是否限制黑名单用户进入
        /// </summary>
        public bool IsLimitBlacklist
        {
            get { return isLimitBlacklist; }
            set { isLimitBlacklist = value; }
        }
        bool isViolation;
        /// <summary>
        /// 是否记录违规
        /// </summary>
        public bool IsViolation
        {
            get { return isViolation; }
            set { isViolation = value; }
        }
        JM_BlacklistSet setting = new JM_BlacklistSet();
        /// <summary>
        /// 黑名单设置
        /// </summary>
        public JM_BlacklistSet Setting
        {
            get { return setting; }
            set { setting = value; }
        }
    }

    #region 在座时长模式设置
    /// <summary>
    /// 座位限时模式设置
    /// </summary>
    public class JM_SeatUsedTimeLimitSet
    {
        bool isUsed;
        /// <summary>
        /// 是否启用续时功能
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }
        int seatHoldTimeLength;
        /// <summary>
        /// 座位保留时间（分钟）
        /// </summary>
        public int SeatHoldTimeLength
        {
            get { return seatHoldTimeLength; }
            set { seatHoldTimeLength = value; }
        }
        string overTimeHandle;
        /// <summary>
        /// 座位超时处理
        /// </summary>
        public string OverTimeHandle
        {
            get { return overTimeHandle; }
            set { overTimeHandle = value; }
        }
        string holdTimeModel;
        /// <summary>
        /// 座位限时模式固定模式：Fixed，自己续时：Free
        /// </summary>
        public string HoldTimeModel
        {
            get { return holdTimeModel; }
            set { holdTimeModel = value; }
        }
        List<string> fixedTimes;
        /// <summary>
        /// 固定时间时间段（时:分）
        /// </summary>
        public List<string> FixedTimes
        {
            get { return fixedTimes; }
            set { fixedTimes = value; }
        }


        bool isUsedContinueTime;
        /// <summary>
        /// 是否可以续时
        /// </summary>
        public bool IsUsedContinueTime
        {
            get { return isUsedContinueTime; }
            set { isUsedContinueTime = value; }
        }
        int delayTimeLength;
        /// <summary>
        /// 续时时长（分钟）
        /// </summary>
        public int DelayTimeLength
        {
            get { return delayTimeLength; }
            set { delayTimeLength = value; }
        }
        int delayLastTimeLength;
        /// <summary>
        /// 最后剩余可以续时时长（分钟）
        /// </summary>
        public int DelayLastTimeLength
        {
            get { return delayLastTimeLength; }
            set { delayLastTimeLength = value; }
        }
        int canContinuedTimes;
        /// <summary>
        /// 可以续时次数（0为不限制次数）
        /// </summary>
        public int CanContinuedTimes
        {
            get { return canContinuedTimes; }
            set { canContinuedTimes = value; }
        }
        bool canNotContinuedWithBookNetFixed;
        /// <summary>
        /// 下一个时段被预约后不能续时（需开启固定时段模式以及多时段预约功能）
        /// </summary>
        public bool CanNotContinuedWithBookNetFixed
        {
            get { return canNotContinuedWithBookNetFixed; }
            set { canNotContinuedWithBookNetFixed = value; }
        }
    }
    #endregion

    #region 座位预约设置
    /// <summary>
    /// 座位预约设置
    /// </summary>
    public class JM_SeatBespeakSet
    {
        bool isUsedSeatBespeak;
        /// <summary>
        /// 是否启用预约
        /// </summary>
        public bool IsUsedSeatBespeak
        {
            get { return isUsedSeatBespeak; }
            set { isUsedSeatBespeak = value; }
        }
        bool isUsedNowDaySeatBespeak;
        /// <summary>
        /// 是否开启当天预约
        /// </summary>
        public bool IsUsedNowDaySeatBespeak
        {
            get { return isUsedNowDaySeatBespeak; }
            set { isUsedNowDaySeatBespeak = value; }
        }
        bool isUsedMultiSpanBespeak;
        /// <summary>
        /// 自定义预约时间
        /// </summary>
        public bool IsUsedMultiSpanBespeak
        {
            get { return isUsedMultiSpanBespeak; }
            set { isUsedMultiSpanBespeak = value; }
        }

        JM_TimeSpan signinTimeSpan;
        /// <summary>
        /// 预约签到时间段（分钟）
        /// </summary>
        public JM_TimeSpan SigninTimeSpan
        {
            get { return signinTimeSpan; }
            set { signinTimeSpan = value; }
        }
        JM_TimeSpan canBespeakTimeSpan;
        /// <summary>
        /// 可以进行预约的时间段（时:分）
        /// </summary>
        public JM_TimeSpan CanBespeakTimeSpan
        {
            get { return canBespeakTimeSpan; }
            set { canBespeakTimeSpan = value; }
        }
        List<JM_TimeSpan> canNotBespeakDate;
        /// <summary>
        /// 不可预约的日期（月/日）
        /// </summary>
        public List<JM_TimeSpan> CanNotBespeakDate
        {
            get { return canNotBespeakDate; }
            set { canNotBespeakDate = value; }
        }
        int nowDaySeatKeepTimeLength;
        /// <summary>
        /// 计时预约（当天）座位保留时长（分钟）
        /// </summary>
        public int NowDaySeatKeepTimeLength
        {
            get { return nowDaySeatKeepTimeLength; }
            set { nowDaySeatKeepTimeLength = value; }
        }

        bool isSpecifiedTime;
        /// <summary>
        /// 是否制定时间段预约（多时段预约）
        /// </summary>
        public bool IsSpecifiedTime
        {
            get { return isSpecifiedTime; }
            set { isSpecifiedTime = value; }
        }
        List<string> specifiedTimeList;
        /// <summary>
        /// 指定的可预约时间段（多时段预约）
        /// </summary>
        public List<string> SpecifiedTimeList
        {
            get { return specifiedTimeList; }
            set { specifiedTimeList = value; }
        }

        bool isCanSelectBespeakSeat;
        /// <summary>
        /// 是否可以选择被预约的座位（当天预约，多时段预约）
        /// </summary>
        public bool IsCanSelectBespeakSeat
        {
            get { return isCanSelectBespeakSeat; }
            set { isCanSelectBespeakSeat = value; }
        }
        int canBespeakSeatCount;
        /// <summary>
        /// 可以预约座位的次数（次/每天）
        /// </summary>
        public int CanBespeakSeatCount
        {
            get { return canBespeakSeatCount; }
            set { canBespeakSeatCount = value; }
        }
        bool isCanBespeakWithOnSeat;
        /// <summary>
        /// 是否可以在拥有座位的情况下预约当天座位（当天预约）
        /// </summary>
        public bool IsCanBespeakWithOnSeat
        {
            get { return isCanBespeakWithOnSeat; }
            set { isCanBespeakWithOnSeat = value; }
        }

    }

    #endregion

    /// <summary>
    /// 限制读者类型设置
    /// </summary>
    public class JM_LimitReaderEnterSet
    {
        bool _Used = false;
        bool _CanEnter = false;
        string _ReaderType = "";
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }
        /// <summary>
        /// 是否可以进入
        /// </summary>
        public bool CanEnter
        {
            get { return _CanEnter; }
            set { _CanEnter = value; }
        }
        /// <summary>
        /// 限制的类型，多个类型用分号隔开
        /// </summary>
        public string ReaderTypes
        {
            get { return _ReaderType; }
            set { _ReaderType = value; }
        }
    }
    /// <summary>
    /// 选座次数限制
    /// </summary>
    public class JM_POSRestrict
    {
        private bool isUsed = true;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }
        private int minutes = 10;
        /// <summary>
        /// 分钟数
        /// </summary>
        public int Minutes
        {
            get { return minutes; }
            set { minutes = value; }
        }
        private int times = 3;
        /// <summary>
        /// 限制次数
        /// </summary>
        public int Times
        {
            get { return times; }
            set { times = value; }
        }
    }
    /// <summary>
    /// 时间段
    /// </summary>
    public class JM_TimeSpan
    {
        string startTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        string endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
    }
}
