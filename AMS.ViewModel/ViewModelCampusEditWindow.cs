using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using AMS.ServiceProxy;

namespace AMS.ViewModel
{
    public class ViewModelCampusEditWindow : ViewModelObject
    {
        AMS.Model.AMS_School _SchoolModel = null;
        AMS.Model.AMS_Campus campusModel = new Model.AMS_Campus();
        private string _CampusNum_CampusNum;
        private Enum.HandleType handle = Enum.HandleType.None;
        /// <summary>
        /// 处理方式
        /// </summary>
        public Enum.HandleType Cmd
        {
            get { return handle; }
            set
            {
                handle = value;
                OnPropertyChanged("Handle");
            }
        }
        private string _ErrorMessage = "";

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                _ErrorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        public ViewModelCampusEditWindow(AMS.Model.AMS_School school)
        {
            SchoolModel = school;
        }

        /// <summary>
        /// 校区信息实体
        /// </summary>
        public AMS.Model.AMS_Campus CampusModel
        {
            get { return campusModel; }
            set
            {
                campusModel = value;
                OnPropertyChanged("CampusModel");
            }
        }
        /// <summary>
        /// 校区所属学校Model
        /// </summary>
        public AMS.Model.AMS_School SchoolModel
        {
            get { return _SchoolModel; }
            set
            {
                _SchoolModel = value;
                OnPropertyChanged("SchoolModel");
            }
        }
        /// <summary>
        /// 校区编号，学校编号部分
        /// </summary>
        public string SchoolNum
        {
            get
            {
                return SchoolModel.Number;
            }
            set
            {
                SchoolModel.Number = value;
                OnPropertyChanged("SchoolNum");
            }
        }
        //private string _CampusNum;
        /// <summary>
        /// 校区编号 校区部分
        /// </summary>
        public string CampusNum
        {
            get
            {
                if (string.IsNullOrEmpty(CampusModel.Number))
                {
                    CampusModel.Number = SchoolModel.Number + (SchoolModel.Campus.Count + 1).ToString("D2");
                }
                return CampusModel.Number.Replace(SchoolModel.Number, "");
            }
            set
            {
                CampusModel.Number = SchoolModel.Number + value;
                OnPropertyChanged("CampusNum");
            }
        }
        /// <summary>
        /// 校区名称
        /// </summary>
        public string CampusName
        {
            get { return CampusModel.Name; }
            set
            {
                CampusModel.Name = value;
                OnPropertyChanged("CampusName");
            }
        }
        /// <summary>
        /// 备注/描述
        /// </summary>
        public string Describe
        {
            get { return CampusModel.Describe; }
            set
            {
                CampusModel.Describe = value;
                OnPropertyChanged("Describe");
            }
        }
        /// <summary>
        /// 确定按钮执行相关操作
        /// </summary>
        /// <returns></returns>
        public bool ButtomSubmit()
        {
            switch (Cmd)
            {
                case Enum.HandleType.Add:
                    return addCampus();
                case Enum.HandleType.Edit:
                    return updateCampusInfo();
                case Enum.HandleType.Delete:
                    return deleteCampusInfo();
            }
            return false;
        }
        /// <summary>
        /// 执行添加校区方法。
        /// </summary>
        /// <returns></returns>
        private bool addCampus()
        {
            if (CheckData())
            {
                try
                {
                    this.campusModel.SchoolId = SchoolModel.Id;
                    string r = SchoolMainWindow.AddCampusInfo(this.CampusModel);
                    if (string.IsNullOrEmpty(r))
                    {
                        ErrorMessage = "";
                        return true;
                    }
                    else
                    {
                        ErrorMessage = r;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改校区
        /// </summary>
        /// <returns></returns>
        private bool updateCampusInfo()
        {
            if (CheckData())
            {
                try
                {
                    this.campusModel.SchoolId = SchoolModel.Id;
                    string r = SchoolMainWindow.UpdateCampusInfo(this.CampusModel);
                    if (string.IsNullOrEmpty(r))
                    {
                        ErrorMessage = "";
                        return true;
                    }
                    else
                    {
                        ErrorMessage = r;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除校区
        /// </summary>
        /// <returns></returns>
        private bool deleteCampusInfo()
        {
            try
            {
                string r = SchoolMainWindow.DeleteCampusInfo(this.CampusModel);
                if (string.IsNullOrEmpty(r))
                {
                    ErrorMessage = "";
                    return true;
                }
                else
                {
                    ErrorMessage = r;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 检查输入项
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(SchoolNum))
            {
                ErrorMessage = "学校编号为空";
                return false;
            }
            if (string.IsNullOrEmpty(CampusModel.Number))
            {
                ErrorMessage = "请输入校区编号";
                return false;
            }
            if (string.IsNullOrEmpty(CampusModel.Name))
            {
                ErrorMessage = "请输入校区名称";
                return false;
            }
            return true;

        }
    }
}
