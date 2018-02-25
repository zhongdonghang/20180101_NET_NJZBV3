using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;
using SeatManage.ClassModel;
using System.Xml;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 触摸屏终端设置
    /// </summary>
    [Serializable]
    public class ClientConfig
    {
        public ClientConfig()
        {
            BackImgage.Add("BookActivation", @"images\ClientBackImage\BookActivation.png");
            BackImgage.Add("btn_violate", @"images\ClientBackImage\btn_violate.png");
            BackImgage.Add("btnAutoChooseSeat", @"images\ClientBackImage\btnAutoChooseSeat.png");
            BackImgage.Add("btnKeyboard", @"images\ClientBackImage\btnKeyboard.png");
            BackImgage.Add("btnOftenSeat", @"images\ClientBackImage\btnOftenSeat.png");
            BackImgage.Add("btnQureyLog", @"images\ClientBackImage\btnQureyLog.png");
            BackImgage.Add("Exit", @"images\ClientBackImage\Exit.png");
            BackImgage.Add("ChooseReadingRoom", @"images\ClientBackImage\ChooseReadingRoom.png");
            BackImgage.Add("ChooseSeatState", @"images\ClientBackImage\ChooseSeatState.png");
            BackImgage.Add("EnterOutForm", @"images\ClientBackImage\EnterOutForm.png");
            BackImgage.Add("FrmShowEnterOutLog", @"images\ClientBackImage\FrmShowEnterOutLog.png");
            BackImgage.Add("readRoomNameLong", @"images\ClientBackImage\readRoomNameLong.png");
            BackImgage.Add("readRoomNameShort", @"images\ClientBackImage\readRoomNameShort.png");
            BackImgage.Add("seatSmall", @"images\ClientBackImage\seatSmall.png");
            BackImgage.Add("SelectSeatForm", @"images\ClientBackImage\SelectSeatForm.png");
            BackImgage.Add("btnSelfChooseSeat", @"images\ClientBackImage\btnSelfChooseSeat.png");
            //BackImgage.Add("btn_leave", @"images\ClientBackImage\btn_leave.png");
            //BackImgage.Add("btn_leave_p", @"images\ClientBackImage\btn_leave_p.png");
            //BackImgage.Add("btn_busy", @"images\ClientBackImage\btn_busy.png");
            //BackImgage.Add("btn_busy_p", @"images\ClientBackImage\btn_busy_p.png");
            //BackImgage.Add("btn_free", @"images\ClientBackImage\btn_free.png");
            //BackImgage.Add("btn_free_p", @"images\ClientBackImage\btn_free_p.png");
            BackImgage.Add("AdImage", @"images\ClientBackImage\AdImage.png");
            //BackImgage.Add("btn_su", @"images\ClientBackImage\btn_su.png");
            //BackImgage.Add("btn_pw_su", @"images\ClientBackImage\btn_pw_su.png");

            BackImgage.Add("ImgBook", @"images\ClientBackImage\ImgBook.png");
            BackImgage.Add("ImgShortLeave", @"images\ClientBackImage\ImgShortLeave.png");
            BackImgage.Add("ImgReader", @"images\ClientBackImage\ImgReader.png");
            BackImgage.Add("ImgStopUse", @"images\ClientBackImage\ImgStopUse.png");
            BackImgage.Add("ImgPower", @"images\ClientBackImage\ImgPower.png");
            BackImgage.Add("ImgSeat", @"images\ClientBackImage\ImgSeat.png");
            BackImgage.Add("ImgSeatUse", @"images\ClientBackImage\ImgSeatUse.png");


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
        private RoomICOType roomType = RoomICOType.Big;//房间类型
        private SelectSeatMode selectMethod = SelectSeatMode.Default;//选座方式
        private bool _UsingEnterNoForSeat = true;//输入座位号选座功能
        private OftenSeat _UsingOftenUsedSeat = new OftenSeat();//常做座位选座
        private bool _UsingPrintSlip = true;//是否打印凭条
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
        /// 房间图标类型
        /// </summary>
        public RoomICOType RoomType
        {
            get { return roomType; }
            set { roomType = value; }
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
        public bool UsingPrintSlip
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

        private Resolution _SystemResoultion = new Resolution();
        /// <summary>
        /// 系统分辨率
        /// </summary>
        public Resolution SystemResoultion
        {
            get { return _SystemResoultion; }
            set { _SystemResoultion = value; }
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
        public static ClientConfig Convert(string xmlClientConfig)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlClientConfig);
                ClientConfig config = new ClientConfig();
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
                    config.UsingPrintSlip = ConfigConvert.ConvertToBool(node.InnerText);
                }
                node = doc.SelectSingleNode("//RootNote/Activation");
                if (node != null)
                {
                    config.UsingActiveBespeakSeat = ConfigConvert.ConvertToBool(node.InnerText);
                }
                node = doc.SelectSingleNode("//RootNote/POSRestrict");
                if (node != null)
                {
                    config.PosTimes.Minutes = int.Parse(node.Attributes["Minutes"].Value);
                    config.PosTimes.Times = int.Parse(node.Attributes["Times"].Value);
                    if (node.Attributes["IsUsed"] != null)
                    {
                        config.PosTimes.IsUsed = ConfigConvert.ConvertToBool(node.Attributes["IsUsed"].Value);
                    }
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
                return config;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Convert(ClientConfig config)
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
            secNode5.InnerText = ConfigConvert.ConvertToString(config.UsingPrintSlip);
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
            secNode7.SetAttribute("IsUsed", ConfigConvert.ConvertToString(config.PosTimes.IsUsed));
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
            doc.AppendChild(root);
            return doc.OuterXml;
        }
    }
    public class ConfigConvert
    {

        /// <summary>
        /// 1/0转换成bool类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ConvertToBool(string value)
        {
            switch (value)
            {
                case "0":
                    return false;

                case "1":
                    return true;
            }
            return false;
        }
        /// <summary>
        /// bool转换成1/0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToString(bool value)
        {
            switch (value)
            {
                case true:
                    return "1";
                case false:
                    return "0";
            }
            return "0";
        }
    }

    /// <summary>
    /// 系统分辨率
    /// </summary>
    [Serializable]
    public class Resolution
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
        public Resolution()
        {
        }
        public Resolution(string x)
        {
            switch (x)
            {
                case "1080":
                    _WindowSize.Location.X = 0;
                    _WindowSize.Location.Y = 920;
                    _WindowSize.Size.X = 1080;
                    _WindowSize.Size.Y = 1000;
                    _TooltipSize.Location.X = 280;
                    _TooltipSize.Location.Y = 1242;
                    _TooltipSize.Size.X = 490;
                    _TooltipSize.Size.Y = 288;
                    break;
                case "1024":
                    _WindowSize.Location.X = 0;
                    _WindowSize.Location.Y = 0;
                    _WindowSize.Size.X = 1024;
                    _WindowSize.Size.Y = 768;
                    _TooltipSize.Location.X = 214;
                    _TooltipSize.Location.Y = 202;
                    _TooltipSize.Size.X = 490;
                    _TooltipSize.Size.Y = 288;
                    break;
            }
        }
    }
    /// <summary>
    /// 窗体尺寸和位置
    /// </summary>
    [Serializable]
    public class FormSize
    {
        LocationSize _FormSize = new LocationSize();

        public LocationSize Size
        {
            get { return _FormSize; }
            set { _FormSize = value; }
        }
        LocationSize _Location = new LocationSize();

        public LocationSize Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
    }
    [Serializable]
    public class LocationSize
    {
        int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }

    /// <summary>
    /// 刷卡限制
    /// </summary>
    [Serializable]
    public class POSRestrict
    {
        private bool isUsed = true;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }
        private int minutes = 10;
        /// <summary>
        /// 分钟数
        /// </summary>
        public int Minutes
        {
            get { return minutes; }
            set { minutes = value; }
        }
        private int times = 3;
        /// <summary>
        /// 限制次数
        /// </summary>
        public int Times
        {
            get { return times; }
            set { times = value; }
        }
        /// <summary>
        /// 把自身转换成XmlNode
        /// </summary>
        /// <returns></returns>
        public XmlElement ToXmlNode()
        {
            return ToXmlNode(this, new XmlDocument());
        }
        public static XmlElement ToXmlNode(POSRestrict set, XmlDocument doc)
        {
            XmlElement secNode = doc.CreateElement("POSRestrict");
            if (set == null)
            {
                set = new POSRestrict() { Minutes = 10, Times = 3, IsUsed = true };
            }
            secNode.SetAttribute("IsUsed", ConfigConvert.ConvertToString(set.IsUsed));
            secNode.SetAttribute("Minutes", set.Minutes.ToString());
            secNode.SetAttribute("Times", set.Times.ToString());
            return secNode;
        }

        public static POSRestrict ToObject(XmlNode node)
        {
            POSRestrict set = new POSRestrict();
            if (node != null)
            {
                set.Minutes = int.Parse(node.Attributes["Minutes"].Value);
                set.Times = int.Parse(node.Attributes["Times"].Value);
                if (node.Attributes["IsUsed"] != null)
                {
                    set.IsUsed = ConfigConvert.ConvertToBool(node.Attributes["IsUsed"].Value);
                }
            }
            return set;
        }
    }
    /// <summary>
    /// 常坐的座位
    /// </summary>
    [Serializable]
    public class OftenSeat
    {
        int _LengthDays = 15;
        /// <summary>
        /// 统计天数
        /// </summary>
        public int LengthDays
        {
            get { return _LengthDays; }
            set { _LengthDays = value; }
        }
        int _SeatCount = 12;
        /// <summary>
        /// 常用座位数
        /// </summary>
        public int SeatCount
        {
            get { return _SeatCount; }
            set { _SeatCount = value; }
        }
        bool _Used = true;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }
        /// <summary>
        /// 把自身转换成XmlNode
        /// </summary>
        /// <returns></returns>
        public XmlElement ToXmlNode()
        {
            return ToXmlNode(this);
        }
        public static XmlElement ToXmlNode(OftenSeat oftenSeat)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement secNode = doc.CreateElement("OftenSeat");
            secNode.InnerText = ConfigConvert.ConvertToString(oftenSeat.Used);
            secNode.SetAttribute("LengthTime", oftenSeat.LengthDays.ToString());
            secNode.SetAttribute("SeatCount", oftenSeat.SeatCount.ToString());
            return secNode;
        }

        public static OftenSeat ToObject(XmlNode node)
        {
            OftenSeat oftenSeat = new OftenSeat();
            if (node != null)
            {
                oftenSeat.Used = ConfigConvert.ConvertToBool(node.InnerText);
                oftenSeat.LengthDays = int.Parse(node.Attributes["LengthTime"].Value);
                oftenSeat.SeatCount = int.Parse(node.Attributes["SeatCount"].Value);
            }
            return oftenSeat;
        }
    }


}
