using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;
using SeatBespeakException;
namespace SeatManage.IPocketBespeakBllService
{
    [ServiceContract]
    public partial interface IPocketBespeakBllService
    {
        /// <summary>
        /// 检查用户登录密码并返回用户信息。验证错误，返回用户不存在的异常
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="connStr">学校访问地址</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LoginFailed))]
        SeatManage.ClassModel.ReaderInfo CheckAndGetReaderInfo(UserInfo user);
        /// <summary>
        /// 根据学号获取学生信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.ReaderInfo GetReaderInfoByCardNo(string cardNo);
        /// <summary>
        /// 根据学号获取学生简单信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.ReaderInfo GetReaderInfoByCardNofalse(string cardNo);
    }
}
