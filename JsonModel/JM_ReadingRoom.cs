using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_ReadingRoom
    {
        string roomNum;
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string RoomNum
        {
            get { return roomNum; }
            set { roomNum = value; }
        }
        string roomName;
        /// <summary>
        /// 阅览室名字
        /// </summary>
        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }

        private string libraryName;
        /// <summary>
        /// 所属图书馆名称
        /// </summary>
        public string LibraryName
        {
            get { return libraryName; }
            set { libraryName = value; }
        }
        private string areaName;
        /// <summary>
        /// 所属区域名称
        /// </summary>
        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }
        private string schoolName;
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName
        {
            get { return schoolName; }
            set { schoolName = value; }
        } 

        JM_RoomSet roomSet;
        /// <summary>
        /// 阅览室设置
        /// </summary>
        public JM_RoomSet RoomSet
        {
            get { return roomSet; }
            set { roomSet = value; }
        }
    }
    /// <summary>
    /// 被预约的阅览室
    /// </summary>
    public class JM_OpenBespeakReadingRoom : JM_ReadingRoom
    { 
        private string defaultBespeakTime;
        /// <summary>
        /// 图书馆默认的预约时间
        /// </summary>
        public string DefaultBespeakTime
        {
            get { return defaultBespeakTime; }
            set { defaultBespeakTime = value; }
        }

        private bool canBespeak;
        /// <summary>
        /// 是否可以被预约
        /// </summary>
        public bool CanBespeak
        {
            get { return canBespeak; }
            set { canBespeak = value; }
        }

        private int _surplusSeatCount;
        /// <summary>
        /// 剩余可预约的座位数
        /// </summary>
        public int SurplusSeatCount
        {
            get { return _surplusSeatCount; }
            set { _surplusSeatCount = value; }
        }
         
        private int canBespeakSeatCount;
        /// <summary>
        /// 可以预约的座位总数
        /// </summary>
        public int CanBespeakSeatCount
        {
            get { return canBespeakSeatCount; }
            set { canBespeakSeatCount = value; }
        }

        private int bespeakedCount;
        /// <summary>
        /// 可以预约的座位总数
        /// </summary>
        public int BespeakedCount
        {
            get { return bespeakedCount; }
            set { bespeakedCount = value; }
        }

        private string remark;
        /// <summary>
        /// 备注，如果有提示信息可以通过该字段传递
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

    }
}
