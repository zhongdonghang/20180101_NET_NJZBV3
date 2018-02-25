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
using Microsoft.Win32;

namespace SchoolNoteEditer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SeatWindow : Window
    {
        public SeatWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            BindingData();
        }
        ViewModel.ViewModel_MainWindow viewModel = new ViewModel.ViewModel_MainWindow();
        /// <summary>
        /// 编辑阅览室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addroom_Click(object sender, RoutedEventArgs e)
        {
            FunPage.ReadingRoomInfoEdit rrie = new FunPage.ReadingRoomInfoEdit();
            rrie.BindingData();
            rrie.ShowDialog();
            BindingData();
        }
        public void BindingData()
        {
            viewModel.GetData();
            BrushLine();
            roomcb.SelectedIndex = 0;
            poscb.SelectedIndex = 0;

        }

        private DrawingBrush _gridBrush;
        private void BrushLine()
        {
            if (_gridBrush == null)
            {
                _gridBrush = new DrawingBrush(new GeometryDrawing(
                    new SolidColorBrush(Colors.White),
                         new Pen(new SolidColorBrush(Colors.Gray), 0.5),
                             new RectangleGeometry(new Rect(0, 0, 20, 20))));
                _gridBrush.Stretch = Stretch.None;
                _gridBrush.TileMode = TileMode.Tile;
                _gridBrush.Viewport = new Rect(0.0, 0.0, 20, 20);
                _gridBrush.ViewportUnits = BrushMappingMode.Absolute;
                seatCanvas.Background = _gridBrush;
            }
        }

        private void roomcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roomcb.SelectedIndex > 0)
            {
                viewModel.Room = roomcb.SelectedItem as ViewModel.ViewModel_ReadingRoom;
                seatCanvas.Children.Clear();
                seatCanvas.Width = viewModel.Room.ReadingRoomModel.SeatList.SeatCol * 20;
                seatCanvas.Height = viewModel.Room.ReadingRoomModel.SeatList.SeatRow * 20;
                if (seatCanvas.Width == 0 && seatCanvas.Height == 0)
                {
                    seatCanvas.Width = 600;
                    seatCanvas.Height = 600;
                }
                foreach (KeyValuePair<string, SeatManage.ClassModel.Seat> seat in viewModel.Room.ReadingRoomModel.SeatList.Seats)
                {
                    UC.UC_Seat uc_Seat = new UC.UC_Seat();
                    uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                    uc_Seat.Seat = seat.Value;
                    uc_Seat.DataContext = uc_Seat.Seat;
                    uc_Seat.IsPower = uc_Seat.IsPower;
                    uc_Seat.IsSuspended = uc_Seat.IsSuspended;
                    uc_Seat.Height = 40;
                    uc_Seat.Width = 40;
                    RotateTransform rotateTransform = (RotateTransform)uc_Seat.bgimg.RenderTransform;
                    rotateTransform.Angle += seat.Value.RotationAngle;
                    uc_Seat.bgimg.RenderTransform = rotateTransform;
                    Canvas.SetLeft(uc_Seat, seat.Value.PositionX * 20);
                    Canvas.SetTop(uc_Seat, seat.Value.PositionY * 20);
                    seatCanvas.Children.Add(uc_Seat);
                }
                foreach (SeatManage.ClassModel.Note note in viewModel.Room.ReadingRoomModel.SeatList.Notes)
                {
                    UC.UC_Node uc_Note = new UC.UC_Node();
                    uc_Note.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                    uc_Note.Note = note;
                    uc_Note.DataContext = uc_Note.Note;
                    uc_Note.Width = note.BaseWidth * 20;
                    uc_Note.Height = note.BaseHeight * 20;
                    RotateTransform rotateTransform = (RotateTransform)uc_Note.bgimg.RenderTransform;
                    rotateTransform.Angle += note.RotationAngle;
                    uc_Note.bgimg.RenderTransform = rotateTransform;
                    Canvas.SetLeft(uc_Note, note.PositionX * 20);
                    Canvas.SetTop(uc_Note, note.PositionY * 20);
                    seatCanvas.Children.Add(uc_Note);
                }
                foreach (ViewModel.RoomPosition p in poscb.Items)
                {
                    if (p.PositionName == viewModel.Room.ReadingRoomModel.SeatList.Position)
                    {
                        poscb.SelectedItem = p;
                        break;
                    }
                }
            }
        }
        UserControl focusElement = null;
        List<UserControl> focusElementList = new List<UserControl>();
        List<UserControl> lastElementList = new List<UserControl>();

        public UserControl FocusElement
        {
            get { return focusElement; }
            set { focusElement = value; }
        }
        string UCtype = "";
        string mouseDownType = "";
        double mouseSX = -1;
        double mouseSY = -1;
        Border br;
        private void seatCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement targetElement = Mouse.Captured as FrameworkElement;
            if (e.LeftButton == MouseButtonState.Pressed && targetElement != null)
            {
                switch (mouseDownType)
                {
                    case "Move":
                        {
                            var pCanvas = e.GetPosition(seatCanvas);
                            double newX = pCanvas.X;
                            if (newX < 0)
                            {
                                newX = 0;
                            }
                            if (newX + targetElement.Width > seatCanvas.Width)
                            {
                                newX = seatCanvas.Width - targetElement.Width;
                            }
                            if (newX % 20 > 0)
                            {
                                newX -= newX % 20;
                            }
                            double newY = pCanvas.Y;
                            if (newY < 0)
                            {
                                newY = 0;
                            }
                            if (newY + targetElement.Height > seatCanvas.Height)
                            {
                                newY = seatCanvas.Height - targetElement.Height;
                            }
                            if (newY % 20 > 0)
                            {
                                newY -= newY % 20;
                            }
                            double moveX = Canvas.GetLeft(targetElement) - newX;
                            double moveY = Canvas.GetTop(targetElement) - newY;
                            if (focusElementList.Count > 0)
                            {
                                foreach (UserControl u in focusElementList)
                                {
                                    Canvas.SetLeft(u, Canvas.GetLeft(u) - moveX);
                                    Canvas.SetTop(u, Canvas.GetTop(u) - moveY);
                                }
                            }
                            else
                            {
                                Canvas.SetLeft(targetElement, newX);
                                Canvas.SetTop(targetElement, newY);
                            }

                        }
                        break;
                    case "Deform":
                        {
                            var pCanvas = e.GetPosition(seatCanvas);
                            double newWidth = pCanvas.X - Canvas.GetLeft(targetElement);
                            if (newWidth < 20)
                            {
                                newWidth = 20;
                            }
                            if (pCanvas.X > seatCanvas.Width)
                            {
                                newWidth = seatCanvas.Width - Canvas.GetLeft(targetElement);
                            }
                            if (newWidth % 20 > 0)
                            {
                                newWidth = (newWidth - newWidth % 20) + 20;
                            }

                            double newHeight = pCanvas.Y - Canvas.GetTop(targetElement);
                            if (newHeight < 20)
                            {
                                newHeight = 20;
                            }
                            if (pCanvas.Y > seatCanvas.Height)
                            {
                                newHeight = seatCanvas.Height - Canvas.GetTop(targetElement);
                            }
                            if (newHeight % 20 > 0)
                            {
                                newHeight = (newHeight - newHeight % 20) + 20;
                            }
                            targetElement.Width = newWidth;
                            targetElement.Height = newHeight;
                        }
                        break;
                    case "Select":
                        {
                            if (!((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
                            {
                                foreach (UserControl u in focusElementList)
                                {
                                    u.BorderThickness = new Thickness(0);
                                }

                                focusElementList.Clear();
                            }
                            var pCanvas = e.GetPosition(seatCanvas);
                            if (mouseSX == -1 || mouseSY == -1)
                            {
                                mouseSX = pCanvas.X;
                                mouseSY = pCanvas.Y;
                                br = new Border();
                                br.BorderThickness = new Thickness(1);
                                br.BorderBrush = new SolidColorBrush(Colors.Blue);
                                Canvas.SetLeft(br, mouseSX);
                                Canvas.SetTop(br, mouseSY);
                                br.Name = "r_Select";
                                seatCanvas.Children.Add(br);
                            }
                            foreach (FrameworkElement fe in seatCanvas.Children)
                            {
                                //左上
                                if (mouseSX >= pCanvas.X && mouseSY >= pCanvas.Y)
                                {
                                    Canvas.SetLeft(br, pCanvas.X);
                                    Canvas.SetTop(br, pCanvas.Y);
                                    br.Width = Math.Abs(pCanvas.X - mouseSX);
                                    br.Height = Math.Abs(pCanvas.Y - mouseSY);
                                    if (Canvas.GetLeft(fe) >= pCanvas.X && Canvas.GetTop(fe) >= pCanvas.Y && (Canvas.GetLeft(fe) + fe.Width) <= mouseSX && (Canvas.GetTop(fe) + fe.Height) <= mouseSY)
                                    {
                                        GetFocusElementList(fe);
                                    }
                                }
                                //左下
                                else if (mouseSX >= pCanvas.X && mouseSY <= pCanvas.Y)
                                {
                                    Canvas.SetLeft(br, pCanvas.X);
                                    Canvas.SetTop(br, mouseSY);
                                    br.Width = Math.Abs(pCanvas.X - mouseSX);
                                    br.Height = Math.Abs(pCanvas.Y - mouseSY);
                                    if (Canvas.GetLeft(fe) >= pCanvas.X && Canvas.GetTop(fe) >= mouseSY && (Canvas.GetLeft(fe) + fe.Width) <= mouseSX && (Canvas.GetTop(fe) + fe.Height) <= pCanvas.Y)
                                    {
                                        GetFocusElementList(fe);
                                    }
                                }
                                //右上
                                else if (mouseSX <= pCanvas.X && mouseSY >= pCanvas.Y)
                                {
                                    Canvas.SetLeft(br, mouseSX);
                                    Canvas.SetTop(br, pCanvas.Y);
                                    br.Width = Math.Abs(pCanvas.X - mouseSX);
                                    br.Height = Math.Abs(pCanvas.Y - mouseSY);
                                    if (Canvas.GetLeft(fe) >= mouseSX && Canvas.GetTop(fe) >= pCanvas.Y && (Canvas.GetLeft(fe) + fe.Width) <= pCanvas.X && (Canvas.GetTop(fe) + fe.Height) <= mouseSY)
                                    {
                                        GetFocusElementList(fe);
                                    }
                                }
                                //右下
                                else if (mouseSX <= pCanvas.X && mouseSY <= pCanvas.Y)
                                {
                                    Canvas.SetLeft(br, mouseSX);
                                    Canvas.SetTop(br, mouseSY);
                                    br.Width = Math.Abs(pCanvas.X - mouseSX);
                                    br.Height = Math.Abs(pCanvas.Y - mouseSY);
                                    if (Canvas.GetLeft(fe) >= mouseSX && Canvas.GetTop(fe) >= mouseSY && (Canvas.GetLeft(fe) + fe.Width) <= pCanvas.X && (Canvas.GetTop(fe) + fe.Height) <= pCanvas.Y)
                                    {
                                        GetFocusElementList(fe);
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }


        private void seatCanvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            mouseDownType = "";
            mouseSX = -1;
            mouseSY = -1;
            seatCanvas.Children.Remove(br);
            br = null;

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            RadioButton rb = e.Source as RadioButton;
            lastElementList.Clear();

            if (rb.Name == "mouseRB")
            {
                UCtype = "";
            }
            else
            {
                UCtype = rb.Name;
            }

        }
        //左击进行拖拽操作
        private void seatCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                FrameworkElement targetElement = e.Source as FrameworkElement;
                if (targetElement != null && (targetElement is UC.UC_Seat || targetElement is UC.UC_Node))
                {
                    Point p = Mouse.GetPosition(targetElement);
                    if (targetElement.Width - p.X > 10 && p.Y > 10)
                    {
                        targetElement.CaptureMouse();
                        mouseDownType = "Move";
                    }
                    else if (targetElement is UC.UC_Node && targetElement.Width - p.X <= 10 && targetElement.Height - p.Y <= 10)
                    {
                        targetElement.CaptureMouse();
                        mouseDownType = "Deform";
                    }
                    if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                    {
                        GetFocusElementList(targetElement);
                    }
                    else
                    {
                        GetFocusElement(targetElement);
                    }
                }
                //if (targetElement != null && (targetElement is UC.UC_Seat || targetElement is UC.UC_Node))
                //{

                //}
                else if (targetElement is Canvas)
                {
                    targetElement.CaptureMouse();
                    mouseDownType = "Select";
                    stopuserbtn.Visibility = System.Windows.Visibility.Collapsed;
                    startuserbtn.Visibility = System.Windows.Visibility.Collapsed;
                    powerbtn.Visibility = System.Windows.Visibility.Collapsed;
                    nopowerbtn.Visibility = System.Windows.Visibility.Collapsed;
                    if (!((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
                    {
                        if (focusElement != null)
                        {
                            focusElement.BorderThickness = new Thickness(0);
                            focusElement = null;
                        }
                        foreach (UserControl u in focusElementList)
                        {
                            u.BorderThickness = new Thickness(0);
                        }
                        focusElementList.Clear();
                    }
                }
            }
        }

        private void GetFocusElementList(FrameworkElement targetElement)
        {
            if (targetElement.Name == "r_Select")
            {
                return;
            }
            stopuserbtn.Visibility = System.Windows.Visibility.Collapsed;
            startuserbtn.Visibility = System.Windows.Visibility.Collapsed;
            powerbtn.Visibility = System.Windows.Visibility.Collapsed;
            nopowerbtn.Visibility = System.Windows.Visibility.Collapsed;
            if (focusElement != null)
            {
                focusElementList.Add(focusElement);
                focusElement = null;
            }
            UserControl uc = targetElement as UserControl;
            if (!focusElementList.Contains(uc))
            {
                uc.BorderThickness = new Thickness(1);
                uc.BorderBrush = new SolidColorBrush(Colors.Blue);
                focusElementList.Add(uc);
            }
            //else
            //{
            //    uc.BorderThickness = new Thickness(0);
            //    focusElementList.Remove(uc);
            //}
        }

        private void GetFocusElement(FrameworkElement targetElement)
        {
            if (targetElement.Name == "r_Select")
            {
                return;
            }
            foreach (UserControl u in focusElementList)
            {
                u.BorderThickness = new Thickness(0);
            }
            focusElementList.Clear();
            if (focusElement != null)
            {
                focusElement.BorderThickness = new Thickness(0);
                stopuserbtn.Visibility = System.Windows.Visibility.Collapsed;
                startuserbtn.Visibility = System.Windows.Visibility.Collapsed;
                powerbtn.Visibility = System.Windows.Visibility.Collapsed;
                nopowerbtn.Visibility = System.Windows.Visibility.Collapsed;
            }
            focusElement = targetElement as UserControl;
            focusElement.BorderThickness = new Thickness(1);
            focusElement.BorderBrush = new SolidColorBrush(Colors.Blue);
            if (focusElement is UC.UC_Seat)
            {
                if ((focusElement as UC.UC_Seat).IsSuspended)
                {
                    startuserbtn.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    stopuserbtn.Visibility = System.Windows.Visibility.Visible;
                }
                if ((focusElement as UC.UC_Seat).IsPower)
                {
                    nopowerbtn.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    powerbtn.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        //SeatManage.ClassModel.Seat tempSeat = new SeatManage.ClassModel.Seat();
        //Dictionary<SeatManage.EnumType.OrnamentType, SeatManage.ClassModel.Note> tempNote = new Dictionary<SeatManage.EnumType.OrnamentType, SeatManage.ClassModel.Note>();
        //右击添加控件
        private void seatCanvas_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement targetElement = e.Source as FrameworkElement;
            if (targetElement != null && targetElement is Canvas && UCtype != "")
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control)
                {
                    ClaerFocue();
                }
                Point p = Mouse.GetPosition(targetElement);
                if (p.X % 20 > 0)
                {
                    p.X -= p.X % 20;
                }
                if (p.Y % 20 > 0)
                {
                    p.Y -= p.Y % 20;
                }
                if (lastElementList.Count > 0)
                {

                    double baseX = Canvas.GetLeft(lastElementList[0]);
                    double baseY = Canvas.GetTop(lastElementList[0]);
                    int lc = lastElementList.Count;
                    foreach (UserControl u in lastElementList)
                    {
                        if (baseX > Canvas.GetLeft(u))
                        {
                            baseX = Canvas.GetLeft(u);
                        }
                        if (baseY > Canvas.GetTop(u))
                        {
                            baseY = Canvas.GetTop(u);
                        }
                    }
                    for (int i = 0; i < lc; i++)
                    {
                        if (lastElementList[i] is UC.UC_Seat)
                        {
                            UC.UC_Seat old_Seat = lastElementList[i] as UC.UC_Seat;
                            UC.UC_Seat uc_Seat = new UC.UC_Seat();
                            uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                            uc_Seat.Seat.ShortSeatNo = serialNum;
                            serialNum = SeatNumAdd(serialNum);
                            RotateTransform rotateTransform_s = (RotateTransform)uc_Seat.bgimg.RenderTransform;
                            rotateTransform_s.Angle = ((RotateTransform)old_Seat.bgimg.RenderTransform).Angle;
                            uc_Seat.bgimg.RenderTransform = rotateTransform_s;
                            uc_Seat.Height = 40;
                            uc_Seat.Width = 40;
                            Canvas.SetLeft(uc_Seat, p.X + (Canvas.GetLeft(lastElementList[i]) - baseX));
                            Canvas.SetTop(uc_Seat, p.Y + (Canvas.GetTop(lastElementList[i]) - baseY));
                            seatCanvas.Children.Add(uc_Seat);
                            lastElementList.Add(uc_Seat);
                            GetFocusElementList(uc_Seat);
                        }
                        else
                        {
                            UC.UC_Node old_Note = lastElementList[i] as UC.UC_Node;
                            UC.UC_Node uc_Note = new UC.UC_Node();
                            uc_Note.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                            uc_Note.Note = new SeatManage.ClassModel.Note() { Type = old_Note.Note.Type };
                            RotateTransform rotateTransform = (RotateTransform)uc_Note.bgimg.RenderTransform;
                            rotateTransform.Angle = ((RotateTransform)old_Note.bgimg.RenderTransform).Angle;
                            uc_Note.bgimg.RenderTransform = rotateTransform;
                            uc_Note.Height = old_Note.Height;
                            uc_Note.Width = old_Note.Width;
                            Canvas.SetLeft(uc_Note, p.X + (Canvas.GetLeft(lastElementList[i]) - baseX));
                            Canvas.SetTop(uc_Note, p.Y + (Canvas.GetTop(lastElementList[i]) - baseY));
                            seatCanvas.Children.Add(uc_Note);
                            lastElementList.Add(uc_Note);
                            GetFocusElementList(uc_Note);
                        }
                    }
                    for (int i = 0; i < lc; i++)
                    {
                        lastElementList.RemoveAt(0);
                    }

                }
                else
                {
                    if (UCtype.Split('_')[0] == "MIX")
                    {
                        string mixtype = UCtype.Split('_')[1];
                        string mixnum = UCtype.Split('_')[2];
                        int seatCount = 0;
                        if (mixtype == "PC")
                        {
                            switch (mixnum)
                            {
                                case "1":
                                    {
                                        UC.UC_Node uc_Note = new UC.UC_Node();
                                        uc_Note.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                                        uc_Note.Note = new SeatManage.ClassModel.Note() { Type = SeatManage.EnumType.OrnamentType.PCTable };
                                        RotateTransform rotateTransform = (RotateTransform)uc_Note.bgimg.RenderTransform;
                                        rotateTransform.Angle = 90 * (int.Parse(mixnum) - 1);
                                        uc_Note.bgimg.RenderTransform = rotateTransform;
                                        uc_Note.Height = 40;
                                        uc_Note.Width = 80;
                                        Canvas.SetLeft(uc_Note, p.X);
                                        Canvas.SetTop(uc_Note, p.Y);
                                        seatCanvas.Children.Add(uc_Note);
                                        lastElementList.Add(uc_Note);
                                        GetFocusElementList(uc_Note);

                                        UC.UC_Seat uc_Seat = new UC.UC_Seat();
                                        uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                                        uc_Seat.Seat.ShortSeatNo = serialNum;
                                        RotateTransform rotateTransform_s = (RotateTransform)uc_Seat.bgimg.RenderTransform;
                                        rotateTransform_s.Angle = 90 * (int.Parse(mixnum) - 1);
                                        uc_Seat.bgimg.RenderTransform = rotateTransform_s;
                                        uc_Seat.Height = 40;
                                        uc_Seat.Width = 40;
                                        Canvas.SetLeft(uc_Seat, p.X + 20);
                                        Canvas.SetTop(uc_Seat, p.Y + 40);
                                        seatCanvas.Children.Add(uc_Seat);
                                        lastElementList.Add(uc_Seat);
                                        GetFocusElementList(uc_Seat);
                                    } break;
                                case "2":
                                    {
                                        UC.UC_Node uc_Note = new UC.UC_Node();
                                        uc_Note.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                                        uc_Note.Note = new SeatManage.ClassModel.Note() { Type = SeatManage.EnumType.OrnamentType.PCTable };
                                        RotateTransform rotateTransform = (RotateTransform)uc_Note.bgimg.RenderTransform;
                                        rotateTransform.Angle = 90 * (int.Parse(mixnum) - 1);
                                        uc_Note.bgimg.RenderTransform = rotateTransform;
                                        uc_Note.Height = 40;
                                        uc_Note.Width = 80;
                                        Canvas.SetLeft(uc_Note, p.X + 20);
                                        Canvas.SetTop(uc_Note, p.Y + 20);
                                        seatCanvas.Children.Add(uc_Note);
                                        lastElementList.Add(uc_Note);
                                        GetFocusElementList(uc_Note);

                                        UC.UC_Seat uc_Seat = new UC.UC_Seat();
                                        uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                                        uc_Seat.Seat.ShortSeatNo = serialNum;
                                        RotateTransform rotateTransform_s = (RotateTransform)uc_Seat.bgimg.RenderTransform;
                                        rotateTransform_s.Angle = 90 * (int.Parse(mixnum) - 1);
                                        uc_Seat.bgimg.RenderTransform = rotateTransform_s;
                                        uc_Seat.Height = 40;
                                        uc_Seat.Width = 40;
                                        Canvas.SetLeft(uc_Seat, p.X);
                                        Canvas.SetTop(uc_Seat, p.Y + 20);
                                        seatCanvas.Children.Add(uc_Seat);
                                        lastElementList.Add(uc_Seat);
                                        GetFocusElementList(uc_Seat);
                                    } break;
                                case "3":
                                    {
                                        UC.UC_Node uc_Note = new UC.UC_Node();
                                        uc_Note.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                                        uc_Note.Note = new SeatManage.ClassModel.Note() { Type = SeatManage.EnumType.OrnamentType.PCTable };
                                        RotateTransform rotateTransform = (RotateTransform)uc_Note.bgimg.RenderTransform;
                                        rotateTransform.Angle = 90 * (int.Parse(mixnum) - 1);
                                        uc_Note.bgimg.RenderTransform = rotateTransform;
                                        uc_Note.Height = 40;
                                        uc_Note.Width = 80;
                                        Canvas.SetLeft(uc_Note, p.X);
                                        Canvas.SetTop(uc_Note, p.Y + 40);
                                        seatCanvas.Children.Add(uc_Note);
                                        lastElementList.Add(uc_Note);
                                        GetFocusElementList(uc_Note);

                                        UC.UC_Seat uc_Seat = new UC.UC_Seat();
                                        uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                                        uc_Seat.Seat.ShortSeatNo = serialNum;
                                        RotateTransform rotateTransform_s = (RotateTransform)uc_Seat.bgimg.RenderTransform;
                                        rotateTransform_s.Angle = 90 * (int.Parse(mixnum) - 1);
                                        uc_Seat.bgimg.RenderTransform = rotateTransform_s;
                                        uc_Seat.Height = 40;
                                        uc_Seat.Width = 40;
                                        Canvas.SetLeft(uc_Seat, p.X + 20);
                                        Canvas.SetTop(uc_Seat, p.Y);
                                        seatCanvas.Children.Add(uc_Seat);
                                        lastElementList.Add(uc_Seat);
                                        GetFocusElementList(uc_Seat);
                                    } break;
                                case "4":
                                    {
                                        UC.UC_Node uc_Note = new UC.UC_Node();
                                        uc_Note.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                                        uc_Note.Note = new SeatManage.ClassModel.Note() { Type = SeatManage.EnumType.OrnamentType.PCTable };
                                        RotateTransform rotateTransform = (RotateTransform)uc_Note.bgimg.RenderTransform;
                                        rotateTransform.Angle = 90 * (int.Parse(mixnum) - 1);
                                        uc_Note.bgimg.RenderTransform = rotateTransform;
                                        uc_Note.Height = 40;
                                        uc_Note.Width = 80;
                                        Canvas.SetLeft(uc_Note, p.X - 20);
                                        Canvas.SetTop(uc_Note, p.Y + 20);
                                        seatCanvas.Children.Add(uc_Note);
                                        lastElementList.Add(uc_Note);
                                        GetFocusElementList(uc_Note);

                                        UC.UC_Seat uc_Seat = new UC.UC_Seat();
                                        uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                                        uc_Seat.Seat.ShortSeatNo = serialNum;
                                        RotateTransform rotateTransform_s = (RotateTransform)uc_Seat.bgimg.RenderTransform;
                                        rotateTransform_s.Angle = 90 * (int.Parse(mixnum) - 1);
                                        uc_Seat.bgimg.RenderTransform = rotateTransform_s;
                                        uc_Seat.Height = 40;
                                        uc_Seat.Width = 40;
                                        Canvas.SetLeft(uc_Seat, p.X + 40);
                                        Canvas.SetTop(uc_Seat, p.Y + 20);
                                        seatCanvas.Children.Add(uc_Seat);
                                        lastElementList.Add(uc_Seat);
                                        GetFocusElementList(uc_Seat);
                                    } break;
                            }

                        }
                        else
                        {
                            switch (mixtype)
                            {
                                case "TS": seatCount = 1; break;
                                case "FS": seatCount = 2; break;
                                case "SS": seatCount = 3; break;
                                case "ES": seatCount = 4; break;
                            }

                            if (mixnum == "1")
                            {
                                UC.UC_Node uc_Note = new UC.UC_Node();
                                uc_Note.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                                uc_Note.Note = new SeatManage.ClassModel.Note() { Type = SeatManage.EnumType.OrnamentType.Table };
                                uc_Note.Height = 40;
                                uc_Note.Width = 40 * seatCount;
                                Canvas.SetLeft(uc_Note, p.X);
                                Canvas.SetTop(uc_Note, p.Y + 40);
                                seatCanvas.Children.Add(uc_Note);
                                lastElementList.Add(uc_Note);
                                GetFocusElementList(uc_Note);

                                for (int i = 0; i < seatCount; i++)
                                {
                                    UC.UC_Seat uc_Seat = new UC.UC_Seat();
                                    uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                                    uc_Seat.Seat.ShortSeatNo = serialNum;
                                    serialNum = SeatNumAdd(serialNum);
                                    RotateTransform rotateTransform = (RotateTransform)uc_Seat.bgimg.RenderTransform;
                                    rotateTransform.Angle = 180;
                                    uc_Seat.bgimg.RenderTransform = rotateTransform;
                                    uc_Seat.Height = 40;
                                    uc_Seat.Width = 40;
                                    Canvas.SetLeft(uc_Seat, p.X + 40 * i);
                                    Canvas.SetTop(uc_Seat, p.Y);
                                    seatCanvas.Children.Add(uc_Seat);
                                    lastElementList.Add(uc_Seat);
                                    GetFocusElementList(uc_Seat);
                                }
                                for (int i = 0; i < seatCount; i++)
                                {
                                    UC.UC_Seat uc_Seat = new UC.UC_Seat();
                                    uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                                    uc_Seat.Seat.ShortSeatNo = serialNum;
                                    serialNum = SeatNumAdd(serialNum);
                                    uc_Seat.Height = 40;
                                    uc_Seat.Width = 40;
                                    Canvas.SetLeft(uc_Seat, p.X + 40 * i);
                                    Canvas.SetTop(uc_Seat, p.Y + 80);
                                    seatCanvas.Children.Add(uc_Seat);
                                    lastElementList.Add(uc_Seat);
                                    GetFocusElementList(uc_Seat);
                                }
                            }
                            else
                            {
                                UC.UC_Node uc_Note = new UC.UC_Node();
                                uc_Note.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                                uc_Note.Note = new SeatManage.ClassModel.Note() { Type = SeatManage.EnumType.OrnamentType.Table };
                                uc_Note.Height = 40 * seatCount;
                                uc_Note.Width = 40;
                                Canvas.SetLeft(uc_Note, p.X + 40);
                                Canvas.SetTop(uc_Note, p.Y);
                                seatCanvas.Children.Add(uc_Note);
                                lastElementList.Add(uc_Note);
                                GetFocusElementList(uc_Note);

                                for (int i = 0; i < seatCount; i++)
                                {
                                    UC.UC_Seat uc_Seat = new UC.UC_Seat();
                                    uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                                    uc_Seat.Seat.ShortSeatNo = serialNum;
                                    serialNum = SeatNumAdd(serialNum);
                                    RotateTransform rotateTransform = (RotateTransform)uc_Seat.bgimg.RenderTransform;
                                    rotateTransform.Angle = 90;
                                    uc_Seat.bgimg.RenderTransform = rotateTransform;
                                    uc_Seat.Height = 40;
                                    uc_Seat.Width = 40;
                                    Canvas.SetLeft(uc_Seat, p.X);
                                    Canvas.SetTop(uc_Seat, p.Y + 40 * i);
                                    seatCanvas.Children.Add(uc_Seat);
                                    lastElementList.Add(uc_Seat);
                                    GetFocusElementList(uc_Seat);
                                }
                                for (int i = 0; i < seatCount; i++)
                                {
                                    UC.UC_Seat uc_Seat = new UC.UC_Seat();
                                    uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                                    uc_Seat.Seat.ShortSeatNo = serialNum;
                                    serialNum = SeatNumAdd(serialNum);
                                    RotateTransform rotateTransform = (RotateTransform)uc_Seat.bgimg.RenderTransform;
                                    rotateTransform.Angle = 270;
                                    uc_Seat.bgimg.RenderTransform = rotateTransform;
                                    uc_Seat.Height = 40;
                                    uc_Seat.Width = 40;
                                    Canvas.SetLeft(uc_Seat, p.X + 80);
                                    Canvas.SetTop(uc_Seat, p.Y + 40 * i);
                                    seatCanvas.Children.Add(uc_Seat);
                                    lastElementList.Add(uc_Seat);
                                    GetFocusElementList(uc_Seat);
                                }
                            }
                        }
                    }
                    else if (UCtype == "Seat")
                    {
                        UC.UC_Seat uc_Seat = new UC.UC_Seat();
                        uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                        uc_Seat.Seat.ShortSeatNo = serialNum;
                        uc_Seat.Height = 40;
                        uc_Seat.Width = 40;
                        Canvas.SetLeft(uc_Seat, p.X);
                        Canvas.SetTop(uc_Seat, p.Y);
                        seatCanvas.Children.Add(uc_Seat);
                        lastElementList.Add(uc_Seat);
                        GetFocusElement(uc_Seat);

                    }
                    else
                    {
                        UC.UC_Node uc_Note = new UC.UC_Node();
                        uc_Note.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                        uc_Note.Note = new SeatManage.ClassModel.Note() { Type = (SeatManage.EnumType.OrnamentType)Enum.Parse(typeof(SeatManage.EnumType.OrnamentType), UCtype) };
                        switch (uc_Note.Note.Type)
                        {
                            case SeatManage.EnumType.OrnamentType.PCTable:
                            case SeatManage.EnumType.OrnamentType.Table:
                            case SeatManage.EnumType.OrnamentType.Door:
                                uc_Note.Height = 40;
                                uc_Note.Width = 80;
                                break;
                            case SeatManage.EnumType.OrnamentType.Roundtable:
                            case SeatManage.EnumType.OrnamentType.AirConditioning:
                            case SeatManage.EnumType.OrnamentType.Elevator:
                            case SeatManage.EnumType.OrnamentType.Stairway:
                                uc_Note.Height = 80;
                                uc_Note.Width = 80;
                                break;
                            case SeatManage.EnumType.OrnamentType.Wall:
                                uc_Note.Height = 20;
                                uc_Note.Width = 20;
                                break;
                            case SeatManage.EnumType.OrnamentType.Bookshelf:
                                uc_Note.Height = 160;
                                uc_Note.Width = 40;
                                break;
                            case SeatManage.EnumType.OrnamentType.Window:
                                uc_Note.Height = 20;
                                uc_Note.Width = 40;
                                break;
                            case SeatManage.EnumType.OrnamentType.Steps:
                                uc_Note.Height = 80;
                                uc_Note.Width = 20;
                                break;
                            default:
                                uc_Note.Height = 40;
                                uc_Note.Width = 40;
                                break;
                        }
                        Canvas.SetLeft(uc_Note, p.X);
                        Canvas.SetTop(uc_Note, p.Y);
                        seatCanvas.Children.Add(uc_Note);
                        lastElementList.Add(uc_Note);
                        GetFocusElement(uc_Note);
                    }
                }
            }
            else if (targetElement != null && targetElement is UC.UC_Seat)
            {
                UC.UC_Seat uc_Seat = targetElement as UC.UC_Seat;
                uc_Seat.Seat.ShortSeatNo = serialNum;
                uc_Seat.txtSeatNo.Text = serialNum;
                GetFocusElement(uc_Seat);
            }
        }

        private void ClaerFocue()
        {
            stopuserbtn.Visibility = System.Windows.Visibility.Collapsed;
            startuserbtn.Visibility = System.Windows.Visibility.Collapsed;
            powerbtn.Visibility = System.Windows.Visibility.Collapsed;
            nopowerbtn.Visibility = System.Windows.Visibility.Collapsed;
            if (focusElement != null)
            {
                focusElement.BorderThickness = new Thickness(0);
                focusElement = null;
            }
            foreach (UserControl u in focusElementList)
            {
                u.BorderThickness = new Thickness(0);
            }
            focusElementList.Clear();
        }
        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="Rate"></param>
        /// <param name="CirPoint"></param>
        /// <param name="MovePoint"></param>
        /// <returns></returns>
        private Point GetNewPoint(double Rate, Point CirPoint, Point MovePoint)
        {
            double Rage2 = Rate / 180 * Math.PI;
            //B点绕A点转R度得到C点坐标，flag: 顺时针1，反时针-1：B是转的点，A是圆心
            //C.X=（B.X-A.X)*COS(R*flag)-(B.Y-A.Y)*Sin(R*flag);
            //C.Y= (B.Y-A.Y)*COS(R*flag)+(B.X-A.X)*sin(R*flag);
            //转的点坐标-圆心坐标
            //圆心坐标+计算坐标=新位置的坐标
            double newx = (double)((MovePoint.X - CirPoint.X) * Math.Cos(Rage2) - (MovePoint.Y - CirPoint.Y) * Math.Sin(Rage2));
            double newy = (double)((MovePoint.Y - CirPoint.Y) * Math.Cos(Rage2) + (MovePoint.X - CirPoint.X) * Math.Sin(Rage2));
            Point newpoint = new Point(CirPoint.X + newx, CirPoint.Y + newy);
            //计算长度
            double lineJ = Math.Sqrt(Math.Pow(Math.Max(newpoint.X, CirPoint.X) - Math.Min(newpoint.X, CirPoint.X), 2) + Math.Pow(Math.Max(newpoint.Y, CirPoint.Y) - Math.Min(newpoint.Y, CirPoint.Y), 2));
            return newpoint;
        }
        /// <summary>
        /// 逆时针转10读
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightrten_Click(object sender, RoutedEventArgs e)
        {
            RotateAngle(-5);
        }
        /// <summary>
        /// 旋转角度
        /// </summary>
        /// <param name="rotateAngle"></param>
        private void RotateAngle(double rotateAngle)
        {
            if (focusElement != null)
            {
                if (focusElement is UC.UC_Seat)
                {
                    UC.UC_Seat seat = focusElement as UC.UC_Seat;
                    RotateTransform rotateTransform = (RotateTransform)seat.bgimg.RenderTransform;
                    rotateTransform.Angle += rotateAngle;
                    if (rotateAngle < 0 && rotateTransform.Angle < 0)
                    {
                        rotateTransform.Angle += 360;
                    }
                    else if (rotateAngle > 0 && rotateTransform.Angle > 360)
                    {
                        rotateTransform.Angle -= 360;
                    }
                    seat.bgimg.RenderTransform = rotateTransform;
                }

                else
                {
                    UC.UC_Node note = focusElement as UC.UC_Node;
                    RotateTransform rotateTransform = (RotateTransform)note.bgimg.RenderTransform;
                    rotateTransform.Angle += rotateAngle;
                    if (rotateAngle < 0 && rotateTransform.Angle < 0)
                    {
                        rotateTransform.Angle += 360;
                    }
                    else if (rotateAngle > 0 && rotateTransform.Angle > 360)
                    {
                        rotateTransform.Angle -= 360;
                    }
                    note.bgimg.RenderTransform = rotateTransform;
                }
            }
            else if (focusElementList.Count > 0)
            {

                double rx = 0;
                double ry = 0;
                foreach (UserControl u in focusElementList)
                {
                    rx += Canvas.GetLeft(u) + u.Width / 2;
                    ry += Canvas.GetTop(u) + u.Height / 2;
                }
                Point rpoint = new Point(rx / focusElementList.Count, ry / focusElementList.Count);
                foreach (UserControl u in focusElementList)
                {
                    Point oldpoint = new Point(Canvas.GetLeft(u) + u.Width / 2, Canvas.GetTop(u) + u.Height / 2);
                    Point newpoint = GetNewPoint(rotateAngle, rpoint, oldpoint);
                    Canvas.SetLeft(u, newpoint.X - u.Width / 2);
                    Canvas.SetTop(u, newpoint.Y - u.Height / 2);
                    if (u is UC.UC_Seat)
                    {
                        UC.UC_Seat seat = u as UC.UC_Seat;
                        RotateTransform rotateTransform = (RotateTransform)seat.bgimg.RenderTransform;
                        rotateTransform.Angle += rotateAngle;
                        if (rotateAngle < 0 && rotateTransform.Angle < 0)
                        {
                            rotateTransform.Angle += 360;
                        }
                        else if (rotateAngle > 0 && rotateTransform.Angle > 360)
                        {
                            rotateTransform.Angle -= 360;
                        }
                        seat.bgimg.RenderTransform = rotateTransform;
                    }
                    else
                    {
                        UC.UC_Node note = u as UC.UC_Node;
                        RotateTransform rotateTransform = (RotateTransform)note.bgimg.RenderTransform;
                        rotateTransform.Angle += rotateAngle;
                        if (rotateAngle < 0 && rotateTransform.Angle < 0)
                        {
                            rotateTransform.Angle += 360;
                        }
                        else if (rotateAngle > 0 && rotateTransform.Angle > 360)
                        {
                            rotateTransform.Angle -= 360;
                        }
                        note.bgimg.RenderTransform = rotateTransform;
                    }
                }
            }
            else
            {
                viewModel.ErrorMessage = "请先选择一个目标";
            }
        }
        /// <summary>
        /// 顺时针转10度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void liftrten_Click(object sender, RoutedEventArgs e)
        {
            RotateAngle(5);
        }
        /// <summary>
        /// 转45读
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void liftrff_Click(object sender, RoutedEventArgs e)
        {
            RotateAngle(45);
        }

        private void rightrff_Click(object sender, RoutedEventArgs e)
        {
            RotateAngle(-45);
        }
        /// <summary>
        /// 转90度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void liftrn_Click(object sender, RoutedEventArgs e)
        {
            RotateAngle(90);
        }

        private void rightn_Click(object sender, RoutedEventArgs e)
        {
            RotateAngle(-90);
        }
        /// <summary>
        /// 是否有电源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void powerbtn_Click(object sender, RoutedEventArgs e)
        {
            if (focusElement != null && focusElement is UC.UC_Seat)
            {
                UC.UC_Seat seat = focusElement as UC.UC_Seat;
                if (seat.IsPower)
                {
                    seat.IsPower = false;
                    powerbtn.Visibility = System.Windows.Visibility.Visible;
                    nopowerbtn.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    seat.IsPower = true;
                    powerbtn.Visibility = System.Windows.Visibility.Collapsed;
                    nopowerbtn.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                viewModel.ErrorMessage = "请先选择一个目标";
            }
        }
        /// <summary>
        /// 是否停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopuserbtn_Click(object sender, RoutedEventArgs e)
        {
            if (focusElement != null && focusElement is UC.UC_Seat)
            {
                UC.UC_Seat seat = focusElement as UC.UC_Seat;
                if (seat.IsSuspended)
                {
                    seat.IsSuspended = false;
                    stopuserbtn.Visibility = System.Windows.Visibility.Visible;
                    startuserbtn.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    seat.IsSuspended = true;
                    stopuserbtn.Visibility = System.Windows.Visibility.Collapsed;
                    startuserbtn.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                viewModel.ErrorMessage = "请先选择一个目标";
            }
        }
        /// <summary>
        /// 座位流水号编号
        /// </summary>
        string serialNum = "001";
        /// <summary>
        /// 备注缓存
        /// </summary>
        string temptxt = "";
        void txtNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temptxt = (sender as TextBox).Text;
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }
        }

        void txtSeatNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                serialNum = SeatNumAdd((sender as TextBox).Text);
                (sender as TextBox).Text = (sender as TextBox).Text.ToUpper();
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
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
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }
            return newNo;

        }
        /// <summary>
        /// 整体顺时针90度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allrightnighty_Click(object sender, RoutedEventArgs e)
        {
            double newWidth = seatCanvas.Height;
            double newHeight = seatCanvas.Width;
            if (seatCanvas.Width > seatCanvas.Height)
            {
                seatCanvas.Height = seatCanvas.Width;
            }
            else
            {
                seatCanvas.Width = seatCanvas.Height;
            }
            foreach (FrameworkElement fe in seatCanvas.Children)
            {
                double newX = newWidth - (Canvas.GetTop(fe) + fe.Height / 2) - fe.Width / 2;
                double newY = Canvas.GetLeft(fe) + fe.Width / 2 - fe.Height / 2;
                if (fe is UC.UC_Seat)
                {
                    UC.UC_Seat seat = fe as UC.UC_Seat;
                    RotateTransform rotateTransform = (RotateTransform)seat.bgimg.RenderTransform;
                    rotateTransform.Angle += 90;
                    if (rotateTransform.Angle > 360)
                    {
                        rotateTransform.Angle -= 360;
                    }
                    seat.bgimg.RenderTransform = rotateTransform;
                }
                else
                {
                    UC.UC_Node note = fe as UC.UC_Node;
                    switch (note.Note.Type)
                    {
                        case SeatManage.EnumType.OrnamentType.Bookshelf:
                        case SeatManage.EnumType.OrnamentType.None:
                        case SeatManage.EnumType.OrnamentType.Pillar:
                        case SeatManage.EnumType.OrnamentType.Roundtable:
                        case SeatManage.EnumType.OrnamentType.Table:
                        case SeatManage.EnumType.OrnamentType.Wall:
                        case SeatManage.EnumType.OrnamentType.Window:
                            newX = newWidth - Canvas.GetTop(fe) - fe.Height;
                            newY = Canvas.GetLeft(fe);
                            double tempheight = fe.Height;
                            fe.Height = fe.Width;
                            fe.Width = tempheight;
                            break;
                        default:
                            RotateTransform rotateTransform = (RotateTransform)note.bgimg.RenderTransform;
                            rotateTransform.Angle += 90;
                            if (rotateTransform.Angle > 360)
                            {
                                rotateTransform.Angle -= 360;
                            }
                            note.bgimg.RenderTransform = rotateTransform;
                            break;
                    }
                }
                Canvas.SetLeft(fe, newX);
                Canvas.SetTop(fe, newY);
                seatCanvas.Width = newWidth;
                seatCanvas.Height = newHeight;
            }
        }
        /// <summary>
        /// 整体逆时针90读
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alllifenighty_Click(object sender, RoutedEventArgs e)
        {
            double newWidth = seatCanvas.Height;
            double newHeight = seatCanvas.Width;
            if (seatCanvas.Width > seatCanvas.Height)
            {
                seatCanvas.Height = seatCanvas.Width;
            }
            else
            {
                seatCanvas.Width = seatCanvas.Height;
            }
            foreach (FrameworkElement fe in seatCanvas.Children)
            {
                double newX = Canvas.GetTop(fe) - fe.Width / 2 + fe.Height / 2;
                double newY = newHeight - (Canvas.GetLeft(fe) + fe.Width / 2) - fe.Height / 2;
                if (fe is UC.UC_Seat)
                {
                    UC.UC_Seat seat = fe as UC.UC_Seat;
                    RotateTransform rotateTransform = (RotateTransform)seat.bgimg.RenderTransform;
                    rotateTransform.Angle -= 90;
                    if (rotateTransform.Angle < 0)
                    {
                        rotateTransform.Angle += 360;
                    }
                    seat.bgimg.RenderTransform = rotateTransform;
                }
                else
                {
                    UC.UC_Node note = fe as UC.UC_Node;
                    switch (note.Note.Type)
                    {
                        case SeatManage.EnumType.OrnamentType.Bookshelf:
                        case SeatManage.EnumType.OrnamentType.None:
                        case SeatManage.EnumType.OrnamentType.Pillar:
                        case SeatManage.EnumType.OrnamentType.Roundtable:
                        case SeatManage.EnumType.OrnamentType.Table:
                        case SeatManage.EnumType.OrnamentType.Wall:
                        case SeatManage.EnumType.OrnamentType.Window:
                            newX = Canvas.GetTop(fe);
                            newY = newHeight - Canvas.GetLeft(fe) - fe.Width;
                            double tempheight = fe.Height;
                            fe.Height = fe.Width;
                            fe.Width = tempheight;
                            break;
                        default:
                            RotateTransform rotateTransform = (RotateTransform)note.bgimg.RenderTransform;
                            rotateTransform.Angle -= 90;
                            if (rotateTransform.Angle < 0)
                            {
                                rotateTransform.Angle += 360;
                            }
                            note.bgimg.RenderTransform = rotateTransform;
                            break;
                    }
                }
                Canvas.SetLeft(fe, newX);
                Canvas.SetTop(fe, newY);
                seatCanvas.Width = newWidth;
                seatCanvas.Height = newHeight;
            }
        }
        /// <summary>
        /// 左边加格子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lifeadd_Click(object sender, RoutedEventArgs e)
        {
            seatCanvas.Width += 80;
            foreach (FrameworkElement fe in seatCanvas.Children)
            {
                Canvas.SetLeft(fe, Canvas.GetLeft(fe) + 80);
            }
        }
        /// <summary>
        /// 右边加格子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightadd_Click(object sender, RoutedEventArgs e)
        {
            seatCanvas.Width += 80;
        }
        /// <summary>
        /// 上面加格子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upadd_Click(object sender, RoutedEventArgs e)
        {
            seatCanvas.Height += 80;
            foreach (FrameworkElement fe in seatCanvas.Children)
            {
                Canvas.SetTop(fe, Canvas.GetTop(fe) + 80);
            }
        }
        /// <summary>
        /// 下面加格子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downadd_Click(object sender, RoutedEventArgs e)
        {
            seatCanvas.Height += 80;
        }
        /// <summary>
        /// 自动调整画板大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autowh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double top = seatCanvas.Height; ;
                double bottom = 0;
                double left = seatCanvas.Width; ;
                double right = 0;

                foreach (FrameworkElement fe in seatCanvas.Children)
                {
                    double fetop = Canvas.GetTop(fe);
                    double feleft = Canvas.GetLeft(fe);
                    double febottom = fetop + fe.Height;
                    double feright = feleft + fe.Width;
                    if (fetop < top)
                    {
                        top = fetop;
                    }
                    if (febottom > bottom)
                    {
                        bottom = febottom;
                    }
                    if (feleft < left)
                    {
                        left = feleft;
                    }
                    if (feright > right)
                    {
                        right = feright;
                    }
                }

                foreach (FrameworkElement fe in seatCanvas.Children)
                {
                    Canvas.SetLeft(fe, Canvas.GetLeft(fe) - left + 40);
                    Canvas.SetTop(fe, Canvas.GetTop(fe) - top + 40);
                }

                seatCanvas.Width = right - left + 80;
                seatCanvas.Height = bottom - top + 80;
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }
        }
        /// <summary>
        /// 方向选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poscb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (poscb.SelectedIndex > -1)
            {
                viewModel.Position = poscb.SelectedItem as ViewModel.RoomPosition;
            }
        }
        /// <summary>
        /// 刷新成新版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toNewVer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                seatCanvas.Height = seatCanvas.Height * 2;
                seatCanvas.Width = seatCanvas.Width * 2;
                foreach (FrameworkElement fe in seatCanvas.Children)
                {
                    Canvas.SetLeft(fe, Canvas.GetLeft(fe) * 2);
                    Canvas.SetTop(fe, Canvas.GetTop(fe) * 2);
                }
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }
        }
        /// <summary>
        /// 刷新成新版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toOldVer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                seatCanvas.Height = seatCanvas.Height / 2;
                seatCanvas.Width = seatCanvas.Width / 2;
                foreach (FrameworkElement fe in seatCanvas.Children)
                {
                    Canvas.SetLeft(fe, Canvas.GetLeft(fe) / 2);
                    Canvas.SetTop(fe, Canvas.GetTop(fe) / 2);
                }
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            SeatManage.ClassModel.SeatLayout seatLayout = new SeatManage.ClassModel.SeatLayout();
            seatLayout.SeatCol = int.Parse((seatCanvas.Width / 20).ToString().Split('.')[0]);
            seatLayout.SeatRow = int.Parse((seatCanvas.Height / 20).ToString().Split('.')[0]);
            if (viewModel.Room == null || string.IsNullOrEmpty(viewModel.Room.No))
            {
                viewModel.ErrorMessage = "请先选择阅览室！";
                return;
            }
            seatLayout.RoomNo = viewModel.Room.No;
            if (viewModel.Position.PositionValue == ViewModel.ReadingRoomPosition.None || string.IsNullOrEmpty(viewModel.Position.PositionName))
            {
                viewModel.ErrorMessage = "请选择阅览室方位！";
                return;
            }
            seatLayout.Position = viewModel.Position.PositionName;
            foreach (FrameworkElement fe in seatCanvas.Children)
            {
                if (fe is UC.UC_Seat)
                {
                    SeatManage.ClassModel.Seat seat = new SeatManage.ClassModel.Seat();
                    UC.UC_Seat uc_seat = fe as UC.UC_Seat;
                    RotateTransform rotateTransform = (RotateTransform)uc_seat.bgimg.RenderTransform;
                    seat.RotationAngle = int.Parse(rotateTransform.Angle.ToString().Split('.')[0]);
                    seat.ReadingRoomNum = viewModel.Room.No;
                    seat.SeatNo = viewModel.Room.No + uc_seat.Seat.ShortSeatNo;
                    seat.BaseHeight = 1;
                    seat.BaseWidth = 1;
                    seat.HavePower = uc_seat.Seat.HavePower;
                    seat.IsSuspended = uc_seat.Seat.IsSuspended;
                    seat.PositionX = Canvas.GetLeft(uc_seat) / 20;
                    seat.PositionY = Canvas.GetTop(uc_seat) / 20;
                    if (seatLayout.Seats.ContainsKey(seat.SeatNo))
                    {
                        viewModel.ErrorMessage = "座位编号" + seat.SeatNo + "重复";
                        return;
                    }
                    else
                    {
                        seatLayout.Seats.Add(seat.SeatNo, seat);
                    }
                }
                else
                {
                    UC.UC_Node uc_note = fe as UC.UC_Node;
                    SeatManage.ClassModel.Note note = new SeatManage.ClassModel.Note();
                    RotateTransform rotateTransform = (RotateTransform)uc_note.bgimg.RenderTransform;
                    note.RotationAngle = int.Parse(rotateTransform.Angle.ToString().Split('.')[0]);
                    note.BaseHeight = int.Parse((uc_note.Height / 20).ToString().Split('.')[0]);
                    note.BaseWidth = int.Parse((uc_note.Width / 20).ToString().Split('.')[0]);
                    note.PositionX = Canvas.GetLeft(uc_note) / 20;
                    note.PositionY = Canvas.GetTop(uc_note) / 20;
                    note.Remark = uc_note.txtNote.Text;
                    note.Type = uc_note.Note.Type;
                    seatLayout.Notes.Add(note);
                }
            }
            seatLayout.IsUpdate = true;
            viewModel.Room.ReadingRoomModel.SeatList = seatLayout;
            viewModel.Save();
            BindingData();
            seatCanvas.Children.Clear();
            seatCanvas.Height = 600;
            seatCanvas.Width = 600;
            viewModel.Room = null;
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.viewModel.Message = "保存成功！";
            mbw.viewModel.Type = Code.MessageBoxType.Success;
            mbw.ShowDialog();
        }
        /// <summary>
        /// 导出图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toImage_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Room == null)
            {
                viewModel.ErrorMessage = "请先选择一个阅览室";
                return;
            }
            SchoolNoteEditer.Code.ToImage drawSeat = new SchoolNoteEditer.Code.ToImage(viewModel.Room.ReadingRoomModel.SeatList);
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
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.viewModel.Message = "导出成功！";
                mbw.viewModel.Type = Code.MessageBoxType.Success;
                mbw.ShowDialog();
                viewModel.ErrorMessage = "导出成功！";
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = "保存失败！" + ex.Message;
            }
        }
        /// <summary>
        /// 导出脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sqlbtn_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Title = "保存为";
            openFileDialog.Filter = "sql文件|*.sql|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "sql";
            bool result = (bool)openFileDialog.ShowDialog();
            if (!result)
            {
                return;
            }
            string fileName = openFileDialog.FileName;
            try
            {
                Code.ToSQLScript.ToSQLString(fileName);
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.viewModel.Message = "导出成功！";
                mbw.viewModel.Type = Code.MessageBoxType.Success;
                mbw.ShowDialog();
                viewModel.ErrorMessage = "导出成功！";
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = "保存失败！" + ex.Message;
            }
        }
        /// <summary>
        /// 初始化阅览室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reNew_Click(object sender, RoutedEventArgs e)
        {
            seatCanvas.Children.Clear();
            seatCanvas.Width = 600;
            seatCanvas.Height = 600;
        }


        private void UCdelete_Click(object sender, RoutedEventArgs e)
        {
            if (focusElement != null)
            {
                seatCanvas.Children.Remove(focusElement);
                focusElement = null;
            }
            else
            {
                foreach (UserControl u in focusElementList)
                {
                    seatCanvas.Children.Remove(u);
                }
                focusElementList.Clear();
            }
        }

        private void opentool_Click(object sender, RoutedEventArgs e)
        {
            toolBox.Visibility = System.Windows.Visibility.Visible;
            closetool.Visibility = System.Windows.Visibility.Visible;
            opentool.Visibility = System.Windows.Visibility.Collapsed;
            if (operBox.Visibility != System.Windows.Visibility.Visible)
            {
                svMap.Margin = new Thickness(244, 121, 18, 8);
            }
            else
            {
                svMap.Margin = new Thickness(244, 121, 122, 8);
            }
        }

        private void closetool_Click(object sender, RoutedEventArgs e)
        {
            toolBox.Visibility = System.Windows.Visibility.Collapsed;
            closetool.Visibility = System.Windows.Visibility.Collapsed;
            opentool.Visibility = System.Windows.Visibility.Visible;
            if (operBox.Visibility != System.Windows.Visibility.Visible)
            {
                svMap.Margin = new Thickness(18, 121, 18, 8);
            }
            else
            {
                svMap.Margin = new Thickness(18, 121, 122, 8);
            }
        }

        private void openOpetor_Click(object sender, RoutedEventArgs e)
        {
            operBox.Visibility = System.Windows.Visibility.Collapsed;
            closeOpetor.Visibility = System.Windows.Visibility.Collapsed;
            openOpetor.Visibility = System.Windows.Visibility.Visible;
            if (toolBox.Visibility != System.Windows.Visibility.Visible)
            {
                svMap.Margin = new Thickness(18, 121, 18, 8);
            }
            else
            {
                svMap.Margin = new Thickness(244, 121, 18, 8);
            }
        }

        private void closeOpetor_Click(object sender, RoutedEventArgs e)
        {
            operBox.Visibility = System.Windows.Visibility.Visible;
            closeOpetor.Visibility = System.Windows.Visibility.Visible;
            openOpetor.Visibility = System.Windows.Visibility.Collapsed;
            if (toolBox.Visibility != System.Windows.Visibility.Visible)
            {
                svMap.Margin = new Thickness(18, 121, 122, 8);
            }
            else
            {
                svMap.Margin = new Thickness(244, 121, 122, 8);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        /// <summary>
        /// 画布大小改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void seatCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            VB_Seat.Height = seatCanvas.Height * slidervalue;
            VB_Seat.Width = seatCanvas.Width * slidervalue;
        }
        double slidervalue = 1;
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slidervalue = slider_seat.Value;
            if (VB_Seat != null && seatCanvas != null)
            {
                VB_Seat.Height = seatCanvas.Height * slidervalue;
                VB_Seat.Width = seatCanvas.Width * slidervalue;
            }
        }

        private void seatCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                slidervalue = slidervalue <= 0.1 ? 0.1 : slidervalue - 0.01;
                slider_seat.Value = slidervalue;
            }
            else
            {
                slidervalue = slidervalue >= 2 ? 2 : slidervalue + 0.01;
                slider_seat.Value = slidervalue;
            }
            if (VB_Seat != null && seatCanvas != null)
            {
                VB_Seat.Height = seatCanvas.Height * slidervalue;
                VB_Seat.Width = seatCanvas.Width * slidervalue;
            }
        }
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_max_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
        }

        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
        /// <summary>
        /// 导出二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toCode_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Room == null)
            {
                viewModel.ErrorMessage = "请先选择一个阅览室";
                return;
            }
            else
            {
                FunPage.DimensionalCode dcw = new FunPage.DimensionalCode();
                dcw.viewModel.RoomInfo = viewModel.Room.ReadingRoomModel;
                dcw.Show();
            }
        }
        /// <summary>
        /// 套用模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void roomCopyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (roomcb.SelectedIndex > 0)
            {
                viewModel.Room.ReadingRoomModel.SeatList = (roomCopycb.SelectedItem as ViewModel.ViewModel_ReadingRoom).ReadingRoomModel.SeatList;
                seatCanvas.Children.Clear();
                seatCanvas.Width = viewModel.Room.ReadingRoomModel.SeatList.SeatCol * 20;
                seatCanvas.Height = viewModel.Room.ReadingRoomModel.SeatList.SeatRow * 20;
                if (seatCanvas.Width == 0 && seatCanvas.Height == 0)
                {
                    seatCanvas.Width = 600;
                    seatCanvas.Height = 600;
                }
                foreach (KeyValuePair<string, SeatManage.ClassModel.Seat> seat in viewModel.Room.ReadingRoomModel.SeatList.Seats)
                {
                    UC.UC_Seat uc_Seat = new UC.UC_Seat();
                    uc_Seat.txtSeatNo.TextChanged += new TextChangedEventHandler(txtSeatNo_TextChanged);
                    uc_Seat.Seat = seat.Value;
                    uc_Seat.DataContext = uc_Seat.Seat;
                    uc_Seat.IsPower = uc_Seat.IsPower;
                    uc_Seat.IsSuspended = uc_Seat.IsSuspended;
                    uc_Seat.Height = 40;
                    uc_Seat.Width = 40;
                    RotateTransform rotateTransform = (RotateTransform)uc_Seat.bgimg.RenderTransform;
                    rotateTransform.Angle += seat.Value.RotationAngle;
                    uc_Seat.bgimg.RenderTransform = rotateTransform;
                    Canvas.SetLeft(uc_Seat, seat.Value.PositionX * 20);
                    Canvas.SetTop(uc_Seat, seat.Value.PositionY * 20);
                    seatCanvas.Children.Add(uc_Seat);
                }
                foreach (SeatManage.ClassModel.Note note in viewModel.Room.ReadingRoomModel.SeatList.Notes)
                {
                    UC.UC_Node uc_Note = new UC.UC_Node();
                    uc_Note.txtNote.TextChanged += new TextChangedEventHandler(txtNote_TextChanged);
                    uc_Note.Note = note;
                    uc_Note.DataContext = uc_Note.Note;
                    uc_Note.Width = note.BaseWidth * 20;
                    uc_Note.Height = note.BaseHeight * 20;
                    RotateTransform rotateTransform = (RotateTransform)uc_Note.bgimg.RenderTransform;
                    rotateTransform.Angle += note.RotationAngle;
                    uc_Note.bgimg.RenderTransform = rotateTransform;
                    Canvas.SetLeft(uc_Note, note.PositionX * 20);
                    Canvas.SetTop(uc_Note, note.PositionY * 20);
                    seatCanvas.Children.Add(uc_Note);
                }
                foreach (ViewModel.RoomPosition p in poscb.Items)
                {
                    if (p.PositionName == viewModel.Room.ReadingRoomModel.SeatList.Position)
                    {
                        poscb.SelectedItem = p;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 导出微信二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toCode_wechar_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Room == null)
            {
                viewModel.ErrorMessage = "请先选择一个阅览室";
                return;
            }
            else
            {
                FunPage.DimensionalCode_WeChar dcw = new FunPage.DimensionalCode_WeChar();
                dcw.viewModel.RoomInfo = viewModel.Room.ReadingRoomModel;
                dcw.Show();
            }
        }
        /// <summary>
        /// 批量导出二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toCode_all_Click(object sender, RoutedEventArgs e)
        {
            FunPage.DimensionalCode_All dcw = new FunPage.DimensionalCode_All();
            dcw.Show();
        }


    }
}
