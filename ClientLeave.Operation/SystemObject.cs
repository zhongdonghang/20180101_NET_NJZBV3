using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SeatManage.ClassModel;
using SeatManage.ISystemTerminal.IPOS;
using SeatManage.EnumType;
using System.IO;

namespace ClientLeaveV2.OperateResult
{
    public class SystemObject
    {
        #region 私有成员
        //终端编号
        private string clientNo = ConfigurationManager.AppSettings["ClientNo"];
        //广告在线
        private string adIsOnline = ConfigurationManager.AppSettings["AdIsOnline"];
        //读卡器
        private IPOSMethod objCardReader = null;
        //进出记录
        private OperateResult _EnterOutLogData = new OperateResult();
        //管理规则
        private RegulationRulesSetting _RegulationRulesSet = null;
        /// <summary>
        /// 图片资源
        /// </summary>
        //private System.Windows.Media.Imaging.BitmapImage _SchoolLogoImagesResource;


        /// <summary>
        /// 阅览室列表
        /// </summary>
        private Dictionary<string, ReadingRoomInfo> _readingRoomList = new Dictionary<string, ReadingRoomInfo>();
        //广告
        private PopAdvertInfo _PopAdvert;

        private List<PromotionAdvertInfo> _PromotionAdvert = new List<PromotionAdvertInfo>();

        private ReaderAdvertInfo _ReaderAdvert;

        private TitleAdvertInfoV2 _TitleAdvert;

        private List<SchoolNoteInfo> _SchoolNote = new List<SchoolNoteInfo>();

        private UserGuideInfo _UserGuide;


        #endregion

        /// <summary>
        /// 使用手册
        /// </summary>
        public UserGuideInfo UserGuide
        {
            get { return _UserGuide; }
            set { _UserGuide = value; }
        }
        /// <summary>
        /// 校园通知
        /// </summary>
        public List<SchoolNoteInfo> SchoolNote
        {
            get { return _SchoolNote; }
            set { _SchoolNote = value; }
        }
        /// <summary>
        /// 冠名广告
        /// </summary>
        public TitleAdvertInfoV2 TitleAdvert
        {
            get { return _TitleAdvert; }
            set { _TitleAdvert = value; }
        }
        /// <summary>
        /// 读者推广
        /// </summary>
        public ReaderAdvertInfo ReaderAdvert
        {
            get { return _ReaderAdvert; }
            set { _ReaderAdvert = value; }
        }
        /// <summary>
        /// 推广广告
        /// </summary>
        public List<PromotionAdvertInfo> PromotionAdvert
        {
            get { return _PromotionAdvert; }
            set { _PromotionAdvert = value; }
        }
        /// <summary>
        /// 弹窗广告
        /// </summary>
        public PopAdvertInfo PopAdvert
        {
            get { return _PopAdvert; }
            set { _PopAdvert = value; }
        }
        /// <summary>
        /// logo
        /// </summary>
        //public System.Windows.Media.Imaging.BitmapImage SchoolLogoImagesResource
        //{
        //    get { return _SchoolLogoImagesResource; }
        //    set { _SchoolLogoImagesResource = value; }
        //}


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

