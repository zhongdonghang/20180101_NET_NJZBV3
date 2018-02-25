using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeatManage.Keyboards
{
    public delegate void EventHandlerSubmit(string keyCode);
    public partial class KeyBoard : Form
    {
        /// <summary>
        /// 确定
        /// </summary>
        public event EventHandlerSubmit Submit; 
        private string _ShowKeyContentMessage;
        /// <summary>
        /// 文本框的ToolTip要显示的内容
        /// </summary>
        public string ShowKeyContentMessage
        {
            get { return _ShowKeyContentMessage; }
            set
            {
                _ShowKeyContentMessage = value;
                toolTip1.SetToolTip(txtNumber, value);
                toolTip1.Show(value, txtNumber, 5000); 
            }
        }
         

        public string TitleContent
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public KeyBoard()
        {
            InitializeComponent();
            if (Screen.PrimaryScreen.Bounds.Width == 1080)
            {
                this.Location = new Point(208, 1242);
            }
            else
            {
                this.Location = new Point(214, 202 - 50);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string seatNo = txtNumber.Text;
            if (seatNo.Length > 0)
            {
                txtNumber.Text = seatNo.Substring(0, seatNo.Length - 1);
            }
             
        }
        public void ShowToolTip(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                toolTip1.SetToolTip(txtNumber, message);
                toolTip1.Show(message, txtNumber, 5000);
            }
        }

        public void Clear()
        {
            txtNumber.Text = "";
        }


        private void myKeyboard_MyKeyDown(string keyCode)
        {
            txtNumber.Text += keyCode;
        }

        private string _EnterCode = "";
        /// <summary>
        /// 输入的编号
        /// </summary>
        public string EnterCode
        {
            get { return _EnterCode; }
            set { _EnterCode = value; }
        }

        private void myKeyboard_MyEnter(object sender, EventArgs e)
        {
            EnterCode = txtNumber.Text;
            if (Submit != null)
            {
                Submit(txtNumber.Text);
            }
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            EnterCode = "";
            this.Close();
        }
    }
}
