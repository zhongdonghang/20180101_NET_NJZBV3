using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace AdvertManageTools.Code
{
    public class SystemVerInfoViewModel : INotifyPropertyChanged
    {
        public SystemVerInfoViewModel()
        {
            
        }

        ObservableCollection<AdvertManage.Model.ProgramUpgradeModel> programList = new ObservableCollection<AdvertManage.Model.ProgramUpgradeModel>();
        /// <summary>
        /// 程序列表
        /// </summary>
        public ObservableCollection<AdvertManage.Model.ProgramUpgradeModel> ProgramList
        {
            get { return programList; }
            set { programList = value;
            OnPropertyChanged("ProgramList");
            }
        }

        public bool GetProgramList()
        {
            try
            {
                List<AdvertManage.Model.ProgramUpgradeModel> list = AdvertManage.BLL.ProgramUpgradeBLL.GetAllProgramInfo();
                programList.Clear();
                foreach (AdvertManage.Model.ProgramUpgradeModel model in list)
                {
                    programList.Add(model);
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
}
