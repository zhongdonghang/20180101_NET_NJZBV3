using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 注释
    /// </summary>
    [Serializable]
    public class Note : RoomLayoutElement
    {
        string _Remark = "";
        /// <summary>
        /// 注释
        /// </summary>
        public string Remark
        {
            get
            {
                return _Remark;
            }
            set
            {
                _Remark = value;
            }
        }
        private SeatManage.EnumType.OrnamentType _Type = EnumType.OrnamentType.None;
        /// <summary>
        /// 备注的装饰物类型
        /// </summary>
        public SeatManage.EnumType.OrnamentType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

    }
}
