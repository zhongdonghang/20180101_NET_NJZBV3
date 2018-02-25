using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SeatManage.SeatManageComm
{
    public class SeatComm
    {
        /// <summary>
        /// 读者状态转换成中文描述
        /// </summary>
        /// <param name="readerState"></param>
        /// <returns></returns>
        public static string ConvertReaderState(SeatManage.EnumType.EnterOutLogType readerState)
        {
            switch (readerState)
            {
                case EnumType.EnterOutLogType.ContinuedTime:
                case EnumType.EnterOutLogType.ComeBack:
                case EnumType.EnterOutLogType.BookingConfirmation:
                case EnumType.EnterOutLogType.ReselectSeat:
                case EnumType.EnterOutLogType.SelectSeat:
                case EnumType.EnterOutLogType.WaitingSuccess:
                    return "在座";
                case SeatManage.EnumType.EnterOutLogType.Leave:
                    return "离开";
                case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                    return "暂离";

            }
            return "";
        }
        /// <summary>
        /// 操作者标识转换成字符串
        /// </summary>
        /// <param name="OperateFlag"></param>
        /// <returns></returns>
        public static string ConvertOperateToString(int OperateFlag)
        {
            switch (OperateFlag)
            {
                case 0:
                    return "管理员设置";
                case 1:
                    return "读者刷卡";
                case 2:
                    return "被其他读者";
                case 3:
                    return "监控服务设置";
            }
            return "";
        }

        /// <summary>
        /// 获取随即数
        /// </summary>
        /// <returns>随即数</returns>
        public static string RndNum()
        {
            return Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 计算文件的MD5校验
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// 转换预约状态
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static string ConvertBookingStatus(SeatManage.EnumType.BookingStatus bookStatus)
        {
            switch (bookStatus)
            {
                case SeatManage.EnumType.BookingStatus.Cencaled:
                    return "已取消";
                case SeatManage.EnumType.BookingStatus.Waiting:
                    return "等待确认";
                case SeatManage.EnumType.BookingStatus.Confinmed:
                    return "已确认";
                default:
                    return "全部";
            }
        }

        /// <summary>
        /// 转换进出记录
        /// </summary>
        /// <param name="enterOutStatus"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.EnterOutLogType ConvertEnterOutType(int enterOutStatus)
        {
            switch (enterOutStatus)
            {
                case 2: return SeatManage.EnumType.EnterOutLogType.ShortLeave;
                case 3: return SeatManage.EnumType.EnterOutLogType.Leave;
                default: return SeatManage.EnumType.EnterOutLogType.Leave;
            }
        }
        /// <summary>
        /// 长座位号转换为短座位号
        /// </summary>
        /// <param name="seatNoAmcount">要显示的座位号长度</param>
        /// <param name="seatNo">原座位号</param>
        /// <returns></returns>
        public static string SeatNoToShortSeatNo(int seatNoAmcount, string seatNo)
        {
            if (!string.IsNullOrEmpty(seatNo) && seatNo.Length >= 9)
            {
                string shortNo = seatNo.Substring(seatNo.Length - seatNoAmcount, seatNoAmcount);
                return shortNo;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 长座位号转换为座位号头
        /// </summary>
        /// <param name="seatNoAmcount">要显示的座位号长度</param>
        /// <param name="seatNo">原座位号</param>
        /// <returns></returns>
        public static string SeatNoToSeatNoHeader(int seatNoAmcount, string seatNo)
        {
            if (!string.IsNullOrEmpty(seatNo) && seatNo.Length >= 9)
            {
                string seatNoHeader = seatNo.Substring(0, seatNo.Length - seatNoAmcount);
                return seatNoHeader;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 通过url获取页面名称
        /// </summary>
        /// <param name="url">完整的页面地址</param>
        /// <returns></returns>
        public static string GetPageName(string url)
        {
            string pageName = "";
            string strSubServer = url.Substring(url.LastIndexOf('/') + 1);
            int index = Convert.ToInt32(strSubServer.IndexOf('?'));
            if (index != -1)
            {
                pageName = strSubServer.Substring(0, index);
            }
            else
            {
                pageName = strSubServer;
            }
            return pageName;
        }
        /// <summary>
        /// 把数值转换为bool类型的值，非0返回true
        /// 0、null、空字符串返回false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ConvertToBoolen(string value)
        {
            switch (value)
            { 
                case "0":
                case null:
                case "":
                case "False":
                case "false":
                    return false;
                default: return true;
            }
        }
        public static bool ConvertToBoolen(int value)
        {
           return ConvertToBoolen(value.ToString());
        }
        /// <summary>
        /// bool值转换为0或者1
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ConvertToInt(bool value)
        { 
            switch (value)
            { 
                case true:
                    return 1;
                default:
                    return 0;
            }
        }
      
    }
}

