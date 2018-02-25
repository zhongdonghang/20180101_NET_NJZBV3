using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SeatManage.ClassModel;
using SeatManage.ISystemTerminal.IPOS;
using SeatManage.EnumType;
using System.IO;
using System.Xml;

namespace SeatClientV3.OperateResult
{
    public class SystemObject
    {
        #region 私有成员
        ///终端编号
        private string _clientNo = ConfigurationManager.AppSettings["ClientNo"];
        //广告在线
        //private string _adIsOnline = ConfigurationManager.AppSettings["AdIsOnline"];
        //终端设置
        private TerminalInfoV2 _clientSetting;
        //读卡器
        private IPOSMethod _objCardReader;
        //打印
        private System.Printing.LocalPrintServer _printServer;
        //进出记录
        private OperateResult _enterOutLogData = new OperateResult();
        //管理规则
        private RegulationRulesSetting _regulationRulesSet;
        //传送消息的程序
        private string _mediaCLient = ConfigurationManager.AppSettings["MediaCLient"];
        private string _launcherClient = ConfigurationManager.AppSettings["LauncherClient"];
        //缩放的大小
        private double _addSize = double.Parse(ConfigurationManager.AppSettings["AddSize"]);
        /// <summary>
        /// 自动全屏
        /// </summary>
        private bool _seatAutoAddSize = ConfigurationManager.AppSettings["SeatAutoAddSize"] == "1";
        private bool _roomAutoAddSize = ConfigurationManager.AppSettings["RoomAutoAddSize"] == "1";
        //读卡模式
        private bool _useCardReader = ConfigurationManager.AppSettings["CardKinds"] == "1";
        /// <summary>
        /// 关闭程序所需要的密码
        /// </summary>
        private string _closeCheckPassword;
        /// <summary>
        /// 二维码签到Url
        /// </summary>
        private string _codeUrl = ConfigurationManager.AppSettings["CodeCheckUrl"];
        /// <summary>
        /// 是否启用二维码签到
        /// </summary>
        private bool _useCodeCheck = ConfigurationManager.AppSettings["CodeCheck"] == "1";


        /// <summary>
        /// 阅览室列表
        /// </summary>
        private Dictionary<string, ReadingRoomInfo> _readingRoomList = new Dictionary<string, ReadingRoomInfo>();

        //广告
        private PopAdvertInfo _popAdvert;

        private List<PromotionAdvertInfo> _promotionAdvert = new List<PromotionAdvertInfo>();

        private ReaderAdvertInfo _readerAdvert;

        private TitleAdvertInfoV2 _titleAdvert;

        private List<SchoolNoteInfo> _schoolNote = new List<SchoolNoteInfo>();

        private UserGuideInfo _userGuide;


        #endregion
        /// <summary>
        /// 是否初始化读卡器
        /// </summary>
        public bool UseCardReader
        {
            get { return _useCardReader; }
            set { _useCardReader = value; }
        }
        /// <summary>
        /// 验证窗体关闭密码
        /// </summary>
        public string CloseCheckPassword
        {
            get { return _closeCheckPassword; }
            set { _closeCheckPassword = value; }
        }
        /// <summary>
        /// 变大的尺寸
        /// </summary>
        public double AddSize
        {
            get { return _addSize; }
            set { _addSize = value; }
        }
        /// <summary>
        /// 自动全屏
        /// </summary>
        public bool SeatAutoAddSize
        {
            get { return _seatAutoAddSize; }
            set { _seatAutoAddSize = value; }
        }
        /// <summary>
        /// 通知媒体进程
        /// </summary>
        public string MediaClient
        {
            get { return _mediaCLient; }
            set { _mediaCLient = value; }
        }
        /// <summary>
        /// 启动程序
        /// </summary>
        public string LauncherClient
        {
            get { return _launcherClient; }
            set { _launcherClient = value; }
        }
        /// <summary>
        /// 使用手册
        /// </summary>
        public UserGuideInfo UserGuide
        {
            get { return _userGuide; }
            set { _userGuide = value; }
        }
        /// <summary>
        /// 校园通知
        /// </summary>
        public List<SchoolNoteInfo> SchoolNote
        {
            get { return _schoolNote; }
            set { _schoolNote = value; }
        }
        /// <summary>
        /// 冠名广告
        /// </summary>
        public TitleAdvertInfoV2 TitleAdvert
        {
            get { return _titleAdvert; }
            set { _titleAdvert = value; }
        }
        /// <summary>
        /// 读者推广
        /// </summary>
        public ReaderAdvertInfo ReaderAdvert
        {
            get { return _readerAdvert; }
            set { _readerAdvert = value; }
        }
        /// <summary>
        /// 推广广告
        /// </summary>
        public List<PromotionAdvertInfo> PromotionAdvert
        {
            get { return _promotionAdvert; }
            set { _promotionAdvert = value; }
        }
        /// <summary>
        /// 弹窗广告
        /// </summary>
        public PopAdvertInfo PopAdvert
        {
            get { return _popAdvert; }
            set { _popAdvert = value; }
        }

