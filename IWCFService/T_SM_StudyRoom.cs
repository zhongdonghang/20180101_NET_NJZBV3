using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;
using SeatManage.EnumType;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 添加研习间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddNewStudyRoom(StudyRoomInfo model);
        /// <summary>
        /// 更新研习间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateStudyRoom(StudyRoomInfo model);
        /// <summary>
        /// 删除研习间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteStudyRoom(StudyRoomInfo model);
        /// <summary>
        /// 获取单个研习间信息
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [OperationContract]
        StudyRoomInfo GetSingleStudyRoonInfo(string roomNo);
        /// <summary>
        /// 获取研习间列表
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [OperationContract]
        List<StudyRoomInfo> GetStudyRoonInfoList(List<string> roomNo);

    }
}
