﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelIssuedSchoolSelectWindow : ViewModelObject
    {
        #region 构造函数
        public ViewModelIssuedSchoolSelectWindow()
        {
        }
        #endregion

        #region 私有成员
        private static readonly string CLASSNAME = "ViewModelIssuedSchoolSelectWindow";
        private ObservableCollection<AMS.Model.AMS_School> _SchoolList = new ObservableCollection<AMS_School>();
        private ObservableCollection<AMS_ProvinceSchoolInfo> _ProvinceSchoolList = new ObservableCollection<AMS_ProvinceSchoolInfo>();
        private ObservableCollection<NodeIssued> _SchoolNodeList = new ObservableCollection<NodeIssued>();
        private ObservableCollection<SchoolInfo> _SchoolInfoList = new ObservableCollection<SchoolInfo>();
        private AMS.ViewModel.ViewModelSchoolInfo _SchoolInfo = new ViewModelSchoolInfo();
        private string _ErrorMessage = "";
        #endregion

        #region 属性
        public ObservableCollection<AMS.Model.AMS_School> SchoolList
        {
            get { return _SchoolList; }
            set { _SchoolList = value; OnPropertyChanged("SchoolList"); }
        }

        public ObservableCollection<AMS_ProvinceSchoolInfo> ProvinceSchoolList
        {
            get { return _ProvinceSchoolList; }
            set { _ProvinceSchoolList = value; OnPropertyChanged("ProvinceSchoolList"); }
        }

        public AMS.ViewModel.ViewModelSchoolInfo SchoolInfo
        {
            get { return _SchoolInfo; }
            set
            {
                _SchoolInfo = value;
                OnPropertyChanged("SchoolInfo");
            }
        }
        /// <summary>
        /// 学校信息列表
        /// </summary>
        public ObservableCollection<SchoolInfo> SchoolInfoList
        {
            get { return _SchoolInfoList; }
            set
            {
                _SchoolInfoList = value;
                OnPropertyChanged("SchoolInfoList");
            }
        }
        /// <summary>
        /// 学校信息树结构
        /// </summary>
        public ObservableCollection<NodeIssued> SchoolNodeList
        {
            get { return _SchoolNodeList; }
            set
            {
                _SchoolNodeList = value;
                OnPropertyChanged("SchoolNodeList");
            }
        }
        private int _CommandId;
        /// <summary>
        /// 下发命令的Id
        /// </summary>
        public int CommandId
        {
            get { return _CommandId; }
            set { _CommandId = value; OnPropertyChanged("CommandId"); }
        }

        private AMS.Model.Enum.CommandType _Command;
        /// <summary>
        /// 下发命令
        /// </summary>
        public AMS.Model.Enum.CommandType Command
        {
            get { return _Command; }
            set { _Command = value; OnPropertyChanged("Command"); }
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
        /// <summary>
        /// 绑定学校信息(省份、学校、校区)
        /// </summary>
        public void GetSchoolList()
        {
            List<AMS.Model.AMS_ProvinceSchoolInfo> modellist = AMS.ServiceProxy.IssuedSchoolSelectWindow.GetProvinceSchoolInfo();
            ProvinceSchoolList.Clear();
            foreach (AMS.Model.AMS_ProvinceSchoolInfo model in modellist)
            {
                ProvinceSchoolList.Add(model);
            }
            SchoolNodeList = schoolModelListToNodeList(ProvinceSchoolList);//转换为List
        }
        /// <summary>
        /// 把学校的Model转换为TreeView的节点
        /// </summary>
        /// <param name="schoolList"></param>
        /// <returns></returns>
        private ObservableCollection<NodeIssued> schoolModelListToNodeList(ObservableCollection<AMS_ProvinceSchoolInfo> provinceSchool)
        {
            ObservableCollection<NodeIssued> provinceNodes = new ObservableCollection<NodeIssued>();
            for (int h = 0; h < provinceSchool.Count; h++)
            {
                ObservableCollection<AMS.Model.AMS_School> schools = new ObservableCollection<AMS_School>();
                foreach (AMS.Model.AMS_School model in provinceSchool[h].Schools)
                {
                    schools.Add(model);
                }
                NodeIssued provinceNode = new NodeIssued();
                provinceNode.Id = provinceSchool[h].ID;
                provinceNode.IsExpanded = true;
                provinceNode.IsChecked = false;
                provinceNode.Name = provinceSchool[h].ProvinceName;
                for (int i = 0; i < schools.Count; i++)
                {
                    NodeIssued schoolNode = new NodeIssued();
                    schoolNode.FatherNode = provinceNode;
                    schoolNode.Id = schools[i].Id;
                    schoolNode.Num = schools[i].Number;
                    schoolNode.Name = schools[i].Name;
                    schoolNode.IsExpanded = true;
                    schoolNode.IsChecked = false;
                    schoolNode.Type = NodeType.School;
                    if (Command == Model.Enum.CommandType.SlipCustomer)
                    {
                        for (int j = 0; j < schools[i].Campus.Count; j++)
                        {
                            NodeIssued campusNode = new NodeIssued();
                            campusNode.FatherNode = schoolNode;
                            campusNode.Id = schools[i].Campus[j].Id;
                            campusNode.Num = schools[i].Campus[j].Number;
                            campusNode.Name = schools[i].Campus[j].Name;
                            campusNode.IsExpanded = false;
                            campusNode.IsChecked = false;
                            campusNode.Type = NodeType.Campus;
                            schoolNode.ChildNodes.Add(campusNode);
                        }
                    }
                    provinceNode.ChildNodes.Add(schoolNode);
                }
                provinceNodes.Add(provinceNode);
            }
            return provinceNodes;
        }
        /// <summary>
        /// 下发事件
        /// </summary>
        /// <param name="cmdtype"></param>
        /// <returns></returns>
        public bool Issued()
        {
            string functionName = "Issued";
            string result = "";
            try
            {
                foreach (AMS.ViewModel.NodeIssued Prnode in SchoolNodeList)
                {
                    foreach (AMS.ViewModel.NodeIssued Schnode in Prnode.ChildNodes)
                    {
                        if (Command == Model.Enum.CommandType.SlipCustomer)
                        {
                            foreach (AMS.ViewModel.NodeIssued CamNode in Schnode.ChildNodes)
                            {
                                if (CamNode.IsChecked)
                                {
                                    AMS.Model.SlipReleaseCampus slipCommand = new SlipReleaseCampus();
                                    slipCommand.CampusId = CamNode.Id;
                                    slipCommand.SlipCustomerId = CommandId;
                                    int SlipId = AMS.ServiceProxy.IssuedSchoolSelectWindow.IssuedSlipCommand(slipCommand);
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        ErrorMessage = string.Format("下发优惠券失败！{0}", result);
                                        return false;
                                    }
                                    AMS.Model.AMS_CommandList model = new AMS_CommandList();
                                    model.SchoolId = Schnode.Id;
                                    model.CommandId = SlipId;
                                    model.ReleaseTime = DateTime.Now;
                                    model.Command = Command;
                                    model.Operator = ViewModelObject.User.ID;
                                    model.FinishFlag = 0;
                                    result = AMS.ServiceProxy.IssuedSchoolSelectWindow.IssuedFiles(model);
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        ErrorMessage = string.Format("下发失败！{0}", result);
                                        return false;
                                    }
                                }
                            }
                        }
                        else if (Schnode.IsChecked)
                        {
                            AMS.Model.AMS_CommandList model = new AMS_CommandList();
                            model.SchoolId = Schnode.Id;
                            model.CommandId = CommandId;
                            model.ReleaseTime = DateTime.Now;
                            model.Command = Command;
                            model.Operator = ViewModelObject.User.ID;
                            model.FinishFlag = 0;
                            result = AMS.ServiceProxy.IssuedSchoolSelectWindow.IssuedFiles(model);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ErrorMessage = string.Format("下发失败！{0}", result);
                                return false;
                            }
                        }
                    }
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

        #endregion
    }
    public class NodeIssued : AMS.ViewModel.ViewModelObject
    {
        public NodeIssued()
        {
            ChildNodes = new ObservableCollection<NodeIssued>();
        }
        ObservableCollection<NodeIssued> nodeEntrys;
        public ObservableCollection<NodeIssued> ChildNodes
        {
            get { return nodeEntrys; }
            set
            {
                nodeEntrys = value;
                OnPropertyChanged("ChildNodes");
            }
        }

        private int _Id;
        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }
        private string _Num;
        /// <summary>
        /// 节点对应实体的编号
        /// </summary>
        public string Num
        {
            get { return _Num; }
            set
            {
                _Num = value;
                OnPropertyChanged("Num");
            }
        }
        private NodeIssued _FatherNode;
        /// <summary>
        /// 父节点
        /// </summary>
        public NodeIssued FatherNode
        {
            get { return _FatherNode; }
            set
            {
                _FatherNode = value;
                OnPropertyChanged("FatherNode");
            }
        }
        private string _Name;
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        NodeType _Type;
        /// <summary>
        /// 节点类型
        /// </summary>
        public NodeType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                OnPropertyChanged("Type");
            }
        }
        private bool isExpanded;
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }
        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                foreach (NodeIssued node in ChildNodes)
                {
                    node.IsChecked = isChecked;
                }
                OnPropertyChanged("IsChecked");
            }
        }
    }
}
