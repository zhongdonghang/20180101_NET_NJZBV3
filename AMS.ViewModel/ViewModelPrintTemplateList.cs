using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelPrintTemplateList : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelPrintTemplateList";
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private ObservableCollection<AMS.Model.AMS_PrintTemplate> _PrintTemplateList = new ObservableCollection<AMS.Model.AMS_PrintTemplate>();
        /// <summary>
        /// 列表
        /// </summary>
        public ObservableCollection<AMS.Model.AMS_PrintTemplate> PrintTemplateList
        {
            get { return _PrintTemplateList; }
            set { _PrintTemplateList = value; OnPropertyChanged("PrintTemplateList"); }
        }
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.AMS_PrintTemplate> modellist = new List<AMS.Model.AMS_PrintTemplate>();
                //TODO:获取数据
                modellist = AMS.ServiceProxy.IPrintTemplateService.GetPrintTemplate();
                PrintTemplateList.Clear();
                foreach (AMS.Model.AMS_PrintTemplate model in modellist)
                {
                    PrintTemplateList.Add(model);
                }
                return true;
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return false;
            }
        }
    }
}
