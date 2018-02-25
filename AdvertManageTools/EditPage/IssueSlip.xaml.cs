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
using System.Windows.Shapes;
using AdvertManageTools.Code;

namespace AdvertManageTools.EditPage
{
    /// <summary>
    /// IssueSlip.xaml 的交互逻辑
    /// </summary>
    public partial class IssueSlip : Window
    {
        public IssueSlip()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private bool _IsRelease = true;
        /// <summary>
        /// 发布还是离线
        /// </summary>
        public bool IsRelease
        {
            get { return _IsRelease; }
            set { _IsRelease = value; }
        }
        public IssueSlipViewModel IssusClip = new IssueSlipViewModel();
        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView myView = sender as TreeView;
            SlipNodes node = myView.SelectedItem as SlipNodes;
            if (node != null)
            {
                if (node.IsChecked)
                {
                    node.IsChecked = false;
                }
                else
                {
                    node.IsChecked = true;
                }
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

                if (_IsRelease)
                {
                    if (IssusClip.SCRelease())
                    {
                        MessageBox.Show("下发完成！");
                        this.Close();
                    }
                }
                else
                {
                    System.Windows.Forms.FolderBrowserDialog foldBrowerDialog = new System.Windows.Forms.FolderBrowserDialog();
                    foldBrowerDialog.ShowDialog();
                    if (!string.IsNullOrEmpty(foldBrowerDialog.SelectedPath))
                    {
                        if (IssusClip.SCDownLoad(foldBrowerDialog.SelectedPath))
                        {
                            MessageBox.Show("离线发布完成！");
                            this.Close();
                        }
                    }
                }

        }
        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IssusClip.GetSchoolInfo();
            campustreeView.ItemsSource = IssusClip.SchoolList;
        }
    }
}
