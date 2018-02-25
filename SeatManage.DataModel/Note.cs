using System;

namespace SeatManage.DataModel
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
        private SystemCode.EnumType.OrnamentType _Type = EnumType.OrnamentType.None;
        /// <summary>
        /// 备注的装饰物类型
        /// </summary>
        public SystemCode.EnumType.OrnamentType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

    }
}
