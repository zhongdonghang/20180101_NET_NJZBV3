/*******************************************
 * 作者：王昊天
 * 创建时间：2013-5-22
 * 说明：阅览室开闭馆计划实体
 * 修改人：
 * 修改日期：
 * *****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 阅览室开闭馆计划实体
    /// </summary>
    [Serializable]
    public class ReadingRoomOpenCloseLogInfo
    {
        private string _ID = "-1";
        /// <summary>
        /// 记录ID
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _ReadingRoomNo = "";
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string ReadingRoomNo
        {
            get { return _ReadingRoomNo; }
            set { _ReadingRoomNo = value; }
        }

        private DateTime _OperateTime = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            get { return _OperateTime; }
            set { _OperateTime = value; }
        }

        private string _OperateNo = "";
        /// <summary>
        /// 操作编号
        /// </summary>
        public string OperateNo
        {
            get { return _OperateNo; }
            set { _OperateNo = value; }
        }

        private ReadingRoomStatus _OpenCloseState = ReadingRoomStatus.None;
        /// <summary>
        /// 开闭馆状态
        /// </summary>
        public ReadingRoomStatus OpenCloseState
        {
            get { return _OpenCloseState; }
            set { _OpenCloseState = value; }
        }

        private LogStatus _Logstatus = LogStatus.Valid;
        /// <summary>
        /// 标示记录是否有效
        /// </summary>
        public LogStatus Logstatus
        {
            get { return _Logstatus; }
            set { _Logstatus = value; }
        }
    }
}
