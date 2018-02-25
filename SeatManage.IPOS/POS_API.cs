using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace SeatManage.IPOS
{
    public class POS_API:IPOS.IPOSMethod
    {
        [StructLayout(LayoutKind.Explicit,Size=280, CharSet = CharSet.Ansi)]
        public struct AccountMsg
        {
            [FieldOffset(0)]
            public string Name;
            [FieldOffset(24)]
            public string SexNo; 		
            [FieldOffset(28)]
            public string DeptCode;			/*���Ŵ���*/
            [FieldOffset(48)]
            public ushort CardNo; 				/*����*/
            [FieldOffset(52)]
            public ushort AccountNo; 				/*�ʺ�*/
            [FieldOffset(56)]
            public string StudentCode; 		/*ѧ��*/
            [FieldOffset(80)]
            public string IDCard; 			/*���֤��*/
            [FieldOffset(104)]
            public string PID;					/*��ݴ���*/
            [FieldOffset(108)]
            public string IDNo; 				/*������*/
            [FieldOffset(124)]
            public short Balance; 				/*�����*/
            [FieldOffset(128)]
            public string Password;			/*��������*/
            [FieldOffset(136)]
            public char ExpireDate;			/*�˻���ֹ����*/
            [FieldOffset(144)]
            public ushort SubSeq;					/*������*/
            [FieldOffset(148)]
            public char IsOpenInSys;			/*�Ƿ��ڱ�ϵͳ�ڿ�ͨ*/
            [FieldOffset(152)]
            public short TerminalNo;				/*�ն˺���,��ȡ����ʱ��Ҫ��д*/
            [FieldOffset(156)]
            public short RetCode;				/*��̨������ֵ*/
            [FieldOffset(160)]
            public char Flag;				/*״̬(2004-08-26����)*/
            [FieldOffset(176)]
            public char Pad;				/*Ԥ���ֶ�*/
        }



        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="CardReaderType">����������0ΪUSB��1Ϊ����</param>
        /// <param name="port">�˿ں�</param>
        /// <param name="Baud_Rate">������</param>
        /// <returns></returns>
        [DllImport("AIO_API.dll")]
        private static extern int TA_CRInit(int CardReaderType, Int16 port, Int32 Baud_Rate);

        /// <summary>
        /// ���ٶ���
        /// </summary>
        /// <param name="i">������������ӿ�Ƭ�ж����Ŀ���</param>
        /// <returns></returns>
        [DllImport("AIO_API.dll")]
        private static extern int TA_FastGetCardNo(uint i);

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="IP">������������IP</param>
        /// <param name="port">�˿�</param>
        /// <param name="SysCode">ϵͳ����</param>
        /// <param name="TerminalNo"></param>
        /// <param name="ProxyOffline">����������Ƿ��ѻ�</param>
        /// <param name="MaxJnl">�����ˮ��</param>
        /// <returns></returns>
        [DllImport("AIO_API.dll")]
        private static extern bool TA_Init(string IP , short port ,  ushort SysCode,  ushort TerminalNo, bool ProxyOffline,  uint MaxJnl);


        /// <summary>
        /// ��ȡ��Ƭ��Ϣ
        /// </summary>
        /// <param name="am">������������ӿ�Ƭ�ж����Ŀ�Ƭ��Ϣ</param>
        /// <returns></returns>
        [DllImport("AIO_API.dll")]
        private static extern int TA_ReadCardSimple(ref AccountMsg pAccMsg);


        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="BeepMSecond">�����ĺ�����</param>
        /// <returns></returns>
        [DllImport("AIO_API.dll")]
        private static extern int TA_CRBeep(UInt16 BeepMSecond);

        #region IPOSMethod ��Ա
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <returns>0Ϊ�ɹ�������Ϊ���ɹ�</returns>
        public string strInitializeCard()
        {
            int flag = TA_CRInit(0,1,19200);
            return flag.ToString();
        }
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <returns>trueΪ�ɹ�</returns>
        public bool boolConnectServer()
        {
            uint uJnl = 1;
            ushort sSysCode = 20;
            ushort sTerminalNo = 10;
            string ipip = "10.9.0.11";
            bool bOffline=true;
            return  TA_Init(ipip, 8500, sSysCode, sTerminalNo, bOffline, uJnl);
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns>���߱��</returns>
        public string strGetCardNo()
        {
            AccountMsg am = new AccountMsg();
            int s = TA_ReadCardSimple(ref am);
            _cardNo = s.ToString() + "," + am.StudentCode;
            return _cardNo; //am.StudentCode.ToString() ;
        }

        /// <summary>
        /// ��ȡ����Ψһ��ʶ
        /// </summary>
        /// <returns>��Ƭ������</returns>
        public string strGetCardID()
        {
            uint cardNo = 0 ;
            int i = TA_FastGetCardNo(cardNo);
            return i.ToString();
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void vCardBeep()
        {
           int i = TA_CRBeep(500);
        }

        #endregion

        #region IPOSMethod ��Ա

        private string _cardNo = "";
        public string CardNo
        {
            get
            {
                return _cardNo;
            }
            set
            {
                _cardNo = value;
            }
        }

        #endregion
    }
}
