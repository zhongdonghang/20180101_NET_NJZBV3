using SeatClientV3.OperateResult;
using SeatManage.ClassModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SeatClientV3.Code;

namespace SeatClientV3.ViewModel
{
    public class RecordTheQueryWindow_ViewModel : INotifyPropertyChanged
    {
        public RecordTheQueryWindow_ViewModel()
        {
            WindowHeight = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
            WindowWidth = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
            WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
            WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
            if (ClientObject.ObjCardReader == null)
            {
                TestMode = "Visible";
            }
        }

        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject ClientObject
        {
            get { return SystemObject.GetInstance(); }
        }
        ReaderInfoViewModel _Reader = new ReaderInfoViewModel();
        /// <summary>
        /// 读者信息
        /// </summary>
        public ReaderInfoViewModel Reader
        {
            get { return _Reader; }
            set { _Reader = value; OnPropertyChanged("Reader"); }
        }
        private double _WindowHeight = 0;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public double WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; OnPropertyChanged("WindowHeight"); }
        }

        private double _WindowWidth = 0;
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public double WindowWidth
        {
            get { return _WindowWidth; }
            set { _WindowWidth = value; OnPropertyChanged("WindowWidth"); }
        }

        private double _WindowLeft = 0;
        /// <summary>
        /// 窗体左上角X轴
        /// </summary>
        public double WindowLeft
        {
            get { return _WindowLeft; }
            set { _WindowLeft = value; OnPropertyChanged("WindowLeft"); }
        }

        private double _WindowTop = 0;
        /// <summary>
        /// 窗体左上角Y轴
        /// </summary>
        public double WindowTop
        {
            get { return _WindowTop; }
            set { _WindowTop = value; OnPropertyChanged("WindowTop"); }
        }
        private int _CloseTime = 0;
        /// <summary>
        /// 窗口关闭时间
        /// </summary>
        public int CloseTime
        {
            get { return _CloseTime; }
            set { _CloseTime = value; OnPropertyChanged("CloseTime"); }
        }
        FormCloseCountdown _CountDown = null;
        /// <summary>
        /// 窗体关闭倒计时
        /// </summary>
        public FormCloseCountdown CountDown
        {
            get { return _CountDown; }
            set { _CountDown = value; }
        }
        private ObservableCollection<SeatManage.ClassModel.EnterOutLogInfo> _EnterOutLogList=new ObservableCollection<EnterOutLogInfo>();
        /// <summary>
        /// 进出记录统计
        /// </summary>
        public ObservableCollection<SeatManage.ClassModel.EnterOutLogInfo> EnterOutLogList
        {
            get { return _EnterOutLogList; }
            set { _EnterOutLogList = value; OnPropertyChanged("EnterOutLogList"); }
        }
        private string _TestMode = "Collapsed";
        /// <summary>
        /// 测试模式
        /// </summary>
        public string TestMode
        {
            get { return _TestMode; }
            set { _TestMode = value; OnPropertyChanged("TestMode"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void AddRecoed(string cardNo)
        {

            EnterOutLogList.Clear();
            Reader.ReaderInfo = SeatManage.Bll.EnterOutOperate.GetReaderInfo(cardNo);
            DateTime date = SeatManage.Bll.ServiceDateTime.Now;
            DateTime beginDate = DateTime.Parse(date.Date.AddDays(-2).ToShortDateString() + " 0:0:0");
            DateTime endDate = DateTime.Parse(date.ToShortDateString() + " 23:59:59");
            List<EnterOutLogInfo> enterOutLogs = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogs(cardNo, null, null, beginDate, endDate);
            IEnumerable<EnterOutLogInfo> query = from items in enterOutLogs orderby items.EnterOutTime select items;
            foreach (var info in query)
            {
                EnterOutLogList.Add(info);
            }
            OnPropertyChanged("EnterOutLogList");
            OnPropertyChanged("Reader");
        }
    }
}
