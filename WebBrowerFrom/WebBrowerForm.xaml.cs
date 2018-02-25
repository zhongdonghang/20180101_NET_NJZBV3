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
 

namespace Wpf_WebBrower
{
    /// <summary>
    /// WebBrowerForm.xaml 的交互逻辑
    /// </summary>
    public partial class WebBrowerForm : Window
    {
        System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();
        System.Windows.Forms.WebBrowser webBrowser = new System.Windows.Forms.WebBrowser();  
        public WebBrowerForm()
        {
            InitializeComponent();

            try
            {
                host.Child = webBrowser;
                this.grid.Children.Add(host);
                Grid.SetRow(host, 1);
                host.Background = new SolidColorBrush(Colors.Red);
                webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);
                webBrowser.NewWindow += new System.ComponentModel.CancelEventHandler(webBrowser_NewWindow);
                webBrowser.ScriptErrorsSuppressed = true;
            }
            catch
                (Exception ex)
            { }
        }

        void webBrowser_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        void web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            //将所有的链接的目标，指向本窗体     
            foreach (System.Windows.Forms.HtmlElement archor in this.webBrowser.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }


            //将所有的FORM的提交目标，指向本窗体     
            foreach (System.Windows.Forms.HtmlElement form in this.webBrowser.Document.Forms)
            {
                form.SetAttribute("target", "_self");
            }
        }
        /// <summary>
        /// IE内核的WebBrowser
        /// </summary>
        public System.Windows.Forms.WebBrowser MyWebBrowser
        {
            get { return webBrowser; } 
        }
    }
}
