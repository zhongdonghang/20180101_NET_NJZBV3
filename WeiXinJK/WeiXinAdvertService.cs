using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AMS.Model;
using System.Web.Script.Serialization;
using WeiXinJK.Model;

namespace WeiXinJK
{
    public class WeiXinAdvertService : IWeiXinAdvertService
    {
        private static WeiXinAccessToken accessTocken = new WeiXinAccessToken(3600);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            if (string.IsNullOrEmpty(accessTocken.Access_token))
            {
                string appID = WeiXinJK.WeiXinJKPram.AppID;
                string appSecret = WeiXinJK.WeiXinJKPram.AppSecret;
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appID, appSecret);
                string result = SeatManage.SeatManageComm.HttpRequest.Post(url, "");
                JsonReader reader = new JsonTextReader(new StringReader(result));
                while (reader.Read())
                {
                    string a = reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value;
                    if ((string)reader.Value == "access_token")
                    {
                        reader.Read();
                        accessTocken.Access_token = (string)reader.Value;
                        return accessTocken.Access_token;
                    }
                    if ((string)reader.Value == "expires_in")
                    {

                    }
                    if ((string)reader.Value == "errcode")
                    {
                        return "";

                    }
                }
            }
            else
            {
                return accessTocken.Access_token;
            }


