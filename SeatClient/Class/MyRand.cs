using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.IO; 
using System.Data.SqlClient;
using System.Data;

namespace SeatClient.Class
{
    /// <summary>
    /// 抽奖码随机数
    /// </summary>
    public class MyRand
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public static readonly string myKey = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
        /// <summary>
        /// 学校代码
        /// </summary>
        private static readonly string SchoolCode = ConfigurationManager.ConnectionStrings["SchoolCode"].ConnectionString;

        #region 加密方法
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="pToEncrypt">需要加密字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns>加密后的字符串</returns>
        private static string Encrypt(string pToEncrypt, string sKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach(byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
            catch
            {
            }
            return "";
        }
        #endregion

        /// <summary>
        /// 获取原始码
        /// </summary>
        /// <returns></returns>
       static int GetSourceCode()
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ExcResult", SqlDbType.Int);
            parameters[0].Direction = ParameterDirection.Output;

         //   DbHelperSQL.Execute_Proc("Proc_GetNewSourceCode", parameters);
            return (int)parameters[0].Value;
        }
        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <returns></returns>
        public static string GetRand()
        {
            string strRand = "";
            string codeNum = GetSourceCode().ToString();
            switch (codeNum.Length)
            { 
                case 1:
                    strRand = SchoolCode + "00000" + codeNum;
                    break;
                case 2:
                    strRand = SchoolCode + "0000" + codeNum;
                    break;
                case 3:
                    strRand = SchoolCode + "000" + codeNum;
                    break;
                case 4:
                    strRand = SchoolCode + "00" + codeNum;
                    break;
                case 5:
                    strRand = SchoolCode + "0" + codeNum;
                    break;
                case 6:
                    strRand = SchoolCode + codeNum;
                    break;
            }
            return Encrypt(strRand, myKey);
        }
    }
}
