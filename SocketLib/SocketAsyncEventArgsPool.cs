using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SocketLib
{
    /// <summary>
    ///  类真正的连接池类，这个类为server提供一个可用的用户连接，并且维持这个连接直到用户断开，并把不用的连接放回连接池中供下一用户连接。
    /// </summary>
    internal sealed class SocketAsyncEventArgsPool:IDisposable
    {
        /// <summary>
        /// 用来存放空闲的连接的，使用时pop出来，使用完后push进去。
        /// </summary>
        internal Stack<SocketAsyncEventArgsWithId> pool;
        /// <summary>
        /// 用来存放正在使用的连接的，key是用户标识，设计的目的是为了统计在线用户数目和查找相应用户的连接，
        /// </summary>
        internal IDictionary<string, SocketAsyncEventArgsWithId> busypool;
        /// <summary>
        /// 这是一个存放用户标识的数组，起一个辅助的功能。
        /// </summary>
        private List<string> keys=new List<string>();
        /// <summary>
        /// 返回连接池中可用的连接数。
        /// </summary>
        internal Int32 Count
        {
            get
            {
                lock (this.pool)
                {
                    return this.pool.Count;
                }
            }
        }
        /// <summary>
        /// 返回在线用户的标识列表。
        /// </summary>
        internal List<string> OnlineUID
        {
            get
            { 
               return keys;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">并发数</param>
        internal SocketAsyncEventArgsPool(Int32 capacity)
        { 
            this.pool = new Stack<SocketAsyncEventArgsWithId>(capacity);
            this.busypool = new Dictionary<string, SocketAsyncEventArgsWithId>(capacity);
        }
        /// <summary>
        /// 用于获取一个可用连接给用户。
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        internal SocketAsyncEventArgsWithId Pop(string uid)
        {
            if (uid == string.Empty || uid == "")
                return null;
            SocketAsyncEventArgsWithId si = null;
            lock (this.pool)
            {
                si = this.pool.Pop();
            }
            si.UID = uid;
            si.State = true;    //mark the state of pool is not the initial step
            si.UserOnLineCounter = 0;
            busypool.Add(uid, si);
            keys.Add(uid);
            return si;
        }
        /// <summary>
        /// 把一个使用完的连接放回连接池。
        /// </summary>
        /// <param name="item"></param>
        internal void Push(SocketAsyncEventArgsWithId item)
        {
            if (item == null)
                throw new ArgumentNullException("SocketAsyncEventArgsWithId对象为空");
            if (item.State == true)
            {
                if (busypool.Keys.Count != 0)
                {
                    if (busypool.Keys.Contains(item.UID))
                    {
                        busypool.Remove(item.UID); 
                        keys.Remove(item.UID); 
                    }
                    else
                        throw new ArgumentException("SocketAsyncEventWithId不在忙碌队列中");
                }
                else
                    throw new ArgumentException("忙碌队列为空");
            }
            item.UID = "-1";
            item.State = false;
            lock (this.pool)
            {
                this.pool.Push(item);
            }
        }
        /// <summary>
        /// 查找在线用户连接，返回这个连接。
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        internal SocketAsyncEventArgsWithId FindByUID(string uid)
        {
            SocketAsyncEventArgsWithId si = null;
            if (busypool.ContainsKey(uid))
            {
                si = busypool[uid];
            }
            return si; 
        }
        /// <summary>
        /// 判断某个用户的连接是否在线。
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        internal bool BusyPoolContains(string uid)
        {
            lock (this.busypool)
            {
                return busypool.Keys.Contains(uid);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            pool.Clear();
            busypool.Clear();
            pool = null;
            busypool = null;
        }

        #endregion
    }
    /// <summary>
    /// 该类是一个用户的连接的最小单元，也就是说对一个用户来说有两个SocketAsyncEventArgs对象，
    /// 这两个对象是一样的，但是有一个用来发送消息，一个接收消息，
    /// 这样做的目的是为了实现双工通讯，提高用户体验。默认的用户标识是"-1”，
    /// 状态是false表示不可用
    /// </summary>
    internal sealed class SocketAsyncEventArgsWithId:IDisposable
    {
        private string uid = "-1";
        private bool state = false;
        private MySocketAsyncEventArgs receivesaea;
        private MySocketAsyncEventArgs sendsaea;
        private int userOnLineCounter = -1;
        private RequestHandler _MsgHandler = null;
        /// <summary>
        /// 对二进制消息分包处理的类
        /// </summary>
        internal RequestHandler MsgHandler
        {
            get { return _MsgHandler; }
            set { _MsgHandler = value; }
        }

        /// <summary>
        /// 为0表示用户在线，如果值>3,表示用户掉线。
        /// </summary>
        internal int UserOnLineCounter
        {
            get { return userOnLineCounter; }
            set { userOnLineCounter = value; }
        }

        /// <summary>
        /// 用户标识，跟MySocketAsyncEventArgs的UID是一样的，在对SocketAsycnEventArgsWithId的UID属性赋值的时候也对MySocketAsyncEventArgs的UID属性赋值。
        /// </summary>
        internal string UID
        {
            get { return uid; }
            set
            {
                uid = value;
                ReceiveSAEA.UID = value;
                SendSAEA.UID = value;
            }
        }
        /// <summary>
        /// 表示连接的可用与否，一旦连接被实例化放入连接池后State即变为True
        /// </summary>
        internal bool State 
        {
            get { return state; }
            set { this.state = value; }
        }
        /// <summary>
        /// 接收数据的SocketAsyncEventArgs
        /// </summary>
        internal MySocketAsyncEventArgs ReceiveSAEA
        {
            set { receivesaea = value; }
            get { return receivesaea; }
        }
        /// <summary>
        /// 发送数据的SocketAsyncEventArgs
        /// </summary>
        internal MySocketAsyncEventArgs SendSAEA
        {
            set { sendsaea = value; }
            get { return sendsaea; }
        }

        //constructor
        internal SocketAsyncEventArgsWithId()
        {
            ReceiveSAEA = new MySocketAsyncEventArgs("Receive");
            SendSAEA = new MySocketAsyncEventArgs("Send");
            _MsgHandler = new RequestHandler();
           
        }

        #region IDisposable Members

        public void Dispose()
        {
            ReceiveSAEA.Dispose();
            SendSAEA.Dispose();
            _MsgHandler = null;
        }

        #endregion
    }
    internal sealed class MySocketAsyncEventArgs : SocketAsyncEventArgs{
        internal string UID;
        private string Property;
        internal MySocketAsyncEventArgs(string property){
            this.Property = property;
        }
    }
}
