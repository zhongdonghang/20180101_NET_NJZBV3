using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace SchoolNoteEditer.Code
{
    public class ToImage
    {
        private SeatManage.ClassModel.SeatLayout _Layout = null;
        public ToImage(SeatManage.ClassModel.SeatLayout seats)
        {
            this._Layout = seats;
        }
        public void Draw(string savePath)
        {
            try
            {
                using (Bitmap bmp = new Bitmap((_Layout.SeatCol + 1) * 25 + 20, (_Layout.SeatRow + 1) * 25 + 20))//绘制画布大小
                {
                    Graphics g = Graphics.FromImage(bmp);
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    //设置高质量插值法
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    //设置高质量,低速度呈现平滑程度
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.Clear(Color.White);
                    foreach (KeyValuePair<string, SeatManage.ClassModel.Seat> seat in _Layout.Seats)
                    {
                        Image imgSeat = global::SchoolNoteEditer.Properties.Resources.Seat;
                        Point[] points = { 
                                         new Point(0,0),
                                         new Point(48, 0), 
                                         new Point(0, 48),
                                     };
                        Matrix matrix = new Matrix(1, 0, 0, 1, (float)seat.Value.PositionX * 25 + 10, (float)seat.Value.PositionY * 25 + 10);
                        matrix.RotateAt(seat.Value.RotationAngle, new PointF(48 / 2, 48 / 2));
                        // 用该矩阵转换points
                        matrix.TransformPoints(points);
                        g.DrawImage(imgSeat, points);
                        g.DrawString(seat.Value.ShortSeatNo, new Font("宋体", 12, FontStyle.Bold), Brushes.White, (points[1].X + points[2].X) / 2 - 15, (points[1].Y + points[2].Y) / 2 - 10);
                    }
                    for (int i = 0; i < _Layout.Notes.Count; i++)
                    {
                        Image imgNote = global::SchoolNoteEditer.Properties.Resources.blank;
                        switch (_Layout.Notes[i].Type)
                        {
                            case SeatManage.EnumType.OrnamentType.AirConditioning:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.AirConditioning;
                                break;
                            case SeatManage.EnumType.OrnamentType.Bookshelf:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.Bookshelf;
                                break;
                            case SeatManage.EnumType.OrnamentType.Door:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.Door;
                                break;
                            case SeatManage.EnumType.OrnamentType.PCTable:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.PCTable;
                                break;
                            case SeatManage.EnumType.OrnamentType.Pillar:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.Pillar;
                                break;
                            case SeatManage.EnumType.OrnamentType.Plant:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.Plant;
                                break;
                            case SeatManage.EnumType.OrnamentType.Roundtable:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.Roundtable;
                                break;
                            case SeatManage.EnumType.OrnamentType.Steps:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.Steps;
                                break;
                            case SeatManage.EnumType.OrnamentType.Table:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.Table;
                                break;
                            case SeatManage.EnumType.OrnamentType.Wall:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.Wall;
                                break;
                            case SeatManage.EnumType.OrnamentType.Window:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.Window;
                                break;
                            case SeatManage.EnumType.OrnamentType.Elevator:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.blank;
                                break;
                            case SeatManage.EnumType.OrnamentType.Escalator:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.blank;
                                break;
                            case SeatManage.EnumType.OrnamentType.Stairway:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.blank;
                                break;
                            case SeatManage.EnumType.OrnamentType.Toilet:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.blank;
                                break;
                            case SeatManage.EnumType.OrnamentType.WaterHouse:
                                imgNote = global::SchoolNoteEditer.Properties.Resources.blank;
                                break;
                        }
                        Point[] points = { 
                                         new Point(0,0),
                                         new Point((int)_Layout.Notes[i].BaseWidth * 25, 0), 
                                         new Point(0, (int)_Layout.Notes[i].BaseHeight * 24),
                                     };
                        Matrix matrix = new Matrix(1, 0, 0, 1, (float)_Layout.Notes[i].PositionX * 25 + 10, (float)_Layout.Notes[i].PositionY * 25 + 10);
                        matrix.RotateAt(_Layout.Notes[i].RotationAngle, new PointF(((float)_Layout.Notes[i].BaseWidth * 25) / 2, ((float)_Layout.Notes[i].BaseHeight * 24) / 2));
                        // 用该矩阵转换points
                        matrix.TransformPoints(points);
                        g.DrawImage(imgNote, points);
                        g.DrawString(_Layout.Notes[i].Remark, new Font("宋体", 12, FontStyle.Bold), Brushes.Black, (points[1].X + points[2].X) / 2 - 15, (points[1].Y + points[2].Y) / 2 - 10);
                    }
                    g.Save();
                    bmp.Save(savePath, ImageFormat.Jpeg);
                }

            }
            catch
            {
                throw;
            }
        }
    }
}
