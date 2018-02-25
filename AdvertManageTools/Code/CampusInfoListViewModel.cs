using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.Model;
using AdvertManage.BLL;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace AdvertManageTools.Code
{
    public class CampusInfoListViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 学校信息
        /// </summary>
        private SchoolInfoListViewModel _SchoolVM = new SchoolInfoListViewModel();

        private ObservableCollection<ComboxIntItem> _ComBoxItems = new ObservableCollection<ComboxIntItem>();
        /// <summary>
        /// 学校下拉框
        /// </summary>
        public ObservableCollection<ComboxIntItem> ComBoxItems
        {
            get { return _ComBoxItems; }
            set
            {
                _ComBoxItems = value;
                Changed("ComBoxItems");
            }
        }
        private ObservableCollection<CampusInfoViewModel> _CampusList = new ObservableCollection<CampusInfoViewModel>();
        /// <summary>
        /// 校区列表
        /// </summary>
        public ObservableCollection<CampusInfoViewModel> CampusList
        {
            get { return _CampusList; }
            set
            {
                _CampusList = value;
                Changed("CampusList");
            }
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public void ComItemGetData()
        {
            try
            {
                _SchoolVM.DataGet();
                ComboxIntItem si = new ComboxIntItem();
                si.Text = "全部学校";
                si.Value = 0;
                _ComBoxItems.Add(si);
                while (_ComBoxItems.Count > 1)
                {
                    _ComBoxItems.RemoveAt(1);
                }
                foreach (SchoolInfoViewModel schoolvm in _SchoolVM.SchoolList)
                {
                    si = new ComboxIntItem();
                    si.Text = schoolvm.Name;
                    si.Value = schoolvm.Id;
                    _ComBoxItems.Add(si);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 获取校区信息
        /// </summary>
        /// <param name="schoolid">所要查询的学校，输入0为查询全部学校</param>
        public void GetData(int schoolid)
        {
            try
            {
                _CampusList.Clear();
                List<AMS_CampusModel> campuslist = AMS_CampusBLL.GetCampusInfoListBySchoolId(schoolid);
                foreach (AMS_CampusModel campus in campuslist)
                {
                    CampusInfoViewModel campusMV = new CampusInfoViewModel();
                    List<AMS_DeviceModel> devicecount = AMS_DeviceBLL.GeDeviceModelByCampusNum(campus.Number);
                    campusMV.DeviceCount = devicecount.Count;
                    campusMV.Describe = campus.Describe;
                    campusMV.Id = campus.Id;
                    campusMV.Name = campus.Name;
                    campusMV.Number = campus.Number;
                    campusMV.Schoolid = campus.SchoolId;
                    campusMV.Schoolname = campus.SchoolName;
                    campusMV.Schoolnum = campus.SchoolNum;
                    _CampusList.Add(campusMV);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        #region INotifyPropertyChanged 成员

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

        #endregion
    }
    public class CampusInfoViewModel : INotifyPropertyChanged
    {
        private int _Id = 0;
        /// <summary>
        /// 校区ID
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
        /// 校区编号
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
                _Number = _Schoolnum + value;
                Changed("ShortNumber");
            }
        }
        private int _Schoolid = 0;
        /// <summary>
        /// 学校id
        /// </summary>
        public int Schoolid
        {
            get
            {
                return _Schoolid;
            }
            set
            {
                _Schoolid = value;
                Changed("Schoolid");
            }
        }
        private string _Name = "";
        /// <summary>
        /// 校区名称
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
        private string _Schoolnum = "";
        /// <summary>
        /// 学校编号
        /// </summary>
        public string Schoolnum
        {
            get { return _Schoolnum; }
            set
            {
                _Schoolnum = value;
                if (_Number.Length > _Schoolnum.Length)
                {
                    _Number = _Schoolnum + _Number.Substring(_Number.Length - 2);
                }
                Changed("Schoolnum");
            }
        }
        private string _Schoolname = "";
        /// <summary>
        /// 学校姓名
        /// </summary>
        public string Schoolname
        {
            get { return _Schoolname; }
            set
            {
                _Schoolname = value;
                Changed("Schoolname");
            }
        }
        private int _DeviceCount = 0;
        /// <summary>
        /// 触摸屏设备数目
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
        private ObservableCollection<ComboxStringItem> _ComBoxItems = new ObservableCollection<ComboxStringItem>();
        /// <summary>
        /// 学校下拉框
        /// </summary>
        public ObservableCollection<ComboxStringItem> ComBoxItems
        {
            get { return _ComBoxItems; }
            set
            {
                _ComBoxItems = value;
                Changed("ComBoxItems");
            }
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public void ComItemGetData()
        {
            try
            {
                _SchoolVM.DataGet();
                _ComBoxItems.Clear();
                foreach (SchoolInfoViewModel schoolvm in _SchoolVM.SchoolList)
                {
                    ComboxStringItem si = new ComboxStringItem();
                    si.Text = schoolvm.Name;
                    si.Value = schoolvm.Number;
                    _ComBoxItems.Add(si);
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
        /// 添加校区
        /// </summary>
        public bool AddCampus()
        {
            try
            {
                AMS_CampusModel samecampus = AMS_CampusBLL.GetCampusInfoByNum(_Number);
                if (samecampus == null)
                {
                    AMS_CampusModel newcampus = new AMS_CampusModel();
                    newcampus.Describe = _Describe;
                    newcampus.Name = _Name;
                    newcampus.Number = _Number;
                    AMS_SchoolModel schoolid = AMS_SchoolBLL.GetSchoolInfoByNum(_Schoolnum);
                    newcampus.SchoolId = schoolid.Id;
                    if (AMS_CampusBLL.AddCampus(newcampus) == AdvertManage.Model.Enum.HandleResult.Failed)
                    {
                        throw new Exception("添加校区失败，具体详情请查看日志文件！");
                    }
                    return true;
                }
                else
                {
                    throw new Exception("已有重复的校区编号！");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 更新校区
        /// </summary>
        public bool UpdateCampus()
        {
            try
            {
                AMS_CampusModel samecampus = AMS_CampusBLL.GetCampusInfoByNum(_Number);
                if (samecampus == null || samecampus.Id == _Id)
                {
                    AMS_CampusModel newcampus = new AMS_CampusModel();
                    newcampus.Describe = _Describe;
                    newcampus.Id = _Id;
                    newcampus.Name = _Name;
                    newcampus.Number = _Number;
                    newcampus.SchoolId = _Schoolid;
                    if (AMS_CampusBLL.UpdateCampus(newcampus) == AdvertManage.Model.Enum.HandleResult.Failed)
                    {
                        throw new Exception("修改校区失败，具体详情请查看日志文件！");
                    }
                    return true;
                }
                else
                {
                    throw new Exception("已有重复的校区编号！");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 删除校区
        /// </summary>
        public bool DeleteCampus()
        {
            try
            {
                if (AMS_CampusBLL.DeleteCampus(_Id) == AdvertManage.Model.Enum.HandleResult.Failed)
                {
                    throw new Exception("删除校区失败，具体详情请查看日志文件！");
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
        #region INotifyPropertyChanged 成员

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

        #endregion
    }
    public class ComboxStringItem
    {
        private string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private string _Text;

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
    }
    public class ComboxIntItem
    {
        private int _Value;

        public int Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private string _Text;

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
    }
}
