using System;

namespace SeatManage.DataModel
{
      [Serializable]
    public class HardAdvertInfo
    {
        string _HardAdvertNo = "";
          /// <summary>
          /// 广告编号
          /// </summary>
        public string HardAdvertNo
        {
            get { return _HardAdvertNo; }
            set { _HardAdvertNo = value; }
        }
        DateTime _EffectDate = DateTime.Parse("1900-1-1");
          /// <summary>
          /// 生效时间
          /// </summary>
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        DateTime _EndDate = DateTime.Parse("1900-1-1");
          /// <summary>
          /// 结束时间
          /// </summary>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        byte[] _AdvertImage = null;
          /// <summary>
          /// 硬广图片
          /// </summary>
        public byte[] AdvertImage
        {
            get { return _AdvertImage; }
            set { _AdvertImage = value; }
        }
    }
}
