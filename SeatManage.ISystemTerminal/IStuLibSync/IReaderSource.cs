using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ISystemTerminal.IStuLibSync
{
    /// <summary>
    /// 读者信息库操作
    /// </summary>
    public interface IReaderSource
    {
        /// <summary>
        /// 同步设置
        /// </summary>
        ClassModel.StuLibSyncSetting StuLibSyncSet
        {
            get;
            set;
        }
        /// <summary>
        /// 获取读者信息
        /// </summary>
        /// <returns></returns>
        List<ClassModel.ReaderInfo> GetSourceReaderInfo();  
        /// <summary>
        /// 读者信息库连接测试
        /// </summary>
        /// <returns></returns>
        bool LinkDataSourceTest();
        /// <summary>
        /// 通过卡片物理编号从源数据库获取读者信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        ClassModel.ReaderInfo GetSourceReaderInfoByCardId(string cardId);
        /// <summary>
        /// 通过学号从源数据获取读者信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        ClassModel.ReaderInfo GetSourceReaderInfoByCardNo(string cardNo);

    }
}
