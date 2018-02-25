using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SeatClientV2.Code
{
    public class GetSeatNoteImage
    {
        public ImageBrush SeatImage(SeatManage.EnumType.SeatUsedState seatImageState)
        {
            SeatClientV2.OperateResult.SystemObject clientobject = SeatClientV2.OperateResult.SystemObject.GetInstance();
            SeatFormImageBrush imageBrush = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage);
            ImageBrush ib = new ImageBrush();
            switch (seatImageState)
            {
                case SeatManage.EnumType.SeatUsedState.None:
                case SeatManage.EnumType.SeatUsedState.NoPowerFree:
                    ib.ImageSource = imageBrush.ImgFreeSeat.ImageSource; break;
                case SeatManage.EnumType.SeatUsedState.HasPowerFree:
                    ib.ImageSource = imageBrush.ImgFreeSeatPW.ImageSource; break;
                case SeatManage.EnumType.SeatUsedState.HasPowerLeave:
                    ib.ImageSource = imageBrush.ImgLeaveSeatPW.ImageSource; break;
                case SeatManage.EnumType.SeatUsedState.NoPowerLeave:
                    ib.ImageSource = imageBrush.ImgLeaveSeat.ImageSource; break;
                case SeatManage.EnumType.SeatUsedState.HasPowerUsed:
                    ib.ImageSource = imageBrush.ImgBusySeatPW.ImageSource; break;
                case SeatManage.EnumType.SeatUsedState.NoPowerUsed:
                    ib.ImageSource = imageBrush.ImgBusySeat.ImageSource; break;
                case SeatManage.EnumType.SeatUsedState.HasPowerStop:
                    ib.ImageSource = imageBrush.ImgStopSeatPW.ImageSource; break;
                case SeatManage.EnumType.SeatUsedState.NoPowerStop:
                    ib.ImageSource = imageBrush.ImgStopSeat.ImageSource; break;
                default:
                    ib = imageBrush.ImgFreeSeat; break;
            }
            return ib;
        }
        public ImageBrush NoteImage(SeatManage.EnumType.OrnamentType noteType)
        {
            SeatClientV2.OperateResult.SystemObject clientobject = SeatClientV2.OperateResult.SystemObject.GetInstance();
            SeatFormImageBrush imageBrush = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage);
            ImageBrush imgNote = new ImageBrush();
            switch (noteType)
            {

                case SeatManage.EnumType.OrnamentType.AirConditioning:
                    imgNote.ImageSource = imageBrush.NoteAirConditioning.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Bookshelf:
                    imgNote.ImageSource = imageBrush.NoteBookshelf.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Door:
                    imgNote.ImageSource = imageBrush.NotenoteDoor.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.PCTable:
                    imgNote.ImageSource = imageBrush.NotePCTableg.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Pillar:
                    imgNote.ImageSource = imageBrush.NotePillar.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Plant:
                    imgNote.ImageSource = imageBrush.NotePlant.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Roundtable:
                    imgNote.ImageSource = imageBrush.NoteRoundtable.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Steps:
                    imgNote.ImageSource = imageBrush.NoteSteps.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Table:
                    imgNote.ImageSource = imageBrush.NoteTable.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Wall:
                    imgNote.ImageSource = imageBrush.NoteWall.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Window:
                    imgNote.ImageSource = imageBrush.NoteWindow.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Elevator:
                    imgNote.ImageSource = imageBrush.NoteElevator.ImageSource;
                    break;
                case SeatManage.EnumType.OrnamentType.Stairway:
                    imgNote.ImageSource = imageBrush.NoteStairway.ImageSource;
                    break;

            }
            return imgNote;
        }
    }
}
