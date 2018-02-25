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

namespace AdvertManageTools.EditPage
{
    /// <summary>
    /// ProgramUpgradeEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProgramUpgradeEditWindow : Window
    {
        public ProgramUpgradeEditWindow()
        {
            InitializeComponent();
            program.HandlerOver += new EventHandler(program_HandlerOver);
        }

        void program_HandlerOver(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {

                this.Close();
            }));
        }
    }
}
