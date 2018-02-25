using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinJK.Model;

namespace PocketBookOnline.Code
{
    public class AutoLoginParamModel
    {
        public AutoLoginParamModel()
        { }
        public AutoLoginParamModel(string ciphertext)
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
                    case "cardNo":
                        CardNo = itemArr[1];
                        break;
                    case "schoolId":
                        SchoolId = itemArr[1];
                        break;
                    case "operateKey":
                       // OperateKey = itemArr[1]; 
                        OperateKey = EnumMenuKey.None;
                        break; 
                        
                }
            }
        }
        /// <summary>
        /// 加密字符串转换为参数Model
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        public static AutoLoginParamModel Parse(string ciphertext)
        {
            AutoLoginParamModel model = new AutoLoginParamModel();
            string plainText = SeatManage.SeatManageComm.AESAlgorithm.DESDecode(ciphertext);
            string[] strArr = plainText.Split('&');
            foreach (string item in strArr)
            {
                string[] itemArr = item.Split('=');
                if (itemArr.Length != 2)
                {
                    return null;
                }
                switch (itemArr[0])
                {
                    case "cardNo":
                        model.CardNo = itemArr[1];
                        break;
                    case "schoolId":
                        model.SchoolId = itemArr[1];
                        break;
                    case "operateKey":
                        {
                            switch (itemArr[1])
                            { 
                                case "0":
                                    model.OperateKey = EnumMenuKey.BindWeiXinId;
                                    break;
                                case "1":
                                    model.OperateKey = EnumMenuKey.GetBespeakLog;
                                    break;
                                case "2":
                                    model.OperateKey = EnumMenuKey.GetRoomUsedState;
                                    break;
                                case "3":
                                    model.OperateKey = EnumMenuKey.ShortLeave;
                                    break;
                                case "4":
                                    model.OperateKey = EnumMenuKey.FreeSeat;
                                    break;
                                case "5":
                                    model.OperateKey = EnumMenuKey.ReserveSeat;
                                    break;
                                case "6":
                                    model.OperateKey = EnumMenuKey.BlackList;
                                    break;
                                case "7":
                                    model.OperateKey = EnumMenuKey.GetReaderState;
                                    break;
                                case "8":
                                    model.OperateKey = EnumMenuKey.GetRules;
                                    break;
                                default :
                                    model.OperateKey = EnumMenuKey.None;
                                    break;

                            }
                            

                           
                        }
                        break;

                }
            }
            return model;
        }

        private string cardNo;
        /// <summary>
        /// 学号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        private string schoolId;
        /// <summary>
        /// 学校Id
        /// </summary>
        public string SchoolId
        {
            get { return schoolId; }
            set { schoolId = value; }
        }
        private EnumMenuKey operateKey;
        /// <summary>
        /// 操作(要跳转的页面)
        /// </summary>
        public EnumMenuKey OperateKey
        {
            get { return operateKey; }
            set { operateKey = value; }
        }
    }
}