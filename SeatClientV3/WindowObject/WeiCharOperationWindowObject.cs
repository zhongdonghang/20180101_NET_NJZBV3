using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatClientV3.WindowObject
{
    public class WeiCharOperationWindowObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        private WeiCharOperationQRWindow _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static WeiCharOperationWindowObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public WeiCharOperationQRWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static WeiCharOperationWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new WeiCharOperationWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private WeiCharOperationWindowObject()
        {
            try
            {
                _window = new WeiCharOperationQRWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("微信窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}
