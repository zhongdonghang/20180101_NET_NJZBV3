using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;
using SeatManage.EnumType; 
namespace SeatManage.IWCFService
{ 
    public partial interface ISeatManageService : IExceptionService
    {
        #region 读者提示相关操作

        /// <summary>
        /// 根据学号和状态获取有效的进出记录
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ReaderNoticeInfo> GetReaderNoticeByCardNoStatus(string cardNo,LogStatus IsRead);
        /// <summary>
        /// 添加读者消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddReaderNotice(ReaderNoticeInfo model);
        /// <summary>
        /// 更新读者消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        HandleResult UpdateReaderNotice(ReaderNoticeInfo model);
        /// <summary>
        /// 设置已读
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        [OperationContract]
        string SetReaderNoteRead(List<SeatManage.ClassModel.ReaderNoticeInfo> modelList);
        #endregion

    }
}
