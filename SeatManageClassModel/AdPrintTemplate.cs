using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 座位凭条上的广告模版
    /// </summary>
    [Serializable]
    public class AdPrintTemplate
    {
        private int id = -1;
        private string name = "";
        private string number = "";
        private string template = "";
        private bool isUsed = false;
        private AdType _Type = AdType.None;
        /// <summary>
        /// 座位凭条上广告模版Id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// 广告编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        /// <summary>
        /// Xml字符串
        /// </summary>
        public string Template
        {
            get { return template; }
            set { template = value; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }
        /// <summary>
        /// 广告类别
        /// </summary>
        public AdType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
      
    }
}
