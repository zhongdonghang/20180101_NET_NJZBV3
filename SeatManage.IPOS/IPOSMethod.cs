using System;
using System.Collections.Generic;
using System.Text;

namespace SeatManage.IPOS
{
    public interface IPOSMethod
    {
        /// <summary>
        /// ��ʼ����������0Ϊ��ʼ���ɹ���1Ϊ���ɹ�
        /// </summary>
        /// <returns>0Ϊ��ʼ���ɹ���1Ϊ���ɹ�</returns>
        string strInitializeCard();
        /// <summary>
        /// ���߿���
        /// </summary>
        string CardNo
        {
            get;
            set;
        }
        /// <summary>
        /// ��ʼ����������tureΪ�ɹ�
        /// </summary>
        /// <returns>tureΪ�ɹ�</returns>
        bool boolConnectServer();

        /// <summary>
        /// ��ȡ���� 
        /// </summary>
        /// <returns>���߱��</returns>
        string strGetCardNo();

        /// <summary>
        /// ��ȡ��Ƭ������
        /// </summary>
        /// <returns>��Ƭ������</returns>
        string strGetCardID();

        /// <summary>
        /// ��������
        /// </summary>
        void vCardBeep();
    }
}
