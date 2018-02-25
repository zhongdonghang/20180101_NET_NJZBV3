using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IService
{
    /// <summary>
    /// windows服务接口
    /// </summary>
    public interface IService : System.IDisposable
    {
        /// <summary>
        /// 开始服务
        /// </summary>
        void Start();
        /// <summary>
        /// 结束服务
        /// </summary>
        void Stop();
         
    }
}
