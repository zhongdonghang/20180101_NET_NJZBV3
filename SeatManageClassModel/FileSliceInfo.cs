using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    /// <summary>
    /// 文件片段信息
    /// </summary>
    public class FileSliceInfo
    {
        //文件名 
        public string Name { get; set; }
        /// <summary>
        /// 文件长度
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// 文件片段起始位置
        /// </summary>
        public long Offset { get; set; }
        /// <summary>
        /// 文件片段二进制流
        /// </summary>
        public byte[] Data { get; set; }

    }
    /// <summary>
    /// 文件Md5码
    /// </summary>
    public class FileSliceInfo_Md5 : FileSliceInfo
    {
        public string Md5 { get; set; }
    }

}
