using System;
using System.Collections.Generic;
using System.Text;

namespace SeatManage.IPOS
{
    public interface IPOSMethod
    {
        /// <summary>
        /// 初始化读卡器。0为初始化成功，1为不成功
        /// </summary>
        /// <returns>0为初始化成功，1为不成功</returns>
        string strInitializeCard();
        /// <summary>
        /// 读者卡号
        /// </summary>
        string CardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 初始化第三方，ture为成功
        /// </summary>
        /// <returns>ture为成功</returns>
        bool boolConnectServer();

        /// <summary>
        /// 获取卡号 
        /// </summary>
        /// <returns>读者编号</returns>
        string strGetCardNo();

        /// <summary>
        /// 获取卡片物理编号
        /// </summary>
        /// <returns>卡片物理编号</returns>
        string strGetCardID();

        /// <summary>
        /// 读卡器响
        /// </summary>
        void vCardBeep();
    }
}
