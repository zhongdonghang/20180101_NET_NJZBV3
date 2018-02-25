using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 接口授权工具
{
    public partial class ClientAuthorization : Form
    {
        public ClientAuthorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SystemAuthorizedkeys
            string sPath = string.Format(@"{0}SystemAuthorized\", AppDomain.CurrentDomain.BaseDirectory);
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            SeatManage.ClassModel.SystemAuthorization model = new SeatManage.ClassModel.SystemAuthorization();
            model.SchoolNum = textBox1.Text;
            model.SeatClientCount = int.Parse(textBox2.Text);
            for (int i = 0; i < model.SeatClientCount; i++)
            {
                model.SeatClientList.Add(model.SchoolNum + (i + 1).ToString("D2"), DateTime.Now);
            }
            string filePath = string.Format(@"{0}SystemAuthorizedkeys", sPath);
            StreamWriter file = new StreamWriter(filePath, false, Encoding.ASCII);
            try
            {
                string strJson = SeatManage.SeatManageComm.JSONSerializer.Serialize(model);
                string ciphertext = SeatManage.SeatManageComm.AESAlgorithm.AESEncrypt(strJson);
                file.WriteLine(ciphertext);
                MessageBox.Show("生成完成！");
                System.Diagnostics.Process.Start("Explorer.exe", sPath);
            }
            catch
            {
                MessageBox.Show("生成失败！");
            }
            finally
            {
                file.Flush();
            }
        }
    }
}
