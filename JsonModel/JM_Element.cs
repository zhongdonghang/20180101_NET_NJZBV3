using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_Element
    {
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
