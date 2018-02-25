using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Drawing.Printing;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;
using SeatManage.ClassModel;
using System.Printing;
namespace AMS.MediaPlayer.UI.Code
{
    public delegate void PrinterStatusEventHandle(SeatManage.EnumType.Printer printerStatus);
    /// <summary>
    /// 打印凭条
    /// </summary>
    public class PrintSlip
    {
        //string HttpUrl = ConfigurationManager.ConnectionStrings["HttpUrl"].ConnectionString;
        System.Printing.LocalPrintServer printServer = null;
        System.Windows.Forms.Timer printerStatusTimer = null;
        /// <summary>
        /// 打印机异常
        /// </summary>
        public event PrinterStatusEventHandle PrinterException;

        public PrintSlip(string printTemplate)
        {
            printServer = new System.Printing.LocalPrintServer();
            printerStatusTimer = new System.Windows.Forms.Timer();
            printerStatusTimer.Interval = 2000;

            printerStatusTimer.Tick += new EventHandler(printerStatusTimer_Tick);
            this.xmlPrintTemplate = printTemplate;
        }
        void printerStatusTimer_Tick(object sender, EventArgs e)
        {
            SeatManage.EnumType.Printer status = PrintStatusHandle();
            if (PrinterException != null)
            {
                PrinterException(status);
            }
        }
        private string xmlPrintTemplate = "";
        /// <summary>
        /// 打印模板
        /// </summary>
        public string PrintTemplate
        {
            get { return xmlPrintTemplate; }
            set { xmlPrintTemplate = value; }
        }



        XmlDocument newDoc = null;//最终的模板

        /// <summary>
        /// 根据模板打印
        /// </summary>
        public void Print()
        {

            // PrintTemplate ="";
            newDoc = new XmlDocument();//原模板
            newDoc.LoadXml(PrintTemplate);
            PrintDocument pd = new PrintDocument();//打印操作的对象
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);//设置边距

            SeatManage.EnumType.Printer printerStatus = PrintStatusHandle();
            TerminalInfoV2 term = SeatManage.Bll.TerminalOperatorService.GetTeminalSetting(MediaPlayer.Code.PlayerSetting.DeviceNo);
            if (printerStatus == SeatManage.EnumType.Printer.NoPaper)
            {
                if (term.PrinterStatus == true)
                {
                    term.PrinterStatus = false;
                    term.LastPrintTimes = term.PrintedTimes;
                    term.PrintedTimes = 0;
                    SeatManage.Bll.TerminalOperatorService.UpdateTeminalSetting(term);
                }
                if (PrinterException != null)
                {
                    PrinterException(SeatManage.EnumType.Printer.NoPaper);
                }
                return;
            }
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.Print();
            if (term != null)
            {
                if (term.PrinterStatus == false)
                {
                    term.PrinterStatus = true;
                }
                term.PrintedTimes++;
                SeatManage.Bll.TerminalOperatorService.UpdateTeminalSetting(term);
            }
        }

        //int i = 0;//记录打印行数
        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                float yPosition = 0;//字符串纵向位置
                float leftMargin = e.MarginBounds.Left;
                float topMargin = e.MarginBounds.Top;
                string line = "";
                Font font;//字体 
                string strFont;//字体名
                string strSize;//字体大小
                XmlNodeList nodes = newDoc.SelectSingleNode("//Print").ChildNodes;//获取文字内容

                for (int i = 0; i < nodes.Count; i++)
                {
                    switch (nodes[i].LocalName)
                    {
                        case "Content":
                            strFont = nodes[i].Attributes["font"].Value;
                            strSize = nodes[i].Attributes["size"].Value;
                            if (nodes[i].Attributes["italic"] != null && nodes[i].Attributes["bold"] != null && nodes[i].Attributes["bold"].Value == "Y" && nodes[i].Attributes["italic"].Value == "Y")
                            {
                                font = new Font(strFont, float.Parse(strSize), FontStyle.Bold | FontStyle.Italic);
                            }
                            else if (nodes[i].Attributes["bold"] != null && nodes[i].Attributes["bold"].Value == "Y")
                            {
                                font = new Font(strFont, float.Parse(strSize), FontStyle.Bold);
                            }
                            else if (nodes[i].Attributes["italic"] != null && nodes[i].Attributes["italic"].Value == "Y")
                            {
                                font = new Font(strFont, float.Parse(strSize), FontStyle.Italic);
                            }
                            else
                            {
                                font = new Font(strFont, float.Parse(strSize));
                            }

                            //替换打印内容中的标签
                            line = nodes[i].InnerText;
                            e.Graphics.DrawString(line, font, Brushes.Black, 0, yPosition);
                            yPosition = yPosition + font.GetHeight();  //判断上一行的高度

                            break;
                        case "Pic":
                            string imgUrl = AMS.MediaPlayer.Code.PlayerSetting.SysPath + "\\CouponsImage\\" + nodes[i].InnerText;
                            Image img = imageLocation(imgUrl);
                            int width = int.Parse((double.Parse(nodes[i].Attributes["width"].Value).ToString().Split('.')[0]).ToString());
                            int height = int.Parse((double.Parse(nodes[i].Attributes["height"].Value).ToString().Split('.')[0]).ToString());
                            e.Graphics.DrawImage(img, new Rectangle(0, (int)yPosition, width, height), 0, 0, img.Width, img.Height, System.Drawing.GraphicsUnit.Pixel);
                            img.Dispose();
                            yPosition = yPosition + height;  //判断上一行的高度
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
            }

        }

        /// <summary>
        /// 下载图片并且获取本地图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Image imageLocation(string url)
        {
            BinaryReader binReader = new BinaryReader(File.Open(url, FileMode.Open));
            FileInfo fileInfo = new FileInfo(url);
            byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
            binReader.Close();
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            Image img = Image.FromStream(ms);
            ms.Close();
            return img;
        }
        /// <summary>
        /// 打印机状态
        /// </summary>
        /// <returns></returns>
        private SeatManage.EnumType.Printer PrintStatusHandle()
        {

            printServer.Refresh();
            PrintQueue printQueue = printServer.DefaultPrintQueue;

            if (printQueue.NumberOfJobs >= 1)
            {
                printerStatusTimer.Start();
                return SeatManage.EnumType.Printer.NoPaper;
            }
            else
            {
                printerStatusTimer.Stop();
                return SeatManage.EnumType.Printer.Normal;
            }
        }
    }
}
