using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.IWCFService
{

    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取读者信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="queryLog">是否查询相关记录</param>
        /// <returns></returns>
        [OperationContract]
        ReaderInfo GetReaderSeatState(string cardNo);
        /// <summary>
        /// 获取读者信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="queryLog">是否查询相关记录</param>
        /// <returns></returns>
        [OperationContract]
        ReaderInfo GetReader(string cardNo, bool queryLog);
        /// <summary>
        /// 以当前时间为标准 获取多长时间内的选座次数
        /// </summary>
        /// <param name="minute">分钟数</param>
        /// <returns></returns>
        [OperationContract]
        int GetReaderChooseSeatTimes(string cardNo, int minutes);
        /// <summary>
        /// 获取阅览室选座次数
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="minutes"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [OperationContract]
        int GetReaderChooseSeatTimesByReadingRoom(string cardNo, int minutes,string roomNo);
        /// <summary>
        /// 获取读者类型列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<string> GetReaderType();

        /// <summary>
        /// 根据卡片物理Id查询读者信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        [OperationContract]
        ReaderInfo GetReaderByCardId(string cardId);

        /// <summary>
        /// 根据卡片物理Id从源读者信息库获取读者信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        [OperationContract]
        ReaderInfo GetReaderByCardIdFromSource(string cardId);
        /// <summary>
        /// 根据学号从源读者信息库获取读者信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        [OperationContract]
        ReaderInfo GetReaderByCardNoFromSource(string cardNo);
        /// <summary>
        /// 清空读者信息库
        /// </summary>
        [OperationContract]
        void ClearReaderInfo();
        /// <summary>
        /// 添加一条读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult Add(ReaderInfo model);
        /// <summary>
        /// 添加大量记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult AddLotSize(List<ReaderInfo> modelList);
        /// <summary>
        /// 删除读者
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult DeleteReaderByCardNo(ReaderInfo model);
        /// <summary>
        /// 更新读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult UpdateReaderInfo(ReaderInfo model);

        /// <summary>
        /// 根据卡列号更新读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult UpdateReaderInfoByCardId(ReaderInfo model); 
    }
}
