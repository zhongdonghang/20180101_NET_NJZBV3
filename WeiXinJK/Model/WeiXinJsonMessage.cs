using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;
using System.IO;

namespace WeiXinJK.Model
{
    public class WeiXinJsonTxtMessage
    {
        public WeiXinJsonTxtMessage(string openid,string context)
        {
            string str = "{ 'touser':'" + openid + "','msgtype':'text','text':{'content':'" + context + "'}}";
            str = str.Replace('\'', '\"');
            mess = str;
        }
        private string mess;
        public string Getmess()
        {
            return mess;
        }
    }
    public class WeiXinJsonTxtToAll
    {
        public WeiXinJsonTxtToAll(string groupID,string context)
        {
            string str=@"{
                            'filter':
                                    {
                                        'group_id':'"+groupID+@"'
                                    },
                            'msgtype':'text',
                            'text':
                                    {
                                        'content':'"+context+@"'
                                    }
                          }";
            str = str.Replace('\'', '\"');
            str = str.Replace("\r\n", "");
            str = str.Replace(" ", "");
            mess = str;
        }
        private string mess;
        public string Getmess()
        {
            return mess;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public  class WeiXinJsonArticle///待测
    {
        public WeiXinJsonArticle(string openid, List<WeiXinAdvertse> articles)
        {
            if (articles.Count > 10)
                mess = "";
            else
            {
                string art = "";
                for (int i = 0; i < articles.Count; i++)
                {
                    art += articles[i].ContentXML;
                    if (i < articles.Count - 1)
                    {
                        art += ",";
                    }

                }
                string str =@"{
                                  'touser':'"+openid+@"',
                                  'msgtype':'news',
                                  'news':
                                    {
                                        'articles': ["+art+@"]
                                    }
                              }";
                str = str.Replace('\'', '\"');
                str = str.Replace("\r\n", "");
                str = str.Replace(" ", "");
                mess = str;
            }
            
        }
        private string mess;
        /// <summary>
        /// 用之前先判断返回值是否为空
        /// </summary>
        /// <returns></returns>
        public string Getmess()
        {
            return mess;
        }

        public static  string AdvertiseToJson(WeiXinAdvertse advert)
        {
            string json = "{ 'title':'" + advert.Title + "','description':'"+advert.Description+"','url':'" + advert.Url + "','picurl':'" + advert.Image + "'}";
            json = json.Replace('\'', '\"');
            return json;
        }
    }
    public class WeiXinJsonArticleToAll///待测
    {
        public WeiXinJsonArticleToAll(string groupID, List<string> articles)
        {
            if (articles.Count > 10)
                mess = "";
            else
            {
                string art = "";
                for (int i = 0; i < articles.Count; i++)
                {
                    art += articles[i].ToString()+",";

                }
                string arts = art.Substring(0,art.Length-1);
                string str =@"{
                                'filter':{'group_id':'"+groupID+@"'},
                                'msgtype':'news',
                                'news':
                                    {
                                        'articles': [" + arts + @"]
                                    }
                              }";
                str = str.Replace('\'', '\"');
                str = str.Replace("\r\n", "");
                str = str.Replace(" ", "");
                mess = str;
            }
        }
        /// <summary>
        /// 广告JOSN
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        public static string AdvertiseToJson(WeiXinAdvertse advert)
        {
            string json = @"{ 
                                 'title':'"+advert.Title+@"',
                                 'description':' ',
                                 'url':'"+advert.Url+@"',
                                 'picurl':'"+advert.Image+@"'
                            }";
            json = json.Replace('\'', '\"');
            json = json.Replace("\r\n", "");
            json = json.Replace(" ", "");
            return json;
        }
        private string mess;
        /// <summary>
        /// 用之前先判断返回值是否为空
        /// </summary>
        /// <returns></returns>
        public string Getmess()
        {
            return mess;
        }
    }
   
}
