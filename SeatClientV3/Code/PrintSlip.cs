using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Printing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Xml;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using FontStyle = System.Drawing.FontStyle;
using Timer = System.Windows.Forms.Timer;

namespace SeatClientV3.Code
{
    public delegate void PrinterStatusEventHandle(Printer printerStatus);
    /// <summary>
    /// 打印凭条
    /// </summary>
    public class PrintSlip
    {
        Thread printThread;
        LocalPrintServer printServer;
        Timer printerStatusTimer;
        /// <summary>
        /// 打印机异常
        /// </summary>
        public event PrinterStatusEventHandle PrinterException;
        /// <summary>
        /// 程序所在的路径
        /// </summary>
        public string DirectoryPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        private PrintSlip()
        {
            //PrintTemplate = GetPrintTemplate();
            printServer = new LocalPrintServer();
            printerStatusTimer = new Timer();
            printerStatusTimer.Interval = 2000;

            printerStatusTimer.Tick += printerStatusTimer_Tick;
        }

        void printerStatusTimer_Tick(object sender, EventArgs e)
        {
            Printer status = PrintStatusHandle();
            if (PrinterException != null)
            {
                PrinterException(status);
            }
        }

        static PrintSlip printer;
        static object _object = new object();
        private PrintData printData;
        public static PrintSlip GetInstance()
        {
            if (printer == null)
            {
                lock (_object)
                {
                    if (printer == null)
                    {
                        return printer = new PrintSlip();
                    }
                }
            }
            return printer;
        }
        private PrintStatus _PrintType;
        /// <summary>
        /// 凭条类型
        /// </summary>
        public PrintStatus PrintType
        {
            get { return _PrintType; }
            set { _PrintType = value; }
        }

