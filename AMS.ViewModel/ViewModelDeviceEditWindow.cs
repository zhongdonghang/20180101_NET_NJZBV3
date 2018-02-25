using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    /// <summary>
    /// 设备列表
    /// </summary>
    public class ViewModelDeviceEditWindow : ViewModelObject
    {
        /// <summary>
        /// 所属校区信息
        /// </summary>
        AMS.Model.AMS_Campus _CampusModel = null;
        AMS.Model.AMS_School _SchoolModel = null;
        private AMS.Model.AMS_Device _DeviceModel = new Model.AMS_Device();
        private Enum.HandleType _Cmd = Enum.HandleType.None;
        private string _ErrorMessage = "";

        public ViewModelDeviceEditWindow(AMS.Model.AMS_Campus campusModel,AMS.Model.AMS_School schoolModel)
        {
            CampusModel = campusModel;
            SchoolModel = schoolModel;
        }
        /// <summary>
        /// 错误消息
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
        /// 处理命令
        /// </summary>
        public Enum.HandleType Cmd
        {
            get { return _Cmd; }
            set
            {
                _Cmd = value;
                OnPropertyChanged("Cmd");
            }
        }

        /// <summary>
        /// 设备所属的校区信息
        /// </summary>
        public AMS.Model.AMS_Campus CampusModel
        {
            get { return _CampusModel; }
            set
            {
                _CampusModel = value;
                OnPropertyChanged("CampusModel");
            }
        }
        public AMS.Model.AMS_School SchoolModel
        {
            get { return _SchoolModel; }
            set
            {
                _SchoolModel = value;
                OnPropertyChanged("CampusModel");
            }
        }
        /// <summary>
        /// 设备信息
        /// </summary>
        public AMS.Model.AMS_Device DeviceModel
        {
            get { return _DeviceModel; }
            set
            {
                _DeviceModel = value;
                OnPropertyChanged("DeviceModel");
            }
        }

        /// <summary>
        /// 校区编号
        /// </summary>
        public string CampusNum
        {
            get { return CampusModel.Number; }
            set
            {
                CampusModel.Number = value;
                OnPropertyChanged("CampusNum");
            }
        }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceNum
        {
            get
            {
                if (string.IsNullOrEmpty(DeviceModel.Number))
                {
                    int c = 0;
                    foreach (AMS.Model.AMS_Campus cam in SchoolModel.Campus)
                    {
                        c += cam.Device.Count;
                    }
                    DeviceModel.Number = SchoolModel.Number + (c + 1).ToString("D2");
                }
                return DeviceModel.Number.Replace(CampusModel.Number.Substring(0,CampusModel.Number.Length-2), ""); }
            set
            {
                DeviceModel.Number = CampusModel.Number.Substring(0, CampusModel.Number.Length - 2) + value;
                OnPropertyChanged("DeviceNum");
            }
        }
        /// <summary>
        /// 设备描述
        /// </summary>
        public string Describe
        {
            get { return DeviceModel.Describe; }
            set
            {
                DeviceModel.Describe = value;
                OnPropertyChanged("Describe");
            }
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <returns></returns>
        public bool ButtonSubmit()
        {
            switch (Cmd)
            {
                case Enum.HandleType.Edit:
                    return UpdateDevice();
                case Enum.HandleType.Add:
                    return AddDevice();
                case Enum.HandleType.Delete:
                    return DeleteDevice();
            }
            return false;
        }
        /// <summary>
        /// 删除设备信息
        /// </summary>
        /// <returns></returns>
        private bool DeleteDevice()
        {

            try
            {
                string r = ServiceProxy.SchoolMainWindow.DeleteDevice(this.DeviceModel);
                if (string.IsNullOrEmpty(r))
                {
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
        /// 更新设备信息
        /// </summary>
        /// <returns></returns>
        private bool UpdateDevice()
        {
            if (checkData())
            {
                try
                {
                    this.DeviceModel.CampusId = this.CampusModel.Id;
                    string r = ServiceProxy.SchoolMainWindow.UpdateDevice(this.DeviceModel);
                    if (string.IsNullOrEmpty(r))
                    {
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
            return false;
        }

        /// <summary>
        /// 添加设备信息
        /// </summary>
        /// <returns></returns>
        private bool AddDevice()
        {
            if (checkData())
            {
                try
                {
                    this.DeviceModel.CampusId = this.CampusModel.Id;
                    string r = ServiceProxy.SchoolMainWindow.AddDevice(this.DeviceModel);
                    if (string.IsNullOrEmpty(r))
                    {
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
            return false;
        }

        /// <summary>
        /// 检验输入正确性
        /// </summary>
        /// <returns></returns>
        private bool checkData()
        {
            if (string.IsNullOrEmpty(DeviceNum))
            {
                ErrorMessage = "请输入设备编号";
                return false;
            }
            else
            {
                int d;
                if (DeviceNum.Length == 2 && int.TryParse(DeviceNum, out d))
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "请正确输入设备编号";
                    return false;
                }
            }
        }

    }

}
