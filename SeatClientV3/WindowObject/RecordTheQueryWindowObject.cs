using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///��¼��ѯ���嵥��
    /// </summary>
    public class RecordTheQueryWindowObject
    {

        /// <summary>
        /// ����
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        private RecordTheQueryWindow _window;
        /// <summary>
        /// ʵ��
        /// </summary>
        private static RecordTheQueryWindowObject _windowObkect;

        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        public RecordTheQueryWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static RecordTheQueryWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new RecordTheQueryWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        private RecordTheQueryWindowObject()
        {
            try
            {
                _window = new RecordTheQueryWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("��¼��ѯ�����ʼ��ʧ�ܣ�" + ex.Message);
                throw ex;
            }
        }
    }
}