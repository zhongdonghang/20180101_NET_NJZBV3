using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class TerminalInfoV2
    {
        private string clientNo = "";
        private string _ScreenshotPath = "";
        private string _Describe = "";
        private bool _IsUpdatePlayList = false;
        private DateTime _StatusUpdateTime = DateTime.Parse("1900-1-1");
        private ClientConfigV2 _DeviceSetting = new ClientConfigV2();
        private string _Status = "";

        private string _TerminalMacAddress = "";
        /// <summary>
        /// 终端MAC地址
        /// </summary>
        public string TerminalMacAddress
        {
            get { return _TerminalMacAddress; }
            set { _TerminalMacAddress = value; }
        }
        /// <summary>
        /// 设备描述
        /// </summary>
        public string Describe
        {
            get { return _Describe; }
            set { _Describe = value; }
        }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string ClientNo
        {
            get { return clientNo; }
            set { clientNo = value; }
        }
        /// <summary>
        /// 截图上传路径
        /// </summary>
        public string ScreenshotPath
        {
            get { return _ScreenshotPath; }
            set { _ScreenshotPath = value; }
        }
        /// <summary>
        /// 状态更新时间
        /// </summary>
        public DateTime StatusUpdateTime
        {
            get { return _StatusUpdateTime; }
            set { _StatusUpdateTime = value; }
        }
        /// <summary>
        /// 是否有需要更新的播放列表
        /// </summary>
        public bool IsUpdatePlayList
        {
            get { return _IsUpdatePlayList; }
            set { _IsUpdatePlayList = value; }
        }
        /// <summary>
        /// 终端设置
        /// </summary>
        public ClientConfigV2 DeviceSetting
        {
            get { return _DeviceSetting; }
            set { _DeviceSetting = value; }
        }

        /// <summary>
        /// 设备状态
        /// </summary>
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        private DateTime _EmpowerLoseEfficacyTime = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 授权失效时间
        /// </summary>
        public DateTime EmpowerLoseEfficacyTime
        {
            get { return _EmpowerLoseEfficacyTime; }
            set { _EmpowerLoseEfficacyTime = value; }
        }
        private bool _PrinterStatus = false;
        /// <summary>
        /// 打印机当前状态 false为缺纸 true为有纸
        /// </summary>
        public bool PrinterStatus
        {
            get { return _PrinterStatus; }
            set { _PrinterStatus = value; }
        }
        private int _LastPrintTimes = 0;
        /// <summary>
        /// 上卷打印纸的打印次数
        /// </summary>
        public int LastPrintTimes
        {
            get { return _LastPrintTimes; }
            set { _LastPrintTimes = value; }
        }
        private int _PrintedTimes = 0;
        /// <summary>
        /// 本卷打印纸已打印次数
        /// </summary>
        public int PrintedTimes
        {
            get { return _PrintedTimes; }
            set { _PrintedTimes = value; }
        }
    }
}
