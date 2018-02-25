using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SeatManage.ClassModel;

namespace SeatManage.ISystemTerminal.IStuLibSync
{
    /// <summary>
    /// 读者信息同步处理事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void EventHandleSync(object sender, SyncPercentEventArgs e);
    /// <summary>
    /// 读者库同步接口，执行同步操作时，需要实现该接口
    /// </summary>
    public interface IStuLibSync
    {
        /// <summary>
        /// 同步正在进行，更新每一条数据时触发。
        /// </summary>
        event EventHandleSync Syncing;
        /// <summary>
        /// 同步结束
        /// </summary>
        event EventHandleSync Synced;
      

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="strConn"></param>
        /// <returns></returns>
        bool LinkDataSourceTest();

        /// <summary>
        /// 执行同步操作
        /// </summary>
        void Sync();

        /// <summary>
        /// 同步设置 
        /// </summary>
        StuLibSyncSetting StuLibSyncSet
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 同步进度
    /// </summary>
    public class SyncPercentEventArgs : EventArgs
    {
        private int _Percent;
        /// <summary>
        /// 同步进度
        /// </summary>
        public int Percent
        {
            get { return _Percent; }
            set { _Percent = value; }
        }
        private SyncType _Type;
        /// <summary>
        /// 同步方式：新增或者是更新
        /// </summary>
        public SyncType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private SyncState _State = SyncState.None;
        /// <summary>
        /// 同步状态
        /// </summary>
        public SyncState State
        {
            get { return _State; }
            set { _State = value; }
        }
    }
    /// <summary>
    /// 同步类型
    /// </summary>
    public enum SyncType
    {
        None = 0,
        /// <summary>
        /// 新增
        /// </summary>
        Add = 1,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 2
    }
    /// <summary>
    /// 同步状态
    /// </summary>
    public enum SyncState
    {
        /// <summary>
        /// 未同步
        /// </summary>
        None = 0,
        /// <summary>
        /// 正在进行
        /// </summary>
        Syncing = 1,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 2,
        /// <summary>
        /// 失败结束
        /// </summary>
        Fail = 3
    }
}
