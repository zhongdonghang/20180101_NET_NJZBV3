namespace WeiXinPocketBookOnline.Code
{
    /// <summary>
    /// 二维码扫描页面的参数类,url格式：?param=******；参数格式：seatNum=xxx&readingRoomNum=xxxx ,
    /// 例如：http://yuyue.juneberry.cn/BookSeat/ScanCode.aspx?param=****
    /// </summary>
    public class ScanCodeParamModel
    { 
        string readingRoomNum;
        string seatNum;
       
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNum
        {
            get { return seatNum; }
            set { seatNum = value; }
        }
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string ReadingRoomNum
        {
            get { return readingRoomNum; }
            set { readingRoomNum = value; }
        }
        public ScanCodeParamModel(string ciphertext)
        {
            string plainText = SeatManage.SeatManageComm.AESAlgorithm.UrlDecode(ciphertext);
            string[] strArr = plainText.Split('&');
            for (int i = 0; i < strArr.Length; i++)
            {
                string[] itemArr = strArr[i].Split('=');
                switch (itemArr[0])
                { 
                    case "readingRoomNum":
                        this.readingRoomNum = itemArr[1];
                        break;
                    case "seatNum":
                        this.seatNum = itemArr[1];
                        break;
                }
            }
        }
    }
}