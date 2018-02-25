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
        private string _Url = "http://yuyue.juneberry.cn/BookSeat/ScanCode.aspx";
        /// <summary>
        /// url
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; Changed("Url"); }
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
