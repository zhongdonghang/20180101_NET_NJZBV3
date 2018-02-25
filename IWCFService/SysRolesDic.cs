/*******************************************
 * 作者：王昊天
 * 创建时间：2013-6-3
 * 说明：角色操作
 * 修改人：
 * 修改日期：
 * *****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 根据用户ID获取对应的角色ID
        /// </summary>
        /// <param name="LoginID"></param>
        /// <returns></returns>
        [OperationContract]
        List<int> GetRoleID(string LoginID);
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="RolesID">角色ID，不输默认获取全部</param>
        /// <param name="RolesID">角色名称，不输默认获取全部</param>
        /// <returns></returns>
        [OperationContract]
        List<SysRolesDicInfo> GetRolesInfo(string RolesID,string RolesName);
        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string AddRole(SysRolesDicInfo model);
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateRole(SysRolesDicInfo model);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteRole(SysRolesDicInfo model);
        /// <summary>
        /// 判断是否有重复的角色名
        /// </summary>
        /// <param name="model">要判断的角色</param>
        /// <param name="mode">是否是更新判断</param>
        /// <returns></returns>
        [OperationContract]
        bool IsExistsSameRoleName(SysRolesDicInfo model,bool mode);
    }
}
