
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;
using System.Drawing;
using SeatManage.SeatManageComm;
using System.Windows.Media.Imaging;
using System.IO;
namespace AMS.ViewModel
{
    public class ViewModelAdHardEditWindow : ViewModelObject
    {
        #region 构造函数
        public ViewModelAdHardEditWindow()
        {
           
        }
        public ViewModelAdHardEditWindow(AMS_HardAd model)
        {
            _HardAdModel = model;
        }
        #endregion

        #region 私有成员
        private static readonly string CLASSNAME = "ViewModelAdHardEditWindow";
        private ViewModelCustomerListWindow _CustomerList = new ViewModelCustomerListWindow();
        /// <summary>
        /// 客户列表
        /// </summary>
        public ViewModelCustomerListWindow CustomerList
        {
            get { return _CustomerList; }
            set { _CustomerList = value; OnPropertyChanged("CustomerList"); }
        }
        private AMS_HardAd _HardAdModel=new AMS_HardAd();
        private bool _IsEdit = false;
        private string _ErrorMessage = "";
        private int _FormHight = 365;
        private string _TextBoxVisibility = "Visible";

        #endregion
            
        #region 属性
        /// <summary>
        /// 隐藏属性
        /// </summary>
        public string TextBoxVisibility
        {
            get { return _TextBoxVisibility; }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public int FormHight
        {
            get { return _FormHight; }
        }
        /// <summary>
        /// Model
        /// </summary>
        public AMS_HardAd HardAdModel
        {
            get { return _HardAdModel; }
            set { _HardAdModel = value; }
        }
        /// <summary>
        /// 硬广ID
        /// </summary>
        public int ID
        {
            get { return _HardAdModel.ID; }
            set { _HardAdModel.ID = value; OnPropertyChanged("ID"); }
        }
        public byte[] AdAdImageModel
        {
            get { return _HardAdModel.AdImage; }
            set { _HardAdModel.AdImage = value; OnPropertyChanged("AdAdImageModel"); }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId
        {
            get { return _HardAdModel.CustomerId; }
            set { _HardAdModel.CustomerId = value; OnPropertyChanged("CustomerId"); }
        }
        /// <summary>
        /// 硬广名称
        /// </summary>
        public string HardAdName
        {
            get { return _HardAdModel.Name; }
            set { _HardAdModel.Name = value; OnPropertyChanged("HardAdName"); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _HardAdModel.Describe; }
            set { _HardAdModel.Describe = value; OnPropertyChanged("Remark"); }
        }
        /// <summary>
        /// 硬广生效时间
        /// </summary>
        public DateTime? EffectDate
        {
            get { return _HardAdModel.EffectDate; }
            set { _HardAdModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 硬广编号
        /// </summary>
        public string HardAdNo
        {
            get { return _HardAdModel.Number; }
            set { _HardAdModel.Number = value; OnPropertyChanged("HardAdNo"); }
        }
        /// <summary>
        /// 硬广结束时间
        /// </summary>
        public DateTime? EndDate
        {
            get { return _HardAdModel.EndDate; }
            set { _HardAdModel.EndDate = value; OnPropertyChanged("EndDate"); }
        }
        private BitmapImage _AdImage;
        /// <summary>
        /// 硬广图片
        /// </summary>
        public BitmapImage AdImage
        {
            get { return _AdImage; }
            set { _AdImage = value; OnPropertyChanged("AdImage"); }
        }
        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(_HardAdModel.OperatorName))
                {
                    return "编辑人" + User.UserName;
                }
                else
                {
                    return _HardAdModel.OperatorName;
                }
            }

        }

        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set
            {
                _IsEdit = value;
                if (_IsEdit)
                {
                    _TextBoxVisibility = "Collapsed";
                    _FormHight = 600;
                }
                OnPropertyChanged("IsEdit");
                OnPropertyChanged("TextBoxVisibility");
                OnPropertyChanged("FormHight");
            }
        }


        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        #endregion

