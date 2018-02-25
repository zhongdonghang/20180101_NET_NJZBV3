using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeatManage
{
    public partial class Tip_WaitSeat : UserControl
    {  
        SeatClient.OperateResult.SystemObject clientobject = SeatClient.OperateResult.SystemObject.GetInstance();
        public Tip_WaitSeat()
        {
            InitializeComponent();
             ClassModel.ReadingRoomInfo roomInfo =  clientobject.EnterOutLogData.Student.AtReadingRoom;
            string shortSeatNo = SeatManageComm.SeatComm.SeatNoToShortSeatNo(roomInfo.Setting.SeatNumAmount, clientobject.EnterOutLogData.EnterOutlog.SeatNo);
            lblSeatInfo.Text = string.Format("   {0}  {1} 座位", roomInfo.Name,shortSeatNo);
        }
    }
}
