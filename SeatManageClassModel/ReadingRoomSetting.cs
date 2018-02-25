using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;
namespace SeatManage.ClassModel
{
    [Serializable]
    /// <summary>
    /// 阅览室设置
    /// </summary>
    public class ReadingRoomSetting
    {
        private SeatChooseMethodSet _SeatChooseMethod = new SeatChooseMethodSet();
        private SeatHoldTimeSet _SeatHoldTime = new SeatHoldTimeSet();
        private int _SeatNumAmount = 3;
        private RoomOpenTimeSet _RoomOpenSet = new RoomOpenTimeSet();
        private NoManagementSet _NoManagement = new NoManagementSet();
        private bool _UsedBlacklistLimit = false;
        private ReadingRoomBlacklistSetting _BlackListSetting = new ReadingRoomBlacklistSetting();
        private bool _IsRecordViolate = false;
        private SeatUsedTimeLimitSet _SeatUsedTimeLimit = new SeatUsedTimeLimitSet();
        private SeatBespeakSet _SeatBespeak = new SeatBespeakSet();
        private LimitReaderEnterSet _LimitReaderEnter = new LimitReaderEnterSet();
        private AdminSetShortLeave _AdminiSetShortLeave = new AdminSetShortLeave();
        private POSRestrict _PosTimes = new POSRestrict();

        public static double GetSeatHoldTime(SeatHoldTimeSet set, DateTime time)
        {
            if (set.UsedAdvancedSet)
            {
                foreach (SeatHoldTimeOption option in set.AdvancedSeatHoldTime)
                {
                    if (option.Used)
                    { //判断指定的时间是否在开始时间和结束时间中间
                        DateTime begintime = DateTime.Parse(time.ToShortDateString() + " " + option.UsedTime.BeginTime);
                        DateTime endtime = DateTime.Parse(time.ToShortDateString() + " " + option.UsedTime.EndTime);
                        if (DateTimeOperate.DateAccord(begintime, endtime, time))
                        {
                            return option.HoldTimeLength;
                        }
                    }
                }
                //遍历结束没有返回，则返回默认保留时长
                return set.DefaultHoldTimeLength;
            }
            else
            {
                //没有启用阅览室设置，则返回默认保留时长
                return set.DefaultHoldTimeLength;
            }
        }
        /// <summary>
        /// 刷卡次数设置
        /// </summary>
        public POSRestrict PosTimes
        {
            get { return _PosTimes; }
            set { _PosTimes = value; }
        }

