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
    public partial class ReadingRoomButton : UserControl
    {
        public ReadingRoomButton()
        {
            InitializeComponent();
        }

        private string _RoomNum = "";
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string RoomNum
        {
            get { return _RoomNum; }
            set { _RoomNum = value; }
        }

        private SeatManage.EnumType.ReadingRoomStatus _RoomStatus = SeatManage.EnumType.ReadingRoomStatus.Close;
        /// <summary>
        /// 阅览室开闭馆状态
        /// </summary>
        public SeatManage.EnumType.ReadingRoomStatus RoomStatus
        {
            get
            {
                return _RoomStatus;
            }
            set
            {
                _RoomStatus = value;
                switch (value)
                {
                    case SeatManage.EnumType.ReadingRoomStatus.BeforeClose:
                        this.BackgroundImage = global::SeatManage.Properties.Resources.gray;
                        break;
                    case SeatManage.EnumType.ReadingRoomStatus.Close:
                        this.BackgroundImage = global::SeatManage.Properties.Resources.gray;
                        break;
                    default:
                        roomBackImage();
                        break;
                }
            }
        }

        private SeatManage.EnumType.ReadingRoomUsingStatus _SeatUsingStatus = SeatManage.EnumType.ReadingRoomUsingStatus.Normal;
        public SeatManage.EnumType.ReadingRoomUsingStatus SeatUsingStatus
        {
            get
            {
                return _SeatUsingStatus;
            }
            set
            {
                _SeatUsingStatus = value;
                roomBackImage();
            }
        }

        private void roomBackImage()
        {
            if (_RoomStatus != SeatManage.EnumType.ReadingRoomStatus.Close && _RoomStatus != SeatManage.EnumType.ReadingRoomStatus.BeforeClose)
            {
                switch (_SeatUsingStatus)
                {

                    case SeatManage.EnumType.ReadingRoomUsingStatus.Crowd:
                        this.BackgroundImage = global::SeatManage.Properties.Resources.btn_yellow;
                        break;
                    case SeatManage.EnumType.ReadingRoomUsingStatus.Full:
                        this.BackgroundImage = global::SeatManage.Properties.Resources.btn_red;
                        break;
                    case SeatManage.EnumType.ReadingRoomUsingStatus.Normal:
                    default:
                        this.BackgroundImage = global::SeatManage.Properties.Resources.btn_blue;
                        break;
                }
            }
        }

        public override string Text
        {
            get
            {
                return roomName.Text;
            }
            set
            {
                roomName.Text = value;
            }
        }
        /// <summary>
        /// 显示的阅览室按钮信息
        /// </summary>
        public string RoomName
        {
            get
            {
                return roomName.Text;
            }
            set
            {
                roomName.Text = value;
            }
        }
        /// <summary>
        /// 座位使用信息
        /// </summary>
        public string RoomUsedTip
        {
            get
            {
                return roomUsedTip.Text;
            }
            set
            {
                roomUsedTip.Text = value;
            }

        }

        

        private void roomName_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }
         
    }
}
