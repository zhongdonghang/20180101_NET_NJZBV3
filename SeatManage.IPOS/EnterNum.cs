using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeatManage.IPOS
{
    public partial class EnterNum : Form
    {
        public EnterNum()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            if (Screen.PrimaryScreen.Bounds.Width == 1080)
            {
                Point location = new Point(240, 1242);
                this.Location = location;
            }
            else
            {
                Point location = new Point(214, 202);
                this.Location = location;
            }
        }

        private string strCardNo = "";
        /// <summary>
        /// 学号
        /// </summary>
        public string StrCardNo
        {
            get { return strCardNo; }
            set { strCardNo = value; }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtSeatNo.Text = txtSeatNo.Text + "1";
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtSeatNo.Text = txtSeatNo.Text + "2";
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtSeatNo.Text = txtSeatNo.Text + "3";
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtSeatNo.Text = txtSeatNo.Text + "4";
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtSeatNo.Text = txtSeatNo.Text + "5";
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtSeatNo.Text = txtSeatNo.Text + "6";
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtSeatNo.Text = txtSeatNo.Text + "7";
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtSeatNo.Text = txtSeatNo.Text + "8";
            timer1.Stop();
            timer1.Interval = 10000;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtSeatNo.Text = txtSeatNo.Text + "9";
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtSeatNo.Text = txtSeatNo.Text + "0";
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            strCardNo = "";
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            string seatNo = txtSeatNo.Text;
            if (seatNo.Length > 0)
            {
                txtSeatNo.Text = seatNo.Substring(0, seatNo.Length - 1);
            }
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            strCardNo = txtSeatNo.Text;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
