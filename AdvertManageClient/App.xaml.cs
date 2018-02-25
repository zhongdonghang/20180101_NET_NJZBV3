using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace AdvertManageClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public AMS.Model.AMS_UserInfo LoginUser = new AMS.Model.AMS_UserInfo();
    }
}
