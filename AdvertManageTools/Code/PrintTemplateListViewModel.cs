using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

namespace AdvertManageTools.Code
{
    public class PrintTemplateListViewModel
    {
        ObservableCollection<PrintTemplateViewModel> _PrintList = new ObservableCollection<PrintTemplateViewModel>();
        /// <summary>
        /// 打印模板类
        /// </summary>
        public ObservableCollection<PrintTemplateViewModel> PrintList
        {
            get { return _PrintList; }
            set { _PrintList = value; Changed("PrintList"); }
        }
        /// <summary>
        /// 数据改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void GetData()
        {
            try
            {
                List<AdvertManage.Model.AMS_PrintTemplateModel> list = AdvertManage.BLL.AMS_PrintTemplateBLL.GetPrintTemplateList();
                _PrintList.Clear();
                foreach (AdvertManage.Model.AMS_PrintTemplateModel info in list)
                {
                    PrintTemplateViewModel vm = new PrintTemplateViewModel();
                    vm.Id = info.Id;
                    vm.Describe = info.Describe;
                    vm.EndTime = info.EndDate;
                    vm.StartTime = info.EffectDate;
                    vm.Template.Clear();
                    vm.Template.Append(info.Template);
                    _PrintList.Add(vm);
                }
            }
            catch
            {
                throw;
            }
        }
    }
    public class PrintTemplateViewModel
    {
        public bool DownLoad(string downloadpath)
        {
            try
            {
                downloadpath = downloadpath + "\\PrintTemplate_" + AdvertManage.BLL.ServerDateTime.Now.Value.ToShortDateString();
                DirectoryInfo dir = new DirectoryInfo(downloadpath);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                else
                {
                    throw new Exception("存在相同的文件夹，请重新选择目录！");
                }
                //创建一个xml对象
                XmlDocument xmlDoc = new XmlDocument();
                //创建开头
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(dec);
                //创建根节点
                XmlElement root = xmlDoc.CreateElement("Root");
                //创建二级节点
                XmlElement SecNode = xmlDoc.CreateElement("PrintTemplate");
                SecNode.SetAttribute("Template", _Template.ToString());
                SecNode.SetAttribute("StartTime", _StartTime.ToShortDateString());
                SecNode.SetAttribute("EndTime", _EndTime.ToShortDateString());
                SecNode.SetAttribute("Describe", _Describe);
                //下载图片
                List<string> imagepath = GetImagesName(_Template.ToString());
                //下载打印模版中的图片
                AdvertManage.BLL.FileOperate fileOperate = new AdvertManage.BLL.FileOperate();
                for (int i = 0; i < imagepath.Count; i++)
                {
                    string fileFullName = string.Format(@"{0}{1}", downloadpath+"\\", imagepath[i]);
                    if (!File.Exists(fileFullName))//文件不存在，则下载。
                    {
                        if (!fileOperate.FileDownLoad(fileFullName, imagepath[i], SeatManage.EnumType.SeatManageSubsystem.SeatSlip))
                        {
                            return false;//下载失败，返回false；
                        }
                    }
                }

                root.AppendChild(SecNode);
                //在根节点中添加二级节点
                root.AppendChild(SecNode);
                //添加根节点
                xmlDoc.AppendChild(root);
                //写入XML
                string xmlpath = downloadpath + "\\TemplateInfo.xml";
                //写入文件
                FileStream fs = new FileStream(xmlpath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(xmlDoc.OuterXml);
                sw.Close();
                fs.Close();


                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
        public PrintTemplateViewModel()
        {
            _Template.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            _Template.AppendLine("<Print>");
            _Template.AppendLine("<Content font=\"宋体\" size=\"14\" bold=\"Y\" italic=\"N\">    #SeatNo#</Content>");
            _Template.AppendLine("<Content font=\"宋体\" size=\"7\" bold=\"N\" italic=\"N\">阅览室:#ReadingRoomName#</Content>");
            _Template.AppendLine("<Content font=\"宋体\" size=\"7\" bold=\"N\" italic=\"N\">学工号:#CardNo#</Content>");
            _Template.AppendLine("<Content font=\"宋体\" size=\"7\" bold=\"N\" italic=\"N\">姓名:#StuName#</Content>");
            _Template.AppendLine("<Content font=\"宋体\" size=\"7\" bold=\"N\" italic=\"N\">原读者:#SecCardNo#</Content>");
            _Template.AppendLine("<Content font=\"宋体\" size=\"7\" bold=\"N\" italic=\"N\">日期:#DateTime#</Content>");
            _Template.AppendLine("<Content font=\"宋体\" size=\"7\" bold=\"N\" italic=\"N\">等待结束:#EndDateTime#</Content>");
            _Template.AppendLine("<Content font=\"宋体\" size=\"7\" bold=\"Y\" italic=\"N\">离开请刷卡</Content>");
            _Template.AppendLine("<Content font=\"宋体\" size=\"7\" bold=\"Y\" italic=\"N\">关注微博:@juneberry官方微博</Content>");
            _Template.AppendLine("</Print>");
        }
        private BitmapImage _Image = new BitmapImage();
        /// <summary>
        /// 打印图片
        /// </summary>
        public BitmapImage Image
        {
            get { return _Image; }
            set { _Image = value; Changed("Image"); }
        }
        private int _Id = 0;
        /// <summary>
        /// 模板编号
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; Changed("Id"); }
        }

        private StringBuilder _Template = new StringBuilder();
        /// <summary>
        /// 模板内容
        /// </summary>
        public StringBuilder Template
        {
            get { return _Template; }
            set { _Template = value; Changed("Template"); }
        }
        private DateTime _StartTime = DateTime.Now;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; Changed("StartTime"); }
        }
        private DateTime _EndTime = DateTime.Now.AddMonths(1);
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; Changed("EndTime"); }
        }
        private string _Describe = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Describe
        {
            get { return _Describe; }
            set
            {
                _Describe = value;
                Changed("Describe");
            }
        }
        private int _IsUse = 1;
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsUse
        {
            get { return _IsUse; }
            set
            {
                _IsUse = value;
                Changed("IsUse");
            }
        }
        /// <summary>
        /// 数据改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// 获取打印模板里的图片名称
        /// </summary>
        /// <param name="slipTemplate"></param>
        /// <returns></returns>
        static List<string> GetImagesName(string slipTemplate)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(slipTemplate);
            XmlNodeList nodes = xmlDoc.SelectNodes("//Print/Pic");
            List<string> imagesName = new List<string>();
            foreach (XmlNode node in nodes)
            {
                imagesName.Add(node.InnerText);
            }
            return imagesName;
        }
    }
}