        #region 方法
        public bool Save()
        {
            string functionName = "SaveHardAd";
            try
            {

                if (_IsEdit)
                {
                    if (string.IsNullOrEmpty(_HardAdModel.Name))
                    {
                        ErrorMessage = "硬广名称不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_HardAdModel.Number))
                    {
                        ErrorMessage = "硬广编号不能为空！";
                        return false;
                    }
                    if (_HardAdModel.CustomerId <0)
                    {
                        ErrorMessage = "客户编号不能为空!";
                        return false;
                    }
                    if (_HardAdModel.EndDate < _HardAdModel.EffectDate)
                    {
                        ErrorMessage = "结束日期不能小于开始日期!";
                        return false;
                    }
                    _HardAdModel.Operator = User.ID;
                    FileStream fs = new FileStream(_AdImage.UriSource.OriginalString, FileMode.Open, FileAccess.Read);
                    byte[] btye = new byte[fs.Length];
                    fs.Read(btye, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    _HardAdModel.AdImage = btye;
                    AMS.ServiceProxy.IHardAdService.UpdateHardAd(_HardAdModel);
                }
                else
                {
                    if (string.IsNullOrEmpty(_HardAdModel.Name))
                    {
                        ErrorMessage = "请填写硬广名称！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_HardAdModel.Number))
                    {
                        ErrorMessage = "请填写硬广编号！";
                        return false;
                    }
                    if(string.IsNullOrEmpty(_HardAdModel.EffectDate.ToString()))
                    {
                        ErrorMessage = "发布日期不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_HardAdModel.EndDate.ToString()))
                    {
                        ErrorMessage = "结束日期不能为空！";
                        return false;
                    }
                    if(_HardAdModel.EndDate<_HardAdModel.EffectDate)
                    {
                       ErrorMessage="结束日期不能小于开始日期";
                       return false;
                    }
                    if (_HardAdModel.CustomerId < 0)
                    {
                        ErrorMessage = "客户编号不能为空!";
                        return false;
                    }
                    _HardAdModel.Operator = User.ID;
                    FileStream fs = new FileStream(_AdImage.UriSource.OriginalString, FileMode.Open, FileAccess.Read);
                    byte[] btye = new byte[fs.Length];
                    fs.Read(btye, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    _HardAdModel.AdImage = btye;
                    string result = AMS.ServiceProxy.IHardAdService.AddNewHardAd(_HardAdModel);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = string.Format("发布失败！{0}", result);
                        return false;
                    }
                }
                return true;
                
            }
            catch (CustomerException ex)
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
        public bool Delete()
        {
            string functionName = "DeleteHardAd";
            try
            {
                AMS.ServiceProxy.IHardAdService.DeleteHardAd(_HardAdModel);
                return true;
            }
            catch (CustomerException ex)
            {

                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return false;
            }
        }
        /// <summary>
        /// 获取硬广对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AMS.Model.AMS_HardAd GetModel(string name)
        {
            string functionName = "GetModel";
            try
            {
                return AMS.ServiceProxy.IHardAdService.GetAdHardModel(name);
            }
            catch (CustomerException ex)
            {

                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return null;
            }
        }
        #endregion

        public void ToListXML()
        {
            Bitmap Logo = BytesToBitmap(_HardAdModel.AdImage);
            _AdImage = BitmapToBitmapImage(Logo);
            OnPropertyChanged("AdImage"); 
        }
        public static Bitmap BytesToBitmap(byte[] bytes)
        {
            //try
            //{
                if (bytes != null)
                {
                    byte[] bytelist = bytes;
                    MemoryStream ms1 = new MemoryStream(bytelist);
                    Bitmap bm = (Bitmap)Image.FromStream(ms1);
                    ms1.Close();
                    return bm;
                }
                else
                {
                    return null;
                }
            //}
            //catch (ArgumentNullException ex)
            //{
            //    throw ex;
            //}
            //catch (ArgumentException ex)
            //{
            //    throw ex;
            //}
        }


        public BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            Bitmap bitmapSource = new Bitmap(bitmap.Width, bitmap.Height);
            int i, j;
            for (i = 0; i < bitmap.Width; i++)
                for (j = 0; j < bitmap.Height; j++)
                {
                    Color pixelColor = bitmap.GetPixel(i, j);
                    Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                    bitmapSource.SetPixel(i, j, newColor);
                }
            MemoryStream ms = new MemoryStream();
            bitmapSource.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(ms.ToArray());
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}
