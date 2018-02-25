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
    public partial class Tip_ComeBack : UserControl
    {
        SeatClient.OperateResult.SystemObject clientobject = SeatClient.OperateResult.SystemObject.GetInstance();
        public Tip_ComeBack()
        {
            InitializeComponent();
            lblRoomName.Text = this.clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
            lblSeatNo.Text =SeatManageComm.SeatComm.SeatNoToShortSeatNo(this.clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, this.clientobject.EnterOutLogData.EnterOutlog.SeatNo);
        }
         
    }
}
