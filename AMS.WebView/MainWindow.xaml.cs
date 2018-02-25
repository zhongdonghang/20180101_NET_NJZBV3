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
using System.Configuration;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using WpfWebBrowser;
using System.ComponentModel;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
namespace AMS.WebView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.wb_WebView.Height = 870;
            this.Height = 920;
            //wb_WebView.Source = homeUrl;
            CreatNewPage(homeUrl);
            cbnTime.Interval = comeBackNormalTime;
            cbnTime.Elapsed += new System.Timers.ElapsedEventHandler(cbnTime_Elapsed);
            //cbnTime.Start();
            messageHelper = new WPFMessage.MessageHelper();
            messageHelper.GetMessage += new WPFMessage.MessageHelper.GetMessageEventHandler(messageHelper_GetMessage);
            (PresentationSource.FromVisual(this) as HwndSource).AddHook(new HwndSourceHook(messageHelper.WndProc));
            hook = new Code.KeyHook();
            hook.HookStart();
        }
        Code.KeyHook hook;
        private void WebBrowserOnNewWindow(object sender, CancelEventArgs e)
        {
            dynamic browser = sender;
            dynamic activeElement = browser.Document.activeElement;
            string link = activeElement.ToString();
            try
            {
                Uri url = new Uri(link);
                CreatNewPage(url);
            }
            catch
            {
            }
            finally
            {
                e.Cancel = true;
            }
        }
        void messageHelper_GetMessage(object sender, string message)
        {
            string[] msg = message.Split(';');
            int type = 0;
            if (int.TryParse(msg[0], out type))
            {
                switch ((SeatManage.EnumType.SendClentMessageType)type)
                {
                    case SeatManage.EnumType.SendClentMessageType.MoveDown:
                        {
                            int size = 0;
                            if (msg.Length > 1 && int.TryParse(msg[1], out size))
                            {
                                this.Top += size;
                            }
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.MoveUp:
                        {
                            int size = 0;
                            if (msg.Length > 1 && int.TryParse(msg[1], out size))
                            {
                                this.Top -= size;
                            }
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.Normal:
                        {
                            btn_WindowMax.Visibility = System.Windows.Visibility.Visible;
                            btn_WindowNormal.Visibility = System.Windows.Visibility.Hidden;
                            cbnTime.Start();
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.OnLock:
                        {
                            btn_WindowMax.Visibility = System.Windows.Visibility.Hidden;
                            btn_WindowNormal.Visibility = System.Windows.Visibility.Hidden;
                            this.Top = 0;
                            this.wbHost.Height = 870;
                            this.Height = 920;
                            cbnTime.Stop();
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.Refresh:
                        {
                            foreach (UIElement UIE in sp_RB.Children)
                            {
                                RadioButton rb = UIE as RadioButton;
                                WebBrowser wb = wbHost.FindName("wb_" + rb.Name.Split('_')[1]) as WebBrowser;
                                wb.Refresh();
                            }
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.Close:
                        {
                            this.Close();
                        }
                        break;
                }
            }
        }

        void cbnTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            cbnTime.Stop();
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                this.wbHost.Height += addSize;
                this.Height += addSize;
                WPFMessage.MessageHelper.SendMessage(sendMessageAPP, SeatManage.EnumType.SendClentMessageType.MoveDown, addSize.ToString());
                btn_WindowMax.Visibility = System.Windows.Visibility.Hidden;
                btn_WindowNormal.Visibility = System.Windows.Visibility.Visible;
            }));
        }
        private WPFMessage.MessageHelper messageHelper;
        private Uri homeUrl = new Uri(ConfigurationManager.AppSettings["HomeUrl"]);
        private double addSize = double.Parse(ConfigurationManager.AppSettings["AddSize"]);
        private double comeBackNormalTime = double.Parse(ConfigurationManager.AppSettings["ComeBackNormalTime"]) * 1000;
        private string sendMessageAPP = ConfigurationManager.AppSettings["SendMessageAPP"];
        private System.Timers.Timer cbnTime = new System.Timers.Timer();
        private int pageID = 0;
        /// <summary>
        /// 窗口最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_WindowMax_Click(object sender, RoutedEventArgs e)
        {
            this.wbHost.Height += addSize;
            this.Height += addSize;
            WPFMessage.MessageHelper.SendMessage(sendMessageAPP, SeatManage.EnumType.SendClentMessageType.MoveDown, addSize.ToString());
            btn_WindowMax.Visibility = System.Windows.Visibility.Hidden;
            btn_WindowNormal.Visibility = System.Windows.Visibility.Visible;
            cbnTime.Stop();
        }
        /// <summary>
        /// 窗口恢复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_WindowNormal_Click(object sender, RoutedEventArgs e)
        {
            this.wbHost.Height -= addSize;
            this.Height -= addSize;
            WPFMessage.MessageHelper.SendMessage(sendMessageAPP, SeatManage.EnumType.SendClentMessageType.MoveUp, addSize.ToString());
            btn_WindowMax.Visibility = System.Windows.Visibility.Visible;
            btn_WindowNormal.Visibility = System.Windows.Visibility.Hidden;
            cbnTime.Start();
        }
        /// <summary>
        /// 回到主页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement UIE in sp_RB.Children)
            {
                RadioButton rb = UIE as RadioButton;
                if (rb.IsChecked.Value)
                {
                    WebBrowser wb = wbHost.FindName("wb_" + rb.Name.Split('_')[1]) as WebBrowser;
                    wb.Source = homeUrl;
                }
            }
        }
        /// <summary>
        /// 前进
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement UIE in sp_RB.Children)
            {
                RadioButton rb = UIE as RadioButton;
                if (rb.IsChecked.Value)
                {
                    WebBrowser wb = wbHost.FindName("wb_" + rb.Name.Split('_')[1]) as WebBrowser;
                    if (wb.CanGoForward)
                    {
                        wb.GoForward();
                    }
                }
            }
        }
        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement UIE in sp_RB.Children)
            {
                RadioButton rb = UIE as RadioButton;
                if (rb.IsChecked.Value)
                {
                    WebBrowser wb = wbHost.FindName("wb_" + rb.Name.Split('_')[1]) as WebBrowser;
                    if (wb.CanGoBack)
                    {
                        wb.GoBack();
                    }
                }
            }
        }
        /// <summary>
        /// 创建新的标签页
        /// </summary>
        /// <param name="pageUrl"></param>
        private void CreatNewPage(Uri pageUrl)
        {
            string webID = (pageID++).ToString();
            WebBrowser wb = new WebBrowser();
            wb.Margin = new Thickness(0, 0, 0, 0);
            wb.Source = pageUrl;
            wb.Name = "wb_" + webID;
            wb.Navigating += new NavigatingCancelEventHandler(wb_Navigating);
            RegisterName(wb.Name, wb);
            wb.LoadCompleted += new LoadCompletedEventHandler(wb_LoadCompleted);
            WebBrowserHelper webBrowserHelper = new WebBrowserHelper(wb);
            HelperRegistery.SetHelperInstance(wb, webBrowserHelper);
            webBrowserHelper.NewWindow += WebBrowserOnNewWindow;
            foreach (UIElement UIE in wbHost.Children)
            {
                UIE.Visibility = System.Windows.Visibility.Hidden;
            }
            wbHost.Children.Add(wb);

            RadioButton rb = new RadioButton();
            rb.Style = (Style)this.FindResource("RadioButtonStyle_Page");
            rb.Height = 40;
            rb.Width = 100;
            rb.Margin = new Thickness(-5, 0, 0, 0);
            rb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            rb.IsChecked = true;
            rb.Name = "rb_" + webID;
            rb.Checked += new RoutedEventHandler(rb_Checked);
            RegisterName(rb.Name, rb);
            foreach (UIElement UIE in sp_RB.Children)
            {
                RadioButton r = UIE as RadioButton;
                r.IsChecked = false;
            }
            sp_RB.Children.Add(rb);
            if (sp_RB.ActualWidth > sp_cav.Width)
            {
                Canvas.SetLeft(sp_RB, sp_cav.Width - sp_RB.ActualWidth - 100);
            }
        }

        void wb_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            SetWebBrowserSilent(sender as WebBrowser, true);
        }
        /// <summary>  
        /// 设置浏览器静默，不弹错误提示框  
        /// </summary>  
        /// <param name="webBrowser">要设置的WebBrowser控件浏览器</param>  
        /// <param name="silent">是否静默</param>  
        private void SetWebBrowserSilent(WebBrowser webBrowser, bool silent)
        {
            FieldInfo fi = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fi != null)
            {
                object browser = fi.GetValue(webBrowser);
                if (browser != null)
                    browser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, browser, new object[] { silent });
            }
        }

        /// <summary>
        /// 点击选项卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            foreach (UIElement UIE in wbHost.Children)
            {
                WebBrowser wb = UIE as WebBrowser;
                if (wb.Name == "wb_" + rb.Name.Split('_')[1])
                {
                    wb.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    wb.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }
        /// <summary>
        /// 页面加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void wb_LoadCompleted(object sender, NavigationEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;
            RadioButton rb = sp_RB.FindName("rb_" + wb.Name.Split('_')[1]) as RadioButton;
            mshtml.IHTMLDocument2 doc = (mshtml.IHTMLDocument2)wb.Document;
            rb.Content = doc.title;
            SetWebBrowserSilent(sender as WebBrowser, true);
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            if (wbHost.Children.Count > 1)
            {
                for (int i = 0; i < sp_RB.Children.Count; i++)
                {
                    RadioButton rb = sp_RB.Children[i] as RadioButton;
                    if (rb.IsChecked.Value)
                    {
                        if (i < 1)
                        {
                            RadioButton rbn = sp_RB.Children[i + 1] as RadioButton;
                            rbn.IsChecked = true;
                            foreach (UIElement UIE in wbHost.Children)
                            {
                                WebBrowser wb = UIE as WebBrowser;
                                if (wb.Name == "wb_" + rbn.Name.Split('_')[1])
                                {
                                    wb.Visibility = System.Windows.Visibility.Visible;
                                }
                                else
                                {
                                    wb.Visibility = System.Windows.Visibility.Hidden;
                                }
                            }
                        }
                        else
                        {
                            RadioButton rbn = sp_RB.Children[i - 1] as RadioButton;
                            rbn.IsChecked = true;
                            foreach (UIElement UIE in wbHost.Children)
                            {
                                WebBrowser wb = UIE as WebBrowser;
                                if (wb.Name == "wb_" + rbn.Name.Split('_')[1])
                                {
                                    wb.Visibility = System.Windows.Visibility.Visible;
                                }
                                else
                                {
                                    wb.Visibility = System.Windows.Visibility.Hidden;
                                }
                            }
                        }
                        wbHost.Children.RemoveAt(i);
                        sp_RB.Children.RemoveAt(i);
                        break;
                    }
                }
                if (sp_RB.ActualWidth > sp_cav.Width)
                {
                    double left = Canvas.GetLeft(sp_RB) + 100;
                    if (left > 0)
                    {
                        left = 0;
                    }
                    Canvas.SetLeft(sp_RB, left);
                }
            }
            else
            {
                WebBrowser wb = wbHost.Children[0] as WebBrowser;
                wb.Source = homeUrl;
            }
        }

        private void btn_pageDown_Click(object sender, RoutedEventArgs e)
        {
            if ((sp_RB.ActualWidth + Canvas.GetLeft(sp_RB)) > sp_cav.Width)
            {
                Canvas.SetLeft(sp_RB, Canvas.GetLeft(sp_RB) - 100);
            }
        }

        private void btn_pageUp_Click(object sender, RoutedEventArgs e)
        {
            if ((Canvas.GetLeft(sp_RB)) < 0)
            {
                if ((Canvas.GetLeft(sp_RB)) > -100)
                {
                    Canvas.SetLeft(sp_RB, 0);
                }
                else
                {
                    Canvas.SetLeft(sp_RB, Canvas.GetLeft(sp_RB) + 100);
                }
            }
        }

        private void btn_keybroad_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"C:\WINDOWS\system32\osk.exe");
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            hook.HookStop();
        }

    }
}
