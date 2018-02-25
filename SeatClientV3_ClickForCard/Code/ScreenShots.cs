using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SeatClientV3.Code
{
    public class ScreenShots
    {
        public static void SaveWindowContent(FrameworkElement source, string fileName)
        {
            //FrameworkElement elem = source.Content as FrameworkElement;
            RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)source.ActualWidth, (int)source.Height, 96d, 96d, PixelFormats.Default);
            targetBitmap.Render(source);
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(targetBitmap));
            // save file to disk
            using (FileStream fs = File.Open(fileName, FileMode.OpenOrCreate))
            {
                encoder.Save(fs);
            }

            Bitmap image = new Bitmap(1080, 1000);
            Graphics bg = Graphics.FromImage(image);
            Image bgi = Image.FromFile(fileName);

            int w = (int)((double)bgi.Height / 1000 * 1080);

            using (Graphics G = Graphics.FromImage(image))
            {
                G.DrawImage(bgi, new Rectangle(0, 0, 1080, 1000),new Rectangle((bgi.Width-w)/2, 0, w, bgi.Height), GraphicsUnit.Pixel);
            }

            bgi.Dispose();
            //bg.DrawImage(bgi, -(w - 1080) / 2, 0, w, h);
            image.Save(fileName);
        }
    }
}
