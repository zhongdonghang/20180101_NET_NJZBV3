using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using AdvertManage.Model;
using AdvertManage.BLL;
using System.IO;

namespace AdvertManageTools.Code
{
    /// <summary>
    /// 硬广列表
    /// </summary>

    public class HardAdListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<HardAdViewModel> _HardAdList = new ObservableCollection<HardAdViewModel>();
        /// <summary>
        /// 硬广列表
        /// </summary>
        public ObservableCollection<HardAdViewModel> HardAdList
        {
            get { return _HardAdList; }
            set
            {
                _HardAdList = value;
                Changed("HardAdList");
            }
        }
        /// <summary>
        /// 数据获取
        /// </summary>
        public void GetData()
        {
            try
            {
                List<AMS_HardAdModel> list = AMS_HardAdBLL.GetHardAdList();
                _HardAdList.Clear();
                foreach (AMS_HardAdModel model in list)
                {
                    HardAdViewModel hardadVM = new HardAdViewModel();
                    hardadVM.ID = model.ID;
                    hardadVM.Number = model.Number;
                    hardadVM.EffectDate = model.EffectDate;
                    hardadVM.EndDate = model.EndDate;
                    //hardadVM.AdImage.BeginInit();
                    //hardadVM.AdImage.StreamSource = new System.IO.MemoryStream(model.AdImage);
                    //hardadVM.AdImage.EndInit();
                    _HardAdList.Add(hardadVM);
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
    /// <summary>
    /// 硬广vm
    /// </summary>
    public class HardAdViewModel : INotifyPropertyChanged
    {

        private int _ID;
        /// <summary>
        /// Id
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                Changed("ID");
            }
        }

        private string _Number;
        /// <summary>
        /// 编号
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

        private DateTime _EffectDate = ServerDateTime.Now.Value.Date;
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set
            {
                _EffectDate = value;
                Changed("EffectDate");
            }
        }

        private DateTime _EndDate = ServerDateTime.Now.Value.AddMonths(1).Date;
        /// <summary>
        ///结束日期
        /// </summary>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                _EndDate = value;
                Changed("EndDate");
            }
        }

        private BitmapImage _AdImage = new BitmapImage();

        public BitmapImage AdImage
        {
            get { return _AdImage; }
            set
            {
                _AdImage = value;
                Changed("AdImage");
            }
        }
        /// <summary>
        /// 硬广图片二进制流
        /// </summary>

        /// <summary>
        /// 新增一条硬广
        /// </summary>
        public bool AddNewHardAd()
        {
            try
            {
                if (_AdImage.UriSource != null && !string.IsNullOrEmpty(_Number) && (_EndDate > _EffectDate))
                {
                    AMS_HardAdModel model = new AMS_HardAdModel();
                    model.Number = _Number;
                    model.EffectDate = _EffectDate;
                    model.EndDate = _EndDate;
                    FileStream fs = new FileStream(_AdImage.UriSource.OriginalString, FileMode.Open, FileAccess.Read);
                    byte[] btye = new byte[fs.Length];
                    fs.Read(btye, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    model.AdImage = btye;
                    if (AMS_HardAdBLL.AddHardAd(model) == AdvertManage.Model.Enum.HandleResult.Failed)
                    {
                        throw new Exception("添加硬广失败，详情请查看错误日志！");
                    }
                    return true;
                }
                else
                {
                    throw new Exception("信息填写有误，请重新确认！");
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
