using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
namespace SeatManage.IPocketBespeak
{
    public interface IMainFunctionPageBll
    {
        /// <summary>
        /// 设置暂离，返回操作结果。
        /// </summary>
        /// <param name="school">学校</param>
        /// <param name="reader">读者</param>
        /// <returns></returns>
        string SetShortLeave(AMS.Model.AMS_School school, ReaderInfo reader);

        /// <summary>
        /// 释放座位，返回操作结果
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        string FreeSeat(AMS.Model.AMS_School school, ReaderInfo reader);


        /// <summary>
        /// 获取所有阅览室的座位使用状态
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        Dictionary<string, ReadingRoomSeatUsedState_Ex> GetAllRoomSeatUsedState(AMS.Model.AMS_School school);

        /// <summary>
        /// 获取读者当前状态
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns> 
        ReaderInfo GetReaderInfo(AMS.Model.AMS_School school, string cardNo);

        /// <summary>
        /// 续时
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        string DelaySeatUsedTime(AMS.Model.AMS_School school, ReaderInfo reader);


    }
}
