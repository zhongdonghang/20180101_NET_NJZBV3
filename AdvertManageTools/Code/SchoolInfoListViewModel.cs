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
    /// <summary>
    /// 学校列表视图
    /// </summary>
    public class SchoolInfoListViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 数据改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<SchoolInfoViewModel> _SchoolList = new ObservableCollection<SchoolInfoViewModel>();
        /// <summary>
        /// 学校列表
        /// </summary>
        public ObservableCollection<SchoolInfoViewModel> SchoolList
        {
            get { return _SchoolList; }
            set
            {
                _SchoolList = value;
                Changed("SchoolList");
            }
        }
        /// <summary>
        /// 数据获取
        /// </summary>
        public bool DataGet()
        {
            try
            {
                _SchoolList.Clear();
                List<AMS_SchoolModel> schoolinfolist = AMS_SchoolBLL.GetAllSchoolInfo();
                foreach (AMS_SchoolModel school in schoolinfolist)
                {
                    SchoolInfoViewModel schoolVM = new SchoolInfoViewModel();
                    schoolVM.Id = school.Id;
                    schoolVM.Name = school.Name;
                    schoolVM.Number = school.Number;
                    schoolVM.ConnectionString = school.ConnectionString;
                    schoolVM.Describe = school.Describe;
                    schoolVM.DTUip = school.DTUip;
                    if (school.Flag == 1)
                    {
                        schoolVM.Flag = true;
                    }
                    else
                    {
                        schoolVM.Flag = false;
                    }
                    List<AMS_CampusModel> campuslist = AMS_CampusBLL.GetCampusInfoListBySchoolNum(school.Number);
                    List<AMS_DeviceModel> devicelist = AMS_DeviceBLL.GeDeviceModelBySchoolNum(school.Number, false);
                    schoolVM.CampusCount = campuslist.Count;
                    schoolVM.DeviceCount = devicelist.Count;
                    _SchoolList.Add(schoolVM);
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            } 
        }
        
    }
    /// <summary>
    /// 学校信息视图
    /// </summary>
    public class SchoolInfoViewModel : INotifyPropertyChanged
    {
        private int _Id = 0;
        /// <summary>
        /// 学校ID
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
        /// 学校编号
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
        private string _Name = "";
        /// <summary>
        /// 学校名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                Changed("Name");
            }
        }
        private string _DTUip = "";
        /// <summary>
        /// 外网服务器地址
        /// </summary>
        public string DTUip
        {
            get { return _DTUip; }
            set
            {
                _DTUip = value;
                Changed("DTUip");
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
        private string _ConnectionString = "";
        /// <summary>
        /// WCF连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set
            {
                _ConnectionString = value;
                Changed("ConnectionString");
            }
        }
        private int _Flag = 0;
        /// <summary>
        /// 是否启用手机预约
        /// </summary>
        public bool Flag
        {
            get
            {
                if (_Flag == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    _Flag = 1;
                }
                else
                {
                    _Flag = 0;
                }
                Changed("Flag");
            }
        }
        private int _CampusCount = 0;
        /// <summary>
        /// 校区数目
        /// </summary>
        public int CampusCount
        {
            get { return _CampusCount; }
            set
            {
                _CampusCount = value;
                Changed("CampusCount");
            }
        }
        private int _DeviceCount = 0;
        /// <summary>
        /// 设备数目
        /// </summary>
        public int DeviceCount
        {
            get { return _DeviceCount; }
            set
            {
                _DeviceCount = value;
                Changed("DeviceCount");
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
        /// <summary>
        /// 删除学校操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteSchool()
        {
            try
            {
                if (AMS_SchoolBLL.DeleteSchoolInfo(_Id) == AdvertManage.Model.Enum.HandleResult.Failed)
                {
                    throw new Exception("删除学校失败，具体详情请查看日志文件！");
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            } 
        }
        /// <summary>
        /// 更新学校
        /// </summary>
        public bool UpdateSchool()
        {
            try
            {
                AMS_SchoolModel sameschool = AMS_SchoolBLL.GetSchoolInfoByNum(_Number);
                if (sameschool == null || sameschool.Id == _Id)
                {
                    AMS_SchoolModel model = new AMS_SchoolModel();
                    model.Id = _Id;
                    model.Name = _Name;
                    model.Number = _Number;
                    model.ConnectionString = _ConnectionString;
                    model.Describe = _Describe;
                    model.DTUip = _DTUip;
                    model.Flag = _Flag;
                    if (AMS_SchoolBLL.UpdateSchoolInfo(model) == AdvertManage.Model.Enum.HandleResult.Failed)
                    {
                        throw new Exception("更新学校失败，具体详情请查看日志文件！");
                    }
                    return true;
                }
                else
                {
                    throw new Exception("已存在相同的学校编号！");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            } 
        }
        /// <summary>
        /// 添加学校
        /// </summary>
        public bool AddSchool()
        {
            try
            {
                AMS_SchoolModel sameschool = AMS_SchoolBLL.GetSchoolInfoByNum(_Number);
                if (sameschool == null)
                {
                    AMS_SchoolModel model = new AMS_SchoolModel();
                    model.Id = _Id;
                    model.Name = _Name;
                    model.Number = _Number;
                    model.ConnectionString = _ConnectionString;
                    model.Describe = _Describe;
                    model.DTUip = _DTUip;
                    model.Flag = _Flag;
                    if (AMS_SchoolBLL.AddSchoolInfo(model) == AdvertManage.Model.Enum.HandleResult.Failed)
                    {
                        throw new Exception("添加学校失败，具体详情请查看日志文件！");
                    }
                    return true;
                }
                else
                {
                    throw new Exception("已存在相同的学校编号！");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            } 
        }
    }
}
