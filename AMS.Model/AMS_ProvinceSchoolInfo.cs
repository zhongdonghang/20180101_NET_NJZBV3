using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    [Serializable] 
    public class AMS_ProvinceSchoolInfo : AMS_Province
    {
        List<AMS_School> schools = new List<AMS_School>();
        /// <summary>
        /// 学校
        /// </summary>
        public List<AMS_School> Schools
        {
            get { return schools; }
            set { schools = value; }
        } 
    }
}
