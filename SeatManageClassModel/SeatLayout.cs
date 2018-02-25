using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    [Serializable]
    /// <summary>
    /// 房间布局
    /// </summary>
    public class SeatLayout
    {
        private int seatRow = 0;
        /// <summary>
        /// 高度
        /// </summary>
        public int SeatRow
        {
            get { return seatRow; }
            set { seatRow = value; }
        }
        private int seatCol = 0;
        /// <summary>
        /// 宽度
        /// </summary>
        public int SeatCol
        {
            get { return seatCol; }
            set { seatCol = value; }
        }
        private string roomNo = "";
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string RoomNo
        {
            get { return roomNo; }
            set { roomNo = value; }
        }
        private string position = "";
        /// <summary>
        /// 方位
        /// </summary>
        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        private bool _isUpdate = true;
        private Dictionary<string, Seat> seats = new Dictionary<string, Seat>();
        /// <summary>
        /// 座位
        /// </summary>
        public Dictionary<string, Seat> Seats
        {
            get { return seats; }
            set { seats = value; }
        }
        private List<Note> notes = new List<Note>();
        /// <summary>
        /// 备注
        /// </summary>
        public List<Note> Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        /// <summary>
        /// 是否有更新
        /// </summary>
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set { _isUpdate = value; }
        }

        public override string ToString()
        {
            return CreadSeatXml(this);
        }
        /// <summary>
        /// 通过座位布局创建Xml
        /// </summary>
        /// <returns></returns>
        public static string CreadSeatXml(SeatLayout seatLayout)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                //创建开头
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                XmlElement Rootelement = doc.CreateElement("seatroot");//最后添加到Doc

                XmlElement SecNode1 = doc.CreateElement("seatinfo");
                //行
                XmlElement ThirdNode1 = doc.CreateElement("rowscount");
                ThirdNode1.InnerText = seatLayout.SeatRow.ToString();
                //列
                XmlElement ThirdNode2 = doc.CreateElement("columscount");
                ThirdNode2.InnerText = seatLayout.SeatCol.ToString();

                //方位
                XmlElement ThirdNode3 = doc.CreateElement("position");
                ThirdNode3.InnerText = seatLayout.Position.ToString();

                XmlElement ThirdNode4 = doc.CreateElement("IsUpdate");
                ThirdNode4.InnerText = seatLayout.IsUpdate.ToString();

                SecNode1.AppendChild(ThirdNode1);
                SecNode1.AppendChild(ThirdNode2);
                SecNode1.AppendChild(ThirdNode3);
                SecNode1.AppendChild(ThirdNode4);

                Rootelement.AppendChild(SecNode1);
                //创建座位节点
                XmlElement SecNode2 = doc.CreateElement("seatdetail");
                foreach (string seatNo in seatLayout.Seats.Keys)
                {
                    XmlElement seatNode = doc.CreateElement("objseat");
                    //   seatNode.SetAttribute("seatno", seatLayout.RoomNo + seatLayout.Seats[seatNo].ShortSeatNo);
                    seatNode.SetAttribute("seatno", seatLayout.Seats[seatNo].SeatNo);
                    seatNode.SetAttribute("haspower", ConfigConvert.ConvertToString(seatLayout.Seats[seatNo].HavePower));
                    seatNode.SetAttribute("isstop", ConfigConvert.ConvertToString(seatLayout.Seats[seatNo].IsSuspended));
                    seatNode.SetAttribute("rownum", seatLayout.Seats[seatNo].PositionY.ToString());
                    seatNode.SetAttribute("colnum", seatLayout.Seats[seatNo].PositionX.ToString());
                    seatNode.SetAttribute("booktype", Convert.ToInt32(seatLayout.Seats[seatNo].CanBeBespeak).ToString());
                    seatNode.SetAttribute("centerx", seatLayout.Seats[seatNo].RotationCenterX.ToString());
                    seatNode.SetAttribute("centery", seatLayout.Seats[seatNo].RotationCenterY.ToString());
                    seatNode.SetAttribute("angle", seatLayout.Seats[seatNo].RotationAngle.ToString());
                    SecNode2.AppendChild(seatNode);
                }
                Rootelement.AppendChild(SecNode2);
                //创建备注节点。。
                XmlElement seatNotes = doc.CreateElement("seatnotes");
                for (int i = 0; i < seatLayout.Notes.Count; i++)
                {
                    XmlElement node = doc.CreateElement("seatnote");
                    node.SetAttribute("strnote", seatLayout.Notes[i].Remark);
                    node.SetAttribute("rownum", seatLayout.Notes[i].PositionY.ToString());
                    node.SetAttribute("colnum", seatLayout.Notes[i].PositionX.ToString());
                    node.SetAttribute("centerx", seatLayout.Notes[i].RotationCenterX.ToString());
                    node.SetAttribute("centery", seatLayout.Notes[i].RotationCenterY.ToString());
                    node.SetAttribute("angle", seatLayout.Notes[i].RotationAngle.ToString());
                    node.SetAttribute("type", ((int)seatLayout.Notes[i].Type).ToString());
                    node.SetAttribute("baseHeight", seatLayout.Notes[i].BaseHeight.ToString());
                    node.SetAttribute("baseWidth", seatLayout.Notes[i].BaseWidth.ToString());
                    seatNotes.AppendChild(node);
                    // throw new Exception("老的构造方法");
                }
                Rootelement.AppendChild(seatNotes);
                doc.AppendChild(Rootelement);
                return doc.OuterXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 判断座位是否提供预约
        /// </summary>
        /// <param name="canBeBook"></param>
        /// <returns></returns>
        private static string getCanBeBook(bool canBeBook)
        {
            switch (canBeBook)
            {
                case true:
                    return "1";
                case false:
                    return "0";
            }
            return "0";
        }

        private static bool getCanBeBook(string canBeBook)
        {
            switch (canBeBook)
            {
                case "1":
                    return true;
                case "0":
                    return false;
            }
            return false;
        }

        /// <summary>
        /// 通过座位的XML创建座位布局
        /// </summary>
        /// <param name="seatLayoutXml"></param>
        /// <returns></returns>
        public static SeatLayout GetSeatLayout(string seatLayoutXml)
        {
            SeatLayout layout = getSeats(seatLayoutXml);
            layout.Notes = getNodes(seatLayoutXml);
            return layout;
        }

        /// <summary>
        /// 解析座位配置
        /// </summary>
        /// <param name="seatXml"></param>
        /// <returns></returns>
        private static SeatLayout getSeats(string seatXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(seatXml);
            SeatLayout seats = new SeatLayout();
            //查找行数和列数
            XmlNode node = doc.SelectSingleNode("//seatroot/seatinfo/rowscount");
            seats.SeatRow = int.Parse(node.InnerText);
            node = doc.SelectSingleNode("//seatroot/seatinfo/columscount");
            seats.SeatCol = int.Parse(node.InnerText);
            //查找方位
            node = doc.SelectSingleNode("//seatroot/seatinfo/position");
            if (node != null)
            {
                seats.Position = node.InnerText;
            }
            node = doc.SelectSingleNode("//seatroot/seatinfo/IsUpdate");
            if (node != null)
            {
                seats.IsUpdate = bool.Parse(node.InnerText);
            }
            //查找座位
            XmlNodeList nodes = doc.SelectNodes("//seatroot/seatdetail/objseat");
            Dictionary<string, Seat> seatList = new Dictionary<string, Seat>();
            foreach (XmlNode element in nodes)
            {
                try
                {
                    Seat seat = new Seat();
                    seat.ShortSeatNo = element.Attributes["seatno"].Value.Substring(6, 3);
                    seat.SeatNo = element.Attributes["seatno"].Value;
                    seat.PositionY = double.Parse(element.Attributes["rownum"].Value);
                    seat.PositionX = double.Parse(element.Attributes["colnum"].Value);
                    seat.HavePower = ConfigConvert.ConvertToBool(element.Attributes["haspower"].Value);
                    seat.CanBeBespeak = getCanBeBook(element.Attributes["booktype"].Value);
                    if (element.Attributes["centerx"] != null)
                    {
                        seat.RotationCenterX = int.Parse(element.Attributes["centerx"].Value);
                    }
                    if (element.Attributes["centery"] != null)
                    {
                        seat.RotationCenterY = int.Parse(element.Attributes["centery"].Value);
                    }
                    if (element.Attributes["angle"] != null)
                    {
                        seat.RotationAngle = int.Parse(element.Attributes["angle"].Value);
                    }
                    if (element.Attributes["isstop"] != null)
                    {
                        seat.IsSuspended = ConfigConvert.ConvertToBool(element.Attributes["isstop"].Value);
                    }
                    seatList.Add(seat.SeatNo, seat);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("解析失败座位失败：{0}；座位编号：{1}", ex.Message, element.Attributes["seatno"].Value));

                }
            }

            Seat temp = new Seat();//交换变量
            //排序
            seatList = (from entry in seatList
                        orderby entry.Value.SeatNo ascending
                        select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

            seats.Seats = seatList;
            return seats;
        }



        /// <summary>
        /// 获取备注节点
        /// </summary>
        /// <param name="seatXml"></param>
        /// <returns></returns>
        private static List<Note> getNodes(string seatXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(seatXml);
            XmlNodeList nodes = doc.SelectNodes("//seatroot/seatnotes/seatnote");
            List<Note> model = new List<Note>();
            foreach (XmlNode element in nodes)
            {
                Note note = new Note();
                note.PositionX = double.Parse(element.Attributes["colnum"].Value);
                note.PositionY = double.Parse(element.Attributes["rownum"].Value);
                note.Remark = element.Attributes["strnote"].Value;
                if (element.Attributes["centerx"] != null)
                {
                    note.RotationCenterX = int.Parse(element.Attributes["centerx"].Value);
                }
                if (element.Attributes["centery"] != null)
                {
                    note.RotationCenterY = int.Parse(element.Attributes["centery"].Value);
                }
                if (element.Attributes["angle"] != null)
                {
                    note.RotationAngle = int.Parse(element.Attributes["angle"].Value);
                }
                if (element.Attributes["type"] != null)
                {
                    note.Type = (EnumType.OrnamentType)int.Parse(element.Attributes["type"].Value);
                }
                if (element.Attributes["baseHeight"] != null)
                {
                    note.BaseHeight = int.Parse(element.Attributes["baseHeight"].Value);
                }
                if (element.Attributes["baseWidth"] != null)
                {
                    note.BaseWidth = int.Parse(element.Attributes["baseWidth"].Value);
                }
                model.Add(note);
            }
            return model;
        }
    }
}
