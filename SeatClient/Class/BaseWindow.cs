﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatClient.Class
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
