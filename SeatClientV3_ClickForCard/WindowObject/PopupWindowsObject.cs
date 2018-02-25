using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    /// 弹窗单例
    /// </summary>
    public class PopupWindowsObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        private PopupWindow _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static PopupWindowsObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public PopupWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static PopupWindowsObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new PopupWindowsObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private PopupWindowsObject()
        {
            try
            {
                _window = new PopupWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("消息提醒窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}