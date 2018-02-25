using System;
using SeatManage.ClassModel;

namespace SeatManage.PocketBespeakBllServiceV2
{
    public partial class PocketBespeakBllService : IPocketBespeakBllServiceV2.IPocketBespeakBllService
    {
        IWCFService.ISeatManageService seatManage = new WcfServiceForSeatManage.SeatManageDateService();
        /// <summary>
        /// 检查用户登录密码并返回用户信息。验证错误，返回用户不存在的异常
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="connStr">学校访问地址</param>
        /// <returns></returns>
        public SeatManage.ClassModel.ReaderInfo CheckAndGetReaderInfo(UserInfo user)
        {
            //验证密码
            string readerNo = "";
            try
            {
                //readerNo = seatManage.CheckUser(user.LoginId, user.Password);
                SeatManage.ClassModel.UserInfo reader = seatManage.GetUserInfo(user.LoginId);
                if (reader != null)
                {
                    if (reader.Password.Equals(SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(user.Password)) && reader.IsUsing == EnumType.LogStatus.Valid)
                    {
                        readerNo = reader.LoginId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (string.IsNullOrEmpty(readerNo))
            {
                throw new Exception("用户名或密码错误");
            }
            try
            {
                ReaderInfo reader = seatManage.GetReader(readerNo, true);
                if (reader != null)
                {

                    return reader;
                }
                else
                {
                    throw new Exception("用户名或密码错误");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("验证失败"));
            }
        }
        /// <summary>
        /// 获取读者信息，并查询读者当前记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public SeatManage.ClassModel.ReaderInfo GetReaderInfoByCardNo(string cardNo)
        {
            return seatManage.GetReader(cardNo, true);
        }




        public ReaderInfo GetReaderInfoByCardNofalse(string cardNo)
        {
            return seatManage.GetReader(cardNo, false);
        }
    }
}
