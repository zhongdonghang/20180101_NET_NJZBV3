using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;

namespace SeatConfig.Code
{
    public class SeatCanvas
    {
        SeatManage.ClassModel.SeatLayout _Layout = new SeatManage.ClassModel.SeatLayout();
        private ObservableCollection<SeatManage.ClassModel.Seat> _seats = new ObservableCollection<SeatManage.ClassModel.Seat>();
        private ObservableCollection<SeatManage.ClassModel.Note> _notes = new ObservableCollection<SeatManage.ClassModel.Note>();
        private string seatRow = "";
        private string seatCol = "";
        private string roomNo = "";

        public string SeatRow
        {
            get { return seatRow; }
            set { seatRow = value; }
        }

        public string SeatCol
        {
            get { return seatCol; }
            set { seatCol = value; }
        }

        public string RoomNo
        {
            get { return roomNo; }
            set { roomNo = value; }
        }
        public ObservableCollection<SeatManage.ClassModel.Seat> Seats
        {
            get { return _seats; }
            set { _seats = value; }
        }
        public ObservableCollection<SeatManage.ClassModel.Note> notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        public SeatCanvas(Dictionary<string, SeatManage.ClassModel.Seat> seats, List<SeatManage.ClassModel.Note> notes)
        {
            foreach (KeyValuePair<string, SeatManage.ClassModel.Seat> seat in seats)
            {
                this._seats.Add(seat.Value);
            }
            for (int i = 0; i < notes.Count; i++)
            {
                this._notes.Add(notes[i]);
            }
        }

        public SeatCanvas()
        { 
        
        }
        
      

        //保存XML
        public void Save()
        {
            //XmlDocument doc = new XmlDocument();
            //try
            //{
            //    //创建开头
            //    XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            //    doc.AppendChild(dec);
            //    XmlElement Rootelement = doc.CreateElement("seatroot");//最后添加到Doc
               
            //    XmlElement SecNode1 = doc.CreateElement("seatinfo");
            //    //行
            //    XmlElement ThirdNode1 = doc.CreateElement("rowscount");
            //    ThirdNode1.InnerText = this.SeatRow;
            //   //列
            //    XmlElement ThirdNode2 = doc.CreateElement("columscount");
            //    ThirdNode2.InnerText = this.SeatCol;
            //    SecNode1.AppendChild(ThirdNode1);
            //    SecNode1.AppendChild(ThirdNode2);

            //    Rootelement.AppendChild(SecNode1);
            //    //创建座位节点
            //    XmlElement SecNode2 = doc.CreateElement("seatdetail");
            //    for (int i = 0; i < this.Seats.Count; i++)
            //    {
            //        XmlElement seatNode = doc.CreateElement("objseat");
            //        seatNode.SetAttribute("seatno", RoomNo + Seats[i].SeatNo);
            //        seatNode.SetAttribute("haspower",Seats[i].HasPower);
            //        seatNode.SetAttribute("rownum",Seats[i].RowNum);
            //        seatNode.SetAttribute("colnum", Seats[i].ColNum);
            //        seatNode.SetAttribute("booktype",Seats[i].BookType);
            //        seatNode.SetAttribute("begintime", Seats[i].BeginTime);
            //        seatNode.SetAttribute("endtime",Seats[i].EndTime);
            //        SecNode2.AppendChild(seatNode);
            //    }
            //    Rootelement.AppendChild(SecNode2);
            //    //创建备注节点。。
            //    XmlElement seatNotes = doc.CreateElement("seatnotes");
            //    for (int i = 0; i < this.notes.Count; i++)
            //    {
            //        XmlElement node = doc.CreateElement("seatnote");
            //        node.SetAttribute("strnote",notes[i].StrNote);
            //        node.SetAttribute("rownum",notes[i].RowNum);
            //        node.SetAttribute("colnum",notes[i].ColNum);
            //        seatNotes.AppendChild(node);
            //    }
            //    Rootelement.AppendChild(seatNotes);
            //    doc.AppendChild(Rootelement);
            //    doc.Save(xmlpath);

            //}
            //catch(Exception ex)
            //{
            //   throw ex;
            //}
        }
    }
}
