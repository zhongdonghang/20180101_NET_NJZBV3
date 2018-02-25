using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 根据状态获取播放列表
        /// </summary>
        /// <param name="status">播放列表状态，None为查询所有的</param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.AMS_PlayList> GetPlayListByStatus(SeatManage.EnumType.LogStatus status);
        /// <summary>
        /// 根据是否失效获取播放列表
        /// </summary>
        /// <param name="status">播放列表状态，None为查询所有的</param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.AMS_PlayList> GetPlayListOverTime(SeatManage.EnumType.LogStatus status);
        /// <summary>
        /// 添加播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult AddPlaylist(SeatManage.ClassModel.AMS_PlayList model);
        /// <summary>
        /// 删除播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult DeletePlaylist(SeatManage.ClassModel.AMS_PlayList model);
        /// <summary>
        /// 更新播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         [OperationContract]
        EnumType.HandleResult UpdatePlaylist(SeatManage.ClassModel.AMS_PlayList model);
        /// <summary>
        /// 根据播放列表编号获取播放列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.AMS_PlayList GetPlayListByNum(string num);


        /// <summary>
        /// 根据状态获取包含MD5的播放列表
        /// </summary>
        /// <param name="status">播放列表状态，None为查询所有的</param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.AMS_PlayListMd5> GetMd5PlayListByStatus(SeatManage.EnumType.LogStatus status);
        /// <summary>
        /// 根据是否失效获取包含Md5的播放列表
        /// </summary>
        /// <param name="status">播放列表状态，None为查询所有的</param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.AMS_PlayListMd5> GetMd5PlayListOverTime(SeatManage.EnumType.LogStatus status);
        /// <summary>
        /// 添加MD5播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult AddMd5Playlist(SeatManage.ClassModel.AMS_PlayListMd5 model);
        /// <summary>
        /// 删除Md5播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult DeleteMd5Playlist(SeatManage.ClassModel.AMS_PlayListMd5 model);
        /// <summary>
        /// 更新Md5播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult UpdateMd5Playlist(SeatManage.ClassModel.AMS_PlayListMd5 model);
        /// <summary>
        /// 根据播放列表编号获取Md5播放列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.AMS_PlayListMd5 GetMd5PlayListByNum(string num);
    }
}
