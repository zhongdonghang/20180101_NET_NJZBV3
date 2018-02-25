using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SeatClient.Class;
using SeatClient.OperateResult;
using SeatManage.SeatManageComm;
using SeatManage.EnumType;

namespace SeatClient
{
    public partial class AppSkin : Form
    {
        HandleResult _InitializeState = HandleResult.Failed;

        public HandleResult InitializeState
        {
            get { return _InitializeState; } 
        }
        public AppSkin()
        {
            InitializeComponent(); 
            int i = Screen.PrimaryScreen.Bounds.Width;
            if (i == 1080)
            {
                this.Size =new Size( 1080,1000); 
                this.Location = new Point(0, 920);
                Point picBoxLocation =new Point ((1080-105)/2,170);
                loadingPictureBox.Location = picBoxLocation;
                Point messageBoxLocatiol = new Point((1080 - 527) / 2, 285);
                lblMessage.Location = messageBoxLocatiol;
            }
            else
            { 
                this.Size = new Size(1024,768);
                this.Location = new Point(0, 0);
            }
        }
        ApplicationInitialize appInit = new ApplicationInitialize();
        private void FrmError_Load(object sender, EventArgs e)
        {
            appInit.EventInitializeMessage += appInit_EventInitializeMessage;
            appInit.InitializeEnd += appInit_InitializeEnd;
            appInit.Run(); 
        }

        void appInit_InitializeEnd(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
               {
                   appInit.EventInitializeMessage -= appInit_EventInitializeMessage;
                   appInit.InitializeEnd -= appInit_InitializeEnd;
                   this.Close();
                   this.Dispose();
                   _InitializeState = HandleResult.Successed;
               }));
        }

        void appInit_EventInitializeMessage(string message)
        {
            try
            {
                this.Invoke(new Action(() =>
                    {
                        lblMessage.Text = message;
                    }));
            }
            catch
            { }
        }
         
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _InitializeState = HandleResult.Failed;
            this.Close();
        }

        private void AppSkin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (appInit.State == HandState.Loading)
                {
                    _InitializeState = HandleResult.Failed;
                    appInit.Dispose(true);
                }
            }
            catch (Exception ex)
            { }
        } 
         


    }
}