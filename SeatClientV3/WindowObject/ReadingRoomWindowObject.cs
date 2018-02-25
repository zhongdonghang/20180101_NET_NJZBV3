using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///����Ϣ���嵥��
    /// </summary>
    public class ReadingRoomWindowObject
    {

        /// <summary>
        /// ����
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        private ReadingRoomWindow _window;
        /// <summary>
        /// ʵ��
        /// </summary>
        private static ReadingRoomWindowObject _windowObkect;

        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        public ReadingRoomWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static ReadingRoomWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new ReadingRoomWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        private ReadingRoomWindowObject()
        {
            try
            {
                _window = new ReadingRoomWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("�����Ҵ����ʼ��ʧ�ܣ�" + ex.Message);
                throw ex;
            }
        }
    }
}