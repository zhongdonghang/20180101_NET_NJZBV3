/***************************************************
 * 作者：王昊天
 * 创建时间：2013-6-9
 * 说明：校区信息操作
 * 修改人：
 * 修改时间：
 * *************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.EnumType;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 新增校区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddSchoolInfo(SeatManage.ClassModel.School model);
        /// <summary>
        /// 修改校区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateSchool(SeatManage.ClassModel.School model);
        /// <summary>
        /// 删除校区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteSchool(SeatManage.ClassModel.School model);
        /// <summary>
        /// 获取校区列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.School> GetSchoolList(string no, string name);
    }
}
