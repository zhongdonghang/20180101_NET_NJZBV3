using System;
namespace AMS.Model
{
    /// <summary>
    /// AMS_Province:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AMS_Province
    {
        public AMS_Province()
        { }
        #region Model
        private int _id = -1;
        private string _provincename = "";
        private string _remark = "";
        /// <summary>
        /// 省份ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName
        {
            set { _provincename = value; }
            get { return _provincename; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}