        /// <summary>
        /// 阅览室设置
        /// </summary>
        public ReadingRoomSetting()
        { }
        /// <summary>
        /// 阅览室设置
        /// </summary>
        /// <param name="xmlRoomSet">XML结构的阅览室设置</param>
        public ReadingRoomSetting(string xmlRoomSet)
        {
            try
            {
                ConvertRoomSetting(xmlRoomSet);
            }
            catch
            {
                throw;
            }
        }
        public static ReadingRoomStatus ReadingRoomOpenState(RoomOpenTimeSet openSeat, DateTime time)
        {
            ReadingRoomStatus openState = ReadingRoomStatus.Close;

            if (openSeat.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = time.DayOfWeek;
                try
                {
                    RoomOpenPlanSet plan = openSeat.RoomOpenPlan[day];

                    if (plan.Used)
                    {
                        foreach (TimeSpace t in plan.OpenTime)
                        {
                            openState = calcRoomState(t.BeginTime, t.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                            switch (openState)
                            { //当前时间阅览室状态为非关闭状态，直接返回结果。否则继续判断
                                case ReadingRoomStatus.BeforeClose:
                                case ReadingRoomStatus.BeforeOpen:
                                case ReadingRoomStatus.Open:
                                    return openState;
                            }
                        }
                        //遍历结束没有返回，则返回最后一次计算的结果
                        return openState;
                    }
                    else
                    {
                        //否则当天没启用高级设置，返回默认开馆状态
                        openState = calcRoomState(openSeat.DefaultOpenTime.BeginTime, openSeat.DefaultOpenTime.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                        return openState;
                    }
                }
                catch
                {
                    //当天没有高级设置，则返回默认开馆状态。
                    openState = calcRoomState(openSeat.DefaultOpenTime.BeginTime, openSeat.DefaultOpenTime.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                    return openState;
                }
            }
            else
            {
                //没有开启高级设置，则返回默认开馆状态。
                openState = calcRoomState(openSeat.DefaultOpenTime.BeginTime, openSeat.DefaultOpenTime.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                return openState;
            }

        }

        #region 属性
        /// <summary>
        /// 是否启用黑名单限制
        /// </summary>
        public bool UsedBlacklistLimit
        {
            get { return _UsedBlacklistLimit; }
            set { _UsedBlacklistLimit = value; }
        }
        /// <summary>
        /// 是否记录违规
        /// </summary>
        public bool IsRecordViolate
        {
            get { return _IsRecordViolate; }
            set { _IsRecordViolate = value; }
        }
        /// <summary>
        /// 黑名单设置
        /// </summary>
        public ReadingRoomBlacklistSetting BlackListSetting
        {
            get { return _BlackListSetting; }
            set { _BlackListSetting = value; }
        }
        /// <summary>
        /// 限制读者进入
        /// </summary>
        public LimitReaderEnterSet LimitReaderEnter
        {
            get { return _LimitReaderEnter; }
            set { _LimitReaderEnter = value; }
        }
        /// <summary>
        /// 座位预约设置
        /// </summary>
        public SeatBespeakSet SeatBespeak
        {
            get { return _SeatBespeak; }
            set { _SeatBespeak = value; }
        }
        /// <summary>
        /// 座位使用时长设置
        /// </summary>
        public SeatUsedTimeLimitSet SeatUsedTimeLimit
        {
            get { return _SeatUsedTimeLimit; }
            set { _SeatUsedTimeLimit = value; }
        }

        /// <summary>
        /// 无人管理模式设置
        /// </summary>
        public NoManagementSet NoManagement
        {
            get { return _NoManagement; }
            set { _NoManagement = value; }
        }
        /// <summary>
        /// 开馆设置
        /// </summary>
        public RoomOpenTimeSet RoomOpenSet
        {
            get { return _RoomOpenSet; }
            set { _RoomOpenSet = value; }
        }
        /// <summary>
        /// 选座方式设置
        /// </summary>
        public SeatChooseMethodSet SeatChooseMethod
        {
            get { return _SeatChooseMethod; }
            set { _SeatChooseMethod = value; }
        }
        /// <summary>
        /// 座位保留时长设置
        /// </summary>
        public SeatHoldTimeSet SeatHoldTime
        {
            get { return _SeatHoldTime; }
            set { _SeatHoldTime = value; }
        }
        /// <summary>
        /// 座位号显示位数
        /// </summary>
        public int SeatNumAmount
        {
            get { return _SeatNumAmount; }
            set { _SeatNumAmount = value; }
        }
        /// <summary>
        /// 管理员暂离时长设置
        /// </summary>
        public AdminSetShortLeave AdminShortLeave
        {
            get { return _AdminiSetShortLeave; }
            set { _AdminiSetShortLeave = value; }
        }
        #endregion

        #region 方法
        public ReadingRoomSetting ConvertRoomSetting(string xmlRoomSet)
        {
            XmlDocument xmlDocSet = new XmlDocument();
            xmlDocSet.LoadXml(xmlRoomSet);
            //黑名单
            //XmlNode node = xmlDocSet.SelectSingleNode("//rootNode/blacklist");
            //this.Blacklist = CreateBlacklistSet(node);
            //限制选择该阅览室的读者类型
            XmlNode node = xmlDocSet.SelectSingleNode("//rootNode/limitReaderEnter");
            if (node == null)
            {
                this.LimitReaderEnter = new LimitReaderEnterSet();
            }
            else
            {
                this.LimitReaderEnter = CreateLimitReaderEnter(node);
            }
            //无人值守模式设置
            node = xmlDocSet.SelectSingleNode("//rootNode/noManagement");
            if (node == null)
            {
                this.NoManagement = new NoManagementSet();
            }
            else
            {
                this.NoManagement = CreateNoManagementSet(node);
            }
            //开馆设置
            node = xmlDocSet.SelectSingleNode("//rootNode/roomOpenSet");
            if (node == null)
            {
                this.RoomOpenSet = new RoomOpenTimeSet();
            }
            else
            {
                this.RoomOpenSet = CreateRoomOpenSet(node);
            }
            //是否限制黑名单进入
            node = xmlDocSet.SelectSingleNode("//rootNode/usedBlacklistLimit");
            if (node == null)
            {
                this._UsedBlacklistLimit = false;
            }
            else
            {
                this._UsedBlacklistLimit = CreateIsUsedBlacklistLimit(node);
            }
            //是否限制黑名单进入
            node = xmlDocSet.SelectSingleNode("//rootNode/isRecordViolate");
            if (node == null)
            {
                this._IsRecordViolate = false;
            }
            else
            {
                this._IsRecordViolate = CreateIsRecordViolate(node);
            }
            node = xmlDocSet.SelectSingleNode("//rootNode/blacklist");
            if (node == null)
            {
                this._BlackListSetting = new ReadingRoomBlacklistSetting();
            }
            else
            {
                this._BlackListSetting = CreateBlacklist(node);
            }
            //座位预约设置
            node = xmlDocSet.SelectSingleNode("//rootNode/seatBespeakSet");
            if (node == null)
            {
                this.SeatBespeak = new SeatBespeakSet();
            }
            else
            {
                this.SeatBespeak = CreateSeatBespeak(node);
            }
            //选座方式设置
            node = xmlDocSet.SelectSingleNode("//rootNode/seatChooseMethod");
            if (node == null)
            {
                this.SeatChooseMethod = new SeatChooseMethodSet();
            }
            else
            {
                this.SeatChooseMethod = CreateSeatChooseMethod(node);
            }
            //暂离座位保留时长设置
            node = xmlDocSet.SelectSingleNode("//rootNode/seatHoldTime");
            if (node == null)
            {
                this.SeatHoldTime = new SeatHoldTimeSet();
            }
            else
            {
                this.SeatHoldTime = CreateSeatHoldTime(node);
            }
            //座位号显示位数
            node = xmlDocSet.SelectSingleNode("//rootNode/seatNumAmount");
            if (node == null)
            {
                this.SeatNumAmount = 3;
            }
            else
            {
                this.SeatNumAmount = CreateSeatNumAmount(node);
            }
            //座位使用时长限制
            node = xmlDocSet.SelectSingleNode("//rootNode/seatUsedTimeLimit");
            if (node == null)
            {
                this.SeatUsedTimeLimit = new SeatUsedTimeLimitSet();
            }
            else
            {
                this.SeatUsedTimeLimit = CreateSeatUsedTimeLimit(node);
            }
            node = xmlDocSet.SelectSingleNode("//rootNode/adminSetShortLeave");
            if (node == null)
            {
                this.AdminShortLeave = new AdminSetShortLeave();
            }
            else
            {
                this.AdminShortLeave = CreateAdminSetShortLeave(node);
            }
            node = xmlDocSet.SelectSingleNode("//rootNode/POSRestrict");
            this.PosTimes = POSRestrict.ToObject(node);
            return this;
        }

        public override string ToString()
        {
            return ConvertToXml(this);
        }
        #endregion

        #region 私有方法
        static XmlDocument doc = null;
        private static string ConvertToXml(ReadingRoomSetting roomSetting)
        {
            doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("rootNode");//创建根节点
            XmlElement element = null;
            //选座方式 
            element = CreateSeatChooseMethod(roomSetting.SeatChooseMethod);
            root.AppendChild(element);
            //暂离座位保留时长
            element = CreateSeatHoldTime(roomSetting.SeatHoldTime);
            root.AppendChild(element);
            //座位号显示位数
            element = CreateSeatNumAmount(roomSetting.SeatNumAmount);
            root.AppendChild(element);
            //开馆设置
            element = CreateRoomOpenSet(roomSetting.RoomOpenSet);
            root.AppendChild(element);
            //无人职守模式
            element = CreateNoManagementSet(roomSetting.NoManagement);
            root.AppendChild(element);
            //黑名单设置
            element = CreateIsUsedBlacklistLimit(roomSetting.UsedBlacklistLimit);
            root.AppendChild(element);
            //启用违规
            element = CreateIsRecordViolate(roomSetting._IsRecordViolate);
            root.AppendChild(element);
            //黑名单设置
            element = CreateBlacklist(roomSetting._BlackListSetting);
            root.AppendChild(element);
            //座位使用时长限制节点
            element = CreateSeatUsedTimeLimit(roomSetting.SeatUsedTimeLimit);
            root.AppendChild(element);
            //座位预约设置
            element = CreateSeatBespeak(roomSetting.SeatBespeak);
            root.AppendChild(element);
            //限制读者进入设置
            element = CreateLimitReaderEnter(roomSetting.LimitReaderEnter);
            root.AppendChild(element);
            //管理员设置暂离时长设置
            element = CreateAdminSetShortLeave(roomSetting.AdminShortLeave);
            root.AppendChild(element);
            //选座次数限制
            element = POSRestrict.ToXmlNode(roomSetting.PosTimes, doc);
            root.AppendChild(element);
            doc.AppendChild(root);
            return doc.OuterXml;
        }

        /// <summary>
        /// 构造座位预约节点
        /// </summary>
        /// <param name="bespeakSet"></param>
        /// <returns></returns>
        private static XmlElement CreateSeatBespeak(SeatBespeakSet bespeakSet)
        {
            XmlElement element = doc.CreateElement("seatBespeakSet");
            element.SetAttribute("bespeakBeforeDays", bespeakSet.BespeakBeforeDays.ToString());
            element.SetAttribute("BespeakSeatCount", bespeakSet.BespeakSeatCount.ToString());
            element.SetAttribute("allowDelayTime", ConfigConvert.ConvertToString(bespeakSet.AllowDelayTime));
            element.SetAttribute("allowShortLeave", ConfigConvert.ConvertToString(bespeakSet.AllowShortLeave));
            element.SetAttribute("allowLeave", ConfigConvert.ConvertToString(bespeakSet.AllowLeave));
            element.SetAttribute("NowDayBespeak", ConfigConvert.ConvertToString(bespeakSet.NowDayBespeak));
            element.SetAttribute("SeatKeepTime", bespeakSet.SeatKeepTime.ToString());
            element.SetAttribute("confirmBeforeTime", bespeakSet.ConfirmTime.BeginTime);
            element.SetAttribute("confirmEndTime", bespeakSet.ConfirmTime.EndTime);
            element.SetAttribute("used", ConfigConvert.ConvertToString(bespeakSet.Used));
            element.SetAttribute("BespeatWithOnSeat", ConfigConvert.ConvertToString(bespeakSet.BespeatWithOnSeat));
            element.SetAttribute("canBespeakBeginTime", bespeakSet.CanBespeatTimeSpace.BeginTime);
            element.SetAttribute("canBespeakEndTime", bespeakSet.CanBespeatTimeSpace.EndTime);
            element.SetAttribute("SpecifiedBespeak", ConfigConvert.ConvertToString(bespeakSet.SpecifiedBespeak));
            element.SetAttribute("SelectBespeakSeat", ConfigConvert.ConvertToString(bespeakSet.SelectBespeakSeat));
            element.SetAttribute("SpecifiedTime", ConfigConvert.ConvertToString(bespeakSet.SpecifiedTime));
            element.SetAttribute("CanBookMultiSpan", ConfigConvert.ConvertToString(bespeakSet.CanBookMultiSpan));
            element.SetAttribute("CanBookUsingSeat", ConfigConvert.ConvertToString(bespeakSet.CanBookUsingSeat));
            XmlElement child1 = doc.CreateElement("bespeakArea");
            child1.SetAttribute("bespeakType", ((int)bespeakSet.BespeakArea.BespeakType).ToString());
            child1.SetAttribute("scale", bespeakSet.BespeakArea.Scale.ToString());
            foreach (TimeSpace timespace in bespeakSet.NoBespeakDates)
            {
                XmlElement option = doc.CreateElement("noBespeakDates");
                option.SetAttribute("beginDate", timespace.BeginTime);
                option.SetAttribute("endDate", timespace.EndTime);
                child1.AppendChild(option);
            }
            element.AppendChild(child1);

            XmlElement child2 = doc.CreateElement("SpecifiedTimeList");
            foreach (DateTime item in bespeakSet.SpecifiedTimeList)
            {
                XmlElement option = doc.CreateElement("TimeItem");
                option.SetAttribute("Time", item.ToShortTimeString());
                child2.AppendChild(option);
            }
            element.AppendChild(child2);


            return element;
        }
        /// <summary>
        /// 解析座位预约设置
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static SeatBespeakSet CreateSeatBespeak(XmlNode node)
        {
            SeatBespeakSet seatBespeakset = new SeatBespeakSet();
            seatBespeakset.BespeakArea.BespeakType = (BespeakAreaType)(int.Parse(node.ChildNodes[0].Attributes["bespeakType"].Value));
            seatBespeakset.BespeakArea.Scale = double.Parse(node.ChildNodes[0].Attributes["scale"].Value);
            seatBespeakset.BespeakBeforeDays = int.Parse(node.Attributes["bespeakBeforeDays"].Value);
            seatBespeakset.ConfirmTime.BeginTime = node.Attributes["confirmBeforeTime"].Value;
            seatBespeakset.ConfirmTime.EndTime = node.Attributes["confirmEndTime"].Value;
            seatBespeakset.CanBespeatTimeSpace.BeginTime = node.Attributes["canBespeakBeginTime"].Value;
            seatBespeakset.CanBespeatTimeSpace.EndTime = node.Attributes["canBespeakEndTime"].Value;
            if (node.Attributes["allowDelayTime"] != null)
            {
                seatBespeakset.AllowDelayTime = ConfigConvert.ConvertToBool(node.Attributes["allowDelayTime"].Value);
            }
            if (node.Attributes["allowShortLeave"] != null)
            {
                seatBespeakset.AllowShortLeave = ConfigConvert.ConvertToBool(node.Attributes["allowShortLeave"].Value);
            }
            if (node.Attributes["allowLeave"] != null)
            {
                seatBespeakset.AllowLeave = ConfigConvert.ConvertToBool(node.Attributes["allowLeave"].Value);
            }
            if (node.Attributes["NowDayBespeak"] != null)
            {
                seatBespeakset.NowDayBespeak = ConfigConvert.ConvertToBool(node.Attributes["NowDayBespeak"].Value);
            }
            if (node.Attributes["SeatKeepTime"] != null)
            {
                seatBespeakset.SeatKeepTime = double.Parse(node.Attributes["SeatKeepTime"].Value);
            }
            if (node.Attributes["SpecifiedBespeak"] != null)
            {
                seatBespeakset.SpecifiedBespeak = ConfigConvert.ConvertToBool(node.Attributes["SpecifiedBespeak"].Value);
            }
            if (node.Attributes["BespeatWithOnSeat"] != null)
            {
                seatBespeakset.BespeatWithOnSeat = ConfigConvert.ConvertToBool(node.Attributes["BespeatWithOnSeat"].Value);
            }
            if (node.Attributes["SpecifiedTime"] != null)
            {
                seatBespeakset.SpecifiedTime = ConfigConvert.ConvertToBool(node.Attributes["SpecifiedTime"].Value);
            }
            if (node.Attributes["SelectBespeakSeat"] != null)
            {
                seatBespeakset.SelectBespeakSeat = ConfigConvert.ConvertToBool(node.Attributes["SelectBespeakSeat"].Value);
            }
            if (node.Attributes["CanBookMultiSpan"] != null)
            {
                seatBespeakset.CanBookMultiSpan = ConfigConvert.ConvertToBool(node.Attributes["CanBookMultiSpan"].Value);
            }
            if (node.Attributes["CanBookUsingSeat"] != null)
            {
                seatBespeakset.CanBookUsingSeat = ConfigConvert.ConvertToBool(node.Attributes["CanBookUsingSeat"].Value);
            }
            if (node.Attributes["BespeakSeatCount"] != null)
            {
                seatBespeakset.BespeakSeatCount = int.Parse(node.Attributes["BespeakSeatCount"].Value);
            }

            XmlNodeList nodes = node.ChildNodes[0].ChildNodes;//.SelectNodes("//seatBespeakSet/noBespeakDates");
            foreach (XmlNode element in nodes)
            {
                seatBespeakset.NoBespeakDates.Add(new TimeSpace(element.Attributes["beginDate"].Value, element.Attributes["endDate"].Value));
            }
            if (node.ChildNodes.Count > 1)
            {
                seatBespeakset.SpecifiedTimeList.Clear();
                nodes = node.ChildNodes[1].ChildNodes;
                foreach (XmlNode element in nodes)
                {
                    seatBespeakset.SpecifiedTimeList.Add(DateTime.Parse(element.Attributes["Time"].Value));
                }
            }
            seatBespeakset.Used = ConfigConvert.ConvertToBool(node.Attributes["used"].Value);
            return seatBespeakset;
        }
        /// <summary>
        /// 构造座位使用时长限制节点
        /// </summary>
        /// <param name="SeatUsedTimeLimit"></param>
        /// <returns></returns>
        private static XmlElement CreateSeatUsedTimeLimit(SeatUsedTimeLimitSet usedTimeLimit)
        {
            XmlElement element = doc.CreateElement("seatUsedTimeLimit");
            element.SetAttribute("used", ConfigConvert.ConvertToString(usedTimeLimit.Used));
            element.SetAttribute("usedTimeLength", usedTimeLimit.UsedTimeLength.ToString());
            element.SetAttribute("overTimeHandle", ((int)usedTimeLimit.OverTimeHandle).ToString());
            element.SetAttribute("IsCanContinuedTimes", ConfigConvert.ConvertToString(usedTimeLimit.IsCanContinuedTime));
            element.SetAttribute("delayTimeLength", usedTimeLimit.DelayTimeLength.ToString());
            element.SetAttribute("continuedTimes", usedTimeLimit.ContinuedTimes.ToString());
            element.SetAttribute("CanDelayTime", usedTimeLimit.CanDelayTime.ToString());
            element.SetAttribute("CanNotContinuedWithBookNetFixed", ConfigConvert.ConvertToString(usedTimeLimit.CanNotContinuedWithBookNetFixed));
            element.SetAttribute("Mode", usedTimeLimit.Mode);
            XmlElement child1 = doc.CreateElement("FixedTimes");
            foreach (DateTime item in usedTimeLimit.FixedTimes)
            {
                XmlElement option = doc.CreateElement("Plan");
                option.SetAttribute("Time", item.ToLongTimeString());
                child1.AppendChild(option);
            }
            element.AppendChild(child1);
            return element;
        }
        /// <summary>
        /// 解析座位使用时长限制节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static SeatUsedTimeLimitSet CreateSeatUsedTimeLimit(XmlNode node)
        {
            SeatUsedTimeLimitSet set = new SeatUsedTimeLimitSet();
            set.ContinuedTimes = int.Parse(node.Attributes["continuedTimes"].Value);
            set.DelayTimeLength = int.Parse(node.Attributes["delayTimeLength"].Value);
            set.OverTimeHandle = (EnterOutLogType)int.Parse(node.Attributes["overTimeHandle"].Value);
            set.IsCanContinuedTime = ConfigConvert.ConvertToBool(node.Attributes["IsCanContinuedTimes"].Value);
            set.Used = ConfigConvert.ConvertToBool(node.Attributes["used"].Value);
            set.UsedTimeLength = int.Parse(node.Attributes["usedTimeLength"].Value);
            set.CanDelayTime = int.Parse(node.Attributes["CanDelayTime"].Value);
            if (node.Attributes["CanNotContinuedWithBookNetFixed"] != null)
            {
                set.CanNotContinuedWithBookNetFixed = ConfigConvert.ConvertToBool(node.Attributes["CanNotContinuedWithBookNetFixed"].Value);
            }
            if (node.Attributes["Mode"] != null)
            {
                set.Mode = node.Attributes["Mode"].Value;
            }
            else
            {
                set.Mode = "Free";
            }
            if (node.ChildNodes.Count > 0)
            {
                foreach (XmlNode element in node.ChildNodes[0].ChildNodes)
                {
                    set.FixedTimes.Add(DateTime.Parse(element.Attributes["Time"].Value));
                }
            }
            return set;
        }
        /// <summary>
        /// 无人管理模式节点
        /// </summary>
        /// <param name="noManagement"></param>
        /// <returns></returns>
        private static XmlElement CreateNoManagementSet(NoManagementSet noManagement)
        {
            XmlElement element = doc.CreateElement("noManagement");
            element.SetAttribute("used", ConfigConvert.ConvertToString(noManagement.Used));
            element.SetAttribute("operatingInterval", noManagement.OperatingInterval.ToString());
            return element;
        }
        /// <summary>
        /// 解析无人管理模式设置
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static NoManagementSet CreateNoManagementSet(XmlNode node)
        {
            NoManagementSet set = new NoManagementSet();
            set.OperatingInterval = int.Parse(node.Attributes["operatingInterval"].Value);
            set.Used = ConfigConvert.ConvertToBool(node.Attributes["used"].Value);
            return set;
        }

        /// <summary>
        /// 开馆设置节点
        /// </summary>
        /// <param name="roomOpenSet"></param>
        /// <returns></returns>
        private static XmlElement CreateRoomOpenSet(RoomOpenTimeSet roomOpenSet)
        {
            //TODO:开馆计划周期
            XmlElement element = doc.CreateElement("roomOpenSet");
            element.SetAttribute("beginTime", roomOpenSet.DefaultOpenTime.BeginTime);
            element.SetAttribute("endTime", roomOpenSet.DefaultOpenTime.EndTime);
            element.SetAttribute("openBeforeTimeLength", roomOpenSet.OpenBeforeTimeLength.ToString());
            element.SetAttribute("closeBeforeTimeLength", roomOpenSet.CloseBeforeTimeLength.ToString());
            element.SetAttribute("usedAdvancedSet", ConfigConvert.ConvertToString(roomOpenSet.UsedAdvancedSet));
            element.SetAttribute("UninterruptibleModel", ConfigConvert.ConvertToString(roomOpenSet.UninterruptibleModel));
            foreach (DayOfWeek day in roomOpenSet.RoomOpenPlan.Keys)
            {
                XmlElement child1 = doc.CreateElement("roomOpenPlan");
                child1.SetAttribute("used", ConfigConvert.ConvertToString(roomOpenSet.RoomOpenPlan[day].Used));
                child1.SetAttribute("dayOfWeek", ((int)day).ToString());
                foreach (TimeSpace openTimes in roomOpenSet.RoomOpenPlan[day].OpenTime)
                {
                    XmlElement child2 = doc.CreateElement("opens");
                    child2.SetAttribute("beginTime", openTimes.BeginTime);
                    child2.SetAttribute("endTime", openTimes.EndTime);
                    child1.AppendChild(child2);
                }
                element.AppendChild(child1);
            }

            return element;
        }
        /// <summary>
        /// 解析开馆设置节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static RoomOpenTimeSet CreateRoomOpenSet(XmlNode node)
        {
            RoomOpenTimeSet set = new RoomOpenTimeSet();
            set.CloseBeforeTimeLength = int.Parse(node.Attributes["closeBeforeTimeLength"].Value);
            set.DefaultOpenTime.BeginTime = node.Attributes["beginTime"].Value;
            set.DefaultOpenTime.EndTime = node.Attributes["endTime"].Value;
            set.OpenBeforeTimeLength = int.Parse(node.Attributes["openBeforeTimeLength"].Value);
            set.UsedAdvancedSet = ConfigConvert.ConvertToBool(node.Attributes["usedAdvancedSet"].Value);
            if (node.Attributes["UninterruptibleModel"] != null)
            {
                set.UninterruptibleModel = ConfigConvert.ConvertToBool(node.Attributes["UninterruptibleModel"].Value);
            }
            XmlNodeList childs = node.ChildNodes;// SelectNodes("//roomOpenSet/roomOpenPlan");
            foreach (XmlNode element in childs)
            {
                DayOfWeek day = (DayOfWeek)int.Parse(element.Attributes["dayOfWeek"].Value);
                RoomOpenPlanSet planSet = new RoomOpenPlanSet();
                planSet.Used = ConfigConvert.ConvertToBool(element.Attributes["used"].Value);
                planSet.Day = day;
                XmlNodeList childs1 = element.ChildNodes;//.SelectNodes("//opens");
                foreach (XmlNode element1 in childs1)
                {
                    planSet.OpenTime.Add(new TimeSpace(element1.Attributes["beginTime"].Value, element1.Attributes["endTime"].Value));
                }
                set.RoomOpenPlan.Add(day, planSet);
            }
            return set;
        }

        /// <summary>
        /// 构造选座方式节点
        /// </summary>
        /// <param name="seatChooseMethod"></param>
        /// <returns></returns>
        private static XmlElement CreateSeatChooseMethod(SeatChooseMethodSet seatChooseMethod)
        {
            //创建选座方式节点
            XmlElement secNode1 = doc.CreateElement("seatChooseMethod");
            secNode1.SetAttribute("chooseMethod", ((int)seatChooseMethod.DefaultChooseMethod).ToString());
            secNode1.SetAttribute("usedAdvancedSet", ConfigConvert.ConvertToString(seatChooseMethod.UsedAdvancedSet));
            foreach (DayOfWeek day in seatChooseMethod.AdvancedSelectSeatMode.Keys)
            {
                //创建选座方式高级设置节点
                XmlElement thirdNode1 = doc.CreateElement("chooseMethodPlan");
                thirdNode1.SetAttribute("dayOfWeek", ((int)day).ToString());
                thirdNode1.SetAttribute("used", ConfigConvert.ConvertToString(seatChooseMethod.AdvancedSelectSeatMode[day].Used));
                foreach (SeatChooseMethodOption option in seatChooseMethod.AdvancedSelectSeatMode[day].PlanOption)
                {
                    XmlElement child3 = doc.CreateElement("planOption");
                    child3.SetAttribute("chooseMethod", ((int)option.ChooseMethod).ToString());
                    child3.SetAttribute("beginTime", option.UsedTime.BeginTime);
                    child3.SetAttribute("endTime", option.UsedTime.EndTime);
                    thirdNode1.AppendChild(child3);
                }
                secNode1.AppendChild(thirdNode1);
            }
            return secNode1;
        }
        /// <summary>
        /// 解析选座方式节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static SeatChooseMethodSet CreateSeatChooseMethod(XmlNode node)
        {
            SeatChooseMethodSet set = new SeatChooseMethodSet();
            set.UsedAdvancedSet = ConfigConvert.ConvertToBool(node.Attributes["usedAdvancedSet"].Value);
            set.DefaultChooseMethod = (SelectSeatMode)int.Parse(node.Attributes["chooseMethod"].Value);
            XmlNodeList nodes = node.ChildNodes;// SelectNodes("//seatChooseMethod/chooseMethodPlan");
            foreach (XmlNode element in nodes)
            {
                DayOfWeek day = (DayOfWeek)int.Parse(element.Attributes["dayOfWeek"].Value);
                SeatChooseMethodPlan plan = new SeatChooseMethodPlan();
                plan.Used = ConfigConvert.ConvertToBool(element.Attributes["used"].Value);
                plan.Day = day;
                XmlNodeList nodes2 = element.ChildNodes;// SelectNodes("//chooseMethodPlan/planOption");
                foreach (XmlNode element2 in nodes2)
                {
                    SeatChooseMethodOption option = new SeatChooseMethodOption();
                    option.UsedTime = new TimeSpace(element2.Attributes["beginTime"].Value, element2.Attributes["endTime"].Value);
                    option.ChooseMethod = (SelectSeatMode)int.Parse(element2.Attributes["chooseMethod"].Value);
                    plan.PlanOption.Add(option);
                }
                set.AdvancedSelectSeatMode.Add(day, plan);
            }
            return set;
        }


        /// <summary>
        /// 座位暂离保留时长节点
        /// </summary>
        /// <param name="seatHoldTime"></param>
        /// <returns></returns>
        private static XmlElement CreateSeatHoldTime(SeatHoldTimeSet seatHoldTime)
        {
            XmlElement element = doc.CreateElement("seatHoldTime");
            element.SetAttribute("defaultHoldTimeLength", seatHoldTime.DefaultHoldTimeLength.ToString());
            element.SetAttribute("usedAdvancedSet", ConfigConvert.ConvertToString(seatHoldTime.UsedAdvancedSet));
            foreach (SeatHoldTimeOption option in seatHoldTime.AdvancedSeatHoldTime)
            {
                XmlElement child = doc.CreateElement("option");
                child.SetAttribute("holdTimeLength", option.HoldTimeLength.ToString());
                child.SetAttribute("beginTime", option.UsedTime.BeginTime);
                child.SetAttribute("endTime", option.UsedTime.EndTime);
                child.SetAttribute("used", ConfigConvert.ConvertToString(option.Used));
                element.AppendChild(child);
            }
            return element;
        }
        /// <summary>
        /// 解析座位暂离保留时长节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static SeatHoldTimeSet CreateSeatHoldTime(XmlNode node)
        {
            SeatHoldTimeSet set = new SeatHoldTimeSet();
            set.UsedAdvancedSet = ConfigConvert.ConvertToBool(node.Attributes["usedAdvancedSet"].Value);
            set.DefaultHoldTimeLength = int.Parse(node.Attributes["defaultHoldTimeLength"].Value);
            XmlNodeList nodes = node.ChildNodes;// SelectNodes("//seatHoldTime/option");
            foreach (XmlNode element in nodes)
            {
                SeatHoldTimeOption option = new SeatHoldTimeOption();
                option.HoldTimeLength = int.Parse(element.Attributes["holdTimeLength"].Value);
                option.Used = ConfigConvert.ConvertToBool(element.Attributes["used"].Value);
                option.UsedTime.BeginTime = element.Attributes["beginTime"].Value;
                option.UsedTime.EndTime = element.Attributes["endTime"].Value;
                set.AdvancedSeatHoldTime.Add(option);
            }
            return set;
        }
        /// <summary>
        /// 设置管理员暂离节点
        /// </summary>
        /// <param name="seatHoldTime"></param>
        /// <returns></returns>
        private static XmlElement CreateAdminSetShortLeave(AdminSetShortLeave adminSetShortLeave)
        {
            XmlElement element = doc.CreateElement("adminSetShortLeave");
            element.SetAttribute("IsUsed", ConfigConvert.ConvertToString(adminSetShortLeave.IsUsed));
            element.SetAttribute("seatholeTime", adminSetShortLeave.HoldTimeLength.ToString());
            return element;
        }
        /// <summary>
        /// 解析管理员暂离节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static AdminSetShortLeave CreateAdminSetShortLeave(XmlNode node)
        {
            AdminSetShortLeave set = new AdminSetShortLeave();
            set.IsUsed = ConfigConvert.ConvertToBool(node.Attributes["IsUsed"].Value);
            set.HoldTimeLength = int.Parse(node.Attributes["seatholeTime"].Value);
            return set;
        }
        /// <summary>
        /// 设置座位号显示位数
        /// </summary>
        /// <param name="seatNumAmount"></param>
        /// <returns></returns>
        private static XmlElement CreateSeatNumAmount(int seatNumAmount)
        {
            XmlElement element = doc.CreateElement("seatNumAmount");
            element.SetAttribute("Amount", seatNumAmount.ToString());
            return element;
        }

        /// <summary>
        /// 解析座位号显示位数
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static int CreateSeatNumAmount(XmlNode node)
        {
            int set = int.Parse(node.Attributes["Amount"].Value);
            return set;
        }
        /// <summary>
        /// 设置是否启用黑名单限制
        /// </summary>
        /// <param name="UsedBlacklistLimit"></param>
        /// <returns></returns>
        private static XmlElement CreateIsUsedBlacklistLimit(bool usedBlacklistLimit)
        {
            XmlElement element = doc.CreateElement("usedBlacklistLimit");
            element.SetAttribute("used", ConfigConvert.ConvertToString(usedBlacklistLimit));
            return element;
        }
        /// <summary>
        /// 设置是否启用黑名单限制
        /// </summary>
        /// <param name="UsedBlacklistLimit"></param>
        /// <returns></returns>
        private static XmlElement CreateIsRecordViolate(bool isRecordViolate)
        {
            XmlElement element = doc.CreateElement("isRecordViolate");
            element.SetAttribute("used", ConfigConvert.ConvertToString(isRecordViolate));
            return element;
        }
        /// <summary>
        /// 解析是否启用黑名单设置的节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static bool CreateIsUsedBlacklistLimit(XmlNode node)
        {
            bool set = ConfigConvert.ConvertToBool(node.Attributes["used"].Value);
            return set;
        }
        private static bool CreateIsRecordViolate(XmlNode node)
        {
            bool set = ConfigConvert.ConvertToBool(node.Attributes["used"].Value);
            return set;
        }
        /// <summary>
        /// 解析黑名单
        /// </summary>
        /// <param name="blacklist"></param>
        /// <returns></returns>
        private static XmlElement CreateBlacklist(ReadingRoomBlacklistSetting blacklist)
        {
            XmlElement element = doc.CreateElement("blacklist");
            element.SetAttribute("used", ConfigConvert.ConvertToString(blacklist.Used));
            element.SetAttribute("violateTimes", blacklist.ViolateTimes.ToString());
            element.SetAttribute("limitDays", blacklist.LimitDays.ToString());
            element.SetAttribute("leaveBlacklist", ((int)blacklist.LeaveBlacklist).ToString());
            element.SetAttribute("ViolateFailDays", blacklist.ViolateFailDays.ToString());
            foreach (ViolationRecordsType violateType in blacklist.ViolateRoule.Keys)
            {
                XmlElement child = doc.CreateElement("violateType");
                child.SetAttribute("used", ConfigConvert.ConvertToString(blacklist.ViolateRoule[violateType]));
                child.SetAttribute("typeValue", ((int)violateType).ToString());
                element.AppendChild(child);
            }
            return element;
        }
        /// <summary>
        /// 保存黑名单
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static ReadingRoomBlacklistSetting CreateBlacklist(XmlNode node)
        {
            //node = doc.SelectSingleNode("//blacklist");
            ReadingRoomBlacklistSetting set = new ReadingRoomBlacklistSetting();
            set.LeaveBlacklist = (LeaveBlacklistMode)int.Parse(node.Attributes["leaveBlacklist"].Value);
            set.LimitDays = int.Parse(node.Attributes["limitDays"].Value);
            set.Used = ConfigConvert.ConvertToBool(node.Attributes["used"].Value);
            set.ViolateTimes = int.Parse(node.Attributes["violateTimes"].Value);
            set.ViolateFailDays = int.Parse(node.Attributes["ViolateFailDays"].Value);
            XmlNodeList nodes = node.ChildNodes;// SelectNodes("//blacklist/violateType");
            foreach (XmlNode element in nodes)
            {
                set.ViolateRoule[(ViolationRecordsType)int.Parse(element.Attributes["typeValue"].Value)] = ConfigConvert.ConvertToBool(element.Attributes["used"].Value);
            }
            return set;
        }
        /// <summary>
        /// 可以选择该阅览室读者类型节点
        /// </summary>
        /// <param name="limitReaderEnter"></param>
        /// <returns></returns>
        private static XmlElement CreateLimitReaderEnter(LimitReaderEnterSet limitReaderEnter)
        {
            XmlElement element = doc.CreateElement("limitReaderEnter");
            element.SetAttribute("readerTypes", limitReaderEnter.ReaderTypes);
            element.SetAttribute("canEnter", ConfigConvert.ConvertToString(limitReaderEnter.CanEnter));
            element.SetAttribute("used", ConfigConvert.ConvertToString(limitReaderEnter.Used));
            return element;
        }
        /// <summary>
        /// 解析可以选择该阅览室读者类型节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static LimitReaderEnterSet CreateLimitReaderEnter(XmlNode node)
        {
            LimitReaderEnterSet set = new LimitReaderEnterSet();
            set.CanEnter = ConfigConvert.ConvertToBool(node.Attributes["canEnter"].Value);
            set.ReaderTypes = node.Attributes["readerTypes"].Value;
            set.Used = ConfigConvert.ConvertToBool(node.Attributes["used"].Value);
            return set;
        }


        #endregion

        #region 根据设置进行判断
        /// <summary>
        /// 根据日期获取阅览室开闭时间
        /// </summary>
        /// <param name="set"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public TimeSpace GetRoomOpenTimeByDate(DateTime date)
        {
            DayOfWeek day = date.DayOfWeek;
            TimeSpace timeList = new TimeSpace();

            if (this.RoomOpenSet.UsedAdvancedSet)
            {
                if (this.RoomOpenSet.RoomOpenPlan[day].Used)
                {
                    timeList = this.RoomOpenSet.RoomOpenPlan[day].OpenTime[0];
                    //bespeakTime = DateTime.Parse(string.Format("{0} {1}", Convert.ToDateTime(date).ToShortDateString(), set.RoomOpenSet.RoomOpenPlan[day].OpenTime[0].BeginTime));
                }
                else
                {
                    timeList.BeginTime = this.RoomOpenSet.DefaultOpenTime.BeginTime;
                    timeList.EndTime = this.RoomOpenSet.DefaultOpenTime.EndTime;
                    //bespeakTime = DateTime.Parse(string.Format("{0} {1}", Convert.ToDateTime(date).ToShortDateString(), set.RoomOpenSet.DefaultOpenTime.BeginTime));
                }
            }
            else
            {
                timeList.BeginTime = this.RoomOpenSet.DefaultOpenTime.BeginTime;
                timeList.EndTime = this.RoomOpenSet.DefaultOpenTime.EndTime;
            }
            return timeList;
        }
        /// <summary>
        /// 当前闭馆时间
        /// </summary>
        public DateTime NowCloseTime(DateTime dateTime)
        {

            if (this.RoomOpenSet.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = DateTime.Now.DayOfWeek;
                RoomOpenPlanSet plan = this.RoomOpenSet.RoomOpenPlan[day];
                if (plan.Used)
                {
                    foreach (TimeSpace t in plan.OpenTime)
                    {
                        if (dateTime < DateTime.Parse(t.EndTime) && dateTime > DateTime.Parse(t.BeginTime))
                        {
                            return DateTime.Parse(t.EndTime);
                        }
                    }
                    return DateTime.Parse(this.RoomOpenSet.DefaultOpenTime.EndTime);
                }
                else
                {
                    return DateTime.Parse(this.RoomOpenSet.DefaultOpenTime.EndTime);
                }
            }
            else
            {
                return DateTime.Parse(this.RoomOpenSet.DefaultOpenTime.EndTime);
            }
        }
        /// <summary>
        /// 当前开馆时间
        /// </summary>
        public DateTime NowOpenTime(DateTime dateTime)
        {
            if (this.RoomOpenSet.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = DateTime.Now.DayOfWeek;
                RoomOpenPlanSet plan = this.RoomOpenSet.RoomOpenPlan[day];
                if (plan.Used)
                {
                    foreach (TimeSpace t in plan.OpenTime)
                    {
                        if (dateTime < DateTime.Parse(t.EndTime) && dateTime > DateTime.Parse(t.BeginTime))
                        {
                            return DateTime.Parse(t.BeginTime);
                        }
                    }
                    return DateTime.Parse(this.RoomOpenSet.DefaultOpenTime.BeginTime);
                }
                else
                {
                    return DateTime.Parse(this.RoomOpenSet.DefaultOpenTime.BeginTime);
                }
            }
            else
            {
                return DateTime.Parse(this.RoomOpenSet.DefaultOpenTime.BeginTime);
            }
        }
        /// <summary>
        /// 获取传值日期的开馆时间
        /// </summary>
        public DateTime DateOpenTime(DateTime date)
        {
            if (this.RoomOpenSet.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = date.DayOfWeek;
                RoomOpenPlanSet plan = this.RoomOpenSet.RoomOpenPlan[day];
                if (plan.Used)
                {
                    foreach (TimeSpace t in plan.OpenTime)
                    {
                        if (date.Date < DateTime.Parse(date.ToShortDateString() + " " + t.EndTime))
                        {
                            return DateTime.Parse(date.ToShortDateString() + " " + t.BeginTime);
                        }
                    }
                    return DateTime.Parse(date.ToShortDateString() + " " + this.RoomOpenSet.DefaultOpenTime.BeginTime);
                }
                else
                {
                    return DateTime.Parse(date.ToShortDateString() + " " + this.RoomOpenSet.DefaultOpenTime.BeginTime);
                }
            }
            else
            {
                return DateTime.Parse(date.ToShortDateString() + " " + this.RoomOpenSet.DefaultOpenTime.BeginTime);
            }
        }
        /// <summary>
        /// 计算座位保留时长
        /// </summary>
        /// <param name="shortLeaveSet">座位暂离设置</param>
        /// <param name="time">所要判断的时间</param>
        /// <returns></returns>
        public double GetSeatHoldTime(DateTime time)
        {
            if (this.SeatHoldTime.UsedAdvancedSet)
            {
                foreach (SeatHoldTimeOption option in this.SeatHoldTime.AdvancedSeatHoldTime)
                {
                    if (option.Used)
                    { //判断指定的时间是否在开始时间和结束时间中间
                        DateTime begintime = DateTime.Parse(time.ToShortDateString() + " " + option.UsedTime.BeginTime);
                        DateTime endtime = DateTime.Parse(time.ToShortDateString() + " " + option.UsedTime.EndTime);
                        if (DateTimeOperate.DateAccord(begintime, endtime, time))
                        {
                            return option.HoldTimeLength;
                        }
                    }
                }
                //遍历结束没有返回，则返回默认保留时长
                return this.SeatHoldTime.DefaultHoldTimeLength;
            }
            else
            {
                //没有启用阅览室设置，则返回默认保留时长
                return this.SeatHoldTime.DefaultHoldTimeLength;
            }
        }
        /// <summary>
        /// 获取可预约的时段
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<DateTime> GetBespeakTimeList(DateTime date)
        {
            if (!this.SeatBespeak.SpecifiedBespeak)
            {
                return new List<DateTime>();
            }
            if (this.SeatBespeak.SpecifiedTime)
            {
                return GetSpecifiedTimeList(date);
            }
            else
            {
                return GetFreeChoiceTimeList(date, 10);
            }
        }
        /// <summary>
        /// 获取自定义预约的时间段
        /// </summary>
        /// <param name="date">需要预约的日期</param>
        /// <param name="roomOCSet">阅览室的开闭馆设置</param>
        /// <param name="bespeakSet">座位预约设置</param>
        /// <param name="interval">间隔时间（分钟）</param>
        /// <returns></returns>
        public List<DateTime> GetFreeChoiceTimeList(DateTime date, int interval)
        {
            List<DateTime> timeList = new List<DateTime>();
            DateTime sTime = DateOpenTime(date);
            if (date.Date == DateTime.Now.Date)
            {
                sTime = DateTime.Now;
            }
            else if (this.RoomOpenSet.UninterruptibleModel)
            {
                sTime = date.Date;
            }
            DateTime time = sTime.AddMinutes(interval - sTime.Minute % interval);
            while (true)
            {
                time = time.AddMinutes(interval);
                if (time.Date > date.Date)
                {
                    break;
                }
                if (ReadingRoomOpenState(time) == SeatManage.EnumType.ReadingRoomStatus.Close)
                {
                    continue;
                }
                timeList.Add(time);
            }
            return timeList;
        }
        /// <summary>
        /// 获取指定日期的指定预约时间段
        /// </summary>
        /// <param name="date">预约的日期</param>
        /// <param name="roomOCSet">阅览室开闭设置</param>
        /// <param name="bespeakSet">预约设置</param>
        /// <returns></returns>
        public List<DateTime> GetSpecifiedTimeList(DateTime date)
        {
            return this.SeatBespeak.SpecifiedTimeList.Where(time => DateTime.Parse(date.ToLongDateString() + " " + time.ToLongTimeString()) > DateTime.Now).Where(time => ReadingRoomOpenState(time) != SeatManage.EnumType.ReadingRoomStatus.Close).ToList();
        }

        /// <summary>
        /// 获取指定日期的指定预约时间段
        /// </summary>
        /// <param name="date">预约的日期</param>
        /// <param name="roomOCSet">阅览室开闭设置</param>
        /// <param name="bespeakSet">预约设置</param>
        /// <returns></returns>
        public List<DateTime> GetSelectTimeList(DateTime date)
        {
            List<DateTime> timeList = new List<DateTime>();
            DateTime minTime = DateOpenTime(date);
            if (minTime > DateTime.Now)
            {
                timeList.Add(minTime);
            }
            if (this.SeatBespeak.SpecifiedTime)
            {
                foreach (DateTime time in this.SeatBespeak.SpecifiedTimeList)
                {
                    if (DateTime.Parse(date.ToLongDateString() + " " + time.ToLongTimeString()) <= DateTime.Now)
                    {
                        continue;
                    }
                    if (ReadingRoomOpenState(time) == SeatManage.EnumType.ReadingRoomStatus.Close)
                    {
                        continue;
                    }
                    timeList.Add(time);
                }
            }
            else
            {
                while (true)
                {
                    minTime = minTime.AddMinutes(10);
                    if (minTime <= DateTime.Now)
                    {
                        continue;
                    }
                    if (minTime.Date > date.Date)
                    {
                        break;
                    }
                    if (ReadingRoomOpenState(minTime) == ReadingRoomStatus.Close)
                    {
                        continue;
                    }
                    timeList.Add(minTime);
                }
            }
            return timeList;
        }
        /// <summary>
        /// 阅览室开放状态
        /// </summary>
        /// <param name="roomOCSet"></param>
        /// <returns></returns>
        public ReadingRoomStatus ReadingRoomOpenState(DateTime time)
        {
            if (this.RoomOpenSet.UninterruptibleModel)
            {
                return ReadingRoomStatus.Open;
            }
            ReadingRoomStatus openState = ReadingRoomStatus.Close;

            if (this.RoomOpenSet.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = time.DayOfWeek;
                try
                {
                    RoomOpenPlanSet plan = this.RoomOpenSet.RoomOpenPlan[day];

                    if (plan.Used)
                    {
                        foreach (TimeSpace t in plan.OpenTime)
                        {
                            openState = calcRoomState(t.BeginTime, t.EndTime, time, this.RoomOpenSet.OpenBeforeTimeLength, this.RoomOpenSet.CloseBeforeTimeLength);
                            switch (openState)
                            { //当前时间阅览室状态为非关闭状态，直接返回结果。否则继续判断
                                case ReadingRoomStatus.BeforeClose:
                                case ReadingRoomStatus.BeforeOpen:
                                case ReadingRoomStatus.Open:
                                    return openState;
                            }
                        }
                        //遍历结束没有返回，则返回最后一次计算的结果
                        return openState;
                    }
                    else
                    {
                        //否则当天没启用高级设置，返回默认开馆状态
                        openState = calcRoomState(this.RoomOpenSet.DefaultOpenTime.BeginTime, this.RoomOpenSet.DefaultOpenTime.EndTime, time, this.RoomOpenSet.OpenBeforeTimeLength, this.RoomOpenSet.CloseBeforeTimeLength);
                        return openState;
                    }
                }
                catch
                {
                    //当天没有高级设置，则返回默认开馆状态。
                    openState = calcRoomState(this.RoomOpenSet.DefaultOpenTime.BeginTime, this.RoomOpenSet.DefaultOpenTime.EndTime, time, this.RoomOpenSet.OpenBeforeTimeLength, this.RoomOpenSet.CloseBeforeTimeLength);
                    return openState;
                }
            }
            else
            {
                //没有开启高级设置，则返回默认开馆状态。
                openState = calcRoomState(this.RoomOpenSet.DefaultOpenTime.BeginTime, this.RoomOpenSet.DefaultOpenTime.EndTime, time, this.RoomOpenSet.OpenBeforeTimeLength, this.RoomOpenSet.CloseBeforeTimeLength);
                return openState;
            }

        }
        /// <summary>
        /// 根据时间计算阅览室的状态
        /// </summary>
        /// <param name="beginTime">开馆时间</param>
        /// <param name="endTime">闭馆时间</param>
        /// <param name="datetime">要判断开放状态的时间</param>
        /// <param name="openBeforeTimeLength">开馆预处理</param>
        /// <param name="closeBeforeTimeLength">闭馆预处理</param>
        /// <returns></returns>
        private static ReadingRoomStatus calcRoomState(string beginTime, string endTime, DateTime datetime, double openBeforeTimeLength, double closeBeforeTimeLength)
        {
            DateTime begindate = DateTime.Parse(datetime.ToShortDateString() + " " + beginTime);
            DateTime enddate = DateTime.Parse(datetime.ToShortDateString() + " " + endTime);

            if (DateTimeOperate.DateAccord(enddate.AddMinutes(-closeBeforeTimeLength), enddate, datetime))//判断是否符合闭馆预处理
            {
                return ReadingRoomStatus.BeforeClose;
            }
            else if (DateTimeOperate.DateAccord(begindate, enddate, datetime))
            {
                return ReadingRoomStatus.Open;
            }
            else if (DateTimeOperate.DateAccord(begindate.AddMinutes(-openBeforeTimeLength), begindate, datetime))//判断是否符合开馆预处理
            {
                return ReadingRoomStatus.BeforeOpen;
            }
            else
            {
                return ReadingRoomStatus.Close;//条件都不符合，则为闭馆。
            }
        }

        /// <summary>
        /// 判断当前时间阅览室选座状态
        /// </summary>
        /// <param name="set"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public SelectSeatMode RoomSelectSeatMode(DateTime date)
        {
            SelectSeatMode chooseMethod = this.SeatChooseMethod.DefaultChooseMethod;
            //判断是否启用高级设置
            if (this.SeatChooseMethod.UsedAdvancedSet)
            {
                DayOfWeek day = date.DayOfWeek;
                try
                {
                    SeatChooseMethodPlan plan = this.SeatChooseMethod.AdvancedSelectSeatMode[day];
                    DateTime strDate = date;
                    //遍历当天的时间段，判断是是否有满足当前时间的设置项
                    foreach (SeatChooseMethodOption option in plan.PlanOption)
                    {
                        DateTime beginDatetime = DateTime.Parse(strDate.ToShortDateString() + " " + option.UsedTime.BeginTime);
                        DateTime endDatetime = DateTime.Parse(strDate.ToShortDateString() + " " + option.UsedTime.EndTime);
                        if (DateTimeOperate.DateAccord(beginDatetime, endDatetime, date))//判断当前时间是否满足项
                        {
                            chooseMethod = option.ChooseMethod;
                            break;
                        }
                    }

                }
                catch
                {
                    chooseMethod = this.SeatChooseMethod.DefaultChooseMethod;
                }
            }

            return chooseMethod;
        }
        /// <summary>
        /// 判断当前时间是否可以预约座位
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsCanBespeakSeat(DateTime time)
        {
            if (this.SeatBespeak.Used)
            {
                foreach (TimeSpace ts in this.SeatBespeak.NoBespeakDates)
                {
                    DateTime begintime = DateTime.Parse(time.Year.ToString() + "-" + ts.BeginTime);
                    DateTime endtime = DateTime.Parse(time.Year.ToString() + "-" + ts.EndTime);
                    //判断读者选择的时间是否在不允许预约的时间范围内
                    if (DateTimeOperate.DateAccord(begintime, endtime, time))
                    {
                        return false;
                    }
                }

                //指定的时间减去提前预约的天数， 

                //DateTime begindate = time.AddDays(-this.SeatBespeak.BespeakBeforeDays);

                //if (DateTimeOperate.DateAccord(begindate, time, DateTime.Now))
                //{
                if (time.Date> DateTime.Now.AddDays(this.SeatBespeak.BespeakBeforeDays).Date)
                {
                    return false;
                }
                //当前时间在允许预约的时间之间，返回允许预约

                DateTime canBespeakBegin = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + this.SeatBespeak.CanBespeatTimeSpace.BeginTime);
                DateTime canBespeakEnd = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + this.SeatBespeak.CanBespeatTimeSpace.EndTime);
                if (canBespeakBegin <= DateTime.Now && canBespeakEnd >= DateTime.Now)
                //if (DateTimeOperate.DateAccord(canBespeakBegin, canBespeakEnd, DateTime.Now))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        #endregion
    }

    /// <summary>
    /// 选座方式设置
    /// </summary>
    [Serializable]
    public class SeatChooseMethodSet
    {

        private SelectSeatMode _DefaultChooseMethod = SelectSeatMode.OptionalMode;
        private bool _UsedAdvancedSet = false;
        private Dictionary<DayOfWeek, SeatChooseMethodPlan> _AdvancedSelectSeatMode = new Dictionary<DayOfWeek, SeatChooseMethodPlan>();
        /// <summary>
        /// 默认选座方式
        /// </summary>
        public SelectSeatMode DefaultChooseMethod
        {
            get { return _DefaultChooseMethod; }
            set { _DefaultChooseMethod = value; }
        }
        /// <summary>
        /// 高级设置
        /// </summary>
        public Dictionary<DayOfWeek, SeatChooseMethodPlan> AdvancedSelectSeatMode
        {
            get { return _AdvancedSelectSeatMode; }
            set { _AdvancedSelectSeatMode = value; }
        }
        /// <summary>
        /// 设置是否启用高级设置
        /// </summary>
        public bool UsedAdvancedSet
        {
            get { return _UsedAdvancedSet; }
            set { _UsedAdvancedSet = value; }
        }

    }
    /// <summary>
    /// 选座方式更改计划
    /// </summary>
    [Serializable]
    public class SeatChooseMethodPlan
    {
        private DayOfWeek _Day;
        /// <summary>
        /// 一周的某一天
        /// </summary>
        public DayOfWeek Day
        {
            get { return _Day; }
            set { _Day = value; }
        }
        private bool _Used;
        /// <summary>
        /// 是否启用计划
        /// </summary>
        public bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }
        private List<SeatChooseMethodOption> _Plan = new List<SeatChooseMethodOption>();
        /// <summary>
        /// 计划时间段
        /// </summary>
        public List<SeatChooseMethodOption> PlanOption
        {
            get { return _Plan; }
            set { _Plan = value; }
        }
    }
    /// <summary>
    /// 选座方式高级选项
    /// </summary>
    [Serializable]
    public class SeatChooseMethodOption
    {
        private TimeSpace _UsedTime = new TimeSpace();
        private SelectSeatMode _ChooseMethod = SelectSeatMode.OptionalMode;
        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpace UsedTime
        {
            get { return _UsedTime; }
            set { _UsedTime = value; }
        }
        /// <summary>
        /// 选座方式
        /// </summary>
        public SelectSeatMode ChooseMethod
        {
            get { return _ChooseMethod; }
            set { _ChooseMethod = value; }
        }
    }
    /// <summary>
    /// 暂时离开设置
    /// </summary>
    [Serializable]
    public class SeatHoldTimeSet
    {

        private int _DefaultHoldTimeLength = 30;
        private bool _UsedAdvancedSet = false;
        private List<SeatHoldTimeOption> _AdvancedSeatHoldTime = new List<SeatHoldTimeOption>();
        /// <summary>
        /// 座位保留时长高级设置
        /// </summary>
        public List<SeatHoldTimeOption> AdvancedSeatHoldTime
        {
            get { return _AdvancedSeatHoldTime; }
            set { _AdvancedSeatHoldTime = value; }
        }
        /// <summary>
        /// 默认保留时间（分钟） 
        /// </summary>
        public int DefaultHoldTimeLength
        {
            get { return _DefaultHoldTimeLength; }
            set { _DefaultHoldTimeLength = value; }
        }
        /// <summary>
        /// 启用高级设置
        /// </summary>
        public bool UsedAdvancedSet
        {
            get { return _UsedAdvancedSet; }
            set { _UsedAdvancedSet = value; }
        }

    }
    /// <summary>
    /// 管理员设置暂离设置
    /// </summary>
    [Serializable]
    public class AdminSetShortLeave
    {
        private bool _IsUsed = false;
        private int _HoldTimeLength = 20;
        /// <summary>
        /// 座位保留时间
        /// </summary>
        public int HoldTimeLength
        {
            get { return _HoldTimeLength; }
            set { _HoldTimeLength = value; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return _IsUsed; }
            set { _IsUsed = value; }
        }

    }
    /// <summary>
    /// 座位保留高级设置
    /// </summary>
    [Serializable]
    public class SeatHoldTimeOption
    {
        private TimeSpace _UsedTime = new TimeSpace();
        private bool _Used = false;
        private int _HoldTimeLength = 60;
        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpace UsedTime
        {
            get { return _UsedTime; }
            set { _UsedTime = value; }
        }

        /// <summary>
        /// 保留时间（分钟）
        /// </summary>
        public int HoldTimeLength
        {
            get { return _HoldTimeLength; }
            set { _HoldTimeLength = value; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }

    }
    /// <summary>
    /// 开闭馆设置
    /// </summary>
    [Serializable]
    public class RoomOpenTimeSet
    {
        private double _OpenBeforeTimeLength = 30;
        private double _CloseBeforeTimeLength = 30;
        private Dictionary<DayOfWeek, RoomOpenPlanSet> _RoomOpenPlan = new Dictionary<DayOfWeek, RoomOpenPlanSet>();
        private TimeSpace _DefaultOpenTime = new TimeSpace("8:00", "21:30");
        private bool _UsedAdvancedSet = false;
        private bool _UninterruptibleModel = false;
        /// <summary>
        /// 设置是否启用高级设置
        /// </summary>
        public bool UsedAdvancedSet
        {
            get { return _UsedAdvancedSet; }
            set { _UsedAdvancedSet = value; }
        }
        /// <summary>
        /// 开馆预处理时长
        /// </summary>
        public double OpenBeforeTimeLength
        {
            get { return _OpenBeforeTimeLength; }
            set { _OpenBeforeTimeLength = value; }
        }
        /// <summary>
        /// 闭馆预处理时长
        /// </summary>
        public double CloseBeforeTimeLength
        {
            get { return _CloseBeforeTimeLength; }
            set { _CloseBeforeTimeLength = value; }
        }
        /// <summary>
        /// 默认开放时间
        /// </summary>
        public TimeSpace DefaultOpenTime
        {
            get { return _DefaultOpenTime; }
            set { _DefaultOpenTime = value; }
        }
        /// <summary>
        /// 开馆计划,键为周几
        /// </summary>
        public Dictionary<DayOfWeek, RoomOpenPlanSet> RoomOpenPlan
        {
            get { return _RoomOpenPlan; }
            set { _RoomOpenPlan = value; }
        }

        /// <summary>
        /// 24小时不间断模式
        /// </summary>
        public bool UninterruptibleModel
        {
            get { return _UninterruptibleModel; }
            set { _UninterruptibleModel = value; }
        }
        /// <summary>
        /// 当前闭馆时间
        /// </summary>
        public DateTime NowCloseTime(DateTime dateTime)
        {

            if (this.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = DateTime.Now.DayOfWeek;
                RoomOpenPlanSet plan = this.RoomOpenPlan[day];
                if (plan.Used)
                {
                    foreach (TimeSpace t in plan.OpenTime)
                    {
                        if (dateTime < DateTime.Parse(t.EndTime) && dateTime > DateTime.Parse(t.BeginTime))
                        {
                            return DateTime.Parse(t.EndTime);
                        }
                    }
                    return DateTime.Parse(this.DefaultOpenTime.EndTime);
                }
                else
                {
                    return DateTime.Parse(this.DefaultOpenTime.EndTime);
                }
            }
            else
            {
                return DateTime.Parse(this.DefaultOpenTime.EndTime);
            }


        }
        /// <summary>
        /// 当前开馆时间
        /// </summary>
        public DateTime NowOpenTime(DateTime dateTime)
        {
            if (this.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = DateTime.Now.DayOfWeek;
                RoomOpenPlanSet plan = this.RoomOpenPlan[day];
                if (plan.Used)
                {
                    foreach (TimeSpace t in plan.OpenTime)
                    {
                        if (dateTime < DateTime.Parse(t.EndTime) && dateTime > DateTime.Parse(t.BeginTime))
                        {
                            return DateTime.Parse(t.BeginTime);
                        }
                    }
                    return DateTime.Parse(this.DefaultOpenTime.BeginTime);
                }
                else
                {
                    return DateTime.Parse(this.DefaultOpenTime.BeginTime);
                }
            }
            else
            {
                return DateTime.Parse(this.DefaultOpenTime.BeginTime);
            }
        }
        /// <summary>
        /// 当前时间下一个开馆时间
        /// </summary>
        public DateTime NowNextOpenTime(DateTime dateTime)
        {
            if (this.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = DateTime.Now.DayOfWeek;
                RoomOpenPlanSet plan = this.RoomOpenPlan[day];
                if (plan.Used)
                {
                    foreach (TimeSpace t in plan.OpenTime)
                    {
                        if (dateTime < DateTime.Parse(t.BeginTime))
                        {
                            return DateTime.Parse(t.BeginTime);
                        }
                    }
                    return DateTime.Parse(this.DefaultOpenTime.BeginTime) > dateTime ? DateTime.Parse(this.DefaultOpenTime.BeginTime) : new DateTime();
                }
                else
                {
                    return DateTime.Parse(this.DefaultOpenTime.BeginTime) > dateTime ? DateTime.Parse(this.DefaultOpenTime.BeginTime) : new DateTime();
                }
            }
            else
            {
                return DateTime.Parse(this.DefaultOpenTime.BeginTime) > dateTime ? DateTime.Parse(this.DefaultOpenTime.BeginTime) : new DateTime();
            }
        }
        /// <summary>
        /// 获取传值日期的开馆时间
        /// </summary>
        public DateTime DateOpenTime(DateTime date)
        {
            if (this.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = date.DayOfWeek;
                RoomOpenPlanSet plan = this.RoomOpenPlan[day];
                if (plan.Used)
                {
                    foreach (TimeSpace t in plan.OpenTime)
                    {
                        if (date.Date < DateTime.Parse(date.ToShortDateString() + " " + t.EndTime))
                        {
                            return DateTime.Parse(date.ToShortDateString() + " " + t.BeginTime);
                        }
                    }
                    return DateTime.Parse(date.ToShortDateString() + " " + this.DefaultOpenTime.BeginTime);
                }
                else
                {
                    return DateTime.Parse(date.ToShortDateString() + " " + this.DefaultOpenTime.BeginTime);
                }
            }
            else
            {
                return DateTime.Parse(date.ToShortDateString() + " " + this.DefaultOpenTime.BeginTime);
            }
        }


    }
    /// <summary>
    /// 开馆计划
    /// </summary>
    [Serializable]
    public class RoomOpenPlanSet
    {
        private DayOfWeek _Week;
        private bool _Used = false;
        private List<TimeSpace> _OpenTime = new List<TimeSpace>();
        /// <summary>
        /// 指定一周的某天
        /// </summary>
        public DayOfWeek Day
        {
            get { return _Week; }
            set { _Week = value; }
        }
        /// <summary>
        /// 是否启用开馆计划
        /// </summary>
        public bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }
        /// <summary>
        /// 计划时间段
        /// </summary>
        public List<TimeSpace> OpenTime
        {
            get { return _OpenTime; }
            set { _OpenTime = value; }
        }
    }
    /// <summary>
    /// 无人管理设置
    /// </summary>
    [Serializable]
    public class NoManagementSet
    {
        private bool _Used = false;
        private double _OperatingInterval = 30;
        /// <summary>
        /// 设置高级设置是否启用
        /// </summary>
        public bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }
        /// <summary>
        /// 操作时间间隔
        /// </summary>
        public double OperatingInterval
        {
            get { return _OperatingInterval; }
            set { _OperatingInterval = value; }
        }
    }

    /// <summary>
    /// 座位使用时间限制设置
    /// </summary>
    [Serializable]
    public class SeatUsedTimeLimitSet
    {
        private bool _Used = false;
        /// <summary>
        /// 是否启用座位使用时长限制
        /// </summary>
        public bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }
        private double _UsedTimeLength = 180;
        /// <summary>
        /// 设置选座后使用的时长
        /// </summary>
        public double UsedTimeLength
        {
            get { return _UsedTimeLength; }
            set { _UsedTimeLength = value; }
        }
        private EnterOutLogType _OverTimeHandle = EnterOutLogType.Leave;
        /// <summary>
        /// 设置座位使用超时后的处理方式
        /// </summary>
        public EnterOutLogType OverTimeHandle
        {
            get { return _OverTimeHandle; }
            set
            {
                switch (value)
                {
                    case EnterOutLogType.Leave:
                    case EnterOutLogType.ShortLeave:
                        _OverTimeHandle = value;
                        break;
                    default: throw new ValueNotRightException("离开处理参数错误");

                }
            }
        }
        private bool _IsCanContinuedTime = true;
        /// <summary>
        /// 是否允许续时
        /// </summary>
        public bool IsCanContinuedTime
        {
            get { return _IsCanContinuedTime; }
            set { _IsCanContinuedTime = value; }
        }
        private int _ContinuedTimes = 3;
        /// <summary>
        /// 设置可续时次数
        /// </summary>
        public int ContinuedTimes
        {
            get { return _ContinuedTimes; }
            set { _ContinuedTimes = value; }
        }
        private double _DelayTimeLength = 120;
        /// <summary>
        /// 设置续时时长
        /// </summary>
        public double DelayTimeLength
        {
            get { return _DelayTimeLength; }
            set { _DelayTimeLength = value; }
        }
        private double _CanDelayTime = 30;
        /// <summary>
        /// 允许续时的倒记时间
        /// </summary>
        public double CanDelayTime
        {
            get { return _CanDelayTime; }
            set { _CanDelayTime = value; }
        }
        private string _Mode = "Free";
        /// <summary>
        /// 在座时长限制模式固定模式：Fixed，自己续时：Free
        /// </summary>
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        private List<DateTime> _FixedTimes = new List<DateTime>();
        /// <summary>
        /// 固定释放时间段
        /// </summary>
        public List<DateTime> FixedTimes
        {
            get { return _FixedTimes; }
            set { _FixedTimes = value; }
        }
        private bool _CanNotContinuedWithBookNetFixed = false;
        /// <summary>
        /// 下一个时段如果座位被预约不允许续时
        /// </summary>
        public bool CanNotContinuedWithBookNetFixed
        {
            get { return _CanNotContinuedWithBookNetFixed; }
            set { _CanNotContinuedWithBookNetFixed = value; }
        }
    }
    /// <summary>
    /// 座位预约设置
    /// </summary>
    [Serializable]
    public class SeatBespeakSet
    {
        private bool _Used = false;
        private double _BespeakBeforeDays = 1;
        private TimeSpace _ConfirmTime = new TimeSpace("10", "20");
        private TimeSpace _CanBespeatTimeSpace = new TimeSpace("0:00:00", "23:59:59");
        private SeatBespeakArea _BespeakArea = new SeatBespeakArea();
        private List<TimeSpace> _NotCanBespeakDates = new List<TimeSpace>();
        private bool _AllowShortLeave = true;
        private bool _AllowDelayTime = false;
        private bool _AllowLeave = true;
        private bool _NowDayBespeak = false;
        private double _SeatKeepTime = 30;
        private bool _SpecifiedBespeak = false;
        private bool _SpecifiedTime = false;
        private List<DateTime> _SpecifiedTimeList = new List<DateTime>() { DateTime.Parse("10:00"), DateTime.Parse("12:00") };
        private bool _SelectBespeakSeat = true;
        private int _BespeakSeatCount = 1;
        private bool _BespeatWithOnSeat = false;
        private bool _CanBookMultiSpan = false;
        private bool _CanBookUsingSeat = false;
        /// <summary>
        /// 允许预约正在被使用的座位
        /// </summary>
        public bool CanBookUsingSeat
        {
            get { return _CanBookUsingSeat; }
            set { _CanBookUsingSeat = value; }
        }
        /// <summary>
        /// 允许多个时间段预约
        /// </summary>
        public bool CanBookMultiSpan
        {
            get { return _CanBookMultiSpan; }
            set { _CanBookMultiSpan = value; }
        }
        /// <summary>
        /// 允许在有座位的情况下预约座位(当天座位)
        /// </summary>
        public bool BespeatWithOnSeat
        {
            get { return _BespeatWithOnSeat; }
            set { _BespeatWithOnSeat = value; }
        }
        /// <summary>
        /// 预约次数限制
        /// </summary>
        public int BespeakSeatCount
        {
            get { return _BespeakSeatCount; }
            set { _BespeakSeatCount = value; }
        }
        /// <summary>
        /// 可选择被预约的座位
        /// </summary>
        public bool SelectBespeakSeat
        {
            get { return _SelectBespeakSeat; }
            set { _SelectBespeakSeat = value; }
        }
        /// <summary>
        /// 是否开启指定时间预约功能
        /// </summary>
        public bool SpecifiedBespeak
        {
            get { return _SpecifiedBespeak; }
            set { _SpecifiedBespeak = value; }
        }
        /// <summary>
        /// 是否开启指定时段
        /// </summary>
        public bool SpecifiedTime
        {
            get { return _SpecifiedTime; }
            set { _SpecifiedTime = value; }
        }
        /// <summary>
        /// 指定预约时间段
        /// </summary>
        public List<DateTime> SpecifiedTimeList
        {
            get { return _SpecifiedTimeList; }
            set { _SpecifiedTimeList = value; }
        }

