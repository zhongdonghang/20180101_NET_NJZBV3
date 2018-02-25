using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_MediaPlayerList : INotifyPropertyChanged
    {

        private ObservableCollection<SeatManage.ClassModel.PlaylistInfo> _PlayList = new ObservableCollection<SeatManage.ClassModel.PlaylistInfo>();

        public ObservableCollection<SeatManage.ClassModel.PlaylistInfo> PlayList
        {
            get { return _PlayList; }
            set { _PlayList = value; OnPropertyChanged("PlayList"); }
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
                List<SeatManage.ClassModel.AMS_Advertisement> modelList = SeatManage.Bll.AdvertisementOperation.GetAdList(null, SeatManage.EnumType.AdType.PlaylistAd);
                PlayList.Clear();
                foreach (SeatManage.ClassModel.AMS_Advertisement model in modelList)
                {
                    SeatManage.ClassModel.PlaylistInfo view = SeatManage.ClassModel.PlaylistInfo.ToModel(model.AdContent);
                    view.AdContent = model.AdContent;
                    view.ID = model.ID;
                    PlayList.Add(view);
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
                error = SeatManage.Bll.AdvertisementOperation.DeleteAdModel(new SeatManage.ClassModel.AMS_Advertisement() { ID = PlayList[index].ID });
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
