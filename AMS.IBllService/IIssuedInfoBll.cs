using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService
    {
        /// <summary>
        /// 获取全部的下发记录列表
        /// *异常处理：直接向上层抛出
        /// </summary>
        /// <returns>获取成功返回List</returns>
        [OperationContract]
        List<AMS.Model.View_CommandList> GetAllCommandList();
        /// <summary>
        /// 根据条件获取下发记录列表
        /// *异常处理：直接向上层抛出
        /// </summary>
        /// <returns>获取成功返回List</returns>
        [OperationContract]
        List<AMS.Model.View_CommandList> GetCommandList(string schoolId, AMS.Model.Enum.CommandType commandType, AMS.Model.Enum.CommandHandleResult handleResult);
        /// <summary>
        /// 获取命令明细列表
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="commandType"></param>
        /// <param name="handleResult"></param>
        /// <returns></returns>
        [OperationContract]
        List<Model.AMS_CommandDetail> GetCommandDetailList(string schoolId, AMS.Model.Enum.CommandType commandType, AMS.Model.Enum.CommandHandleResult handleResult);
        /// <summary>
        /// 新增下发记录
        /// *异常处理：直接在catch中return异常信息，自定义异常直接return自定义的错误
        /// </summary>
        /// <param name="model">下发命令的model</param>
        /// <returns>成功返回null或""值，失败返回失败信息</returns>
        [OperationContract]
        string AddNewCommandList(AMS.Model.AMS_CommandList model);
        //[OperationContract]
        //string UpdateCommandList(AMS.Model.AMS_CommandList model);
        //[OperationContract]
        //string DeleteCommandList(AMS.Model.AMS_CommandList model);
        /// <summary>
        /// 新增优惠券校区记录
        /// *异常处理：直接在catch中return异常信息，自定义异常直接return自定义的错误
        /// </summary>
        /// <param name="model">model，需要校区ID和优惠券ID</param>
        /// <returns>成功返回空，失败返回错误信息</returns>
        [OperationContract]
        int AddNewCampusCommandList(AMS.Model.SlipReleaseCampus model);
        /// <summary>
        /// 取消下发
        /// *异常处理：直接在catch中return异常信息，自定义异常直接return自定义的错误
        /// </summary>
        /// <param name="model">model，需要校区ID和优惠券ID</param>
        /// <returns>成功返回空，失败返回错误信息</returns>
        [OperationContract]
        bool DelCommandDetailInfo(int id);
    }
}
