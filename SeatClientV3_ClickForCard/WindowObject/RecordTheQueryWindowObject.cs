using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///记录查询窗体单例
    /// </summary>
    public class RecordTheQueryWindowObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        private RecordTheQueryWindow _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static RecordTheQueryWindowObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public RecordTheQueryWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static RecordTheQueryWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new RecordTheQueryWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private RecordTheQueryWindowObject()
        {
            try
            {
                _window = new RecordTheQueryWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("记录查询窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}