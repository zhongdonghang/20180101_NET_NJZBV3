using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SeatManage.EnumType;
using SeatClientV3.OperateResult;
using SeatClientV3.FunWindow;

namespace SeatClientV3.ViewModel
{
    public class ReaderNoticeWindow_ViewModel : INotifyPropertyChanged
    {
        public ReaderNoticeWindow_ViewModel()
        {
            clientObject = SeatClientV3.OperateResult.SystemObject.GetInstance();
            WindowWidth = (double)clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 620;
            WindowHeight = (double)clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 320;
            WindowLeft = (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;

            AddReaderNoticeInfoList();
        }
        private SeatClientV3.OperateResult.SystemObject clientObject;
        /// <summary>
        /// 基础类
        /// </summary>
        public SeatClientV3.OperateResult.SystemObject ClientObject
        {
            get { return SeatClientV3.OperateResult.SystemObject.GetInstance(); }
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

        private List<SeatManage.ClassModel.ReaderNoticeInfo> _ReaderNoticeInfoList;
        /// <summary>
        /// 消息列表
        /// </summary>
        public List<SeatManage.ClassModel.ReaderNoticeInfo> ReaderNoticeInfoList
        {
            get { return _ReaderNoticeInfoList; }
            set { _ReaderNoticeInfoList = value; OnPropertyChanged("ReaderNoticeInfoList"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void AddReaderNoticeInfoList()
        {
            try
            {
                ReaderNoticeInfoList = clientObject.EnterOutLogData.Student.NoticeInfo;
                SeatManage.Bll.T_SM_ReaderNotice.SetReaderNoteRead(ReaderNoticeInfoList);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("更新短消息读取状态遇到异常" + ex.Message);
                MessageWindow errorWindow = new MessageWindow(MessageType.Exception);
                errorWindow.ShowDialog();
            }
        }
    }
}
