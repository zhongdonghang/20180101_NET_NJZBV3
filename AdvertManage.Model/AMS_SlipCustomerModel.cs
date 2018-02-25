using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvertManage.Model
{
    /// <summary>
    /// 优惠券客户
    /// </summary>
    [Serializable]
    public class AMS_SlipCustomerModel
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
        /// 编号
        /// </summary>
        public string Number
        {
            get;
            set;
        }
        /// <summary>
        /// 显示的图片地址
        /// </summary>
        public string ImageUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 优惠内容
        /// </summary>
        public string SlipTemplate
        {
            get;
            set;
        }
       
        /// <summary>
        /// 客户图片Logo
        /// </summary>
        public string CustomerImage
        {
            get;
            set;
        }
        /// <summary>
        /// 校区编号
        /// </summary>
        public string CampusNum
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
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 是否打印优惠券
        /// </summary>
        public bool IsPrint
        {
            get;
            set;
        }
    }
}
