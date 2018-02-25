using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ISystemTerminal.IStuLibSync;

namespace SeatManage.StudentSync
{
    public class StudentSync : IStuLibSync
    {
        #region IStuLibSync 成员

        public event EventHandleSync Syncing;

        public event EventHandleSync Synced;

        private IReaderSource readerSource = null;
        public StudentSync()
        {
            readerSource = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("IReaderSource") as IReaderSource ; //ReaderSourceAssemblyFactory.CreateReaderSourceAssembly();
        }

        public bool LinkDataSourceTest()
        {
            if (readerSource == null)
            {
                throw new Exception("尚未初始化读者信息库访问对象");
            }
            else
            {
                if (_StuLibSyncSet != null)
                {
                    readerSource.StuLibSyncSet = _StuLibSyncSet;
                }
                else
                {
                    throw new Exception("尚未初始化读者信息同步设置属性");
                }
                return readerSource.LinkDataSourceTest();
            }
        }

        public void Sync()
        {
            try
            {

                List<SeatManage.ClassModel.ReaderInfo> readerList = null;
                try
                {
                    if (_StuLibSyncSet != null)
                    {
                        readerSource.StuLibSyncSet = _StuLibSyncSet;
                    }
                    else
                    {
                        throw new Exception("尚未初始化读者信息同步设置属性");
                    }
                    readerList = readerSource.GetSourceReaderInfo();
                    if (readerList.Count < 500)
                    {
                        throw new Exception(string.Format("读者信息异常，仅有：{0}条数据",readerList.Count));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("同步已终止，获取读者信息失败：{0}", ex.Message));
                }
                //清空数据库然后再执行添加操作
                SeatManage.Bll.T_SM_Reader.Clear();
                for (int i = 0; i < readerList.Count; i++)
                {
                    try
                    {
                        SeatManage.Bll.T_SM_Reader.Add(readerList[i]);
                        if (Syncing != null)
                        {
                            SyncPercentEventArgs args = new SyncPercentEventArgs();
                            args.Percent = (int)(((double)i / (double)readerList.Count) * 100);
                            args.State = SyncState.Success;
                            args.Type = SyncType.Add;
                            Syncing(this, args);
                        }
                    }
                    catch
                    {
                        if (Syncing != null)
                        {
                            SyncPercentEventArgs args = new SyncPercentEventArgs();
                            args.Percent = (int)((double)i / (double)readerList.Count) * 100;
                            args.State = SyncState.Fail;
                            args.Type = SyncType.Add;
                            Syncing(this, args);
                        }
                    }
                }
                if (Synced != null)
                {
                    SyncPercentEventArgs args = new SyncPercentEventArgs();
                    args.Percent = 100;
                    args.State = SyncState.Success;
                    args.Type = SyncType.None;
                    Synced(this, args);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        SeatManage.ClassModel.StuLibSyncSetting _StuLibSyncSet = null;
        public ClassModel.StuLibSyncSetting StuLibSyncSet
        {
            get
            {
                return _StuLibSyncSet;
            }
            set
            {
                _StuLibSyncSet = value;
            }
        }

        #endregion


    }
}
