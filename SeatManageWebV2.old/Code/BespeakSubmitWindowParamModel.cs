using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace SeatManageWebV2.Code
{
    public class BespeakSubmitWindowParamModel
    {
        /// <summary>
        ///  将获取到的url参数解密，并得到参数值赋给model
        /// </summary>
        /// <param name="ciphertext">密文</param>
        public BespeakSubmitWindowParamModel(string ciphertext)
        {
            string plainText = SeatManage.SeatManageComm.AESAlgorithm.DESDecode(ciphertext);
            string[] strArr = plainText.Split('&');
            foreach (string item in strArr)
            {
                string[] itemArr = item.Split('=');
                if (itemArr.Length != 2)
                {
                    return;
                }
                switch (itemArr[0])
                {
                    case "seatNo":
                        SeatNo = itemArr[1];
                        break;
                    case "seatShortNo":
                        ShortSeatNo = itemArr[1];
                        break;
                    case "date":
                        BespeakDate = itemArr[1];
                        break;
                    case "roomNo":
                        RoomNo = itemArr[1];
                        break;
                    case "timeSpan":
                        string tstr = itemArr[1];
                        if (!string.IsNullOrEmpty(tstr))
                        {
                            string[] sps = tstr.Split(';');
                            foreach (string s in sps)
                            {
                                if (!string.IsNullOrEmpty(s))
                                {
                                    TimeSpan.Add(DateTime.Parse(DateTime.FromBinary(long.Parse(BespeakDate)).ToLongDateString() + " " + s));
                                }
                            }
                        }
                        break;

                }
            }
        }

        private string seatNo;
        /// <summary>
        /// 完整座位号
        /// </summary>
        public string SeatNo
        {
            get { return seatNo; }
            set { seatNo = value; }
        }
        private string shortSeatNo;
        /// <summary>
        /// 短座位号
        /// </summary>
        public string ShortSeatNo
        {
            get { return shortSeatNo; }
            set { shortSeatNo = value; }
        }
        private string bespeakDate;
        /// <summary>
        /// 预约日期
        /// </summary>
        public string BespeakDate
        {
            get { return bespeakDate; }
            set { bespeakDate = value; }
        }
        private string roomNo;
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string RoomNo
        {
            get { return roomNo; }
            set { roomNo = value; }
        }
        private List<DateTime> timeSpan = new List<DateTime>();
        /// <summary>
        /// 可预约时段
        /// </summary>
        public List<DateTime> TimeSpan
        {
            get { return timeSpan; }
            set { timeSpan = value; }
        }
    }
}