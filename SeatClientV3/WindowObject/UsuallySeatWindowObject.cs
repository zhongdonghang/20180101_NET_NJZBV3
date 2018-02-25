using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///������λ���嵥��
    /// </summary>
    public class UsuallySeatWindowObject
    {

        /// <summary>
        /// ����
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        private UsuallySeatWindow _window;
        /// <summary>
        /// ʵ��
        /// </summary>
        private static UsuallySeatWindowObject _windowObkect;

        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        public UsuallySeatWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static UsuallySeatWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new UsuallySeatWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        private UsuallySeatWindowObject()
        {
            try
            {
                _window = new UsuallySeatWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("������λ�����ʼ��ʧ�ܣ�" + ex.Message);
                throw ex;
            }
        }
    }
}