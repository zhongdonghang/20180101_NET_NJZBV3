using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SchoolPocketBookOnlineV2.Code
{
    public class LdapHelper
    {
        public static bool CheckPassword(string plainText, string hashText)
        {
            if (string.IsNullOrEmpty(hashText))
            {
                return false;
            }

            if (hashText.StartsWith("{B64}"))
            {
                return GetB64Password(plainText) == hashText;
            }
            else if (hashText.StartsWith("{MD5}"))
            {
                return GetMd5Password(plainText) == hashText;
            }
            else if (hashText.StartsWith("{SHA}"))
            {
                return GetSHAPassword(plainText) == hashText;
            }
            else if (hashText.StartsWith("{SSHA}"))
            {
                var bytes = Convert.FromBase64String(hashText.Substring(6));
                var saltBytes = new byte[bytes.Length - 20];
                for (int i = 0; i < saltBytes.Length; ++i)
                {
                    saltBytes[i] = bytes[20 + i];
                }

                var hashText2 = GetSSHAPassword(plainText, saltBytes);
                return hashText == hashText2;
            }

            return false;
        }

        public static string GetB64Password(string plainText)
        {
            var bytes = Encoding.ASCII.GetBytes(plainText);
            return "{B64}" + Convert.ToBase64String(bytes);
        }


        public static string GetMd5Password(string plainText)
        {
            var md5Hasher = MD5.Create();
            var md5Bytes = md5Hasher.ComputeHash(Encoding.ASCII.GetBytes(plainText));
            var sBuilder = new StringBuilder();

            for (int i = 0; i < md5Bytes.Length; i++)
            {
                sBuilder.Append(md5Bytes[i].ToString("X2"));
            }

            return "{MD5}" + sBuilder.ToString();
        }

        public static string GetSHAPassword(string plainText)
        {
            var sha1Hasher = SHA1.Create();
            var plainTextBytes = Encoding.ASCII.GetBytes(plainText);
            var SHA1Bytes = sha1Hasher.ComputeHash(plainTextBytes);
            return "{SHA}" + Convert.ToBase64String(SHA1Bytes);
        }

        public static string GetSSHAPassword(string plainText)
        {
            var saltBytes = GenerateSalt(8);
            return GetSSHAPassword(plainText, saltBytes);
        }

        public static string GetSSHAPassword(string plainText, byte[] saltBytes)
        {
            var sha1Hasher = SHA1.Create();

            var plainTextBytes = Encoding.ASCII.GetBytes(plainText);
            var plainTextWithSaltBytes = AppendByteArray(plainTextBytes, saltBytes);
            var saltedSHA1Bytes = sha1Hasher.ComputeHash(plainTextWithSaltBytes);
            var saltedSHA1WithAppendedSaltBytes = AppendByteArray(saltedSHA1Bytes, saltBytes);
            return "{SSHA}" + Convert.ToBase64String(saltedSHA1WithAppendedSaltBytes);
        }

        private static byte[] GenerateSalt(int saltSize)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[saltSize];
            rng.GetBytes(buff);
            return buff;
        }

        private static byte[] AppendByteArray(byte[] byteArray1, byte[] byteArray2)
        {
            var byteArrayResult =
                    new byte[byteArray1.Length + byteArray2.Length];

            for (var i = 0; i < byteArray1.Length; i++)
                byteArrayResult[i] = byteArray1[i];
            for (var i = 0; i < byteArray2.Length; i++)
                byteArrayResult[byteArray1.Length + i] = byteArray2[i];

            return byteArrayResult;
        }
    }
}
