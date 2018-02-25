using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SeatClientV2.Code
{
    public class ConvertNoteImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SeatClientV2.OperateResult.SystemObject clientobject = SeatClientV2.OperateResult.SystemObject.GetInstance();
            SeatFormImageBrush imageBrush = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage);
            SeatManage.EnumType.OrnamentType noteType = (SeatManage.EnumType.OrnamentType)value;
            switch (noteType)
            {

                case SeatManage.EnumType.OrnamentType.AirConditioning:
                    return imageBrush.NoteAirConditioning;
                case SeatManage.EnumType.OrnamentType.Bookshelf:
                    return imageBrush.NoteBookshelf;
                case SeatManage.EnumType.OrnamentType.Door:
                    return imageBrush.NotenoteDoor;
                case SeatManage.EnumType.OrnamentType.PCTable:
                    return imageBrush.NotePCTableg;
                case SeatManage.EnumType.OrnamentType.Pillar:
                    return imageBrush.NotePillar;
                case SeatManage.EnumType.OrnamentType.Plant:
                    return imageBrush.NotePlant;
                case SeatManage.EnumType.OrnamentType.Roundtable:
                    return imageBrush.NoteRoundtable;
                case SeatManage.EnumType.OrnamentType.Steps:
                    return imageBrush.NoteSteps;
                case SeatManage.EnumType.OrnamentType.Table:
                    return imageBrush.NoteTable;
                case SeatManage.EnumType.OrnamentType.Wall:
                    return imageBrush.NoteWall;
                case SeatManage.EnumType.OrnamentType.Window:
                    return imageBrush.NoteWindow;
                case SeatManage.EnumType.OrnamentType.Elevator:
                    return imageBrush.NoteElevator;
                case SeatManage.EnumType.OrnamentType.Stairway:
                    return imageBrush.NoteStairway;
                default:
                    return imageBrush.Noteblank;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
