using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///短消息窗体单例
    /// </summary>
    public class ReaderNoteWindowObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        private ReaderNoteWindow _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static ReaderNoteWindowObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public ReaderNoteWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static ReaderNoteWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new ReaderNoteWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private ReaderNoteWindowObject()
        {
            try
            {
                _window = new ReaderNoteWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("短消息窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}