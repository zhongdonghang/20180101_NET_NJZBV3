using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketLib
{
    /// <summary>
    /// 管理连接缓冲区的类，职责是为每一个连接维持一个接收数据的区域。
    /// 它的设计也采用了类似与池的技术，先实例化好多内存区域，
    /// 并把每一块的地址放入栈中，每执行依次pop时拿出一块区域来给SocketAsyncEventArgs对象作为Buffer.
    /// </summary>
    internal sealed class BufferManager:IDisposable
    {
        private Byte[] buffer;
        private Int32 bufferSize;
        private Int64 numSize;
        private Int32 currentIndex;
        private Stack<Int32> freeIndexPool;//空闲的indexpool

        internal BufferManager(Int32 numSize, Int32 bufferSize)
        {
            this.bufferSize = bufferSize;
            this.numSize = numSize;
            this.currentIndex = 0;
            this.freeIndexPool = new Stack<Int32>();
        }
        /// <summary>
        /// 释放一个buffer
        /// </summary>
        /// <param name="args"></param>
        internal void FreeBuffer(SocketAsyncEventArgs args)
        {
            this.freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }
        /// <summary>
        /// 初始化buffer，分配能容纳最大并发数的缓冲区
        /// </summary>
        internal void InitBuffer()
        {
            this.buffer = new Byte[this.numSize];
        }
        /// <summary>
        /// 设置一个buffer
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        internal Boolean SetBuffer(SocketAsyncEventArgs args)
        {
            if (this.freeIndexPool.Count > 0)
            {
                args.SetBuffer(this.buffer, this.freeIndexPool.Pop(), this.bufferSize);
            }
            else 
            {
                if ((this.numSize - this.bufferSize) < this.currentIndex)
                {
                    return false;
                }
                args.SetBuffer(this.buffer, this.currentIndex, this.bufferSize);
                this.currentIndex += this.bufferSize;
            }
            return true;
        }

        #region IDisposable Members

        public void Dispose()
        {
            buffer = null;
            freeIndexPool = null;
        }

        #endregion
    }
}