        /// <summary>
        /// 当天预约座位保留时间
        /// </summary>
        public double SeatKeepTime
        {
            get { return _SeatKeepTime; }
            set { _SeatKeepTime = value; }
        }
        /// <summary>
        /// 是否允许预约当天座位
        /// </summary>
        public bool NowDayBespeak
        {
            get { return _NowDayBespeak; }
            set { _NowDayBespeak = value; }
        }
        /// <summary>
        /// 预约网站允许暂离
        /// </summary>
        public bool AllowShortLeave
        {
            get { return _AllowShortLeave; }
            set { _AllowShortLeave = value; }
        }
        /// <summary>
        /// 预约网站允许续时
        /// </summary>
        public bool AllowDelayTime
        {
            get { return _AllowDelayTime; }
            set { _AllowDelayTime = value; }
        }
        /// <summary>
        /// 预约网站允许释放座位
        /// </summary>
        public bool AllowLeave
        {
            get { return _AllowLeave; }
            set { _AllowLeave = value; }
        }
        /// <summary>
        /// 不提供预约的日期范围
        /// </summary>
        public List<TimeSpace> NoBespeakDates
        {
            get { return _NotCanBespeakDates; }
            set { _NotCanBespeakDates = value; }
        }
        /// <summary>
        /// 能够预约的时间范围
        /// </summary>
        public TimeSpace CanBespeatTimeSpace
        {
            get { return _CanBespeatTimeSpace; }
            set { _CanBespeatTimeSpace = value; }
        }
        /// <summary>
        /// 是否使用预约功能
        /// </summary>
        public bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }
        /// <summary>
        /// 提前预约天数
        /// </summary>
        public double BespeakBeforeDays
        {
            get { return _BespeakBeforeDays; }
            set { _BespeakBeforeDays = value; }
        }
        /// <summary>
        /// 确定时间
        /// </summary>
        public TimeSpace ConfirmTime
        {
            get { return _ConfirmTime; }
            set { _ConfirmTime = value; }
        }
        /// <summary>
        /// 可预约的座位预约区域
        /// </summary>
        public SeatBespeakArea BespeakArea
        {
            get { return _BespeakArea; }
            set { _BespeakArea = value; }
        }

    }

