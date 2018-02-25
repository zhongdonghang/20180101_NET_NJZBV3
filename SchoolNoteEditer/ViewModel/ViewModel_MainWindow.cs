using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_MainWindow : INotifyPropertyChanged
    {
        ObservableCollection<ViewModel.ViewModel_ReadingRoom> _ReadingRooms = new ObservableCollection<ViewModel.ViewModel_ReadingRoom>();
        /// <summary>
        /// 阅览室列表
        /// </summary>
        public ObservableCollection<ViewModel.ViewModel_ReadingRoom> ReadingRooms
        {
            get { return _ReadingRooms; }
            set { _ReadingRooms = value; Changed("ReadingRooms"); }
        }
        ViewModel.ViewModel_ReadingRoom _room;
        /// <summary>
        /// 正在编辑的阅览室
        /// </summary>
        public ViewModel.ViewModel_ReadingRoom Room
        {
            get { return _room; }
            set { _room = value; Changed("Room"); }
        }

        private string _ErrorMessage = "";
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; Changed("ErrorMessage"); }
        }
        ObservableCollection<RoomPosition> _ReadingRoomPostion = new ObservableCollection<RoomPosition>();
        /// <summary>
        /// 阅览室方向
        /// </summary>
        public ObservableCollection<RoomPosition> ReadingRoomPostion
        {
            get { return _ReadingRoomPostion; }
            set { _ReadingRoomPostion = value; Changed("ReadingRoomPostion"); }
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
        #region INotifyPropertyChanged 成员
        public void GetData()
        {
            try
            {
                ReadingRoomPostion.Clear();
                ReadingRoomPostion.Add(new RoomPosition { PositionName = "请选择", PositionValue = ReadingRoomPosition.None });
                ReadingRoomPostion.Add(new RoomPosition { PositionName = "东", PositionValue = ReadingRoomPosition.East });
                ReadingRoomPostion.Add(new RoomPosition { PositionName = "西", PositionValue = ReadingRoomPosition.West });
                ReadingRoomPostion.Add(new RoomPosition { PositionName = "南", PositionValue = ReadingRoomPosition.South });
                ReadingRoomPostion.Add(new RoomPosition { PositionName = "北", PositionValue = ReadingRoomPosition.North });
                ReadingRooms.Clear();
                ObservableCollection<ViewModel.ViewModel_ReadingRoom> ReadingRoomList = SchoolNoteEditer.Code.ReadingRoomEdit.GetReadingRooms();
                ReadingRooms.Add(new ViewModel.ViewModel_ReadingRoom() { No = "0", Name = "请选择" });
                foreach (ViewModel.ViewModel_ReadingRoom vm in ReadingRoomList)
                {
                    ReadingRooms.Add(vm);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        public void Save()
        {
            if (SeatManage.Bll.T_SM_ReadingRoom.UpdateSeatLayout(Room.ReadingRoomModel.SeatList) == SeatManage.EnumType.HandleResult.Failed)
            {
                ErrorMessage = "保存失败！";
            }
            else
            {
                ErrorMessage = "保存成功！";
            }
        }

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
