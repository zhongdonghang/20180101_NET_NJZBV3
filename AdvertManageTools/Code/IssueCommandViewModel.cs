using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace AdvertManageTools.Code
{
    public class IssueCommandViewModel : INotifyPropertyChanged
    {
        public IssueCommandViewModel()
        {
            GetSchoolInfo();
        }

        ObservableCollection<SchoolNodes> _SchoolInfo = new ObservableCollection<SchoolNodes>();
        /// <summary>
        /// 学校信息
        /// </summary>
        public ObservableCollection<SchoolNodes> SchoolInfo
        {
            get { return _SchoolInfo; }
            set
            {
                _SchoolInfo = value;
                OnPropertyChanged("SchoolInfo");
            }
        }
        /// <summary>
        /// 获取学校信息
        /// </summary>
        public void GetSchoolInfo()
        {
            List<AdvertManage.Model.AMS_SchoolModel> list = AdvertManage.BLL.AMS_SchoolBLL.GetAllSchoolInfo();
            foreach (AdvertManage.Model.AMS_SchoolModel model in list)
            {
                SchoolNodes node = new SchoolNodes();
                node.SchoolId = model.Id;
                node.SchoolName = model.Name;
                node.SchoolNumber = model.Number;
                SchoolInfo.Add(node);
            }
        }
        /// <summary>
        /// 下发操作
        /// </summary>
        public bool Issue(int commandId, AdvertManage.Model.Enum.CommandType command)
        {
            try
            {
                foreach (SchoolNodes school in SchoolInfo)
                {
                    if (school.IsChecked)
                    {
                        AdvertManage.Model.AMS_CommandListModel model = new AdvertManage.Model.AMS_CommandListModel();
                        model.Command = command;
                        model.CommandId = commandId;
                        model.FinishFlag = AdvertManage.Model.Enum.CommandHandleResult.Wait;
                        model.ReleaseTime = DateTime.Now;
                        model.SchoolId = school.SchoolId;
                        AdvertManage.BLL.AMS_CommandBLL.AddAMS_CommandList(model);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            } 
        }

        #region 通知事件
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
    /// <summary>
    /// 学校节点
    /// </summary>
    public class SchoolNodes : INotifyPropertyChanged
    {
        bool _IsChecked = false;
        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        string _SchoolName = "";
        /// <summary>
        /// 学校名字
        /// </summary>
        public string SchoolName
        {
            get { return _SchoolName; }
            set
            {
                _SchoolName = value;
                OnPropertyChanged("SchoolName");
            }
        }
        int _SchoolId = -1;
        /// <summary>
        /// 学校Id
        /// </summary>
        public int SchoolId
        {
            get { return _SchoolId; }
            set
            {
                _SchoolId = value;
                OnPropertyChanged("SchoolId");
            }
        }
        string _SchoolNumber = "";
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNumber
        {
            get { return _SchoolNumber; }
            set
            {
                _SchoolNumber = value;
                OnPropertyChanged("SchoolNumber");
            }
        }

        #region 通知事件
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
