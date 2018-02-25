/********************************************
 * 作者：王昊天
 * 创建时间：2013-6-3
 * 说明：功能页面
 * 修改人：
 * 修改时间：
 * *****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class SysFuncDicInfo
    {
        private string _No = "";
        /// <summary>
        /// 功能页面编号
        /// </summary>
        public string No
        {
            get { return _No; }
            set { _No = value; }
        }

        private string _Mame = "";
        /// <summary>
        /// 功能页面名称
        /// </summary>
        public string Name
        {
            get { return _Mame; }
            set { _Mame = value; }
        }

        private string _PageUrl = "";
        /// <summary>
        /// 页面地址，相对路径
        /// </summary>
        public string PageUrl
        {
            get { return _PageUrl; }
            set { _PageUrl = value; }
        }

        private string _Order = "";
        /// <summary>
        /// 功能类型
        /// </summary>
        public string Order
        {
            get { return _Order; }
            set { _Order = value; }
        }
    }
}
