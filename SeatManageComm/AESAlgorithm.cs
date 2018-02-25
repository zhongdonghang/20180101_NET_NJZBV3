using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SeatManage.SeatManageComm
{
    public class AESAlgorithm
    {
        //默认密钥向量    
        private static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string key = "nanjingzhibaiwenxin";
        /// <summary>      
        /// AES加密算法   
        /// </summary>   
        /// <param name="plainText">明文字符串</param>   
        /// <param name="strKey">密钥</param>   
        /// <returns>返回加密后的密文字节数组</returns>   
        public static string AESEncrypt(string plainText)
        {
            return AESEncrypt(plainText, key);
        }

        /// <summary>      
        /// AES加密算法   
        /// </summary>   
        /// <param name="plainText">明文字符串</param>   
        /// <param name="strKey">密钥</param>   
        /// <returns>返回加密后的密文字节数组</returns>   
        public static string AESEncrypt(string plainText, string strKey)
        {        //分组加密算法           
            SymmetricAlgorithm des = Rijndael.Create();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组       
            //设置密钥及密钥向量   
            des.Key = Encoding.UTF8.GetBytes(strKey.Substring(0, 16));
            des.IV = _key1;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组   
            cs.Close();
            ms.Close();
            string strEncrypt = Convert.ToBase64String(cipherBytes);
            return strEncrypt;

        }
        /// <summary>   
        /// AES解密   
        /// </summary>   
        /// <param name="cipherText">密文字节数组</param>   
        /// <param name="strKey">密钥</param>   
        /// <returns>返回解密后的字符串</returns>   
        public static string AESDecrypt(string plainText)
        {
            return AESDecrypt(plainText, key);
        }
        /// <summary>   
        /// AES解密   
        /// </summary>   
        /// <param name="cipherText">密文字节数组</param>   
        /// <param name="strKey">密钥</param>   
        /// <returns>返回解密后的字符串</returns>   
        public static string AESDecrypt(string plainText, string strKey)
        {
            try
            {
                byte[] cipherText = Convert.FromBase64String(plainText);
                SymmetricAlgorithm des = Rijndael.Create();
                des.Key = Encoding.UTF8.GetBytes(strKey.Substring(0, 16));
                des.IV = _key1;
                byte[] decryptBytes = new byte[cipherText.Length];
                MemoryStream ms = new MemoryStream(cipherText);
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
                cs.Read(decryptBytes, 0, decryptBytes.Length);
                cs.Close();
                ms.Close();
                string strDecrypt = Encoding.UTF8.GetString(decryptBytes);
                return strDecrypt.Replace("\0", "");
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("解密字符串“{0}”出错，错误信息：{1}", plainText, ex.Message));
                return plainText;
            }
        }

        /// <summary>
        /// url加密、Aes加密算法，替换掉url中的特殊字符
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>+换成_  /换成-  =换成~</returns>
        public static string UrlEncode(string plainText)
        {
            string str= AESEncrypt(plainText);//加密
            return str.Replace('+', '_').Replace('/', '-').Replace('=','~');
        }

        /// <summary>
        /// url解密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string UrlDecode(string plainText)
        {
            string str = plainText.Replace('_', '+').Replace('-', '/').Replace('~','=');
            return AESDecrypt(str);
        }


        /*************************************************没有特殊字符的加密***********************************************/

        public static string DESEncode(string str)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(str);
         
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            stream.Close();
            return builder.ToString();
        }
        /// <summary>   
        /// Des 解密 GB2312     
        /// </summary>   
        /// <param name="str">Desc string</param> 
        /// <param name="key">Key ,必须为8位 </param> 
        /// <returns></returns>   
        public static string DESDecode(string str)
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                byte[] buffer = new byte[str.Length / 2];
                for (int i = 0; i < (str.Length / 2); i++)
                {
                    int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                    buffer[i] = (byte)num2;
                }
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                stream.Close();
                return Encoding.GetEncoding("GB2312").GetString(stream.ToArray());
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}
