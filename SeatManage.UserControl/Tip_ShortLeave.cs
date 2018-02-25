using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatManage.ClassModel;
using SeatManage.Bll;
using SeatManage.SeatManageComm; 

namespace SeatManage
{
    public partial class Tip_ShortLeave : UserControl
    {
        SeatClient.OperateResult.SystemObject clientobject = SeatClient.OperateResult.SystemObject.GetInstance();
        DateTime _NowDate = DateTime.Parse("1900-1-1");
        public Tip_ShortLeave()
        {
            InitializeComponent();
            _NowDate = ServiceDateTime.Now;
        }

        private void Tip_ShortLeave_Load(object sender, EventArgs e)
        {
            
            string roomNum = clientobject.EnterOutLogData.EnterOutlog.ReadingRoomNo;
            ReadingRoomInfo room = clientobject.EnterOutLogData.Student.AtReadingRoom;
            if (room.Setting.SeatHoldTime.UsedAdvancedSet)
            {
                for (int i = 0; i < room.Setting.SeatHoldTime.AdvancedSeatHoldTime.Count; i++)
                {
                    if (room.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].Used)
                    {
                        DateTime startDate = DateTime.Parse(_NowDate.ToShortDateString() + " " + room.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.BeginTime);
                        DateTime endDate = DateTime.Parse(_NowDate.ToShortDateString() + " " + room.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.EndTime);
                        if (DateTimeOperate.DateAccord(startDate, endDate, _NowDate))
                        {
                            lblMinutes.Text = room.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength.ToString();
                            return;
                        }
                    } 
                }
            } 
            lblMinutes.Text = room.Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
        }
    }
}
