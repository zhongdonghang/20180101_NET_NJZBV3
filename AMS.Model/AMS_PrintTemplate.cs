using System;
namespace AMS.Model
{
    /// <summary>
    /// AMS_PrintTemplate:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AMS_PrintTemplate
    {
        public AMS_PrintTemplate()
        { }
        #region Model
        private int _id;
        private int _customerid = -1;
        private string _customerName;
        private string _name;
        private string _number;
        private int? _operator;
        private string _OperatorName;
        private string _template;
        private DateTime? _effectdate;
        private DateTime? _enddate;
        private string _describe;
        /// <summary>
        /// 打印模版ID
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
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
        /// 打印模版名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 打印模版编号
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
        /// 操作人姓名
        /// </summary>
        public string OperatorName
        {
            get { return _OperatorName; }
            set { _OperatorName = value; }
        }
        /// <summary>
        /// xml存放的打印模版
        /// </summary>
        public string Template
        {
            set { _template = value; }
            get { return _template; }
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
        /// 描述
        /// </summary>
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }
        #endregion Model

    }
}

