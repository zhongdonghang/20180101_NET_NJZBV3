using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility;

namespace SeatManage.DAL
{
    public class ConnectionTest
    {
        /// <summary>
        /// 连接字符串测试
        /// </summary>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public static bool ConnectionStingTest(string connstr)
        {
            try
            {
                return DbHelperSQL.ConnectionStingTest(connstr);
            }
            catch
            {
                throw;
            }
        }
    }
}
