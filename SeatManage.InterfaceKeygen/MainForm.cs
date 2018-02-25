using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeatManage.InterfaceKeygen
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string interfacekey = "";
                if (radioButton1.Checked)
                {
                    interfacekey = "JuneberryReadingRoomInterfaceKey";
                }
                else if (radioButton2.Checked)
                {
                    interfacekey = "JuneberryAccessInterfaceKey";
                }
                else if (radioButton3.Checked)
                {
                    interfacekey = "JuneberryMediaReleaseKey";
                }
                string pass = SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(new List<string>() { textBox1.Text, interfacekey });
                textBox2.Text = pass.Substring(0, 8) + "-" + pass.Substring(8, 8) + "-" + pass.Substring(16, 8) + "-" + pass.Substring(24, 8);
            }
            else
            {
                MessageBox.Show("学校编号不能为空！");
            }
        }
    }
}
