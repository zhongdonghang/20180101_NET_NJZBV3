using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum OrnamentType
    {
        /// <summary>
        /// 空
        /// </summary>
        None=-1,
        /// <summary>
        /// 桌子，方桌
        /// </summary>
        Table=0,
        /// <summary>
        /// 电脑桌
        /// </summary>
        PCTable=1,
        /// <summary>
        /// 圆桌
        /// </summary>
        Roundtable=2,
        /// <summary>
        /// 盆景
        /// </summary>
        Plant=3,
        /// <summary>
        /// 门
        /// </summary>
        Door=4,
        /// <summary>
        /// 窗户
        /// </summary>
        Window=5,
        /// <summary>
        /// 墙，分割线
        /// </summary>
        Wall=6,
        /// <summary>
        /// 书架
        /// </summary>
        Bookshelf=7,
        /// <summary>
        /// 柱子
        /// </summary>
        Pillar=8,
        /// <summary>
        /// 楼梯
        /// </summary>
        Stairway=9,
        /// <summary>
        /// 电动扶梯
        /// </summary>
        Escalator=10,
        /// <summary>
        /// 电梯
        /// </summary>
        Elevator=11,
        /// <summary>
        /// 空调
        /// </summary>
        AirConditioning=12,
        /// <summary>
        /// 洗手间
        /// </summary>
        Toilet=13,
        /// <summary>
        /// 开水房
        /// </summary>
        WaterHouse=14,
        /// <summary>
        /// 台阶
        /// </summary>
        Steps=15

    }
}
