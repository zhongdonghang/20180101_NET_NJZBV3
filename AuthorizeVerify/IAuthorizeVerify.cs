using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 授权验证接口
/// </summary>
public interface IAuthorizeVerify
{
    /// <summary>
    /// 验证
    /// </summary>
    /// <returns></returns>
    bool Verify(string userName, string pwd, string serviceName, string method);
}

