using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatClient.Updater.Code;
using System.Threading;

namespace SeatClient.Updater
{
    public partial class Form1 : Form
    {

        NetWork check = new NetWork();
        public Form1()
        {
            InitializeComponent();
            int i = Screen.PrimaryScreen.Bounds.Width;
            if (i == 1080)
            {
                this.Size = new Size(1080, 1920);
                this.Location = new Point(0, 0);
                panel1.Location = new Point((1080 - 530) / 2, 1180);
                panel2.Location = new Point((1080 - 530) / 2, 1180);
            }
            else
            {
                this.Size = new Size(1024, 768);
                this.Location = new Point(0, 0);
            }
            check.EventInitializeMessage += new Code.Message(check_EventInitializeMessage);
            check.InitializeEnd += new EventHandler(check_InitializeEnd);

            panel2.Visible = true;
            panel1.Visible = false;
        }
        Thread myThread = null;
        void check_InitializeEnd(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                panel2.Visible = false;
                panel1.Visible = true;
            }));
            UpdateOperate SeatClientUpdate = new UpdateOperate();
            SeatClientUpdate.DownloadFile.HandleProgress += new SeatManage.Bll.EventHandleFileTransport(DownloadFile_HandleProgress);
            SeatClientUpdate.Downloaded += DownloadFile_Downloaded;
            SeatClientUpdate.HandlerError += new EventHandlerUpdateMessage(SeatClientUpdate_HandlerError);
            myThread = new Thread(new ThreadStart(SeatClientUpdate.DownloadUpdateFiles));
            myThread.Start();
        }
        bool isError = false;
        void SeatClientUpdate_HandlerError(string message)
        {
            isError = true;
            this.Invoke(new Action(() =>
            {
                panel2.Visible = false;
                panel1.Visible = false;
                panel3.Visible = true;
                lblErrorMessage.ForeColor = Color.Red;
                lblErrorMessage.Text = string.Format("{0}{1}\r", lblErrorMessage.Text, message);
            }));
        }

        void check_EventInitializeMessage(string message)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    label2.Text = message;

                }));
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            check.Run();
        }

        void DownloadFile_Downloaded(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                if (!isError)
                {
                    this.Close();
                    this.Dispose();
                }
            }));
        }

        void DownloadFile_HandleProgress(int message)
        {
            this.Invoke(new Action(() =>
            {
                lblMessage.Text = "正在下载终端程序…";
                progressBar1.Value = message;
            }));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                check.Dispose(true);
                // myThread.Abort();
            }
            catch (Exception ex)
            {

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
