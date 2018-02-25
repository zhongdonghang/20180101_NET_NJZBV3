using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using SeatManage.IPocketBespeakBllService;
using WcfServiceForSeatManage;
using SeatBespeakException;
namespace SeatManage.PocketBespeakBllService
{
    public partial class PocketBespeakBllService : IPocketBespeakBllService.IPocketBespeakBllService
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
                readerNo = seatManage.CheckUser(user.LoginId, user.Password);
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
                UserInfo userInfo = seatManage.GetUserInfo(readerNo);
                //if (userInfo.UserType != SeatManage.EnumType.UserType.Reader)
                //{
                //    throw new Exception("权限验证失败");
                //}
                //验证成功，返回读者信息
                if (!string.IsNullOrEmpty(readerNo))
                {
                    ReaderInfo reader = seatManage.GetReader(readerNo, true);
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
