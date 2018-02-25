using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///�뿪���嵥��
    /// </summary>
    public class LeaveWindowObject
    {

        /// <summary>
        /// ����
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        private LeaveWindow _window;
        /// <summary>
        /// ʵ��
        /// </summary>
        private static LeaveWindowObject _windowObkect;

        /// <summary>
        /// ��Ϣ��ʾ��
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
        /// ���캯��
        /// </summary>
        private LeaveWindowObject()
        {
            try
            {
                _window = new LeaveWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("����ѡ�����ʼ��ʧ�ܣ�" + ex.Message);
                throw ex;
            }
        }
    }
}