using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SeatManage.EnumType;
using SeatClientV2.OperateResult;

namespace SeatClientV2.ViewModel
{
    public class ReaderNoteWindow_ViewModel : INotifyPropertyChanged
    {
        public ReaderNoteWindow_ViewModel()
        {
            clientObject = SeatClientV2.OperateResult.SystemObject.GetInstance();
            WindowHeight = clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y;
            WindowWidth = clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X;
            WindowLeft = clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X;
            WindowTop = clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y;
            if (clientObject.TitleAdvert!=null)
            {
                TitleAd = clientObject.TitleAdvert.TextContent;
                clientObject.TitleAdvert.Usage.WatchCount++;
            }
            else
            {
                TitleAd = "Juneberry提醒您";
            }
            AddReaderNoticeInfoList();
        }
        private SeatClientV2.OperateResult.SystemObject clientObject;
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
        private string _TitleAd;
        /// <summary>
        /// 标题
        /// </summary>
        public string TitleAd
        {
            get { return _TitleAd; }
            set { _TitleAd = value; OnPropertyChanged("TitleAd"); }
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
                PopupWindow errorWindow = new PopupWindow(SeatManage.EnumType.TipType.Exception);
                errorWindow.ShowDialog();
            }
        }
    }
}
