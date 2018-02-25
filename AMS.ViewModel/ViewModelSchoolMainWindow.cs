using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelSchoolMainWindow : AMS.ViewModel.ViewModelObject
    {
        public ViewModelSchoolMainWindow()
        {
            SchoolInfo = new ViewModelSchoolInfo();
            SchoolInfoDetail = new ViewModelSchoolInfoDetail();
            CampusInfoDetail = new ViewModelCampusInfoDetail();
        }
        private List<NodeEntry> schoolNodeList;
        private List<AMS_ProvinceSchoolInfo> modelSchoolList;
        private AMS.ViewModel.ViewModelSchoolInfo _SchoolInfo;
        private AMS.ViewModel.ViewModelSchoolInfoDetail _SchoolInfoDetail;
        private ViewModelCampusInfoDetail _CampusInfoDetail;


        #region 属性
        /// <summary>
        /// 校区详细信息
        /// </summary>
        public ViewModelCampusInfoDetail CampusInfoDetail
        {
            get { return _CampusInfoDetail; }
            set
            {
                _CampusInfoDetail = value;
                OnPropertyChanged("CampusInfoDetail");
            }
        }
        /// <summary>
        /// 学校信息树结构
        /// </summary>
        public List<NodeEntry> SchoolNodeList
        {
            get { return schoolNodeList; }
            set
            {
                schoolNodeList = value;
                OnPropertyChanged("SchoolNodeList");
            }
        }
        /// <summary>
        /// 学校信息实体
        /// </summary>
        public List<AMS_ProvinceSchoolInfo> ModelSchoolList
        {
            get { return modelSchoolList; }
            set
            {
                modelSchoolList = value;
                OnPropertyChanged("ModelSchoolList");
            }
        }
        /// <summary>
        /// 显示所有学校信息的UI对应的ViewModel
        /// </summary>
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
        /// 具体学校信息的用户控件对应的ViewModel
        /// </summary>
        public AMS.ViewModel.ViewModelSchoolInfoDetail SchoolInfoDetail
        {
            get { return _SchoolInfoDetail; }
            set
            {
                _SchoolInfoDetail = value;
                OnPropertyChanged("SchoolInfoDetail");
            }
        }
        #endregion

        /// <summary>
        /// 获取所有学校信息，并转换为树结构显示在ui上
        /// </summary>
        public void GetSchoolList()
        {
            try
            {
                modelSchoolList = AMS.ServiceProxy.SchoolMainWindow.GetSchoolList();//获取学校信息
                //SchoolNodeList = schoolModelListToNodeList(modelSchoolList);//转换为List
                if (SchoolNodeList != null)
                {
                    SchoolNodeList = RefriashschoolModelListToNodeList(modelSchoolList, SchoolNodeList);
                }
                else
                {
                    SchoolNodeList = schoolModelListToNodeList(modelSchoolList);
                }
                SchoolInfo.ShowSchoolList(modelSchoolList);
            }
            catch (Exception ex)
            {
                //；
            }
        }
        /// <summary>
        /// 获取当前选中节点的信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSelectNodeInfo<T>(NodeEntry selectedNode)
        {
            T model = default(T);
            switch (selectedNode.Type)
            {
                case NodeType.School:
                    model = (T)(GetSchoolModelInfo(selectedNode) as object);//如果节点是学校，则获取学校信息，返回
                    break;
                case NodeType.Campus:
                    model = (T)(GetCampusModelInfo(selectedNode) as object);
                    break;
                case NodeType.Province:
                    model = (T)(GetProvinceInfo(selectedNode) as object);
                    break;
                case NodeType.Device:
                    model = (T)(GetDeviceModelInfo(selectedNode) as object);
                    break;
            }
            return model;
        }
        public void TreeViewSelectedItemHandle(NodeEntry selectedNode)
        {
            if (selectedNode == null)
            {
                return;
            }
            switch (selectedNode.Type)
            {
                case NodeType.School:
                    ShowSchoolDetail(selectedNode);
                    break;
                case NodeType.Campus:
                    CampusInfoDetail.Visibility = System.Windows.Visibility.Visible;
                    SchoolInfoDetail.Visibility = System.Windows.Visibility.Collapsed;
                    this.SchoolInfo.Visibility = System.Windows.Visibility.Collapsed;
                    AMS.Model.AMS_Campus campusModel = null;
                    //获取当前选中节点的Model信息
                    foreach (AMS_ProvinceSchoolInfo p in ModelSchoolList)
                    {
                        bool isStop = false;
                        foreach (AMS_School s in p.Schools)
                        {
                            foreach (AMS_Campus campus in s.Campus)
                            {
                                if (selectedNode.Id == campus.Id)
                                {
                                    campusModel = campus;
                                    isStop = true;
                                    break;
                                }
                            }
                            if (isStop)
                            {
                                break;
                            }
                        }
                        if (isStop)
                        {
                            break;
                        }
                    }
                    this.CampusInfoDetail.ShowDeviceList(campusModel);
                    this.CampusInfoDetail.ShowCampusDetail(campusModel);
                    break;
                case NodeType.Province:
                    ShowProvinceSchools(selectedNode);
                    break;
            }
        }

        private void ShowProvinceSchools(NodeEntry selectedNode)
        {
            CampusInfoDetail.Visibility = System.Windows.Visibility.Collapsed;
            SchoolInfoDetail.Visibility = System.Windows.Visibility.Collapsed;
            this.SchoolInfo.Visibility = System.Windows.Visibility.Visible;
            List<AMS_ProvinceSchoolInfo> selectedProvince = new List<AMS_ProvinceSchoolInfo>();
            foreach (AMS_ProvinceSchoolInfo psi in ModelSchoolList)
            {
                if (psi.ID == selectedNode.Id)
                {
                    selectedProvince.Add(psi);
                    break;
                }
            }
            SchoolInfo.ShowSchoolList(modelSchoolList);
        }
        /// <summary>
        /// 显示学校信息
        /// </summary>
        /// <param name="selectedNode"></param>
        private void ShowSchoolDetail(NodeEntry selectedNode)
        {
            SchoolInfoDetail.Visibility = System.Windows.Visibility.Visible;
            this.SchoolInfo.Visibility = System.Windows.Visibility.Collapsed;
            CampusInfoDetail.Visibility = System.Windows.Visibility.Collapsed;
            AMS.Model.AMS_School school = null;
            //获取当前选中节点的Model信息
            foreach (AMS_ProvinceSchoolInfo p in ModelSchoolList)
            {
                bool isStop = false;
                foreach (AMS_School s in p.Schools)
                {
                    if (s.Id == selectedNode.Id)
                    {
                        school = s;
                        isStop = true;
                        break;
                    }
                }
                if (isStop)
                {
                    break;
                }
            }
            foreach (SchoolInfo s in SchoolInfo.SchoolInfoList)
            {
                if (s.Id == selectedNode.Id)
                {
                    SchoolInfoDetail.SchoolDetail = s;
                    break;
                }
            }
            SchoolInfoDetail.ConvertToCampusList(school);
        }
        /// <summary>
        /// 获取当前选中学校的学校节点
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <returns></returns>
        private AMS_School GetSchoolModelInfo(NodeEntry selectedNode)
        {
            AMS.Model.AMS_School school = null;
            //获取当前选中节点的Model信息
            foreach (AMS_ProvinceSchoolInfo p in ModelSchoolList)
            {
                foreach (AMS_School s in p.Schools)
                {
                    if (s.Id == selectedNode.Id)
                    {
                        school = s;
                        return school;
                    }
                }

            } return null;
        }

        /// <summary>
        /// 获取当前选中校区的Model
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <returns></returns>
        private AMS_Campus GetCampusModelInfo(NodeEntry selectedNode)
        {
            AMS.Model.AMS_Campus campusModel = null;
            //获取当前选中节点的Model信息
            foreach (AMS_ProvinceSchoolInfo p in ModelSchoolList)
            {
                foreach (AMS_School s in p.Schools)
                {
                    foreach (AMS_Campus campus in s.Campus)
                    {
                        if (selectedNode.Id == campus.Id)
                        {
                            campusModel = campus;
                            return campusModel;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 获取选中节点的是设备信息
        /// </summary>
        /// <param name="selectNode"></param>
        /// <returns></returns>
        private AMS_Device GetDeviceModelInfo(NodeEntry selectedNode)
        {
            AMS.Model.AMS_Device deviceModel = null;
            //获取当前选中节点的Model信息
            foreach (AMS_ProvinceSchoolInfo p in ModelSchoolList)
            {
                foreach (AMS_School s in p.Schools)
                {
                    foreach (AMS_Campus campus in s.Campus)
                    {
                        foreach (AMS_Device device in campus.Device)
                        {
                            if (selectedNode.Id == device.Id)
                            {
                                deviceModel = device;
                                return deviceModel;
                            }
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <returns></returns>
        private AMS_ProvinceSchoolInfo GetProvinceInfo(NodeEntry selectedNode)
        {
            AMS.Model.AMS_ProvinceSchoolInfo schoolModel = null;
            foreach (AMS_ProvinceSchoolInfo p in ModelSchoolList)
            {
                if (selectedNode.Id == p.ID)
                {
                    schoolModel = p;
                    return schoolModel;
                }
            }
            return null;
        }
        /// <summary>
        /// 把学校的Model转换为TreeView的节点
        /// </summary>
        /// <param name="schoolList"></param>
        /// <returns></returns>
        private List<NodeEntry> schoolModelListToNodeList(List<AMS_ProvinceSchoolInfo> provinceSchool)
        {
            List<NodeEntry> provinceNodes = new List<NodeEntry>();
            for (int h = 0; h < provinceSchool.Count; h++)
            {
                List<AMS.Model.AMS_School> schools = provinceSchool[h].Schools;
                NodeEntry provinceNode = new NodeEntry();
                provinceNode.Id = provinceSchool[h].ID;
                provinceNode.IsExpanded = false;
                provinceNode.Name = provinceSchool[h].ProvinceName;
                provinceNode.Type = NodeType.Province;
                provinceNode.NodeImage = "/Image/ProvinceTreeImage.png";
                provinceNode.AddMenuName = "添加新学校";
                provinceNode.AddMenuVisibility = "Visible";
                for (int i = 0; i < schools.Count; i++)
                {
                    NodeEntry schoolNode = new NodeEntry();
                    schoolNode.FatherNode = provinceNode;
                    schoolNode.Id = schools[i].Id;
                    schoolNode.Num = schools[i].Number;
                    schoolNode.Name = schools[i].Name;
                    schoolNode.IsExpanded = false;
                    schoolNode.Type = NodeType.School;
                    schoolNode.NodeImage = "/Image/SchoolTreeImage.png";
                    schoolNode.AddMenuName = "添加新校区";
                    schoolNode.AddMenuVisibility = "Visible";
                    schoolNode.UpdataMenuName = "修改学校信息";
                    schoolNode.UpdateMenuVisibility = "Visible";
                    schoolNode.DeleteMenuName = "删除学校";
                    schoolNode.DeleteMenuVisibility = "Visible";
                    for (int j = 0; j < schools[i].Campus.Count; j++)
                    {
                        NodeEntry campusNode = new NodeEntry();
                        campusNode.FatherNode = schoolNode;
                        campusNode.Id = schools[i].Campus[j].Id;
                        campusNode.Num = schools[i].Campus[j].Number;
                        campusNode.Name = schools[i].Campus[j].Name;
                        campusNode.IsExpanded = false;
                        campusNode.Type = NodeType.Campus;
                        campusNode.NodeImage = "/Image/CampusTreeImage.png";
                        campusNode.AddMenuName = "添加新设备";
                        campusNode.AddMenuVisibility = "Visible";
                        campusNode.UpdataMenuName = "修改校区信息";
                        campusNode.UpdateMenuVisibility = "Visible";
                        campusNode.DeleteMenuName = "删除校区";
                        campusNode.DeleteMenuVisibility = "Visible";
                        for (int k = 0; k < schools[i].Campus[j].Device.Count; k++)
                        {
                            NodeEntry deviceNode = new NodeEntry();
                            deviceNode.FatherNode = campusNode;
                            deviceNode.Id = schools[i].Campus[j].Device[k].Id;
                            deviceNode.Num = schools[i].Campus[j].Device[k].Number;
                            deviceNode.Name = schools[i].Campus[j].Device[k].Number;
                            deviceNode.IsExpanded = false;
                            deviceNode.Type = NodeType.Device;
                            deviceNode.NodeImage = "/Image/DeviceTreeImage.png";
                            deviceNode.UpdataMenuName = "修改设备信息";
                            deviceNode.UpdateMenuVisibility = "Visible";
                            deviceNode.DeleteMenuName = "删除设备";
                            deviceNode.DeleteMenuVisibility = "Visible";
                            campusNode.ChildNodes.Add(deviceNode);
                        }
                        schoolNode.ChildNodes.Add(campusNode);
                    }
                    //schoolNodes.Add(schoolNode);
                    provinceNode.ChildNodes.Add(schoolNode);
                }
                provinceNodes.Add(provinceNode);
            }
            return provinceNodes;
        }
        /// <summary>
        /// 重新刷新保持展开状态
        /// </summary>
        /// <param name="provinceSchool"></param>
        /// <param name="oldNodeList"></param>
        /// <returns></returns>
        private List<NodeEntry> RefriashschoolModelListToNodeList(List<AMS_ProvinceSchoolInfo> provinceSchool, List<NodeEntry> oldNodeList)
        {
            List<NodeEntry> provinceNodes = new List<NodeEntry>();
            for (int h = 0; h < provinceSchool.Count; h++)
            {
                List<AMS.Model.AMS_School> schools = provinceSchool[h].Schools;
                NodeEntry provinceNode = new NodeEntry();
                provinceNode.Id = provinceSchool[h].ID;
                NodeEntry oldprovinceNode = new NodeEntry();
                foreach (NodeEntry n in oldNodeList)
                {
                    if (provinceNode.Id == n.Id)
                    {
                        oldprovinceNode = n;
                        break;
                    }
                }
                if (oldprovinceNode.Id ==-1)
                {
                    provinceNode.IsExpanded = false;
                }
                else
                {
                    provinceNode.IsExpanded = oldprovinceNode.IsExpanded;
                }
                provinceNode.Name = provinceSchool[h].ProvinceName;
                provinceNode.Type = NodeType.Province;
                provinceNode.NodeImage = "/Image/ProvinceTreeImage.png";
                provinceNode.AddMenuName = "添加新学校";
                provinceNode.AddMenuVisibility = "Visible";
                for (int i = 0; i < schools.Count; i++)
                {
                    NodeEntry schoolNode = new NodeEntry();
                    schoolNode.FatherNode = provinceNode;
                    schoolNode.Id = schools[i].Id;
                    schoolNode.Num = schools[i].Number;
                    schoolNode.Name = schools[i].Name;
                    NodeEntry oldschoolNode = new NodeEntry();
                    foreach (NodeEntry n in oldprovinceNode.ChildNodes)
                    {
                        if (schoolNode.Id == n.Id)
                        {
                            oldschoolNode = n;
                            break;
                        }
                    }
                    if (oldschoolNode.Id == -1)
                    {
                        schoolNode.IsExpanded = false;
                    }
                    else
                    {
                        schoolNode.IsExpanded = oldschoolNode.IsExpanded;
                    }
                    schoolNode.Type = NodeType.School;
                    schoolNode.NodeImage = "/Image/SchoolTreeImage.png";
                    schoolNode.AddMenuName = "添加新校区";
                    schoolNode.AddMenuVisibility = "Visible";
                    schoolNode.UpdataMenuName = "修改学校信息";
                    schoolNode.UpdateMenuVisibility = "Visible";
                    schoolNode.DeleteMenuName = "删除学校";
                    schoolNode.DeleteMenuVisibility = "Visible";
                    for (int j = 0; j < schools[i].Campus.Count; j++)
                    {
                        NodeEntry campusNode = new NodeEntry();
                        campusNode.FatherNode = schoolNode;
                        campusNode.Id = schools[i].Campus[j].Id;
                        campusNode.Num = schools[i].Campus[j].Number;
                        campusNode.Name = schools[i].Campus[j].Name;
                        NodeEntry oldcampusNode = new NodeEntry();
                        foreach (NodeEntry n in oldschoolNode.ChildNodes)
                        {
                            if (campusNode.Id == n.Id)
                            {
                                oldcampusNode = n;
                                break;
                            }
                        }
                        if (oldcampusNode.Id == -1)
                        {
                            campusNode.IsExpanded = false;
                        }
                        else
                        {
                            campusNode.IsExpanded = oldcampusNode.IsExpanded;
                        }
                        campusNode.Type = NodeType.Campus;
                        campusNode.NodeImage = "/Image/CampusTreeImage.png";
                        campusNode.AddMenuName = "添加新设备";
                        campusNode.AddMenuVisibility = "Visible";
                        campusNode.UpdataMenuName = "修改校区信息";
                        campusNode.UpdateMenuVisibility = "Visible";
                        campusNode.DeleteMenuName = "删除校区";
                        campusNode.DeleteMenuVisibility = "Visible";
                        for (int k = 0; k < schools[i].Campus[j].Device.Count; k++)
                        {
                            NodeEntry deviceNode = new NodeEntry();
                            deviceNode.FatherNode = campusNode;
                            deviceNode.Id = schools[i].Campus[j].Device[k].Id;
                            deviceNode.Num = schools[i].Campus[j].Device[k].Number;
                            deviceNode.Name = schools[i].Campus[j].Device[k].Number;
                            deviceNode.IsExpanded = false;
                            deviceNode.Type = NodeType.Device;
                            deviceNode.NodeImage = "/Image/DeviceTreeImage.png";
                            deviceNode.UpdataMenuName = "修改设备信息";
                            deviceNode.UpdateMenuVisibility = "Visible";
                            deviceNode.DeleteMenuName = "删除设备";
                            deviceNode.DeleteMenuVisibility = "Visible";
                            campusNode.ChildNodes.Add(deviceNode);
                        }
                        schoolNode.ChildNodes.Add(campusNode);
                    }
                    //schoolNodes.Add(schoolNode);
                    provinceNode.ChildNodes.Add(schoolNode);
                }
                provinceNodes.Add(provinceNode);
            }
            return provinceNodes;
        }
    }

    public enum NodeType
    {
        Province,
        School,
        Campus,
        Device
    }
    public class NodeEntry : AMS.ViewModel.ViewModelObject
    {
        public NodeEntry()
        {
            ChildNodes = new List<NodeEntry>();
        }
        List<NodeEntry> nodeEntrys;
        public List<NodeEntry> ChildNodes
        {
            get { return nodeEntrys; }
            set
            {
                nodeEntrys = value;
                OnPropertyChanged("ChildNodes");
            }
        }

        private int _Id = -1;
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
        private NodeEntry _FatherNode;
        /// <summary>
        /// 父节点
        /// </summary>
        public NodeEntry FatherNode
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
        private string _NodeImage = "";
        /// <summary>
        /// 节点图标
        /// </summary>
        public string NodeImage
        {
            get { return _NodeImage; }
            set { _NodeImage = value; OnPropertyChanged("NodeImage"); }
        }

        private string _AddMenuName = "";
        /// <summary>
        /// 增加菜单名字
        /// </summary>
        public string AddMenuName
        {
            get { return _AddMenuName; }
            set { _AddMenuName = value; OnPropertyChanged("AddMenuName"); }
        }
        private string _UpdataMenuName = "";
        /// <summary>
        /// 修改菜单名称
        /// </summary>
        public string UpdataMenuName
        {
            get { return _UpdataMenuName; }
            set { _UpdataMenuName = value; OnPropertyChanged("UpdataMenuName"); }
        }
        private string _DeleteMenuName = "";
        /// <summary>
        /// 删除菜单名称
        /// </summary>
        public string DeleteMenuName
        {
            get { return _DeleteMenuName; }
            set { _DeleteMenuName = value; OnPropertyChanged("DeleteMenuName"); }
        }
        private string _AddMenuVisibility = "Collapsed";
        /// <summary>
        /// 添加菜单显示
        /// </summary>
        public string AddMenuVisibility
        {
            get { return _AddMenuVisibility; }
            set { _AddMenuVisibility = value; OnPropertyChanged("AddMenuVisibility"); }
        }
        /// <summary>
        /// 更新菜单显示
        /// </summary>
        private string _UpdateMenuVisibility = "Collapsed";

        public string UpdateMenuVisibility
        {
            get { return _UpdateMenuVisibility; }
            set { _UpdateMenuVisibility = value; OnPropertyChanged("UpdateMenuVisibility"); }
        }

        private string _DeleteMenuVisibility = "Collapsed";
        /// <summary>
        /// 删除菜单显示
        /// </summary>
        public string DeleteMenuVisibility
        {
            get { return _DeleteMenuVisibility; }
            set { _DeleteMenuVisibility = value; OnPropertyChanged("DeleteMenuVisibility"); }
        }
    }
}
