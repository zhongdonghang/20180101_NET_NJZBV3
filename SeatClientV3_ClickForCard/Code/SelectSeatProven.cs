using SeatManage.ClassModel;
using SeatManage.EnumType;
using System;
using System.Linq;
using SeatClientV3.WindowObject;

namespace SeatClientV3.Code
{
    /// <summary>
    /// 选座验证
    /// </summary>
    public class SelectSeatProven
    {
        /// <summary>
        /// 选座验证操作。
        /// </summary>
        /// <param name="student">学生信息</param>
        /// <param name="room">选择的阅览室</param>
        /// <param name="clientconfig">客户端设置</param>
        /// <returns></returns>
        public static bool ProvenReaderState(ReaderInfo student, ReadingRoomInfo room, BlacklistSetting blacklistSet, ClientConfigV2 clientconfig)
        {
            if (SeatManage.Bll.EnterOutOperate.CheckReaderChooseSeatTimes(student.CardNo, clientconfig.PosTimes))
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SelectSeatFrequent);
                return true;
            }
            if (SeatManage.Bll.EnterOutOperate.CheckReaderChooseSeatTimesByReadingRoom(student.CardNo, room.Setting.PosTimes, room.No))
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SelectSeatFrequent);
                return true;
            }
            if (!room.Setting.UsedBlacklistLimit)
            {
                return false;
            }
            if (student.BlacklistLog.Count <= 0)
            {
                return false;
            }
            if (!room.Setting.BlackListSetting.Used)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.IsBlacklist);
                return true;
            }
            bool isBlack = student.BlacklistLog.Any(blinfo => blinfo.ReadingRoomID == room.No);
            if (isBlack)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.IsBlacklist);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 选座方式验证
        /// </summary>
        /// <param name="clientset">终端选座方式</param>
        /// <param name="roomSelectSeatSet">阅览室设置的选座方式</param>
        /// <returns></returns>
        public static SelectSeatMode ProvenSelectSeatMethod(ClientConfigV2 clientset, SeatChooseMethodSet roomSelectSeatSet)
        {
            return clientset.SelectMethod == SelectSeatMode.Default ? SeatManage.Bll.NowReadingRoomState.RoomSelectSeatMode(roomSelectSeatSet, SeatManage.Bll.ServiceDateTime.Now) : clientset.SelectMethod;
        }

        /// <summary>
        /// 验证读者类型
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="roomSet"></param>
        /// <returns></returns>
        public static bool ProvenReaderType(ReaderInfo reader, ReadingRoomSetting roomSet)
        {
            if (!roomSet.LimitReaderEnter.Used)
            {
                return true;
            }
            string[] readerTypes = roomSet.LimitReaderEnter.ReaderTypes.Split(';');
            if (readerTypes.Any(t => reader.ReaderType == t))
            {
                return roomSet.LimitReaderEnter.CanEnter;
            }
            return !roomSet.LimitReaderEnter.CanEnter;
        }

        /// <summary>
        /// 验证房间号时候在本触摸屏所管理的阅览室里
        /// </summary>
        /// <param name="roomNum"></param>
        /// <param name="clientconfig"></param>
        /// <returns></returns>
        public static bool CheckReadingRoomInThisClient(string roomNum, ClientConfig clientconfig)
        {
            return clientconfig.Rooms.Any(t => clientconfig.Rooms[0] == roomNum);
        }
    }
}
