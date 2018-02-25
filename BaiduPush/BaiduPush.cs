using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BaiduPush
{
    public class BaiduPush
    {

        public string httpMehtod { get; set; }
        public PushOptions opts { get; set; }
        public string url { get; set; }
        public string secret_key { get; set; }
        public BaiduPush(string httpMehtod, string secret_key)
        {
            this.httpMehtod = httpMehtod; 
            this.url = "http://api.tuisong.baidu.com/rest/3.0/";
            this.secret_key = secret_key;

        }


        /// <summary>
        /// 推送消息到单台设备
        /// </summary>
        /// <param name="opts">
        /// 1.channel_id    string  必须      必须为端上初始化channel成功之后返回的channel_id， 唯一对应一台设备 
        /// 2.msg_type      number  非必须    消息类型，取值如下：0：消息；1：通知。默认为0  
        /// 3.msg           string  必须      详情见BaiduPushNotification 消息内容，json格式 
        /// 4.msg_expires   number  非必须     0~604800(86400*7)，默认为5小时(18000秒) 相对于当前时间的消息过期时间，单位为秒 
        /// 5.deploy_status number  非必须     取值为：1：开发状态；2：生产状态； 若不指定，则默认设置为生产状态。 设置iOS应用的部署状态，仅iOS应用推送时使用 
        /// </param>
        /// <returns></returns>
        public string PushSingleDevice(PushOptions opts)
        {
            string postMethod = "push/single_device";
            var postStr = GetPushParameter(opts, postMethod);
            string result = requestUrl(postStr, postMethod);
            return result;
        }
        /// <summary>
        /// 推送广播消息
        /// </summary>
        /// <param name="opts">
        ///1.msg_type         number  否   取值如下：0：消息；1：通知。默认为0 消息类型 
        ///2.msg              string  是   详情见消息/通知数据格式 消息内容，json格式 
        ///3.msg_expires      number  否   0~604800(86400*7)，默认为5小时(18000秒) 消息过期时间，单位为秒 
        ///4.deploy_status    number  否   取值为：1：开发状态；2：生产状态； 若不指定，则默认设置为生产状态。 设置iOS应用的部署状态，仅iOS应用推送时使用 
        ///5.send_time        number  否   指定的实际发送时间，必须在当前时间60s以外，1年以内 定时推送，用于指定的实际发送时间 
        ///</param>
        /// <returns></returns>
        public string PushAll(PushOptions opts)
        {
            string postMethod = "push/all";
            var postStr = GetPushParameter(opts, postMethod);
            string result = requestUrl(postStr, postMethod);
            return result;
        }
        /// <summary>
        /// 推送组播消息
        /// </summary>
        /// <param name="opts">
        ///type             number  是 目前固定值为 1 推送的标签类型 
        ///tag              string  是 已创建的tag名称 标签名 
        ///msg_type         number  否 取值如下：0：消息；1：通知。默认为0 消息类型 
        ///msg              string  是 详情见消息/通知数据格式 消息内容，json格式 
        ///msg_expires      number  否 0~604800(86400*7)，默认为5小时(18000秒) 消息过期时间，单位为秒 
        ///deploy_status    number 否 取值为：1：开发状态；2：生产状态 若不指定，则默认设置为生产状态。 设置iOS应用的部署状态，仅iOS应用推送时使用 
        ///send_time        number  否 指定的实际发送时间，必须在当前时间60s以外，1年以内 定时推送，用于指定的实际发送时间  
        /// </param>
        /// <returns></returns>
        public string PushTags(PushOptions opts)
        {
            string postMethod = "push/tags";
            var postStr = GetPushParameter(opts, postMethod);
            string result = requestUrl(postStr, postMethod);
            return result;
        }
        /// <summary>
        /// 推送消息到给定的一组设备(批量单播)
        /// </summary>
        /// <param name="opts">
        ///channel_ids      string  是   一组channel_id（最多为一万个）组成的json数组字符串 对应一批设备 
        ///msg_type         number  否   取值如下：0：消息；1：通知。默认为0 消息类型 
        ///msg              string  是   详情见消息/通知数据格式 消息内容，json格式 
        ///msg_expires      number  否   1~86400，默认为1天(86400秒) 消息过期时间，单位为秒 
        ///topic_id         string  是   字母、数字及下划线组成，长度限制为1~128 分类主题名称  
        /// </param>
        /// <returns></returns>
        public string PushBatchDevice(PushOptions opts)
        {
            string postMethod = "push/batch_device";
            var postStr = GetPushParameter(opts, postMethod);
            string result = requestUrl(postStr, postMethod);
            return result;
        }
        /// <summary>
        /// 创建标签组
        /// </summary>
        /// <param name="opts">
        /// tag     string  是   1~128字节，但不能为‘default’ 标签名称 
        /// </param>
        /// <returns></returns>
        public string AppCreateTag(PushOptions opts)
        {
            string postMethod = "app/create_tag";
            var postStr = GetPushParameter(opts, postMethod);
            string result = requestUrl(postStr, postMethod);
            return result;
        }
        /// <summary>
        /// 向tag中批量添加设备
        /// </summary>
        /// <param name="opts">
        ///tag          string  是   1~128字节，但不能为‘default’ 标签名称 
        ///channel_ids  string  是   一组channel_id（最少1个，最多为10个）组成的json数组字符串 对应一批设备 
        ///</param>
        /// <returns></returns>
        public string TagAdd_devices(PushOptions opts)
        {
            string postMethod = "app/create_tag";
            var postStr = GetPushParameter(opts, postMethod);
            string result = requestUrl(postStr, postMethod);
            return result;
        }
        /// <summary>
        /// 请求对应的Url。
        /// </summary>
        /// <param name="postParameter">要发送的参数</param>
        /// <param name="postMethod">请求目标</param>
        /// <returns></returns>
        private string requestUrl(string postParameter, string postMethod)
        {
            string pushUrl = string.Format("{0}{1}", this.url, postMethod);
            byte[] data = Encoding.UTF8.GetBytes(postParameter);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            WebClient webClient = new WebClient();
            try
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
                webClient.Headers.Add("User-Agent", "BCCS_SDK/3.0 (Windows 7 Ultimate ) C#/4.0 (Baidu Push SDK for PHP v4.0) .net framework/4.0 ZEND/2.6.0");

                byte[] responseData = webClient.UploadData(pushUrl, "POST", data);//得到返回字符流  
                string srcString = Encoding.UTF8.GetString(responseData);//解码  
                return srcString;
            }
            catch (WebException ex)
            {
                Stream stream = ex.Response.GetResponseStream();
                string m = ex.Response.Headers.ToString();
                byte[] buf = new byte[256];
                stream.Read(buf, 0, 256);
                stream.Close();
                int count = 0;
                foreach (var b in buf)
                {
                    if (b > 0)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
                return Encoding.UTF8.GetString(buf, 0, count);
            }
        }

        //public string PushMessage(PushOptions opts)
        //{

        //    var postStr = GetPushParameter(opts); 
        //    byte[] data = Encoding.UTF8.GetBytes(postStr);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
        //    WebClient webClient = new WebClient();
        //    try
        //    {
        //        webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
        //        webClient.Headers.Add("User-Agent", "BCCS_SDK/3.0 (Windows 7 Ultimate ) C#/4.0 (Baidu Push SDK for PHP v4.0) .net framework/4.0 ZEND/2.6.0");

        //        byte[] responseData = webClient.UploadData(this.url, "POST", data);//得到返回字符流  
        //        string srcString = Encoding.UTF8.GetString(responseData);//解码  
        //        return "Post:" + postStr + "\r\n\r\n" + "Response:" + srcString;
        //    }
        //    catch (WebException ex)
        //    {
        //        Stream stream = ex.Response.GetResponseStream();
        //        string m = ex.Response.Headers.ToString();
        //        byte[] buf = new byte[256];
        //        stream.Read(buf, 0, 256);
        //        stream.Close();
        //        int count = 0;
        //        foreach (var b in buf)
        //        {
        //            if (b > 0)
        //            {
        //                count++;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //        return " Post:" + postStr + ex.Message + "\r\n\r\n" + Encoding.UTF8.GetString(buf, 0, count);
        //    }
        //}

        /// <summary>
        /// 获取参数字符串
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        private string GetPushParameter(PushOptions opts, string method)
        { 
            string url = string.Format("{0}{1}", this.url, method);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //将键值对按照key的升级排列
            var props = typeof(PushOptions).GetProperties().OrderBy(p => p.Name);
            foreach (var p in props)
            {
                if (p.GetValue( opts, null) != null)
                {
                    dic.Add(p.Name, p.GetValue(opts, null).ToString());
                }
            }
            //生成sign时，不能包含sign标签，所有移除
            dic.Remove("sign");
            var preData = new StringBuilder();
            foreach (var l in dic)
            {
                preData.Append(l.Key);
                preData.Append("=");
                preData.Append(l.Value);
            }
            //按要求拼接字符串，并urlencode编码
            var str = HttpUtility.UrlEncode(this.httpMehtod.ToUpper() +  url + preData.ToString() + this.secret_key, System.Text.Encoding.UTF8);
            var strSignUpper = new StringBuilder();
            int perIndex = 0;
            //字符串中百分号后面两位转换为大写
            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i].ToString();
                if (str[i] == '%')
                {
                    perIndex = i;
                }
                if (i - perIndex == 1 || i - perIndex == 2)
                {
                    c = c.ToUpper();
                }
                strSignUpper.Append(c);
            }
            strSignUpper = strSignUpper.Replace("(", "%28").Replace(")", "%29");
            var sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSignUpper.ToString(), "MD5").ToLower();

            //加入生成好的sign键值对
            dic.Add("sign", sign);
            var strb = new StringBuilder();
            //int tagIndex = 0;
            foreach (var l in dic)
            {
                strb.Append(l.Key);
                strb.Append("=");
                strb.Append(l.Value);
                strb.Append("&");
            }
            var postStr = strb.ToString().EndsWith("&") ? strb.ToString().Remove(strb.ToString().LastIndexOf('&')) : strb.ToString();
            return postStr;
        }
    }
}
