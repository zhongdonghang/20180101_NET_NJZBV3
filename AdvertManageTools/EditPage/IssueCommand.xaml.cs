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
    /// IssueCommand.xaml 的交互逻辑
    /// </summary>
    public partial class IssueCommand : Window
    {
        #region 依赖属性
        /// <summary>
        /// 下发命令的Id
        /// </summary>
        public int CommandId
        {
            get { return (int)GetValue(CommandIdProperty); }
            set { SetValue(CommandIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandIdProperty =
            DependencyProperty.Register("CommandId", typeof(int), typeof(IssueCommand), new UIPropertyMetadata(-1));

        /// <summary>
        /// 下发命令
        /// </summary>
        public AdvertManage.Model.Enum.CommandType Command
        {
            get { return (AdvertManage.Model.Enum.CommandType)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(AdvertManage.Model.Enum.CommandType), typeof(IssueCommand), new UIPropertyMetadata(AdvertManage.Model.Enum.CommandType.None));

        #endregion

        public IssueCommand()
        {
            InitializeComponent();
            this.DataContext = vm;
        }
        IssueCommandViewModel vm = new IssueCommandViewModel();

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView myView = sender as TreeView;
            SchoolNodes node = myView.SelectedItem as SchoolNodes;
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.Issue(CommandId, Command);
                MessageBox.Show("下发成功");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("执行遇到异常：{0}",ex.Message));
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
