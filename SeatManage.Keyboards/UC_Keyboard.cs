using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeatManage.Keyboards
{
    public partial class UC_Keyboard : UserControl
    {
        /// <summary>
        /// 按键按下事件
        /// </summary>
        public event EventHandlerSubmit MyKeyDown;
    
        /// <summary>
        /// 确认
        /// </summary>
        public event EventHandler MyEnter;
        public UC_Keyboard()
        {
            InitializeComponent();
        }

        private void keyNum1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string keyName = btn.Name;
            string keyCode = keyName.Substring(keyName.Length - 1, 1);
            if (MyKeyDown != null)
            {
                MyKeyDown(keyCode);
            }
        }
         

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MyEnter != null)
            {
                MyEnter(sender, e);
            }
        }
 
    }
}
