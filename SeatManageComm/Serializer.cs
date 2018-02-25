using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Web.Script.Serialization;
namespace SeatManage.SeatManageComm
{
    public class ByteSerializer
    {
        #region 二进制序列化
        private static MemoryStream SerializeBinary(object obj)//将对象序列化为内存流 
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                ms.Seek(0, SeekOrigin.Begin); 
                bf.Serialize(ms, obj);
                return ms;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static byte[] ObjectToByte(object obj)//将对象序列化为二进制数组      
        {
            MemoryStream ms = SerializeBinary(obj); 
            byte[] bytes = ms.ToArray();
            byte[] gzipbytes = GZip.Compress(ms.ToArray());
            Console.WriteLine("对象压缩前大小：{0},压缩后：{1}",bytes.Length,gzipbytes.Length);
            return gzipbytes;
        }
        private static object DeserializeBinary(MemoryStream ms)//将类存流反序列化为对象    
        {
            ms.Seek(0, SeekOrigin.Begin); 
            BinaryFormatter bf = new BinaryFormatter();
            object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }
        public static T DeserializeByte<T>(byte[] buffer)//将二进制数组能反序列化为对象   
        {

            try
            {
                MemoryStream ms = new MemoryStream(GZip.Decompress(buffer));
                return (T)DeserializeBinary(ms);
            }
            catch (Exception ex)
            { 
                ex.Source = string.Format("ByteSerializer.DeserializeByte");
                throw ex;
            }
        }
        #endregion

        #region byte数组压缩
        /// <summary>
        /// 压缩数据
        /// </summary>
        public class GZip
        {
            /// <summary>
            /// 将字节数组进行压缩后返回压缩的字节数组
            /// </summary>
            /// <param name="data">需要压缩的数组</param>
            /// <returns>压缩后的数组</returns>
            public static byte[] Compress(byte[] data)
            {
                MemoryStream stream = new MemoryStream();
                GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress);
                gZipStream.Write(data, 0, data.Length);
                gZipStream.Close();
                return stream.ToArray();
            }

            /// <summary>
            /// 解压字符数组
            /// </summary>
            /// <param name="data">压缩的数组</param>
            /// <returns>解压后的数组</returns>
            public static byte[] Decompress(byte[] data)
            {
                MemoryStream stream = new MemoryStream();

                GZipStream gZipStream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress);

                byte[] bytes = new byte[40960];
                int n;
                while ((n = gZipStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    stream.Write(bytes, 0, n);
                }
                gZipStream.Close();
                return stream.ToArray();
            }
        }
        #endregion


    }
    /// <summary>
    /// Json序列化
    /// </summary>
    public class JSONSerializer
    {

        static JavaScriptSerializer serializer = new JavaScriptSerializer();
        /// <summary>
        /// 对象序列化成字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(Object obj)
        {
            try
            {
                string result = serializer.Serialize(obj);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        /// <summary>
        /// 字符串序列化成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string input)
        {
            try
            {
                T obj = serializer.Deserialize<T>(input);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}

