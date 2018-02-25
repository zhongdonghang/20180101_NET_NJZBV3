using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelProjectVersionWindow : ViewModelObject
    {
        #region 构造函数
        public ViewModelProjectVersionWindow()
        {
            _ProgramUpgrade = new ObservableCollection<Model.ProgramUpgrade>();
        }
        #endregion

        private static readonly string CLASSNAME = "ViewModelProjectVersionWindow";
        #region 成员
        private ObservableCollection<AMS.Model.ProgramUpgrade> _ProgramUpgrade;

        public ObservableCollection<AMS.Model.ProgramUpgrade> ProgramUpgrade
        {
            get { return _ProgramUpgrade; }
            set { _ProgramUpgrade = value; OnPropertyChanged("ProgramUpgrade"); }
        }

        private string _ErrorMessage = "";

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 获取项目升级记录信息
        /// </summary>
        /// <returns></returns>
        public void GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.ProgramUpgrade> modelList = new List<Model.ProgramUpgrade>();
                modelList = AMS.ServiceProxy.ProjecVersionWindow.GetProjectList();
                ProgramUpgrade.Clear();
                foreach (AMS.Model.ProgramUpgrade model in modelList)
                {
                    ProgramUpgrade.Add(model);
                }
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        #endregion
    }
}