        // private string strClassName = ConfigurationManager.AppSettings["RandCode"];
        /// <summary>
        /// 多线程打印
        /// </summary>
        /// <param name="printType"></param>
        /// <param name="data"></param>
        /// <param name="clientNo"></param>
        public void ThreadPrint(PrintStatus printType, PrintData data, string clientNo)
        {
            ThreadPrintType = printType;
            ThreadPrintData = data;
            ThreadClientNo = clientNo;
            printThread = new Thread(Print);
            printThread.Start();
        }
        PrintStatus ThreadPrintType;
        PrintData ThreadPrintData;
        string ThreadClientNo;
        /// <summary>
        /// 执行打印操作
        /// </summary>
        /// <param name="_PrintType"></param>
        /// <param name="data"></param>
        public void Print()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
             {
                 Printer printerStatus = PrintStatusHandle();
                 TerminalInfoV2 term = TerminalOperatorService.GetTeminalSetting(ThreadClientNo);
                 if (printerStatus == Printer.NoPaper)
                 {
                     if (term.PrinterStatus)
                     {
                         term.PrinterStatus = false;
                         term.LastPrintTimes = term.PrintedTimes;
                         term.PrintedTimes = 0;
                         TerminalOperatorService.UpdateTeminalSetting(term);
                     }
                     if (PrinterException != null)
                     {
                         PrinterException(Printer.NoPaper);
                     }
                     return;
                 }
                 if (printerStatus == Printer.Normal)
                 {
                     term.PrinterStatus = true;
                     term.DeviceSetting.IsAnyPaper = true;
                 }

                 _PrintType = ThreadPrintType;
                 printData = ThreadPrintData;
                 XmlDocument doc = new XmlDocument();//原模板
                 doc.LoadXml(GetPrintTemplate());
                 PrintDocument pd = new PrintDocument();//打印操作的对象
                 pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);//设置边距
                 switch (PrintType)
                 {
                     //根据凭条的类型，在模板前增加【预约】或【等待】字样的节点
                     case PrintStatus.General:
                         newDoc = doc;
                         break;
                     case PrintStatus.Book:
                         newDoc = InsertXmlNode(PrintStatus.Book, doc);
                         break;
                     case PrintStatus.Wait:
                         newDoc = InsertXmlNode(PrintStatus.Wait, doc);
                         break;
                 }
                 pd.PrintPage += pd_PrintPage;
                 pd.Print();
                 term.PrintedTimes++;
                 TerminalOperatorService.UpdateTeminalSetting(term);
             }));
        }
        XmlDocument newDoc;//最终的模板
        private string PrintTemplate = "";
        /// <summary>
        /// 执行打印操作
        /// </summary>
        /// <param name="_PrintType"></param>
        /// <param name="data"></param>
        public void Print(PrintStatus printType, PrintData data, string clientNo)
        {
            Printer printerStatus = PrintStatusHandle();
            TerminalInfoV2 term = TerminalOperatorService.GetTeminalSetting(clientNo);
            if (printerStatus == Printer.NoPaper)
            {
                if (term.PrinterStatus)
                {
                    term.PrinterStatus = false;
                    term.LastPrintTimes = term.PrintedTimes;
                    term.PrintedTimes = 0;
                    TerminalOperatorService.UpdateTeminalSetting(term);
                }
                if (PrinterException != null)
                {
                    PrinterException(Printer.NoPaper);
                }
                return;
            }
            if (printerStatus == Printer.Normal)
            {
                term.PrinterStatus = true;
                term.DeviceSetting.IsAnyPaper = true;
            }

            _PrintType = printType;
            printData = data;
            XmlDocument doc = new XmlDocument();//原模板
            doc.LoadXml(GetPrintTemplate());
            PrintDocument pd = new PrintDocument();//打印操作的对象
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);//设置边距
            switch (PrintType)
            {
                //根据凭条的类型，在模板前增加【预约】或【等待】字样的节点
                case PrintStatus.General:
                    newDoc = doc;
                    break;
                case PrintStatus.Book:
                    newDoc = InsertXmlNode(PrintStatus.Book, doc);
                    break;
                case PrintStatus.Wait:
                    newDoc = InsertXmlNode(PrintStatus.Wait, doc);
                    break;
            }
            pd.PrintPage += pd_PrintPage;
            pd.Print();
            term.PrintedTimes++;
            TerminalOperatorService.UpdateTeminalSetting(term);
        }

        //int i = 0;//记录打印行数
        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPosition = 0;//字符串纵向位置

            string line = "";
            Font font;//字体 
            string strFont;//字体名
            string strSize;//字体大小
            XmlNodeList nodes = newDoc.SelectSingleNode("//Print").ChildNodes;//获取文字内容

            Regex reg = new Regex("#.[a-z0-9A-Z]*#");
            MatchCollection mc;//正则表达式验证结果
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
                        mc = reg.Matches(line);
                        if (mc.Count > 0)
                        {
                            string strMarkValue = PrintTemplateMark(mc[0].Value, _PrintType);
                            if (strMarkValue == null)
                            {
                                continue;
                            }
                            line = line.Replace(mc[0].Value, strMarkValue);
                        }
                        e.Graphics.DrawString(line, font, Brushes.Black, 0, yPosition);
                        yPosition = yPosition + font.GetHeight();  //判断上一行的高度

                        break;
                    case "Pic":
                        string imgName = nodes[i].InnerText;
                        Image img = imageLocation(imgName);
                        int width = int.Parse((double.Parse(nodes[i].Attributes["width"].Value).ToString().Split('.')[0]));
                        int height = int.Parse((double.Parse(nodes[i].Attributes["height"].Value).ToString().Split('.')[0]));
                        e.Graphics.DrawImage(img, new Rectangle(0, (int)yPosition, width, height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                        img.Dispose();
                        yPosition = yPosition + height;  //判断上一行的高度
                        break;
                }
            }
        }

        /// <summary>
        /// 下载图片并且获取本地图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Image imageLocation(string imageName)
        {
            /**
             *  判断图片在临时文件夹中是否存在。如果存在则不下载。
             *  修改时间：2013-9-5 王随
             */
            string filePath = string.Format(@"{0}temp\{1}", DirectoryPath, imageName);
            if (!File.Exists(filePath))
            {//如果本地文件不存在，则下载
                FileOperate fileDownload = new FileOperate();
                if (!fileDownload.FileDownLoad(filePath, imageName, SeatManageSubsystem.PrintReceiptAd))
                {
                    return
                        null;
                }
            }
            Image img = Image.FromFile(filePath);
            return img;
        }

        /// <summary>
        /// 解析打印模板中的标签
        /// </summary>
        /// <param name="mark">标签</param>
        /// <returns></returns>
        string PrintTemplateMark(string mark, PrintStatus printType)
        {
            switch (mark)
            {
                case "#SecCardNo#":
                    //判断凭条是否为等待，是则打印原来的读者学号
                    if (printType == PrintStatus.Wait)
                    {
                        return printData.SecCardNo;
                    }
                    return null;
                case "#ReadingRoomName#":
                    return printData.ReadingRoomName;//阅览室名称
                case "#SeatNo#":
                    return printData.SeatNo;//座位号
                case "#StuName#":
                    return printData.ReaderName;//学生姓名
                case "#CardNo#":
                    return printData.CardNo;//学号
                case "#DateTime#":
                    return string.Format("{0}月{1}日 {2}", printData.EnterTime.Month, printData.EnterTime.Day, printData.EnterTime.ToLongTimeString());//printData.EnterTime.Month+.ToString().Replace('-', '/');//进入时间
                case "#EndDateTime#"://等待结束时间
                    if (printType == PrintStatus.Wait)
                    {
                        return string.Format("{0}月{1}日 {2}", printData.WaitEndDateTime.Month, printData.WaitEndDateTime.Day, printData.WaitEndDateTime.ToLongTimeString()); printData.WaitEndDateTime.ToString().Replace('-', '/');
                    }
                    return null;
                case "#RandNo#"://其他（随机码或者其他，需要在接口中实现）
                    throw new Exception("暂未实现");
                    break;
            }
            return "";
        }



        /// <summary>
        /// 根据凭条类型插入凭条头
        /// </summary>
        /// <param name="printType"></param>
        private XmlDocument InsertXmlNode(PrintStatus printType, XmlDocument doc)
        {
            XmlDocument tempDoc = doc;

            XmlNode root = tempDoc.SelectSingleNode("//Print");//找出根节点
            XmlNode tempNode = null;
            try
            {
                tempNode = tempDoc.SelectNodes("//Print/Content")[0];//找到节点第一项
            }
            catch
            {
                throw new Exception("打印凭条失败，解析模板错误:" + PrintTemplate);
            }
            if (tempNode != null)
            {
                XmlElement newNode = tempDoc.CreateElement("Content");//创建一个新的节点，并且把该节点插入到内容头
                switch (printType)
                {
                    case PrintStatus.Book:
                        newNode.InnerText = "预约";
                        break;
                    case PrintStatus.Wait:
                        newNode.InnerText = "等待";
                        break;
                }
                newNode.SetAttribute("font", "宋体");
                newNode.SetAttribute("size", "9");
                newNode.SetAttribute("bold", "Y");
                root.InsertBefore(newNode, tempNode);
            }
            else
            {
                throw new Exception("打印凭条失败，解析模板错误:" + PrintTemplate);
            }
            return tempDoc;
        }
        /// <summary>
        /// 获取打印模板
        /// </summary>
        /// <returns></returns>
        string GetPrintTemplate()
        {
            return T_SM_PrintTemplate.GetPrintTemplate(printData.CardNo);
        }

        /// <summary>
        /// 打印机状态
        /// </summary>
        /// <returns></returns>
        private Printer PrintStatusHandle()
        {

            printServer.Refresh();
            PrintQueue printQueue = printServer.DefaultPrintQueue;

            if (printQueue.NumberOfJobs >= 1)
            {
                printerStatusTimer.Start();
                return Printer.NoPaper;
            }
            printerStatusTimer.Stop();
            return Printer.Normal;
        }

    }
    /// <summary>
    /// 要打印的数据
    /// </summary>
    public struct PrintData
    {
        private string _SecCardNo;  //判断凭条是否为等待，是则打印原来的读者学号 
        /// <summary>
        /// 第二个读者的学号
        /// </summary>
        public string SecCardNo
        {
            get { return _SecCardNo; }
            set { _SecCardNo = value; }
        }
        private string _ReadingRoomName; //阅览室名称 
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; }
        }
        private string _SeatNo;//座位号
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get { return _SeatNo; }
            set { _SeatNo = value; }
        }
        private string _StuName; //学生姓名
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string ReaderName
        {
            get { return _StuName; }
            set { _StuName = value; }
        }
        private string _CardNo; //学号
        /// <summary>
        /// 学号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }
        private DateTime _DateTime; //进入时间
        /// <summary>
        /// 进入时间
        /// </summary>
        public DateTime EnterTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }
        private DateTime _EndDateTime; //等待结束时间 
        /// <summary>
        /// 等待结束时间 
        /// </summary>
        public DateTime WaitEndDateTime
        {
            get { return _EndDateTime; }
            set { _EndDateTime = value; }
        }
        private string _RandNo; //其他（随机码或者其他，需要在接口中实现）
        /// <summary>
        /// 随机码或者其他，需要在接口中实现
        /// </summary>
        public string RandNo
        {
            get { return _RandNo; }
            set { _RandNo = value; }
        }

    }
    public enum PrintStatus
    {
        /// <summary>
        /// 一般选座凭条
        /// </summary>
        General,
        /// <summary>
        /// 预约凭条
        /// </summary>
        Book,
        /// <summary>
        /// 等待座位凭条
        /// </summary>
        Wait

    }
}
