using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.Configuration;
using SeatManage.ISystemTerminal.IPOS;
using System.Drawing;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;
using SeatManage.Bll;
namespace SeatClient.OperateResult
{
    /// <summary>
    /// 系统初始化
    /// </summary>
    public class SystemObject
    {
        /// <summary>
        /// 终端设置的设备编号
        /// </summary>
        private string clientNo = ConfigurationManager.AppSettings["ClientNo"];
        private string adIsOnline = ConfigurationManager.AppSettings["AdIsOnline"];
        private TerminalInfo _clientSetting = null;
        private HardAdvertInfo _HardAdvert = null;
        private Dictionary<string, Bitmap> _BackgroundImagesResource = new Dictionary<string, Bitmap>();
        private Dictionary<string, ReadingRoomInfo> _readingRoomList = new Dictionary<string, ReadingRoomInfo>();
        private IPOSMethod objCardReader = null;
        private System.Printing.LocalPrintServer _PrintServer = null;
        private OperateResult _EnterOutLogData = new OperateResult();
        private RegulationRulesSetting _RegulationRulesSet = null;
        /// <summary>
        /// 座位管理系统规则设置
        /// </summary>
        public RegulationRulesSetting RegulationRulesSet
        {
            get
            {
                if (_RegulationRulesSet != null)
                {
                    return _RegulationRulesSet;
                }
                else
                {
                    _RegulationRulesSet = GetRegulationRulesSetting();
                    return _RegulationRulesSet;
                }
            }
            set { _RegulationRulesSet = value; }
        }
        /// <summary>
        /// 硬广图片
        /// </summary>
        public HardAdvertInfo HardAdvert
        {
            get
            {
                if (_HardAdvert == null)
                {
                    if (adIsOnline == "true")
                    {
                        _HardAdvert = SeatManage.Bll.AMS_HardAd.GetHardAd();
                    }
                    else
                    {
                        _HardAdvert = new HardAdvertInfo();
                        _HardAdvert.AdvertImage =SeatManage.SeatManageComm.ImageStream.ImageToStream( BackgroundImagesResource["AdImage"]);
                    }
                }
                return _HardAdvert;
            }
            set { _HardAdvert = value; }
        }

        private static SystemObject systemObject = null;
        private static object _object = new object();
        public event EventHandler UpdateConfigError;
        public event EventHandler UpdateForm;
        TimeLoop timeloop = null;
        private SystemObject()
        {
            //设置定时器，间隔10s执行一次阅览室设置更新
            timeloop = new TimeLoop(20000);
            timeloop.TimeTo += timeloop_TimeTo;
            try
            {
                _RegulationRulesSet = GetRegulationRulesSetting();
                _clientSetting = GetClientSet();
                if (_clientSetting == null)
                {
                    return;
                }
                _BackgroundImagesResource = GetBitmapImageResource(_clientSetting.DeviceSetting);
                timeloop.TimeStrat();
            }
            catch
            {
                throw;
            }
        }