        /// <summary>
        /// 二维码签到Url
        /// </summary>
        public string CodeUrl
        {
            get { return _codeUrl; }
            set { _codeUrl = value; }
        }

        /// <summary>
        /// 是否启用二维码签到
        /// </summary>
        public bool UseCodeCheck
        {
            get { return _useCodeCheck; }
            set { _useCodeCheck = value; }
        }
        /// <summary>
        /// 基础类
        /// </summary>
        private static SystemObject _systemObject;
        /// <summary>
        /// 控制基础类
        /// </summary>
        private static object Object = new object();
        public event EventHandler UpdateConfigError;
        readonly SeatManage.SeatManageComm.TimeLoop _timeloop;
        private SystemObject()
        {
            //设置定时器，间隔10s执行一次阅览室设置更新
            _timeloop = new SeatManage.SeatManageComm.TimeLoop(20000);
            _timeloop.TimeTo += timeloop_TimeTo;
            try
            {
                _regulationRulesSet = GetRegulationRulesSetting();
                _clientSetting = GetClientSet();
                _closeCheckPassword = GetClosePw();
                if (_clientSetting == null)
                {
                    return;
                }

                GetAdvert();
                _timeloop.TimeStrat();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("初始化终端设置失败" + ex.Message);
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
                _closeCheckPassword = GetClosePw();
                if (_clientSetting == null)
                {
                    throw new Exception("获取信息失败");
                }
                _readingRoomList = GetReadingRooms();
                UploadAdvertUsage();
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
            _timeloop.TimeStrat();
        }
        /// <summary>
        /// 暂停自动更新
        /// </summary>
        public void StopUpdateConfig()
        {
            _timeloop.TimeStop();
        }
        /// <summary>
        /// 获得单个SystemObject实例
        /// </summary>
        /// <returns></returns>
        public static SystemObject GetInstance()
        {
            if (_systemObject == null)
            {
                lock (Object)
                {
                    if (_systemObject == null)
                    {
                        return _systemObject = new SystemObject();
                    }
                }
            }
            return _systemObject;
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
                if (_objCardReader != null)
                {
                    return _objCardReader;
                }
                else
                {
                    if (ConfigurationManager.AppSettings["CardKinds"] == "1")
                    {
                        try
                        {
                            _objCardReader = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("IPOSMethod") as IPOSMethod;
                        }
                        catch
                        {
                            _objCardReader = null;
                        }
                        return _objCardReader;
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
            get { return _enterOutLogData; }
            set
            {
                _enterOutLogData = value;
            }
        }

        /// <summary>
        /// 打印服务
        /// </summary>
        public System.Printing.LocalPrintServer PrintServer
        {
            get
            {
                if (_printServer != null)
                {
                    return _printServer;
                }
                else
                {
                    _printServer = new System.Printing.LocalPrintServer();
                    return _printServer;
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
                if (_regulationRulesSet != null)
                {
                    return _regulationRulesSet;
                }
                else
                {
                    _regulationRulesSet = GetRegulationRulesSetting();
                    return _regulationRulesSet;
                }
            }
            set { _regulationRulesSet = value; }
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

        public bool RoomAutoAddSize
        {
            get { return _roomAutoAddSize; }
            set { _roomAutoAddSize = value; }
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
                return SeatManage.Bll.TerminalOperatorService.GetTeminalSetting(_clientNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取验证密码
        /// </summary>
        /// <returns></returns>
        private string GetClosePw()
        {
            try
            {
                UserInfo admin = SeatManage.Bll.Users_ALL.GetUserInfo("admin");
                if (admin != null)
                {
                    SetCheckPassword(admin.Password);
                    return admin.Password;
                }
                else
                {
                    string xmlpw = GetCheckPassword();
                    if (xmlpw != null)
                    {
                        return xmlpw;
                    }
                    else
                    {
                        return "";
                    }
                }

            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取关闭密码失败：{0}", ex.Message));
                return "";
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
                try
                {
                    List<ReadingRoomInfo> rooms = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(_clientSetting.DeviceSetting.Rooms);
                    return rooms.ToDictionary(room => room.No);
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取阅览室失败：{0}", ex.Message));
                    return new Dictionary<string, ReadingRoomInfo>();
                }
            }
        }
        /// <summary>
        /// 保存本地密码
        /// </summary>
        /// <param name="password"></param>
        private void SetCheckPassword(string password)
        {
            try
            {
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = string.Format("{0}CheckKey", fileDircetoryPath);
                string strLogFilePath = filePath;
                File.WriteAllText(strLogFilePath, password);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("保存关闭密码失败：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 获取关闭窗口的密码
        /// </summary>
        /// <returns></returns>
        private string GetCheckPassword()
        {
            try
            {
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = string.Format("{0}CheckKey", fileDircetoryPath);
                string strLogFilePath = filePath;
                string password = File.ReadAllText(strLogFilePath);
                return password;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取关闭密码失败：{0}", ex.Message));
                return "";
            }
        }
        /// <summary>
        /// 获取广告
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
                        case AdType.PopAd:
                            {
                                if (_popAdvert != null)
                                {
                                    break;
                                }
                                _popAdvert = PopAdvertInfo.ToModel(advert.AdContent);
                                _popAdvert.ID = advert.ID;
                                _popAdvert.InitializeUsage();
                                if (!Directory.Exists(path + "images\\AdImage\\PopImage\\"))
                                {
                                    Directory.CreateDirectory(path + "images\\AdImage\\PopImage\\");
                                }
                                if (!fileoperator.FileDownLoad(path + "images\\AdImage\\PopImage\\" + _popAdvert.PopImagePath, _popAdvert.PopImagePath, SeatManageSubsystem.PopAd))
                                {
                                    _popAdvert = null;
                                    SeatManage.SeatManageComm.WriteLog.Write(string.Format("弹窗广告初始化失败：{0}", advert.Num));
                                }
                            }
                            break;
                        case AdType.PromotionAd:
                            {
                                PromotionAdvertInfo model = PromotionAdvertInfo.ToModel(advert.AdContent);
                                model.ID = advert.ID;
                                model.InitializeUsage();
                                if (!Directory.Exists(path + "images\\AdImage\\PromotionImage\\"))
                                {
                                    Directory.CreateDirectory(path + "images\\AdImage\\PromotionImage\\");
                                }
                                if (fileoperator.FileDownLoad(path + "images\\AdImage\\PromotionImage\\" + model.AdImagePath, model.AdImagePath, SeatManageSubsystem.PromotionAd))
                                {
                                    _promotionAdvert.Add(model);
                                }
                                else
                                {
                                    SeatManage.SeatManageComm.WriteLog.Write(string.Format("推广广告初始化失败：{0}", advert.Num));
                                }
                            }
                            break;
                        case AdType.ReaderAd:
                            {
                                if (_readerAdvert != null)
                                {
                                    break;
                                }
                                _readerAdvert = ReaderAdvertInfo.ToModel(advert.AdContent);
                                _readerAdvert.ID = advert.ID;
                                _readerAdvert.InitializeUsage();
                                if (!Directory.Exists(path + "images\\AdImage\\ReaderImage\\"))
                                {
                                    Directory.CreateDirectory(path + "images\\AdImage\\ReaderImage\\");
                                }
                                if (!fileoperator.FileDownLoad(path + "images\\AdImage\\ReaderImage\\" + _readerAdvert.ReaderAdImagePath, _readerAdvert.ReaderAdImagePath, SeatManageSubsystem.ReaderAd))
                                {
                                    _readerAdvert = null;
                                    SeatManage.SeatManageComm.WriteLog.Write(string.Format("读者广告初始化失败：{0}", advert.Num));
                                }
                            }
                            break;
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
                                    _schoolNote.Add(model);
                                }
                                else
                                {
                                    SeatManage.SeatManageComm.WriteLog.Write(string.Format("推广广告初始化失败：{0}", advert.Num));
                                }
                            }
                            break;
                        case AdType.TitleAd:
                            {
                                _titleAdvert = TitleAdvertInfoV2.ToModel(advert.AdContent);
                                _titleAdvert.ID = advert.ID;
                                _titleAdvert.InitializeUsage();
                            }
                            break;
                    }
                }
                _userGuide = SeatManage.Bll.T_SM_SystemSet.GetUserGuide();
                if (_userGuide != null)
                {
                    if (!Directory.Exists(path + "images\\AdImage\\UserGuide\\"))
                    {
                        Directory.CreateDirectory(path + "images\\AdImage\\UserGuide\\");
                    }
                    foreach (string file in _userGuide.ImageFilePath)
                    {

                        if (!fileoperator.FileDownLoad(path + "images\\AdImage\\UserGuide\\" + file, file, SeatManageSubsystem.UserGuide))
                        {
                            SeatManage.SeatManageComm.WriteLog.Write("使用手册初始化失败");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("广告初始化失败：{0}", ex.Message));
            }
        }
        private void UploadAdvertUsage()
        {
            try
            {
                string error = "";
                if (_popAdvert != null)
                {
                    error = SeatManage.Bll.AdvertisementOperation.UpdateAdvertUsage(_popAdvert.Usage);
                    if (error != "")
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传状态失败：{0}", error));
                    }
                    else
                    {
                        _popAdvert.Usage.Clean();
                    }
                }
                if (_promotionAdvert.Count > 0)
                {
                    foreach (PromotionAdvertInfo item in _promotionAdvert)
                    {
                        error = SeatManage.Bll.AdvertisementOperation.UpdateAdvertUsage(item.Usage);
                        if (error != "")
                        {
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传状态失败：{0}", error));
                        }
                        else
                        {
                            item.Usage.Clean();
                        }
                    }
                }
                if (_readerAdvert != null)
                {
                    error = SeatManage.Bll.AdvertisementOperation.UpdateAdvertUsage(_readerAdvert.Usage);
                    if (error != "")
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传状态失败：{0}", error));
                    }
                    else
                    {
                        _readerAdvert.Usage.Clean();
                    }
                }
                if (_titleAdvert != null)
                {
                    error = SeatManage.Bll.AdvertisementOperation.UpdateAdvertUsage(_titleAdvert.Usage);
                    if (error != "")
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传状态失败：{0}", error));
                    }
                    else
                    {
                        _titleAdvert.Usage.Clean();
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传状态失败：{0}", ex.Message));
                throw;
            }
        }
        #endregion
    }
    /// <summary>
    /// 操作结果保存类
    /// </summary>
    public class OperateResult
    {
        private ReaderInfo _student = new ReaderInfo();
        /// <summary>
        /// 读者信息
        /// </summary> 
        public ReaderInfo Student
        {
            get { return _student; }
            set { _student = value; }
        }

        private ReadingRoomInfo _roomInfo = null;
        /// <summary>
        /// 读者选择的阅览室
        /// </summary>
        public ReadingRoomInfo RoomInfo
        {
            get { return _roomInfo; }
            set { _roomInfo = value; }
        }

        private ClientOperation _chooseResult = ClientOperation.None;
        /// <summary>
        /// 选座操作的流程控制
        /// </summary> 
        public ClientOperation FlowControl
        {
            get { return _chooseResult; }
            set { _chooseResult = value; }
        }
        private EnterOutLogInfo _enterOutlog = null;
        /// <summary>
        /// 进出记录
        /// </summary>
        public EnterOutLogInfo EnterOutlog
        {
            get { return _enterOutlog; }
            set { _enterOutlog = value; }
        }

        private WaitSeatLogInfo _waitSeatLogModel = null;
        /// <summary>
        /// 等待记录的实体
        /// </summary>
        public WaitSeatLogInfo WaitSeatLogModel
        {
            get { return _waitSeatLogModel; }
            set { _waitSeatLogModel = value; }
        }

        private BespeakLogInfo _bespeakLogInfo = null;
        /// <summary>
        /// 预约记录的实体
        /// </summary>
        public BespeakLogInfo BespeakLogInfo
        {
            get { return _bespeakLogInfo; }
            set { _bespeakLogInfo = value; }
        }
    }

}
