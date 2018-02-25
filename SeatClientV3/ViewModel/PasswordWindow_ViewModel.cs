using SeatClientV3.OperateResult;
using SeatManage.EnumType;
using System.ComponentModel;
using System.Windows.Forms;

namespace SeatClientV3.ViewModel
{
    public class PasswordWindow_ViewModel : INotifyPropertyChanged
    {

        public PasswordWindow_ViewModel()
        {
            WindowWidth = ClientObject.ClientSetting == null ? 580 : (double)ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 580;
            WindowHeight = ClientObject.ClientSetting == null ? 400 : (double)ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 400;
            WindowLeft = ClientObject.ClientSetting == null ? Screen.PrimaryScreen.Bounds.Width / 2 -290 : ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = ClientObject.ClientSetting == null ? Screen.PrimaryScreen.Bounds.Height - 600 : ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;
        }
        #region 属性 成员
        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject ClientObject
        {
            get { return SystemObject.GetInstance(); }
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

        HandleResult operateResule = HandleResult.Failed;
        /// <summary>
        /// 操作结果，成功或者失败
        /// </summary>
        public HandleResult OperateResule
        {
            get { return operateResule; }
            set { operateResule = value; OnPropertyChanged("OperateResule"); }
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            try
            {
                if (ClientObject == null)
                {
                    OperateResule = HandleResult.Successed;
                    return true;
                }
                if (ClientObject.CloseCheckPassword == SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(password))
                {
                    OperateResule = HandleResult.Successed;
                    return true;
                }
                else
                {
                    OperateResule = SeatManage.EnumType.HandleResult.Failed;
                    return false;

                }
            }
            catch
            {
                OperateResule = SeatManage.EnumType.HandleResult.Failed;
                return false;

            }
        }
        #endregion

    }
}
