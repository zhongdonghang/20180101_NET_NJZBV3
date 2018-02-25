using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    [Serializable]
     public class AMS_RollTitles
    {
        public AMS_RollTitles()
        {}

        private int _ID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _Name;
        /// <summary>
        /// 滚动名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private DateTime? _EffectDate;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        private DateTime? _EndDate;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private int _CustomerId;
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        private string _Type;
        /// <summary>
        /// 内容
        /// </summary>
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private int _OperatorID;
        /// <summary>
        /// 操作人
        /// </summary>
        public int OperatorID
        {
            get { return _OperatorID; }
            set { _OperatorID = value; }
        }
        private string _OpratorName;
        /// <summary>
        /// 操作者
        /// </summary>
        public string OpratorName
        {
            get { return _OpratorName; }
            set { _OpratorName = value; }
        }

        private string _Num;
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _Num; }
            set { _Num = value; }
        }

    }
}
