using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DBUtility
{
    public abstract class DbCommon
    {
        /// <summary>
        /// ��ȡ�����ñ���
        /// </summary>
        /// <param name="strKey">����</param>
        /// <returns></returns>
        public static string GetConfigString(string strKey)
        {
            string strReturnValue = "";
            try
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[strKey]))
                {
                    strReturnValue = ConfigurationManager.AppSettings[strKey];
                }
                else
                {
                    Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    strReturnValue = cf.AppSettings.Settings[strKey].ToString();
                }
            }
            catch 
            {

                Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                strReturnValue = cf.AppSettings.Settings[strKey].ToString();
            }
            return strReturnValue;
        }
    }
}
