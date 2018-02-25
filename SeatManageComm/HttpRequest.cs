using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace SeatManage.SeatManageComm
{
    public class HttpRequest
    {
        public static string Post(string url, string content)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";  //设置POST请求模式
            byte[] PostData = System.Text.Encoding.UTF8.GetBytes(content);
            request.ContentType = "application/x-www-form-urlencoded";  //设置ContntType ，这句很重要，否则无法传递参数
            request.ContentLength = PostData.Length;                  //设置请求内容大小，当然就设置成我们的参数字节数据大小。
            Stream requestStream = request.GetRequestStream();        //获取请求流
            requestStream.Write(PostData, 0, PostData.Length);        //将参数字节数组写入到请求流里
            requestStream.Close();                                    //关闭请求流
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();        //执行请求，获取响应对象
            Stream stream = response.GetResponseStream();                            //获取响应流
            StreamReader sr = new StreamReader(stream);                              //创建流读取对象
            string responseHTML = sr.ReadToEnd();                      //读取响应流
            response.Close();                                          //关闭响应流 
            return responseHTML;
        }
    }
}
