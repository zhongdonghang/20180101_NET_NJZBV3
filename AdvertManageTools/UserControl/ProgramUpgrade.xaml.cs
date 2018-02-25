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
using System.Threading;

namespace AdvertManageTools.UserControl
{
    /// <summary>
    /// ProgramUpgrade.xaml 的交互逻辑
    /// </summary>
    public partial class ProgramUpgrade : System.Windows.Controls.UserControl
    {
        ProgramUpgradeViewModel vm = new ProgramUpgradeViewModel();
        public event EventHandler HandlerOver;
        public ProgramUpgrade()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (!string.IsNullOrEmpty(fbd.SelectedPath))
            {
                vm.DirPath = fbd.SelectedPath;
                vm.BuildUpdateConfigFile(vm.DirPath);
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox item = sender as ComboBox;
            if (item != null)
            {
                SystemItem selectItem = item.SelectedItem as SystemItem;

                if (selectItem != null && selectItem.Value != -1)
                {
                    vm.SystemType = (AdvertManage.Model.Enum.SeatManageSubsystem)selectItem.Value;
                    vm.FindSystemMessage();
                }
                else if (item == null)
                {
                    MessageBox.Show("系统类型有误");
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SystemType == AdvertManage.Model.Enum.SeatManageSubsystem.None)
            {
                MessageBox.Show("请选择要发布的系统类型");
                return;
            }
            if (string.IsNullOrEmpty(vm.Version))
            {
                MessageBox.Show("请填写版本号");
                return;
            }
            if (vm.Nodes == null || vm.Nodes.Count == 0)
            {
                MessageBox.Show("请选择要发布的程序");
                return;
            }

            string startProgram = vm.CheckIsCheckFile(vm.Nodes);
            if (string.IsNullOrEmpty(startProgram))
            {
                MessageBox.Show("请展开树状菜单，选择程序的启动文件。");
                return;
            }
            if (startProgram.Substring(startProgram.LastIndexOf('.') + 1) != "exe")
            {
                MessageBox.Show("启动程序必须是以.exe为后缀的可执行文件");
                return;
            }
            if (string.IsNullOrEmpty(vm.UpdateLog))
            {
                MessageBox.Show("请填写更新说明");
                return;
            }
            vm.StartProgram = startProgram;

            myThread = new Thread(new ThreadStart(fileUpload));
            myThread.Start();
        }
        Thread myThread = null;

        void fileUpload()
        {
            if (!string.IsNullOrEmpty(vm.BuildUpdateFile()))
            {
                MessageBox.Show("版本号有重复，请修改版本号");
                return;
            }
            updateProgress.Dispatcher.Invoke(new Action(() =>
            {
                updateProgress.Visibility = System.Windows.Visibility.Visible;
            }));

            AdvertManage.BLL.FileOperate fileUpdate = new AdvertManage.BLL.FileOperate();
            for (int i = 0; i < vm.FilePaths.Count; i++)
            {

                string p = vm.FilePaths[i];
                string fullpath = string.Format(@"{0}{1}", vm.DirPath, p);
                fileUpdate.UpdateFile(fullpath, p, (SeatManage.EnumType.SeatManageSubsystem)(int)vm.SystemType);

                updateProgress.Dispatcher.Invoke(new Action(() =>
                    { 
                        updateProgress.Value = ((double)i / (vm.FilePaths.Count - 1)) * 100;
                        lblProgress.Content = (int)updateProgress.Value + "%";
                    }));
            }

            updateProgress.Dispatcher.Invoke(new Action(() =>
                    {
                        updateProgress.Visibility = System.Windows.Visibility.Collapsed;
                        lblProgress.Content = "已经完成";
                    }));
            vm.SystemUpdate();
            MessageBox.Show("上传完成");
            if (HandlerOver != null)
            {
                HandlerOver(this, new EventArgs());
            }
        }


        private void systemInfo_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView myTreeView = sender as TreeView;

            FileNodeItem selectItem = myTreeView.SelectedItem as FileNodeItem;
            if (selectItem != null && selectItem.IsCanSelected)
            {
                selectItem.IsChecked = true;
            }
        }

    }
}
