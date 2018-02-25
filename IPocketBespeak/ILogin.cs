using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
namespace SeatManage.IPocketBespeak
{
    public interface ILogin
    {
        /// <summary>
        /// 从本地服务器查询所有的学校记录
        /// 把学校信息存入Session
        /// </summary>
        /// <returns></returns>
        List<AMS.Model.AMS_School> GetAllSchoolFromLocal();

        /// <summary>
        /// 检查用户登录密码并返回用户信息。验证错误，返回用户不存在的异常
        /// 验证成功后把ReaderInfo存入Session
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="school">学校信息</param>
        /// <returns></returns>
        SeatManage.ClassModel.ReaderInfo CheckAndGetReaderInfo(UserInfo user, AMS.Model.AMS_School school);
        /// <summary>
        /// 根据学号获取读者信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        SeatManage.ClassModel.ReaderInfo GetReaderInfoByCardNo(string cardNo, AMS.Model.AMS_School school);
        /// <summary>
        /// 获取单个学校信息
        /// </summary>
        /// <param name="schoolId">学校Id</param>
        /// <returns></returns>
        AMS.Model.AMS_School GetSingleSchoolInfo(string schoolId);
        /// <summary>
        /// 根据学号获取读者简单信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        SeatManage.ClassModel.ReaderInfo GetReaderInfoByCardNofalse(string cardNo, AMS.Model.AMS_School school);

    }
}
