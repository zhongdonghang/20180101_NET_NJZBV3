using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using System.Xml;
using DBUtility;
using System.Collections.Generic;
using System.Configuration;

namespace SeatManageWeb.WebService
{
    /// <summary>
    /// SMS_DataQueryInterface 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SMS_DataQueryInterface : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            if (!Verifylicensing())
            {
                return "非法操作，此接口未进行授权！";
            }
            return "Hello World";
        }

        /// <summary>
        /// 检验阅览室ID
        /// </summary>
        /// <param name="readingRoomNo"></param>
        /// <returns></returns>
        private bool checkReadingRoomNo(string readingRoomNo)
        {
            if (readingRoomNo.Length != 6)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        /// <summary>
        /// 根据阅览室编号获取阅览室信息
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        private ReadingRoomInfo GetSingleRoomInfo(string roomNum)
        {
            return T_SM_ReadingRoom.GetSingleRoomInfo(roomNum);
        }
        /// <summary>
        /// 获取阅览室状态
        /// </summary>
        /// <param name="ReadingRoomNo">阅览室编号</param>
        /// <returns></returns>
        [WebMethod]
        public string ReadingRoomState(string ReadingRoomNo)
        {
            try
            {
                if (!Verifylicensing())
                {
                    return "非法操作，此接口未进行授权！";
                }
                if (!checkReadingRoomNo(ReadingRoomNo))
                {
                    return "阅览室编号错误";
                }
                string result = "";
                ReadingRoomInfo room = GetSingleRoomInfo(ReadingRoomNo);
                SeatManage.EnumType.ReadingRoomStatus roomState = NowReadingRoomState.ReadingRoomOpenState(room.Setting.RoomOpenSet, DateTime.Now);
                string readingRoomState = "";
                switch (roomState)
                {
                    case SeatManage.EnumType.ReadingRoomStatus.Open:
                        readingRoomState = "开放";
                        break;
                    case SeatManage.EnumType.ReadingRoomStatus.BeforeOpen:
                        readingRoomState = "即将开馆";
                        break;
                    case SeatManage.EnumType.ReadingRoomStatus.Close:
                        readingRoomState = "关闭";
                        break;
                    case SeatManage.EnumType.ReadingRoomStatus.BeforeClose:
                        readingRoomState = "即将关闭";
                        break;
                }
                result = string.Format("<ReadingRoomState><RoomName No='{0}' SchoolName='{1}' LibraryName='{2}'>{3}</RoomName><Status>{4}</Status></ReadingRoomState>", ReadingRoomNo, room.Libaray.School.Name, room.Libaray.Name, room.Name, readingRoomState);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 区域座位使用状态
        /// </summary>
        /// <param name="ReadingRoomNo">区域编号</param>
        /// <returns></returns>
        [WebMethod]
        public string SeatUsedInfo(string ReadingRoomNo)
        {
            try
            {
                if (!Verifylicensing())
                {
                    return "非法操作，此接口未进行授权！";
                }
                if (!checkReadingRoomNo(ReadingRoomNo))
                {
                    return "阅览室编号错误";
                }

                ReadingRoomInfo room = GetSingleRoomInfo(ReadingRoomNo);
                ReadingRoomSeatUsedState roomSeat = NowReadingRoomState.GetRoomSeatUsedState(ReadingRoomNo);
                int allSeat = roomSeat.SeatAmountAll;//总座位数
                int NotSeatPeople = roomSeat.SeatAmountFree;//剩余座位数
                int usedSeat = roomSeat.SeatAmountUsed;
                string result = string.Format("<SeatUsedInfo><RoomName No='{0}' SchoolName='{1}' LibraryName='{2}'>{3}</RoomName><Seat UsedSum='{4}' AllSum='{5}' freeSeat='{6}' /></SeatUsedInfo>", ReadingRoomNo, room.Libaray.School.Name, room.Libaray.Name, room.Name, usedSeat, allSeat, NotSeatPeople);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 读者当前状态
        /// </summary>
        /// <param name="StuNo"></param>
        /// <returns></returns>
        [WebMethod]
        public string StuState(string StuNo)
        {
            if (!Verifylicensing())
            {
                return "非法操作，此接口未进行授权！";
            }
            string result = "";
            T_SM_Reader reader = new T_SM_Reader();
            ReaderInfo readerModel = new ReaderInfo();
            readerModel = reader.GetReader(StuNo);
            string state = "";
            string seatNo = "";
            string readingRoomName = "";
            if (!string.IsNullOrEmpty(readerModel.CardNo))
            {
                if (readerModel.EnterOutLog != null)
                {
                    switch (readerModel.EnterOutLog.EnterOutState)
                    {
                        case SeatManage.EnumType.EnterOutLogType.ComeBack:
                        case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                        case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                        case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                        case SeatManage.EnumType.EnterOutLogType.WaitingCancel:
                        case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                        case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                            state = "在座";
                            seatNo = readerModel.EnterOutLog.SeatNo;
                            string rrId = readerModel.EnterOutLog.ReadingRoomNo;
                            readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                            break;
                        case SeatManage.EnumType.EnterOutLogType.Leave:
                        case SeatManage.EnumType.EnterOutLogType.None:
                        case SeatManage.EnumType.EnterOutLogType.BookingCancel:
                            state = "无座";
                            break;
                        case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                            state = "暂离";
                            seatNo = readerModel.EnterOutLog.SeatNo;
                            readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                            break;
                        case SeatManage.EnumType.EnterOutLogType.Waiting:
                            state = "等待座位";
                            seatNo = readerModel.EnterOutLog.SeatNo;
                            readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                            break;
                        case SeatManage.EnumType.EnterOutLogType.BespeakWaiting:
                            state = "存在未确认预约座位";
                            seatNo = readerModel.EnterOutLog.SeatNo;
                            readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                            break;
                    }
                }
                result = string.Format("<StuState><Student Name='{0}' CardNo='{1}' RoomName='{2}'  SeatNo='{3}' Status='{4}'></Student></StuState>", readerModel.Name, readerModel.CardNo, readingRoomName, seatNo, state);

            }
            else
            {
                result = "<StuState><Student Name='' CardNo='' RoomName=''  SeatNo='' Status=''></Student></StuState>";
            }
            return result;
        }

        /// <summary>
        /// 读者进出记录
        /// </summary>
        /// <param name="StuNo">学号</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        [WebMethod]
        public string StuInOutLog(string StuNo, string StartDate, string EndDate)
        {
            try
            {
                if (!Verifylicensing())
                {
                    return "非法操作，此接口未进行授权！";
                }
                List<EnterOutLogInfo> list = T_SM_EnterOutLog.GetEnterOutLogs(StuNo, null, null, DateTime.Parse(StartDate), DateTime.Parse(EndDate));
                //TODO:转换成xml结构的算法
                //创建一个xml对象
                XmlDocument xmlDoc = new XmlDocument();
                //创建开头
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(dec);
                //创建根节点
                XmlElement root = xmlDoc.CreateElement("StuInOutLog");
                xmlDoc.AppendChild(root);

                for (int i = 0; i < list.Count; i++)
                {
                    XmlElement logNode = xmlDoc.CreateElement("Log");
                    string seatNo = list[i].SeatNo;
                    string roomName = list[i].ReadingRoomName;
                    string message = list[i].Remark;
                    string time = list[i].EnterOutTime.ToString();
                    logNode.SetAttribute("SeatNo", seatNo);
                    logNode.SetAttribute("RoomName", roomName);
                    logNode.SetAttribute("DateTime", time);
                    logNode.SetAttribute("Describe", message);
                    root.AppendChild(logNode);
                }
                return xmlDoc.OuterXml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 读者预约记录
        /// </summary>
        /// <param name="StuNo">学号</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        [WebMethod]
        public string StuBookLog(string StuNo)
        {
            try
            {
                if (!Verifylicensing())
                {
                    return "非法操作，此接口未进行授权！";
                }
                List<SeatManage.ClassModel.BespeakLogInfo> list = T_SM_SeatBespeak.GetBespeakList(StuNo, null, DateTime.Parse("1900-1-1"), 0, new List<SeatManage.EnumType.BookingStatus>() { SeatManage.EnumType.BookingStatus.Waiting });
                //TODO:转换成xml结构的算法
                //创建一个xml对象
                XmlDocument xmlDoc = new XmlDocument();
                //创建开头
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(dec);
                //创建根节点
                XmlElement root = xmlDoc.CreateElement("StuBookingLog");
                xmlDoc.AppendChild(root);

                for (int i = 0; i < list.Count; i++)
                {
                    XmlElement logNode = xmlDoc.CreateElement("Log");
                    string seatNo = list[i].SeatNo;
                    string roomName = list[i].ReadingRoomName;
                    string message = list[i].Remark;
                    string bookingtime = list[i].BsepeakTime.ToString();
                    string submittime = list[i].SubmitTime.ToString();
                    logNode.SetAttribute("SeatNo", seatNo);
                    logNode.SetAttribute("RoomName", roomName);
                    logNode.SetAttribute("SubmitTime", submittime);
                    logNode.SetAttribute("BookingTime", bookingtime);
                    logNode.SetAttribute("Describe", message);
                    root.AppendChild(logNode);
                }
                return xmlDoc.OuterXml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 读者违规记录
        /// </summary>
        /// <param name="stuNo">学号</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        [WebMethod]
        public string StuViolateDiscipline(string stuNo, string startDate, string EndDate)
        {
            try
            {
                if (!Verifylicensing())
                {
                    return "非法操作，此接口未进行授权！";
                }
                List<ViolationRecordsLogInfo> list = T_SM_ViolateDiscipline.GetViolationRecords(stuNo, null, startDate + " 00:00:00", EndDate + " 23:59:59", SeatManage.EnumType.LogStatus.None, SeatManage.EnumType.LogStatus.None);

                XmlDocument xmlDoc = new XmlDocument();
                //创建开头
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(dec);
                //创建根节点
                XmlElement root = xmlDoc.CreateElement("StuViolateDiscipline");
                xmlDoc.AppendChild(root);
                for (int i = 0; i < list.Count; i++)
                {
                    XmlElement logNode = xmlDoc.CreateElement("Log");
                    string cardNo = list[i].CardNo;
                    string seatId = list[i].SeatID;
                    string readingRoomName = list[i].ReadingRoomName;
                    string enterOutTime = list[i].EnterOutTime.ToString();
                    string violateType = list[i].Remark;
                    string handleState = "";
                    switch (list[i].BlacklistID)
                    {
                        case "-1":
                            handleState = "未处理";
                            break;
                        default:
                            handleState = "已加入黑名单";
                            break;
                    }
                    logNode.SetAttribute("SeatNo", seatId);
                    logNode.SetAttribute("RoomName", readingRoomName);
                    logNode.SetAttribute("DateTime", enterOutTime);
                    logNode.SetAttribute("Describe", violateType);
                    logNode.SetAttribute("HandleResult", handleState);
                    root.AppendChild(logNode);
                }
                return xmlDoc.OuterXml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 读者黑名单记录
        /// </summary>
        /// <param name="stuNo">学号</param>
        /// <param name="startDate">起始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        [WebMethod]
        public string StuBlacklistLog(string stuNo, string startDate, string EndDate)
        {
            try
            {
                if (!Verifylicensing())
                {
                    return "非法操作，此接口未进行授权！";
                }
                List<BlackListInfo> list = T_SM_Blacklist.GetAllBlackListInfo(stuNo, SeatManage.EnumType.LogStatus.None, startDate + " 00:00:00", EndDate + " 23:59:59");
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(dec);
                //创建根节点
                XmlElement root = xmlDoc.CreateElement("BlacklistLog");
                xmlDoc.AppendChild(root);
                for (int i = 0; i < list.Count; i++)
                {
                    string readngRoomName = list[i].ReadingRoomName;
                    string outBlcklist = list[i].OutBlacklistMode.ToString(); ;
                    string AddTime = list[i].AddTime.ToString();
                    string outTime = list[i].OutTime.ToString();
                    string BlacklistState = list[i].BlacklistState.ToString();

                    XmlElement logNode = xmlDoc.CreateElement("Log");
                    logNode.SetAttribute("ReadingRoomName", readngRoomName);
                    logNode.SetAttribute("AddTime", AddTime);
                    logNode.SetAttribute("OutTime", outTime);
                    logNode.SetAttribute("BlacklistState", BlacklistState);
                    root.AppendChild(logNode);
                }

                return xmlDoc.OuterXml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 认证许可
        /// </summary>
        /// <returns></returns>
        private bool Verifylicensing()
        {
            try
            {
                if (SeatManage.Bll.Registry.ReadingRoomInterfaceIsAuthorize())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            //string interfacekey = ConfigurationManager.AppSettings["ReadingRoomInterfaceKey"];
            //interfacekey = interfacekey.Replace("-", "");
            //string ClientNo = SeatManage.Bll.ClientConfigOperate.GetTerminalsInfo()[0].ClientNo;
            //if (SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(new List<string>() { ClientNo.Substring(0, ClientNo.Length - 2), "JuneberryAccessInterfaceKey" }) == interfacekey)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
