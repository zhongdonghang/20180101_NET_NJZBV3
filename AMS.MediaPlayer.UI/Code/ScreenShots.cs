using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AMS.MediaPlayer.UI.Code
{
    public class ScreenShots
    {
        public static void SaveWindowContent(Window source, string fileName)
        {
            FrameworkElement elem = source.Content as FrameworkElement;
            RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)elem.ActualWidth, (int)elem.ActualHeight, 96d, 96d, PixelFormats.Default);
            targetBitmap.Render(source);
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(targetBitmap));
            // save file to disk
            using (FileStream fs = File.Open(fileName, FileMode.OpenOrCreate))
            {
                encoder.Save(fs);
            }
        }
    }
}
