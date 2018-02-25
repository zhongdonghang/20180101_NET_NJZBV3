using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
namespace SeatConfig.Code
{
    public class DrawSeatLayout
    {
        private SeatManage.ClassModel.SeatLayout _Layout = null;
        public DrawSeatLayout(SeatManage.ClassModel.SeatLayout seats)
        {
            this._Layout = seats;
        }
        public void Draw(string savePath)
        {
            //using (Bitmap bmp = new Bitmap((_Layout.SeatCol + 1) * 48 + 20, (_Layout.SeatRow + 1) * 48 + 20))//绘制画布大小
            //{
            //    Image img = global::SeatConfig.Properties.Resources.Seat;//.GetThumbnailImage(48,48,;

            //    Graphics g = Graphics.FromImage(bmp);

            //    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            //    //设置高质量插值法
            //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //    //设置高质量,低速度呈现平滑程度
            //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;



            //    g.Clear(Color.White);
            //    foreach (KeyValuePair<string, SeatManage.ClassModel.Seat> seat in _Layout.Seats)
            //    {
            //        Point p = new Point(seat.Value.PositionX * 48 + 10, seat.Value.PositionY * 48 + 10);//计算当前座位的位置
            //        PointF pSeatNo = new PointF(p.X, p.Y + 14);
            //        //  g.DrawImage(img, new Rectangle(p.X, p.Y, 48, 48), 0, 0, 48, 48, GraphicsUnit.Pixel);
            //        g.DrawImage(img, new Rectangle(p.X, p.Y, 48, 48), new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);
            //        g.DrawString(seat.Value.SeatNo, new Font("宋体", 12, FontStyle.Bold), Brushes.Black, pSeatNo);

            //        //  break;
            //    }
            //    for (int i = 0; i < _Layout.Notes.Count; i++)
            //    {
            //        Point p = new Point(_Layout.Notes[i].PositionX * 48 + 10, _Layout.Notes[i].PositionY * 48 + 10);
            //        PointF pNote = new PointF(p.X, p.Y);
            //        g.DrawString(_Layout.Notes[i].Remark, new Font("宋体", 12, FontStyle.Bold), Brushes.Black, pNote);
            //    }
            //    g.Save();
            //    bmp.Save(savePath, ImageFormat.Jpeg);
            //}
        }
    }
}
