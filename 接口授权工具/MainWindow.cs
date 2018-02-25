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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FunctionAuthorization f = new FunctionAuthorization();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebServiceAuthorization f = new WebServiceAuthorization();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InterfaceKeygen ik = new InterfaceKeygen();
            ik.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MediaPassword mp = new MediaPassword();
            mp.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClientAuthorization ca = new ClientAuthorization();
            ca.ShowDialog();
        }
    }
}
