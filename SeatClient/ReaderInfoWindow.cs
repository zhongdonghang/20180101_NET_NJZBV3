using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatClient.OperateResult;

namespace SeatClient
{
    public partial class ReaderInfoWindow : Form
    {
        public ReaderInfoWindow()
        {
            InitializeComponent();
            lblTitleAd.Text = clientobject.ClientSetting.TitleAd;
        }
        SeatClient.OperateResult.SystemObject clientobject = SystemObject.GetInstance();
        private SeatManage.ClassModel.ReaderInfo read = new SeatManage.ClassModel.ReaderInfo();
        /// <summary>
        /// 读者信息
        /// </summary>
        public SeatManage.ClassModel.ReaderInfo Read
        {
            get { return read; }
            set { read = value; }
        }
        private void ReaderInfoWindow_Load(object sender, EventArgs e)
        {
            label4.Text = read.CardNo;
            label5.Text = read.Name;
            if (read.Sex.Equals("1"))
            {
                label6.Text = "男";
            }
            else
            {
                label6.Text = "女";
            }
            
        }
    }
}
