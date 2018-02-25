using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///离开窗体单例
    /// </summary>
    public class LeaveWindowObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        private LeaveWindow _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static LeaveWindowObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public LeaveWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static LeaveWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new LeaveWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private LeaveWindowObject()
        {
            try
            {
                _window = new LeaveWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("操作选择窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}