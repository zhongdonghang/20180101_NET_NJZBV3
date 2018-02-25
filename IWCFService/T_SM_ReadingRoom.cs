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
        #region 阅览室信息的相关操作
        /// <summary>
        /// 根据阅览室编号查询对应的阅览室信息
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        [OperationContract]
        List<ReadingRoomInfo> GetReadingRoomInfo(List<string> roomNum);

        /// <summary>
        /// 根据条件获取阅览室信息
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="libraryNum">图书馆编号</param>
        /// <param name="schoolNum">校区编号</param>
        /// <returns></returns>
        [OperationContract]
        List<ReadingRoomInfo> GetReadingRooms(List<string> roomNum, List<string> libraryNum, List<string> schoolNum);
        /// <summary>
        /// 添加阅览室信息
        /// </summary>
        /// <param name="room">要添加的阅览室信息</param>
        [OperationContract]
        bool AddReadingRoom(ReadingRoomInfo room);
        /// <summary>
        /// 更新阅览室信息
        /// </summary>
        /// <param name="room"></param>
        [OperationContract]
        bool UpdateReadingRoom(ReadingRoomInfo room);
        /// <summary>
        /// 删除阅览室信息
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [OperationContract]
        bool deleteReadingRoom(ReadingRoomInfo room);
        /// <summary>
        /// 获取阅览室座位使用状态
        /// </summary>
        /// <param name="roomNum">阅览室号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, ReadingRoomSeatUsedState> GetRoomSeatUsedState(List<string> roomNums);
        /// <summary>
        /// 获取阅览室座位布局
        /// </summary>
        /// <param name="roomNum">房间编号</param>
        /// <returns></returns>
        [OperationContract]
        SeatLayout GetRoomSeatLayOut(string roomNum);

        /// <summary>
        /// 更新阅览室座位布局
        /// </summary>
        /// <param name="seatLayout"></param>
        /// <returns></returns>
        [OperationContract]
       SeatManage.EnumType.HandleResult UpdateSeatLayout(SeatLayout seatLayout);
        /// <summary>
        /// 查看阅览室编号是否重复
        /// </summary>
        /// <param name="ReadingRoomNo"></param>
        /// <returns></returns>
        [OperationContract]
        bool ReadingRoomIsExists(string ReadingRoomNo);
        /// <summary>
        /// 获取阅览室座位使用状态
        /// </summary>
        /// <param name="roomNum">阅览室号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, ReadingRoomSeatUsedState> GetRoomSeatUsedStateV2(List<string> roomNums);
        /// <summary>
        /// 获取阅览室座位使用状态
        /// </summary>
        /// <param name="roomNum">阅览室号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, ReadingRoomSeatUsedState> GetRoomSeatUsedStateV3(List<string> roomNums);

        /// <summary>
        /// 获取阅览室座位使用状态
        /// </summary>
        /// <param name="roomNum">阅览室号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, ReadingRoomSeatUsedState> GetRoomSeatUsedStateV4(List<string> roomNums);
        #endregion
    }
}
