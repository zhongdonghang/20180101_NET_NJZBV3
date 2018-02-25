using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///�����嵥��
    /// </summary>
    public class MainWindowObject
    {

        /// <summary>
        /// ����
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        private MainWindow _window;
        /// <summary>
        /// ʵ��
        /// </summary>
        private static MainWindowObject _windowObkect;

        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        public MainWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static MainWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new MainWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        private MainWindowObject()
        {
            try
            {
                _window = new MainWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("�������ʼ��ʧ�ܣ�" + ex.Message);
                throw ex;
            }
        }
    }
}