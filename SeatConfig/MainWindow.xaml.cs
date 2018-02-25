using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SeatConfig.ViewModel;
using SeatConfig.Code;
using Microsoft.Win32;

namespace SeatConfig
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        /// <summary>
        /// 座位流水号编号
        /// </summary>
        string serialNum = "001";
        /// <summary>
        /// 备注缓存
        /// </summary>
        string temptxt = "";
        /// <summary>
        /// 行位置
        /// </summary>
        int GridRowLocation;
        /// <summary>
        /// 列位置
        /// </summary>
        int GridColLocation;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DrowGrid();
            RemoveSeat();
            e.Handled = true;
        }

        private void DrowGrid()
        {
            //应用布局：
            //1.删除在布局之外的座位或者备注
            //2.删除
            seatGrid.RowDefinitions.Clear();
            seatGrid.ColumnDefinitions.Clear();
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                int countRow = int.Parse(textBox1.Text);
                seatGrid.Height = countRow * 48;
                for (int i = 0; i < countRow; i++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(48);
                    seatGrid.RowDefinitions.Add(row);
                }
            }
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                int countCol = int.Parse(textBox2.Text);
                seatGrid.Width = countCol * 48;
                for (int i = 0; i < countCol; i++)
                {
                    ColumnDefinition col = new ColumnDefinition();
                    col.Width = new GridLength(48);
                    seatGrid.ColumnDefinitions.Add(col);
                }
            }
        }

        /// <summary>
        /// 移除座位
        /// </summary>
        void RemoveSeat()
        {
            List<UIElement> isRemoveElement = new List<UIElement>();
            for (int i = 0; i < seatGrid.Children.Count; i++)
            {
                UIElement element = seatGrid.Children[i];
                int seatColPosition = Grid.GetColumn(element) + 1;
                int seatRowPosition = Grid.GetRow(element) + 1;
                if (seatColPosition > seatGrid.ColumnDefinitions.Count || seatRowPosition > seatGrid.RowDefinitions.Count)
                {
                    isRemoveElement.Add(element);
                }
            }
            foreach (UIElement element in isRemoveElement)
            {
                seatGrid.Children.Remove(element);
            }

        }


        private void seatGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            object j = e.OriginalSource;
            if (j is ScrollViewer)
            {
                Point p = e.GetPosition(seatGrid);
                GridRowLocation = (int)p.Y / 48;
                GridColLocation = (int)p.X / 48;
                if (rdbSeat.IsChecked == true)
                {
                    UserCtl.CtlSeat seat = new UserCtl.CtlSeat();
                    seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                    seat.Seat.PositionX = GridColLocation;
                    seat.Seat.ShortSeatNo = serialNum;
                    seat.Seat.PositionY = GridRowLocation;
                    //RotateTransform rotateTransform2 = new RotateTransform(90);
                    //rotateTransform2.CenterX = 96;
                    //rotateTransform2.CenterY = 96;
                    //seat.RenderTransform = rotateTransform2;
                    //seat.RenderTransformOrigin = new Point(0.5, 0.5);
                    this.seatGrid.Children.Add(seat);
                    Grid.SetColumn(seat, GridColLocation);
                    Grid.SetRow(seat, GridRowLocation);
                    serialNum = SeatNumAdd(serialNum);
                }
                else if (rdbNote.IsChecked == true)
                {
                    UserCtl.CtlNode node = new UserCtl.CtlNode();
                    node.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                    node.Note.PositionX = GridColLocation;
                    node.Note.PositionY = GridRowLocation;
                    node.Note.Remark = temptxt;
                    this.seatGrid.Children.Add(node);
                    Grid.SetColumn(node, GridColLocation);
                    Grid.SetRow(node, GridRowLocation);
                }
            }

            e.Handled = true;
        }

        void txtNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temptxt = (sender as TextBox).Text;
            }
            catch
            {

            }
        }

        void txtSeatNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                serialNum = SeatNumAdd((sender as TextBox).Text);
                (sender as TextBox).Text = (sender as TextBox).Text.ToUpper();
            }
            catch
            {

            }
        }
        string SeatNumAdd(string noText)
        {
            string newNo = "001";
            try
            {
                bool isadd = false;
                Char[] chr = noText.ToUpper().ToCharArray();
                if (chr[2] == 'Z')
                {
                    isadd = true;
                    chr[2] = 'A';
                }
                else if (chr[2] == '9')
                {
                    isadd = true;
                    chr[2] = '0';
                }
                else
                {
                    chr[2] = Convert.ToChar(chr[2] + 1);
                }

                if (isadd)
                {
                    if (chr[1] == 'Z')
                    {
                        chr[1] = 'A';
                    }
                    else if ((chr[1] == '9'))
                    {
                        chr[1] = '0';
                    }
                    else
                    {
                        chr[1] = Convert.ToChar(chr[1] + 1);
                        isadd = false;
                    }
                }

                if (isadd)
                {
                    if (chr[0] == 'Z')
                    {
                        chr[0] = 'A';
                    }
                    else if ((chr[0] == '9'))
                    {
                        chr[0] = '0';
                    }
                    else
                    {
                        chr[0] = Convert.ToChar(chr[0] + 1);
                    }
                }
                newNo = new string(chr);
            }
            catch
            {

            }
            return newNo;

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SeatConfig.Code.SeatCanvas seatCanvas = new Code.SeatCanvas();
            seatCanvas.RoomNo = textBox3.Text;
            if (string.IsNullOrEmpty(seatCanvas.RoomNo))
            {
                MessageBox.Show("阅览室编号不能为空");
                return;
            }
            seatCanvas.SeatCol = textBox2.Text;
            seatCanvas.SeatRow = textBox1.Text;
            viewModel.Room.SeatList.Seats.Clear();
            viewModel.Room.SeatList.Notes.Clear();
            viewModel.Room.SeatList.RoomNo = seatCanvas.RoomNo;
            foreach (UIElement element in seatGrid.Children)
            {
                if (element is SeatConfig.UserCtl.CtlSeat)
                {
                    UserCtl.CtlSeat seat = element as UserCtl.CtlSeat;
                    seat.Seat.SeatNo = seatCanvas.RoomNo + seat.Seat.ShortSeatNo;
                    seat.Seat.ReadingRoomNum = seatCanvas.RoomNo;
                    viewModel.Room.SeatList.Seats.Add(seat.Seat.SeatNo, seat.Seat);
                }
                else if (element is SeatConfig.UserCtl.CtlNode)
                {
                    UserCtl.CtlNode note = element as UserCtl.CtlNode;
                    viewModel.Room.SeatList.Notes.Add(note.Note);
                    // seatCanvas.notes.Add(note.Note);
                }
            }
            try
            {
                viewModel.UpdateSeat();
                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("座位保存失败" + ex.Message);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.GetReadingRooms();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox ObjSender = sender as ComboBox;
            if (ObjSender.SelectedItem is SeatManage.ClassModel.ReadingRoomInfo)
            {
                SeatManage.ClassModel.ReadingRoomInfo room = ObjSender.SelectedItem as SeatManage.ClassModel.ReadingRoomInfo;
                if (room.No == "")
                {
                    viewModel.Room = null;
                }
                else
                {
                    viewModel.Room = room;
                    SeatLayout();
                }
            }
        }

        private void SeatLayout()
        {
            //SeatConfig.Code.SeatCanvas seatCanvas = new Code.SeatCanvas(viewModel.Room.SeatList.Seats, viewModel.Room.SeatList.Notes);
            //DrowGrid();//绘制网格
            //seatGrid.Children.RemoveRange(0, seatGrid.Children.Count);
            ////布局座位
            //for (int i = 0; i < seatCanvas.Seats.Count; i++)
            //{
            //    UserCtl.CtlSeat seat = new UserCtl.CtlSeat();
            //    seat.Seat = seatCanvas.Seats[i];
            //    seat.DataContext = seat.Seat;
            //    this.seatGrid.Children.Add(seat);
            //    Grid.SetColumn(seat, seat.Seat.PositionX);
            //    Grid.SetRow(seat, seat.Seat.PositionY);
            //}
            ////布局备注
            //for (int i = 0; i < seatCanvas.notes.Count; i++)
            //{
            //    UserCtl.CtlNode node = new UserCtl.CtlNode();
            //    node.Note = seatCanvas.notes[i];
            //    node.DataContext = node.Note;
            //    this.seatGrid.Children.Add(node);
            //    Grid.SetColumn(node, node.Note.PositionX);
            //    Grid.SetRow(node, node.Note.PositionY);
            //}
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            DrawSeatLayout drawSeat = new DrawSeatLayout(viewModel.Room.SeatList);
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Title = "保存为";
            openFileDialog.Filter = "jpg文件|*.jpg|bmp文件|*.bmp|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "jpg";
            bool result = (bool)openFileDialog.ShowDialog();
            if (!result)
            {
                return;
            }
            string fileName = openFileDialog.FileName;
            try
            {
                drawSeat.Draw(fileName);
                MessageBox.Show("导出成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败:" + ex.Message);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// 方向绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Position_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox ObjSender = sender as ComboBox;
            if (ObjSender.SelectedItem is RoomPosition)
            {
                RoomPosition position = ObjSender.SelectedItem as RoomPosition;
                viewModel.Position.PositionName = position.PositionName;
                viewModel.Position.PositionValue = position.PositionValue;
            }
        }

    }
}
