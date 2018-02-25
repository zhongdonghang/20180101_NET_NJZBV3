using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.ViewModel
{
    public class ViewModelCampusInfoDetail : ViewModelObject
    {
        private CampusInfo _CampusDetail;
        private List<DeviceInfo> _DeviceList;
        private System.Windows.Visibility _Visibility = System.Windows.Visibility.Collapsed;
        /// <summary>
        /// 是否显示
        /// </summary>
        public System.Windows.Visibility Visibility
        {
            get { return _Visibility; }
            set
            {
                _Visibility = value;
                OnPropertyChanged("Visibility");
            }
        }
        /// <summary>
        /// 校区详细信息
        /// </summary>
        public CampusInfo CampusDetail
        {
            get { return _CampusDetail; }
            set
            {
                _CampusDetail = value;
                OnPropertyChanged("CampusDetail");
            }
        }
        /// <summary>
        /// 设备列表
        /// </summary>
        public List<DeviceInfo> DeviceList
        {
            get { return _DeviceList; }
            set
            {
                _DeviceList = value;
                OnPropertyChanged("DeviceList");
            }
        }
        /// <summary>
        /// 把学校信息转换为可以显示的信息
        /// </summary>
        /// <param name="campus"></param>
        public void ShowCampusDetail(AMS.Model.AMS_Campus campus)
        {
            CampusInfo campusDetail = new CampusInfo();
            campusDetail.Id = campus.Id;
            campusDetail.Name = campus.Name;
            campusDetail.Number = campus.Number;
            campusDetail.Address = campus.Address;
            campusDetail.DeviceCount = campus.Device.Count;
            this.CampusDetail = campusDetail;
        }

        /// <summary>
        /// 显示设备list信息
        /// </summary>
        /// <param name="campus"></param>
        public void ShowDeviceList(AMS.Model.AMS_Campus campus)
        {
            List<DeviceInfo> devices = new List<DeviceInfo>();
            foreach (AMS.Model.AMS_Device d in campus.Device)
            {
                DeviceInfo di = new DeviceInfo();
                di.Id = d.Id;
                di.IsDelete = d.IsDel == true ? "停用" : "启用";
                di.Number = d.Number;
                di.Status = d.Flag == true ? "未获取" : "已更新";
                di.Describe = d.Describe;
                di.CaputrePath = d.CaputrePath;
                di.CaputreTime = d.CaputreTime;
                devices.Add(di);
            }
            DeviceList = devices;

        }


    }
}
