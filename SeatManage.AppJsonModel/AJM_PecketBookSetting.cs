using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_PecketBookSetting
    {
        private bool _useShortLeave = true;
        private bool _useComeBack = false;
        private bool _useCanLeave = true;
        private bool _useContinue = false;
        private bool _useWaitSeat = false;
        private bool _useCancelWait = false;
        private bool _useBookComfirm = false;
        private bool _useBookSeat = true;
        private bool _useBookNowDaySeat = true;
        private bool _useBookNextDaySeat = true;
        private bool _useCancelBook = true;
        private bool _useSelectSeat = false;
        private bool _useChangeSeat = true;
        private bool _useSelectLog = true;
        /// <summary>
        /// 允许用户设置座位暂离
        /// </summary>
        public bool UseShortLeave
        {
            get { return _useShortLeave; }
            set { _useShortLeave = value; }
        }
        /// <summary>
        /// 允许用户暂离回来
        /// </summary>
        public bool UseComeBack
        {
            get { return _useComeBack; }
            set { _useComeBack = value; }
        }
        /// <summary>
        /// 允许用户释放座位
        /// </summary>
        public bool UseCanLeave
        {
            get { return _useCanLeave; }
            set { _useCanLeave = value; }
        }
        /// <summary>
        /// 允许用户续时
        /// </summary>
        public bool UseContinue
        {
            get { return _useContinue; }
            set { _useContinue = value; }
        }
        /// <summary>
        /// 允许用户等待座位
        /// </summary>
        public bool UseWaitSeat
        {
            get { return _useWaitSeat; }
            set { _useWaitSeat = value; }
        }
        /// <summary>
        /// 允许用户取消座位等待
        /// </summary>
        public bool UseCancelWait
        {
            get { return _useCancelWait; }
            set { _useCancelWait = value; }
        }
        /// <summary>
        /// 允许用户预约签到
        /// </summary>
        public bool UseBookComfirm
        {
            get { return _useBookComfirm; }
            set { _useBookComfirm = value; }
        }
        /// <summary>
        /// 允许用户预约座位
        /// </summary>
        public bool UseBookSeat
        {
            get { return _useBookSeat; }
            set { _useBookSeat = value; }
        }
        /// <summary>
        /// 允许用户预约当天座位
        /// </summary>
        public bool UseBookNowDaySeat
        {
            get { return _useBookNowDaySeat; }
            set { _useBookNowDaySeat = value; }
        }
        /// <summary>
        /// 允许用户预约隔天座位
        /// </summary>
        public bool UseBookNextDaySeat
        {
            get { return _useBookNextDaySeat; }
            set { _useBookNextDaySeat = value; }
        }
        /// <summary>
        /// 允许用户取消预约
        /// </summary>
        public bool UseCancelBook
        {
            get { return _useCancelBook; }
            set { _useCancelBook = value; }
        }
        /// <summary>
        /// 允许用户选择座位
        /// </summary>
        public bool UseSelectSeat
        {
            get { return _useSelectSeat; }
            set { _useSelectSeat = value; }
        }
        /// <summary>
        /// 允许用户更换座位
        /// </summary>
        public bool UseChangeSeat
        {
            get { return _useChangeSeat; }
            set { _useChangeSeat = value; }
        }
    }
}
