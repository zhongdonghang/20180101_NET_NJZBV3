using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SeatClientV3.Code
{
    public class SeatFormImageBrush
    {
        private static SeatFormImageBrush MyImageBrushs = null;
        private static object _object = new object();
        private SeatFormImageBrush(Dictionary<string, string> bitmapResource)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            _NoteAirConditioning.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_AirConditioning"]));

            _Noteblank.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_blank"]));

            _NoteBookshelf.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Bookshelf"]));

            _NoteElevator.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Elevator"]));

            _NotenoteDoor.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Door"]));

            _NotePCTableg.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_PCTable"]));

            _NotePillar.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Pillar"]));

            _NotePlant.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Plant"]));

            _NoteRoundtable.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Roundtable"]));

            _NoteStairway.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Stairway"]));

            _NoteSteps.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Steps"]));

            _NoteTable.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Table"]));

            _NoteWall.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Wall"]));

            _NoteWindow.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["note_Window"]));

            _ImgBook.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["ImgBook"]));

            _ImgPower.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["ImgPower"]));

            _ImgReader.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["ImgReader"]));

            _ImgSeat.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["ImgSeat"]));

            _ImgShortLeave.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["ImgShortLeave"]));

            _ImgStopUse.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["ImgStopUse"]));

            _ImgSeatUse.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["ImgSeatUse"]));
        }

        public static SeatFormImageBrush GetInstance(Dictionary<string, string> bitmapResource)
        {
            if (MyImageBrushs == null)
            {
                lock (_object)
                {
                    if (MyImageBrushs == null)
                    {
                        return MyImageBrushs = new SeatFormImageBrush(bitmapResource);
                    }
                }
            }
            return MyImageBrushs;
        }

        ImageBrush _NoteAirConditioning = new ImageBrush();
        /// <summary>
        /// 空调
        /// </summary>
        public ImageBrush NoteAirConditioning
        {
            get { return _NoteAirConditioning; }
            set { _NoteAirConditioning = value; }
        }
        ImageBrush _Noteblank = new ImageBrush();
        /// <summary>
        /// 空白
        /// </summary>
        public ImageBrush Noteblank
        {
            get { return _Noteblank; }
            set { _Noteblank = value; }
        }
        ImageBrush _NoteBookshelf = new ImageBrush();
        /// <summary>
        /// 书架
        /// </summary>
        public ImageBrush NoteBookshelf
        {
            get { return _NoteBookshelf; }
            set { _NoteBookshelf = value; }
        }
        ImageBrush _NotenoteDoor = new ImageBrush();
        /// <summary>
        /// 门
        /// </summary>
        public ImageBrush NotenoteDoor
        {
            get { return _NotenoteDoor; }
            set { _NotenoteDoor = value; }
        }
        ImageBrush _NoteElevator = new ImageBrush();
        /// <summary>
        /// 电梯
        /// </summary>
        public ImageBrush NoteElevator
        {
            get { return _NoteElevator; }
            set { _NoteElevator = value; }
        }
        ImageBrush _NotePCTableg = new ImageBrush();
        /// <summary>
        /// 电脑桌
        /// </summary>
        public ImageBrush NotePCTableg
        {
            get { return _NotePCTableg; }
            set { _NotePCTableg = value; }
        }
        ImageBrush _NotePillar = new ImageBrush();
        /// <summary>
        /// 柱子
        /// </summary>
        public ImageBrush NotePillar
        {
            get { return _NotePillar; }
            set { _NotePillar = value; }
        }
        ImageBrush _NotePlant = new ImageBrush();
        /// <summary>
        /// 盆栽
        /// </summary>
        public ImageBrush NotePlant
        {
            get { return _NotePlant; }
            set { _NotePlant = value; }
        }
        ImageBrush _NoteRoundtable = new ImageBrush();
        /// <summary>
        /// 圆桌
        /// </summary>
        public ImageBrush NoteRoundtable
        {
            get { return _NoteRoundtable; }
            set { _NoteRoundtable = value; }
        }
        ImageBrush _NoteStairway = new ImageBrush();
        /// <summary>
        /// 楼梯
        /// </summary>
        public ImageBrush NoteStairway
        {
            get { return _NoteStairway; }
            set { _NoteStairway = value; }
        }
        ImageBrush _NoteSteps = new ImageBrush();
        /// <summary>
        /// 台阶
        /// </summary>
        public ImageBrush NoteSteps
        {
            get { return _NoteSteps; }
            set { _NoteSteps = value; }
        }
        ImageBrush _NoteTable = new ImageBrush();
        /// <summary>
        /// 桌子
        /// </summary>
        public ImageBrush NoteTable
        {
            get { return _NoteTable; }
            set { _NoteTable = value; }
        }
        ImageBrush _NoteWall = new ImageBrush();
        /// <summary>
        /// 墙
        /// </summary>
        public ImageBrush NoteWall
        {
            get { return _NoteWall; }
            set { _NoteWall = value; }
        }
        ImageBrush _NoteWindow = new ImageBrush();
        /// <summary>
        /// 窗户
        /// </summary>
        public ImageBrush NoteWindow
        {
            get { return _NoteWindow; }
            set { _NoteWindow = value; }
        }

        ImageBrush _ImgBook = new ImageBrush();
        /// <summary>
        /// 预约按钮
        /// </summary>
        public ImageBrush ImgBook
        {
            get { return _ImgBook; }
            set { _ImgBook = value; }
        }
        ImageBrush _ImgPower = new ImageBrush();
        /// <summary>
        /// 电源
        /// </summary>
        public ImageBrush ImgPower
        {
            get { return _ImgPower; }
            set { _ImgPower = value; }
        }
        ImageBrush _ImgReader = new ImageBrush();
        /// <summary>
        /// 读者
        /// </summary>
        public ImageBrush ImgReader
        {
            get { return _ImgReader; }
            set { _ImgReader = value; }
        }
        ImageBrush _ImgSeat = new ImageBrush();
        /// <summary>
        /// 座位
        /// </summary>
        public ImageBrush ImgSeat
        {
            get { return _ImgSeat; }
            set { _ImgSeat = value; }
        }
        ImageBrush _ImgSeatUse = new ImageBrush();
        /// <summary>
        /// 使用中的座位
        /// </summary>
        public ImageBrush ImgSeatUse
        {
            get { return _ImgSeatUse; }
            set { _ImgSeatUse = value; }
        }
        ImageBrush _ImgShortLeave = new ImageBrush();
        /// <summary>
        /// 暂离
        /// </summary>
        public ImageBrush ImgShortLeave
        {
            get { return _ImgShortLeave; }
            set { _ImgShortLeave = value; }
        }
        ImageBrush _ImgStopUse = new ImageBrush();
        /// <summary>
        /// 停止使用
        /// </summary>
        public ImageBrush ImgStopUse
        {
            get { return _ImgStopUse; }
            set { _ImgStopUse = value; }
        }

    }
}
