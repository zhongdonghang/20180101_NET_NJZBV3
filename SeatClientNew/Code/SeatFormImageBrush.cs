using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Drawing;

namespace SeatClientV2.Code
{
    public class SeatFormImageBrush
    {
        private static SeatFormImageBrush MyImageBrushs = null;
        private static object _object = new object();
        private SeatFormImageBrush(Dictionary<string, string> bitmapResource)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            //_SeatFormBackImage.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["SelectSeatForm"]));

            //_ImgFreeSeat.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["btn_free"]));

            //_ImgFreeSeatPW.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["btn_free_p"]));

            //_ImgBusySeat.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["btn_busy"]));

            //_ImgBusySeatPW.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["btn_busy_p"]));

            //_ImgLeaveSeat.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["btn_leave"]));

            //_ImgLeaveSeatPW.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["btn_leave_p"]));

            //_BtnExit.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["Exit"]));

            //_KeyBoard.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["btnKeyboard"]));

            //_ImgStopSeat.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["btn_su"]));

            //_ImgStopSeatPW.ImageSource = BitmapToBitmpImage.ToBitmapImage(string.Format("{0}{1}", path, bitmapResource["btn_pw_su"]));

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



        ImageBrush _KeyBoard = new ImageBrush();
        /// <summary>
        /// 快速选座按钮背景图片
        /// </summary>
        public ImageBrush KeyBoardImage
        {
            get { return _KeyBoard; }
            set { _KeyBoard = value; }
        }

        ImageBrush _SeatFormBackImage = new ImageBrush();
        /// <summary>
        /// 窗体背景图片
        /// </summary>
        public ImageBrush SeatFormBackImage
        {
            get { return _SeatFormBackImage; }
            set { _SeatFormBackImage = value; }
        }
        ImageBrush _ImgStopSeat = new ImageBrush();

        /// <summary>
        /// 停用无电源座位
        /// </summary>
        public ImageBrush ImgStopSeat
        {
            get { return _ImgStopSeat; }
            set { _ImgStopSeat = value; }
        }
        ImageBrush _ImgStopSeatPW = new ImageBrush();

        /// <summary>
        /// 停用有电源
        /// </summary>
        public ImageBrush ImgStopSeatPW
        {
            get { return _ImgStopSeatPW; }
            set { _ImgStopSeatPW = value; }
        }
        ImageBrush _ImgFreeSeat = new ImageBrush();
        /// <summary>
        /// 空闲无电源座位
        /// </summary>
        public ImageBrush ImgFreeSeat
        {
            get { return _ImgFreeSeat; }
            set { _ImgFreeSeat = value; }
        }
        ImageBrush _ImgFreeSeatPW = new ImageBrush();

        /// <summary>
        /// 空闲有电源
        /// </summary>
        public ImageBrush ImgFreeSeatPW
        {
            get { return _ImgFreeSeatPW; }
            set { _ImgFreeSeatPW = value; }
        }
        ImageBrush _ImgBusySeat = new ImageBrush();

        /// <summary>
        /// 使用中的座位
        /// </summary>
        public ImageBrush ImgBusySeat
        {
            get { return _ImgBusySeat; }
            set { _ImgBusySeat = value; }
        }

        ImageBrush _ImgBusySeatPW = new ImageBrush();
        /// <summary>
        /// 使用中有电源
        /// </summary>
        public ImageBrush ImgBusySeatPW
        {
            get { return _ImgBusySeatPW; }
            set { _ImgBusySeatPW = value; }
        }
        ImageBrush _ImgLeaveSeat = new ImageBrush();
        /// <summary>
        /// 暂离的座位
        /// </summary>
        public ImageBrush ImgLeaveSeat
        {
            get { return _ImgLeaveSeat; }
            set { _ImgLeaveSeat = value; }
        }
        ImageBrush _ImgLeaveSeatPW = new ImageBrush();
        /// <summary>
        /// 暂离有电源
        /// </summary>
        public ImageBrush ImgLeaveSeatPW
        {
            get { return _ImgLeaveSeatPW; }
            set { _ImgLeaveSeatPW = value; }
        }

        ImageBrush _BtnExit = new ImageBrush();
        /// <summary>
        /// 退出
        /// </summary>
        public ImageBrush BtnExit
        {
            get { return _BtnExit; }
            set { _BtnExit = value; }
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
