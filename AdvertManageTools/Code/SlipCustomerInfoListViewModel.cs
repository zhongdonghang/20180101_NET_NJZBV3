using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using AdvertManage.Model;
using AdvertManage.BLL;

namespace AdvertManageTools.Code
{
    /// <summary>
    /// 优惠券列表
    /// </summary>
    public class SlipCustomerInfoListViewModel : INotifyPropertyChanged
    {
        ObservableCollection<SlipCustomerInfoViewModel> _SlipCustomerList = new ObservableCollection<SlipCustomerInfoViewModel>();
        /// <summary>
        /// 优惠劵列表
        /// </summary>
        public ObservableCollection<SlipCustomerInfoViewModel> SlipCustomerList
        {
            get { return _SlipCustomerList; }
            set
            {
                _SlipCustomerList = value;
                Changed("SlipCustomerList");
            }
        }
        /// <summary>
        /// 数据获取
        /// </summary>
        public void GetData()
        {
            try
            {
                List<AMS_SlipCustomerModel> modellist = AMS_SlipCustomerBLL.GetSlipCustomerList();
                _SlipCustomerList.Clear();
                foreach (AMS_SlipCustomerModel model in modellist)
                {
                    SlipCustomerInfoViewModel SlipVM = new SlipCustomerInfoViewModel();
                    SlipVM.Id = model.Id;
                    SlipVM.EffectDate = model.EffectDate;
                    SlipVM.EndDate = model.EndDate;
                    SlipVM.Number = model.Number;
                    SlipVM.IsPrint = model.IsPrint;
                    SlipVM.SlipTemplateXML = model.SlipTemplate;
                    _SlipCustomerList.Add(SlipVM);
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取中的项
        /// </summary>
        /// <returns></returns>
        public List<int> SelectIdList()
        {
            List<int> list = new List<int>();
            foreach (SlipCustomerInfoViewModel vm in _SlipCustomerList)
            {
                if (vm.IsSelect)
                {
                    list.Add(vm.Id);
                }
            }
            return list;
        }
        /// <summary>
        /// 数据改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    /// <summary>
    /// 优惠券类
    /// </summary>
    public class SlipCustomerInfoViewModel : INotifyPropertyChanged
    {
        private bool _IsSelect = false;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelect
        {
            get { return _IsSelect; }
            set
            {
                _IsSelect = value;
                Changed("IsSelect");
            }
        }
        /// <summary>
        /// Id
        /// </summary>
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                Changed("Id");
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        private string _Number;

        public string Number
        {
            get { return _Number; }
            set
            {
                _Number = value;
                Changed("Number");
            }
        }

        /// <summary>
        /// 显示的图片地址
        /// </summary>
        private BitmapImage _ImageUrl;

        public BitmapImage ImageUrl
        {
            get { return _ImageUrl; }
            set
            {
                _ImageUrl = value;
                Changed("ImageUrl");
            }
        }

        /// <summary>
        /// 优惠内容
        /// </summary>
        private SlipTemplateViewModel _SlipTemplate = new SlipTemplateViewModel();

        public SlipTemplateViewModel SlipTemplate
        {
            get { return _SlipTemplate; }
            set
            {
                _SlipTemplate = value;
                Changed("SlipTemplate");
            }
        }
        private string _SlipTemplateXML = "";
        /// <summary>
        /// 打印模板xml
        /// </summary>
        public string SlipTemplateXML
        {
            get { return _SlipTemplateXML; }
            set { _SlipTemplateXML = value; }
        }

        /// <summary>
        /// 客户图片Logo
        /// </summary>
        private BitmapImage _CustomerImage;

        public BitmapImage CustomerImage
        {
            get { return _CustomerImage; }
            set
            {
                _CustomerImage = value;
                Changed("CustomerImage");
            }
        }

        /// <summary>
        /// 校区编号
        /// </summary>
        private string _CampusNum;

        public string CampusNum
        {
            get { return _CampusNum; }
            set
            {
                _CampusNum = value;
                Changed("CampusNum");
            }
        }

        /// <summary>
        /// 生效日期
        /// </summary>
        private DateTime _EffectDate = ServerDateTime.Now.Value;

        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set
            {
                _EffectDate = value;
                Changed("EffectDate");
            }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        private DateTime _EndDate = ServerDateTime.Now.Value.AddMonths(1);

        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                _EndDate = value;
                Changed("EndDate");
            }
        }
        private bool _IsPrint = true;
        /// <summary>
        /// 是否打印凭条
        /// </summary>
        public bool IsPrint
        {
            get { return _IsPrint; }
            set { _IsPrint = value; }
        }
        /// <summary>
        /// 保存
        /// </summary>
        public bool AddSC()
        {
            try
            {
                if (string.IsNullOrEmpty(_Number) || _ImageUrl == null || _CustomerImage == null)
                {
                    throw new Exception("信息填写不完整！");
                }
                else
                {
                    if (_IsPrint && (string.IsNullOrEmpty(_SlipTemplate.MainTitleName) || string.IsNullOrEmpty(_SlipTemplate.PreferentialInfo) || string.IsNullOrEmpty(_SlipTemplate.CustomerInfo)))
                    {
                        throw new Exception("信息填写不完整！");
                    }
                    else
                    {
                        AMS_SlipCustomerModel samemodel = AMS_SlipCustomerBLL.GetSlipCustomerByNum(_Number);
                        if (samemodel != null)
                        {
                            throw new Exception("已存在相同的编号！");
                        }
                        AMS_SlipCustomerModel model = new AMS_SlipCustomerModel();
                        model.Number = _Number;
                        model.EffectDate = _EffectDate.Date;
                        model.EndDate = _EndDate.Date;
                        model.IsPrint = _IsPrint;
                        model.ImageUrl = _Number + "_" + _ImageUrl.UriSource.LocalPath.Substring(_ImageUrl.UriSource.LocalPath.LastIndexOf("\\") + 1);
                        if (_IsPrint)
                        {
                            model.SlipTemplate = _SlipTemplate.ToXml(_Number);
                        }
                        model.CustomerImage = _Number + "_" + _CustomerImage.UriSource.LocalPath.Substring(_CustomerImage.UriSource.LocalPath.LastIndexOf("\\") + 1);
                        FileOperate fo = new FileOperate();
                        if (!fo.UpdateFile(_CustomerImage.UriSource.LocalPath, model.CustomerImage, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer))
                        {
                            throw new Exception("Logo上传失败！");
                        }
                        if (!fo.UpdateFile(_ImageUrl.UriSource.LocalPath, model.ImageUrl, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer))
                        {
                            throw new Exception("图片上传失败！");
                        }
                        if (_IsPrint)
                        {
                            if (_SlipTemplate.LogoImage != null && !fo.UpdateFile(_SlipTemplate.LogoImage.UriSource.LocalPath, _Number + "_" + _SlipTemplate.LogoImage.UriSource.LocalPath.Substring(_SlipTemplate.LogoImage.UriSource.LocalPath.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.SlipCustomer))
                            {
                                throw new Exception("凭条Logo上传失败！");
                            }
                            if (!fo.UpdateFile(_SlipTemplate.JuneberryLogo.UriSource.LocalPath, "南京智佰闻欣logo.png", SeatManage.EnumType.SeatManageSubsystem.SlipCustomer))
                            {
                                throw new Exception("公司logo上传失败！");
                            }
                        }
                        if (AMS_SlipCustomerBLL.AddSlipCustomer(model) == AdvertManage.Model.Enum.HandleResult.Failed)
                        {
                            throw new Exception("优惠劵保存失败！详情请查看日志文件！");
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 数据改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    /// <summary>
    /// 优惠券条目
    /// </summary>
    public class SlipCustomerItemViewModel : INotifyPropertyChanged
    {
        private string _SCItem;
        /// <summary>
        /// 优惠条目
        /// </summary>
        public string SCItem
        {
            get { return _SCItem; }
            set
            {
                _SCItem = value;
                Changed("SCItem");
            }
        }
        /// <summary>
        /// 数据改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    /// <summary>
    /// 打印模板类
    /// </summary>
    public class SlipTemplateViewModel : INotifyPropertyChanged
    {

        private string _MainTitleName;
        /// <summary>
        /// 主标题
        /// </summary>
        public string MainTitleName
        {
            get { return _MainTitleName; }
            set
            {
                _MainTitleName = value;
                Changed("MainTitleName");
            }
        }
        private string _SubtitleName;
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubtitleName
        {
            get { return _SubtitleName; }
            set
            {
                _SubtitleName = value;
                Changed("SubtitleName");
            }
        }
        private BitmapImage _LogoImage;
        /// <summary>
        /// 商户logo
        /// </summary>
        public BitmapImage LogoImage
        {
            get { return _LogoImage; }
            set
            {
                _LogoImage = value;
                Changed("LogoImage");
            }
        }
        private string _PreferentialInfo;
        /// <summary>
        /// 优惠信息
        /// </summary>
        public string PreferentialInfo
        {
            get { return _PreferentialInfo; }
            set
            {
                _PreferentialInfo = value;
                Changed("PreferentialInfo");
            }
        }
        private string _DiscountInfo;
        /// <summary>
        /// 折扣信息
        /// </summary>
        public string DiscountInfo
        {
            get { return _DiscountInfo; }
            set
            {
                _DiscountInfo = value;
                Changed("DiscountInfo");
            }
        }
        private string _PreferentialLaseInfo;
        /// <summary>
        /// 优惠信息续
        /// </summary>
        public string PreferentialLaseInfo
        {
            get { return _PreferentialLaseInfo; }
            set
            {
                _PreferentialLaseInfo = value;
                Changed("PreferentialLaseInfo");
            }
        }

        private ObservableCollection<SlipCustomerItemViewModel> _SCitems = new ObservableCollection<SlipCustomerItemViewModel>();
        /// <summary>
        /// 优惠条目
        /// </summary>
        public ObservableCollection<SlipCustomerItemViewModel> SCitems
        {
            get { return _SCitems; }
            set
            {
                _SCitems = value;
                Changed("SCitems");
            }
        }
        private string _CustomerInfo;
        /// <summary>
        /// 商户信息
        /// </summary>
        public string CustomerInfo
        {
            get { return _CustomerInfo; }
            set
            {
                _CustomerInfo = value;
                Changed("CustomerInfo");
            }
        }
        /// <summary>
        /// 智佰闻欣logo
        /// </summary>
        private BitmapImage _JuneberryLogo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\南京智佰闻欣logo.png"));

        public BitmapImage JuneberryLogo
        {
            get { return _JuneberryLogo; }
            set { _JuneberryLogo = value; }
        }
        /// <summary>
        /// 数据改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// 转换成打印XML模板
        /// </summary>
        public string ToXml(string SCNo)
        {
            StringBuilder PrinterXml = new StringBuilder();
            PrinterXml.Append("<?xml version='1.0' encoding='utf-8'?>");
            PrinterXml.Append("<Print>");
            //添加标题
            PrinterXml.Append(TilteCentor(_MainTitleName));
            //添加副标题
            if (!string.IsNullOrEmpty(_SubtitleName))
            {
                PrinterXml.Append(SubtilteCentor(_SubtitleName));
            }
            //添加Logo
            if (_LogoImage != null)
            {
                double imgheigt = (130 / _LogoImage.Width) * _LogoImage.Height;
                PrinterXml.Append("<Pic width=\"130\" height=\"" + imgheigt + "\">" + SCNo + "_" + _LogoImage.UriSource.LocalPath.Substring(_LogoImage.UriSource.LocalPath.LastIndexOf("\\") + 1) + "</Pic>");
            }
            //添加优惠信息
            PrinterXml.Append(TextToXml(_PreferentialInfo).ToString());
            //添加折扣信息
            if (!string.IsNullOrEmpty(_DiscountInfo))
            {
                PrinterXml.Append(DiscountCentor(_DiscountInfo));
            }
            //添加优惠信息续
            if (!string.IsNullOrEmpty(_PreferentialLaseInfo))
            {
                PrinterXml.Append(TextToXml(_PreferentialLaseInfo).ToString());
            }
            //加1空行
            PrinterXml.Append("<Content font=\"宋体\" size=\"7\" bold=\"N\" italic=\"N\"> </Content>");
            //添加具体优惠信息
            foreach (SlipCustomerItemViewModel scvm in _SCitems)
            {
                if (!string.IsNullOrEmpty(scvm.SCItem))
                {
                    PrinterXml.Append(TextToXml(scvm.SCItem).ToString());
                    //加一空行
                    PrinterXml.Append("<Content font=\"宋体\" size=\"7\" bold=\"N\" italic=\"N\"> </Content>");
                }
            }
            //添加商户信息
            PrinterXml.Append(TextToXml(_CustomerInfo).ToString());
            PrinterXml.Append("<Pic width=\"130\" height=\"23.2\">南京智佰闻欣logo.png</Pic>");
            PrinterXml.Append("</Print>");
            return PrinterXml.ToString();
        }
        #region 私有方法
        /// <summary>
        /// 标题居中
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private static string TilteCentor(string title)
        {
            string text = title;
            int TitleCount = System.Text.Encoding.Default.GetByteCount(title);
            //小于10个字符用20号字体
            if (TitleCount <= 10)
            {
                for (int i = 0; i < (10 - TitleCount) / 2; i++)
                {
                    text = " " + text + "";
                }
                return "<Content font=\"宋体\" size=\"20\" bold=\"Y\" italic=\"N\">" + text + "</Content>";
            }
            else if (TitleCount <= 12)
            {
                for (int i = 0; i < (12 - TitleCount) / 2; i++)
                {
                    text = " " + text + "";
                }
                return "<Content font=\"宋体\" size=\"16\" bold=\"Y\" italic=\"N\">" + text + "</Content>";
            }
            else if (TitleCount <= 14)
            {
                for (int i = 0; i < (14 - TitleCount) / 2; i++)
                {
                    text = " " + text + "";
                }
                return "<Content font=\"宋体\" size=\"14\" bold=\"Y\" italic=\"N\">" + text + "</Content>";
            }
            else
            {
                for (int i = 0; i < (16 - TitleCount) / 2; i++)
                {
                    text = " " + text + "";
                }
                return "<Content font=\"宋体\" size=\"12\" bold=\"Y\" italic=\"N\">" + text + "</Content>";
            }
        }
        /// <summary>
        /// 副标题居中
        /// </summary>
        /// <param name="SubtilteCentor"></param>
        /// <returns></returns>
        private static string SubtilteCentor(string SubtilteCentor)
        {
            string text = SubtilteCentor;
            int SubtilteCount = System.Text.Encoding.Default.GetByteCount(SubtilteCentor);
            for (int i = 0; i < (16 - SubtilteCount) / 2; i++)
            {
                text = " " + text + "";
            }
            return "<Content font=\"宋体\" size=\"12\" bold=\"Y\" italic=\"N\">" + text + "</Content>";
        }
        /// <summary>
        /// 文本转换打印模板
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static StringBuilder TextToXml(string text)
        {

            StringBuilder sb = new StringBuilder();
            string tmp = text;
            tmp = tmp.Replace("\r\n", "_");
            string[] tmps = tmp.Split('_');
            string t = "";
            foreach (string str in tmps)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    t = t + str.Substring(i, 1);
                    if (System.Text.Encoding.Default.GetByteCount(t) >= 27)
                    {
                        sb.Append("<Content font=\"宋体\" size=\"7\" bold=\"N\" italic=\"N\">" + t + "</Content>");
                        t = "";
                    }
                    else if (str.Length == (i + 1))
                    {
                        sb.Append("<Content font=\"宋体\" size=\"7\" bold=\"N\" italic=\"N\">" + t + "</Content>");
                        t = "";
                    }
                }
            }
            return sb;
        }
        /// <summary>
        /// 折扣居中
        /// </summary>
        /// <param name="SubtilteCentor"></param>
        /// <returns></returns>
        private static string DiscountCentor(string discount)
        {
            string text = discount;
            int SubtilteCount = System.Text.Encoding.Default.GetByteCount(discount);
            for (int i = 0; i < (16 - SubtilteCount) / 2; i++)
            {
                text = " " + text + "";
            }
            return "<Content font=\"宋体\" size=\"12\" bold=\"Y\" italic=\"N\">" + text + "</Content>";
        }
        #endregion
    }
}
