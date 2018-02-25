using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AdvertManageClient.Code
{
    public class LoginNameValidation:ValidationRule
    {
        private int _MaxLength = 0;
        /// <summary>
        /// 字符串最大长度
        /// </summary>
        public int MaxLength
        {
            get { return _MaxLength; }
            set { _MaxLength = value; }
        }
        private int _MinLength = 0;
        /// <summary>
        /// 字符串最小长度
        /// </summary>
        public int MinLength
        {
            get { return _MinLength; }
            set { _MinLength = value; }
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string strValue = value as string;
            try
            {
                if (strValue.Length < MinLength || strValue.Length > MaxLength)
                {
                    return new ValidationResult(false, string.Format("登录名长度为{0}至{1}个字符",MinLength,MaxLength));
                }
                else
                {
                    return new ValidationResult(true,null);
                }
            }
            catch
                (Exception ex) {
                    return new ValidationResult(false,string.Format("验证登录名失败:{0}",ex.Message));
            }

        }
    }
}
