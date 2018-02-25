using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatManageWebV2.Code
{
    /// <summary>
    /// 座位操作
    /// </summary>
    public class SeatHandleClass
    {
        /// <summary>
        /// 获取座位信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public SeatManage.ClassModel.Seat GetSeatUsedInfoBySeatNo(string cardNo)
        { 
           SeatManage.ClassModel.Seat seat =SeatManage.Bll.T_SM_Seat.GetSeatInfoBySeatNo(cardNo);
           return seat;
        }
    }
}