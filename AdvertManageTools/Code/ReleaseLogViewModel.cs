using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace AdvertManageTools.Code
{
    public class ReleaseLogViewModel : INotifyPropertyChanged
    {
        ObservableCollection<AdvertManage.Model.AMS_CommandListModel> _CommandList = new ObservableCollection<AdvertManage.Model.AMS_CommandListModel>(); 
        ObservableCollection<AdvertManage.Model.AMS_SchoolModel> _SchoolList = new ObservableCollection<AdvertManage.Model.AMS_SchoolModel>();
        ObservableCollection<CommandTypeItem> _CommandTypeList = new ObservableCollection<CommandTypeItem>();
        ObservableCollection<CommandHandleResultItem> _CommandHandleResultList = new ObservableCollection<CommandHandleResultItem>();
        /// <summary>
        /// 获取的命令列表
        /// </summary>
        public ObservableCollection<AdvertManage.Model.AMS_CommandListModel> CommandList
        {
            get { return _CommandList; }
            set
            {
                _CommandList = value;
                OnPropertyChanged("CommandList");
            }
        }

        /// <summary>
        /// 学校列表
        /// </summary>
        public ObservableCollection<AdvertManage.Model.AMS_SchoolModel> SchoolList
        {
            get { return _SchoolList; }
            set
            {
                _SchoolList = value;
                OnPropertyChanged("SchoolList");
            }
        }

        /// <summary>
        /// 命令列表
        /// </summary>
        public ObservableCollection<CommandTypeItem> CommandTypeList
        {
            get { return _CommandTypeList; }
            set { _CommandTypeList = value; OnPropertyChanged("CommandTypeList"); }
        }

        /// <summary>
        /// 处理结果列表
        /// </summary>
        public ObservableCollection<CommandHandleResultItem> CommandHandleResultList
        {
            get { return _CommandHandleResultList; }
            set { _CommandHandleResultList = value; }
        }
        /// <summary>
        /// 绑定学校列表
        /// </summary>
        public void BindSchool()
        {
            SchoolList.Clear();
            List<AdvertManage.Model.AMS_SchoolModel> schoolList = AdvertManage.BLL.AMS_SchoolBLL.GetAllSchoolInfo();
            SchoolList.Add(new AdvertManage.Model.AMS_SchoolModel() {  Id=-1, Name="请选择"});
            foreach (AdvertManage.Model.AMS_SchoolModel model in schoolList)
            {
                SchoolList.Add(model);
            }
        }
        /// <summary>
        /// 绑定命令类型下拉列表
        /// </summary>
        public void BindCommandTypeItem()
        {
            CommandTypeList.Clear();
            CommandTypeList.Add(new CommandTypeItem() { TypeItem = AdvertManage.Model.Enum.CommandType.None, Text = "全部" });
            CommandTypeList.Add(new CommandTypeItem() { TypeItem = AdvertManage.Model.Enum.CommandType.Playlist, Text = "播放列表" });
            CommandTypeList.Add(new CommandTypeItem() { TypeItem = AdvertManage.Model.Enum.CommandType.SlipCustomer, Text = "优惠券" });
            CommandTypeList.Add(new CommandTypeItem() { TypeItem = AdvertManage.Model.Enum.CommandType.ProgramUpgrade, Text = "程序更新" });
            CommandTypeList.Add(new CommandTypeItem() { TypeItem = AdvertManage.Model.Enum.CommandType.HardAd, Text = "硬广" });
            CommandTypeList.Add(new CommandTypeItem() { TypeItem = AdvertManage.Model.Enum.CommandType.PrintTemplate, Text = "凭条模板" });
            CommandTypeList.Add(new CommandTypeItem() { TypeItem = AdvertManage.Model.Enum.CommandType.TitleAd, Text = "标题广告" });
        }
        /// <summary>
        /// 绑定命令处理结果下拉列表
        /// </summary>
        public void BindCommandHandleResultItem()
        {
            CommandHandleResultList.Clear();
            CommandHandleResultList.Add(new CommandHandleResultItem() { HandleResult = AdvertManage.Model.Enum.CommandHandleResult.None, Text = "全部" });
            CommandHandleResultList.Add(new CommandHandleResultItem() { HandleResult = AdvertManage.Model.Enum.CommandHandleResult.Wait, Text = "等待获取" });
            CommandHandleResultList.Add(new CommandHandleResultItem() { HandleResult = AdvertManage.Model.Enum.CommandHandleResult.Getting, Text = "正在获取" });
            CommandHandleResultList.Add(new CommandHandleResultItem() { HandleResult = AdvertManage.Model.Enum.CommandHandleResult.Success, Text = "成功下发" });
            CommandHandleResultList.Add(new CommandHandleResultItem() { HandleResult = AdvertManage.Model.Enum.CommandHandleResult.Failed, Text = "失败" });
        }
        /// <summary>
        /// 绑定命令类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandType"></param>
        /// <param name="handleResult"></param>
        public void BindCommandModel(int id, AdvertManage.Model.Enum.CommandType commandType, AdvertManage.Model.Enum.CommandHandleResult handleResult)
        {
            CommandList.Clear();
            List<AdvertManage.Model.AMS_CommandListModel> cmdList = AdvertManage.BLL.AMS_CommandBLL.GetCommandListByCondition(id, commandType, handleResult);
            foreach (AdvertManage.Model.AMS_CommandListModel model in cmdList)
            {
                CommandList.Add(model);
            }
        }

        #region 通知事件
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
    /// <summary>
    /// 命令项
    /// </summary>
    public class CommandTypeItem : INotifyPropertyChanged
    {
        string text = "";

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged("Text"); }
        }
        AdvertManage.Model.Enum.CommandType _TypeItem = AdvertManage.Model.Enum.CommandType.None;
        public AdvertManage.Model.Enum.CommandType TypeItem
        {
            get { return _TypeItem; }
            set
            {
                _TypeItem = value;
                OnPropertyChanged("TypeItem");
            }
        }

        #region 通知事件
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
    /// <summary>
    /// 命令处理结果项
    /// </summary>
    public class CommandHandleResultItem : INotifyPropertyChanged
    {
        string text = "";

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }
        private AdvertManage.Model.Enum.CommandHandleResult _HandleResult = AdvertManage.Model.Enum.CommandHandleResult.None;
        /// <summary>
        /// 处理结果
        /// </summary>
        public AdvertManage.Model.Enum.CommandHandleResult HandleResult
        {
            get { return _HandleResult; }
            set
            {
                _HandleResult = value;
                OnPropertyChanged("HandleResult");
            }
        }



        #region 通知事件
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
