using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;
using SeatClientV3.FunWindow;

namespace SeatClientV3.Code
{
     ///<summary>
     ///选座验证
     ///</summary>
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
                MessageWindow selectTooWindow = new MessageWindow(MessageType.SelectSeatFrequent);
                selectTooWindow.ShowDialog();
                return true;
            }
            if (SeatManage.Bll.EnterOutOperate.CheckReaderChooseSeatTimesByReadingRoom(student.CardNo, room.Setting.PosTimes, room.No))
            {
                MessageWindow selectTooWindow = new MessageWindow(MessageType.SelectSeatFrequent);
                selectTooWindow.ShowDialog();
                return true;
            }


            if (room.Setting.UsedBlacklistLimit)
            {
                if (student.BlacklistLog.Count > 0)
                {
                    if (room.Setting.BlackListSetting.Used)
                    {
                        bool isBlack = false;
                        foreach (BlackListInfo blinfo in student.BlacklistLog)
                        {
                            if (blinfo.ReadingRoomID == room.No)
                            {
                                isBlack = true;
                                break;
                            }
                        }
                        if (isBlack)
                        {
                            MessageWindow blacklistWindow = new MessageWindow(MessageType.RoomBlacklist);
                            blacklistWindow.ShowDialog();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        MessageWindow blacklistWindow = new MessageWindow(MessageType.RoomBlacklist);
                        blacklistWindow.ShowDialog();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }
        /// <summary>
        /// 选座方式验证
        /// </summary>
        /// <param name="clientset">终端选座方式</param>
        /// <param name="roomSelectSeatSet">阅览室设置的选座方式</param>
        /// <returns></returns>
        public static SelectSeatMode ProvenSelectSeatMethod(ClientConfigV2 clientset, SeatChooseMethodSet roomSelectSeatSet)
        {
            SelectSeatMode method = SelectSeatMode.OptionalMode;
            if (clientset.SelectMethod == SelectSeatMode.Default)
            {
                method = NowReadingRoomState.RoomSelectSeatMode(roomSelectSeatSet, SeatManage.Bll.ServiceDateTime.Now);
            }
            else
            {
                method = clientset.SelectMethod;
            }
            return method;
        }
        /// <summary>
        /// 验证座位状态
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        public static bool ProvenSeatState(string seatNum)
        {
            throw new NotImplementedException();

        }
        /// <summary>
        /// 验证读者类型
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="roomSet"></param>
        /// <returns></returns>
        public static bool ProvenReaderType(ReaderInfo reader, ReadingRoomSetting roomSet)
        {
            if (roomSet.LimitReaderEnter.Used)
            {
                string[] readerTypes = roomSet.LimitReaderEnter.ReaderTypes.Split(';');
                for (int i = 0; i < readerTypes.Length; i++)
                {
                    if (reader.ReaderType == readerTypes[i])//如果读者类型和限制的类型一致，则返回该类型的选择权限。
                    {
                        return roomSet.LimitReaderEnter.CanEnter;
                    }
                }
                return !roomSet.LimitReaderEnter.CanEnter;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 验证房间号时候在本触摸屏所管理的阅览室里
        /// </summary>
        /// <param name="roomNum"></param>
        /// <param name="clientconfig"></param>
        /// <returns></returns>
        public static bool CheckReadingRoomInThisClient(string roomNum, ClientConfig clientconfig)
        {
            bool isExists = false;
            for (int i = 0; i < clientconfig.Rooms.Count; i++)
            {
                if (clientconfig.Rooms[0] == roomNum)
                {
                    isExists = true;
                    break;
                }
            }
            return isExists;
        }
    }
}
