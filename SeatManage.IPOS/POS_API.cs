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
            public string DeptCode;			/*部门代码*/
            [FieldOffset(48)]
            public ushort CardNo; 				/*卡号*/
            [FieldOffset(52)]
            public ushort AccountNo; 				/*帐号*/
            [FieldOffset(56)]
            public string StudentCode; 		/*学号*/
            [FieldOffset(80)]
            public string IDCard; 			/*身份证号*/
            [FieldOffset(104)]
            public string PID;					/*身份代码*/
            [FieldOffset(108)]
            public string IDNo; 				/*身份序号*/
            [FieldOffset(124)]
            public short Balance; 				/*现余额*/
            [FieldOffset(128)]
            public string Password;			/*消费密码*/
            [FieldOffset(136)]
            public char ExpireDate;			/*账户截止日期*/
            [FieldOffset(144)]
            public ushort SubSeq;					/*补助戳*/
            [FieldOffset(148)]
            public char IsOpenInSys;			/*是否在本系统内开通*/
            [FieldOffset(152)]
            public short TerminalNo;				/*终端号码,提取补助时需要填写*/
            [FieldOffset(156)]
            public short RetCode;				/*后台处理返回值*/
            [FieldOffset(160)]
            public char Flag;				/*状态(2004-08-26增加)*/
            [FieldOffset(176)]
            public char Pad;				/*预留字段*/
        }



        /// <summary>
        /// 初始化读卡器
        /// </summary>
        /// <param name="CardReaderType">读卡器类型0为USB，1为串口</param>
        /// <param name="port">端口号</param>
        /// <param name="Baud_Rate">波特率</param>
        /// <returns></returns>
        [DllImport("AIO_API.dll")]
        private static extern int TA_CRInit(int CardReaderType, Int16 port, Int32 Baud_Rate);

        /// <summary>
        /// 快速读卡
        /// </summary>
        /// <param name="i">－输出参数，从卡片中读出的卡号</param>
        /// <returns></returns>
        [DllImport("AIO_API.dll")]
        private static extern int TA_FastGetCardNo(uint i);

        /// <summary>
        /// 初始化第三方
        /// </summary>
        /// <param name="IP">第三方服务器IP</param>
        /// <param name="port">端口</param>
        /// <param name="SysCode">系统代码</param>
        /// <param name="TerminalNo"></param>
        /// <param name="ProxyOffline">代理服务器是否脱机</param>
        /// <param name="MaxJnl">最大流水号</param>
        /// <returns></returns>
        [DllImport("AIO_API.dll")]
        private static extern bool TA_Init(string IP , short port ,  ushort SysCode,  ushort TerminalNo, bool ProxyOffline,  uint MaxJnl);


        /// <summary>
        /// 获取卡片信息
        /// </summary>
        /// <param name="am">－输出参数，从卡片中读出的卡片信息</param>
        /// <returns></returns>
        [DllImport("AIO_API.dll")]
        private static extern int TA_ReadCardSimple(ref AccountMsg pAccMsg);


        /// <summary>
        /// 读卡器峰鸣
        /// </summary>
        /// <param name="BeepMSecond">峰鸣的毫秒数</param>
        /// <returns></returns>
        [DllImport("AIO_API.dll")]
        private static extern int TA_CRBeep(UInt16 BeepMSecond);

        #region IPOSMethod 成员
        /// <summary>
        /// 初始化读卡器
        /// </summary>
        /// <returns>0为成功，其他为不成功</returns>
        public string strInitializeCard()
        {
            int flag = TA_CRInit(0,1,19200);
            return flag.ToString();
        }
        /// <summary>
        /// 初始化第三方
        /// </summary>
        /// <returns>true为成功</returns>
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
        /// 获取卡号
        /// </summary>
        /// <returns>读者编号</returns>
        public string strGetCardNo()
        {
            AccountMsg am = new AccountMsg();
            int s = TA_ReadCardSimple(ref am);
            _cardNo = s.ToString() + "," + am.StudentCode;
            return _cardNo; //am.StudentCode.ToString() ;
        }

        /// <summary>
        /// 获取卡的唯一标识
        /// </summary>
        /// <returns>卡片物理编号</returns>
        public string strGetCardID()
        {
            uint cardNo = 0 ;
            int i = TA_FastGetCardNo(cardNo);
            return i.ToString();
        }

        /// <summary>
        /// 读卡器响
        /// </summary>
        public void vCardBeep()
        {
           int i = TA_CRBeep(500);
        }

        #endregion

        #region IPOSMethod 成员

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