    /// <summary>
    /// 可预约的区域
    /// </summary>
    [Serializable]
    public class SeatBespeakArea
    {
        private BespeakAreaType _BespeakArea = BespeakAreaType.Percentage;
        private double _Scale = 0.3;
        /// <summary>
        /// 预约区域的类型
        /// </summary>
        public BespeakAreaType BespeakType
        {
            get { return _BespeakArea; }
            set { _BespeakArea = value; }
        }
        /// <summary>
        /// 可预约座位的比例
        /// </summary>
        public double Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }
    }
    /// <summary>
    /// 限制读者进入设置
    /// </summary>
    [Serializable]
    public class LimitReaderEnterSet
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
    /// 参数错误异常
    /// </summary>
    [Serializable]
    public class ValueNotRightException : Exception
    {
        public ValueNotRightException(string message)
            : base(message)
        {
        }
    }
    /// <summary>
    /// 表示开始时间和结束时间的一段时间
    /// </summary>
    [Serializable]
    public class TimeSpace
    {
        string _BeginTime = "";
        string _EndTime = "";

        public TimeSpace()
        { }

        public TimeSpace(string beginTime, string endTime)
        {
            _BeginTime = beginTime;
            _EndTime = endTime;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime
        {
            get { return _BeginTime; }
            set { _BeginTime = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        public override string ToString()
        {
            return string.Format("BeginTime:{0},EndTime:{1}", _BeginTime, _EndTime);
        }
    }

    /// <summary>
    /// 黑名单限制区域
    /// </summary>
    //public enum RoomArea
    //{
    //    None=-1,
    //    /// <summary>
    //    /// 限制本阅览室黑名单记录
    //    /// </summary>
    //    OnelySelf = 0,
    //    /// <summary>
    //    /// 限制所有黑名单记录
    //    /// </summary>
    //    AllRoom = 1
    //}
    /// <summary>
    /// 可预约范围指定
    /// </summary>
    [Serializable]
    public enum BespeakAreaType
    {
        /// <summary>
        /// 指定可预约的座位
        /// </summary>
        AppointSeat = 0,
        /// <summary>
        /// 提供预约的百分比
        /// </summary>
        Percentage = 1
    }
    /// <summary>
    /// 黑名单设置
    /// </summary>
    [Serializable]
    public class ReadingRoomBlacklistSetting
    {
        private bool _Used = true;
        private Dictionary<ViolationRecordsType, bool> _ViolateRoule = new Dictionary<ViolationRecordsType, bool>();
        private int _ViolateTimes = 3;
        private LeaveBlacklistMode _LeaveBlacklist = LeaveBlacklistMode.AutomaticMode;
        private int _LimitDays = 7;
        private int _ViolateFailDays = 60;
        public ReadingRoomBlacklistSetting()
        {
            //初始化7种违规规则
            _ViolateRoule.Add(ViolationRecordsType.BookingTimeOut
                             , true);
            _ViolateRoule.Add(ViolationRecordsType.LeaveByAdmin
                            , true);
            _ViolateRoule.Add(ViolationRecordsType.SeatOutTime
                            , false);
            _ViolateRoule.Add(ViolationRecordsType.ShortLeaveByAdminOutTime
                            , true);
            _ViolateRoule.Add(ViolationRecordsType.ShortLeaveByReaderOutTime
                            , true);
            _ViolateRoule.Add(ViolationRecordsType.ShortLeaveByServiceOutTime
                            , false);
            _ViolateRoule.Add(ViolationRecordsType.ShortLeaveOutTime
                            , false);
            _ViolateRoule.Add(ViolationRecordsType.CancelWaitByAdmin
                            , false);
        }
        /// <summary>
        /// 违规规则，值为true说明需要记录违规
        /// </summary>
        public Dictionary<ViolationRecordsType, bool> ViolateRoule
        {
            get { return _ViolateRoule; }
            set { _ViolateRoule = value; }
        }
        /// <summary>
        /// 是否使用黑名单设置
        /// </summary>
        public bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }

        /// <summary>
        /// 违规次数，超过该次数加到黑名单
        /// </summary>
        public int ViolateTimes
        {
            get { return _ViolateTimes; }
            set { _ViolateTimes = value; }
        }
        /// <summary>
        /// 离开黑名单方式
        /// </summary>
        public LeaveBlacklistMode LeaveBlacklist
        {
            get { return _LeaveBlacklist; }
            set { _LeaveBlacklist = value; }
        }
        /// <summary>
        /// 限制天数
        /// </summary>
        public int LimitDays
        {
            get { return _LimitDays; }
            set { _LimitDays = value; }
        }
        /// <summary>
        /// 违规失效天数
        /// </summary>
        public int ViolateFailDays
        {
            get { return _ViolateFailDays; }
            set { _ViolateFailDays = value; }
        }

    }

}
