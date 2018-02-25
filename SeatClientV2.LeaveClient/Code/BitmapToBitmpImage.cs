using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace ClientLeaveV2.Code
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
