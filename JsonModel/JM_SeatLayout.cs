using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_SeatLayout
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
        private List<JM_Seat> seats = new List<JM_Seat>();
        /// <summary>
        /// 座位布局
        /// </summary>
        public List<JM_Seat> Seats
        {
            get { return seats; }
            set { seats = value; }
        }
        private List<JM_Node> nodes = new List<JM_Node>();
        /// <summary>
        /// 备注与装饰物
        /// </summary>
        public List<JM_Node> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }
    }



    public class JM_Node : JM_Element
    {
        string _Remark = "";
        /// <summary>
        /// 注释
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private string _Type;
        /// <summary>
        /// 备注的装饰物类型
        /// </summary>
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
    }
}
