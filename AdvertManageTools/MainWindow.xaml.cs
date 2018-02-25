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

namespace AdvertManageTools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           // List<AdvertManage.Model.AMS_CommandListModel> list = AdvertManage.BLL.AMS_CommandBLL.GetCommandListBySchoolNum("101001");
        }

        private void tabControlschoolinfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                TabItem ti = e.AddedItems[0] as TabItem;
                if (ti != null)
                {
                    switch (ti.Name)
                    {
                        case "schooltab": schoolInfo.DataBinding(); break;
                        case "campustab": campusInfo.ComboxDataBinding(); campusInfo.DataBinding(); break;
                        case "devicetab": deviceInfo.ComboxBingding(); deviceInfo.DataBinding(); break;

                    }
                }
            }
        }

        private void tabControlmain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                TabItem ti = e.AddedItems[0] as TabItem;
                if (ti != null)
                {
                    switch (ti.Name)
                    { 
                        case "systemtap": systemVerInfo.DataBind(); break;
                        case "playerlisttap": playlistInfo.DataBinding(); break;
                        case "SCtap": slipCustomerInfo.DataBinding(); break;
                        case "hardAdtap": hardAdInfo.DataBinding(); break;
                        case "TitleAdtap": titleAdInfo.DataBinding(); break;
                        case "logtap": releaseLog.DataBind(); break;
                        case "printtap": printTemplateInfo.DataBinding(); break;
                    }
                }
            }
        }
    }
}
