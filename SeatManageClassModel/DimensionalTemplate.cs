using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    public class DimensionalTemplate
    {
        private double _Height = 0;
        /// <summary>
        /// 模板高度
        /// </summary>
        public double Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        private double _Width = 0;
        /// <summary>
        /// 模板宽度
        /// </summary>
        public double Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        private double _PrintHeight = 0;
        /// <summary>
        /// 打印实际高度
        /// </summary>
        public double PrintHeight
        {
            get { return _PrintHeight; }
            set { _PrintHeight = value; }
        }
        private double _PrintWidth = 0;
        /// <summary>
        /// 打印实际宽度
        /// </summary>
        public double PrintWidth
        {
            get { return _PrintWidth; }
            set { _PrintWidth = value; }
        }
        private int _DPI = 300;
        /// <summary>
        /// DPI
        /// </summary>
        public int DPI
        {
            get { return _DPI; }
            set { _DPI = value; }
        }
        private int _SeatCodeCount = 0;
        /// <summary>
        /// 二维码数目
        /// </summary>
        public int SeatCodeCount
        {
            get { return _SeatCodeCount; }
            set { _SeatCodeCount = value; }
        }
        private string _Name = "";
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        List<DimensionalElement> _ElementList = new List<DimensionalElement>();
        /// <summary>
        /// 元素列表
        /// </summary>
        public List<DimensionalElement> ElementList
        {
            get { return _ElementList; }
            set { _ElementList = value; }
        }
        List<string> _ImageFiles = new List<string>();
        /// <summary>
        /// 图片路径
        /// </summary>
        public List<string> ImageFiles
        {
            get { return _ImageFiles; }
            set { _ImageFiles = value; }
        }
        /// <summary>
        /// 返回当前模板的XML
        /// </summary>
        /// <returns></returns>
        public string ToXml()
        {
            return ToXml(this);
        }
        /// <summary>
        /// 将模板转换为XML
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ToXml(DimensionalTemplate model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            root.SetAttribute("DPI", model.DPI.ToString());
            root.SetAttribute("Height", model.Height.ToString());
            root.SetAttribute("Width", model.Width.ToString());
            root.SetAttribute("Name", model.Name);
            root.SetAttribute("PrintHeight", model.PrintHeight.ToString());
            root.SetAttribute("PrintWidth", model.PrintWidth.ToString());
            root.SetAttribute("SeatCodeCount", model.SeatCodeCount.ToString());

            //包含的元素
            XmlElement ElementNode = xmlDoc.CreateElement("ElementList");
            foreach (DimensionalElement element in model.ElementList)
            {
                XmlElement ThrNode = xmlDoc.CreateElement("Element");
                ThrNode.SetAttribute("Alignment", element.Alignment.ToString());
                ThrNode.SetAttribute("Angle", element.Angle.ToString());
                ThrNode.SetAttribute("FontColor", element.FontColor);
                ThrNode.SetAttribute("FontSize", element.FontSize.ToString());
                ThrNode.SetAttribute("Height", element.Height.ToString());
                ThrNode.SetAttribute("Width", element.Width.ToString());
                ThrNode.SetAttribute("ImageFile", element.ImageFile);
                ThrNode.SetAttribute("Order", element.Order.ToString());
                ThrNode.SetAttribute("PosintionX", element.PosintionX.ToString());
                ThrNode.SetAttribute("PosintionY", element.PosintionY.ToString());
                ThrNode.SetAttribute("Text", element.Text);
                ThrNode.SetAttribute("Type", element.Type.ToString());
                ElementNode.AppendChild(ThrNode);
            }
            root.AppendChild(ElementNode);

            //模板包含的文件
            XmlElement FileNode = xmlDoc.CreateElement("ImageList");
            foreach (string file in model.ImageFiles)
            {
                XmlElement ThrNode = xmlDoc.CreateElement("Image");
                ThrNode.SetAttribute("Path", file);
                FileNode.AppendChild(ThrNode);
            }
            root.AppendChild(FileNode);

            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static DimensionalTemplate Parse(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            DimensionalTemplate model = new DimensionalTemplate();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                model.DPI = int.Parse(node.Attributes["DPI"].Value);
                model.Height = double.Parse(node.Attributes["Height"].Value);
                model.Width = double.Parse(node.Attributes["Width"].Value);
                model.Name = node.Attributes["Name"].Value;
                model.PrintHeight = double.Parse(node.Attributes["PrintHeight"].Value);
                model.PrintWidth = double.Parse(node.Attributes["PrintWidth"].Value);
                model.SeatCodeCount = int.Parse(node.Attributes["SeatCodeCount"].Value);
                //转换元素
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/ElementList/Element");
                foreach (XmlNode ItemNode in nodes)
                {
                    DimensionalElement element = new DimensionalElement();
                    element.Alignment = (ElementTextAlignment)System.Enum.Parse(typeof(ElementTextAlignment), ItemNode.Attributes["Alignment"].Value);
                    element.Angle = double.Parse(ItemNode.Attributes["Angle"].Value);
                    element.FontColor = ItemNode.Attributes["FontColor"].Value;
                    element.FontSize = int.Parse(ItemNode.Attributes["FontSize"].Value);
                    element.Height = double.Parse(ItemNode.Attributes["Height"].Value);
                    element.Width = double.Parse(ItemNode.Attributes["Width"].Value);
                    element.PosintionX = double.Parse(ItemNode.Attributes["PosintionX"].Value);
                    element.PosintionY = double.Parse(ItemNode.Attributes["PosintionY"].Value);
                    element.ImageFile = ItemNode.Attributes["ImageFile"].Value;
                    element.Order = int.Parse(ItemNode.Attributes["Order"].Value);
                    element.Text = ItemNode.Attributes["Text"].Value;
                    element.Type = (DimensionalElementTye)System.Enum.Parse(typeof(DimensionalElementTye), ItemNode.Attributes["Type"].Value);
                    model.ElementList.Add(element);
                }
                //转换图片地址
                nodes = xmlDoc.SelectNodes("//Root/ImageList/Image");
                foreach (XmlNode ItemNode in nodes)
                {
                    model.ImageFiles.Add(ItemNode.Attributes["Path"].Value);
                }

                return model;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    /// <summary>
    /// 二维码内部元素
    /// </summary>
    public class DimensionalElement
    {
        private double _PosintionX = 0;
        /// <summary>
        /// X坐标
        /// </summary>
        public double PosintionX
        {
            get { return _PosintionX; }
            set { _PosintionX = value; }
        }
        private double _PosintionY = 0;
        /// <summary>
        /// Y坐标
        /// </summary>
        public double PosintionY
        {
            get { return _PosintionY; }
            set { _PosintionY = value; }
        }
        private double _Height = 0;
        /// <summary>
        /// 高度
        /// </summary>
        public double Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        private double _Width = 0;
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        private double _Angle = 0;
        /// <summary>
        /// 角度
        /// </summary>
        public double Angle
        {
            get { return _Angle; }
            set { _Angle = value; }
        }
        private DimensionalElementTye _Type = DimensionalElementTye.None;
        /// <summary>
        /// 元素类型
        /// </summary>
        public DimensionalElementTye Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private int _Order = 1;
        /// <summary>
        /// 排序号
        /// </summary>
        public int Order
        {
            get { return _Order; }
            set { _Order = value; }
        }
        private string _ImageFile = "";
        /// <summary>
        /// 图片文件
        /// </summary>
        public string ImageFile
        {
            get { return _ImageFile; }
            set { _ImageFile = value; }
        }
        private string _Text;
        /// <summary>
        /// 文本信息
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        private string _FontColor = "#FFFFFFFF";
        /// <summary>
        /// 文字颜色
        /// </summary>
        public string FontColor
        {
            get { return _FontColor; }
            set { _FontColor = value; }
        }
        private int _FontSize = 32;
        /// <summary>
        /// 文字尺寸
        /// </summary>
        public int FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }
        private ElementTextAlignment _Alignment = ElementTextAlignment.Left;
        /// <summary>
        /// 文本对齐方式
        /// </summary>
        public ElementTextAlignment Alignment
        {
            get { return _Alignment; }
            set { _Alignment = value; }
        }
    }
    /// <summary>
    /// 二维码模板类型
    /// </summary>
    public enum DimensionalElementTye
    {
        /// <summary>
        /// 空值
        /// </summary>
        None = -1,
        /// <summary>
        /// 座位二维码
        /// </summary>
        SeatCode = 0,
        /// <summary>
        /// 阅览室名称
        /// </summary>
        ReadingRoomName = 1,
        /// <summary>
        /// 座位编号
        /// </summary>
        SeatNo = 2,
        /// <summary>
        /// 文本信息
        /// </summary>
        Text = 3,
        /// <summary>
        /// 底板背景图
        /// </summary>
        Background = 4,
        /// <summary>
        /// 图片
        /// </summary>
        Image = 5,
    }
    /// <summary>
    /// 文本对齐方式
    /// </summary>
    public enum ElementTextAlignment
    {
        /// <summary>
        /// 左对齐
        /// </summary>
        Left = 0,
        /// <summary>
        /// 右对齐
        /// </summary>
        Right = 1,
        /// <summary>
        /// 中心对齐
        /// </summary>
        Center = 2,
    }
}
