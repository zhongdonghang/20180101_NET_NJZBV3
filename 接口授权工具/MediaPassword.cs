using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 接口授权工具
{
    public partial class MediaPassword : Form
    {
        public MediaPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string schoolNo = textBox2.Text;
            DateTime dt = DateTime.Now;
            string cps = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(schoolNo);
            string dts = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32((dt.Year + dt.Month + dt.Day + dt.Hour).ToString());
            string pw = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32("Juneberry" + cps + dts);
            textBox1.Text = pw;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Text = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(textBox3.Text);
        }
    }
}
