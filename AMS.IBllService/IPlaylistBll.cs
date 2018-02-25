using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService
    {
        /// <summary>
        /// 获取全部的播放列表
        /// *异常处理：直接向上层抛出
        /// </summary>
        /// <returns>获取成功返回List</returns>
        [OperationContract]
        List<AMS.Model.AMS_PlayList> GetPlayListList();

        /// <summary>
        /// 新增播放列表
        /// *异常处理：直接在catch中return异常信息，自定义异常直接return自定义的错误
        /// *自定义异常：播放列表的编号不能重复
        /// </summary>
        /// <param name="model">播放列表的model，可以不包含ID和备注信息</param>
        /// <returns>成功返回null或""值，失败返回失败信息</returns>
        [OperationContract]
        string AddNewPlayList(AMS.Model.AMS_PlayList model);

        /// <summary>
        /// 更新播放列表
        /// *异常处理：直接在catch中return异常信息，自定义异常直接return自定义的错误
        /// *自定义异常：播放列表的编号不能其他播放列表重复
        /// </summary>
        /// <param name="model">播放列表的model，ID不能为空</param>
        /// <returns>成功返回null或""值，失败返回失败信息</returns>
        [OperationContract]
        string UpdatePlayList(AMS.Model.AMS_PlayList model);

        /// <summary>
        /// 删除播放列表
        /// *异常处理：直接在catch中return异常信息，自定义异常直接return自定义的错误
        /// *自定义异常：判断当前播放列表是否正在被下发
        /// </summary>
        /// <param name="model">需要删除的播放列表的model，只需要删除列表的ID其它属性可以为空</param>
        /// <returns>成功返回null或""值，失败返回失败信息</returns>
        [OperationContract]
        string DeletePlayList(AMS.Model.AMS_PlayList model);

        /// <summary>
        /// 获取单个的播放列表
        /// *异常处理：直接向上层抛出
        /// </summary>
        /// <returns>获取成功返回List</returns>
        [OperationContract]
        AMS.Model.AMS_PlayList GetPlayListByID(int id);
    }
}
