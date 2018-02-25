using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.AppJsonModel
{
    public class AJM_Element
    {
        private string _seatNo = "";
        private bool _hasPower;
        private double _baseWidth = 1;
        private double _baseHeight = 1;
        private double _x = 0;
        private double _y = 0;
        private int _angle = 0;
        private string _elementType = "seat";
        private string _remark = "";
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get { return _seatNo; }
            set { _seatNo = value; }
        }

        /// <summary>
        /// 是否有电源 true：有 false：无
        /// </summary>
        public bool HasPower
        {
            get { return _hasPower; }
            set { _hasPower = value; }
        }
        /// <summary>
        /// 座位图标宽度基数
        /// </summary>
        public double BaseWidth
        {
            get { return _baseWidth; }
            set { _baseWidth = value; }
        }
        /// <summary>
        /// 座位图标高度
        /// </summary>
        public double BaseHeight
        {
            get { return _baseHeight; }
            set { _baseHeight = value; }
        }
        /// <summary>
        /// x坐标
        /// </summary>
        public double X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// y坐标
        /// </summary>
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
        /// <summary>
        /// 旋转角度
        /// </summary>
        public int Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }
        /// <summary>
        /// 标志当前元素类型
        /// </summary>
        public string ElementType
        {
            get { return _elementType; }
            set { _elementType = value; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
    }
}
