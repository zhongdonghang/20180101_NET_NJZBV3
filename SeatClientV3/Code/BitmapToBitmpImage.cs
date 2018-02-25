using System;
using System.Windows.Media.Imaging;

namespace SeatClientV3.Code
{
    public class BitmapToBitmpImage
    {
         public static BitmapImage ToBitmapImage(string bitmapPath) 
         { 
             BitmapImage bitmapImage = new BitmapImage(new Uri(bitmapPath,UriKind.RelativeOrAbsolute)); 
             return bitmapImage; 
         }

    }
}
