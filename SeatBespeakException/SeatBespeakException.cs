using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatBespeakException
{
    /// <summary>
    /// 用户不存在
    /// </summary>
    [Serializable]
    public class UserNotExist : Exception
    {
        public UserNotExist()
            : base("用户不存在")
        {

        } 
    }
    /// <summary>
    /// 登录异常错误提示
    /// </summary>
      [Serializable]
    public class LoginFailed : Exception
    {
        public LoginFailed(string message) :
            base(message)
        { 
            
        }
    }
    /// <summary>
    /// 远程服务访问失败
    /// </summary>
      [Serializable]
    public class RemoteServiceLinkFailed : Exception
    {
        public RemoteServiceLinkFailed()
            : base("远程服务访问失败")
        {

        }

        public RemoteServiceLinkFailed(string message)
            : base(message)
        { }
    }
    /// <summary>
    /// 读者操作失败。
    /// </summary>
      [Serializable]
    public class ReaderHandlerFailed : Exception
    {
        public ReaderHandlerFailed(string message)
            : base(message)
        { }
    }

    /// <summary>
    /// 预约座位失败
    /// </summary>
      [Serializable]
    public class BespeakSeatFailed : Exception
    {
        public BespeakSeatFailed(string message)
            : base(message)
        { 
            
        }
    }


}
