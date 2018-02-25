using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_WeiXinUserInfo
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 读者类型
        /// </summary>
        public string ReaderType { get; set; }

        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNo { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolNanme { get; set; }
        /// <summary>
        /// 读者当前状态，有效进出记录，有效等待记录，有效预约记录
        /// </summary>
        public AJM_ReaderStatus AjmReaderStatus
        {
            get { return _ajmReaderStatus; }
            set { _ajmReaderStatus = value; }
        }
        /// <summary>
        /// 阅览室状态
        /// </summary>
        public AJM_ReadingRoomState AjmReadingRoomState
        {
            get { return _ajmReadingRoomState; }
            set { _ajmReadingRoomState = value; }
        }
        /// <summary>
        /// 预约网站设置
        /// </summary>
        public AJM_PecketBookSetting AjmPecketBookSetting
        {
            get { return _ajmPecketBookSetting; }
            set { _ajmPecketBookSetting = value; }
        }
        /// <summary>
        /// 黑名单记录
        /// </summary>
        public AJM_BlacklistLog AjmBlacklistLog
        {
            get { return _ajmBlacklistLog; }
            set { _ajmBlacklistLog = value; }
        }
        /// <summary>
        /// 违规记录
        /// </summary>
        public AJM_ViolationRecordsLogInfo AjmViolationRecordsLogInfo
        {
            get { return _ajmViolationRecordsLogInfo; }
            set { _ajmViolationRecordsLogInfo = value; }
        }
        /// <summary>
        /// 预约记录
        /// </summary>
        public AJM_BespeakLog AjmBespeakLog
        {
            get { return _ajmBespeakLog; }
            set { _ajmBespeakLog = value; }
        }
        /// <summary>
        /// 进出记录
        /// </summary>
        public AJM_EnterOutLog AjmEnterOutLog
        {
            get { return _ajmEnterOutLog; }
            set { _ajmEnterOutLog = value; }
        }

        AJM_ReaderStatus _ajmReaderStatus = new AJM_ReaderStatus();
        AJM_ReadingRoomState _ajmReadingRoomState=new AJM_ReadingRoomState();
        AJM_PecketBookSetting _ajmPecketBookSetting = new AJM_PecketBookSetting();
        AJM_BlacklistLog _ajmBlacklistLog=new AJM_BlacklistLog();
        AJM_ViolationRecordsLogInfo _ajmViolationRecordsLogInfo=new AJM_ViolationRecordsLogInfo();
        AJM_BespeakLog _ajmBespeakLog=new AJM_BespeakLog();
        AJM_EnterOutLog _ajmEnterOutLog=new AJM_EnterOutLog();
    }
}
