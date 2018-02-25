using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SeatManage.ClassModel;
using System.Configuration;
using SeatManage.ISystemTerminal.IPOS;

namespace SeatClientLeave.Code
{
    /// <summary>
    /// 离开终端
    /// 用来存放临时数据.
    /// </summary>
    public class LeaveClientObject
    {
        private LeaveClientObject()
        {
            _HardAdvert = SeatManage.Bll.AMS_HardAd.GetHardAd();
            _TitleAdvert = SeatManage.Bll.AMS_TitleAd.GetTitleAdvertInfo();
        }
        /// <summary>
        /// 离开终端配置信息
        /// </summary>
        public  LeaveClientSetting ClientSet = new LeaveClientSetting();
        /// <summary>
        /// 读者进出信息
        /// </summary>
        private SeatManage.ClassModel.ReaderInfo _ReaderInfo = null;

       
        private Dictionary<string, Bitmap> backImgage = null;//背景图片
        private HardAdvertInfo _HardAdvert = null;
        private TitleAdvertInfo _TitleAdvert = null;
        private ClientConfig _clientSetting = new ClientConfig();

       
        private static LeaveClientObject leaveObject = null;
        private static object _object = new object();
        public static LeaveClientObject GetInstance()
        {
            if (leaveObject == null)
            {
                lock (_object)
                {
                    if (leaveObject == null)
                    {
                        return leaveObject = new LeaveClientObject();
                    }
                }
            }
            return leaveObject;
        }
        /// <summary>
        /// 读者进出信息
        /// </summary>
        public SeatManage.ClassModel.ReaderInfo ReaderInfo
        {
            get { return _ReaderInfo; }
            set { _ReaderInfo = value; }
        }
        /// <summary>
        /// 终端设置信息
        /// </summary>
        public ClientConfig ClientSetting
        {
             
            get {
                _clientSetting.SystemResoultion = new Resolution("1024");
                return _clientSetting; }
            set { _clientSetting = value; }
        }
        /// <summary>
        /// 硬广图片
        /// </summary>
        public SeatManage.ClassModel.HardAdvertInfo HardAdvert
        {
            get
            { 
                return _HardAdvert;
            }
             
        }
        /// <summary>
        /// 标题广告
        /// </summary>
        public TitleAdvertInfo TitleAdvert
        {
            get { return _TitleAdvert; } 
        }
        /// <summary>
        /// 读卡器信息
        /// </summary>
        private SeatManage.ISystemTerminal.IPOS.IPOSMethod _ObjCardReader = null;
        /// <summary>
        /// 读卡器信息
        /// </summary>
        public SeatManage.ISystemTerminal.IPOS.IPOSMethod ObjCardReader
        {
            get
            {
                if (_ObjCardReader != null)
                {
                    return _ObjCardReader;
                }
                else
                {
                    if (ConfigurationManager.AppSettings["CardKinds"] == "1")
                    {
                        _ObjCardReader = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("IPOSMethod") as IPOSMethod; //SeatManage.InterfaceFactory.SystemTerminalFactory.CreatePOSMethod();
                        return _ObjCardReader;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        
        /// <summary>
        /// 背景图片资源
        /// </summary>
        public Dictionary<string, Bitmap> BackgroundImagesResource
        {
            get
            {
                if (backImgage == null)
                {
                    backImgage = new Dictionary<string, Bitmap>();
                    string temp = "";
                    try
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory;
                        foreach (string imgKey in _clientSetting.BackImgage.Keys)
                        {
                            temp = string.Format("{0}{1}", path, _clientSetting.BackImgage[imgKey]);
                            backImgage.Add(imgKey, new Bitmap(temp));
                        }
                        return backImgage;
                    }
                    catch (Exception ex)
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("背景图片初始化失败：{0} {1},{2}", _clientSetting.BackImgage[temp], ex.Message, temp));
                        return new Dictionary<string, Bitmap>();
                    }
                }
                return backImgage;
            }
        }
       
       
    }
}
