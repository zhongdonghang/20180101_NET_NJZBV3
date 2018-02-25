using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_NowRoomSet
    {
        private string selectSelectModel;
        /// <summary>
        /// 选座模式
        /// </summary>
        public string SelectSelectModel
        {
            get { return selectSelectModel; }
            set { selectSelectModel = value; }
        }
        private string nowRoomOpenTime;
        /// <summary>
        /// 当前阅览室开放时间
        /// </summary>
        public string NowRoomOpenTime
        {
            get { return nowRoomOpenTime; }
            set { nowRoomOpenTime = value; }
        }
        private string nowRoomCloseTime;
        /// <summary>
        /// 当前阅览室关闭时间
        /// </summary>
        public string NowRoomCloseTime
        {
            get { return nowRoomCloseTime; }
            set { nowRoomCloseTime = value; }
        }
        private int shortLeaveHoldTime;
        /// <summary>
        /// 当前阅览室
        /// </summary>
        public int ShortLeaveHoldTime
        {
            get { return shortLeaveHoldTime; }
            set { shortLeaveHoldTime = value; }
        }
        private bool isCanBespeakSeat;
        /// <summary>
        /// 是否可以预约座位
        /// </summary>
        public bool IsCanBespeakSeat
        {
            get { return isCanBespeakSeat; }
            set { isCanBespeakSeat = value; }
        }
        private bool isCanBespeakNowSeat;
        /// <summary>
        /// 是否可以预约当前座位
        /// </summary>
        public bool IsCanBespeakNowSeat
        {
            get { return isCanBespeakNowSeat; }
            set { isCanBespeakNowSeat = value; }
        }
        private bool isCanWaitSeat;
        /// <summary>
        /// 是否可以等待座位
        /// </summary>
        public bool IsCanWaitSeat
        {
            get { return isCanWaitSeat; }
            set { isCanWaitSeat = value; }
        }
        private bool isCanContinueTime;
        /// <summary>
        /// 是否可以续时
        /// </summary>
        public bool IsCanContinueTime
        {
            get { return isCanContinueTime; }
            set { isCanContinueTime = value; }
        }
        private string nowState;
        /// <summary>
        /// 阅览室当前状态
        /// </summary>
        public string NowState
        {
            get { return nowState; }
            set { nowState = value; }
        }
        private bool isCanSelectBespeakSeat;
        /// <summary>
        /// 是否可以选择被预约的座位
        /// </summary>
        public bool IsCanSelectBespeakSeat
        {
            get { return isCanSelectBespeakSeat; }
            set { isCanSelectBespeakSeat = value; }
        }
        private bool isLimitBlacklist;
        /// <summary>
        /// 是否限制黑名单用户
        /// </summary>
        public bool IsLimitBlacklist
        {
            get { return isLimitBlacklist; }
            set { isLimitBlacklist = value; }
        }
        private bool isLimitReaderType;
        /// <summary>
        /// 是否限制用户类型进入
        /// </summary>
        public bool IsLimitReaderType
        {
            get { return isLimitReaderType; }
            set { isLimitReaderType = value; }
        }
        private bool isEnterOnly;
        /// <summary>
        /// 是否允许进入
        /// </summary>
        public bool IsEnterOnly
        {
            get { return isEnterOnly; }
            set { isEnterOnly = value; }
        }
        private string limitType;
        /// <summary>
        /// 被限制或者允许进入的用户类型
        /// </summary>
        public string LimitType
        {
            get { return limitType; }
            set { limitType = value; }
        }
    }
}
