using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_Receipt : INotifyPropertyChanged
    {
        private SeatManage.ClassModel.PrintReceiptInfo _PrintReceiptModel = new SeatManage.ClassModel.PrintReceiptInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public SeatManage.ClassModel.PrintReceiptInfo PrintReceiptModel
        {
            get { return _PrintReceiptModel; }
            set { _PrintReceiptModel = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _PrintReceiptModel.Num; }
            set
            {
                _PrintReceiptModel.Num = value;
                TemplateItem.TemplateNo = value;
                OnPropertyChanged("Num");
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _PrintReceiptModel.Name; }
            set { _PrintReceiptModel.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_PrintReceiptModel.EffectDate == null || _PrintReceiptModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _PrintReceiptModel.EffectDate = DateTime.Now.Date;
                }
                return _PrintReceiptModel.EffectDate;
            }
            set { _PrintReceiptModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_PrintReceiptModel.EndDate == null || _PrintReceiptModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _PrintReceiptModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _PrintReceiptModel.EndDate;
            }
            set { _PrintReceiptModel.EndDate = value; OnPropertyChanged("EndDate"); }
        }


        private bool _IsEdit = false;
        /// <summary>
        /// 是否是编辑模式
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; OnPropertyChanged("IsEdit"); }
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
        private ViewModelPrintTemplateInfo _TemplateItem = new ViewModelPrintTemplateInfo();
        /// <summary>
        /// 打印子项
        /// </summary>
        public ViewModelPrintTemplateInfo TemplateItem
        {
            get { return _TemplateItem; }
            set { _TemplateItem = value; OnPropertyChanged("TemplateItem"); }
        }
        /// <summary>
        /// 获取
        /// </summary>
        public void Getmodel()
        {
            PrintReceiptModel.TemplateItem.Clear();
            foreach (ViewModelPrintItem item in _TemplateItem.PrintIiemList)
            {
                SeatManage.ClassModel.PrintItem model = new SeatManage.ClassModel.PrintItem();
                model.IsImage = item.IsImage;
                if (model.IsImage)
                {
                    model.ImageHeight = item.ImageHeight * (130 / item.ImageWidth);
                    model.ImageWidth = 130;
                    model.ImagePath = item.ImageName;
                }
                else
                {
                    model.FontSize = item.FontSize;
                    model.IsBold = item.IsBold == "Bold" ? true : false;
                    model.IsItalic = item.IsItalic == "Italic" ? true : false;
                    model.IsImage = item.IsImage;
                    model.TextInfo = item.TextInfo;
                }
                PrintReceiptModel.TemplateItem.Add(model);

            }
        }
        public void ToViewModel()
        {
            TemplateItem.PrintIiemList.Clear();
            foreach (SeatManage.ClassModel.PrintItem item in PrintReceiptModel.TemplateItem)
            {
                ViewModelPrintItem viewmodel = new ViewModelPrintItem();
                viewmodel.IsImage = item.IsImage;
                if (viewmodel.IsImage)
                {
                    viewmodel.ImageInfo = ImageSaveLocation(item.ImagePath);
                }
                else
                {
                    viewmodel.FontSize = item.FontSize;
                    viewmodel.IsBold = item.IsBold ? "Bold" : "Normal";
                    viewmodel.IsItalic = item.IsItalic ? "Italic" : "Normal";
                    viewmodel.IsImage = item.IsImage;
                    viewmodel.TextInfo = item.TextInfo;
                }
                TemplateItem.PrintIiemList.Add(viewmodel);
            }
        }
        /// <summary>
        /// 下载图片并且获取本地图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private BitmapImage ImageSaveLocation(string imageName)
        {

            string filePath = string.Format(@"{0}temp\{1}", AppDomain.CurrentDomain.BaseDirectory, imageName.Split('_')[1]);
            if (!File.Exists(filePath))
            {//如果本地文件不存在，则下载
                SeatManage.Bll.FileOperate fileDownload = new SeatManage.Bll.FileOperate();
                if (!fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.PrintReceiptAd))
                {
                    return
                        null;
                }
            }
            BitmapImage img = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
            return img;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public bool Save()
        {
            try
            {

                if (string.IsNullOrEmpty(PrintReceiptModel.Num))
                {
                    ErrorMessage = "编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(PrintReceiptModel.Name))
                {
                    ErrorMessage = "名称不能为空！";
                    return false;
                }

                if (TemplateItem.PrintIiemList.Count < 1)
                {
                    ErrorMessage = "打印项目不能为空！";
                    return false;
                }

                if (PrintReceiptModel.EffectDate == null)
                {
                    ErrorMessage = "请选择开始日期！";
                    return false;
                }
                if (PrintReceiptModel.EndDate == null)
                {
                    ErrorMessage = "请选择结束日期！";
                    return false;
                }
                if (PrintReceiptModel.EndDate < PrintReceiptModel.EffectDate)
                {
                    ErrorMessage = "结束日期不能小于开始日期！";
                    return false;
                }



                if (!IsEdit && SeatManage.Bll.AdvertisementOperation.GetAdModel(PrintReceiptModel.Num, SeatManage.EnumType.AdType.PrintReceiptAd) != null)
                {
                    ErrorMessage = "存在相同的编号或名称！";
                    return false;
                }
                //添加上传图片
                PrintReceiptModel.ImageFilePath.Clear();

                foreach (ViewModel.ViewModelPrintItem pitem in TemplateItem.PrintIiemList)
                {
                    if (pitem.IsImage)
                    {
                        bool isnew = true;
                        foreach (string samefile in PrintReceiptModel.ImageFilePath)
                        {
                            if (samefile == pitem.ImagePath)
                            {
                                isnew = false;
                                break;
                            }
                        }
                        if (isnew)
                        {
                            PrintReceiptModel.ImageFilePath.Add(pitem.ImagePath);
                        }
                    }
                }

                //TODO:图片上传，优惠券保存
                string result = "";
                SeatManage.Bll.FileOperate fileUpload = new SeatManage.Bll.FileOperate();
                for (int i = 0; i < PrintReceiptModel.ImageFilePath.Count; i++)
                {

                    if (!fileUpload.UpdateFile(PrintReceiptModel.ImageFilePath[i], PrintReceiptModel.Num + "_" + PrintReceiptModel.ImageFilePath[i].Substring(PrintReceiptModel.ImageFilePath[i].LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.PrintReceiptAd))
                    {
                        ErrorMessage = string.Format("上传图片失败！{0}", result);
                        return false;
                    }
                    PrintReceiptModel.ImageFilePath[i] = PrintReceiptModel.Num + "_" + PrintReceiptModel.ImageFilePath[i].Substring(PrintReceiptModel.ImageFilePath[i].LastIndexOf("\\") + 1);
                }
                Getmodel();
                PrintReceiptModel.Type = SeatManage.EnumType.AdType.PrintReceiptAd;
                PrintReceiptModel.AdContent = SeatManage.ClassModel.PrintReceiptInfo.ToXml(PrintReceiptModel);
                SeatManage.ClassModel.AMS_Advertisement model = new SeatManage.ClassModel.AMS_Advertisement();
                model.AdContent = PrintReceiptModel.AdContent;
                model.EffectDate = PrintReceiptModel.EffectDate;
                model.EndDate = PrintReceiptModel.EndDate;
                model.ID = PrintReceiptModel.ID;
                model.ImageFilePath = PrintReceiptModel.ImageFilePath;
                model.Name = PrintReceiptModel.Name;
                model.Num = PrintReceiptModel.Num;
                model.Type = SeatManage.EnumType.AdType.PrintReceiptAd;
                string resultstr = "";
                if (IsEdit)
                {
                    //TODO:更新                  
                    resultstr = SeatManage.Bll.AdvertisementOperation.UpdateAdModel(model);
                }
                else
                {
                    //DOTO:添加
                    resultstr = SeatManage.Bll.AdvertisementOperation.AddAdModel(model);
                }
                if (!string.IsNullOrEmpty(resultstr))
                {
                    ErrorMessage = string.Format("保存失败！{0}", resultstr);
                    return false;
                }
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.viewModel.Message = "保存成功！";
                mbw.viewModel.Type = Code.MessageBoxType.Success;
                mbw.ShowDialog();
                return true;
            }

            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
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

    public class ViewModelPrintTemplateInfo : INotifyPropertyChanged
    {
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private ObservableCollection<ViewModelPrintItem> _PrintIiemList = new ObservableCollection<ViewModelPrintItem>();
        /// <summary>
        /// 项目列表
        /// </summary>
        public ObservableCollection<ViewModelPrintItem> PrintIiemList
        {
            get { return _PrintIiemList; }
            set { _PrintIiemList = value; OnPropertyChanged("PrintIiemList"); }
        }
        private string _TemplateNo = "";
        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateNo
        {
            get { return _TemplateNo; }
            set
            {
                _TemplateNo = value;
                foreach (ViewModelPrintItem item in PrintIiemList)
                {
                    item.TemplateNo = value;
                }
                OnPropertyChanged("TemplateNo");
            }
        }
        /// <summary>
        /// 打印图片信息
        /// </summary>
        public Dictionary<string, string> PrintImages
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (ViewModelPrintItem item in PrintIiemList)
                {
                    if (item.IsImage)
                    {
                        dic.Add(item.ImageName, item.ImagePath);
                    }
                }
                return dic;
            }
        }
        /// <summary>
        /// 打印信息
        /// </summary>
        public string XmlTxt
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<?xml version='1.0' encoding='utf-8'?>");
                sb.Append("<Print>");
                foreach (ViewModelPrintItem item in PrintIiemList)
                {
                    if (item.IsImage || !item.IsImage && !string.IsNullOrEmpty(item.TextInfo))
                    {
                        sb.Append(item.XmlTxt);
                    }
                }
                sb.Append("</Print>");
                return sb.ToString();
            }
        }
        /// <summary>
        /// 向上移动
        /// </summary>
        /// <param name="index"></param>
        public void UpMoveItem(int index)
        {
            string functionName = "UpMoveItem";
            try
            {
                if (index < 1)
                {
                    ErrorMessage = "已移动到队列最前！";
                    return;
                }
                PrintIiemList.Move(index, index - 1);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        /// <summary>
        /// 向下移动
        /// </summary>
        /// <param name="index"></param>
        public void DownMoveItem(int index)
        {
            try
            {
                if (index == PrintIiemList.Count - 1)
                {
                    ErrorMessage = "已移动到队列最后！";
                    return;
                }
                PrintIiemList.Move(index, index + 1);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="index"></param>
        public void DeleteItem(int index)
        {
            string functionName = "DeleteItem";
            try
            {
                PrintIiemList.RemoveAt(index);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="index"></param>
        public void AddItem(ViewModelPrintItem item)
        {
            try
            {
                item.TemplateNo = TemplateNo;
                PrintIiemList.Add(item);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        /// <summary>
        /// 插入项目
        /// </summary>
        /// <param name="index"></param>
        public void InsertItem(ViewModelPrintItem item, int index)
        {
            try
            {
                item.TemplateNo = TemplateNo;
                PrintIiemList.Insert(index, item);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        /// <summary>
        /// 添加图片
        /// </summary>
        public ViewModelPrintItem AddImage(string imagePath)
        {
            try
            {
                ViewModelPrintItem item = new ViewModelPrintItem();
                item.IsImage = true;
                item.ImageInfo = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                return item;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return new ViewModelPrintItem();
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
    public class ViewModelPrintItem : INotifyPropertyChanged
    {

        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }

        private string _TemplateNo = "";
        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateNo
        {
            get { return _TemplateNo; }
            set
            {
                _TemplateNo = value;
                if (IsImage)
                {
                    _ImageName = _TemplateNo + "_" + _ImageInfo.UriSource.LocalPath.Substring(_ImageInfo.UriSource.LocalPath.LastIndexOf("\\") + 1);
                }
                OnPropertyChanged("TemplateNo");
            }
        }

        private string _TextInfo = "";
        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextInfo
        {
            get
            {
                return _TextInfo;
            }
            set
            {
                _TextInfo = Temptext(value);
                OnPropertyChanged("TextInfo");
            }
        }

        private double _ImageHeight;
        /// <summary>
        /// 图片高度
        /// </summary>
        public double ImageHeight
        {
            get { return _ImageHeight; }
        }
        private double _ImageWidth;
        /// <summary>
        /// 图片宽度
        /// </summary>
        public double ImageWidth
        {
            get { return _ImageWidth; }
        }
        private BitmapImage _ImageInfo;
        /// <summary>
        /// 图片
        /// </summary>
        public BitmapImage ImageInfo
        {
            get { return _ImageInfo; }
            set
            {
                _ImageInfo = value;
                _ImageName = _TemplateNo + "_" + _ImageInfo.UriSource.LocalPath.Substring(_ImageInfo.UriSource.LocalPath.LastIndexOf("\\") + 1);
                _ImagePath = _ImageInfo.UriSource.LocalPath;
                _ImageHeight = _ImageInfo.Height;
                _ImageWidth = _ImageInfo.Width;
                OnPropertyChanged("ImageInfo");
            }
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
        }
        /// <summary>
        /// 图片名称
        /// </summary>
        private string _ImageName;
        public string ImageName
        {
            get { return _ImageName; }
        }
        private bool _IsImage = false;
        /// <summary>
        /// 是否是图片
        /// </summary>
        public bool IsImage
        {
            get { return _IsImage; }
            set { _IsImage = value; OnPropertyChanged("IsImage"); OnPropertyChanged("ImageControlVisible"); OnPropertyChanged("TextControlVisible"); }
        }
        private int _FontSize = 8;
        /// <summary>
        /// 文字大小
        /// </summary>
        public int FontSize
        {
            get { return _FontSize; }
            set
            {
                _TextInfo = Temptext(_TextInfo);
                if (value < 1)
                {
                    _FontSize = 1;
                }
                else if (value > 26)
                {
                    _FontSize = 26;
                }
                else
                {
                    _FontSize = value;
                }
                OnPropertyChanged("FontSize"); OnPropertyChanged("TextInfo");
            }
        }
        private bool _IsBold = false;
        /// <summary>
        /// 是否加粗
        /// </summary>
        public string IsBold
        {
            get
            {
                if (_IsBold)
                {
                    return "Bold";
                }
                else
                {
                    return "Normal";
                }
            }
            set
            {
                _TextInfo = Temptext(_TextInfo);
                if (value == "Bold")
                {
                    _IsBold = true;
                }
                else
                {
                    _IsBold = false;
                }
                OnPropertyChanged("IsBold");
                OnPropertyChanged("TextInfo");
            }
        }
        private bool _IsItalic = false;
        /// <summary>
        /// 是否斜体
        /// </summary>
        public string IsItalic
        {
            get
            {
                if (_IsItalic)
                {
                    return "Italic";
                }
                else
                {
                    return "Normal";
                }
            }
            set
            {
                _TextInfo = Temptext(_TextInfo);
                if (value == "Italic")
                {
                    _IsItalic = true;
                }
                else
                {
                    _IsItalic = false;
                }
                OnPropertyChanged("IsItalic");
                OnPropertyChanged("TextInfo");
            }
        }
        /// <summary>
        /// 图片空间显示
        /// </summary>
        public string ImageControlVisible
        {
            get
            {
                if (_IsImage)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
        /// <summary>
        /// 文本空间显示
        /// </summary>
        public string TextControlVisible
        {
            get
            {
                if (!_IsImage)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
        /// <summary>
        /// Xml格式
        /// </summary>
        public string XmlTxt
        {
            get { return ToXml(); }
        }
        public string ToXml()
        {
            try
            {
                if (_IsImage)
                {
                    return string.Format("<Pic width=\"130\" height=\"{0}\">{1}</Pic>",
                        (_ImageHeight * (130 / _ImageWidth)).ToString().Split('.')[0],
                        _ImageName);
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    string[] tmps = _TextInfo.Split('\n');
                    foreach (string str in tmps)
                    {
                        sb.AppendFormat("<Content font=\"宋体\" size=\"{0}\" bold=\"{1}\" italic=\"{2}\">{3}</Content>", _FontSize.ToString(), (_IsBold) ? "Y" : "N", (_IsItalic) ? "Y" : "N", str);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return "";
            }
        }
        private string Temptext(string value)
        {
            string temptxt = value.Replace("\n", "");
            string txt = "";
            string str = "";
            for (int i = 0; i < temptxt.Length; i++)
            {
                str += temptxt.Substring(i, 1);
                if (System.Text.Encoding.Default.GetByteCount(str) >= 200 / (_IsBold ? _FontSize + 1 : _FontSize))
                {
                    if (txt == "")
                    {
                        txt = str;
                    }
                    else
                    {
                        txt += "\n" + str;
                    }
                    str = "";
                }
                else if (i == temptxt.Length - 1)
                {
                    if (txt == "")
                    {
                        txt = str;
                    }
                    else
                    {
                        txt += "\n" + str;
                    }
                    str = "";
                }
            }
            return txt;
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
