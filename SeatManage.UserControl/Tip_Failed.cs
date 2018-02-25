using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatManage.EnumType;

namespace SeatManage
{
    public partial class Tip_Failed : UserControl
    {
        public Tip_Failed(TipType tipType)
        {
            InitializeComponent();
            switch (tipType)
            { 
                case TipType.SelectSeatResult:
                    label1.Text = "     对不起，选座失败，该座位正在被其他读者选择。";
                    break;
                case TipType.SeatUsing:
                    label1.Text = "      座位正在使用中";
                    break;
            }
        }
    }
}
