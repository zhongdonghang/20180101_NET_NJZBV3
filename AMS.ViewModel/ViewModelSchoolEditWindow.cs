using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AMS.ServiceProxy;

namespace AMS.ViewModel
{
    public class ViewModelSchoolEditWindow : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelSchoolList";
        private string _ErrorMessage = "";
        private Enum.HandleType _OperateType = Enum.HandleType.None;
        private AMS.Model.AMS_ProvinceSchoolInfo  _Province;
        private AMS.Model.AMS_School _SchoolModelInfo;  

        public ViewModelSchoolEditWindow(Enum.HandleType handle)
        {
            OperateType = handle;
            if (OperateType == Enum.HandleType.Add)
            {
                SchoolModelInfo = new Model.AMS_School();
            }
        }

        public AMS.Model.AMS_School SchoolModelInfo
        {
            get { return _SchoolModelInfo; }
            set
            {
                _SchoolModelInfo = value;
                OnPropertyChanged("SchoolModelInfo");
            }
        }
       
        /// <summary>
        /// 学校所属省份信息
        /// </summary>
        public AMS.Model.AMS_ProvinceSchoolInfo  Province
        {
            get { return _Province; }
            set
            {
                _Province = value;
                SchoolModelInfo.ProvinceID = Province.ID;
                SchoolModelInfo.ProvinceName = Province.ProvinceName;
                OnPropertyChanged("SelectionProvince");
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                _ErrorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        /// <summary>
        /// 操作方式
        /// </summary>
        public Enum.HandleType OperateType
        {
            get { return _OperateType; }
            set
            {
                _OperateType = value;
                OnPropertyChanged("OperateType");
            }
        } 
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNo
        {
            get { return SchoolModelInfo.Number; }
            set { SchoolModelInfo.Number = value; OnPropertyChanged("SchoolNo"); }
        }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName
        {
            get { return SchoolModelInfo.Name; }
            set { SchoolModelInfo.Name = value; OnPropertyChanged("Schoolname"); }
        }

        /// <summary>
        /// 学校IP或远程地址
        /// </summary>
        public string Dtuip
        {
            get { return SchoolModelInfo.DTUip; }
            set { SchoolModelInfo.DTUip = value; OnPropertyChanged("Dtuip"); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Describe
        {
            get { return SchoolModelInfo.Describe; }
            set { SchoolModelInfo.Describe = value; OnPropertyChanged("Describe"); }
        }

        /// <summary>
        /// 预约WCF连接
        /// </summary>
        public string BookWebConnectionstring
        {
            get { return SchoolModelInfo.ConnectionString; }
            set { SchoolModelInfo.ConnectionString = value; OnPropertyChanged("BookWebConnectionstring"); }
        }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Linkman
        {
            get { return SchoolModelInfo.LinkMan; }
            set { SchoolModelInfo.LinkMan = value; OnPropertyChanged("Linkman"); }
        }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string LinkAddress
        {
            get { return SchoolModelInfo.LinkAddress; }
            set { SchoolModelInfo.LinkAddress = value; OnPropertyChanged("Linkaddress"); }
        }
    
 

        /// <summary>
        /// 读卡器信息
        /// </summary>
        public string Cardinfo
        {
            get { return SchoolModelInfo.CardInfo; }
            set { SchoolModelInfo.CardInfo = value; OnPropertyChanged("Cardinfo"); }
        }
        /// <summary>
        /// 安装进度
        /// </summary>
        public string ExecuteProgress
        {
            get { return SchoolModelInfo.ExecuteProgress; }
            set
            {
                SchoolModelInfo.ExecuteProgress = value;
                OnPropertyChanged("ExecuteProgress");
            }
        }
        /// <summary>
        /// 安装人
        /// </summary>
        public string InstallMan
        {
            get { return SchoolModelInfo.InstallMan; }
            set { SchoolModelInfo.InstallMan = value; OnPropertyChanged("InstallMan"); }
        }

        /// <summary>
        /// 安装日期
        /// </summary>
        public string InstallDate
        {
            get { return SchoolModelInfo.InstallDate; }
            set { SchoolModelInfo.InstallDate = value; OnPropertyChanged("InstallDate"); }
        }
        /// <summary>
        /// 接口信息
        /// </summary>
        public string Interfaceinfo
        {
            get { return SchoolModelInfo.InterfaceInfo; }
            set
            {
                SchoolModelInfo.InterfaceInfo = value;
                OnPropertyChanged("Interfaceinfo");
            }
        }
        /// <summary>
        /// 座位预约
        /// </summary>
        public bool IsSeatBespeak
        {
            get { return SchoolModelInfo.IsSeatBespeak; }
            set
            {
                SchoolModelInfo.IsSeatBespeak = value;
                OnPropertyChanged("IsSeatBespeak");
            }
        }

        public bool Submit()
        {
            switch (OperateType)
            {
                case Enum.HandleType.Add:
                    return   AddSchoolInfo(); 
                case Enum.HandleType.Edit:
                    return UpdateSchoolInfo(); 
                case Enum.HandleType.Delete:
                    return DeleteSchoolInfo(); 
            }
            return false;
        }
        /// <summary>
        /// 添加学校 
        /// </summary>
        /// <returns></returns>
        private bool AddSchoolInfo()
        {
            if (!CheckData())
            {//数据检验不通过
                return false;
            }
            else
            {
                try
                {
                    string handleResult = SchoolMainWindow.AddSchoolInfo(SchoolModelInfo);
                    if (string.IsNullOrEmpty(handleResult))
                    {
                        return true;
                    }
                    else
                    {
                        ErrorMessage = handleResult;
                        return false;
                    }
                }
                catch (AMS.Model.CustomerException ex)
                {
                    ErrorMessage = ex.Message;
                    return false;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return false;
                } 
            }
        }
        /// <summary>
        /// 删除学校信息
        /// </summary>
        /// <returns></returns>
        private bool DeleteSchoolInfo()
        {
            try
            {
                string handleResult = SchoolMainWindow.DeleteSchoolInfo(SchoolModelInfo);
                if (string.IsNullOrEmpty(handleResult))
                {
                    return true; 
                }
                else
                {
                    ErrorMessage = handleResult;
                    return false;
                }
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 更新学校信息
        /// </summary>
        /// <returns></returns>
        private bool UpdateSchoolInfo()
        {
            if (!CheckData())
            {//数据检验不通过
                return false;
            }
            else
            {
                try
                {
                    string handleResult = SchoolMainWindow.UpdateSchoolInfo(SchoolModelInfo);
                    if (string.IsNullOrEmpty(handleResult))
                    {
                        return true;
                        ErrorMessage = "";
                    }
                    else
                    {
                        ErrorMessage = handleResult;
                        return false;
                    }
                }
                catch (AMS.Model.CustomerException ex)
                {
                    ErrorMessage = ex.Message;
                    return false;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 检验输入数据的完整性
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(SchoolModelInfo.Number))
            {
                ErrorMessage = "请输入学校编号";
                return false;
            }
            if (string.IsNullOrEmpty(SchoolModelInfo.Name))
            {
                ErrorMessage = "请输入学校名称";
                return false;
            }
            if (SchoolModelInfo.ProvinceID == -1)
            {
                ErrorMessage = "请选择学校省份";
                return false;
            }
            ErrorMessage = "";
            return true;

        }
    }

}
