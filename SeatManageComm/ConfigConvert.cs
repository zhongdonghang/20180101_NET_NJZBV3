using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.SeatManageComm
{
    public class ConfigConvert
    {
        /// <summary>
        /// 1/0转换成bool类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ConvertToBool(string value)
        {
            switch (value)
            {
                case "0":
                    return false;

                case "1":
                    return true;
            }
            return false;
        }
        /// <summary>
        /// bool转换成1/0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToString(bool value)
        {
            switch (value)
            {
                case true:
                    return "1";
                case false:
                    return "0";
            }
            return "0";
        }
    }
}
