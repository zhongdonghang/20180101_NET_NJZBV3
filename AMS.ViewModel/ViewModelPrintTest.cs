using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Drawing.Printing;

namespace AMS.ViewModel
{
    public class ViewModelPrintTest : ViewModelObject
    {
        public ViewModelPrintTest(SeatManage.EnumType.SeatManageSubsystem printtype, string template)
        {
            type = printtype;
            printText = template;
        }
        private SeatManage.EnumType.SeatManageSubsystem type;
        private XmlDocument newDoc;
        private string printText;

        /// <summary>
        /// 根据模板打印
        /// </summary>
        public string Print()
        {
            try
            {
                // PrintTemplate ="";
                newDoc = new XmlDocument();//原模板
                if (type == SeatManage.EnumType.SeatManageSubsystem.SeatSlip)
                {
                    newDoc.LoadXml(GetTestTemplate(printText));
                }
                else
                {
                    newDoc.LoadXml(printText);
                }
                PrintDocument pd = new PrintDocument();//打印操作的对象
                pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);//设置边距
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                pd.Print();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
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
                            string imgUrl = AppDomain.CurrentDomain.BaseDirectory + "\\TestTemp\\" + type.ToString() + "\\" + nodes[i].InnerText;
                            AMS.ServiceProxy.FileOperate download = new ServiceProxy.FileOperate();
                            download.FileDownLoad(imgUrl, nodes[i].InnerText, type);
                            Image img = imageLocation(imgUrl);
                            int width = Convert.ToInt32(nodes[i].Attributes["width"].Value.ToString().Split('.')[0]);
                            int height = Convert.ToInt32(nodes[i].Attributes["height"].Value.ToString().Split('.')[0]);
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
        private string GetTestTemplate(string temp)
        {
            XmlDocument defaultdoc = new XmlDocument();//原模板
            defaultdoc.LoadXml(GetTestTemp());
            XmlNodeList defaultnodes = defaultdoc.SelectSingleNode("//Print").ChildNodes;

            XmlDocument advertdoc = new XmlDocument();//广告模板
            advertdoc.LoadXml(temp);
            XmlNodeList advertnodes = advertdoc.SelectSingleNode("//Print").ChildNodes;

            StringBuilder newTemplate = new StringBuilder();
            newTemplate.Append("<?xml version='1.0' encoding='utf-8'?>");
            newTemplate.Append("<Print>");
            foreach (XmlNode item in defaultnodes)
            {
                newTemplate.Append(item.OuterXml);
            }
            foreach (XmlNode item in advertnodes)
            {
                newTemplate.Append(item.OuterXml);
            }
            newTemplate.Append("</Print>");
            return newTemplate.ToString();
        }
        private string GetTestTemp()
        {
            StringBuilder template = new StringBuilder();
            template.Append("<?xml version='1.0' encoding='utf-8'?>");
            template.Append("<Print>");
            template.Append("<Content font='黑体' size='14' bold='Y'>    007</Content>");
            template.Append("<Content font='宋体' size='8' bold='N'>阅览室名称：一楼研究生自修室</Content>");
            template.Append("<Content font='宋体' size='8' bold='N'>学号：208804213</Content>");
            template.Append("<Content font='宋体' size='8' bold='N'>姓名：张三</Content>");
            template.Append("<Content font='宋体' size='7' bold='N'>日期:2014-1-1 8:00:00</Content>");
            template.Append("<Content font='宋体' size='7' bold='Y'>离开请刷卡</Content>");
            template.Append("</Print>");
            return template.ToString();
        }
    }
}
