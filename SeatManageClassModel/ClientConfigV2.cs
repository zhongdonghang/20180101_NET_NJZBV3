using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class ClientConfigV2
    {
        public ClientConfigV2()
        {
            BackImgage.Add("SchoolLogoImage", @"images\ClientBackImage\SchoolLogoImage.png");
            //座位
            BackImgage.Add("ImgSeat", @"images\ClientBackImage\ImgSeat.png");  
            BackImgage.Add("ImgSeatUsing", @"images\ClientBackImage\ImgSeatUsing.png");
            BackImgage.Add("ImgSeatShortLeave",@"images\ClientBackImage\ImgSeatShortLeave.png");
            BackImgage.Add("ImgSeatDisable", @"images\ClientBackImage\ImgSeatDisable.png");
            //读者 
            BackImgage.Add("ImgReader", @"images\ClientBackImage\ImgReader.png");
            BackImgage.Add("ImgShortLeaveReader", @"images\ClientBackImage\ImgShortLeaveReader.png");
            //状态图标
            BackImgage.Add("ImgBook", @"images\ClientBackImage\ImgBook.png");
            BackImgage.Add("ImgShortLeave", @"images\ClientBackImage\ImgShortLeave.png");
            BackImgage.Add("ImgStopUse", @"images\ClientBackImage\ImgStopUse.png");
            BackImgage.Add("ImgPower", @"images\ClientBackImage\ImgPower.png");
            BackImgage.Add("ImgSeatUse", @"images\ClientBackImage\ImgSeatUse.png");
            //备注Note
            BackImgage.Add("note_AirConditioning", @"images\ClientBackImage\note_AirConditioning.png");
            BackImgage.Add("note_blank", @"images\ClientBackImage\note_blank.png");
            BackImgage.Add("note_Bookshelf", @"images\ClientBackImage\note_Bookshelf.png");
            BackImgage.Add("note_Door", @"images\ClientBackImage\note_Door.png");
            BackImgage.Add("note_Elevator", @"images\ClientBackImage\note_Elevator.png");
            BackImgage.Add("note_Pillar", @"images\ClientBackImage\note_Pillar.png");
            BackImgage.Add("note_Plant", @"images\ClientBackImage\note_Plant.png");
            BackImgage.Add("note_Roundtable", @"images\ClientBackImage\note_Roundtable.png");
            BackImgage.Add("note_Stairway", @"images\ClientBackImage\note_Stairway.png");
            BackImgage.Add("note_Steps", @"images\ClientBackImage\note_Steps.png");
            BackImgage.Add("note_Table", @"images\ClientBackImage\note_Table.png");
            BackImgage.Add("note_Wall", @"images\ClientBackImage\note_Wall.png");
            BackImgage.Add("note_Window", @"images\ClientBackImage\note_Window.png");
            BackImgage.Add("note_PCTable", @"images\ClientBackImage\note_PCTable.png");

        }
        #region 成员变量
        private string clientNo = "";//终端编号
        private SelectSeatMode selectMethod = SelectSeatMode.Default;//选座方式
        private bool _UsingEnterNoForSeat = true;//输入座位号选座功能
        private OftenSeat _UsingOftenUsedSeat = new OftenSeat();//常做座位选座
        private PrintSlipMode _UsingPrintSlip = PrintSlipMode.AutoPrint;//是否打印凭条
        private bool _UsingActiveBespeakSeat = true;//是否使用预约激活功能
        private List<string> rooms = new List<string>();//触摸屏管理的阅览室
        private Dictionary<string, string> backImgage = new Dictionary<string, string>();//背景图片
        private bool _IsShowClosedRoom = true;//是否显示已关闭的阅览室
        private bool _IsShowInitPOS = false;//是否显示读卡器初始化按钮
        private int _LastPrintTimes = 0;//上一次打印纸打印的总张数
        private int _PrintedTimes = 0;//本卷打印纸已打印次数
        private bool _IsAnyPaper = true;//打印机是否有纸
        #endregion
        #region 属性
        /// <summary>
        /// 是否显示读卡器初始化按钮
        /// </summary>
        public bool IsShowInitPOS
        {
            get { return _IsShowInitPOS; }
            set { _IsShowInitPOS = value; }
        }

        /// <summary>
        /// 是否显示关闭的阅览室
        /// </summary>
        public bool IsShowClosedRoom
        {
            get { return _IsShowClosedRoom; }
            set { _IsShowClosedRoom = value; }
        }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string ClientNo
        {
            get { return clientNo; }
            set { clientNo = value; }
        }
        /// <summary>
        /// 选座方式
        /// </summary>
        public SelectSeatMode SelectMethod
        {
            get { return selectMethod; }
            set { selectMethod = value; }
        }
        /// <summary>
        /// 是否启用输入座位号选座功能
        /// </summary>
        public bool UsingEnterNoForSeat
        {
            get { return _UsingEnterNoForSeat; }
            set { _UsingEnterNoForSeat = value; }
        }
        /// <summary>
        /// 是否启用常坐座位选座功能
        /// </summary>
        public OftenSeat UsingOftenUsedSeat
        {
            get { return _UsingOftenUsedSeat; }
            set { _UsingOftenUsedSeat = value; }
        }
        /// <summary>
        /// 是否启用打印凭条
        /// </summary>
        public PrintSlipMode UsingPrintSlip
        {
            get { return _UsingPrintSlip; }
            set { _UsingPrintSlip = value; }
        }
        /// <summary>
        /// 是否启用预约功能激活
        /// </summary>
        public bool UsingActiveBespeakSeat
        {
            get { return _UsingActiveBespeakSeat; }
            set { _UsingActiveBespeakSeat = value; }
        }
        /// <summary>
        /// 触摸屏所管理的阅览室
        /// </summary>
        public List<string> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        /// <summary>
        /// 背景图片
        /// </summary>
        public Dictionary<string, string> BackImgage
        {
            get { return backImgage; }
            set { backImgage = value; }
        }

        private POSRestrict posTimes = new POSRestrict();
        /// <summary>
        /// 刷卡次数限制
        /// </summary>
        public POSRestrict PosTimes
        {
            get { return posTimes; }
            set { posTimes = value; }
        }

        private ResolutionV2 _SystemResoultion = new ResolutionV2();
        /// <summary>
        /// 系统分辨率
        /// </summary>
        public ResolutionV2 SystemResoultion
        {
            get { return _SystemResoultion; }
            set { _SystemResoultion = value; }
        }
        private WindowCountDown _WinCountDown = new WindowCountDown();
        /// <summary>
        /// 窗体倒计时
        /// </summary>
        public WindowCountDown WinCountDown
        {
            get { return _WinCountDown; }
            set { _WinCountDown = value; }
        }
        /// <summary>
        /// 本卷打印纸已打印次数
        /// </summary>
        public int PrintedTimes
        {
            get { return _PrintedTimes; }
            set { _PrintedTimes = value; }
        }
        /// <summary>
        /// 上一卷打印纸打印总次数
        /// </summary>
        public int LastPrintTimes
        {
            get { return _LastPrintTimes; }
            set { _LastPrintTimes = value; }
        }
        /// <summary>
        /// 打印机是否有纸（0：没纸，1：有纸）
        /// </summary>
        public bool IsAnyPaper
        {
            get { return _IsAnyPaper; }
            set { _IsAnyPaper = value; }
        }
        #endregion
        /// <summary>
        /// 把自身的实例转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Convert(this);
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="xmlClientConfig"></param>
        /// <returns></returns>
        public static ClientConfigV2 Convert(string xmlClientConfig)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlClientConfig);
                ClientConfigV2 config = new ClientConfigV2();
                XmlNode node = doc.SelectSingleNode("//RootNote/ClientNo");
                if (node != null)
                {
                    config.ClientNo = node.InnerText;
                }
                node = doc.SelectSingleNode("//RootNote/ChooseSeatMothed");
                if (node != null)
                {
                    config.SelectMethod = (SelectSeatMode)int.Parse(node.InnerText);
                }
                node = doc.SelectSingleNode("//RootNote/Nokeyboard");
                if (node != null)
                {
                    config.UsingEnterNoForSeat = ConfigConvert.ConvertToBool(node.InnerText);
                }
                node = doc.SelectSingleNode("//RootNote/IsShowInitPOS");
                if (node != null)
                {
                    config.IsShowInitPOS = ConfigConvert.ConvertToBool(node.InnerText);
                }
                node = doc.SelectSingleNode("//RootNote/IsShowClosedRoom");
                if (node != null)
                {
                    config._IsShowClosedRoom = ConfigConvert.ConvertToBool(node.InnerText);
                }
                node = doc.SelectSingleNode("//RootNote/OftenSeat");
                if (node != null)
                {
                    config.UsingOftenUsedSeat.Used = ConfigConvert.ConvertToBool(node.InnerText);
                    config.UsingOftenUsedSeat.LengthDays = int.Parse(node.Attributes["LengthTime"].Value);
                    config.UsingOftenUsedSeat.SeatCount = int.Parse(node.Attributes["SeatCount"].Value);
                }
                node = doc.SelectSingleNode("//RootNote/isPrint");
                if (node != null)
                {
                    config.UsingPrintSlip = (PrintSlipMode)int.Parse(node.InnerText);
                }
                node = doc.SelectSingleNode("//RootNote/Activation");
                if (node != null)
                {
                    config.UsingActiveBespeakSeat = ConfigConvert.ConvertToBool(node.InnerText);
                }
                node = doc.SelectSingleNode("//RootNote/POSRestrict");
                if (node != null)
                {
                    if (node.Attributes["IsUsed"] != null)
                    {
                        config.PosTimes.IsUsed = bool.Parse(node.Attributes["IsUsed"].Value);
                    }
                    config.PosTimes.Minutes = int.Parse(node.Attributes["Minutes"].Value);
                    config.PosTimes.Times = int.Parse(node.Attributes["Times"].Value);
                }
                XmlNodeList nodes = doc.SelectNodes("//RootNote/AddReadingRoom/ReadRoomID");
                foreach (XmlNode element in nodes)
                {
                    config.Rooms.Add(element.InnerText);
                }
                nodes = doc.SelectNodes("//RootNote/img/backgroundimg");
                foreach (XmlNode element in nodes)
                {
                    config.BackImgage[element.Attributes["id"].Value] = element.InnerText;
                }
                node = doc.SelectSingleNode("//RootNote/Resolution/FormSet/size");
                config.SystemResoultion.WindowSize.Size.X = int.Parse(node.Attributes["x"].Value);
                config.SystemResoultion.WindowSize.Size.Y = int.Parse(node.Attributes["y"].Value);
                node = doc.SelectSingleNode("//RootNote/Resolution/FormSet/location");
                config.SystemResoultion.WindowSize.Location.X = int.Parse(node.Attributes["x"].Value);
                config.SystemResoultion.WindowSize.Location.Y = int.Parse(node.Attributes["y"].Value);
                node = doc.SelectSingleNode("//RootNote/Resolution/TooltipSet/size");
                config.SystemResoultion.TooltipSize.Size.X = int.Parse(node.Attributes["x"].Value);
                config.SystemResoultion.TooltipSize.Size.Y = int.Parse(node.Attributes["y"].Value);
                node = doc.SelectSingleNode("//RootNote/Resolution/TooltipSet/location");
                config.SystemResoultion.TooltipSize.Location.X = int.Parse(node.Attributes["x"].Value);
                config.SystemResoultion.TooltipSize.Location.Y = int.Parse(node.Attributes["y"].Value);

                node = doc.SelectSingleNode("//RootNote/WinCountDown");
                if (node != null)
                {
                    config.WinCountDown.AccessActive = int.Parse(node.Attributes["AccessActive"].Value);
                    config.WinCountDown.LastSeatWindow = int.Parse(node.Attributes["LastSeatWindow"].Value);
                    config.WinCountDown.LeaveWindow = int.Parse(node.Attributes["LeaveWindow"].Value);
                    config.WinCountDown.LogSerachWindow = int.Parse(node.Attributes["LogSerachWindow"].Value);
                    config.WinCountDown.MessageWindow = int.Parse(node.Attributes["MessageWindow"].Value);
                    if (node.Attributes["RoomWindow"] != null)
                    {
                        config.WinCountDown.RoomWindow = int.Parse(node.Attributes["RoomWindow"].Value);
                    }
                    if (node.Attributes["SeatWindow"] != null)
                    {
                        config.WinCountDown.SeatWindow = int.Parse(node.Attributes["SeatWindow"].Value);
                    }
                    if (node.Attributes["UsuallySeatWindow"] != null)
                    {
                        config.WinCountDown.UsuallySeatWindow = int.Parse(node.Attributes["UsuallySeatWindow"].Value);
                    }
                    if (node.Attributes["ReaderNoticeWindow"] != null)
                    {
                        config.WinCountDown.ReaderNoticeWindow = int.Parse(node.Attributes["ReaderNoticeWindow"].Value);
                    }
                    if (node.Attributes["KeyboardWindow"] != null)
                    {
                        config.WinCountDown.KeyboardWindow = int.Parse(node.Attributes["KeyboardWindow"].Value);
                    }
                }

                return config;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Convert(ClientConfigV2 config)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("RootNote");
            XmlElement secNode1 = doc.CreateElement("ClientNo");
            secNode1.InnerText = config.ClientNo;
            root.AppendChild(secNode1);
            XmlElement secNode2 = doc.CreateElement("ChooseSeatMothed");
            secNode2.InnerText = ((int)config.SelectMethod).ToString();
            root.AppendChild(secNode2);
            XmlElement secNode3 = doc.CreateElement("Nokeyboard");

            secNode3.InnerText = ConfigConvert.ConvertToString(config.UsingEnterNoForSeat);
            root.AppendChild(secNode3);

            XmlElement secNode4 = doc.CreateElement("OftenSeat");
            secNode4.InnerText = ConfigConvert.ConvertToString(config.UsingOftenUsedSeat.Used);
            secNode4.SetAttribute("LengthTime", config.UsingOftenUsedSeat.LengthDays.ToString());
            secNode4.SetAttribute("SeatCount", config.UsingOftenUsedSeat.SeatCount.ToString());
            root.AppendChild(secNode4);
            XmlElement secNode5 = doc.CreateElement("isPrint");
            secNode5.InnerText = ((int)config.UsingPrintSlip).ToString();
            root.AppendChild(secNode5);
            XmlElement nodeShowClosedRoom = doc.CreateElement("IsShowClosedRoom");
            nodeShowClosedRoom.InnerText = ConfigConvert.ConvertToString(config.IsShowClosedRoom);
            root.AppendChild(nodeShowClosedRoom);
            XmlElement secNode6 = doc.CreateElement("Activation");
            secNode6.InnerText = ConfigConvert.ConvertToString(config.UsingActiveBespeakSeat);
            root.AppendChild(secNode6);

            XmlElement secNode11 = doc.CreateElement("IsShowInitPOS");
            secNode11.InnerText = ConfigConvert.ConvertToString(config.IsShowInitPOS);
            root.AppendChild(secNode11);

            XmlElement secNode7 = doc.CreateElement("POSRestrict");
            secNode7.SetAttribute("Minutes", config.PosTimes.Minutes.ToString());
            secNode7.SetAttribute("Times", config.PosTimes.Times.ToString());
            secNode7.SetAttribute("IsUsed", config.PosTimes.IsUsed.ToString());
            root.AppendChild(secNode7);
            XmlElement secNode8 = doc.CreateElement("AddReadingRoom");
            foreach (string roomNo in config.Rooms)
            {
                XmlElement thirdNode = doc.CreateElement("ReadRoomID");
                thirdNode.InnerText = roomNo;
                secNode8.AppendChild(thirdNode);
            }
            root.AppendChild(secNode8);

            XmlElement secNode9 = doc.CreateElement("img");
            foreach (string elementName in config.BackImgage.Keys)
            {
                XmlElement thirdNode = doc.CreateElement("backgroundimg");
                thirdNode.InnerText = config.BackImgage[elementName];
                thirdNode.SetAttribute("id", elementName);
                secNode9.AppendChild(thirdNode);
            }
            root.AppendChild(secNode9);

            XmlElement secNode10 = doc.CreateElement("Resolution");
            XmlElement thirdNode1 = doc.CreateElement("FormSet");
            XmlElement fourthNode1 = doc.CreateElement("size");
            fourthNode1.SetAttribute("x", config.SystemResoultion.WindowSize.Size.X.ToString());
            fourthNode1.SetAttribute("y", config.SystemResoultion.WindowSize.Size.Y.ToString());
            thirdNode1.AppendChild(fourthNode1);
            XmlElement fourthNode2 = doc.CreateElement("location");
            fourthNode2.SetAttribute("x", config.SystemResoultion.WindowSize.Location.X.ToString());
            fourthNode2.SetAttribute("y", config.SystemResoultion.WindowSize.Location.Y.ToString());
            thirdNode1.AppendChild(fourthNode2);

            secNode10.AppendChild(thirdNode1);
            XmlElement thirdNode2 = doc.CreateElement("TooltipSet");
            XmlElement fourthNode3 = doc.CreateElement("size");
            fourthNode3.SetAttribute("x", config.SystemResoultion.TooltipSize.Size.X.ToString());
            fourthNode3.SetAttribute("y", config.SystemResoultion.TooltipSize.Size.Y.ToString());
            thirdNode2.AppendChild(fourthNode3);
            XmlElement fourthNode4 = doc.CreateElement("location");
            fourthNode4.SetAttribute("x", config.SystemResoultion.TooltipSize.Location.X.ToString());
            fourthNode4.SetAttribute("y", config.SystemResoultion.TooltipSize.Location.Y.ToString());
            thirdNode2.AppendChild(fourthNode4);
            secNode10.AppendChild(thirdNode2);
            root.AppendChild(secNode10);

            XmlElement secNode12 = doc.CreateElement("WinCountDown");
            secNode12.SetAttribute("AccessActive", config.WinCountDown.AccessActive.ToString());
            secNode12.SetAttribute("LastSeatWindow", config.WinCountDown.LastSeatWindow.ToString());
            secNode12.SetAttribute("LeaveWindow", config.WinCountDown.LeaveWindow.ToString());
            secNode12.SetAttribute("LogSerachWindow", config.WinCountDown.LogSerachWindow.ToString());
            secNode12.SetAttribute("MessageWindow", config.WinCountDown.MessageWindow.ToString());
            secNode12.SetAttribute("RoomWindow", config.WinCountDown.RoomWindow.ToString());
            secNode12.SetAttribute("SeatWindow", config.WinCountDown.SeatWindow.ToString());
            secNode12.SetAttribute("KeyboardWindow", config.WinCountDown.KeyboardWindow.ToString());
            secNode12.SetAttribute("ReaderNoticeWindow", config.WinCountDown.ReaderNoticeWindow.ToString());
            secNode12.SetAttribute("UsuallySeatWindow", config.WinCountDown.UsuallySeatWindow.ToString());
            root.AppendChild(secNode12);

            doc.AppendChild(root);
            return doc.OuterXml;
        }
    }
    /// <summary>
    /// 系统分辨率
    /// </summary>
    [Serializable]
    public class ResolutionV2
    {
        private FormSize _WindowSize = new FormSize();

        public FormSize WindowSize
        {
            get { return _WindowSize; }
            set { _WindowSize = value; }
        }
        private FormSize _TooltipSize = new FormSize();

        public FormSize TooltipSize
        {
            get { return _TooltipSize; }
            set { _TooltipSize = value; }
        }
        public ResolutionV2()
        {
        }
        public ResolutionV2(string x)
        {
            switch (x)
            {
                case "1080":
                    _WindowSize.Location.X = 0;
                    _WindowSize.Location.Y = 920;
                    _WindowSize.Size.X = 1080;
                    _WindowSize.Size.Y = 1000;
                    _TooltipSize.Location.X = 245;
                    _TooltipSize.Location.Y = 1165;
                    _TooltipSize.Size.X = 590;
                    _TooltipSize.Size.Y = 470;
                    break;
                case "1024":
                    _WindowSize.Location.X = 0;
                    _WindowSize.Location.Y = 0;
                    _WindowSize.Size.X = 1024;
                    _WindowSize.Size.Y = 768;
                    _TooltipSize.Location.X = 217;
                    _TooltipSize.Location.Y = 145;
                    _TooltipSize.Size.X = 590;
                    _TooltipSize.Size.Y = 470;
                    break;
                case "1280":
                    _WindowSize.Location.X = 0;
                    _WindowSize.Location.Y = 0;
                    _WindowSize.Size.X = 1280;
                    _WindowSize.Size.Y = 800;
                    _TooltipSize.Location.X = 217;
                    _TooltipSize.Location.Y = 145;
                    _TooltipSize.Size.X = 590;
                    _TooltipSize.Size.Y = 470;
                    break;
                case "1440":
                    _WindowSize.Location.X = 0;
                    _WindowSize.Location.Y = 0;
                    _WindowSize.Size.X = 1440;
                    _WindowSize.Size.Y = 900;
                    _TooltipSize.Location.X = 217;
                    _TooltipSize.Location.Y = 145;
                    _TooltipSize.Size.X = 590;
                    _TooltipSize.Size.Y = 470;
                    break;
                case "1920":
                    _WindowSize.Location.X = 0;
                    _WindowSize.Location.Y = 0;
                    _WindowSize.Size.X = 1920;
                    _WindowSize.Size.Y = 1080;
                    _TooltipSize.Location.X = 217;
                    _TooltipSize.Location.Y = 145;
                    _TooltipSize.Size.X = 590;
                    _TooltipSize.Size.Y = 470;
                    break;
                default:
                    _WindowSize.Location.X = 0;
                    _WindowSize.Location.Y = 920;
                    _WindowSize.Size.X = 1080;
                    _WindowSize.Size.Y = 1000;
                    _TooltipSize.Location.X = 245;
                    _TooltipSize.Location.Y = 1165;
                    _TooltipSize.Size.X = 590;
                    _TooltipSize.Size.Y = 470;
                    break;
            }
        }
    }
    /// <summary>
    /// 窗口倒计时设置
    /// </summary>
    public class WindowCountDown
    {
        private int _LogSerachWindow = 60;
        /// <summary>
        /// 记录查询窗体
        /// </summary>
        public int LogSerachWindow
        {
            get { return _LogSerachWindow; }
            set { _LogSerachWindow = value; }
        }
        private int _RoomWindow = 60;
        /// <summary>
        /// 阅览室列表窗体
        /// </summary>
        public int RoomWindow
        {
            get { return _RoomWindow; }
            set { _RoomWindow = value; }
        }
        private int _SeatWindow = 60;
        /// <summary>
        /// 座位视图窗体
        /// </summary>
        public int SeatWindow
        {
            get { return _SeatWindow; }
            set { _SeatWindow = value; }
        }
        private int _LastSeatWindow = 30;
        /// <summary>
        /// 剩余座位窗体
        /// </summary>
        public int LastSeatWindow
        {
            get { return _LastSeatWindow; }
            set { _LastSeatWindow = value; }
        }
        private int _AccessActive = 20;
        /// <summary>
        /// 预约激活窗体
        /// </summary>
        public int AccessActive
        {
            get { return _AccessActive; }
            set { _AccessActive = value; }
        }
        private int _LeaveWindow = 10;
        /// <summary>
        /// 离开操作窗体
        /// </summary>
        public int LeaveWindow
        {
            get { return _LeaveWindow; }
            set { _LeaveWindow = value; }
        }
        private int _MessageWindow = 7;
        /// <summary>
        /// 消息提示窗体
        /// </summary>
        public int MessageWindow
        {
            get { return _MessageWindow; }
            set { _MessageWindow = value; }
        }
        private int _ReaderNoticeWindow = 10;
        /// <summary>
        /// 读者信息提示
        /// </summary>
        public int ReaderNoticeWindow
        {
            get { return _ReaderNoticeWindow; }
            set { _ReaderNoticeWindow = value; }
        }

        private int _UsuallySeatWindow = 20;
        /// <summary>
        /// 常坐座位
        /// </summary>
        public int UsuallySeatWindow
        {
            get { return _UsuallySeatWindow; }
            set { _UsuallySeatWindow = value; }
        }
        private int _keyboardWindow = 30;
        /// <summary>
        /// 键盘窗体
        /// </summary>
        public int KeyboardWindow
        {
            get { return _keyboardWindow; }
            set { _keyboardWindow = value; }
        }
    }
}
