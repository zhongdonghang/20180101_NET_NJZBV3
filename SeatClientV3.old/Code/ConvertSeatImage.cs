using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SeatClientV3.Code
{
    /// <summary>
    /// wpf绑定数据转换器，把座位的状态转换相应的背景图片。
    /// </summary>
   public class ConvertSeatImage:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SeatClientV3.OperateResult.SystemObject clientobject = SeatClientV3.OperateResult.SystemObject.GetInstance();
            SeatFormImageBrush imageBrush = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage);
            SeatManage.EnumType.SeatUsedState seatUsedState = (SeatManage.EnumType.SeatUsedState)value;
            switch (seatUsedState)
            { 
                case SeatManage.EnumType.SeatUsedState.None:
                case SeatManage.EnumType.SeatUsedState.NoPowerFree:
                    return imageBrush.ImgFreeSeat;  
                case SeatManage.EnumType.SeatUsedState.HasPowerFree:
                    return imageBrush.ImgFreeSeatPW;
                case SeatManage.EnumType.SeatUsedState.HasPowerLeave:
                    return imageBrush.ImgLeaveSeatPW;
                case  SeatManage.EnumType.SeatUsedState.NoPowerLeave:
                    return imageBrush.ImgLeaveSeat;
                case SeatManage.EnumType.SeatUsedState.HasPowerUsed:
                    return imageBrush.ImgBusySeatPW;
                case SeatManage.EnumType.SeatUsedState.NoPowerUsed:
                    return imageBrush.ImgBusySeat;
                case SeatManage.EnumType.SeatUsedState.HasPowerStop:
                    return imageBrush.ImgStopSeatPW;
                case SeatManage.EnumType.SeatUsedState.NoPowerStop:
                    return imageBrush.ImgStopSeat;
                default:
                    return imageBrush.ImgFreeSeat;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
