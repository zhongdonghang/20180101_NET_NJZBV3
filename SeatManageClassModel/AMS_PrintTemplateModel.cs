using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 打印模板
    /// </summary>
    [Serializable]
   public class AMS_PrintTemplateModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 打印模板
        /// </summary>
        public string Template
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
        /// 结束日期
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe
        {
            get;
            set;
        }

        public string Num { get; set; }
    }
}