            return "";
        }
        /// <summary>
        /// 将信息通过post方式发给微信服务器
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        //public string SeatManage.SeatManageComm.HttpRequest.Post(string url, string content)
        //{
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        //    request.Method = "POST";  //设置POST请求模式
        //    byte[] PostData = System.Text.Encoding.UTF8.GetBytes(content);
        //    request.ContentType = "application/x-www-form-urlencoded";  //设置ContntType ，这句很重要，否则无法传递参数
        //    request.ContentLength = PostData.Length;                  //设置请求内容大小，当然就设置成我们的参数字节数据大小。
        //    Stream requestStream = request.GetRequestStream();        //获取请求流
        //    requestStream.Write(PostData, 0, PostData.Length);        //将参数字节数组写入到请求流里
        //    requestStream.Close();                                    //关闭请求流
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();        //执行请求，获取响应对象
        //    Stream stream = response.GetResponseStream();                            //获取响应流
        //    StreamReader sr = new StreamReader(stream);                              //创建流读取对象
        //    string responseHTML = sr.ReadToEnd();                      //读取响应流
        //    response.Close();                                          //关闭响应流 
        //    return responseHTML;

        //}

        /// <summary>
        /// 返回空字符串则执行成功
        /// </summary>
        /// <param name="weixinID"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendTxtMessage(string weixinID, string message)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=";
            WeiXinJK.Model.WeiXinJsonTxtMessage json = new Model.WeiXinJsonTxtMessage(weixinID, message);
            string token = string.IsNullOrEmpty(accessTocken.Access_token) ? GetAccessToken() : accessTocken.Access_token;
            string result = SeatManage.SeatManageComm.HttpRequest.Post(url + token, json.Getmess());
            JsonReader reader = new JsonTextReader(new StringReader(result));
            while (reader.Read())
            {
                if ((string)reader.Value == "errcode")
                {
                    reader.Read();
                    if (reader.Value.ToString() == "42001")
                    {
                        GetAccessToken(); //有可能无限递归
                        return SendTxtMessage(weixinID, message);
                    }
                    else if (reader.Value.ToString() == "0")
                        return "";
                    else
                        return "error:" + reader.Value.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 返回空字符串则执行成功
        /// </summary>
        /// <param name="weixinID"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        public string SendArticleMessage(string weixinID, List<AMS.Model.WeiXinAdvertse> article)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=";
            WeiXinJK.Model.WeiXinJsonArticle art = new Model.WeiXinJsonArticle(weixinID, article);

            string token = string.IsNullOrEmpty(accessTocken.Access_token) ? GetAccessToken() : accessTocken.Access_token;
            string result = SeatManage.SeatManageComm.HttpRequest.Post(url + token, art.Getmess());
            JsonReader reader = new JsonTextReader(new StringReader(result));
            while (reader.Read())
            {
                if ((string)reader.Value == "errcode")
                {
                    reader.Read();
                    if (reader.Value.ToString() == "42001")//判断是否为accesstoken过期
                    {
                        GetAccessToken(); //有可能无限递归
                        return SendArticleMessage(weixinID, article);
                    }
                    else if (reader.Value.ToString() == "0")
                        return "";
                    else
                        return "error:" + reader.Value.ToString();
                }
            }
            File.WriteAllText(@"D:/JOSN.txt", art.Getmess());
            return "";


        }
        /// <summary>
        /// 待测
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public string SendArticleToAll(List<string> article)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=";
            Model.WeiXinJsonArticleToAll art = new Model.WeiXinJsonArticleToAll("0", article);

            string token = string.IsNullOrEmpty(accessTocken.Access_token) ? GetAccessToken() : accessTocken.Access_token;
            string result = SeatManage.SeatManageComm.HttpRequest.Post(url + token, art.Getmess());
            JsonReader reader = new JsonTextReader(new StringReader(result));
            while (reader.Read())
            {
                if ((string)reader.Value == "errcode")
                {
                    reader.Read();
                    if (reader.Value.ToString() == "42001")//判断是否为accesstoken过期
                    {
                        GetAccessToken(); //有可能无限递归
                        return SendArticleToAll(article);
                    }
                    else if (reader.Value.ToString() == "0")
                        return "";
                    else
                        return "error:" + reader.Value.ToString();
                }
            }
            return "";
        }
        /// <summary>
        /// 默认分组（未分组时）id为0
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendTxtToALL(string message)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=";
            WeiXinJK.Model.WeiXinJsonTxtToAll txt = new Model.WeiXinJsonTxtToAll("0", message);
            string token = string.IsNullOrEmpty(accessTocken.Access_token) ? GetAccessToken() : accessTocken.Access_token;
            string result = SeatManage.SeatManageComm.HttpRequest.Post(url + token, txt.Getmess());
            JsonReader reader = new JsonTextReader(new StringReader(result));
            while (reader.Read())
            {
                if ((string)reader.Value == "errcode")
                {
                    reader.Read();
                    if (reader.Value.ToString() == "42001")//判断是否为accesstoken过期
                    {
                        GetAccessToken(); //有可能无限递归
                        return SendTxtToALL(message);
                    }
                    else if (reader.Value.ToString() == "0")
                        return "";
                    else
                        return "error:" + reader.Value.ToString();
                }
            }
            return "";
        }


        /// <summary>
        /// 返回空字符串则执行成功
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public string BuildUpMenu(string json)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=";
            string token = string.IsNullOrEmpty(accessTocken.Access_token) ? GetAccessToken() : accessTocken.Access_token;
            string result = SeatManage.SeatManageComm.HttpRequest.Post(url + token, json);
            JsonReader reader = new JsonTextReader(new StringReader(result));
            while (reader.Read())
            {
                if ((string)reader.Value == "errcode")
                {
                    reader.Read();
                    if (reader.Value.ToString() == "42001")//判断是否为accesstoken过期
                    {
                        GetAccessToken(); //有可能无限递归
                        return BuildUpMenu(json);
                    }
                    else if (reader.Value.ToString() == "0")
                        return "成功";
                    else
                        return "error:" + reader.Value.ToString();
                }
            }
            return "";
        }


        public string GetCityWeather(long cityid)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format("http://www.weather.com.cn/data/sk/{0}.html", cityid));
            string requestUrl = string.Format("http://www.weather.com.cn/data/sk/{0}.html", cityid);
            request.Timeout = 5000;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            JavaScriptSerializer s = new JavaScriptSerializer();
            Dictionary<string, object> JsonData = (Dictionary<string, object>)s.DeserializeObject(sr.ReadLine());
            Dictionary<string, object> dataSet = (Dictionary<string, object>)JsonData["weatherinfo"];
            string str = string.Format("{0}{1}的实况为：气温：{2}度\n风向：{3}{4}\t相对湿度：{5}", dataSet["time"].ToString(), dataSet["city"].ToString(), dataSet["temp"].ToString(), dataSet["WD"].ToString(), dataSet["WS"].ToString(), dataSet["SD"].ToString());
            return str;
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="weixinId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string SendTxtMessage(string jsonMsg)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token="; 
            string token = string.IsNullOrEmpty(accessTocken.Access_token) ? GetAccessToken() : accessTocken.Access_token;
            string result = SeatManage.SeatManageComm.HttpRequest.Post(url + token, jsonMsg);
            JsonReader reader = new JsonTextReader(new StringReader(result));
            while (reader.Read())
            {
                if ((string)reader.Value == "errcode")
                {
                    reader.Read();
                    if (reader.Value.ToString() == "42001")
                    {
                        GetAccessToken(); //有可能无限递归
                        return SendTxtMessage(jsonMsg);
                    }
                    else if (reader.Value.ToString() == "0")
                        return "";
                    else
                        return "error:" + reader.Value.ToString();
                }
            }
            return ""; 
 
        }
    }
}
