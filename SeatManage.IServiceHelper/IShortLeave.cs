using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ServiceHelper
{
    public interface IShortLeave
    {
        /// <summary>
        /// 把自己的座位设置为暂时离开
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        string ShortLeave(string cardNo);
        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        string ComeBack(string cardNo);
    }
}
