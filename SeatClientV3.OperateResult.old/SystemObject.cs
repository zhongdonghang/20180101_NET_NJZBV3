using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SeatManage.ClassModel;
using SeatManage.ISystemTerminal.IPOS;
using SeatManage.EnumType;
using System.IO;

namespace SeatClientV3.OperateResult
{
    public class SystemObject
    {
        #region 私有成员
        //终端编号
        private string clientNo = ConfigurationManager.AppSettings["ClientNo"];
        //终端设置
        private TerminalInfoV2 _clientSetting = null;
        //读卡器
        private IPOSMethod objCardReader = null;
        //打印
        private System.Printing.LocalPrintServer _PrintServer = null;
        //进出记录
        private OperateResult _EnterOutLogData = new OperateResult();
        //管理规则
        private RegulationRulesSetting _RegulationRulesSet = null;

        /// <summary>
        /// 阅览室列表
        /// </summary>
        //private Dictionary<string, ReadingRoomInfo> _readingRoomList = new Dictionary<string, ReadingRoomInfo>();
        //校园通知
        private List<SchoolNoteInfo> _SchoolNote = new List<SchoolNoteInfo>();

        /// <summary>
        ///调试模式
        /// </summary>
        private bool _IsTestModel = ConfigurationManager.AppSettings["CardKinds"] == "1" ? false : true;
        
        #endregion
        /// <summary>
        /// 刷卡测试模式
        /// </summary>
        public bool IsTestModel
        {
            get { return _IsTestModel; }
            set { _IsTestModel = value; }
        }
        /// <summary>
        /// 校园通知
        /// </summary>
        public List<SchoolNoteInfo> SchoolNote
        {
            get { return _SchoolNote; }
            set { _SchoolNote = value; }
        }


        private static SystemObject systemObject = null;
        private static object _object = new object();
        public event EventHandler UpdateConfigError;
        public event EventHandler UpdateForm;
        SeatManage.SeatManageComm.TimeLoop timeloop = null;
        private SystemObject()
        {
            //设置定时器，间隔10s执行一次阅览室设置更新
            timeloop = new SeatManage.SeatManageComm.TimeLoop(20000);
            timeloop.TimeTo += timeloop_TimeTo;
            try
            {
                _RegulationRulesSet = GetRegulationRulesSetting();
                _clientSetting = GetClientSet();
                if (_clientSetting == null)
                {
                    return;
                }

                GetAdvert();
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
                //else
                //{
                //    if (UpdateForm != null)
                //    {
                //        UpdateForm(this, new EventArgs());
                //    }
                //}
                //_readingRoomList = GetReadingRooms();
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
        /// 终端设置
        /// </summary>
        public TerminalInfoV2 ClientSetting
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
                    if (!IsTestModel)
                    {
                        try
                        {
                            objCardReader = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("IPOSMethod") as IPOSMethod; 
                        }
                        catch
                        {
                            objCardReader = null;
                        }
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
        /// 黑名单设置
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
        /// 触摸屏所管理的区域编号
        /// </summary>
        //public Dictionary<string, ReadingRoomInfo> ReadingRoomList
        //{
        //    get
        //    {
        //        if (_readingRoomList.Count == 0)
        //        {
        //            _readingRoomList = GetReadingRooms();
        //        }
        //        return _readingRoomList;
        //    }
        //}
        #region 私有方法

        /// <summary>
        /// 获取座位管理规则设置
        /// </summary>
        /// <returns></returns>
        private RegulationRulesSetting GetRegulationRulesSetting()
        {
            try
            {
                return SeatManage.Bll.T_SM_SystemSet.GetRegulationRulesSetting();
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
        private TerminalInfoV2 GetClientSet()
        {
            try
            {
                return SeatManage.Bll.TerminalOperatorService.GetTeminalSetting(clientNo);
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
        private System.Windows.Media.Imaging.BitmapImage GetSchoolLogoBitmapImageResource(ClientConfigV2 config)
        {
            string temp = "";
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;

                temp = string.Format("{0}{1}", path, config.BackImgage["SchoolLogoImage"]);
                if (System.IO.File.Exists(temp))
                {
                    return new System.Windows.Media.Imaging.BitmapImage(new Uri(temp, UriKind.RelativeOrAbsolute));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("背景图片初始化失败：{0} {1},{2}", config.BackImgage[temp], ex.Message, temp));
                return null;
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
        /// <summary>
        /// 获取通知
        /// </summary>
        private void GetAdvert()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                SeatManage.Bll.FileOperate fileoperator = new SeatManage.Bll.FileOperate();
                List<AMS_Advertisement> advertList = SeatManage.Bll.AdvertisementOperation.GetAdList(false, AdType.None);
                foreach (AMS_Advertisement advert in advertList)
                {
                    switch (advert.Type)
                    {
                        case AdType.SchoolNotice:
                            {
                                SchoolNoteInfo model = SchoolNoteInfo.ToModel(advert.AdContent);
                                model.ID = advert.ID;
                                model.InitializeUsage();
                                if (!Directory.Exists(path + "images\\AdImage\\NoteImage\\"))
                                {
                                    Directory.CreateDirectory(path + "images\\AdImage\\NoteImage\\");
                                }
                                if (fileoperator.FileDownLoad(path + "images\\AdImage\\NoteImage\\" + model.NoteImagePath, model.NoteImagePath, SeatManageSubsystem.SchoolNotice))
                                {
                                    _SchoolNote.Add(model);
                                }
                                else
                                {
                                    SeatManage.SeatManageComm.WriteLog.Write(string.Format("校园通知初始化失败：{0}", advert.Num));
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("校园通知初始化失败：{0}", ex.Message));
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

        private BespeakLogInfo bespeakLogInfo = null;
        /// <summary>
        /// 预约记录的实体
        /// </summary>
        public BespeakLogInfo BespeakLogInfo
        {
            get { return bespeakLogInfo; }
            set { bespeakLogInfo = value; }
        }
    }
}
