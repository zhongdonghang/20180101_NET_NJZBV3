using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SeatManage.SeatManageComm
{
    public class MD5Algorithm
    {
        /// <summary>
        /// MD5 32位加密 大写
        /// </summary>
        /// <param name="pwd">明文</param>
        /// <returns>pwdMD5密文</returns>
        public static string GetMD5Str32(string pwd)
        {
            string pwdMD5 = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            for (int i = 0; i < s.Length; i++)
            {
                pwdMD5 = pwdMD5 + s[i].ToString("X2");
            }
            return pwdMD5;
        }
        /// <summary>
        /// MD5 32位加密 小写
        /// </summary>
        /// <param name="pwd">明文</param>
        /// <returns>pwdMD5密文</returns>
        public static string GetMD5Str32WithListKey(List<string> Keys)
        {
            Keys.Sort();
            StringBuilder tmpStr = new StringBuilder();
            for (int i = 0; i < Keys.Count; i++)
            {
                tmpStr.Append(Keys[i]);
            }
            string pwdMD5 = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(tmpStr.ToString()));
            for (int i = 0; i < s.Length; i++)
            {
                pwdMD5 = pwdMD5 + s[i].ToString("X2");
            }
            return pwdMD5;
        }
    }
}
