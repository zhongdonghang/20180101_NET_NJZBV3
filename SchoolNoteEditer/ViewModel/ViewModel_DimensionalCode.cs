using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using SeatManage.SeatManageComm;
using System.Drawing.Imaging;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_DimensionalCode : INotifyPropertyChanged
    {

        private SeatManage.ClassModel.ReadingRoomInfo _RoomInfo = new SeatManage.ClassModel.ReadingRoomInfo();
        /// <summary>
        /// 阅览室
        /// </summary>
        public SeatManage.ClassModel.ReadingRoomInfo RoomInfo
        {
            get { return _RoomInfo; }
            set { _RoomInfo = value; }
        }
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; Changed("ErroeMessage"); }
        }
        private string _SavePath = "";
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath
        {
            get { return _SavePath; }
            set
            {
                _SavePath = value;
                if (_SavePath.Length > 0 && _SavePath[_SavePath.Length - 1] != '\\')
                {
                    _SavePath += '\\';
                }
                Changed("SavePath");
            }
        }
        private int codeconut = 1;
        /// <summary>
        /// 并联数目
        /// </summary>
        public int Codeconut
        {
            get { return codeconut; }
            set { codeconut = value; }
        }
        //private string _Url = "http://yuyue.juneberry.cn/BookSeat/ScanCode.aspx";
        private string _Url = "http://192.168.1.100/QRcodeDecode/SeatInfo.aspx";
        /// <summary>
        /// url
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set
            {
                _Url = value;
                Changed("Url");
            }
        }

        private bool _IsUseTemplate = false;
        /// <summary>
        /// 是否启用模板
        /// </summary>
        public bool IsUseTemplate
        {
            get { return _IsUseTemplate; }
            set { _IsUseTemplate = value; Changed("IsUseTemplate"); }
        }

        private string _NumOrder = "1,2,3,4";
        /// <summary>
        /// 模板顺序
        /// </summary>
        public string NumOrder
        {
            get { return _NumOrder; }
            set { _NumOrder = value; Changed("NumOrder"); }
        }

        private bool _PathSave = false;
        /// <summary>
        /// 部分输出
        /// </summary>
        public bool PathSave
        {
            get { return _PathSave; }
            set { _PathSave = value; Changed("PathSave"); }
        }

        private string _OutputNum = "1-100";
        /// <summary>
        /// 输出序列
        /// </summary>
        public string OutputNum
        {
            get { return _OutputNum; }
            set { _OutputNum = value; Changed("OutputNum"); }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (string.IsNullOrEmpty(_SavePath.Trim()))
            {
                ErrorMessage = "保存地址不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(_Url.Trim()))
            {
                ErrorMessage = "保存地址不能为空！";
                return false;
            }
            try
            {
                string codeURL = _Url + "?param=";
                string AESCode = "seatNum={0}&readingRoomNum={1}";
                string savePath = _SavePath + _RoomInfo.No + "_" + _RoomInfo.Name + "\\";
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                if (!_IsUseTemplate)
                {
                    //默认
                    foreach (KeyValuePair<string, SeatManage.ClassModel.Seat> seat in _RoomInfo.SeatList.Seats)
                    {
                        Bitmap dCode = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seat.Value.SeatNo, _RoomInfo.No)));
                        Graphics dNum = Graphics.FromImage(dCode);
                        dNum.DrawRectangle(new Pen(Color.White, 60), 141, 141, 60, 60);
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        dNum.DrawString(seat.Value.ShortSeatNo, new Font("黑体", 46, FontStyle.Bold), Brushes.Black, 172, 141, sf);
                        dNum.Save();
                        dCode.Save(savePath + seat.Key + ".jpg", ImageFormat.Jpeg);
                    }
                }
                else
                {
                    string[] no = _NumOrder.Split(',');
                    int a = int.Parse(no[0]) - 1;
                    int b = int.Parse(no[1]) - 1;
                    int c = int.Parse(no[2]) - 1;
                    int d = int.Parse(no[3]) - 1;
                    List<SeatManage.ClassModel.Seat> seatList = new List<SeatManage.ClassModel.Seat>();
                    foreach (KeyValuePair<string, SeatManage.ClassModel.Seat> seat in _RoomInfo.SeatList.Seats)
                    {
                        seatList.Add(seat.Value);
                    }
                    if (_PathSave)
                    {
                        string[] num = _OutputNum.Split('-');
                        int s = int.Parse(num[0]) - 1;
                        int e = int.Parse(num[1]) - 1;
                        seatList.RemoveRange(0, s);
                        seatList.RemoveRange(e - s + 1, seatList.Count - (e - s + 1));
                    }


                    //江西师范

                    //seatList.OrderBy(v => v.SeatNo);
                    //StringFormat sf = new StringFormat();
                    //sf.Alignment = StringAlignment.Far;

                    //StringFormat sf1 = new StringFormat();
                    //sf1.Alignment = StringAlignment.Center;
                    //for (int i = 0; i < seatList.Count; i = i + 4)
                    //{
                    //    {
                    //        Bitmap image = new Bitmap(1772, 945);
                    //        Graphics bg = Graphics.FromImage(image);
                    //        Image bgi = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码bg.jpg");
                    //        bg.DrawImage(bgi, 0, 0, 1772, 945);

                    //        Graphics code1 = Graphics.FromImage(image);
                    //        Image c1 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + a].SeatNo, _RoomInfo.No)));
                    //        code1.DrawImage(c1, 177, 223, 530, 530);
                    //        Graphics Num1 = Graphics.FromImage(image);
                    //        Num1.DrawString(seatList[i + a].ShortSeatNo, new Font("方正兰亭粗黑_GBK", 72, FontStyle.Bold), Brushes.White, 439, 825, sf1);

                    //        Graphics code2 = Graphics.FromImage(image);
                    //        Image c2 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + b].SeatNo, _RoomInfo.No)));
                    //        code2.DrawImage(c2, 1062, 223, 530, 530);
                    //        Graphics Num2 = Graphics.FromImage(image);
                    //        Num1.DrawString(seatList[i + b].ShortSeatNo, new Font("方正兰亭粗黑_GBK", 72, FontStyle.Bold), Brushes.White, 1329, 825, sf1);

                    //        Graphics logo1 = Graphics.FromImage(image);
                    //        Image logo = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码logo.jpg");
                    //        logo1.DrawImage(logo, 344, 395, 190, 190);
                    //        Graphics logo2 = Graphics.FromImage(image);
                    //        logo2.DrawImage(logo, 1234, 395, 190, 190);

                    //        Graphics NumRoom = Graphics.FromImage(image);
                    //        NumRoom.DrawString(_RoomInfo.Name, new Font("方正兰亭粗黑_GBK", 46, FontStyle.Bold), Brushes.White, 1650, 64, sf);

                    //        image.Save(savePath + _RoomInfo.Name + "_" + seatList[i + a].ShortSeatNo + "_" + seatList[i + b].ShortSeatNo + ".jpg", ImageFormat.Jpeg);
                    //    }
                    //    {
                    //        Bitmap image = new Bitmap(1772, 945);

                    //        Graphics bg = Graphics.FromImage(image);
                    //        Image bgi = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码bg.jpg");
                    //        bg.DrawImage(bgi, 0, 0, 1772, 945);

                    //        Graphics code1 = Graphics.FromImage(image);
                    //        Image c1 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + c].SeatNo, _RoomInfo.No)));
                    //        code1.DrawImage(c1, 177, 223, 530, 530);
                    //        Graphics Num1 = Graphics.FromImage(image);
                    //        Num1.DrawString(seatList[i + c].ShortSeatNo, new Font("方正兰亭粗黑_GBK", 72, FontStyle.Bold), Brushes.White, 439, 825, sf1);

                    //        Graphics code2 = Graphics.FromImage(image);
                    //        Image c2 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + d].SeatNo, _RoomInfo.No)));
                    //        code2.DrawImage(c2, 1062, 223, 530, 530);
                    //        Graphics Num2 = Graphics.FromImage(image);
                    //        Num1.DrawString(seatList[i + d].ShortSeatNo, new Font("方正兰亭粗黑_GBK", 72, FontStyle.Bold), Brushes.White, 1329, 825, sf1);

                    //        Graphics logo1 = Graphics.FromImage(image);
                    //        Image logo = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码logo.jpg");
                    //        logo1.DrawImage(logo, 344, 395, 190, 190);
                    //        Graphics logo2 = Graphics.FromImage(image);
                    //        logo2.DrawImage(logo, 1234, 395, 190, 190);

                    //        Graphics NumRoom = Graphics.FromImage(image);
                    //        NumRoom.DrawString(_RoomInfo.Name, new Font("方正兰亭粗黑_GBK", 46, FontStyle.Bold), Brushes.White, 1650, 64, sf);

                    //        image.Save(savePath + _RoomInfo.Name + "_" + seatList[i + c].ShortSeatNo + "_" + seatList[i + d].ShortSeatNo + ".jpg", ImageFormat.Jpeg);
                    //    }
                    //}

                    //集美大学

                    seatList.OrderBy(v => v.SeatNo);
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Near;

                    StringFormat sf1 = new StringFormat();
                    sf1.Alignment = StringAlignment.Center;

                    switch (codeconut)
                    {
                        case 1:
                            {
                                for (int i = 0; i < seatList.Count; i++)
                                {
                                    Bitmap image1 = new Bitmap(744, 744);
                                    {
                                        Graphics bg = Graphics.FromImage(image1);
                                        Image bgi = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码bg_JMDX_S.jpg");
                                        bg.DrawImage(bgi, 0, 0, 744, 744);

                                        Graphics code1 = Graphics.FromImage(image1);
                                        Image c1 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i].SeatNo, _RoomInfo.No)));
                                        code1.DrawImage(c1, 185, 187, 375, 375);
                                        Graphics Num1 = Graphics.FromImage(image1);
                                        Num1.DrawString(seatList[i].ShortSeatNo, new Font("方正综艺简体", 100, FontStyle.Regular), Brushes.White, 388, 590, sf1);

                                        Graphics logo1 = Graphics.FromImage(image1);
                                        Image logo = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码logo_JMDX.png");
                                        logo1.DrawImage(logo, 325, 325, 98, 98);

                                        Graphics NumRoom = Graphics.FromImage(image1);
                                        NumRoom.DrawString(_RoomInfo.Name, new Font("汉仪超粗宋简", 36, FontStyle.Regular), Brushes.White, 40, 40, sf);

                                        image1.Save(savePath + _RoomInfo.Name + "_" + seatList[i].ShortSeatNo + ".jpg", ImageFormat.Jpeg);
                                    }
                                }
                            }; break;

                        case 2:
                            {
                                for (int i = 0; i < seatList.Count; i = i + 4)
                                {
                                    //Bitmap image = new Bitmap(1488, 1488);
                                    Bitmap image1 = new Bitmap(1488, 744);
                                    Bitmap image2 = new Bitmap(1488, 744);
                                    {
                                        Graphics bg = Graphics.FromImage(image1);
                                        Image bgi = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码bg_JMDX.jpg");
                                        bg.DrawImage(bgi, 0, 0, 1488, 744);

                                        Graphics code1 = Graphics.FromImage(image1);
                                        Image c1 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + a].SeatNo, _RoomInfo.No)));
                                        code1.DrawImage(c1, 200, 187, 375, 375);
                                        Graphics Num1 = Graphics.FromImage(image1);
                                        Num1.DrawString(seatList[i + a].ShortSeatNo, new Font("方正综艺简体", 100, FontStyle.Regular), Brushes.White, 388, 590, sf1);

                                        Graphics code2 = Graphics.FromImage(image1);
                                        Image c2 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + b].SeatNo, _RoomInfo.No)));
                                        code2.DrawImage(c2, 915, 187, 375, 375);
                                        Graphics Num2 = Graphics.FromImage(image1);
                                        Num1.DrawString(seatList[i + b].ShortSeatNo, new Font("方正综艺简体", 100, FontStyle.Regular), Brushes.White, 1103, 590, sf1);

                                        Graphics logo1 = Graphics.FromImage(image1);
                                        Image logo = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码logo_JMDX.png");
                                        logo1.DrawImage(logo, 340, 325, 98, 98);
                                        Graphics logo2 = Graphics.FromImage(image1);
                                        logo2.DrawImage(logo, 1057, 325, 98, 98);

                                        Graphics NumRoom = Graphics.FromImage(image1);
                                        NumRoom.DrawString(_RoomInfo.Name, new Font("汉仪超粗宋简", 46, FontStyle.Regular), Brushes.White, 70, 36, sf);

                                        image1.Save(savePath + _RoomInfo.Name + "_" + seatList[i + a].ShortSeatNo + "_" + seatList[i + b].ShortSeatNo + ".jpg", ImageFormat.Jpeg);
                                    }
                                    {
                                        Graphics bg = Graphics.FromImage(image2);
                                        Image bgi = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码bg_JMDX.jpg");
                                        bg.DrawImage(bgi, 0, 0, 1488, 744);

                                        Graphics code1 = Graphics.FromImage(image2);
                                        Image c1 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + c].SeatNo, _RoomInfo.No)));
                                        code1.DrawImage(c1, 200, 187, 375, 375);
                                        Graphics Num1 = Graphics.FromImage(image2);
                                        Num1.DrawString(seatList[i + c].ShortSeatNo, new Font("方正综艺简体", 100, FontStyle.Regular), Brushes.White, 388, 590, sf1);

                                        Graphics code2 = Graphics.FromImage(image2);
                                        Image c2 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + d].SeatNo, _RoomInfo.No)));
                                        code2.DrawImage(c2, 915, 187, 375, 375);
                                        Graphics Num2 = Graphics.FromImage(image2);
                                        Num1.DrawString(seatList[i + d].ShortSeatNo, new Font("方正综艺简体", 100, FontStyle.Regular), Brushes.White, 1103, 590, sf1);

                                        Graphics logo1 = Graphics.FromImage(image2);
                                        Image logo = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码logo_JMDX.png");
                                        logo1.DrawImage(logo, 340, 325, 98, 98);
                                        Graphics logo2 = Graphics.FromImage(image2);
                                        logo2.DrawImage(logo, 1057, 325, 98, 98);

                                        Graphics NumRoom = Graphics.FromImage(image2);
                                        NumRoom.DrawString(_RoomInfo.Name, new Font("汉仪超粗宋简", 46, FontStyle.Regular), Brushes.White, 70, 36, sf);

                                        image2.Save(savePath + _RoomInfo.Name + "_" + seatList[i + c].ShortSeatNo + "_" + seatList[i + d].ShortSeatNo + ".jpg", ImageFormat.Jpeg);
                                    }
                                    //Graphics img1 = Graphics.FromImage(image);
                                    //img1.DrawImage(image1, 0, 744, 1488, 744);

                                    //image2.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                    //Graphics img2 = Graphics.FromImage(image);
                                    //img2.DrawImage(image2, 0, 0, 1488, 744);

                                    //image.Save(savePath + _RoomInfo.Name + "_" + seatList[i + a].ShortSeatNo + "_" + seatList[i + b].ShortSeatNo + "_" + seatList[i + c].ShortSeatNo + "_" + seatList[i + d].ShortSeatNo + ".jpg", ImageFormat.Jpeg);
                                }
                            }; break;
                        case 4:
                            {
                                for (int i = 0; i < seatList.Count; i = i + 4)
                                {
                                    Bitmap image = new Bitmap(1488, 1488);
                                    Bitmap image1 = new Bitmap(1488, 744);
                                    Bitmap image2 = new Bitmap(1488, 744);
                                    {
                                        Graphics bg = Graphics.FromImage(image1);
                                        Image bgi = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码bg_JMDX.jpg");
                                        bg.DrawImage(bgi, 0, 0, 1488, 744);

                                        Graphics code1 = Graphics.FromImage(image1);
                                        Image c1 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + a].SeatNo, _RoomInfo.No)));
                                        code1.DrawImage(c1, 200, 187, 375, 375);
                                        Graphics Num1 = Graphics.FromImage(image1);
                                        Num1.DrawString(seatList[i + a].ShortSeatNo, new Font("方正综艺简体", 100, FontStyle.Regular), Brushes.White, 388, 590, sf1);

                                        Graphics code2 = Graphics.FromImage(image1);
                                        Image c2 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + b].SeatNo, _RoomInfo.No)));
                                        code2.DrawImage(c2, 915, 187, 375, 375);
                                        Graphics Num2 = Graphics.FromImage(image1);
                                        Num1.DrawString(seatList[i + b].ShortSeatNo, new Font("方正综艺简体", 100, FontStyle.Regular), Brushes.White, 1103, 590, sf1);

                                        Graphics logo1 = Graphics.FromImage(image1);
                                        Image logo = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码logo_JMDX.png");
                                        logo1.DrawImage(logo, 340, 325, 98, 98);
                                        Graphics logo2 = Graphics.FromImage(image1);
                                        logo2.DrawImage(logo, 1057, 325, 98, 98);

                                        Graphics NumRoom = Graphics.FromImage(image1);
                                        NumRoom.DrawString(_RoomInfo.Name, new Font("汉仪超粗宋简", 46, FontStyle.Regular), Brushes.White, 70, 36, sf);

                                        //image1.Save(savePath + _RoomInfo.Name + "_" + seatList[i + a].ShortSeatNo + "_" + seatList[i + b].ShortSeatNo + ".jpg", ImageFormat.Jpeg);
                                    }
                                    {
                                        Graphics bg = Graphics.FromImage(image2);
                                        Image bgi = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码bg_JMDX.jpg");
                                        bg.DrawImage(bgi, 0, 0, 1488, 744);

                                        Graphics code1 = Graphics.FromImage(image2);
                                        Image c1 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + c].SeatNo, _RoomInfo.No)));
                                        code1.DrawImage(c1, 200, 187, 375, 375);
                                        Graphics Num1 = Graphics.FromImage(image2);
                                        Num1.DrawString(seatList[i + c].ShortSeatNo, new Font("方正综艺简体", 100, FontStyle.Regular), Brushes.White, 388, 590, sf1);

                                        Graphics code2 = Graphics.FromImage(image2);
                                        Image c2 = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[i + d].SeatNo, _RoomInfo.No)));
                                        code2.DrawImage(c2, 915, 187, 375, 375);
                                        Graphics Num2 = Graphics.FromImage(image2);
                                        Num1.DrawString(seatList[i + d].ShortSeatNo, new Font("方正综艺简体", 100, FontStyle.Regular), Brushes.White, 1103, 590, sf1);

                                        Graphics logo1 = Graphics.FromImage(image2);
                                        Image logo = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "二维码logo_JMDX.png");
                                        logo1.DrawImage(logo, 340, 325, 98, 98);
                                        Graphics logo2 = Graphics.FromImage(image2);
                                        logo2.DrawImage(logo, 1057, 325, 98, 98);

                                        Graphics NumRoom = Graphics.FromImage(image2);
                                        NumRoom.DrawString(_RoomInfo.Name, new Font("汉仪超粗宋简", 46, FontStyle.Regular), Brushes.White, 70, 36, sf);

                                        //image2.Save(savePath + _RoomInfo.Name + "_" + seatList[i + c].ShortSeatNo + "_" + seatList[i + d].ShortSeatNo + ".jpg", ImageFormat.Jpeg);
                                    }
                                    Graphics img1 = Graphics.FromImage(image);
                                    img1.DrawImage(image1, 0, 744, 1488, 744);

                                    image2.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                    Graphics img2 = Graphics.FromImage(image);
                                    img2.DrawImage(image2, 0, 0, 1488, 744);

                                    image.Save(savePath + _RoomInfo.Name + "_" + seatList[i + a].ShortSeatNo + "_" + seatList[i + b].ShortSeatNo + "_" + seatList[i + c].ShortSeatNo + "_" + seatList[i + d].ShortSeatNo + ".jpg", ImageFormat.Jpeg);
                                }
                            }; break;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = "导出失败," + ex.Message;
                return false;
            }
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
