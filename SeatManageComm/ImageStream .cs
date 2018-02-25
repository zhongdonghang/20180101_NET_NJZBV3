/*******************************
 * 作者：王随
 * 创建日期：2013-5-30 14：23
 * 用于图片文件和二进制流之间的转换
 * 
 * **********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace SeatManage.SeatManageComm
{
    /// <summary>
    /// 图片和二进制流之间的操作
    /// </summary>
    public class ImageStream
    {
        /// <summary>
        /// Bitmap类型转换为byte数组
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToStream(Bitmap image)
        {
            if (image != null)
            {
                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = ms.GetBuffer();  //byte[]   bytes=   ms.ToArray(); 这两句都可以，至于区别么，下面有解释
                ms.Close();
                return bytes;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 二进制流转换为图片
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Bitmap StreamToImage(byte[] bytes)
        {
            if (bytes != null)
            {
                byte[] bytelist = bytes;
                MemoryStream ms1 = new MemoryStream(bytelist);
                Bitmap bm = (Bitmap)Image.FromStream(ms1);
                ms1.Close();
                return bm;
            }
            else
            {
                return null;
            }
        }

        
    }
}
