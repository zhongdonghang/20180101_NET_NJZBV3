using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using SeatManage.ClassModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AMS.MediaPlayer.Code
{
    public class GetCoupons : INotifyPropertyChanged
    {
        int looptime;
        //private int _looptime=0;
        Timer t;
        Timer t1;
        public event EventHandler LogoChanage;
        public event EventHandler ScrollStart;

        #region 构造逻辑
        public GetCoupons()
        {
            _CouponsList.Add(1, new ObservableCollection<CouponsInfo>());
            _CouponsList.Add(2, new ObservableCollection<CouponsInfo>());
            _CouponsList.Add(3, new ObservableCollection<CouponsInfo>());
            _CouponsList.Add(4, new ObservableCollection<CouponsInfo>());
            _CouponsList.Add(5, new ObservableCollection<CouponsInfo>());
            _CouponsList.Add(6, new ObservableCollection<CouponsInfo>());
            _CouponsList.Add(7, new ObservableCollection<CouponsInfo>());
            _CouponsList.Add(8, new ObservableCollection<CouponsInfo>());
        }
        #endregion

        private Dictionary<int, ObservableCollection<CouponsInfo>> _CouponsList = new Dictionary<int, ObservableCollection<CouponsInfo>>();
        /// <summary>
        /// 优惠券列表
        /// </summary>
        public Dictionary<int, ObservableCollection<CouponsInfo>> CouponsList
        {
            get { return _CouponsList; }
            set { _CouponsList = value; PropertyValueChanded("CouponsList"); }
        }


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
                DateTime nowDT = SeatManage.Bll.ServiceDateTime.Now;
                List<SeatManage.ClassModel.AMS_Advertisement> modelList = SeatManage.Bll.AdvertisementOperation.GetAdList(false, SeatManage.EnumType.AdType.SlipCustomerAd);
                foreach (AMS_Advertisement item in modelList)
                {
                    CouponsInfo coupons = CouponsInfo.ToModel(item.AdContent);
                    for (int i = 0; i < coupons.PopItemList.Count; i++)
                    {
                        if (coupons.PopItemList[i].EffectDate > nowDT || coupons.PopItemList[i].EndDate < nowDT)
                        {
                            coupons.PopItemList.Remove(coupons.PopItemList[i]);
                            i--;
                        }
                    }
                    coupons.ID = item.ID;
                    CouponsList[coupons.Station].Add(coupons);
                }
                SeatManage.Bll.FileOperate getImage = new SeatManage.Bll.FileOperate();
                foreach (KeyValuePair<int, ObservableCollection<CouponsInfo>> item in _CouponsList)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        for (int j = 0; j < item.Value[i].ImageFilePath.Count; j++)
                        {
                            string imageName = string.Format("{0}{1}", PlayerSetting.SysPath + "\\CouponsImage\\", item.Value[i].ImageFilePath[j]);
                            getImage.FileDownLoad(imageName, item.Value[i].ImageFilePath[j], SeatManage.EnumType.SeatManageSubsystem.SlipCustomerAd);
                        }
                    }
                    if (item.Value.Count == 0)
                    {
                        CouponsInfo coupons = new CouponsInfo();
                        coupons.Num = "";
                        coupons.LogoImage = PlayerSetting.DefaultVideosPath + PlayerSetting.DefaultVideo;
                        item.Value.Add(coupons);
                    }

                }
                t1.Stop();
                if (ScrollStart != null)
                {
                    ScrollStart(this, new EventArgs());
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
        private int index = 1;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        void timer1_Elapsed1(object sender, ElapsedEventArgs e)
        {
            timer1.Stop();
            if (_CouponsList[index].Count > 1)
            {
                _CouponsList[index].Add(_CouponsList[index][0]);
                _CouponsList[index].RemoveAt(0);
                if (LogoChanage != null)
                {
                    LogoChanage(this, new EventArgs());
                }
            }
            index++;
            if (index > 8)
            {
                index = 1;
            }
            timer1.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性值改变时通知绑定对象的方法
        /// </summary>
        /// <param name="propertyName"></param>
        private void PropertyValueChanded(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
