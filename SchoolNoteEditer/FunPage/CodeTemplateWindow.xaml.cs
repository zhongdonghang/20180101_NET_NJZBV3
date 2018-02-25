using Microsoft.Win32;
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

namespace SchoolNoteEditer.FunPage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CodeTemplateWindow : Window
    {
        public CodeTemplateWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
        ViewModel.VM_MainWindow viewModel = new ViewModel.VM_MainWindow();
        string mouseDownType = "";
        Point ptargetElement = new Point();
        /// <summary>
        /// 载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cb_Angle.SelectedIndex = 0;
            cb_TextAlignment.SelectedIndex = 0;
        }
        /// <summary>
        /// 鼠标控件按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void elementCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement targetElement = e.Source as FrameworkElement;
            if (targetElement != null && (targetElement is UC.UC_Element || targetElement is UC.UC_TextElement))
            {
                Point p = Mouse.GetPosition(targetElement);
                if (targetElement.Width - p.X > 30 && p.Y > 30)
                {

                    targetElement.CaptureMouse();
                    mouseDownType = "Move";
                    ptargetElement = p;
                }
                else if (targetElement.Width - p.X <= 30 && targetElement.Height - p.Y <= 30)
                {
                    targetElement.CaptureMouse();
                    mouseDownType = "Deform";
                }
            }
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void elementCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement targetElement = Mouse.Captured as FrameworkElement;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (targetElement is UC.UC_Element)
                {
                    ElementMove(e, targetElement as UC.UC_Element);
                }
                else if (targetElement is UC.UC_TextElement)
                {
                    ElementMove(e, targetElement as UC.UC_TextElement);
                }
            }
        }
        /// <summary>
        /// 鼠标移动拖拽
        /// </summary>
        /// <param name="e"></param>
        /// <param name="targetElement"></param>
        private void ElementMove(MouseEventArgs e, UC.UC_Element targetElement)
        {
            ElementGetFouce(targetElement);
            Point pCanvas = e.GetPosition(elementCanvas);
            pCanvas.X = pCanvas.X % 1 == 0 ? pCanvas.X : pCanvas.X - (pCanvas.X % 1);
            pCanvas.Y = pCanvas.Y % 1 == 0 ? pCanvas.Y : pCanvas.Y - (pCanvas.Y % 1);
            switch (mouseDownType)
            {
                case "Move":
                    {
                        double newX = pCanvas.X - ptargetElement.X;
                        double newY = pCanvas.Y - ptargetElement.Y;
                        newX = newX % viewModel.MinXY == 0 ? newX : newX - (newX % viewModel.MinXY);
                        newY = newY % viewModel.MinXY == 0 ? newY : newY - (newY % viewModel.MinXY);
                        targetElement.viewModel.ElementLeft = newX < 0 ? 0 : newX;
                        targetElement.viewModel.ElementTop = newY < 0 ? 0 : newY;
                        viewModel.TemplateWidth = (newX + targetElement.Width) > viewModel.TemplateWidth ? newX + targetElement.Width : viewModel.TemplateWidth;
                        viewModel.TemplateHeight = (newY + targetElement.Height) > viewModel.TemplateHeight ? newY + targetElement.Height : viewModel.TemplateHeight;
                        VB_Element.Height = viewModel.TemplateHeight * daltaSliderValue / 30;
                        VB_Element.Width = viewModel.TemplateWidth * daltaSliderValue / 30;
                    }
                    break;
                case "Deform":
                    {
                        double newWidth = (pCanvas.X - targetElement.viewModel.ElementLeft) > (pCanvas.Y - targetElement.viewModel.ElementTop) ? ((pCanvas.X - targetElement.viewModel.ElementLeft) < 100 ? 100 : pCanvas.X - targetElement.viewModel.ElementLeft) : ((pCanvas.Y - targetElement.viewModel.ElementTop) < 100 ? 100 : pCanvas.Y - targetElement.viewModel.ElementTop);
                        double newHeight = ((newWidth / targetElement.viewModel.ElementWidth) * targetElement.viewModel.ElementHeight);
                        targetElement.viewModel.ElementWidth = newWidth;
                        targetElement.viewModel.ElementHeight = newHeight;
                        viewModel.TemplateWidth = pCanvas.X > viewModel.TemplateWidth ? pCanvas.X : viewModel.TemplateWidth;
                        viewModel.TemplateHeight = pCanvas.Y > viewModel.TemplateHeight ? pCanvas.X : viewModel.TemplateHeight;
                        VB_Element.Height = viewModel.TemplateHeight * daltaSliderValue / 30;
                        VB_Element.Width = viewModel.TemplateWidth * daltaSliderValue / 30;
                    }
                    break;
            }
        }
        private void ElementMove(MouseEventArgs e, UC.UC_TextElement targetElement)
        {
            ElementGetFouce(targetElement);
            Point pCanvas = e.GetPosition(elementCanvas);
            pCanvas.X = pCanvas.X % 1 == 0 ? pCanvas.X : pCanvas.X - (pCanvas.X % 1);
            pCanvas.Y = pCanvas.Y % 1 == 0 ? pCanvas.Y : pCanvas.Y - (pCanvas.Y % 1);
            switch (mouseDownType)
            {
                case "Move":
                    {
                        double newX = pCanvas.X - ptargetElement.X;
                        double newY = pCanvas.Y - ptargetElement.Y;
                        newX = newX % viewModel.MinXY == 0 ? newX : newX - (newX % viewModel.MinXY);
                        newY = newY % viewModel.MinXY == 0 ? newY : newY - (newY % viewModel.MinXY);
                        targetElement.viewModel.ElementLeft = newX < 0 ? 0 : newX;
                        targetElement.viewModel.ElementTop = newY < 0 ? 0 : newY;
                        viewModel.TemplateWidth = (newX + targetElement.Width) > viewModel.TemplateWidth ? newX + targetElement.Width : viewModel.TemplateWidth;
                        viewModel.TemplateHeight = (newY + targetElement.Height) > viewModel.TemplateHeight ? newY + targetElement.Height : viewModel.TemplateHeight;
                        VB_Element.Height = viewModel.TemplateHeight * daltaSliderValue / 30;
                        VB_Element.Width = viewModel.TemplateWidth * daltaSliderValue / 30;
                    }
                    break;
                case "Deform":
                    {
                        targetElement.viewModel.ElementWidth = (pCanvas.X - targetElement.viewModel.ElementLeft) < 100 ? 100 : pCanvas.X - targetElement.viewModel.ElementLeft;
                        targetElement.viewModel.ElementHeight = (pCanvas.Y - targetElement.viewModel.ElementTop) < 50 ? 50 : pCanvas.Y - targetElement.viewModel.ElementTop;
                        viewModel.TemplateWidth = pCanvas.X > viewModel.TemplateWidth ? pCanvas.X : viewModel.TemplateWidth;
                        viewModel.TemplateHeight = pCanvas.Y > viewModel.TemplateHeight ? pCanvas.X : viewModel.TemplateHeight;
                        VB_Element.Height = viewModel.TemplateHeight * daltaSliderValue / 30;
                        VB_Element.Width = viewModel.TemplateWidth * daltaSliderValue / 30;
                    }
                    break;
            }
        }
        /// <summary>
        /// 获取焦点
        /// </summary>
        /// <param name="targetElement"></param>
        private void ElementGetFouce(UC.UC_Element targetElement)
        {
            foreach (UserControl fe in elementCanvas.Children)
            {
                fe.BorderThickness = new Thickness(0);
            }
            targetElement.BorderThickness = new Thickness(5);
            viewModel.NowEditViewElement = targetElement.viewModel;
            cb_Angle.SelectedValue = viewModel.NowEditViewElement.Angle;
            cb_TextAlignment.SelectedValue = viewModel.NowEditViewElement.TextAlignment;
            g_color.Visibility = System.Windows.Visibility.Collapsed;
            g_Other.Visibility = System.Windows.Visibility.Collapsed;
            g_Text.Visibility = System.Windows.Visibility.Collapsed;
            switch (targetElement.viewModel.Type)
            {
                case SeatManage.ClassModel.DimensionalElementTye.Background:
                case SeatManage.ClassModel.DimensionalElementTye.Image:
                    break;
                case SeatManage.ClassModel.DimensionalElementTye.ReadingRoomName:
                    g_Text.Visibility = System.Windows.Visibility.Visible;
                    g_color.Visibility = System.Windows.Visibility.Visible;
                    break;
                case SeatManage.ClassModel.DimensionalElementTye.SeatCode:
                    g_Other.Visibility = System.Windows.Visibility.Visible;
                    break;
                case SeatManage.ClassModel.DimensionalElementTye.SeatNo:
                    g_Text.Visibility = System.Windows.Visibility.Visible;
                    g_color.Visibility = System.Windows.Visibility.Visible;
                    g_Other.Visibility = System.Windows.Visibility.Visible;
                    break;
                case SeatManage.ClassModel.DimensionalElementTye.Text:
                    g_Text.Visibility = System.Windows.Visibility.Visible;
                    g_color.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
            //this.DataContext = viewModel;
        }
        private void ElementGetFouce(UC.UC_TextElement targetElement)
        {
            foreach (UserControl fe in elementCanvas.Children)
            {
                fe.BorderThickness = new Thickness(0);
            }
            targetElement.BorderThickness = new Thickness(5);
            viewModel.NowEditViewElement = targetElement.viewModel;
            cb_Angle.SelectedValue = viewModel.NowEditViewElement.Angle;
            cb_TextAlignment.SelectedValue = viewModel.NowEditViewElement.TextAlignment;
            g_color.Visibility = System.Windows.Visibility.Collapsed;
            g_Other.Visibility = System.Windows.Visibility.Collapsed;
            g_Text.Visibility = System.Windows.Visibility.Collapsed;
            switch (targetElement.viewModel.Type)
            {
                case SeatManage.ClassModel.DimensionalElementTye.Background:
                case SeatManage.ClassModel.DimensionalElementTye.Image:
                    break;
                case SeatManage.ClassModel.DimensionalElementTye.ReadingRoomName:
                    g_Text.Visibility = System.Windows.Visibility.Visible;
                    g_color.Visibility = System.Windows.Visibility.Visible;
                    break;
                case SeatManage.ClassModel.DimensionalElementTye.SeatCode:
                    g_Other.Visibility = System.Windows.Visibility.Visible;
                    break;
                case SeatManage.ClassModel.DimensionalElementTye.SeatNo:
                    g_Text.Visibility = System.Windows.Visibility.Visible;
                    g_color.Visibility = System.Windows.Visibility.Visible;
                    g_Other.Visibility = System.Windows.Visibility.Visible;
                    break;
                case SeatManage.ClassModel.DimensionalElementTye.Text:
                    g_Text.Visibility = System.Windows.Visibility.Visible;
                    g_color.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
            //this.DataContext = viewModel;
        }
        /// <summary>
        /// 释放鼠标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void elementCanvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            mouseDownType = "";
            ptargetElement = new Point();
            CanvalNewSize();
        }
        /// <summary>
        /// 重新改变模板尺寸
        /// </summary>
        private void CanvalNewSize()
        {
            double maxH = 0;
            double maxW = 0;
            foreach (FrameworkElement fe in elementCanvas.Children)
            {
                double feH = Canvas.GetTop(fe) + fe.Height;
                double feW = Canvas.GetLeft(fe) + fe.Width;
                maxH = maxH < feH ? feH : maxH;
                maxW = maxW < feW ? feW : maxW;
            }
            viewModel.TemplateHeight = maxH == 0 ? viewModel.TemplateHeight : maxH;
            viewModel.TemplateWidth = maxW == 0 ? viewModel.TemplateHeight : maxW;
            VB_Element.Height = viewModel.TemplateHeight * daltaSliderValue / 30;
            VB_Element.Width = viewModel.TemplateWidth * daltaSliderValue / 30;
            foreach (FrameworkElement fe in elementCanvas.Children)
            {
                if (fe is UC.UC_Element)
                {
                    (fe as UC.UC_Element).viewModel.TemplateHeight = viewModel.TemplateHeight;
                    (fe as UC.UC_Element).viewModel.TemplateWidth = viewModel.TemplateWidth;
                }
                else if (fe is UC.UC_TextElement)
                {
                    (fe as UC.UC_TextElement).viewModel.TemplateHeight = viewModel.TemplateHeight;
                    (fe as UC.UC_TextElement).viewModel.TemplateWidth = viewModel.TemplateWidth;
                }
            }
        }
        /// <summary>
        /// 缩放比例
        /// </summary>
        double daltaSliderValue = 30;
        /// <summary>
        /// 鼠标滚动缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void elementCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                daltaSliderValue = daltaSliderValue <= 1 ? 1 : daltaSliderValue - 1;
            }
            else
            {
                daltaSliderValue = daltaSliderValue >= 60 ? 60 : daltaSliderValue + 1;
            }

            VB_Element.Height = viewModel.TemplateHeight * daltaSliderValue / 30;
            VB_Element.Width = viewModel.TemplateWidth * daltaSliderValue / 30;
            int i = e.Delta;
        }

        /// <summary>
        /// 角度选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Angle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_Angle.SelectedIndex > -1)
            {
                viewModel.NowEditViewElement.Angle = (cb_Angle.SelectedItem as ViewModel.VM_Angle).Value;
            }
        }
        /// <summary>
        /// 对齐方式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_TextAlignment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_TextAlignment.SelectedIndex > -1)
            {
                viewModel.NowEditViewElement.TextAlignment = (cb_TextAlignment.SelectedItem as ViewModel.VM_TextAlignment).Value;
            }
        }
        /// <summary>
        /// 字号增大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_FontSizeLower_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NowEditViewElement.FontSize = viewModel.NowEditViewElement.FontSize == 0 ? 0 : viewModel.NowEditViewElement.FontSize - 1;
        }
        /// <summary>
        /// 字号减小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_FontSizePluse_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NowEditViewElement.FontSize++;
        }
        /// <summary>
        /// 添加背景图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddBG_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "背景图片|*.jpg;*.bmp;*.jpeg;*.png";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                UC.UC_Element element = new UC.UC_Element();
                element.viewModel.ImagePath = ofd.FileName;
                element.viewModel.Type = SeatManage.ClassModel.DimensionalElementTye.Background;
                element.viewModel.ElementLeft = 0;
                element.viewModel.ElementTop = 0;
                elementCanvas.Children.Insert(0, element);
                CanvalNewSize();
            }
        }

        /// <summary>
        /// 添加二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddCode_Click(object sender, RoutedEventArgs e)
        {
            UC.UC_Element element = new UC.UC_Element();
            element.viewModel.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "CodeImage.png";
            element.viewModel.Type = SeatManage.ClassModel.DimensionalElementTye.SeatCode;
            element.viewModel.ElementLeft = 0;
            element.viewModel.ElementTop = 0;
            elementCanvas.Children.Add(element);
            CanvalNewSize();
        }

        /// <summary>
        /// 添加装饰图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "装饰图片|*.jpg;*.bmp;*.jpeg;*.png";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                UC.UC_Element element = new UC.UC_Element();
                element.viewModel.ImagePath = ofd.FileName;
                element.viewModel.Type = SeatManage.ClassModel.DimensionalElementTye.Image;
                element.viewModel.ElementLeft = 0;
                element.viewModel.ElementTop = 0;
                elementCanvas.Children.Add(element);
                CanvalNewSize();
            }
        }
        /// <summary>
        /// 添加座位编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SeatNo_Click(object sender, RoutedEventArgs e)
        {
            UC.UC_TextElement element = new UC.UC_TextElement();
            element.viewModel.Type = SeatManage.ClassModel.DimensionalElementTye.SeatNo;
            element.viewModel.ElementLeft = 0;
            element.viewModel.ElementTop = 0;
            element.viewModel.ElementHeight = 100;
            element.viewModel.ElementWidth = 300;
            element.viewModel.FontSize = 100;
            element.viewModel.Text = "A01";
            element.viewModel.TextAlignment = SeatManage.ClassModel.ElementTextAlignment.Center;
            elementCanvas.Children.Add(element);
            CanvalNewSize();
        }
        /// <summary>
        /// 添加阅览室编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RoomNo_Click(object sender, RoutedEventArgs e)
        {
            UC.UC_TextElement element = new UC.UC_TextElement();
            element.viewModel.Type = SeatManage.ClassModel.DimensionalElementTye.ReadingRoomName;
            element.viewModel.ElementLeft = 0;
            element.viewModel.ElementTop = 0;
            element.viewModel.ElementHeight = 80;
            element.viewModel.ElementWidth = 500;
            element.viewModel.FontSize = 50;
            element.viewModel.Text = "A202第一期刊阅览室B区";
            elementCanvas.Children.Add(element);
            CanvalNewSize();
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Remark_Click(object sender, RoutedEventArgs e)
        {
            UC.UC_TextElement element = new UC.UC_TextElement();
            element.viewModel.Type = SeatManage.ClassModel.DimensionalElementTye.Text;
            element.viewModel.ElementLeft = 0;
            element.viewModel.ElementTop = 0;
            element.viewModel.ElementHeight = 80;
            element.viewModel.ElementWidth = 400;
            element.viewModel.FontSize = 50;
            element.viewModel.Text = "【座位二维码扫描】";
            elementCanvas.Children.Add(element);
            CanvalNewSize();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath == string.Empty)
            {
                return;
            }
            viewModel.SavePath = fbd.SelectedPath;
            viewModel.Model.ElementList.Clear();
            viewModel.Model.ImageFiles.Clear();
            foreach (UserControl fe in elementCanvas.Children)
            {
                SeatManage.ClassModel.DimensionalElement ucElement = new SeatManage.ClassModel.DimensionalElement();
                if (fe is UC.UC_Element)
                {
                    ucElement = (fe as UC.UC_Element).viewModel.Model;
                }
                else if (fe is UC.UC_TextElement)
                {
                    ucElement = (fe as UC.UC_TextElement).viewModel.Model;
                }
                SeatManage.ClassModel.DimensionalElement newElement = new SeatManage.ClassModel.DimensionalElement();
                newElement.Alignment = ucElement.Alignment;
                newElement.Angle = ucElement.Angle;
                newElement.FontColor = ucElement.FontColor;
                newElement.FontSize = ucElement.FontSize;
                newElement.Height = ucElement.Height;
                newElement.ImageFile = ucElement.ImageFile;
                newElement.Order = ucElement.Order;
                newElement.PosintionX = ucElement.PosintionX;
                newElement.PosintionY = ucElement.PosintionY;
                newElement.Text = ucElement.Text;
                newElement.Type = ucElement.Type;
                newElement.Width = ucElement.Width;
                viewModel.Model.ElementList.Add(newElement);
            }
            viewModel.Save();
        }
        /// <summary>
        /// 控件复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Copy_Click(object sender, RoutedEventArgs e)
        {
            double newX = viewModel.NowEditViewElement.ElementLeft;
            double newY = viewModel.NowEditViewElement.ElementTop;
            double newAngle = viewModel.NowEditViewElement.Angle;
            ElementCopy(newX, newY, newAngle);
        }
        /// <summary>
        /// 左右反转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_LR_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NowEditViewElement.ElementLeft = viewModel.NowEditViewElement.ElementRight;
        }
        /// <summary>
        /// 对角线翻转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_LRTBA_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NowEditViewElement.ElementLeft = viewModel.NowEditViewElement.ElementRight;
            viewModel.NowEditViewElement.ElementTop = viewModel.NowEditViewElement.ElementBottom;
            viewModel.NowEditViewElement.Angle = Math.Abs(viewModel.NowEditViewElement.Angle - 180);
        }
        /// <summary>
        /// 上下反转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_TB_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NowEditViewElement.ElementTop = viewModel.NowEditViewElement.ElementBottom;
        }
        /// <summary>
        /// 左右翻转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_LRA_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NowEditViewElement.ElementLeft = viewModel.NowEditViewElement.ElementRight;
            viewModel.NowEditViewElement.Angle = Math.Abs(viewModel.NowEditViewElement.Angle - 180);
        }
        /// <summary>
        /// 上下翻转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_TBA_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NowEditViewElement.ElementTop = viewModel.NowEditViewElement.ElementBottom;
            viewModel.NowEditViewElement.Angle = Math.Abs(viewModel.NowEditViewElement.Angle - 180);
        }
        
        /// <summary>
        ///复制元素
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="newAngle"></param>
        private void ElementCopy(double newX, double newY, double newAngle)
        {
            if (viewModel.NowEditViewElement.Type == SeatManage.ClassModel.DimensionalElementTye.ReadingRoomName || viewModel.NowEditViewElement.Type == SeatManage.ClassModel.DimensionalElementTye.SeatNo || viewModel.NowEditViewElement.Type == SeatManage.ClassModel.DimensionalElementTye.Text)
            {
                UC.UC_TextElement element = new UC.UC_TextElement();
                element.viewModel.TextAlignment = viewModel.NowEditViewElement.TextAlignment;

                element.viewModel.Color = viewModel.NowEditViewElement.Color;
                element.viewModel.FontSize = viewModel.NowEditViewElement.FontSize;
                element.viewModel.Text = viewModel.NowEditViewElement.Text;
                element.viewModel.ElementHeight = viewModel.NowEditViewElement.ElementHeight;
                element.viewModel.ElementWidth = viewModel.NowEditViewElement.ElementWidth;
                element.viewModel.Type = viewModel.NowEditViewElement.Type;
                if (element.viewModel.Type == SeatManage.ClassModel.DimensionalElementTye.SeatNo)
                {
                    foreach (FrameworkElement fe in elementCanvas.Children)
                    {
                        if (fe is UC.UC_TextElement && (fe as UC.UC_TextElement).viewModel.Type== SeatManage.ClassModel.DimensionalElementTye.SeatNo)
                        {
                            element.viewModel.Order++;
                        }
                    }
                }

                element.viewModel.ElementLeft = newX;
                element.viewModel.ElementTop = newY;
                element.viewModel.Angle = newAngle;

                elementCanvas.Children.Add(element);
                CanvalNewSize();
                ElementGetFouce(element);
            }
            else
            {
                UC.UC_Element element = new UC.UC_Element();
                element.viewModel.ImagePath = viewModel.NowEditViewElement.ImagePath;
                element.viewModel.ElementHeight = viewModel.NowEditViewElement.ElementHeight;
                element.viewModel.ElementWidth = viewModel.NowEditViewElement.ElementWidth;
                element.viewModel.Type = viewModel.NowEditViewElement.Type;
                if (element.viewModel.Type == SeatManage.ClassModel.DimensionalElementTye.SeatCode)
                {
                    foreach (FrameworkElement fe in elementCanvas.Children)
                    {
                        if (fe is UC.UC_Element && (fe as UC.UC_Element).viewModel.Type == SeatManage.ClassModel.DimensionalElementTye.SeatCode)
                        {
                            element.viewModel.Order++;
                        }
                    }
                }

                element.viewModel.ElementLeft = newX;
                element.viewModel.ElementTop = newY;
                element.viewModel.Angle = newAngle;

                elementCanvas.Children.Add(element);
                CanvalNewSize();
                ElementGetFouce(element);
            }
        }

        
    }
}
