using System;
using System.ComponentModel;

namespace SeatClientV3.ViewModel
{

    public class ReadingRoomUC_ViewModel : INotifyPropertyChanged
    {
        private string _ReadingRoomNo = "";
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string ReadingRoomNo
        {
            get { return _ReadingRoomNo; }
            set { _ReadingRoomNo = value; Changed("ReadingRoomNo"); }
        }
        private string _ReadingRoomName = "";
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; Changed("ReadingRoomName"); }
        }
        private int _AllSeatCount = 0;
        /// <summary>
        /// 总座位数
        /// </summary>
        public int AllSeatCount
        {
            get { return _AllSeatCount; }
            set { _AllSeatCount = value; Changed("UseStatusInfo"); }
        }
        private int _UsedSeatCount = 0;
        /// <summary>
        /// 剩余座位数
        /// </summary>
        public int UsedSeatCount
        {
            get { return _UsedSeatCount; }
            set { _UsedSeatCount = value; Changed("UseStatusInfo"); }
        }
        private int _BookingSeatCount = 0;
        /// <summary>
        /// 预约座位数目
        /// </summary>
        public int BookingSeatCount
        {
            get { return _BookingSeatCount; }
            set { _BookingSeatCount = value; Changed("UseStatusInfo"); }
        }
        private bool _IsBook = false;
        /// <summary>
        /// 是否开启预约
        /// </summary>
        public bool IsBook
        {
            get { return _IsBook; }
            set { _IsBook = value; Changed("UseStatusInfo"); }
        }
        /// <summary>
        /// 使用情况
        /// </summary>
        public string UseStatusInfo
        {
            get
            {
                if (_IsBook)
                {
                    return _UsedSeatCount + "/" + _BookingSeatCount + "/" + _AllSeatCount;
                }
                else
                {
                    return _UsedSeatCount + "/" + _AllSeatCount;
                }
            }
        }
        private SeatManage.EnumType.ReadingRoomUsingStatus _Usage = SeatManage.EnumType.ReadingRoomUsingStatus.None;
        /// <summary>
        /// 使用状态
        /// </summary>
        public SeatManage.EnumType.ReadingRoomUsingStatus Usage
        {
            get { return _Usage; }
            set
            {
                if (_Usage != value)
                {
                    _Usage = value;
                    Changed("Usage");
                    if (UsageStateChange != null)
                    {
                        UsageStateChange(this, new EventArgs());
                    }
                }

            }
        }
        private SeatManage.EnumType.ReadingRoomStatus _Status = SeatManage.EnumType.ReadingRoomStatus.None;
        /// <summary>
        /// 开闭馆状态
        /// </summary>
        public SeatManage.EnumType.ReadingRoomStatus Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    Changed("Status");
                    if (UsageStateChange != null)
                    {
                        UsageStateChange(this, new EventArgs());
                    }
                }
            }
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

        public event EventHandler UsageStateChange;
    }
}
