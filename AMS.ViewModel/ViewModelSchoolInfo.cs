using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
namespace AMS.ViewModel
{
    public class ViewModelSchoolInfo : AMS.ViewModel.ViewModelObject
    {
        private List<SchoolInfo> _SchoolInfoList = null;
        private Visibility _Visibility = Visibility.Visible;
         
        /// <summary>
        /// 学校信息列表，对应ListView显示的内容
        /// </summary>
        public List<SchoolInfo> SchoolInfoList
        {
            get { return _SchoolInfoList; }
            set
            {
                _SchoolInfoList = value;
                OnPropertyChanged("SchoolInfoList");
            }
        }
        /// <summary>
        /// 控制元素是否显示
        /// </summary>
        public Visibility Visibility
        {
            get { return _Visibility; }
            set
            {
                _Visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        /// <summary>
        /// 把学校信息转换成ListView上显示的数据结构
        /// </summary>
        /// <param name="provinceSchools"></param>
        public void ShowSchoolList(List<AMS.Model.AMS_ProvinceSchoolInfo> provinceSchools)
        {
            List<SchoolInfo> schoolInfoList = new List<SchoolInfo>();
            for (int i = 0; i < provinceSchools.Count; i++)
            {
                foreach (AMS.Model.AMS_School school in provinceSchools[i].Schools)
                {
                    SchoolInfo schoolInfo = new SchoolInfo();
                    schoolInfo.Province = provinceSchools[i].ProvinceName;
                    schoolInfo.Id = school.Id;
                    schoolInfo.Name = school.Name;
                    schoolInfo.Number = school.Number;
                    schoolInfo.InstallDate = school.InstallDate;
                    schoolInfo.LinkMan = school.LinkMan;
                    schoolInfo.CampusCount = school.Campus.Count;
                    schoolInfo.InstallMan = school.InstallMan;
                    schoolInfo.Isbook = school.IsSeatBespeak;
                    schoolInfo.InterfaceInfo = school.InterfaceInfo;
                    schoolInfo.CardInfo = school.CardInfo;
                    schoolInfo.ExecuteProgress = school.ExecuteProgress;
                    schoolInfo.Address = school.LinkAddress;
                    foreach (AMS.Model.AMS_Campus campus in school.Campus)
                    {
                        schoolInfo.DeviceCount += campus.Device.Count;
                    }
                    schoolInfoList.Add(schoolInfo);
                }
            }
            SchoolInfoList = schoolInfoList;
        }

    }
    /// <summary>
    /// 学校信息
    /// PS:地址添加到校区信息中
    /// </summary>
    public class SchoolInfo : ViewModelObject
    {
        private int _Id;
        /// <summary>
        /// 学校Id
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }
        private string number;
        /// <summary>
        /// 学校编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; OnPropertyChanged("Number"); }
        }
        private bool isbook;
        /// <summary>
        /// 学校编号
        /// </summary>
        public bool Isbook
        {
            get { return isbook; }
            set { isbook = value; OnPropertyChanged("Isbook"); }
        }
        private string name;
        /// <summary>
        /// 学校名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private string province;
        /// <summary>
        /// 省份
        /// </summary>
        public string Province
        {
            get { return province; }
            set { province = value; OnPropertyChanged("Province"); }
        }
        private int campusCount;
        /// <summary>
        /// 校区数量
        /// </summary>
        public int CampusCount
        {
            get { return campusCount; }
            set { campusCount = value; OnPropertyChanged("CampusCount"); }
        }
        private int deviceCount;
        /// <summary>
        /// 设备数量
        /// </summary>
        public int DeviceCount
        {
            get { return deviceCount; }
            set { deviceCount = value; OnPropertyChanged("DeviceCount"); }
        }
        private string linkMan;
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan
        {
            get { return linkMan; }
            set { linkMan = value; OnPropertyChanged("LinkMan"); }
        }
        private string address;
        /// <summary>
        /// 学校地址
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged("Address"); }
        }
        private string executeProgress;
        /// <summary>
        /// 安装进度
        /// </summary>
        public string ExecuteProgress
        {
            get { return executeProgress; }
            set { executeProgress = value; OnPropertyChanged("ExecuteProgress"); }
        }
        private string installDate;
        /// <summary>
        /// 安装日期
        /// </summary>
        public string InstallDate
        {
            get { return installDate; }
            set { installDate = value; OnPropertyChanged("InstallDate"); }
        }

        private string _InstallMan;
        /// <summary>
        /// 安装人
        /// </summary>
        public string InstallMan
        {
            get { return _InstallMan; }
            set
            {
                _InstallMan = value;
                OnPropertyChanged("InstallMan");
            }
        }

        private string _interfaceInfo;
        /// <summary>
        /// 接口信息
        /// </summary>
        public string InterfaceInfo
        {
            get { return _interfaceInfo; }
            set { _interfaceInfo = value; OnPropertyChanged("InterfaceInfo"); }
        }
        private string _cardInfo;
        /// <summary>
        /// 一卡通信息
        /// </summary>
        public string CardInfo
        {
            get { return _cardInfo; }
            set { _cardInfo = value; OnPropertyChanged("CardInfo"); }
        }
    }
    

    /// <summary>
    /// 设备详细信息
    /// </summary>
    public class CampusInfo : ViewModelObject
    {
        private int id;
        /// <summary>
        /// 校区Id
        /// </summary>
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private string _Number;
        /// <summary>
        /// 校区编号
        /// </summary>
        public string Number
        {
            get { return _Number; }
            set
            {
                _Number = value;
                OnPropertyChanged("Number");
            }
        }
        private string _Name;
        /// <summary>
        /// 校区名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        private int _DeviceCount;
        /// <summary>
        /// 设备数量
        /// </summary>
        public int DeviceCount
        {
            get { return _DeviceCount; }
            set
            {
                _DeviceCount = value;
                OnPropertyChanged("DeviceCount");
            }
        }
         

        private string _Address;
        /// <summary>
        /// 学校地址
        /// </summary>
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                OnPropertyChanged("Address");
            }
        }
    }
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceInfo : ViewModelObject
    {
        private int id;
        private string _Number;
        private string status;
        private string describe;
        private string isUsing;
        private DateTime? caputreTime;
        /// <summary>
        /// 截图时间
        /// </summary>
        public DateTime? CaputreTime
        {
            get { return caputreTime; }
            set { caputreTime = value; }
        }
        /// <summary>
        /// 设备Id
        /// </summary>
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        /// <summary>
        /// 校区编号
        /// </summary>
        public string Number
        {
            get { return _Number; }
            set
            {
                _Number = value;
                OnPropertyChanged("Number");
            }
        }
        /// <summary>
        /// 设备状态
        /// </summary>
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe
        {
            get { return describe; }
            set
            {
                describe = value;
                OnPropertyChanged("Describe");
            }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsDelete
        {
            get { return isUsing; }
            set { isUsing = value; OnPropertyChanged("IsDelete"); }
        }

        private string _CaputrePath;
        /// <summary>
        /// 设备截图路径
        /// </summary>
        public string CaputrePath
        {
            get { return _CaputrePath; }
            set { _CaputrePath = value;
            OnPropertyChanged("CaputrePath");
            }
        }
    }
}
