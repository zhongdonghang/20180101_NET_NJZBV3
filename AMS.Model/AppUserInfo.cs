using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    /// <summary>
    /// app 用户的信息 
    /// </summary>
   public class AppUserInfo
    {
        private string cardNo;

        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        private string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private string channelId;

        public string ChannelId
        {
            get { return channelId; }
            set { channelId = value; }
        }
        private int schoolId;

        public int SchoolId
        {
            get { return schoolId; }
            set { schoolId = value; }
        }

        private string schoolNumber;
       /// <summary>
       /// 学校编号
       /// </summary>
        public string SchoolNumber
        {
            get { return schoolNumber; }
            set { schoolNumber = value; }
        }

        private string schoolName;

        public string SchoolName
        {
            get { return schoolName; }
            set { schoolName = value; }
        }

    }
}
