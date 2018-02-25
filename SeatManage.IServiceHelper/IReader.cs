using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ServiceHelper
{
    public interface IReader 
    {
        /// <summary>
        /// 根据卡列号获取读者信息
        /// </summary>
        /// <param name="cardSN"></param>
        /// <returns></returns>
        string GetBaseReaderInfoByCardId(string cardId);
        /// <summary>
        /// 获取读者的基本信息
        /// </summary>
        /// <param name="cardNum">学号</param>
        /// <returns></returns>
        string GetBaseReaderInfo(string cardNum);
        /// <summary>
        /// 获取读者实时记录，根据指定参数可返回当前有效的预约记录、选座记录、等待记录、黑名单记录
        /// </summary>
        /// <param name="cardNum">读者学号</param>
        /// <param name="getItemsParameter">该参数指定要获取哪些项，json格式</param>
        /// <returns></returns>
        string GetReaderActualTimeRecord(string cardNum, string getItemsParameter);
        /// <summary>
        /// 获取指定天数内的所有预约记录
        /// </summary>
        /// <param name="cardNum">学号</param>
        /// <param name="beforeDays">指定天数</param>
        /// <returns></returns>
        string GetReaderBespeakRecord(string cardNum, int pageIndex, int pageSize);
        /// <summary>
        /// 获取读者指定天数内的选择座位的记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="beforeDays">指定天数</param>
        /// <returns></returns>
        string GetReaderChooseSeatRecord(string cardNum, int pageIndex, int pageSize);
        /// <summary>
        /// 获取读者黑名单记录
        /// </summary>
        /// <param name="cardNum">学号</param>
        /// <param name="beforeDays">指定天数</param>
        /// <returns></returns>
        string GetReaderBlacklistRecord(string cardNum, int pageIndex, int pageSize);
        /// <summary>
        /// 获取读者帐号信息，通过该方式可以判断读者是否可以执行预约操作。
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="beforeDays"></param>
        /// <returns></returns>
        string GetReaderAccount(string cardNum,string password);
        /// <summary>
        /// 获取读者违规记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="beforeDays"></param>
        /// <returns></returns>
        string GetViolateDiscipline(string cardNum, int pageIndex, int pageSize);
    }
}
