using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.Model.Enum;

namespace AdvertManage.Model
{
    public class AMS_EnterOutLog
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 学校Id
        /// </summary>
        public int SchoolId
        {
            get;
            set;
        }
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNum
        {
            get;
            set;
        }
        /// <summary>
        /// 读者学号
        /// </summary>
        public string CardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 进出记录编号
        /// </summary>
        public string EnterOutNo
        {
            get;
            set;
        }
        /// <summary>
        /// 记录状态
        /// </summary>
        public Model.Enum.EnterOutLogType EnterOutState
        {
            get;
            set;
        }
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get;
            set;
        }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime EnterOutTime
        {
            get;
            set;
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public Operation Operator
        {
            get;
            set;
        }
        /// <summary>
        /// 记录有效状态
        /// </summary>
        public Model.Enum.LogStatus EnterOutType
        {
            get;
            set;
        }
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string ReadingRoomNum
        {
            get;
            set;
        }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string TerminalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 记录备注
        /// </summary>
        public string Remark
        { get; set; }
    }
}
