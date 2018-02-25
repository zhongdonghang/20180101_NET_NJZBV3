using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_PrommotionList : INotifyPropertyChanged
    {

        private ObservableCollection<SeatManage.ClassModel.PromotionAdvertInfo> _PromotionList = new ObservableCollection<SeatManage.ClassModel.PromotionAdvertInfo>();

        public ObservableCollection<SeatManage.ClassModel.PromotionAdvertInfo> PromotionList
        {
            get { return _PromotionList; }
            set { _PromotionList = value; OnPropertyChanged("PromotionList"); }
        }

        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        public void GetDate()
        {
            try
            {
                List<SeatManage.ClassModel.AMS_Advertisement> modelList = SeatManage.Bll.AdvertisementOperation.GetAdList(null, SeatManage.EnumType.AdType.PromotionAd);
                PromotionList.Clear();
                foreach (SeatManage.ClassModel.AMS_Advertisement model in modelList)
                {
                    SeatManage.ClassModel.PromotionAdvertInfo view = SeatManage.ClassModel.PromotionAdvertInfo.ToModel(model.AdContent);
                    view.AdContent = model.AdContent;
                    view.ID = model.ID;
                    PromotionList.Add(view);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                SeatManage.SeatManageComm.WriteLog.Write("获取学校通知失败" + ex.Message);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        public void Delete(int index)
        {
            try
            {
                string error = "";
                error = SeatManage.Bll.AdvertisementOperation.DeleteAdModel(new SeatManage.ClassModel.AMS_Advertisement() { ID = PromotionList[index].ID });
                if (error == "")
                {
                    ErrorMessage = "删除成功！";
                    MessageBoxWindow mbw = new MessageBoxWindow();
                    mbw.viewModel.Message = "删除成功！";
                    mbw.viewModel.Type = Code.MessageBoxType.Success;
                    mbw.ShowDialog();
                }
                else
                {
                    ErrorMessage = "删除失败";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                SeatManage.SeatManageComm.WriteLog.Write("删除失败" + ex.Message);
            }
        }



        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
