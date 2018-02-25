using System;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.InterfaceFactory;
using SeatManage.ISystemTerminal.IStuLibSync;
using SeatManage.SeatManageComm;

namespace SeatService.SyncService.Code
{
    public class ReaderSync
    {
        /// <summary>
        /// 添加的读者信息
        /// </summary>
        private int addAmount;
        /// <summary>
        /// 更新的读者信息
        /// </summary>
        private int updateAmount;
        /// <summary>
        /// 失败的读者信息
        /// </summary>
        private int filedAmount;
        /// <summary>
        /// 同步读者信息
        /// </summary>
        public void SysnceReaderInfo(ref bool isUpdate)
        {
            try
            {
                StuLibSyncSetting syncSet = T_SM_SystemSet.GetStuLibSync();
                if (syncSet == null)
                {
                    return;
                }
                if (syncSet.SyncMode != StudentSyncMode.OptionalSync)
                {
                    return;
                }
                if (DateTime.Compare(DateTime.Parse(ServiceDateTime.Now.ToShortDateString() + " " + syncSet.SyncTime), ServiceDateTime.Now) < 0)
                {
                    if (!isUpdate)
                    {
                        return;
                    }
                    addAmount = 0;
                    updateAmount = 0;
                    filedAmount = 0;
                    IStuLibSync stuLibSync;
                    try
                    {
                        stuLibSync = AssemblyFactory.CreateAssembly("IStuLibSync") as IStuLibSync;
                    }
                    catch
                    {
                        isUpdate = true;
                        throw;
                    }
                    stuLibSync.StuLibSyncSet = syncSet;
                    //同步开始执行事件
                    stuLibSync.Syncing += StuLibSync_Syncing;
                    //同步结束事件
                    stuLibSync.Synced += StuLibSync_Synced;
                    isUpdate = false;
                    stuLibSync.Sync();
                }
                else  //如果时间过了，重新变为true；
                {
                    isUpdate = true;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("读者信息同步：读者信息同步遇到异常：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 同步完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StuLibSync_Synced(object sender, SyncPercentEventArgs e)
        {
            if (
            e.State == SyncState.Fail)
            {
                WriteLog.Write("读者信息同步：同步失败。");
            }
            WriteLog.Write(string.Format("读者信息同步：同步完成，新增了{0}条，更新了{1}条，失败{2}条", addAmount, updateAmount, filedAmount));

        }
        /// <summary>
        /// 同步中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StuLibSync_Syncing(object sender, SyncPercentEventArgs e)
        {
            switch (e.Type)
            {
                case SyncType.Add:
                    if (e.State == SyncState.Success)
                    {
                        addAmount += 1;
                    }
                    else
                    {
                        filedAmount += 1;
                    }
                    break;
                case SyncType.Update:
                    if (e.State == SyncState.Success)
                    {
                        updateAmount += 1;
                    }
                    else
                    {
                        filedAmount += 1;
                    }
                    break;
            }
        }
    }
}
