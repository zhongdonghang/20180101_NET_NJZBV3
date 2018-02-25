using SeatManage.SeatManageComm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_DimensionalTemplate : INotifyPropertyChanged
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
            set { _ErrorMessage = value; Changed("ErrorMessage"); }
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
        private string _TemplatePath = "";
        /// <summary>
        /// 
        /// </summary>
        public string TemplatePath
        {
            get { return _TemplatePath; }
            set { _TemplatePath = value; }
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
        private string _TemplateName = "请选择模板";

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; Changed("TemplateName"); }
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
        public void LoadTemplate()
        {
            if (string.IsNullOrEmpty(_TemplatePath))
            {
                return;
            }
            if (!Directory.Exists(_TemplatePath))
            {
                ErrorMessage = "模板路径不存在！";
                return;
            }
            string xmlFilePath = _TemplatePath + "\\Template.xml";
            if (!File.Exists(xmlFilePath))
            {
                ErrorMessage = "模板加载失败！";
                return;
            }
            try
            {
                //加载模板
                XmlDocument docXml = new XmlDocument();
                docXml.Load(xmlFilePath);
                SeatManage.ClassModel.DimensionalTemplate template = SeatManage.ClassModel.DimensionalTemplate.Parse(docXml.OuterXml);
                TemplateName = template.Name;
            }
            catch
            {
                ErrorMessage = "模板加载失败！";
            }
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
                        Bitmap dCode = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seat.Value.SeatNo, _RoomInfo.No)), 6, 12, "M");
                        Graphics dNum = Graphics.FromImage(dCode);
                        dNum.DrawRectangle(new Pen(Color.White, 60), 165, 165, 60, 60);
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        dNum.DrawString(seat.Value.ShortSeatNo, new Font("黑体", 46, FontStyle.Bold), Brushes.Black, 196, 165, sf);
                        dNum.Save();
                        dCode.Save(savePath + seat.Key + ".jpg", ImageFormat.Jpeg);
                    }
                    return true;
                }
                if (string.IsNullOrEmpty(_TemplatePath))
                {
                    ErrorMessage = "请选择模板！";
                    return false;
                }
                if (!Directory.Exists(_TemplatePath))
                {
                    ErrorMessage = "模板路径不存在！";
                    return false;
                }
                string xmlFilePath = _TemplatePath + "\\Template.xml";
                if (!File.Exists(xmlFilePath))
                {
                    ErrorMessage = "模板加载失败！";
                    return false;
                }
                //加载模板
                XmlDocument docXml = new XmlDocument();
                docXml.Load(xmlFilePath);
                SeatManage.ClassModel.DimensionalTemplate template = SeatManage.ClassModel.DimensionalTemplate.Parse(docXml.OuterXml);
                int seatCodeCount = template.SeatCodeCount;
                List<SeatManage.ClassModel.DimensionalElement> seatCodeList = template.ElementList.FindAll(u => u.Type == SeatManage.ClassModel.DimensionalElementTye.SeatCode);
                List<SeatManage.ClassModel.DimensionalElement> seatNoList = template.ElementList.FindAll(u => u.Type == SeatManage.ClassModel.DimensionalElementTye.SeatNo);
                List<SeatManage.ClassModel.DimensionalElement> roomNameList = template.ElementList.FindAll(u => u.Type == SeatManage.ClassModel.DimensionalElementTye.ReadingRoomName);
                List<SeatManage.ClassModel.DimensionalElement> bgList = template.ElementList.FindAll(u => u.Type == SeatManage.ClassModel.DimensionalElementTye.Background);
                List<SeatManage.ClassModel.DimensionalElement> imageList = template.ElementList.FindAll(u => u.Type == SeatManage.ClassModel.DimensionalElementTye.Image);
                List<SeatManage.ClassModel.DimensionalElement> textList = template.ElementList.FindAll(u => u.Type == SeatManage.ClassModel.DimensionalElementTye.Text);

                //整理排序号
                string[] no = _NumOrder.Split(',');
                if (no.Length % seatCodeCount != 0)
                {
                    ErrorMessage = "排序号必须为模板联数的倍数！";
                    return false;
                }
                List<int> seatOrderNo = new List<int>();
                for (int i = 0; i < no.Length; i++)
                {
                    seatOrderNo.Add(int.Parse(no[i]) - 1);
                }

                List<SeatManage.ClassModel.Seat> seatList = new List<SeatManage.ClassModel.Seat>();
                foreach (KeyValuePair<string, SeatManage.ClassModel.Seat> seat in _RoomInfo.SeatList.Seats)
                {
                    seatList.Add(seat.Value);
                }
                //处理部分导出
                if (_PathSave)
                {
                    string[] num = _OutputNum.Split('-');
                    int s = int.Parse(num[0]) - 1;
                    int e = int.Parse(num[1]) - 1;
                    if (s < 1)
                    {
                        ErrorMessage = "起始编号必须大于零！";
                        return false;
                    }
                    if (e > seatList.Count)
                    {
                        ErrorMessage = "结束编号不能大于座位总数！";
                        return false;
                    }
                    seatList.RemoveRange(0, s);
                    seatList.RemoveRange(e - s + 1, seatList.Count - (e - s + 1));
                }
                Bitmap image = null;
                List<Image> seatQRList = new List<Image>();
                for (int i = 0; i < seatList.Count; i++)
                {
                    int seatOrder = (i / seatOrderNo.Count) * seatOrderNo.Count + seatOrderNo[i % seatOrderNo.Count];
                    int seatNo = i % seatCodeCount;
                    //创建模板
                    if (seatNo == 0)
                    {
                        image = new Bitmap((int)template.Width, (int)template.Height);
                        Graphics bg = Graphics.FromImage(image);
                        //加载背景图片
                        for (int j = 0; j < bgList.Count; j++)
                        {

                            Bitmap imgB = new Bitmap((int)bgList[j].Width, (int)bgList[j].Height);
                            Graphics ibg = Graphics.FromImage(imgB);
                            Image bgi = Bitmap.FromFile(_TemplatePath + "\\" + bgList[j].ImageFile);
                            if (bgi == null)
                            {
                                ErrorMessage = "背景图片加载失败！";
                                return false;
                            }
                            ibg.DrawImage(bgi, 0, 0, (int)bgList[j].Width, (int)bgList[j].Height);
                            ibg.Dispose();
                            switch ((int)bgList[j].Angle)
                            {
                                case 90:
                                    imgB.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                    break;
                                case 180:
                                    imgB.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                    break;
                                case 270:
                                    imgB.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    break;
                            }
                            bg.DrawImage(imgB, (int)bgList[j].PosintionX, (int)bgList[j].PosintionY, (int)bgList[j].Width, (int)bgList[j].Height);

                        }
                        bg.Dispose();
                    }

                    //座位编号二维码
                    Image seatQR = QRCode.GetDimensionalCode(codeURL + AESAlgorithm.UrlEncode(string.Format(AESCode, seatList[seatOrder].SeatNo, _RoomInfo.No)), 6, 12, "H");
                    switch ((int)seatCodeList[seatNo].Angle)
                    {
                        case 90:
                            seatQR.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 180:
                            seatQR.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case 270:
                            seatQR.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                    }
                    Graphics seatg = Graphics.FromImage(image);
                    seatg.DrawImage(seatQR, (int)seatCodeList[seatNo].PosintionX, (int)seatCodeList[seatNo].PosintionY, (int)seatCodeList[seatNo].Width, (int)seatCodeList[seatNo].Height);
                    //座位编号
                    seatg.DrawImage(DrawText(seatNoList[seatNo], seatList[seatOrder].ShortSeatNo), (int)seatNoList[seatNo].PosintionX, (int)seatNoList[seatNo].PosintionY, (int)seatNoList[seatNo].Width, (int)seatNoList[seatNo].Height);
                    seatg.Dispose();
                    if (seatNo == seatCodeCount - 1 || i + 1 == seatList.Count)
                    {
                        Graphics gi = Graphics.FromImage(image);
                        for (int j = 0; j < roomNameList.Count; j++)
                        {
                            gi.DrawImage(DrawText(roomNameList[j], _RoomInfo.Name), (int)roomNameList[j].PosintionX, (int)roomNameList[j].PosintionY, (int)roomNameList[j].Width, (int)roomNameList[j].Height);
                        }
                        for (int j = 0; j < textList.Count; j++)
                        {
                            gi.DrawImage(DrawText(textList[j], textList[j].Text), (int)textList[j].PosintionX, (int)textList[j].PosintionY, (int)textList[j].Width, (int)textList[j].Height);
                        }

                        for (int j = 0; j < imageList.Count; j++)
                        {
                            Image img = Bitmap.FromFile(_TemplatePath + "\\" + imageList[j].ImageFile);
                            if (img == null)
                            {
                                ErrorMessage = "图片加载失败！";
                                return false;
                            }
                            switch ((int)imageList[j].Angle)
                            {
                                case 90:
                                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                    break;
                                case 180:
                                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                    break;
                                case 270:
                                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    break;
                            }
                            gi.DrawImage(img, (int)imageList[j].PosintionX, (int)imageList[j].PosintionY, (int)imageList[j].Width, (int)imageList[j].Height);
                        }
                        gi.Dispose();
                        string imageName = "";
                        for (int k = 0; k < seatCodeCount; k++)
                        {
                            int ii = i - k;
                            int order = (ii / seatOrderNo.Count) * seatOrderNo.Count + seatOrderNo[ii % seatOrderNo.Count];
                            imageName = "_" + seatList[order].ShortSeatNo + imageName;
                        }
                        image.Save(savePath + _RoomInfo.Name + imageName + ".jpg", ImageFormat.Jpeg);
                        image.Dispose();
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
        /// <summary>
        /// 添加文字
        /// </summary>
        /// <param name="textElement"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private Bitmap DrawText(SeatManage.ClassModel.DimensionalElement textElement, string text)
        {
            //System.Drawing.Brush brushConverter = new System.Windows.Media.BrushConverter();
            Brush fontcolor = new SolidBrush(Color.FromArgb(Convert.ToInt32(textElement.FontColor.Substring(1), 16)));
            int fontx = 0;
            int fonty = 0;
            StringFormat fontsf = new StringFormat();
            switch (textElement.Alignment)
            {
                case SeatManage.ClassModel.ElementTextAlignment.Center:
                    fontsf.Alignment = StringAlignment.Center;
                    fontx = (int)(textElement.Width / 2);
                    break;
                case SeatManage.ClassModel.ElementTextAlignment.Left:
                    fontsf.Alignment = StringAlignment.Near;
                    fontx = 0;
                    break;
                case SeatManage.ClassModel.ElementTextAlignment.Right:
                    fontsf.Alignment = StringAlignment.Far;
                    fontx = (int)textElement.Width;
                    break;
            }
            Bitmap fontImage = new Bitmap((int)textElement.Width, (int)textElement.Height);
            Graphics seatg = Graphics.FromImage(fontImage);
            seatg.DrawString(text, new Font("方正综艺简体", textElement.FontSize, FontStyle.Regular), fontcolor, fontx, fonty, fontsf);
            switch ((int)textElement.Angle)
            {
                case 90:
                    fontImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 180:
                    fontImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 270:
                    fontImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            //fontImage.Save(_SavePath + "\\" + text + ".jpg", ImageFormat.Jpeg);
            return fontImage;
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
