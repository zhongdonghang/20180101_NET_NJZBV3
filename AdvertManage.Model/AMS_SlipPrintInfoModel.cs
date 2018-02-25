using System;
namespace AdvertManage.Model
{
    /// <summary>
    /// View_SlipPrintInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AMS_SlipPrintInfoModel
    {
        public AMS_SlipPrintInfoModel()
        { }
        #region Model
        private int _CampusId;
        private string _campusNum;
        private string _CampusName;
        private int _slipcustomerid;
        private string _slipcustomernum;
        private DateTime? _date;
        private int? _printamount;
        private int? _lookoveramount;
        /// <summary>
        /// 校区Id
        /// </summary>
        public int CampusId
        {
            set { _CampusId = value; }
            get { return _CampusId; }
        }
        /// <summary>
        /// 校区编号
        /// </summary>
        public string CompusNum
        {
            set { _campusNum = value; }
            get { return _campusNum; }
        }
        /// <summary>
        /// 校区名称
        /// </summary>
        public string CampusName
        {
            set { _CampusName = value; }
            get { return _CampusName; }
        }
        /// <summary>
        /// 优惠券Id
        /// </summary>
        public int SlipCustomerId
        {
            set { _slipcustomerid = value; }
            get { return _slipcustomerid; }
        }
        /// <summary>
        /// 凭条编号
        /// </summary>
        public string SlipCustomerNum
        {
            set { _slipcustomernum = value; }
            get { return _slipcustomernum; }
        }
        /// <summary>
        /// 优惠券编号
        /// </summary>
        public DateTime? Date
        {
            set { _date = value; }
            get { return _date; }
        }
        /// <summary>
        /// 打印次数
        /// </summary>
        public int? PrintAmount
        {
            set { _printamount = value; }
            get { return _printamount; }
        }
        /// <summary>
        /// 查看次数
        /// </summary>
        public int? LookOverAmount
        {
            set { _lookoveramount = value; }
            get { return _lookoveramount; }
        }

        #endregion Model



    }
}

