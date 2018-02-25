using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaiduPush
{
    public enum Device_Type
    {
        Android = 3,
        IOS = 4,
    }

    public class PushOptions
    {
        /// <summary>
        /// 推送到单台设备的消息参数
        /// </summary>
        /// <param name="channel_id">必须！唯一对应一台设备 ，为端上初始化channel成功之后返回的channel_id  </param>
        /// <param name="msg_type">消息类型，取值如下：0：消息；1：通知。默认为0</param>
        /// <param name="msg">消息内容，json格式</param>
        /// <param name="msg_expires">相对于当前时间的消息过期时间，单位为秒，0~604800(86400*7)，默认为5小时(18000秒)</param>
        /// <param name="deploy_status">取值为：1：开发状态；2：生产状态； 若不指定，则默认设置为生产状态。</param>
        /// <param name="apikey">必须！应用的api key,用于标识应用,</param>
        /// <param name="timestamp">必须！用户发起请求时的unix时间戳。本次请求签名的有效时间为该时间戳向后10分钟</param>
        /// <param name="expires">非必须   用户指定本次请求签名的失效时间。格式为unix时间戳形式，用于防止 replay 型攻击。为保证防止 replay攻击算法的正确有效，请保证客户端系统时间正确</param>
        /// <param name="device_type">非必须   当一个应用同时支持多个设备平台类型（比如：Android和iOS），请务必设置该参数。其余情况可不设置</param>
        public PushOptions(string channel_id, uint? msg_type, string msg,   uint? deploy_status, string apikey, uint? timestamp, uint? expires, Device_Type device_type)
        {
            this.channel_id = channel_id;
            this.msg_type = msg_type;
            this.msg = msg;
            this.msg_expires = msg_expires;
            this.deploy_status = deploy_status;
            this.apikey = apikey;
            this.timestamp = timestamp;
            this.expires = expires;
            this.device_type = (uint)device_type;
        }
         /// <summary>
        /// 推送广播消息
         /// </summary>
        /// <param name="msg_type">否 取值如下：0：消息；1：通知。默认为0 </param>
        /// <param name="msg">是   消息内容，json格式 </param>
        /// <param name="msg_expires">否  0~604800(86400*7)，默认为5小时(18000秒) 消息过期时间，单位为秒 </param>
        /// <param name="deploy_status">否  取值为：1：开发状态；2：生产状态； 若不指定，则默认设置为生产状态。 设置iOS应用的部署状态，仅iOS应用推送时使用 </param>
        /// <param name="send_time">否  指定的实际发送时间，必须在当前时间60s以外，1年以内 定时推送，用于指定的实际发送时间 </param>
        /// <param name="apikey">必须！应用的api key,用于标识应用,</param>
        /// <param name="timestamp">必须！用户发起请求时的unix时间戳。本次请求签名的有效时间为该时间戳向后10分钟</param>
        /// <param name="expires">用户指定本次请求签名的失效时间。格式为unix时间戳形式，用于防止 replay 型攻击。为保证防止 replay攻击算法的正确有效，请保证客户端系统时间正确</param>
        /// <param name="device_type">当一个应用同时支持多个设备平台类型（比如：Android和iOS），请务必设置该参数。其余情况可不设置</param>
        public PushOptions(uint? msg_type, string msg, uint? msg_expires, uint? deploy_status, uint? send_time, string apikey, uint? timestamp, uint? expires, Device_Type device_type)
        { 
            this.msg_type = msg_type;
            this.msg = msg;
            this.msg_expires = msg_expires;
            this.deploy_status = deploy_status;
            this.send_time = send_time;

            this.apikey = apikey;
            this.timestamp = timestamp;
            this.expires = expires;
            this.device_type = (uint)device_type;
        }
        /// <summary>
        /// 推送消息或通知给指定的标签
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="msg_type">否 取值如下：0：消息；1：通知。默认为0 </param>
        /// <param name="msg">是   消息内容，json格式 </param>
        /// <param name="msg_expires">否  0~604800(86400*7)，默认为5小时(18000秒) 消息过期时间，单位为秒 </param>
        /// <param name="deploy_status">否  取值为：1：开发状态；2：生产状态； 若不指定，则默认设置为生产状态。 设置iOS应用的部署状态，仅iOS应用推送时使用 </param>
        /// <param name="send_time">否  指定的实际发送时间，必须在当前时间60s以外，1年以内 定时推送，用于指定的实际发送时间 </param>
        /// <param name="apikey">必须！应用的api key,用于标识应用,</param>
        /// <param name="timestamp">必须！用户发起请求时的unix时间戳。本次请求签名的有效时间为该时间戳向后10分钟</param>
        /// <param name="expires">用户指定本次请求签名的失效时间。格式为unix时间戳形式，用于防止 replay 型攻击。为保证防止 replay攻击算法的正确有效，请保证客户端系统时间正确</param>
        /// <param name="device_type">当一个应用同时支持多个设备平台类型（比如：Android和iOS），请务必设置该参数。其余情况可不设置</param>
        public PushOptions(string tag,uint? msg_type, string msg, uint? msg_expires, uint? deploy_status, uint? send_time, string apikey, uint? timestamp, uint? expires, Device_Type device_type)
        {
            this.type = 1;
            this.tag = tag;

            this.msg_type = msg_type;
            this.msg = msg;
            this.msg_expires = msg_expires;
            this.deploy_status = deploy_status;
            this.send_time = send_time;

            this.apikey = apikey;
            this.timestamp = timestamp;
            this.expires = expires;
            this.device_type = (uint)device_type;
        }

        /// <summary>
        /// 推送消息给批量设备（批量单播） 
        /// </summary>
        /// <param name="channel_ids">必须！ 一组channel_id（最多为一万个）组成的json数组字符串 对应一批设备  </param>
        /// <param name="msg_type">消息类型，取值如下：0：消息；1：通知。默认为0</param>
        /// <param name="msg">消息内容，json格式</param>
        /// <param name="msg_expires">相对于当前时间的消息过期时间，单位为秒，0~604800(86400*7)，默认为5小时(18000秒)</param>
        /// <param name="deploy_status">取值为：1：开发状态；2：生产状态； 若不指定，则默认设置为生产状态。</param>
        /// <param name="apikey">必须！应用的api key,用于标识应用,</param>
        /// <param name="timestamp">必须！用户发起请求时的unix时间戳。本次请求签名的有效时间为该时间戳向后10分钟</param>
        /// <param name="expires">非必须   用户指定本次请求签名的失效时间。格式为unix时间戳形式，用于防止 replay 型攻击。为保证防止 replay攻击算法的正确有效，请保证客户端系统时间正确</param>
        /// <param name="device_type">非必须   当一个应用同时支持多个设备平台类型（比如：Android和iOS），请务必设置该参数。其余情况可不设置</param>
        public PushOptions(string channel_ids, uint? msg_type, string msg, uint? msg_expires, uint? deploy_status, string apikey, uint? timestamp, uint? expires)
        {
            this.channel_ids = channel_ids;
            this.msg_type = msg_type;
            this.msg = msg;
            this.msg_expires = msg_expires;
            this.deploy_status = deploy_status;
            this.apikey = apikey;
            this.timestamp = timestamp;
            this.expires = expires;
            this.device_type = (uint)device_type;
        }
     
        public string channel_ids { get; set; }
       public string tag { get; set; }
        public uint? type { get; set; }

        public uint? send_time { get; set; }
        public string channel_id { get; set; }
        public uint? msg_type { get; set; }
       public string msg { get; set; }
       public uint? msg_expires { get; set; }
       public uint? deploy_status { get; set; }

       public string apikey { get; set; }
       public uint? timestamp { get; set; }
       public uint? expires { get; set; }
       public uint device_type { get; set; }


        //public PushOptions( 
        //     string apikey,
        //     string user_id,
        //     uint push_type,
        //     string channel_id,
        //     string tag,
        //     uint device_type,
        //     uint msg_type,
        //     string msg, 
        //     uint message_expires,
        //     uint timestamp,
        //     uint expires,
        //     uint v)
        //{
        //    this.apikey = apikey;
        //    this.channel_id = channel_id;
        //    this.tag = tag;
        //    this.device_type = device_type;
        //    this.msg_type = msg_type;
        //    this.msg = msg;
        //    this.msg_expires = message_expires;
        //    this.timestamp = timestamp;
        //    this.expires = expires;

        //}

        ////单播
        //public PushOptions( 
        //    string apikey,
        //    string user_id,
        //    string channel_id, 
        //    string messages, 
        //    uint timestamp
        //    )
        //{
        //    this.apikey = apikey;
        //    this.channel_id = channel_id;
        //    this.msg = messages;
        //    this.timestamp = timestamp;

        //    this.device_type = device_type;
        //}
        ////组播
        //public PushOptions( 
        //    string apikey,
        //    string tag, 
        //    string messages, 
        //    uint timestamp
        //    )
        //{
        //    this.apikey = apikey;
        //    this.tag = tag;
        //    this.msg = messages;
        //    this.timestamp = timestamp;

        //    this.device_type = device_type;
        //}

        ////广播
        //public PushOptions( 
        //   string apikey, 
        //   string messages, 
        //   uint timestamp
        //   )
        //{
        //    this.apikey = apikey;
        //    this.msg = messages;
        //    this.timestamp = timestamp;

        //    this.device_type = device_type;
        //}


        //public string apikey { get; set; }	//string 	是 	访问令牌，明文AK，可从此值获得App的信息，配合sign中的sk做合法性身份认证。 

        //public string channel_id { get; set; }	//string 	否 	通道标识
        //public string tag { get; set; }	//string 	否 	标签名称，不超过128字节
        ///// <summary>
        ///// uint 	否 	设备类型，取值范围为：1～5
        /////
        /////                                  百度Channel支持多种设备，各种设备的类型编号如下：（支持某种组合，如：1,2,4:）
        /////
        /////                              1：浏览器设备；
        /////
        /////                                2：PC设备；

        /////                               3：Andriod设备；

        /////                              4：iOS设备；

        /////                                 5：Windows Phone设备；
        /////
        /////                               如果存在此字段，则向指定的设备类型推送消息。 默认不区分设备类型。
        ///// </summary>
        //public uint? device_type { get; set; }
        ///// <summary>
        ///// uint 	否 	消息类型
        /////
        /////     0：消息（透传） 
        /////        1：通知 
        /////        默认值为0。
        ///// </summary>
        //public uint? msg_type { get; set; }
        //public string msg { get; set; } 	/*string 	是 	指定消息内容，单个消息为单独字符串，多个消息用json数组表示。

        //                                            如果有二进制的消息内容，请先做BASE64的编码。

        //                                            一次推送最多10个消息。

        //                                            注：当message_type=1且为Android端接收消息时，需按照以下格式：

        //                                            "{ 
        //                                               \"title\" : \"hello\" ,
        //                                               \"description\": \"hello\"
        //                                             }"

        //                                            说明：

        //                                                title : 通知标题，可以为空；如果为空则设为appid对应的Android应用名称。
        //                                                description：通知文本内容，不能为空，否则Android端上不展示。 */

        //public uint? msg_expires { get; set; }	//uint 	否 	指定消息的过期时间，默认为86400秒。必须和messages一一对应。
        //public uint timestamp { get; set; }	//uint 	是 	用户发起请求时的unix时间戳。本次请求签名的有效时间为该时间戳+10分钟。
        //public string sign { get; set; } 	//string 	是 	调用参数签名值，与apikey成对出现。
        //public uint? expires { get; set; }//	uint 	否 	用户指定本次请求签名的失效时间。格式为unix时间戳形式。

        //public uint? deploy_status { get; set; } //部署状态。指定应用当前的部署状态，可取值：1：开发状态 2：生产状态


    }
}
