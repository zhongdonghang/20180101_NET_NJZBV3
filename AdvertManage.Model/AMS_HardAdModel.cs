using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvertManage.Model
{
    /// <summary>
    /// 硬广
    /// </summary>
    [Serializable]
    public class AMS_HardAdModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Number
        {
            get;
            set;
        }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectDate
        {
            get;
            set;
        }
        /// <summary>
        ///结束日期
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 硬广图片二进制流
        /// </summary>
        public byte[] AdImage
        {
            get;
            set;
        }
    }
}
