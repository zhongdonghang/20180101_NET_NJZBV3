using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatClientV2.Code
{
    public class BaseWindow
    {
         /// <summary>
         /// 程序所在的路径
         /// </summary>
        public string DirectoryPath
        { 
            get { return AppDomain.CurrentDomain.BaseDirectory; } 
        }

    }
}
