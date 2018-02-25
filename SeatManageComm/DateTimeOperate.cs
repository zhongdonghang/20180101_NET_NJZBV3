using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.SeatManageComm
{
    /// <summary>
    /// 时间相关操作
    /// </summary>
    public class DateTimeOperate
    {
        /// <summary>
        /// 判断时间是否在开始时间和结束时间之间
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="datetime">要比较的时间</param>
        /// <returns></returns>
        public static bool DateAccord(DateTime beginTime, DateTime endTime, DateTime datetime)
        {
            if ((datetime.CompareTo(beginTime) > 0) && (datetime.CompareTo(endTime) < 0)) 
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