        void timeloop_TimeTo(object sender, EventArgs e)
        {
            try
            {
                GC.Collect();
                StopUpdateConfig();
                _clientSetting = GetClientSet();//获取终端设置
                if (_clientSetting == null)
                {
                    throw new Exception("获取信息失败");
                }
                else
                {
                    if (UpdateForm != null)
                    {
                        UpdateForm(this, new EventArgs());
                    }
                }
                _readingRoomList = GetReadingRooms();

            }
            catch
            {
                if (UpdateConfigError != null)
                {
                    UpdateConfigError(this, new EventArgs());
                }
            }
            finally
            {
                StartAutoUpdateConfig();
            }
        }
        /// <summary>
        /// 开始自动更新
        /// </summary>
        public void StartAutoUpdateConfig()
        {
            timeloop.TimeStrat();
        }
        /// <summary>
        /// 暂停自动更新
        /// </summary>
        public void StopUpdateConfig()
        {
            timeloop.TimeStop();
        }
        /// <summary>
        /// 获得单个SystemObject实例
        /// </summary>
        /// <returns></returns>
        public static SystemObject GetInstance()
        {
            if (systemObject == null)
            {
                lock (_object)
                {
                    if (systemObject == null)
                    {
                        return systemObject = new SystemObject();
                    }
                }
            }
            return systemObject;
        }
        /// <summary>
        /// 背景图片资源
        /// </summary>
        public Dictionary<string, Bitmap> BackgroundImagesResource
        {
            get { return _BackgroundImagesResource; }
            set { _BackgroundImagesResource = value; }
        }
        /// <summary>
        /// 终端设置
        /// </summary>
        public TerminalInfo ClientSetting
        {
            get
            {
                if (_clientSetting == null)
                {
                    _clientSetting = GetClientSet();
                    return _clientSetting;
                }
                else
                {
                    return _clientSetting;
                }
            }
        }
        /// <summary>
        /// 读卡器接口对象
        /// </summary>
        public IPOSMethod ObjCardReader
        {
            get
            {
                if (objCardReader != null)
                {
                    return objCardReader;
                }
                else
                {
                    if (ConfigurationManager.AppSettings["CardKinds"] == "1")
                    {
                        objCardReader = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("IPOSMethod") as IPOSMethod; //SeatManage.InterfaceFactory.SystemTerminalFactory.CreatePOSMethod();
                        return objCardReader;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// 进出记录的操作结果数据
        /// </summary>
        public OperateResult EnterOutLogData
        {
            get { return _EnterOutLogData; }
            set
            {
                _EnterOutLogData = value;
            }
        }

        /// <summary>
        /// 打印服务
        /// </summary>
        public System.Printing.LocalPrintServer PrintServer
        {
            get
            {
                if (_PrintServer != null)
                {
                    return _PrintServer;
                }
                else
                {
                    _PrintServer = new System.Printing.LocalPrintServer();
                    return _PrintServer;
                }
            }
        }

        /// <summary>
        /// 触摸屏所管理的区域编号
        /// </summary>
        public Dictionary<string, ReadingRoomInfo> ReadingRoomList
        {
            get
            {
                if (_readingRoomList.Count == 0)
                {
                    _readingRoomList = GetReadingRooms();
                }
                return _readingRoomList;
            }
        }


        #region 私有方法
        /// <summary>
        /// 获取座位管理规则设置
        /// </summary>
        /// <returns></returns>
        private RegulationRulesSetting GetRegulationRulesSetting()
        {
            try
            {
                return T_SM_SystemSet.GetRegulationRulesSetting();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 根据终端上配置的设备编号，从服务器上获取终端设置
        /// </summary>
        /// <returns></returns>
        private TerminalInfo GetClientSet()
        {
            try
            {
                TerminalInfo terminal = SeatManage.Bll.ClientConfigOperate.GetClientConfig(clientNo);
                if (adIsOnline != "true")
                {
                    terminal.TitleAd = "";
                }
                return terminal;
            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        /// <summary>
        /// 把图片路径转换为资源字典
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private Dictionary<string, Bitmap> GetBitmapImageResource(ClientConfig config)
        {
            Dictionary<string, Bitmap> imageResource = new Dictionary<string, Bitmap>();
            string temp = "";
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                foreach (string imgKey in _clientSetting.DeviceSetting.BackImgage.Keys)
                {
                    temp = string.Format("{0}{1}", path, config.BackImgage[imgKey]);
                    if (System.IO.File.Exists(temp))
                    {

                        imageResource.Add(imgKey, new Bitmap(temp));
                    }
                    else
                    {
                        imageResource.Add(imgKey,null);
                    }
                }
                return imageResource;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("背景图片初始化失败：{0} {1},{2}", config.BackImgage[temp], ex.Message, temp));
                return new Dictionary<string, Bitmap>();
            }
        }

        /// <summary>
        /// 获取终端所管理的阅览室设置
        /// </summary>
        private Dictionary<string, ReadingRoomInfo> GetReadingRooms()
        {
            object obj = new object();
            lock (obj)
            {
                Dictionary<string, ReadingRoomInfo> roomList = new Dictionary<string, ReadingRoomInfo>();
                List<ReadingRoomInfo> rooms = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(_clientSetting.DeviceSetting.Rooms);

                foreach (ReadingRoomInfo room in rooms)
                {
                    roomList.Add(room.No, room);
                }
                return roomList;
            }
        }
        #endregion
    }

    public class OperateResult
    {
        private ReaderInfo student = new ReaderInfo();
        /// <summary>
        /// 读者信息
        /// </summary> 
        public ReaderInfo Student
        {
            get { return student; }
            set { student = value; }
        }

        private ReadingRoomInfo _RoomInfo = null;
        /// <summary>
        /// 读者选择的阅览室
        /// </summary>
        public ReadingRoomInfo RoomInfo
        {
            get { return _RoomInfo; }
            set { _RoomInfo = value; }
        }

        private ClientOperation chooseResult = ClientOperation.None;
        /// <summary>
        /// 选座操作的流程控制
        /// </summary> 
        public ClientOperation FlowControl
        {
            get { return chooseResult; }
            set { chooseResult = value; }
        }
        private EnterOutLogInfo enterOutlog = null;
        /// <summary>
        /// 进出记录
        /// </summary>
        public EnterOutLogInfo EnterOutlog
        {
            get { return enterOutlog; }
            set { enterOutlog = value; }
        }

        private WaitSeatLogInfo waitSeatLogModel = null;
        /// <summary>
        /// 等待记录的实体
        /// </summary>
        public WaitSeatLogInfo WaitSeatLogModel
        {
            get { return waitSeatLogModel; }
            set { waitSeatLogModel = value; }
        }
    }
}
