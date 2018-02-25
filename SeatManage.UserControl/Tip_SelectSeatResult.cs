using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatManage.EnumType;

namespace SeatManage
{
    public partial class Tip_SelectSeatResult : UserControl
    {
        SeatClient.OperateResult.SystemObject clientobject = SeatClient.OperateResult.SystemObject.GetInstance();

        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="handelresult"></param>
        public Tip_SelectSeatResult(TipType handleresult)
        {
            InitializeComponent(); 
            switch (handleresult)
            {
                case TipType.SelectSeatResult:
                    string roomNo = clientobject.EnterOutLogData.EnterOutlog.ReadingRoomNo;
                    SeatManage.ClassModel.ReadingRoomInfo roomInfo =clientobject.EnterOutLogData.Student.AtReadingRoom;  
                    string shortSeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(roomInfo.Setting.SeatNumAmount, clientobject.EnterOutLogData.EnterOutlog.SeatNo);

                    lblCardNo.Text = clientobject.EnterOutLogData.Student.CardNo;
                    lblReadingRoomName.Text = roomInfo.Name;
                    lblSeatNo.Text = shortSeatNo;
                    break;
                case TipType.BespeatSeatConfirmSuccess:
                    string roomNum = clientobject.EnterOutLogData.Student.BespeakLog[0].ReadingRoomNo;
                    SeatManage.ClassModel.ReadingRoomInfo roomInfo1 = clientobject.EnterOutLogData.Student.AtReadingRoom;
                    string shortSeatNo1 = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(roomInfo1.Setting.SeatNumAmount, clientobject.EnterOutLogData.Student.BespeakLog[0].SeatNo);
                    lblCardNo.Text = clientobject.EnterOutLogData.Student.CardNo;
                    lblReadingRoomName.Text = roomInfo1.Name;
                    lblSeatNo.Text = shortSeatNo1;
                    break;
            }

        }
    }
}
