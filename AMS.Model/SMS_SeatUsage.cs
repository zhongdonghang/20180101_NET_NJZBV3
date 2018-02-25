using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AMS.Model
{
    public class SMS_SeatUsage
    {
        #region 基础属性
        private int _ID = -1;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _SchoolID = -1;
        /// <summary>
        /// 学校ID
        /// </summary>
        public int SchoolID
        {
            get { return _SchoolID; }
            set { _SchoolID = value; }
        }

        private DateTime _UploadDate = new DateTime();
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadDate
        {
            get { return _UploadDate; }
            set { _UploadDate = value; }
        }

        private string _SeatUsageXml = "";
        /// <summary>
        /// 座位使用情况XML
        /// </summary>
        public string SeatUsageXml
        {
            get { return _SeatUsageXml; }
            set { _SeatUsageXml = value; }
        }
        #endregion


        #region 扩展属性

        private string _SchoolNum = "";
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNum
        {
            get { return _SchoolNum; }
            set { _SchoolNum = value; }
        }

        private string _SchoolName = "";
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }
        private int _SeatCount = 0;
        /// <summary>
        /// 座位数量
        /// </summary>
        public int SeatCount
        {
            get { return _SeatCount; }
            set { _SeatCount = value; }
        }
        private int _UserCount = 0;
        /// <summary>
        /// 用户数量
        /// </summary>
        public int UserCount
        {
            get { return _UserCount; }
            set { _UserCount = value; }
        }
        private int _RushCardCount = 0;
        /// <summary>
        /// 刷卡次数
        /// </summary>
        public int RushCardCount
        {
            get { return _RushCardCount; }
            set { _RushCardCount = value; }
        }
        private DateTime _StartTime = new DateTime();
        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        private DateTime _EndTime = new DateTime();
        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        private SeatUsageInfo _SeatUeage = new SeatUsageInfo();
        /// <summary>
        /// 座位使用情况
        /// </summary>
        public SeatUsageInfo SeatUeage
        {
            get { return _SeatUeage; }
            set { _SeatUeage = value; }
        }

        private Dictionary<string, DeviceUsageInfo> _DeviceUsage = new Dictionary<string, DeviceUsageInfo>();
        /// <summary>
        /// 设备使用情况
        /// </summary>
        public Dictionary<string, DeviceUsageInfo> DeviceUsage
        {
            get { return _DeviceUsage; }
            set { _DeviceUsage = value; }
        }

        private BlackListRecordsInfo _BlackListRecords = new BlackListRecordsInfo();
        /// <summary>
        /// 黑名单和违规记录
        /// </summary>
        public BlackListRecordsInfo BlackListRecords
        {
            get { return _BlackListRecords; }
            set { _BlackListRecords = value; }
        }

        #endregion
        /// <summary>
        /// 转换成XML
        /// </summary>
        /// <returns></returns>
        public string ToXml()
        {
            return ToXml(this);
        }
        /// <summary>
        /// 转换成XML
        /// </summary>
        /// <returns></returns>
        public static string ToXml(SMS_SeatUsage model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            root.SetAttribute("SchoolNum", model.SchoolNum);
            root.SetAttribute("UploadDate", model.UploadDate.ToShortDateString());
            root.SetAttribute("UserCount", model.UserCount.ToString());
            root.SetAttribute("SeatCount", model.SeatCount.ToString());
            root.SetAttribute("RushCardCount", model.RushCardCount.ToString());

            //座位使用情况节点
            XmlElement SeatUsageNode = xmlDoc.CreateElement("SeatUsage");
            SeatUsageNode.SetAttribute("EnterOutVisitors", model.SeatUeage.EnterOutVisitors.ToString());
            //选座情况节点
            XmlElement SelectSeatNode = xmlDoc.CreateElement("SelectSeat");
            SelectSeatNode.SetAttribute("SelectSeatCount", model.SeatUeage.SelectSeatCount.ToString());
            SelectSeatNode.SetAttribute("SelectSeatCountByAdmin", model.SeatUeage.SelectSeatCountByAdmin.ToString());
            SelectSeatNode.SetAttribute("ReselectSeatCount", model.SeatUeage.ReselectSeatCount.ToString());
            SelectSeatNode.SetAttribute("WaitSeatCount", model.SeatUeage.WaitSeatCount.ToString());
            SeatUsageNode.AppendChild(SelectSeatNode);
            //预约情况节点
            XmlElement BookingSeatNode = xmlDoc.CreateElement("BookingSeat");
            BookingSeatNode.SetAttribute("BookingCount", model.SeatUeage.BookingCount.ToString());
            BookingSeatNode.SetAttribute("BookingCancelCount", model.SeatUeage.BookingCancelCount.ToString());
            BookingSeatNode.SetAttribute("BookingConfirmCount", model.SeatUeage.BookingConfirmCount.ToString());
            BookingSeatNode.SetAttribute("BookingOverTimeCount", model.SeatUeage.BookingOverTimeCount.ToString());
            SeatUsageNode.AppendChild(BookingSeatNode);
            //暂离情况节点
            XmlElement ShortLeaveNode = xmlDoc.CreateElement("ShortLeave");
            ShortLeaveNode.SetAttribute("ShortLeaveCount", model.SeatUeage.ShortLeaveCount.ToString());
            ShortLeaveNode.SetAttribute("ShortLeaveCountByAdmin", model.SeatUeage.ShortLeaveCountByAdmin.ToString());
            ShortLeaveNode.SetAttribute("ShortLeaveCountByReader", model.SeatUeage.ShortLeaveCountByReader.ToString());
            ShortLeaveNode.SetAttribute("ShortLeaveCountByService", model.SeatUeage.ShortLeaveCountByService.ToString());
            ShortLeaveNode.SetAttribute("ShortLeaveCountByUser", model.SeatUeage.ShortLeaveCountByUser.ToString());
            SeatUsageNode.AppendChild(ShortLeaveNode);
            //释放座位情况节点
            XmlElement LeaveNode = xmlDoc.CreateElement("Leave");
            LeaveNode.SetAttribute("LeaveCountByAdmin", model.SeatUeage.LeaveCountByAdmin.ToString());
            LeaveNode.SetAttribute("LeaveCountByService", model.SeatUeage.LeaveCountByService.ToString());
            LeaveNode.SetAttribute("LeaveCountByUser", model.SeatUeage.LeaveCountByUser.ToString());
            SeatUsageNode.AppendChild(LeaveNode);
            //续时
            XmlElement ContniueTimeNode = xmlDoc.CreateElement("ContniueTime");
            ContniueTimeNode.SetAttribute("ContniueTimeCount", model.SeatUeage.ContniueTimeCount.ToString());
            ContniueTimeNode.SetAttribute("ContniueTimeCountByAdmin", model.SeatUeage.ContniueTimeCountByAdmin.ToString());
            ContniueTimeNode.SetAttribute("ContniueTimeCountByService", model.SeatUeage.ContniueTimeCountByService.ToString());
            ContniueTimeNode.SetAttribute("ContniueTimeCountByUser", model.SeatUeage.ContniueTimeCountByUser.ToString());
            SeatUsageNode.AppendChild(ContniueTimeNode);
            //添加到根节点
            root.AppendChild(SeatUsageNode);

            //设备使用情况节点
            XmlElement DeviceUsageNode = xmlDoc.CreateElement("DeviceUsage");
            foreach (KeyValuePair<string, DeviceUsageInfo> device in model.DeviceUsage)
            {
                XmlElement DeviceNode = xmlDoc.CreateElement("Device");
                DeviceNode.SetAttribute("DeviceNum", device.Value.DeviceNum.ToString());
                DeviceNode.SetAttribute("DeviceName", device.Value.DeviceName.ToString());
                DeviceNode.SetAttribute("ContniueTimeCount", device.Value.ContniueTimeCount.ToString());
                DeviceNode.SetAttribute("LeaveCount", device.Value.LeaveCount.ToString());
                DeviceNode.SetAttribute("RushCardCount", device.Value.RushCardCount.ToString());
                DeviceNode.SetAttribute("SelectSeatCount", device.Value.SelectSeatCount.ToString());
                DeviceNode.SetAttribute("ShortLeaveCount", device.Value.ShortLeaveCount.ToString());
                DeviceNode.SetAttribute("ComeBackCount", device.Value.ComeBackCount.ToString());
                DeviceNode.SetAttribute("BookingConfirmCount", device.Value.BookingConfirmCount.ToString());
                DeviceUsageNode.AppendChild(DeviceNode);
            }
            //添加到根节点
            root.AppendChild(DeviceUsageNode);

            //黑名单
            XmlElement BlacklistNode = xmlDoc.CreateElement("Blacklist");
            BlacklistNode.SetAttribute("BlackListCount", model.BlackListRecords.BlackListCount.ToString());
            BlacklistNode.SetAttribute("ViolationRecordsCount", model.BlackListRecords.ViolationRecordsCount.ToString());
            foreach (KeyValuePair<SeatManage.EnumType.ViolationRecordsType, ViolationInfo> violation in model.BlackListRecords.ViolationRecords)
            {
                XmlElement ViolationNode = xmlDoc.CreateElement("Violation");
                ViolationNode.SetAttribute("Type", violation.Value.Type.ToString());
                ViolationNode.SetAttribute("Count", violation.Value.Count.ToString());
                BlacklistNode.AppendChild(ViolationNode);
            }
            //添加到根节点
            root.AppendChild(BlacklistNode);
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }
        public static SMS_SeatUsage ToModel(string xml)
        {
            SMS_SeatUsage model = new SMS_SeatUsage();
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                model.SchoolNum = node.Attributes["SchoolNum"].Value;
                model.UploadDate = DateTime.Parse(node.Attributes["UploadDate"].Value);
                model.UserCount = int.Parse(node.Attributes["UserCount"].Value);
                model.SeatCount = int.Parse(node.Attributes["SeatCount"].Value);
                model.RushCardCount = int.Parse(node.Attributes["RushCardCount"].Value);

                node = xmlDoc.SelectSingleNode("//Root/SeatUsage");
                model.SeatUeage.EnterOutVisitors = int.Parse(node.Attributes["EnterOutVisitors"].Value);
                //选座情况
                node = xmlDoc.SelectSingleNode("//Root/SeatUsage/SelectSeat");
                model.SeatUeage.SelectSeatCount = int.Parse(node.Attributes["SelectSeatCount"].Value);
                model.SeatUeage.SelectSeatCountByAdmin = int.Parse(node.Attributes["SelectSeatCountByAdmin"].Value);
                model.SeatUeage.ReselectSeatCount = int.Parse(node.Attributes["ReselectSeatCount"].Value);
                model.SeatUeage.WaitSeatCount = int.Parse(node.Attributes["WaitSeatCount"].Value);
                //预约情况
                node = xmlDoc.SelectSingleNode("//Root/SeatUsage/BookingSeat");
                model.SeatUeage.BookingCount = int.Parse(node.Attributes["BookingCount"].Value);
                model.SeatUeage.BookingCancelCount = int.Parse(node.Attributes["BookingCancelCount"].Value);
                model.SeatUeage.BookingConfirmCount = int.Parse(node.Attributes["BookingConfirmCount"].Value);
                model.SeatUeage.BookingOverTimeCount = int.Parse(node.Attributes["BookingOverTimeCount"].Value);
                //暂离情况
                node = xmlDoc.SelectSingleNode("//Root/SeatUsage/ShortLeave");
                model.SeatUeage.ShortLeaveCount = int.Parse(node.Attributes["ShortLeaveCount"].Value);
                model.SeatUeage.ShortLeaveCountByAdmin = int.Parse(node.Attributes["ShortLeaveCountByAdmin"].Value);
                model.SeatUeage.ShortLeaveCountByReader = int.Parse(node.Attributes["ShortLeaveCountByReader"].Value);
                model.SeatUeage.ShortLeaveCountByService = int.Parse(node.Attributes["ShortLeaveCountByService"].Value);
                model.SeatUeage.ShortLeaveCountByUser = int.Parse(node.Attributes["ShortLeaveCountByUser"].Value);
                //释放座位情况
                node = xmlDoc.SelectSingleNode("//Root/SeatUsage/Leave");
                model.SeatUeage.LeaveCountByAdmin = int.Parse(node.Attributes["LeaveCountByAdmin"].Value);
                model.SeatUeage.LeaveCountByService = int.Parse(node.Attributes["LeaveCountByService"].Value);
                model.SeatUeage.LeaveCountByUser = int.Parse(node.Attributes["LeaveCountByUser"].Value);
                //续时
                node = xmlDoc.SelectSingleNode("//Root/SeatUsage/ContniueTime");
                model.SeatUeage.ContniueTimeCount = int.Parse(node.Attributes["ContniueTimeCount"].Value);
                model.SeatUeage.ContniueTimeCountByAdmin = int.Parse(node.Attributes["ContniueTimeCountByAdmin"].Value);
                model.SeatUeage.ContniueTimeCountByService = int.Parse(node.Attributes["ContniueTimeCountByService"].Value);
                model.SeatUeage.ContniueTimeCountByUser = int.Parse(node.Attributes["ContniueTimeCountByUser"].Value);
                //设备使用情况
                XmlNodeList nodeList = xmlDoc.SelectNodes("//Root/DeviceUsage/Device");
                foreach (XmlNode nodeItem in nodeList)
                {
                    DeviceUsageInfo device = new DeviceUsageInfo();
                    device.DeviceNum = nodeItem.Attributes["DeviceNum"].Value;
                    device.DeviceName = nodeItem.Attributes["DeviceName"].Value;
                    device.ContniueTimeCount = int.Parse(nodeItem.Attributes["ContniueTimeCount"].Value);
                    device.LeaveCount = int.Parse(nodeItem.Attributes["LeaveCount"].Value);
                    device.RushCardCount = int.Parse(nodeItem.Attributes["RushCardCount"].Value);
                    device.SelectSeatCount = int.Parse(nodeItem.Attributes["SelectSeatCount"].Value);
                    device.ShortLeaveCount = int.Parse(nodeItem.Attributes["ShortLeaveCount"].Value);
                    device.BookingConfirmCount = int.Parse(nodeItem.Attributes["BookingConfirmCount"].Value);
                    device.ComeBackCount = int.Parse(nodeItem.Attributes["ComeBackCount"].Value);
                    model.DeviceUsage.Add(device.DeviceNum, device);
                }

                //黑名单
                node = xmlDoc.SelectSingleNode("//Root/Blacklist");
                model.BlackListRecords.BlackListCount = int.Parse(node.Attributes["BlackListCount"].Value);
                model.BlackListRecords.ViolationRecordsCount = int.Parse(node.Attributes["ViolationRecordsCount"].Value);
                //违规
                nodeList = xmlDoc.SelectNodes("//Root/Blacklist/Violation");
                foreach (XmlNode nodeItem in nodeList)
                {
                    ViolationInfo violation = new ViolationInfo();
                    model.BlackListRecords.ViolationRecords[(SeatManage.EnumType.ViolationRecordsType)System.Enum.Parse(typeof(SeatManage.EnumType.ViolationRecordsType),nodeItem.Attributes["Type"].Value)].Count = int.Parse(nodeItem.Attributes["Count"].Value);
                }

            }
            catch
            {
                throw;
            }
            return model;
        }

        #region Xml转化

        #endregion

    }
    /// <summary>
    /// 座位使用情况
    /// </summary>
    public class SeatUsageInfo
    {
        private int _EnterOutVisitors = 0;
        /// <summary>
        /// 进出人次
        /// </summary>
        public int EnterOutVisitors
        {
            get { return _EnterOutVisitors; }
            set { _EnterOutVisitors = value; }
        }
        private int _SelectSeatCount = 0;
        /// <summary>
        /// 选座次数
        /// </summary>
        public int SelectSeatCount
        {
            get { return _SelectSeatCount; }
            set { _SelectSeatCount = value; }
        }
        private int _ReselectSeatCount = 0;
        /// <summary>
        /// 重选次数
        /// </summary>
        public int ReselectSeatCount
        {
            get { return _ReselectSeatCount; }
            set { _ReselectSeatCount = value; }
        }
        private int _BookingConfirmCount = 0;
        /// <summary>
        /// 预约入座次数
        /// </summary>
        public int BookingConfirmCount
        {
            get { return _BookingConfirmCount; }
            set { _BookingConfirmCount = value; }
        }
        private int _WaitSeatCount = 0;
        /// <summary>
        /// 等待座位次数
        /// </summary>
        public int WaitSeatCount
        {
            get { return _WaitSeatCount; }
            set { _WaitSeatCount = value; }
        }
        private int _SelectSeatCountByAdmin = 0;
        /// <summary>
        /// 管理员指定座位
        /// </summary>
        public int SelectSeatCountByAdmin
        {
            get { return _SelectSeatCountByAdmin; }
            set { _SelectSeatCountByAdmin = value; }
        }
        private int _ShortLeaveCount = 0;
        /// <summary>
        /// 暂离次数
        /// </summary>
        public int ShortLeaveCount
        {
            get { return _ShortLeaveCount; }
            set { _ShortLeaveCount = value; }
        }
        private int _ShortLeaveCountByUser = 0;
        /// <summary>
        /// 读者自己暂离
        /// </summary>
        public int ShortLeaveCountByUser
        {
            get { return _ShortLeaveCountByUser; }
            set { _ShortLeaveCountByUser = value; }
        }
        private int _ShortLeaveCountByReader = 0;
        /// <summary>
        /// 被其他读者暂离
        /// </summary>
        public int ShortLeaveCountByReader
        {
            get { return _ShortLeaveCountByReader; }
            set { _ShortLeaveCountByReader = value; }
        }
        private int _ShortLeaveCountByAdmin = 0;
        /// <summary>
        /// 被管理员暂离
        /// </summary>
        public int ShortLeaveCountByAdmin
        {
            get { return _ShortLeaveCountByAdmin; }
            set { _ShortLeaveCountByAdmin = value; }
        }
        private int _ShortLeaveCountByService = 0;
        /// <summary>
        /// 被服务暂离
        /// </summary>
        public int ShortLeaveCountByService
        {
            get { return _ShortLeaveCountByService; }
            set { _ShortLeaveCountByService = value; }
        }
        private int _LeaveCountByUser = 0;
        /// <summary>
        /// 自己离开
        /// </summary>
        public int LeaveCountByUser
        {
            get { return _LeaveCountByUser; }
            set { _LeaveCountByUser = value; }
        }
        private int _LeaveCountByAdmin = 0;
        /// <summary>
        /// 被管理员释放
        /// </summary>
        public int LeaveCountByAdmin
        {
            get { return _LeaveCountByAdmin; }
            set { _LeaveCountByAdmin = value; }
        }
        private int _LeaveCountByService = 0;
        /// <summary>
        /// 被服务释放
        /// </summary>
        public int LeaveCountByService
        {
            get { return _LeaveCountByService; }
            set { _LeaveCountByService = value; }
        }
        private int _ContniueTimeCount = 0;
        /// <summary>
        /// 续时
        /// </summary>
        public int ContniueTimeCount
        {
            get { return _ContniueTimeCount; }
            set { _ContniueTimeCount = value; }
        }
        private int _ContniueTimeCountByUser = 0;
        /// <summary>
        /// 自己续时
        /// </summary>
        public int ContniueTimeCountByUser
        {
            get { return _ContniueTimeCountByUser; }
            set { _ContniueTimeCountByUser = value; }
        }
        private int _ContniueTimeCountByAdmin = 0;
        /// <summary>
        /// 被管理员续时
        /// </summary>
        public int ContniueTimeCountByAdmin
        {
            get { return _ContniueTimeCountByAdmin; }
            set { _ContniueTimeCountByAdmin = value; }
        }
        private int _ContniueTimeCountByService = 0;
        /// <summary>
        /// 服务续时
        /// </summary>
        public int ContniueTimeCountByService
        {
            get { return _ContniueTimeCountByService; }
            set { _ContniueTimeCountByService = value; }
        }
        private int _BookingCancelCount = 0;
        /// <summary>
        /// 预约取消次数
        /// </summary>
        public int BookingCancelCount
        {
            get { return _BookingCancelCount; }
            set { _BookingCancelCount = value; }
        }
        private int _BookingOverTimeCount = 0;
        /// <summary>
        /// 预约超时次数
        /// </summary>
        public int BookingOverTimeCount
        {
            get { return _BookingOverTimeCount; }
            set { _BookingOverTimeCount = value; }
        }
        private int _BookingCount = 0;
        /// <summary>
        /// 预约次数
        /// </summary>
        public int BookingCount
        {
            get { return _BookingCount; }
            set { _BookingCount = value; }
        }

    }
    /// <summary>
    /// 设备使用情况
    /// </summary>
    public class DeviceUsageInfo
    {
        private string _DeviceNum = "";
        /// <summary>
        /// 终端编号
        /// </summary>
        public string DeviceNum
        {
            get { return _DeviceNum; }
            set { _DeviceNum = value; }
        }
        private string _DeviceName = "";
        /// <summary>
        /// 终端名称
        /// </summary>
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }
        private int _RushCardCount = 0;
        /// <summary>
        /// 刷卡次数
        /// </summary>
        public int RushCardCount
        {
            get { return _RushCardCount; }
            set { _RushCardCount = value; }
        }
        private int _SelectSeatCount = 0;
        /// <summary>
        /// 选座次数
        /// </summary>
        public int SelectSeatCount
        {
            get { return _SelectSeatCount; }
            set { _SelectSeatCount = value; }
        }
        private int _ShortLeaveCount = 0;
        /// <summary>
        /// 暂离次数
        /// </summary>
        public int ShortLeaveCount
        {
            get { return _ShortLeaveCount; }
            set { _ShortLeaveCount = value; }
        }
        private int _LeaveCount = 0;
        /// <summary>
        /// 离开次数
        /// </summary>
        public int LeaveCount
        {
            get { return _LeaveCount; }
            set { _LeaveCount = value; }
        }
        private int _ContniueTimeCount = 0;
        /// <summary>
        /// 续时次数
        /// </summary>
        public int ContniueTimeCount
        {
            get { return _ContniueTimeCount; }
            set { _ContniueTimeCount = value; }
        }
        private int _ComeBackCount = 0;
        /// <summary>
        /// 回来
        /// </summary>
        public int ComeBackCount
        {
            get { return _ComeBackCount; }
            set { _ComeBackCount = value; }
        }
        private int _BookingConfirmCount = 0;
        /// <summary>
        /// 预约确认次数
        /// </summary>
        public int BookingConfirmCount
        {
            get { return _BookingConfirmCount; }
            set { _BookingConfirmCount = value; }
        }
    }
    /// <summary>
    /// 黑名单和违规
    /// </summary>
    public class BlackListRecordsInfo
    {
        public BlackListRecordsInfo()
        {
            for (int i = -1; i < 9; i++)
            {
                ViolationInfo model = new ViolationInfo();
                model.Type = (SeatManage.EnumType.ViolationRecordsType)i;
                _ViolationRecords.Add(model.Type, model);
            }
        }
        private int _ViolationRecordsCount = 0;
        /// <summary>
        /// 违规次数
        /// </summary>
        public int ViolationRecordsCount
        {
            get { return _ViolationRecordsCount; }
            set { _ViolationRecordsCount = value; }
        }
        private int _BlackListCount = 0;
        /// <summary>
        /// 黑名单次数
        /// </summary>
        public int BlackListCount
        {
            get { return _BlackListCount; }
            set { _BlackListCount = value; }
        }
        private Dictionary<SeatManage.EnumType.ViolationRecordsType, ViolationInfo> _ViolationRecords = new Dictionary<SeatManage.EnumType.ViolationRecordsType, ViolationInfo>();
        /// <summary>
        /// 违规次数
        /// </summary>
        public Dictionary<SeatManage.EnumType.ViolationRecordsType, ViolationInfo> ViolationRecords
        {
            get { return _ViolationRecords; }
            set { _ViolationRecords = value; }
        }
    }
    /// <summary>
    /// 违规记录
    /// </summary>
    public class ViolationInfo
    {
        private SeatManage.EnumType.ViolationRecordsType _Type = SeatManage.EnumType.ViolationRecordsType.None;
        /// <summary>
        /// 违规类型
        /// </summary>
        public SeatManage.EnumType.ViolationRecordsType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                switch (_Type)
                {

                    case SeatManage.EnumType.ViolationRecordsType.BookingTimeOut: return "预约超时";
                    case SeatManage.EnumType.ViolationRecordsType.LeaveByAdmin: return "被管理员释放座位";
                    case SeatManage.EnumType.ViolationRecordsType.LeaveNotReadCard: return "离开未刷卡";
                    case SeatManage.EnumType.ViolationRecordsType.SeatOutTime: return "在座超时";
                    case SeatManage.EnumType.ViolationRecordsType.ShortLeaveByAdminOutTime: return "暂离超时(管理员)";
                    case SeatManage.EnumType.ViolationRecordsType.ShortLeaveByReaderOutTime: return "暂离超时(读者)";
                    case SeatManage.EnumType.ViolationRecordsType.ShortLeaveByServiceOutTime: return "暂离超时(服务)";
                    case SeatManage.EnumType.ViolationRecordsType.ShortLeaveOutTime: return "暂离超时";
                    default: return "未知";
                }
            }
        }
        private int _Count = 0;
        /// <summary>
        /// 违规次数
        /// </summary>
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }
    }
}
