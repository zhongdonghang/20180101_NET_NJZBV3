using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelProvinceEditWindow : ViewModelObject
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewModelProvinceEditWindow()
        {
            _ProvinceModel = new Model.AMS_Province();
        }
        #endregion

        #region 私有成员
        /// <summary>
        /// 类名
        /// </summary>
        private static readonly string CLASSNAME = "ViewModelProvinceEditWindow";
        /// <summary>
        /// Model
        /// </summary>
        private AMS.Model.AMS_Province _ProvinceModel;


        /// <summary>
        /// 错误信息
        /// </summary>
        private string _ErrorMessage = "";
        /// <summary>
        /// 是否更新
        /// </summary>
        private bool _IsEdit = false;
        #endregion

        #region 属性
        public AMS.Model.AMS_Province ProvinceModel
        {
            get { return _ProvinceModel; }
            set { _ProvinceModel = value; OnPropertyChanged("ProvinceModel"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string ProvinceNameTxt
        {
            get { return _ProvinceModel.ProvinceName; }
            set { _ProvinceModel.ProvinceName = value; OnPropertyChanged("ProvinceNameTxt"); }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; OnPropertyChanged("IsEdit"); }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            string functionName = "Save";
            try
            {
                if (string.IsNullOrEmpty(_ProvinceModel.ProvinceName))
                {
                    ErrorMessage = "添加的名称不能为空！";
                    return false;
                }
                string result = "";
                if (_IsEdit)
                {
                    //TODO:更新操作
                    result = AMS.ServiceProxy.IProvinceService.UpdataProvince(_ProvinceModel);
                }
                else
                {
                    //TODO:添加省份操作
                    result = AMS.ServiceProxy.IProvinceService.AddNewProvince(_ProvinceModel);
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
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string functionName = "Delete";
            try
            {
                //TODO:s删除操作
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
        #endregion
    }
}
