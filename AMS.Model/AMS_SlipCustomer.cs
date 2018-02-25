using System;
namespace AMS.Model
{
    /// <summary>
    /// AMS_SlipCustomer:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AMS_SlipCustomer
    {
        public AMS_SlipCustomer()
        { }
        #region Model
        private int _id;
        private string _number;
        private int? _operator;
        private string _operatorName;
        private int _customerid = -1;
        private string _slipname;
        private string _imageurl;
        private string _sliptemplate;
        private string _couponsxml;
        private string _customerimage;
        private string _campusnum;
        private DateTime? _releasedate = DateTime.Now;
        private DateTime? _effectdate;
        private DateTime? _enddate;
        private int? _type;
        private bool? _isprint;
        private string _describe;
        private string _customerName;
        /// <summary>
        /// 客户名字
        /// </summary>
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }
        /// <summary>
        /// 优惠券ID
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatorName
        {
            get { return _operatorName; }
            set { _operatorName = value; }
        }
        /// <summary>
        /// 优惠券编号
        /// </summary>
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public int? Operator
        {
            set { _operator = value; }
            get { return _operator; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId
        {
            set { _customerid = value; }
            get { return _customerid; }
        }
        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string SlipName
        {
            set { _slipname = value; }
            get { return _slipname; }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        /// 优惠券打印模版
        /// </summary>
        public string SlipTemplate
        {
            set { _sliptemplate = value; }
            get { return _sliptemplate; }
        }
        /// <summary>
        /// 优惠券网页展示
        /// </summary>
        public string CouponsXml
        {
            set { _couponsxml = value; }
            get { return _couponsxml; }
        }
        /// <summary>
        /// 客户图片
        /// </summary>
        public string CustomerImage
        {
            set { _customerimage = value; }
            get { return _customerimage; }
        }
        /// <summary>
        /// 校区编号
        /// </summary>
        public string CampusNum
        {
            set { _campusnum = value; }
            get { return _campusnum; }
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? EffectDate
        {
            set { _effectdate = value; }
            get { return _effectdate; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 是否打印
        /// </summary>
        public bool? IsPrint
        {
            set { _isprint = value; }
            get { return _isprint; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }
        #endregion Model
        /// <summary>
        /// 下发时间
        /// </summary>
        public DateTime? ReleaseDate
        {
            set { _releasedate = value; }
            get { return _releasedate; }
        }
    }
}

