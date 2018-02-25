using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketMsgData
{
    [Serializable]
    public class SocketMsgBase
    {
        TcpSeatManageSubSystem _SubSystem;
        public TcpSeatManageSubSystem SubSystem
        {
            get { return _SubSystem; }
            set { _SubSystem = value; }
        }
        private string _Method;
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName
        {
            get { return _Method; }
            set { _Method = value; }
        }
        private string _Sender;
        /// <summary>
        /// 请求发起者
        /// </summary>
        public string Sender
        {
            get { return _Sender; }
            set { _Sender = value; }
        }
        private string _Target;
        /// <summary>
        /// 请求目标
        /// </summary>
        public string Target
        {
            get { return _Target; }
            set { _Target = value; }
        }
        private string _ErrorMsg;
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set { _ErrorMsg = value; }
        }
        /// <summary>
        /// 连接类型0：端连接  1：常连接
        /// </summary>
        public string LinkType
        {
            get;
            set;
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public TcpMsgDataType MsgType
        {
            get;
            set;
        }
        /// <summary>
        /// 请求的方法。
        /// </summary>
        public RequestMethodEnum RequestMethod
        {
            get;
            set;
        }
        /// <summary>
        /// 请求的方法。
        /// </summary>
        public string RequestMethodType
        {
            get;
            set;
        }
    }

    [Serializable]
    public class SocketRequest : SocketMsgBase
    {

        List<object> parameters = new List<object>();
        /// <summary>
        /// 参数
        /// </summary>
        public List<object> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

    }

    /// <summary>
    /// socket请求的响应结果
    /// </summary>
    [Serializable]
    public class SocketResponse : SocketMsgBase
    {

        private object _Result;
        /// <summary>
        /// 获取的结果
        /// </summary>
        public object Result
        {
            get { return _Result; }
            set { _Result = value; }
        }


    }
    /// <summary>
    /// Tcp客户端类型
    /// </summary>
    [Serializable]
    public enum TcpSeatManageSubSystem
    {
        None,
        WeiXinSeatBespeak,
        SchoolService,
        SocketListener,
        SocketClient,
        AndroidApp,
    }
    /// <summary>
    /// 消息类型
    /// </summary>
    [Serializable]
    public enum TcpMsgDataType
    {
        None,
        /// <summary>
        /// 要求服务器转发
        /// </summary>
        Relay,
        /// <summary>
        /// 客户端编号
        /// </summary>
        ClientToken,
        /// <summary>
        /// 心跳包
        /// </summary>
        Heartbeat,
        /// <summary>
        /// 微信消息通知
        /// </summary>
        WeiXinNotice,
        /// <summary>
        /// 消息推送
        /// </summary>
        MsgPush
    }
    /// <summary>
    /// 请求执行方法的枚举。
    /// </summary>
    public enum RequestMethodEnum
    {
        /// <summary>
        /// 验证用户密码，并返回读者信息
        /// </summary>
        GetReaderAccount,
        /// <summary>
        /// 获取可预约的阅览室
        /// </summary>
        GetOpenBespeakRooms,
        /// <summary>
        /// 提交预约信息
        /// </summary>
        SubmitBespeakInfo,
        /// <summary>
        /// 提预约信息 用于提交自定义预约信息。 
        /// </summary>
        SubmitBespeakInfoCustomTime,
        /// <summary>
        /// 取消预约记录
        /// </summary>
        CancelBespeakLog,
        /// <summary>
        /// 获取扫描的座位信息
        /// </summary>
        GetScanCodeSeatInfo,
        /// <summary>
        /// 扫码更换座位的服务
        /// </summary>
        ChangeSeat,
        /// <summary>
        /// 释放座位操作
        /// </summary>
        FreeSeat,
        /// <summary>
        /// 获取读者实时记录
        /// </summary>
        GetReaderActualTimeRecord,
        /// <summary>
        /// 获取读者预约记录
        /// </summary>
        GetReaderBespeakRecord,
        /// <summary>
        /// 获取读者的黑名单记录
        /// </summary>
        GetReaderBlacklistRecord,

        /// <summary>
        /// 获取读者的违规记录
        /// </summary>
        GetViolateDiscipline,
        /// <summary>
        /// 获取读者的选座记录
        /// </summary>
        GetReaderChooseSeatRecord,
        /// <summary>
        /// 获取阅览室座位使用情况
        /// </summary>
        GetAllRoomSeatUsedInfo,
        /// <summary>
        /// 获取阅览室可预约的座位信息
        /// </summary>
        GetSeatsBespeakInfoByRoomNum,
        /// <summary>
        /// 设置暂时离开
        /// </summary>
        ShortLeave,
        /// <summary>
        ///获取读者消息提醒
        /// </summary>
        GetReaderNotice,
        /// <summary>
        /// 获取读者座位状态
        /// </summary>
        GetReaderSeatState
    }
}
