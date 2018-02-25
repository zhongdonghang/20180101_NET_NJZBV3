using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvertManage.Model
{
    /// <summary>
    /// 冠名广告
    /// </summary>
    [Serializable]
    public class AMS_TitleAdModel
    {
        /// <summary>
        /// 广告Id
        /// </summary>
        public int Id
        {
            get;
            set;

        }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime EffectDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string AdContent
        {
            get;
            set;
        }
    }
}
