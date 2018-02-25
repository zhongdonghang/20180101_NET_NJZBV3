using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace JsonRPC
{
    public class JsonSerializer
    {
        public static string ToJson<T>(T t)
        {
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ds.WriteObject(ms, t);

            string strReturn = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return strReturn;
        }

        public static T FromJson<T>(string strJson) where T : class
        {
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strJson));
            return ds.ReadObject(ms) as T;
        }
        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(string url)
        {
             url = url.Replace(@"/", "`");
             url = url.Replace(@"\", "~");
            //url = url.Replace(" ", "%20");
            //url = url.Replace("+", "%2B");
            //url = url.Replace("#", "%23");
            //url = url.Replace("&", "%26");
            //url = url.Replace("=", "%3D");
            //url = url.Replace("?", "%3F");
            //url = url.Replace("{", "%7B");
            //url = url.Replace("}", "%7d"); 
            //url = url.Replace("\"", "%22");
            //url = url.Replace(".", "'.'");
            //url = url.Replace(",", "%27");
            return url;
        }
        /// <summary>
        /// url解码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(string url)
        {
             url = url.Replace("`", @"/");
             url = url.Replace("~", @"\");
            //url = url.Replace("%20", " ");
            //url = url.Replace("%2B", "+");
            //url = url.Replace("%23", "#");
            //url = url.Replace("%26", "&");
            //url = url.Replace("%3D", "=");
            //url = url.Replace("%3F", "?");
            //url = url.Replace("%7B", "{");
            //url = url.Replace("%7d", "}"); 
            //url = url.Replace("%22", "\"");
            //url = url.Replace("'.'", ".");
            //url = url.Replace("%27", ",");
            return url;
        }



    }
}
