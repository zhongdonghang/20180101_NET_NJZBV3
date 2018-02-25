using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace SchoolNoteEditer.ViewModel
{
    public class VM_MainWindow : INotifyPropertyChanged
    {
        public VM_MainWindow()
        {
            AngleList.Add(new VM_Angle() { Name = "0°", Value = 0 });
            AngleList.Add(new VM_Angle() { Name = "90°", Value = 90 });
            AngleList.Add(new VM_Angle() { Name = "180°", Value = 180 });
            AngleList.Add(new VM_Angle() { Name = "270°", Value = 270 });

            TextAlignmentList.Add(new VM_TextAlignment() { Name = "左对齐", Value = SeatManage.ClassModel.ElementTextAlignment.Left });
            TextAlignmentList.Add(new VM_TextAlignment() { Name = "右对齐", Value = SeatManage.ClassModel.ElementTextAlignment.Right });
            TextAlignmentList.Add(new VM_TextAlignment() { Name = "中心对齐", Value = SeatManage.ClassModel.ElementTextAlignment.Center });

            TemplateHeight = 300;
            TemplateWidth = 300;
        }

        private SeatManage.ClassModel.DimensionalTemplate _Model = new SeatManage.ClassModel.DimensionalTemplate();
        /// <summary>
        /// model
        /// </summary>
        public SeatManage.ClassModel.DimensionalTemplate Model
        {
            get { return _Model; }
            set { _Model = value; OnPropertyChanged("Model"); }
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name
        {
            get { return _Model.Name; }
            set { _Model.Name = value; OnPropertyChanged("Name"); }
        }
        public int DPI
        {
            get { return _Model.DPI; }
            set
            {
                _Model.DPI = value;
                PrintHeight = TemplateHeight * 2.54 / DPI;
                PrintWidth = TemplateWidth * 2.54 / DPI;
                OnPropertyChanged("DPI");
            }
        }
        /// <summary>
        /// 模板高度
        /// </summary>
        public double TemplateHeight
        {
            get { return _Model.Height; }
            set
            {
                _Model.Height = value;
                PrintHeight = TemplateHeight * 2.54 / DPI;
                OnPropertyChanged("TemplateHeight");
            }
        }
        /// <summary>
        /// 模板宽度
        /// </summary>
        public double TemplateWidth
        {
            get { return _Model.Width; }
            set
            {
                _Model.Width = value;
                PrintWidth = TemplateWidth * 2.54 / DPI;
                OnPropertyChanged("TemplateWidth");
            }
        }
        /// <summary>
        /// 实际高度
        /// </summary>
        public double PrintHeight
        {
            get { return _Model.PrintHeight; }
            set { _Model.PrintHeight = value; OnPropertyChanged("PrintHeight"); }
        }
        /// <summary>
        /// 实际宽度
        /// </summary>
        public double PrintWidth
        {
            get { return _Model.PrintWidth; }
            set { _Model.PrintWidth = value; OnPropertyChanged("PrintWidth"); }
        }

        private VM_UC_Element _NowEditViewElement = new VM_UC_Element();
        /// <summary>
        /// 
        /// </summary>
        public VM_UC_Element NowEditViewElement
        {
            get { return _NowEditViewElement; }
            set { _NowEditViewElement = value; OnPropertyChanged("NowEditViewElement"); }
        }
        private ObservableCollection<VM_UC_Element> _ElementList = new ObservableCollection<VM_UC_Element>();
        /// <summary>
        /// 元素
        /// </summary>
        public ObservableCollection<VM_UC_Element> ElementList
        {
            get { return _ElementList; }
            set { _ElementList = value; OnPropertyChanged("ElementList"); }
        }
        private ObservableCollection<VM_TextAlignment> _TextAlignmentList = new ObservableCollection<VM_TextAlignment>();
        /// <summary>
        /// 对齐方式
        /// </summary>
        public ObservableCollection<VM_TextAlignment> TextAlignmentList
        {
            get { return _TextAlignmentList; }
            set { _TextAlignmentList = value; OnPropertyChanged("TextAlignmentList"); }
        }
        /// <summary>
        /// 角度列表
        /// </summary>
        private ObservableCollection<VM_Angle> _AngleList = new ObservableCollection<VM_Angle>();
        /// <summary>
        /// 对齐方式
        /// </summary>
        public ObservableCollection<VM_Angle> AngleList
        {
            get { return _AngleList; }
            set { _AngleList = value; OnPropertyChanged("AngleList"); }
        }

        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private string _SavePath = "";
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath
        {
            get { return _SavePath; }
            set { _SavePath = value; }
        }

        private int _MinXY = 5;
        /// <summary>
        /// 最小刻度线
        /// </summary>
        public int MinXY
        {
            get { return _MinXY; }
            set { _MinXY = value; }
        }
        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            try
            {
                if (string.IsNullOrEmpty(_Model.Name))
                {
                    ErrorMessage = "模板名称不能为空！";
                    return;
                }
                List<SeatManage.ClassModel.DimensionalElement> seatCodelList = _Model.ElementList.FindAll(u => u.Type == SeatManage.ClassModel.DimensionalElementTye.SeatCode);
                List<SeatManage.ClassModel.DimensionalElement> seatNolList = _Model.ElementList.FindAll(u => u.Type == SeatManage.ClassModel.DimensionalElementTye.SeatNo);
                if (seatCodelList.Count < 1)
                {
                    ErrorMessage = "请添加座位二维码！";
                    return;
                }
                if (seatNolList.Count != seatCodelList.Count)
                {
                    ErrorMessage = "二维码和座位号数目不对应！";
                    return;
                }
                seatCodelList = seatCodelList.OrderBy(u => u.Order).ToList();
                seatNolList = seatNolList.OrderBy(u => u.Order).ToList();
                for (int i = 0; i < seatCodelList.Count; i++)
                {
                    if (seatNolList[i].Order != seatCodelList[i].Order)
                    {
                        ErrorMessage = "二维码和座位号排序号不对应！";
                        return;
                    }
                }
                string savePath = _SavePath + "\\" + _Model.Name + "\\";
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                _Model.SeatCodeCount = seatCodelList.Count;
                _Model.ImageFiles.Clear();
                int bgNo = 1;
                int imgNo = 1;
                foreach (SeatManage.ClassModel.DimensionalElement element in _Model.ElementList)
                {
                    if (element.Type == SeatManage.ClassModel.DimensionalElementTye.Background)
                    {
                        File.Copy(element.ImageFile, savePath + "BG" + bgNo.ToString("00") + element.ImageFile.Substring(element.ImageFile.LastIndexOf('.')));
                        element.ImageFile = "BG" + bgNo.ToString("00") + element.ImageFile.Substring(element.ImageFile.LastIndexOf('.'));
                        _Model.ImageFiles.Add(element.ImageFile);
                        bgNo++;
                    }
                    if (element.Type == SeatManage.ClassModel.DimensionalElementTye.Image)
                    {
                        File.Copy(element.ImageFile, savePath + "IMG" + imgNo.ToString("00") + element.ImageFile.Substring(element.ImageFile.LastIndexOf('.')));
                        element.ImageFile = "IMG" + imgNo.ToString("00") + element.ImageFile.Substring(element.ImageFile.LastIndexOf('.'));
                        _Model.ImageFiles.Add(element.ImageFile);
                        imgNo++;
                    }
                }
                string saveXmlPath = savePath + "Template.xml";
                if (!File.Exists(saveXmlPath))
                {
                    FileStream fs = File.Create(saveXmlPath);
                    fs.Close();
                }
                StreamWriter sw = new StreamWriter(saveXmlPath);
                sw.Write(_Model.ToXml());
                sw.Flush();
                sw.Close();
                ErrorMessage = "保存成功！";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
