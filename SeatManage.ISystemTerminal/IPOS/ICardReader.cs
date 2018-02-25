using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ISystemTerminal.IPOS
{
    /// <summary>
    /// 读卡方法接口
    /// </summary>
    public interface ICardReader
    {
        /// <summary>
        /// 读卡器初始化
        /// </summary>
        /// <returns></returns>
        bool Init();
        /// <summary>
        /// 连接第三方
        /// </summary>
        /// <returns></returns>
        bool ConnectServer(); 
        /// <summary>
        /// 读取卡片信息
        /// </summary>
        /// <returns></returns>
        string GetCardNo();
        /// <summary>
        /// 读取卡片Id
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        string GetCardId();

        void Beep();



    }
}
