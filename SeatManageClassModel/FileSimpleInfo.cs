using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 文件简单信息
    /// </summary>
    [Serializable]
   public class FileSimpleInfo
    {
        //文件名 
        public string Name { get; set; }
        /// <summary>
        /// 文件长度
        /// </summary>
        public long Length { get; set; } 
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyDateTime { get; set; } 
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } 
    }
}
