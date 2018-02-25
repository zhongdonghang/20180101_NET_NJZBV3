using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeatManage.ISystemTerminal.IStuLibSync;

namespace SeatManageWebV2.Code
{
    public class WorkSync
    {
        public SyncState State =  SyncState.None;//0-没有开始,1-正在运行,2-成功结束,3-失败结束 
        private int _Percent = 0;//完成百分比 
        private DateTime _StartTime;
        private DateTime _FinishTime;
        private DateTime _ErrorTime;
        private int _AddAmount = 0;
        private int _UpdateAmount = 0;
        private int _ErrorAmount = 0;
        /// <summary>
        /// 添加条数
        /// </summary>
        public int AddAmount
        {
            get { return _AddAmount; }
        }
        /// <summary>
        /// 更新条数
        /// </summary>
        public int UpdateAmount
        {
            get { return _UpdateAmount; }
        }
        /// <summary>
        /// 错误条数
        /// </summary>
        public int ErrorAmount
        {
            get { return _ErrorAmount; } 
        }
        /// <summary>
        /// 进度
        /// </summary>
        public int Percent
        {
            get { return _Percent; }
        }
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        /// <summary>
        /// 任务结束时间
        /// </summary>
        public DateTime FinishTime
        {
            get { return _FinishTime; }
            set { _FinishTime = value; }
        }
        /// <summary>
        /// 出错时间
        /// </summary>
        public DateTime ErrorTime
        {
            get { return _ErrorTime; }
            set { _ErrorTime = value; }
        }

        IStuLibSync StuLibSync;
        public WorkSync()
        {
            try
            {
                StuLibSync = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("IStuLibSync") as IStuLibSync; //SeatManage.InterfaceFactory.SystemTerminalFactory.CreateStuLibSync();
                StuLibSync.StuLibSyncSet = SeatManage.Bll.T_SM_SystemSet.GetStuLibSync();
                StuLibSync.Syncing += new EventHandleSync(StuLibSync_Syncing);
                StuLibSync.Synced += new EventHandleSync(StuLibSync_Synced);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        void StuLibSync_Synced(object sender, SyncPercentEventArgs e)
        {
            if (_UpdateAmount > 0)
            {
                State = SyncState.Fail;
                _FinishTime = DateTime.Now;
            }
            else
            {
                _FinishTime = DateTime.Now;
                State = SyncState.Success;
            }
        }

        void StuLibSync_Syncing(object sender, SyncPercentEventArgs e)
        {
            State = SyncState.Syncing;
            _Percent = e.Percent;
            if (e.Type == SyncType.Add)
            {
                if (e.State == SyncState.Success)
                {
                    _AddAmount += 1;
                }
                else if (e.State == SyncState.Fail)
                {
                    _ErrorAmount += 1;
                }
            }
            else if (e.Type == SyncType.Update)
            {
                if (e.State == SyncState.Success)
                {
                    _UpdateAmount += 1;
                }
                else if (e.State == SyncState.Fail)
                {
                    _ErrorAmount += 1;
                }
            }
        }

        System.Threading.Thread thread;
        public void runwork()
        {
            lock (this)
            {
                if (State !=  SyncState.Syncing)
                {
                    State = SyncState.Syncing;
                    _StartTime = DateTime.Now;
                    thread = new System.Threading.Thread(new System.Threading.ThreadStart(dowork));
                    thread.Start();
                }
            }
        }

        private void dowork()
        {
            try
            {
                State = SyncState.Syncing;
                StuLibSync.Sync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                _ErrorTime = DateTime.Now;
                _FinishTime = DateTime.Now;
                
            }
        }
        /// <summary>
        /// 页面关闭，释放同步操作的线程
        /// </summary>
        public void Dispose()
        {
            if (thread != null)
            {
                try
                {
                    thread.Abort();
                }
                catch { }
            }
        }
    }
}