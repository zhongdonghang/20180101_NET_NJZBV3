using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.IO;

namespace AMS.MediaPlayer.ViewModel
{
    public class ViewModel_CouponsShow : INotifyPropertyChanged
    {
        private SeatManage.ClassModel.CouponsInfo _CouponsModel = new SeatManage.ClassModel.CouponsInfo();
        /// <summary>
        /// model
        /// </summary>
        public SeatManage.ClassModel.CouponsInfo CouponsModel
        {
            get { return _CouponsModel; }
            set { _CouponsModel = value; PropertyValueChanded("NowNum"); PropertyValueChanded("ItemList"); }
        }
        private ObservableCollection<ViewModel_CouponsShowItem> _ItemList = new ObservableCollection<ViewModel_CouponsShowItem>();
        /// <summary>
        /// 显示列表
        /// </summary>
        public ObservableCollection<ViewModel_CouponsShowItem> ItemList
        {
            get { return _ItemList; }
            set { _ItemList = value; PropertyValueChanded("ItemList"); }
        }
        private ViewModel_CouponsShowItem _NowItem = new ViewModel_CouponsShowItem();
        /// <summary>
        /// 当前显示项
        /// </summary>
        public ViewModel_CouponsShowItem NowItem
        {
            get { return _NowItem; }
            set
            {
                _NowItem = value;
                PropertyValueChanded("NowItem");
                PropertyValueChanded("NowNum");
            }
        }
        /// <summary>
        /// 当前项
        /// </summary>
        public string NowNum
        {
            get { return (ItemList.IndexOf(NowItem) + 1) + "/" + ItemList.Count; }
        }

        private SeatManage.ClassModel.AMS_AdvertUsage _Usage = new SeatManage.ClassModel.AMS_AdvertUsage();
        /// <summary>
        /// 查看状态
        /// </summary>
        public SeatManage.ClassModel.AMS_AdvertUsage Usage
        {
            get { return _Usage; }
            set { _Usage = value; }
        }
        /// <summary>
        /// 绑定子项目
        /// </summary>
        public void bindingItem()
        {
            _ItemList.Clear();
            foreach (SeatManage.ClassModel.CouponsInfoItem item in _CouponsModel.PopItemList)
            {
                ViewModel_CouponsShowItem viewModel = new ViewModel_CouponsShowItem();
                viewModel.ItemModel = item;
                viewModel.ShowImage = LoadImage(viewModel.ItemModel.PpoImagePath);
                _ItemList.Add(viewModel);
            }
            if (_ItemList.Count > 0)
            {
                NowItem = _ItemList[0];
            }
        }
        /// <summary>
        /// 初始化使用状态
        /// </summary>
        public void NewUsage()
        {
            _Usage.AdvertID = _CouponsModel.ID;
            _Usage.AdvertNum = _CouponsModel.Num;
            _Usage.AdvertType = _CouponsModel.Type;
            _Usage.WatchCount++;
            foreach (SeatManage.ClassModel.CouponsInfoItem item in _CouponsModel.PopItemList)
            {
                SeatManage.ClassModel.AdvertisementUsage usage = new SeatManage.ClassModel.AdvertisementUsage();
                usage.AdvertNum = item.ID;
                _Usage.ItemUsage.Add(usage.AdvertNum, usage);
            }
            if (_Usage.ItemUsage.Count > 0)
            {
                _Usage.ItemUsage[_CouponsModel.PopItemList[0].ID].WatchCount++;
            }
        }
        /// <summary>
        /// 更新使用状态
        /// </summary>
        public void UpdateUsage()
        {
            try
            {
                string error = SeatManage.Bll.AdvertisementOperation.UpdateAdvertUsage(_Usage);
                if (error != "")
                {
                    SeatManage.SeatManageComm.WriteLog.Write(error);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
            }
        }
        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private BitmapImage LoadImage(string imagePath)
        {
            //二进制流转换成图片，放入image控件中
            try
            {
                string imageName = string.Format("{0}{1}", AMS.MediaPlayer.Code.PlayerSetting.SysPath + "\\CouponsImage\\", imagePath);
                BinaryReader binReader = new BinaryReader(File.Open(imageName, FileMode.Open));
                FileInfo fileInfo = new FileInfo(imageName);
                byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                binReader.Close();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(bytes);
                bitmap.EndInit();

                return bitmap;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 打印优惠券
        /// </summary>
        public void Print()
        {
            if (_NowItem.ItemModel.IsPrint)
            {
                if (!string.IsNullOrEmpty(_NowItem.ItemModel.PrintXml))
                {
                    AMS.MediaPlayer.UI.Code.PrintSlip printer = new AMS.MediaPlayer.UI.Code.PrintSlip(_NowItem.ItemModel.PrintXml);
                    printer.Print();
                    if (_Usage.ItemUsage.ContainsKey(_NowItem.ItemModel.ID))
                    {
                        _Usage.ItemUsage[_NowItem.ItemModel.ID].PrintCount++;
                    }
                }
            }
        }
        /// <summary>
        /// 左移
        /// </summary>
        public void MoveLeft()
        {
            int index = ItemList.IndexOf(_NowItem);
            if (index > 0)
            {
                NowItem = ItemList[index - 1];
                if (_Usage.ItemUsage.ContainsKey(_NowItem.ItemModel.ID))
                {
                    _Usage.ItemUsage[_NowItem.ItemModel.ID].WatchCount++;
                }
            }
        }
        /// <summary>
        /// 右移
        /// </summary>
        public void MoveRight()
        {
            int index = ItemList.IndexOf(_NowItem);
            if (index < ItemList.Count - 1)
            {
                NowItem = ItemList[index + 1];
                if (_Usage.ItemUsage.ContainsKey(_NowItem.ItemModel.ID))
                {
                    _Usage.ItemUsage[_NowItem.ItemModel.ID].WatchCount++;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性值改变时通知绑定对象的方法
        /// </summary>
        /// <param name="propertyName"></param>
        private void PropertyValueChanded(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    /// <summary>
    /// 显示项
    /// </summary>
    public class ViewModel_CouponsShowItem : INotifyPropertyChanged
    {
        private SeatManage.ClassModel.CouponsInfoItem _ItemModel = new SeatManage.ClassModel.CouponsInfoItem();
        /// <summary>
        /// model
        /// </summary>
        public SeatManage.ClassModel.CouponsInfoItem ItemModel
        {
            get { return _ItemModel; }
            set { _ItemModel = value; PropertyValueChanded("ItemModel"); }
        }
        /// <summary>
        /// 是否打印
        /// </summary>
        public string IsPrint
        {
            get
            {
                if (_ItemModel.IsPrint)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
        private BitmapImage _ShowImage = new BitmapImage();
        /// <summary>
        /// 显示图片
        /// </summary>
        public BitmapImage ShowImage
        {
            get { return _ShowImage; }
            set { _ShowImage = value; PropertyValueChanded("ShowImage"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性值改变时通知绑定对象的方法
        /// </summary>
        /// <param name="propertyName"></param>
        private void PropertyValueChanded(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
