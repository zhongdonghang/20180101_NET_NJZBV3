using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace LastSeatClient.ViewModel
{
    public class VM_UC_LibStatus : VM_BasicModel
    {
        /// <summary>
        /// 阅览室名称
        /// </summary>
        private string _RoomName = "";
        /// <summary>
        /// 阅览室编号
        /// </summary>
        private string _RoomNo = "";
        /// <summary>
        /// 总座位数
        /// </summary>
        private int _AllSeatCount = 0;
        /// <summary>
        /// 使用数
        /// </summary>
        private int _UsingCount = 0;
        /// <summary>
        /// 预约数
        /// </summary>
        private int _BookingCount = 0;
        /// <summary>
        /// 开闭状态
        /// </summary>
        private ReadingRoomStatus _RoomStatus = ReadingRoomStatus.Open;


        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; Changed("RoomName"); }
        }

        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string RoomNo
        {
            get { return _RoomNo; }
            set { _RoomNo = value; Changed("RoomNo"); }
        }

        /// <summary>
        /// 总座位数
        /// </summary>
        public int AllSeatCount
        {
            get { return _AllSeatCount; }
            set { _AllSeatCount = value; Changed("UsingInfoText"); Changed("StatusAngle"); Changed("ArcColor"); }
        }

        /// <summary>
        /// 使用数
        /// </summary>
        public int UsingCount
        {
            get { return _UsingCount; }
            set { _UsingCount = value; Changed("UsingInfoText"); Changed("StatusAngle"); Changed("ArcColor"); }
        }

        /// <summary>
        /// 预约数
        /// </summary>
        public int BookingCount
        {
            get { return _BookingCount; }
            set { _BookingCount = value; Changed("UsingInfoText"); Changed("StatusAngle"); Changed("ArcColor"); }
        }

        /// <summary>
        /// 使用状态
        /// </summary>
        public ReadingRoomUsingStatus UsingStatus
        {
            get { return RoomSeatUsingState; }
        }

        /// <summary>
        /// 开闭状态
        /// </summary>
        public ReadingRoomStatus RoomStatus
        {
            get { return _RoomStatus; }
            set { _RoomStatus = value; Changed("RoomNameColor"); Changed("UsingInfoColor"); Changed("ArcColor"); }
        }
        /// <summary>
        /// 使用数目
        /// </summary>
        public string UsingInfoText
        {
            get { return "使用：" + _UsingCount + "/预约：" + _BookingCount + "/总数：" + _AllSeatCount; }
        }
        /// <summary>
        /// 显示角度
        /// </summary>
        public int StatusAngle
        {
            get { return _AllSeatCount == 0 ? 135 : (int)((double)_UsingCount / (double)_AllSeatCount * 265 + 135); }
        }
        /// <summary>
        /// 圆环的颜色
        /// </summary>
        public string ArcColor
        {
            get
            {
                //#FF8B8B8B灰色 #FF24D61C绿色 #FFD30505红色 #FFE6A822黄色
                if (_RoomStatus == ReadingRoomStatus.BeforeClose || _RoomStatus == ReadingRoomStatus.Close)
                {
                    return "#FF8B8B8B";
                }
                else
                {
                    switch (RoomSeatUsingState)
                    {
                        case ReadingRoomUsingStatus.Normal: return "#FF128512";
                        case ReadingRoomUsingStatus.Crowd: return "#FFE6A822";
                        case ReadingRoomUsingStatus.Full: return "#FFD30505";
                        default: return "#FF8B8B8B";
                    }
                }
            }
        }
        /// <summary>
        /// 阅览室名称底色
        /// </summary>
        public string RoomNameColor
        {
            get
            {
                //#FF8B8B8B灰色 #FF24D61C绿色 #FFD30505红色 #FFE6A822黄色
                return _RoomStatus == ReadingRoomStatus.BeforeClose || _RoomStatus == ReadingRoomStatus.Close ? "#FF8B8B8B" : "#FF5C6E9F";
            }
        }
        /// <summary>
        /// 阅览室使用情况底色
        /// </summary>
        public string UsingInfoColor
        {
            get
            {
                //#FF8B8B8B灰色 #FF24D61C绿色 #FFD30505红色 #FFE6A822黄色
                return _RoomStatus == ReadingRoomStatus.BeforeClose || _RoomStatus == ReadingRoomStatus.Close ? "#FF8B8B8B" : "#FF828FB5";
            }
        }

        /// <summary>
        /// 阅览室座位使用状态
        /// </summary>
        public ReadingRoomUsingStatus RoomSeatUsingState
        {
            get
            {
                if (_AllSeatCount == 0)
                {
                    return ReadingRoomUsingStatus.Full;
                }
                double state = double.Parse(_UsingCount.ToString()) / double.Parse(_AllSeatCount.ToString());
                if (state < 0.6)
                    return ReadingRoomUsingStatus.Normal;
                if (state < 1)
                    return ReadingRoomUsingStatus.Crowd;
                if (state == 1)
                    return ReadingRoomUsingStatus.Full;
                return ReadingRoomUsingStatus.Normal;
            }
        }
    }
}
