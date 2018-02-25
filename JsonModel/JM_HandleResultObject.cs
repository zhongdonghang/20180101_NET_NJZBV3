using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    /// <summary>
    /// 封装执行的结果以及返回信息,信息为object类型
    /// </summary>
    public class JM_HandleResultObject
    {
        bool result;
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool Result
        {
            get { return result; }
            set { result = value; }
        }
        object msg;
        /// <summary>
        /// 返回信息
        /// </summary>
        public object Msg
        {
            get { return msg; }
            set { msg = value; }
        }
    }
}
