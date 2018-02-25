using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_TitleAd : INotifyPropertyChanged
    {

        private SeatManage.ClassModel.TitleAdvertInfoV2 _TitleAdvertModel = new SeatManage.ClassModel.TitleAdvertInfoV2();
        /// <summary>
        /// 广告model
        /// </summary>
        public SeatManage.ClassModel.TitleAdvertInfoV2 TitleAdvertModel
        {
            get { return _TitleAdvertModel; }
            set { _TitleAdvertModel = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _TitleAdvertModel.Num; }
            set { _TitleAdvertModel.Num = value; OnPropertyChanged("Num"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _TitleAdvertModel.Name; }
            set { _TitleAdvertModel.Name = value; OnPropertyChanged("Name"); }
        }
        /// <summary>
        /// 冠名
        /// </summary>
        public string TextContent
        {
            get { return _TitleAdvertModel.TextContent; }
            set { _TitleAdvertModel.TextContent = value; OnPropertyChanged("TextContent"); }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_TitleAdvertModel.EffectDate == null || _TitleAdvertModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _TitleAdvertModel.EffectDate = DateTime.Now.Date;
                }
                return _TitleAdvertModel.EffectDate;
            }
            set { _TitleAdvertModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_TitleAdvertModel.EndDate == null || _TitleAdvertModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _TitleAdvertModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _TitleAdvertModel.EndDate;
            }
            set { _TitleAdvertModel.EndDate = value; OnPropertyChanged("EndDate"); }
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

        public bool Save()
        {
            string functionName = "save";
            try
            {
                if (string.IsNullOrEmpty(TitleAdvertModel.Num))
                {
                    ErrorMessage = "编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(TitleAdvertModel.Name))
                {
                    ErrorMessage = "名称不能为空！";
                    return false;
                }
                if (_TitleAdvertModel.TextContent == "")
                {
                    ErrorMessage = "内容不能为空！";
                    return false;
                }
                if (TitleAdvertModel.EffectDate == null)
                {
                    ErrorMessage = "开始时间不能为空！";
                    return false;
                }
                if (TitleAdvertModel.EndDate == null)
                {
                    ErrorMessage = "结束时间不能为空！";
                    return false;
                }
                if (TitleAdvertModel.EndDate < TitleAdvertModel.EffectDate)
                {
                    ErrorMessage = "结束时间要大于开始时间！";
                    return false;
                }
                if (!IsEdit && SeatManage.Bll.AdvertisementOperation.GetAdModel(TitleAdvertModel.Num, SeatManage.EnumType.AdType.TitleAd) != null)
                {
                    ErrorMessage = "已存在存在相同名称或编号的弹窗冠名！";
                    return false;
                }
                TitleAdvertModel.ImageFilePath.Clear();

                string resultstr = "";

                TitleAdvertModel.Type = SeatManage.EnumType.AdType.TitleAd;
                SeatManage.ClassModel.AMS_Advertisement model = new SeatManage.ClassModel.AMS_Advertisement();
                model.Type = SeatManage.EnumType.AdType.TitleAd;
                model.ID = TitleAdvertModel.ID;
                model.Name = TitleAdvertModel.Name;
                model.Num = TitleAdvertModel.Num;
                model.AdContent = TitleAdvertModel.ToXml();
                model.EffectDate = TitleAdvertModel.EffectDate;
                model.EndDate = TitleAdvertModel.EndDate;
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
}
