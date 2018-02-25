using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    /// <summary>
    /// 房间布局基本元素
    /// </summary>
    public class RoomLayoutElement
    {
        private int _Height = 57;
        /// <summary>
        /// 元素高度
        /// </summary>
        public int Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
            }
        }

        private int _Width = 57;
        /// <summary>
        /// 元素宽度
        /// </summary>
        public int Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
            }
        }
        private double _PosintionX = 0;
        /// <summary>
        /// 元素位置的X坐标
        /// </summary>
        public double PositionX
        {
            get
            {
                return _PosintionX;
            }
            set
            {
                _PosintionX = value;
            }
        }
        private double _PositionY = 0;
        /// <summary>
        /// 元素位置的Y坐标
        /// </summary>
        public double PositionY
        {
            get
            {
                return _PositionY;
            }
            set
            {
                _PositionY = value;
            }
        }
        private int _RotationCenterX = 0;
        /// <summary>
        /// 旋转X坐标
        /// </summary>
        public int RotationCenterX
        {
            get { return _RotationCenterX; }
            set { _RotationCenterX = value; }
        }

        private int _RotationCenterY = 0;
        /// <summary>
        /// 旋转Y坐标
        /// </summary>
        public int RotationCenterY
        {
            get { return _RotationCenterY; }
            set { _RotationCenterY = value; }
        }
        private int _RotationAngle = 0;
        /// <summary>
        /// 旋转角度
        /// </summary>
        public int RotationAngle
        {
            get { return _RotationAngle; }
            set { _RotationAngle = value; }
        }

        private double _BaseHeight = 1;
        /// <summary>
        /// 元素高度
        /// </summary>
        public double BaseHeight
        {
            get
            {
                return _BaseHeight;
            }
            set
            {
                _BaseHeight = value;
            }
        }

        private double _BaseWidth = 1;
        /// <summary>
        /// 元素宽度
        /// </summary>
        public double BaseWidth
        {
            get
            {
                return _BaseWidth;
            }
            set
            {
                _BaseWidth = value;
            }
        }
    }
}
