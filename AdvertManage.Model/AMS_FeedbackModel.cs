using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvertManage.Model
{
    /// <summary>
    /// 意见反馈Model
    /// </summary>
    [Serializable]
    public class AMS_FeedbackModel
    {
        public int Id
        {
            get;
            set;
        }
        public string CardNo
        {
            get;
            set;
        }
        public string SchoolId
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public DateTime SubmitTime
        {
            get;
            set;
        }
    }
}
