using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.AppJsonModel
{
    public class AJM_HandleResult
    {
        bool _result;
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }
        string _msg;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg
        {
            get { return _msg; }
            set { _msg = value; }
        }
    }
}
