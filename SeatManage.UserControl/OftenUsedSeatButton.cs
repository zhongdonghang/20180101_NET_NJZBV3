using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeatManage.MyUserControl
{
    public partial class OftenUsedSeatButton : UserControl
    {
        public OftenUsedSeatButton()
        {
            InitializeComponent();
        }

        private string _ReadingRoomNo = "";
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string ReadingRoomNo
        {
            get { return _ReadingRoomNo; }
            set { _ReadingRoomNo = value; }
        }
        private string _ShortSeatNo = "";
        /// <summary>
        /// 显示的座位号
        /// </summary>
        public string ShortSeatNo
        {
            get { return _ShortSeatNo; }
            set
            {
                _ShortSeatNo = value;
                label1.Text = string.Format("{0} {1}", _ReadingRoomName, _ShortSeatNo);
            }
        }
        private string _SeatNo = "";
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get { return _SeatNo; }
            set { _SeatNo = value; }
        }
        private string _ReadingRoomName = "";
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set
            {
                _ReadingRoomName = value;
                label1.Text = string.Format("{0} {1}", _ReadingRoomName, _ShortSeatNo);
            }
        }

        public override string Text
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
    }
}
