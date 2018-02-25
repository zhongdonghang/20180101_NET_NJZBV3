using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AuthorizeVerify
{
    public class FunctionAuthorizeInfo
    {
        string schoolNum = "";
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNum
        {
            get { return schoolNum; }
            set { schoolNum = value; }
        }

        List<string> systemFunction = new List<string>();
        /// <summary>
        /// 注册的系统功能
        /// </summary>
        public List<string> SystemFunction
        {
            get { return systemFunction; }
            set { systemFunction = value; }
        }

        /// <summary>
        /// 解析授权。
        /// </summary>
        /// <param name="strAuthorizes">授权字符串，格式为userName=AES密文</param>
        /// <returns></returns>
        public static FunctionAuthorizeInfo AnalyzeAuthorize(string authorized_keys_filePath)
        {
            FunctionAuthorizeInfo authorizeInfo = null;
            string encryptionContext = "";
            if (File.Exists(authorized_keys_filePath))
            {
                encryptionContext = File.ReadAllText(authorized_keys_filePath, Encoding.ASCII);
            }
            else
            {
                throw new Exception("未找到授权文件。");
            }
            try
            {
                authorizeInfo = SeatManage.SeatManageComm.JSONSerializer.Deserialize<FunctionAuthorizeInfo>(SeatManage.SeatManageComm.AESAlgorithm.AESDecrypt(encryptionContext));
            }
            catch (Exception ex)
            {

            }
            return authorizeInfo;
        }
    }

}
