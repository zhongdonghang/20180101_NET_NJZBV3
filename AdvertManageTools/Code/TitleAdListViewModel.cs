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
    public class TitleAdListViewModel : INotifyPropertyChanged
    {
        ObservableCollection<TitleAdViewModel> _TitleAdList = new ObservableCollection<TitleAdViewModel>();
        /// <summary>
        /// 广告列表
        /// </summary>
        public ObservableCollection<TitleAdViewModel> TitleAdList
        {
            get { return _TitleAdList; }
            set { _TitleAdList = value; Changed("TitleAdList"); }
        }
        /// <summary>
        /// 数据获取
        /// </summary>
        public void GetData()
        {
            try
            {
                _TitleAdList.Clear();
                List<AMS_TitleAdModel> modellist = AMS_TitleAdBLL.GetTitleAd();
                foreach (AMS_TitleAdModel model in modellist)
                {
                    TitleAdViewModel titleVM = new TitleAdViewModel();
                    titleVM.Id = model.Id;
                    titleVM.EffectDate = model.EffectDate;
                    titleVM.EndDate = model.EndDate;
                    titleVM.AdContent = model.AdContent;
                    _TitleAdList.Add(titleVM);
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
    public class TitleAdViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 广告Id
        /// </summary>
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; Changed("Id"); }
        }
        /// <summary>
        /// 生效时间
        /// </summary>
        private DateTime _EffectDate=ServerDateTime.Now.Value.Date;

        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; Changed("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime _EndDate = ServerDateTime.Now.Value.AddMonths(1).Date;

        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; Changed("EndDate"); }
        }
        /// <summary>
        /// 内容
        /// </summary>
        private string _AdContent;

        public string AdContent
        {
            get { return _AdContent; }
            set { _AdContent = value; Changed("AdContent"); }
        }
        /// <summary>
        /// 新增广告
        /// </summary>
        public bool AddNewTitle()
        {
            try
            {
                if (!string.IsNullOrEmpty(_AdContent) && (_EndDate > _EffectDate))
                {
                    AMS_TitleAdModel model = new AMS_TitleAdModel();
                    model.AdContent = _AdContent;
                    model.EffectDate = _EffectDate;
                    model.EndDate = _EndDate;
                    if (AMS_TitleAdBLL.AddTitleAd(model) == AdvertManage.Model.Enum.HandleResult.Failed)
                    {
                        throw new Exception("发布失败！详情请查看日志文件！");
                    }
                    return true;
                }
                else
                {
                    throw new Exception("填写内容有误，请重新检查！");
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
