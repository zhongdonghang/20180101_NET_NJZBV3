using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Timers;
using System.Data;
using System.Configuration;
using AMS.MediaPlayer.Code;
using System.Xml;

namespace AMS.MediaPlayer.UI
{
    /// <summary>
    /// 广告业务逻辑
    /// </summary>
    public class AdLogic : INotifyPropertyChanged
    {
        //private string ImageUploadPath;
        //private string Campus;
        int looptime;
        //private int _looptime=0;
        Timer t;
        Timer t1;
        public event EventHandler ImageUrlChanage;
        public event EventHandler SCStart;
        #region 构造逻辑
        public AdLogic()
        {
            GetCustomer();
            //PrintSlip(""); 
        }
        #endregion

        #region 广告
        List<SeatManage.ClassModel.AMS_SlipCustomer> _SCModel = new List<SeatManage.ClassModel.AMS_SlipCustomer>();
        public List<SeatManage.ClassModel.AMS_SlipCustomer> SCModel
        {
            get
            {
                return _SCModel;
            }
            set
            {
                _SCModel = value;
            }
        }
        public bool isnull
        {
            get;
            set;
        }
        #endregion

        #region url变化逻辑
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <returns></returns>
        public void GetCustomer()
        {
            t1 = new Timer(1000);
            t1.Elapsed += new ElapsedEventHandler(t1_Elapsed);
            looptime = 0;
            t1.Start();
            t = new Timer(5000);
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
            t.Start();

        }

        void t1_Elapsed(object sender, ElapsedEventArgs e)
        {
            looptime += 1;
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            t.Stop();
            try
            {
                List<SeatManage.ClassModel.AMS_SlipCustomer> sm = new List<SeatManage.ClassModel.AMS_SlipCustomer>();
                sm = SeatManage.Bll.AMS_SlipCustomer.GetSlipCustomerList(PlayerSetting.CampusNo);//获取优惠券客户信息 
                DownSilpCustomer(sm);
                if (sm.Count <= 7 && sm.Count > 0)//客户总数不足14，循环添加n倍，直到超过14个为止
                {
                    int count = sm.Count;
                    while (true)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            sm.Add(sm[i]);
                        }
                        if (sm.Count > 7)
                        {
                            break;
                        }
                    }
                }
                t1.Stop();
                SCModel = sm;
                if (SCStart != null)
                {
                    SCStart(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                if (looptime > 300)
                {
                    t1.Stop();
                }
                else
                {
                    t.Start();
                }
            }
        }
        /// <summary>
        /// 下载优惠券图片
        /// </summary>
        /// <param name="list"></param>
        void DownSilpCustomer(List<SeatManage.ClassModel.AMS_SlipCustomer> list)
        {
            try
            {
                SeatManage.Bll.FileOperate getImage = new SeatManage.Bll.FileOperate();
                for (int i = 0; i < list.Count; i++)
                {
                    SeatManage.ClassModel.AMS_SlipCustomer model = list[i];
                    string printlogoName = GetLogoFileName(model.SlipTemplate);
                    string printlogoPath = string.Format("{0}{1}", PlayerSetting.SysPath + "\\SlipImage\\", GetLogoFileName(model.SlipTemplate));
                    string imageName = string.Format("{0}{1}", PlayerSetting.SysPath + "\\SlipImage\\", model.ImageName);
                    string logoName = string.Format("{0}{1}", PlayerSetting.SysPath + "\\SlipImage\\", model.CustomerLogo);
                    getImage.FileDownLoad(printlogoPath, printlogoName, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                    getImage.FileDownLoad(imageName, model.ImageName, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                    getImage.FileDownLoad(logoName, model.CustomerLogo, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                }
                string juneberryLogoPath = string.Format("{0}{1}", PlayerSetting.SysPath + "\\SlipImage\\", "南京智佰闻欣logo.png");
                getImage.FileDownLoad(juneberryLogoPath, "南京智佰闻欣logo.png", SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message); 
            }
        }

        Timer timer1 = null;
        /// <summary>
        /// 触发定时切换凭条位图片
        /// </summary>
        public void imageUrlChange()
        {
            timer1 = new Timer();
            timer1.Interval = PlayerSetting.AdLoopTime;
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed1);
            timer1.Start();
        }

        public void Start()
        {
            timer1.Start();
        }
        public void Stop()
        {
            timer1.Stop();
        }

        void timer1_Elapsed1(object sender, ElapsedEventArgs e)
        {
            timer1.Stop();
            SCModel.Add(SCModel[0]);
            SCModel.RemoveAt(0);
            if (ImageUrlChanage != null)
            {
                ImageUrlChanage(this, new EventArgs());
            }
            timer1.Start();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性值改变时通知绑定对象的方法
        /// </summary>
        /// <param name="propertyName"></param>
        private void PropertyValueChanded(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// 获取打印模板的图片
        /// </summary>
        /// <param name="printXML"></param>
        /// <returns></returns>
        private static string GetLogoFileName(string printXML)
        {
            string filename = "";
            if (!string.IsNullOrEmpty(printXML))
            {
                XmlDocument Templatedoc = new XmlDocument();
                Templatedoc.LoadXml(printXML);
                XmlElement Templateroot = Templatedoc.DocumentElement;
                XmlNodeList Templatexnlist = ((XmlNode)Templateroot).ChildNodes;
                for (int i = 0; i < Templatexnlist.Count; i++)
                {
                    if (Templatexnlist[i].Name == "Pic" && Templatexnlist[i].InnerText != "南京智佰闻欣logo.png")
                    {
                        filename = Templatexnlist[i].InnerText;
                    }
                }
            }
            return filename;
        }
    }
}