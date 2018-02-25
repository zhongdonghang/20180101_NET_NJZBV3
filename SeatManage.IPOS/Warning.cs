using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeatManage.IPOS
{
    public enum WarningType
    {
        /// <summary>
        /// 询问
        /// </summary>
        Ask,
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 失败
        /// </summary>
        Filed
    }

    public partial class Warning : Form
    {
        WarningType frmType;
        public Warning(WarningType type)
        {
            InitializeComponent();
            frmType = type;

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

        bool returnValue = false;
        /// <summary>
        /// 返回值
        /// </summary>
        public bool ReturnValue
        {
            get { return returnValue; }
            set { returnValue = value; }
        }

        private StuInfo _cardNo;
        /// <summary>
        /// 卡号
        /// </summary>
        public StuInfo Student 
        {
            get { return _cardNo; }
            set { _cardNo = value; }
        } 

        private void btnYes_Click(object sender, EventArgs e)
        {
            ReturnValue = true;
            this.Close();
        }

        private void btnConcel_Click(object sender, EventArgs e)
        {
            ReturnValue = false;
            this.Close();
        }


        private void Warning_Load(object sender, EventArgs e)
        {
            switch (frmType)
            {
                case WarningType.Ask:
                    this.BackgroundImage = global::SeatManage.IPOS.Properties.Resources.img_bg3;
                    this.panel1.Visible = true;
                    DataLoad();
                    break;
                case WarningType.Success:
                    this.BackgroundImage = global::SeatManage.IPOS.Properties.Resources.img_bg4;
                    this.btnYes.BackgroundImage = global::SeatManage.IPOS.Properties.Resources.btn_keybord_ChooseSeat;
                    this.panel1.Visible = false ;
                    this.label2.Text = "卡片已激活，点击选座";
                    this.label7.Text = "直接进行选座操作。";
                    break;
                case WarningType.Filed:
                    this.BackgroundImage = global::SeatManage.IPOS.Properties.Resources.img_bg4;

                    this.panel1.Visible = false ;
                    this.label2.Text = "学号验证失败，";
                    this.label7.Text = "请输入正确的学号！";
                    this.label3.Visible = false;
                    break;
            }
        }

        void DataLoad()
        {
            if (Student != null)
            {
                label1.Text = "请确认个人信息：";
                label3.Text = "学号：" + Student.CardNo;
                if (!string.IsNullOrEmpty(Student.Name))
                {
                    label4.Text = "姓名：" + Student.Name;
                }
                else
                {
                    label4.Text = "";
                }
                if (!string.IsNullOrEmpty(Student.Org))
                {
                    label5.Text = "院系：" + Student.Org;
                }
                else
                {
                    label5.Text = "";
                }
                if (!string.IsNullOrEmpty(Student.Rank))
                {
                    label6.Text = "类别：" + Student.Rank;
                }
                else
                {
                    label6.Text = "";
                }
            }
            else
            {
                label3.Text = "   获取信息失败，";
                label4.Text = "请确认学号是否正确！";
                label5.Text = "";
                label6.Text = "";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
