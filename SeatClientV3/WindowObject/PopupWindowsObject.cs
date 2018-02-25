using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    /// ��������
    /// </summary>
    public class PopupWindowsObject
    {

        /// <summary>
        /// ����
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        private PopupWindow _window;
        /// <summary>
        /// ʵ��
        /// </summary>
        private static PopupWindowsObject _windowObkect;

        /// <summary>
        /// ��Ϣ��ʾ��
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
        /// ���캯��
        /// </summary>
        private PopupWindowsObject()
        {
            try
            {
                _window = new PopupWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("��Ϣ���Ѵ����ʼ��ʧ�ܣ�" + ex.Message);
                throw ex;
            }
        }
    }
}