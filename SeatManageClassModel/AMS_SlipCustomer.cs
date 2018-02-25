using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 优惠券客户信息
    /// </summary>
    public class AMS_SlipCustomer
    {
        private int id = -1;
        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string _No = "";
        /// <summary>
        /// 编号
        /// </summary>
        public string No
        {
            get { return _No; }
            set { _No = value; }
        }
        private string _Name = "";
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _ImageUrl = "";
        /// <summary>
        /// 显示的图片地址
        /// </summary>
        public string ImageName
        { 
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }
        private string _SlipTemplate = "";
        /// <summary>
        /// 凭条模板
        /// </summary>
        public string SlipTemplate
        {
            get { return _SlipTemplate; }
            set { _SlipTemplate = value; }
        }
        private DateTime _EffectDate = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        private DateTime _EndDate = DateTime.Parse("1900-1-1");

        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        private int _Type ;
        /// <summary>
        /// 客户类型
        /// </summary>
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private int _PrintCount;
        /// <summary>
        /// 打印次数
        /// </summary>
        public int PrintAmount
        {
            get { return _PrintCount; }
            set { _PrintCount = value; }
        }
        private string _CustomerImage = "";
        /// <summary>
        /// 客户Logo
        /// </summary>
        public string CustomerLogo
        {
            get { return _CustomerImage; }
            set { _CustomerImage = value; }
        }
        private string _CampusNum = "";
        /// <summary>
        /// 校区编号
        /// </summary>
        public string CampusNum
        {
            get { return _CampusNum; }
            set { _CampusNum = value; }
        }

        /// <summary>
        /// 优惠券查看次数
        /// </summary>
        public int LookOverAmount 
        { get; set; }

        private bool _IsPrint = true;
        /// <summary>
        /// 优惠券是否打印
        /// </summary>
        public bool IsPrint
        {
            get { return _IsPrint; }
            set { _IsPrint = value; }
        }

        private string _num;
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _num; }
            set { _num = value; }
        }
    }
}
