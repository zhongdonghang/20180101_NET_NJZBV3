using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///�������嵥��
    /// </summary>
    public class AppLoadingWindowObject
    {

        /// <summary>
        /// ����
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        private AppLoadingWindow _window;
        /// <summary>
        /// ʵ��
        /// </summary>
        private static AppLoadingWindowObject _windowObkect;

        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        public AppLoadingWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static AppLoadingWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new AppLoadingWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        private AppLoadingWindowObject()
        {
            try
            {
                _window = new AppLoadingWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("�����ʼ��ʧ�ܣ�" + ex.Message);
                throw ex;
            }
        }
    }
}