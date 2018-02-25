using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.IWCFService
{
    public partial interface IAdvertManageService
    {
        /// <summary>
        /// 根据Id获取播放列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.AMS_PlayListModel GetPlaylistById(int id);
        /// <summary>
        /// 根据编号获取视频播放列表
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.AMS_PlayListModel GetPlaylistByNum(string number);
        /// <summary>
        /// 获取所有播放列表
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [OperationContract]
        List<Model.AMS_PlayListModel> GetPlaylist( );
        /// <summary>
        /// 添加播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult AddPlayList(AdvertManage.Model.AMS_PlayListModel model);
        /// <summary>
        /// 更新播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult UpdatePlayList(AdvertManage.Model.AMS_PlayListModel model);
        /// <summary>
        /// 根据Id删除播放列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
          [OperationContract]
        Model.Enum.HandleResult DeletePlayList(int id);

          /// <summary>
          /// 根据Id获取Md5播放列表
          /// </summary>
          /// <param name="id"></param>
          /// <returns></returns>
          [OperationContract]
          AdvertManage.Model.AMS_PlayListMd5Model GetMD5PlaylistById(int id);
          /// <summary>
          /// 根据编号获取Md5视频播放列表
          /// </summary>
          /// <param name="number"></param>
          /// <returns></returns>
          [OperationContract]
          AdvertManage.Model.AMS_PlayListMd5Model GetMd5PlaylistByNum(string number);
          /// <summary>
          /// 获取所有Md5播放列表
          /// </summary>
          /// <param name="number"></param>
          /// <returns></returns>
          [OperationContract]
          List<Model.AMS_PlayListMd5Model> GetMd5Playlist();
          /// <summary>
          /// 添加Md5播放列表
          /// </summary>
          /// <param name="model"></param>
          /// <returns></returns>
          [OperationContract]
          Model.Enum.HandleResult AddMd5PlayList(AdvertManage.Model.AMS_PlayListMd5Model model);
          /// <summary>
          /// 更新Md5播放列表
          /// </summary>
          /// <param name="model"></param>
          /// <returns></returns>
          [OperationContract]
          Model.Enum.HandleResult UpdateMd5PlayList(AdvertManage.Model.AMS_PlayListMd5Model model);

    }
}
