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
using AdvertManageTools.Code;
using AdvertManageTools.EditPage;

namespace AdvertManageTools.UserControl
{
    /// <summary>
    /// PrintTemplateInfo.xaml 的交互逻辑
    /// </summary>
    public partial class PrintTemplateInfo : System.Windows.Controls.UserControl
    {
        public PrintTemplateInfo()
        {
            InitializeComponent();
            this.DataContext = this;
            TemplateGrid.ItemsSource = printtemplatelist.PrintList;
        }
        PrintTemplateListViewModel printtemplatelist = new PrintTemplateListViewModel();
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void DataBinding()
        {
            printtemplatelist.GetData();
        }
        /// <summary>
        /// 下发播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelease_Click(object sender, RoutedEventArgs e)
        {
            PrintTemplateViewModel Releasetemplate = TemplateGrid.Items[TemplateGrid.SelectedIndex] as PrintTemplateViewModel;
            IssueCommand ic = new IssueCommand();
            ic.Command = AdvertManage.Model.Enum.CommandType.PrintTemplate;
            ic.CommandId = Releasetemplate.Id;
            ic.ShowDialog();
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            PrintTemplateViewModel Releasetemplate = TemplateGrid.Items[TemplateGrid.SelectedIndex] as PrintTemplateViewModel;
            System.Windows.Forms.FolderBrowserDialog foldBrowerDialog = new System.Windows.Forms.FolderBrowserDialog();
            foldBrowerDialog.ShowDialog();
            if (!string.IsNullOrEmpty(foldBrowerDialog.SelectedPath))
            {
                if (Releasetemplate.DownLoad(foldBrowerDialog.SelectedPath))
                {
                    MessageBox.Show("发布离线版本成功！");
                }
                else
                {
                    MessageBox.Show("发布离线版本失败！");
                }
            }
        }
    }
}