                GetAdvert();
                //SchoolLogoImagesResource = GetSchoolLogoBitmapImageResource(_clientSetting.DeviceSetting);
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
                _readingRoomList = GetReadingRooms();
                if (_readingRoomList.Count < 1)
                {
                    if (UpdateConfigError != null)
                    {
                        UpdateConfigError(this, new EventArgs());
                    }
                }
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
                        try
                        {

                            objCardReader = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("IPOSMethod") as IPOSMethod; //SeatManageNew.InterfaceFactory.SystemTerminalFactory.CreatePOSMethod();
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("读卡器初始化成功"));
                        }
                        catch
                        {
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("读卡器初始化失败"));
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
                List<ReadingRoomInfo> rooms = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null); ;

                foreach (ReadingRoomInfo room in rooms)
                {
                    roomList.Add(room.No, room);
                }
                return roomList;
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
                                if (_PopAdvert != null)
                                {
                                    break;
                                }
                                _PopAdvert = PopAdvertInfo.ToModel(advert.AdContent);
                                _PopAdvert.ID = advert.ID;
                                _PopAdvert.InitializeUsage();
                                if (!Directory.Exists(path + "images\\AdImage\\PopImage\\"))
                                {
                                    Directory.CreateDirectory(path + "images\\AdImage\\PopImage\\");
                                }
                                if (!fileoperator.FileDownLoad(path + "images\\AdImage\\PopImage\\" + _PopAdvert.PopImagePath, _PopAdvert.PopImagePath, SeatManageSubsystem.PopAd))
                                {
                                    _PopAdvert = null;
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
                                    //model.PromotionImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(model.AdImagePath, UriKind.RelativeOrAbsolute));
                                    _PromotionAdvert.Add(model);
                                }
                                else
                                {
                                    SeatManage.SeatManageComm.WriteLog.Write(string.Format("推广广告初始化失败：{0}", advert.Num));
                                }
                            }
                            break;
                        case AdType.ReaderAd:
                            {
                                if (_ReaderAdvert != null)
                                {
                                    break;
                                }
                                _ReaderAdvert = ReaderAdvertInfo.ToModel(advert.AdContent);
                                _ReaderAdvert.ID = advert.ID;
                                _ReaderAdvert.InitializeUsage();
                                if (!Directory.Exists(path + "images\\AdImage\\ReaderImage\\"))
                                {
                                    Directory.CreateDirectory(path + "images\\AdImage\\ReaderImage\\");
                                }
                                if (!fileoperator.FileDownLoad(path + "images\\AdImage\\ReaderImage\\" + _ReaderAdvert.ReaderAdImagePath, _ReaderAdvert.ReaderAdImagePath, SeatManageSubsystem.ReaderAd))
                                {
                                    //_ReaderAdvert.ReaderImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(_ReaderAdvert.ReaderAdImagePath, UriKind.RelativeOrAbsolute));
                                    _ReaderAdvert = null;
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
                                    //model.NoteImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(model.NoteImagePath, UriKind.RelativeOrAbsolute));
                                    _SchoolNote.Add(model);
                                }
                                else
                                {
                                    SeatManage.SeatManageComm.WriteLog.Write(string.Format("推广广告初始化失败：{0}", advert.Num));
                                }
                            }
                            break;
                        case AdType.TitleAd:
                            {
                                _TitleAdvert = TitleAdvertInfoV2.ToModel(advert.AdContent);
                                _TitleAdvert.ID = advert.ID;
                                _TitleAdvert.InitializeUsage();
                            }
                            break;
                    }
                }
                _UserGuide = SeatManage.Bll.T_SM_SystemSet.GetUserGuide();
                if (_UserGuide != null)
                {
                    if (!Directory.Exists(path + "images\\AdImage\\UserGuide\\"))
                    {
                        Directory.CreateDirectory(path + "images\\AdImage\\UserGuide\\");
                    }
                    foreach (string file in _UserGuide.ImageFilePath)
                    {

                        if (!fileoperator.FileDownLoad(path + "images\\AdImage\\UserGuide\\" + file, file, SeatManageSubsystem.UserGuide))
                        {
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("使用手册初始化失败"));
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
                if (_PopAdvert != null)
                {
                    error = SeatManage.Bll.AdvertisementOperation.UpdateAdvertUsage(_PopAdvert.Usage);
                    if (error != "")
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传状态失败：{0}", error));
                    }
                    else
                    {
                        _PopAdvert.Usage.Clean();
                    }
                }
                if (_PromotionAdvert.Count > 0)
                {
                    foreach (PromotionAdvertInfo item in _PromotionAdvert)
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
                if (_ReaderAdvert != null)
                {
                    error = SeatManage.Bll.AdvertisementOperation.UpdateAdvertUsage(_ReaderAdvert.Usage);
                    if (error != "")
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传状态失败：{0}", error));
                    }
                    else
                    {
                        _ReaderAdvert.Usage.Clean();
                    }
                }
                if (_TitleAdvert != null)
                {
                    error = SeatManage.Bll.AdvertisementOperation.UpdateAdvertUsage(_TitleAdvert.Usage);
                    if (error != "")
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传状态失败：{0}", error));
                    }
                    else
                    {
                        _TitleAdvert.Usage.Clean();
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
