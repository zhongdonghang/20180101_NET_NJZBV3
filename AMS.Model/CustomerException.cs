using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    public class CustomerException : Exception
    {
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }
        private string _ErrorSourcesClass = "";
        /// <summary>
        /// 错误来源类
        /// </summary>
        public string ErrorSourcesClass
        {
            get { return _ErrorSourcesClass; }
            set { _ErrorSourcesClass = value; }
        }
        private string _ErrorSourcesFunction = "";
        /// <summary>
        /// 错误来源方法
        /// </summary>
        public string ErrorSourcesFunction
        {
            get { return _ErrorSourcesFunction; }
            set { _ErrorSourcesFunction = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public CustomerException(string message)
        {
            _ErrorMessage = message;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="errorclass">错误类</param>
        /// <param name="errorfunction">错误方法</param>
        public CustomerException(string message, string errorclass, string errorfunction)
        {
            _ErrorMessage = message;
            _ErrorSourcesClass = errorclass;
            _ErrorSourcesFunction = errorfunction;
        }
    }
}
