/********************************************
 * 作者：王昊天
 * 创建时间：2013-6-4
 * 说明：功能页面操作
 * 修改人：
 * 修改时间：
 * *****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;
namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        #region 功能页面相关操作
        /// <summary>
        /// 获取功能页面信息列表
        /// </summary>
        /// <param name="Order">分类，不填就默认查全部</param>
        /// <returns></returns>
        [OperationContract]
        List<SysFuncDicInfo> GetFuncPage(string Order,string Num);
        /// <summary>
        /// 添加功能页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string AddNewFuncPage(SysFuncDicInfo model);
        /// <summary>
        /// 修改功能页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateFuncPage(SysFuncDicInfo model);
        /// <summary>
        /// 删除功能页面，连表删除，会把菜单以及菜单相关联的权限删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteFuncPage(SysFuncDicInfo model);
        #endregion
    }
}
