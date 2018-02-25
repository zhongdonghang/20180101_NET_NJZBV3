using System.ComponentModel;

namespace SeatClientV3.ViewModel
{
    public class UC_Tip_ViewModel : INotifyPropertyChanged
    {

        #region 属性
        private string _TitleMessage = "";
        /// <summary>
        /// 消息标题
        /// </summary>
        public string TitleMessage
        {
            get { return _TitleMessage; }
            set { _TitleMessage = value; OnPropertyChanged("TitleMessage"); }
        }

        private string _ShowMessage = "";
        /// <summary>
        /// 消息内容内容
        /// </summary>
        public string ShowMessage
        {
            get { return _ShowMessage; }
            set { _ShowMessage = value; OnPropertyChanged("ShowMessage"); }
        }

        private string _ReadingRoomName = "";
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; OnPropertyChanged("ReadingRoomName"); }
        }

        private string _SeatNo = "";
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get { return _SeatNo; }
            set { _SeatNo = value; OnPropertyChanged("SeatNo"); }
        }

        private string _StartTime = "";
        /// <summary>
        /// 开始时段
        /// </summary>
        public string StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; OnPropertyChanged("StartTime"); }
        }
        private string _EndTime = "";
        /// <summary>
        /// 结束时段
        /// </summary>
        public string EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; OnPropertyChanged("EndTime"); }
        }
        private string _SingleTime = "";
        /// <summary>
        /// 独立的时间
        /// </summary>
        public string SingleTime
        {
            get { return _SingleTime; }
            set { _SingleTime = value; OnPropertyChanged("SingleTime"); }
        }
        private string _LastCount = "N";
        /// <summary>
        /// 剩余次数/分钟数/天数
        /// </summary>
        public string LastCount
        {
            get { return _LastCount; }
            set { _LastCount = value; OnPropertyChanged("LastCount"); }
        }

        private string _ReaderType = "";
        /// <summary>
        /// 读者类型
        /// </summary>
        public string ReaderType
        {
            get { return _ReaderType; }
            set { _ReaderType = value; OnPropertyChanged("ReaderType"); }
        }

        private string _ReaderNo = "";
        /// <summary>
        /// 读者学号
        /// </summary>
        public string ReaderNo
        {
            get { return _ReaderNo; }
            set { _ReaderNo = value; OnPropertyChanged("ReaderNo"); }
        }
        private string _TipVisible = "Hidden";
        /// <summary>
        /// 限时模式提示标语是否显示
        /// </summary>
        public string TipVisible
        {
            get { return _TipVisible; }
            set { _TipVisible = value; OnPropertyChanged("TipVisible"); }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
