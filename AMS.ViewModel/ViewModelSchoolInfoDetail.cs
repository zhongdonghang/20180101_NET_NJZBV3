using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.ViewModel
{
    public class ViewModelSchoolInfoDetail : ViewModelObject
    {
        private SchoolInfo _SchoolDetail;
        private List<CampusInfo> _CampusList;
        private System.Windows.Visibility _Visibility = System.Windows.Visibility.Collapsed;
        /// <summary>
        /// 用户控件是否显示
        /// </summary>
        public System.Windows.Visibility Visibility
        {
            get { return _Visibility; }
            set { _Visibility = value;
            OnPropertyChanged("Visibility");
            }
        }
        /// <summary>
        /// 具体的学校
        /// </summary>
        public SchoolInfo SchoolDetail
        {
            get { return _SchoolDetail; }
            set
            {
                _SchoolDetail = value;
                OnPropertyChanged("SchoolDetail");
            }
        }
        /// <summary>
        /// 校区列表
        /// </summary>
        public List<CampusInfo> CampusList
        {
            get { return _CampusList; }
            set
            {
                _CampusList = value;
                OnPropertyChanged("CampusList");
            }
        }
        /// <summary>
        /// 学校信息转换成要显示的列表并显示
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public List<CampusInfo> ConvertToCampusList(AMS.Model.AMS_School school)
        {
            List<CampusInfo> campuses = new List<CampusInfo>();
            for (int i = 0; i < school.Campus.Count; i++)
            { 
                CampusInfo campus = new CampusInfo();
                campus.Id = school.Campus[i].Id;
                campus.Name = school.Campus[i].Name;
                campus.Number = school.Campus[i].Number;
                campus.Address = school.Campus[i].Address;
                campus.DeviceCount = school.Campus[i].Device.Count;
                campuses.Add(campus); 
            }
            CampusList = campuses;
            return campuses;
        }
    }
}
