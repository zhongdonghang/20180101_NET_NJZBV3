﻿using System;
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

namespace ClientLeaveV2.MyUserControl
{
    /// <summary>
    /// UC_Tip_ContinueTimeAutoNoAgain.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_ContinueTimeAutoNoAgain : UserControl
    {
        public UC_Tip_ContinueTimeAutoNoAgain(ViewModel.UC_Tip_ViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
