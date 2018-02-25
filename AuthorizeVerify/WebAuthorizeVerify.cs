using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthorizeVerify;
using System.IO;

 
    public class WebAuthorizeVerify:IAuthorizeVerify
    {
        private static Dictionary<string,ServiceAuthorize> authorizeInfo =null; //授权信息，可能会有多个用户的授权信息
        private static Object lockObj = new object();
        public WebAuthorizeVerify()
        {
            if (authorizeInfo == null)
            {
                lock (lockObj)
                {
                    if (authorizeInfo == null)
                    {
                        readAuthorized();//读取授权文件
                    }
                }
            }
        }
        /// <summary>
        /// 读取授权
        /// </summary>
        private void readAuthorized()
        {
            string authorized_keys_filePath = string.Format("{0}/ws_authorized_keys", AppDomain.CurrentDomain.BaseDirectory);
            authorizeInfo = ServiceAuthorize.AnalyzeAuthorize(authorized_keys_filePath);
        }
        /// <summary>
        /// 执行验证操作
        /// </summary>
        /// <param name="serviceName">如果</param>
        /// <param name="method"></param>
        /// <returns></returns>
        public bool Verify(string userName,string pwd,string serviceName,string methodName)
        {
            return true;
            try
            {
                if (!authorizeInfo.ContainsKey(userName))
                {
                    readAuthorized();
                    if (!authorizeInfo.ContainsKey(userName))//如果读取一次之后授权信息还是不存在，则直接返回false
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            ServiceAuthorize authirize = authorizeInfo[userName];
            if (authirize.UserPwd.Equals(pwd))
            {
                foreach (ServiceAuthorizeItem authorizeItem in authirize.ServiceAuthorizeItems)
                {
                    if (authorizeItem.ServiceName.Equals(serviceName))
                    {
                        foreach (string method in authorizeItem.AllowMethodName)
                        {
                            if (method.Equals(methodName))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
         



       
    } 
