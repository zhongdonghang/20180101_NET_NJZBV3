using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AuthorizeVerify
{
    public class ServiceAuthorize
    {
        bool isAdmin;
        /// <summary>
        /// 是否是管理权限的账号
        /// </summary>
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }
        string schoolNo;
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNo
        {
            get { return schoolNo; }
            set { schoolNo = value; }
        }
        string userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        string userPwd;
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd
        {
            get { return userPwd; }
            set { userPwd = value; }
        }
        List<ServiceAuthorizeItem> _ServiceAuthorizeItems = new List<ServiceAuthorizeItem>();
        /// <summary>
        /// 允许访问的服务项
        /// </summary>
        public List<ServiceAuthorizeItem> ServiceAuthorizeItems
        {
            get { return _ServiceAuthorizeItems; }
            set { _ServiceAuthorizeItems = value; }
        }

        /// <summary>
        /// 解析授权。
        /// </summary>
        /// <param name="strAuthorizes">授权字符串，格式为userName=AES密文</param>
        /// <returns></returns>
        public static Dictionary<string, ServiceAuthorize> AnalyzeAuthorize(string authorized_keys_filePath)
        {
            string encryptionContext = "";
            if (File.Exists(authorized_keys_filePath))
            {
                encryptionContext = File.ReadAllText(authorized_keys_filePath, Encoding.ASCII);
            }
            else
            {
                throw new Exception("未找到授权文件。");
            }

            Dictionary<string, ServiceAuthorize> authorizes = new Dictionary<string, ServiceAuthorize>();
            string[] arrAuthorizes = encryptionContext.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string authorize in arrAuthorizes)
            {
                try
                {
                    string authorizeUserName = authorize.Substring(0, authorize.IndexOf('='));
                    string authorizeContent = authorize.Substring(authorize.IndexOf('=')+1);
                    ServiceAuthorize authorizeObj = SeatManage.SeatManageComm.JSONSerializer.Deserialize<ServiceAuthorize>(SeatManage.SeatManageComm.AESAlgorithm.AESDecrypt(authorizeContent));
                    authorizes.Add(authorizeUserName, authorizeObj);
                }
                catch (Exception ex)
                { 
                    
                }
            }
            return authorizes;
        }
    }

    /// <summary>
    /// 每一个服务的授权
    /// </summary>
    public class ServiceAuthorizeItem
    {
        

        /// <summary>
        /// 服务授权
        /// </summary>
        string serviceName;
        /// <summary>
        /// 方法名称
        /// </summary>
        public string ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; }
        }
        List<string> allowMethodName = new List<string>();
        /// <summary>
        /// 允许访问的方法名
        /// </summary>
        public List<string> AllowMethodName
        {
            get { return allowMethodName; }
            set { allowMethodName = value; }
        }

       
    }
    
}
