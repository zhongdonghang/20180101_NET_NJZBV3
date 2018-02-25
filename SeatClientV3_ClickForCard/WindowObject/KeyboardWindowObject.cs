using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///���̴��嵥��
    /// </summary>
    public class KeyboardWindowObject
    {

        /// <summary>
        /// ����
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        private KeyboardWindow _window;
        /// <summary>
        /// ʵ��
        /// </summary>
        private static KeyboardWindowObject _windowObkect;

        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        public KeyboardWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static KeyboardWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new KeyboardWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        private KeyboardWindowObject()
        {
            try
            {
                _window = new KeyboardWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("�������봰���ʼ��ʧ�ܣ�" + ex.Message);
                throw ex;
            }
        }
    }
}