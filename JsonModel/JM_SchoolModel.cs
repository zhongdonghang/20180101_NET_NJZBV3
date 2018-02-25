using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatManage.JsonModel
{
    public class JM_SchoolModel
    {
        private string schoolId;

        public string SchoolId
        {
            get { return schoolId; }
            set { schoolId = value; }
        }
        private string schoolName;

        public string SchoolName
        {
            get { return schoolName; }
            set { schoolName = value; }
        }
        private string schoolNum;

        public string SchoolNum
        {
            get { return schoolNum; }
            set { schoolNum = value; }
        }

        private string serviceUrl;

        public string ServiceUrl
        {
            get { return serviceUrl; }
            set { serviceUrl = value; }
        }
        
    }
}