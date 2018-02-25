using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
namespace SeatConfig.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        ObservableCollection<RoomPosition> _ReadingRoomPostion = new ObservableCollection<RoomPosition>();
        /// <summary>
        /// 阅览室方向
        /// </summary>
        public ObservableCollection<RoomPosition> ReadingRoomPostion
        {
            get { return _ReadingRoomPostion; }
            set { _ReadingRoomPostion = value; Changed("ReadingRoomPostion"); }
        }

        ObservableCollection<SeatManage.ClassModel.ReadingRoomInfo> _readingRooms = new ObservableCollection<SeatManage.ClassModel.ReadingRoomInfo>();
        /// <summary>
        /// 阅览室列表
        /// </summary>
        public ObservableCollection<SeatManage.ClassModel.ReadingRoomInfo> ReadingRoom
        {
            get { return _readingRooms; }
            set { _readingRooms = value; Changed("ReadingRoom"); }
        }
        RoomPosition _Position = new RoomPosition();
        /// <summary>
        /// 方位
        /// </summary>
        public RoomPosition Position
        {
            get { return _Position; }
            set { _Position = value; Changed("Position"); }
        }

        SeatManage.ClassModel.ReadingRoomInfo _Room = new SeatManage.ClassModel.ReadingRoomInfo();

        /// <summary>
        /// 阅览室
        /// </summary>
        public SeatManage.ClassModel.ReadingRoomInfo Room
        {
            get { return _Room; }
            set { _Room = value; Changed("Room"); }
        }
        /// <summary>
        /// 获取所有的阅览室列表
        /// </summary>
        public void GetReadingRooms()
        {
            SeatManage.Bll.ReadingRoomOperate readingRoomBll = new SeatManage.Bll.ReadingRoomOperate();
            List<SeatManage.ClassModel.ReadingRoomInfo> rooms = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, null, null);
            this.ReadingRoom.Clear();
            for (int i = 0; i < rooms.Count; i++)
            {
                if (this.ReadingRoom.Count == 0)
                {
                    this.ReadingRoom.Add(new SeatManage.ClassModel.ReadingRoomInfo { No = "", Name = "请选择", SeatList = new SeatManage.ClassModel.SeatLayout() });
                }
                this.ReadingRoom.Add(rooms[i]);
            }
            ReadingRoomPostion.Add(new RoomPosition { PositionName = "请选择", PositionValue = ReadingRoomPosition.None });
            ReadingRoomPostion.Add(new RoomPosition { PositionName = "东", PositionValue = ReadingRoomPosition.East });
            ReadingRoomPostion.Add(new RoomPosition { PositionName = "西", PositionValue = ReadingRoomPosition.West });
            ReadingRoomPostion.Add(new RoomPosition { PositionName = "南", PositionValue = ReadingRoomPosition.South });
            ReadingRoomPostion.Add(new RoomPosition { PositionName = "北", PositionValue = ReadingRoomPosition.North });
        }
        /// <summary>
        /// 根据ID查找阅览室
        /// </summary>
        /// <param name="readingRoomId"></param>
        public void FindReadingRoom(string readingRoomId)
        {
            Room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(readingRoomId);
        }
        public void UpdateSeat()
        {
            //TODO:更新座位         /**********测试************/
            SeatManage.Bll.T_SM_ReadingRoom.UpdateSeatLayout(Room.SeatList);
        }


        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
    /// <summary>
    /// 阅览室方向
    /// </summary>
    public class RoomPosition
    {
        private string _PositionName;
        /// <summary>
        /// 方位名
        /// </summary>
        public string PositionName
        {
            get { return _PositionName; }
            set { _PositionName = value; }
        }
        private ReadingRoomPosition _PositionValue;
        /// <summary>
        /// 方位枚举
        /// </summary>
        public ReadingRoomPosition PositionValue
        {
            get { return _PositionValue; }
            set { _PositionValue = value; }
        }
    }
    /// <summary>
    /// 方位枚举
    /// </summary>
    public enum ReadingRoomPosition
    {
        /// <summary>
        /// 空
        /// </summary>
        None = -1,
        /// <summary>
        /// 东
        /// </summary>
        East = 0,
        /// <summary>
        /// 西
        /// </summary>
        West = 1,
        /// <summary>
        /// 南
        /// </summary>
        South = 2,
        /// <summary>
        /// 北
        /// </summary>
        North = 3,
    }
}
