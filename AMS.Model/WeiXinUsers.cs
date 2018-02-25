using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    public  class WeiXinUsers
    {
        string cardNo;
        /// <summary>
        /// 学生卡号
        /// </summary>
        public string CardNo
        {
            get { return cardNo ; }
            set { cardNo  = value; }
        }
        AMS_School schoolInfo;

        /// <summary>
        /// 学校信息
        /// </summary>
        public AMS_School SchoolInfo
        {
            get { return schoolInfo; }
            set { schoolInfo = value; }
        }
       
        string weixinID;
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeixinID
        {
            get { return weixinID; }
            set { weixinID = value; }
        }
    }
}
