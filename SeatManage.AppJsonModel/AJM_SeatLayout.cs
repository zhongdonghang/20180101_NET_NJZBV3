using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.AppJsonModel
{
    public class AJM_SeatLayout
    {
        private bool _isUpdate = false;
        private int _rowsCount = 0;
        private int _columsCount = 0;
        private List<AJM_Element> _elementList = new List<AJM_Element>();
        /// <summary>
        /// 座位图是否更新 true：是 false：否
        /// </summary>
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set { _isUpdate = value; }
        }
        /// <summary>
        /// 布局总行数
        /// </summary>
        public int RowsCount
        {
            get { return _rowsCount; }
            set { _rowsCount = value; }
        }
        /// <summary>
        /// 布局总列数
        /// </summary>
        public int ColumsCount
        {
            get { return _columsCount; }
            set { _columsCount = value; }
        }
        /// <summary>
        /// 布局元素list
        /// </summary>
        public List<AJM_Element> ElementList
        {
            get { return _elementList; }
            set { _elementList = value; }
        }
    }
}
