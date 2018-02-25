using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using AdvertManage.Model;
using AdvertManage.BLL;
using System.Collections.ObjectModel;

namespace AdvertManageTools.Code
{
    public class DeviceInfoListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DeviceInfoViewModel> _DeviceList = new ObservableCollection<DeviceInfoViewModel>();
        /// <summary>
        /// 设备列表
        /// </summary>
        public ObservableCollection<DeviceInfoViewModel> DeviceList
        {
            get { return _DeviceList; }
            set
            {
                _DeviceList = value;
                Changed("DeviceList");
            }
        }
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <param name="schoolid"></param>
        /// <param name="campusid"></param>
        public void DateGet(string schoolno, string campusno)
        {
            try
            {
                List<AMS_DeviceModel> deviceinfolist = new List<AMS_DeviceModel>();
                if (string.IsNullOrEmpty(schoolno))
                {
                    deviceinfolist = AMS_DeviceBLL.GeDeviceModelByCampusNum(null);
                }
                else
                {
                    if (string.IsNullOrEmpty(campusno))
                    {
                        deviceinfolist = AMS_DeviceBLL.GeDeviceModelBySchoolNum(schoolno, false);
                    }
                    else
                    {
                        deviceinfolist = AMS_DeviceBLL.GeDeviceModelByCampusNum(campusno);
                    }
                }
                _DeviceList.Clear();
                DateTime nowdt = ServerDateTime.Now.Value;
                foreach (AMS_DeviceModel device in deviceinfolist)
                {
                    DeviceInfoViewModel deviceVM = new DeviceInfoViewModel();
                    deviceVM.Campusid = device.CampusId;
                    deviceVM.Campusname = device.CampusName;
                    deviceVM.Campusnumber = device.CampusNumber;
                    deviceVM.Caputrepath = device.CaputrePath;
                    if (device.CaputreTime != null)
                    {
                        deviceVM.LastCaputreTime = (nowdt - device.CaputreTime.Value).TotalMinutes.ToString("0.0") + "分钟前";
                    }
                    deviceVM.Caputretime = device.CaputreTime;
                    deviceVM.Describe = device.Describe;
                    deviceVM.Flag = device.Flag;
                    deviceVM.Id = device.Id;
                    deviceVM.Isdel = device.IsDel;
                    deviceVM.Number = device.Number;
                    deviceVM.Schoolid = device.SchoolId;
                    deviceVM.Schoolnumber = device.SchoolNumber;
                    deviceVM.Schooname = device.SchooName;
                    _DeviceList.Add(deviceVM);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 学校信息
        /// </summary>
        private SchoolInfoListViewModel _SchoolVM = new SchoolInfoListViewModel();
        /// <summary>
        /// 下拉框
        /// </summary>
        private ObservableCollection<ComboxStringItem> _SchoolComBoxItems = new ObservableCollection<ComboxStringItem>();
        /// <summary>
        /// 学校下拉框
        /// </summary>
        public ObservableCollection<ComboxStringItem> SchoolComBoxItems
        {
            get { return _SchoolComBoxItems; }
            set
            {
                _SchoolComBoxItems = value;
                Changed("SchoolComBoxItems");
            }
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public void SchoolComItemGetData()
        {
            try
            {
                _SchoolVM.DataGet();
                ComboxStringItem si = new ComboxStringItem();
                si.Text = "全部学校";
                si.Value = "";
                _SchoolComBoxItems.Add(si);
                while (_SchoolComBoxItems.Count > 1)
                {
                    _SchoolComBoxItems.RemoveAt(1);
                }
                foreach (SchoolInfoViewModel schoolvm in _SchoolVM.SchoolList)
                {
                    si = new ComboxStringItem();
                    si.Text = schoolvm.Name;
                    si.Value = schoolvm.Number;
                    _SchoolComBoxItems.Add(si);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 校区信息
        /// </summary>
        private CampusInfoListViewModel _CampusVM = new CampusInfoListViewModel();
        private ObservableCollection<ComboxStringItem> _CampusComBoxItems = new ObservableCollection<ComboxStringItem>();
        /// <summary>
        /// 校区绑定
        /// </summary>
        public ObservableCollection<ComboxStringItem> CampusComBoxItems
        {
            get { return _CampusComBoxItems; }
            set
            {
                _CampusComBoxItems = value;
                Changed("CampusComBoxItems");
            }
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public void CampusComItemGetData(string schoolNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(schoolNo))
                {
                    AMS_SchoolModel schoolid = AMS_SchoolBLL.GetSchoolInfoByNum(schoolNo);
                    _CampusVM.GetData(schoolid.Id);
                    ComboxStringItem si = new ComboxStringItem();
                    si.Text = "全部校区";
                    si.Value = "";
                    _CampusComBoxItems.Add(si);
                    while (_CampusComBoxItems.Count > 1)
                    {
                        _CampusComBoxItems.RemoveAt(1);
                    }
                    foreach (CampusInfoViewModel campuslvm in _CampusVM.CampusList)
                    {
                        si = new ComboxStringItem();
                        si.Text = campuslvm.Name;
                        si.Value = campuslvm.Number;
                        _CampusComBoxItems.Add(si);
                    }
                }
                else
                {
                    ComboxStringItem si = new ComboxStringItem();
                    si.Text = "全部校区";
                    si.Value = "";
                    _CampusComBoxItems.Add(si);
                    while (_CampusComBoxItems.Count > 1)
                    {
                        _CampusComBoxItems.RemoveAt(1);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
    public class DeviceInfoViewModel : INotifyPropertyChanged
    {
        private int _Id = 0;
        /// <summary>
        /// 设备id
        /// </summary>

        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                Changed("Id");
            }
        }
        private string _Number = "";
        /// <summary>
        /// 设备编号
        /// </summary>
        public string Number
        {
            get { return _Number; }
            set
            {
                _Number = value;
                Changed("Number");
            }
        }
        private int _Campusid = 0;
        /// <summary>
        /// 校区ID
        /// </summary>
        public int Campusid
        {
            get { return _Campusid; }
            set
            {
                _Campusid = value;
                Changed("Campusid");
            }
        }

        private bool? _Isdel = false;
        /// <summary>
        /// 是否被删除
        /// </summary>
        public bool? Isdel
        {
            get { return _Isdel; }
            set
            {
                _Isdel = value;
                Changed("Isdel");
            }
        }
        private bool? _Flag = true;
        /// <summary>
        /// 是否有更新
        /// </summary>
        public bool? Flag
        {
            get { return _Flag; }
            set
            {
                _Flag = value;
                Changed("Flag");
            }
        }
        private string _Describe = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Describe
        {
            get { return _Describe; }
            set
            {
                _Describe = value;
                Changed("Describe");
            }
        }
        private string _Caputrepath = "";
        /// <summary>
        /// 截图地址
        /// </summary>
        public string Caputrepath
        {
            get { return _Caputrepath; }
            set
            {
                _Caputrepath = value;
                Changed("_Caputrepath");
            }
        }
        private string _LastCaputreTime;
        /// <summary>
        /// 最后设备状态时间
        /// </summary>
        public string LastCaputreTime
        {
            get { return _LastCaputreTime; }
            set
            {
                _LastCaputreTime = value;
                Changed("LastCaputreTime");
            }
        }
        private DateTime? _Caputretime;
        /// <summary>
        /// 截图时间
        /// </summary>
        public DateTime? Caputretime
        {
            get { return _Caputretime; }
            set
            {
                _Caputretime = value;
                Changed("Caputretime");
            }
        }
        private int _Schoolid = 0;
        /// <summary>
        /// 学校id
        /// </summary>
        public int Schoolid
        {
            get { return _Schoolid; }
            set
            {
                _Schoolid = value;
                Changed("Schoolid");
            }
        }
        private string _Schoolnumber = "";
        /// <summary>
        /// 学校编号
        /// </summary>
        public string Schoolnumber
        {
            get { return _Schoolnumber; }
            set
            {
                _Schoolnumber = value;
                if (_Number.Length > _Schoolnumber.Length)
                {
                    _Number = _Schoolnumber + _Number.Substring(_Number.Length - 2);
                }
                Changed("Schoolnumber");
            }
        }
        /// <summary>
        /// 短编号
        /// </summary>
        public string ShortNumber
        {
            get
            {
                if (_Number.Length > 2)
                {
                    return _Number.Substring(_Number.Length - 2);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                _Number = _Schoolnumber + value;
                Changed("ShortNumber");
            }
        }
        private string _Schooname = "";
        /// <summary>
        /// 学校名称
        /// </summary>
        public string Schooname
        {
            get { return _Schooname; }
            set
            {
                _Schooname = value;
                Changed("Schooname");
            }
        }
        private string _Campusname = "";
        /// <summary>
        /// 校区名称
        /// </summary>
        public string Campusname
        {
            get { return _Campusname; }
            set
            {
                _Campusname = value;
                Changed("Campusname");
            }
        }
        private string _Campusnumber = "";
        /// <summary>
        /// 校区编号
        /// </summary>
        public string Campusnumber
        {
            get { return _Campusnumber; }
            set
            {
                _Campusnumber = value;
                Changed("Campusnumber");
            }
        }
        /// <summary>
        /// 学校信息
        /// </summary>
        private SchoolInfoListViewModel _SchoolVM = new SchoolInfoListViewModel();
        /// <summary>
        /// 下拉框
        /// </summary>
        private ObservableCollection<ComboxStringItem> _SchoolComBoxItems = new ObservableCollection<ComboxStringItem>();
        /// <summary>
        /// 学校下拉框
        /// </summary>
        public ObservableCollection<ComboxStringItem> SchoolComBoxItems
        {
            get { return _SchoolComBoxItems; }
            set
            {
                _SchoolComBoxItems = value;
                Changed("SchoolComBoxItems");
            }
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public void SchoolComItemGetData()
        {
            try
            {
                _SchoolVM.DataGet();
                _SchoolComBoxItems.Clear();
                foreach (SchoolInfoViewModel schoolvm in _SchoolVM.SchoolList)
                {
                    ComboxStringItem si = new ComboxStringItem();
                    si.Text = schoolvm.Name;
                    si.Value = schoolvm.Number;
                    _SchoolComBoxItems.Add(si);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 校区信息
        /// </summary>
        private CampusInfoListViewModel _CampusVM = new CampusInfoListViewModel();
        private ObservableCollection<ComboxIntItem> _CampusComBoxItems = new ObservableCollection<ComboxIntItem>();
        /// <summary>
        /// 校区绑定
        /// </summary>
        public ObservableCollection<ComboxIntItem> CampusComBoxItems
        {
            get { return _CampusComBoxItems; }
            set
            {
                _CampusComBoxItems = value;
                Changed("CampusComBoxItems");
            }
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public void CampusComItemGetData(string schoolNo)
        {
            try
            {
                AMS_SchoolModel schoolid = AMS_SchoolBLL.GetSchoolInfoByNum(schoolNo);
                _CampusVM.GetData(schoolid.Id);
                _CampusComBoxItems.Clear();
                if (_CampusVM.CampusList.Count > 0)
                {
                    foreach (CampusInfoViewModel campuslvm in _CampusVM.CampusList)
                    {
                        ComboxIntItem si = new ComboxIntItem();
                        si.Text = campuslvm.Name;
                        si.Value = campuslvm.Id;
                        _CampusComBoxItems.Add(si);
                    }
                }
                else
                {
                    ComboxIntItem si = new ComboxIntItem();
                    si.Text = "请先添加此学校的校区";
                    si.Value = 0;
                    _CampusComBoxItems.Add(si);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 添加设备
        /// </summary>
        public bool AddDevice()
        {
            try
            {
                AMS_DeviceModel samedevice = AMS_DeviceBLL.GetDevicebyNo(_Number);
                if (samedevice == null)
                {
                    AMS_DeviceModel newdevice = new AMS_DeviceModel();
                    if (_Campusid == 0)
                    {
                        throw new Exception("请选择校区！");
                    }
                    newdevice.CampusId = _Campusid;
                    newdevice.IsDel = _Isdel;
                    newdevice.Number = _Number;
                    newdevice.Flag = _Flag;
                    newdevice.Describe = _Describe;
                    if (AMS_DeviceBLL.AddDeviceModel(newdevice) == AdvertManage.Model.Enum.HandleResult.Failed)
                    {
                        throw new Exception("设备添加失败！详情请查看日志文件");
                    }
                    return true;
                }
                else
                {
                    throw new Exception("该设备编号已存在！");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 更新设备
        /// </summary>
        public bool UpdateDevice()
        {
            try
            {
                AMS_DeviceModel samedevice = AMS_DeviceBLL.GetDevicebyNo(_Number);
                if (samedevice == null || samedevice.Id == _Id)
                {
                    AMS_DeviceModel newdevice = new AMS_DeviceModel();
                    newdevice.CampusId = _Campusid;
                    newdevice.Id = _Id;
                    newdevice.CaputrePath = _Caputrepath;
                    newdevice.CaputreTime = _Caputretime;
                    newdevice.IsDel = _Isdel;
                    newdevice.Number = _Number;
                    newdevice.Flag = true;
                    newdevice.Describe = _Describe;
                    if (AMS_DeviceBLL.UpdateDeviceModel(newdevice) == AdvertManage.Model.Enum.HandleResult.Failed)
                    {
                        throw new Exception("设备更新失败！详情请查看日志文件");
                    }
                    return true;
                }
                else
                {
                    throw new Exception("该设备编号已存在！");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }


        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
