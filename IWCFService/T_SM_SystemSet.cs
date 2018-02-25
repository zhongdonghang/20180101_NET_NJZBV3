/***************************************************
 * 作者：王昊天
 * 时间：2013-5-23
 * 说明：服务设置
 * 修改人：王随
 * 修改时间：6月14日 14:59 把名称T_SM_ServiceSet修改为T_SM_SystemSet
 * ************************************************/
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
        /// <summary>
        /// 获取服务设置
        /// </summary>
        ///<param name="ID">设置编号</param>
        /// <returns></returns>
        [OperationContract]
        StuLibSyncSetting GetStuLibSync();
        /// <summary>
        /// 更新读者库同步设置
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateStuLibSync(StuLibSyncSetting set);
        /// <summary>
        /// 获取黑名单设置
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        RegulationRulesSetting GetRegulationRulesSetting();
        /// <summary>
        /// 更新黑名单设置
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateRegulationRulesSetting(RegulationRulesSetting set);
        /// <summary>
        /// 更新门禁接口设置
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateAccessSetting(AccessSetting set);
        /// <summary>
        /// 获取门禁接口设置
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        AccessSetting GetAccessSetting();
        /// <summary>
        /// 获取默认打印模板
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetDefaultPrintTemplate();
        /// <summary>
        /// 获取用户指南
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        UserGuideInfo GetUserGuide();
        /// <summary>
        /// 更新使用手册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateUserGuide(UserGuideInfo model);
        /// <summary>
        /// 获取移动客户端设置
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MoveWebAppSetting GetMoveWebAppSetting();
        /// <summary>
        /// 保存移动终端配置
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool SaveMoveWebAppSetting(MoveWebAppSetting model);
        /// <summary>
        /// 获取消息推送设置
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MsgPostSet GetMsgPostSet();
        /// <summary>
        /// 保存消息设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool SaveMsgPostSet(MsgPostSet model);
        /// <summary>
        /// 获取手机网站设置
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PecketBookWebSetting GetPecketBookWebSetting();
        /// <summary>
        /// 更新手机网站设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdatePecketBookWebSetting(PecketBookWebSetting model);

        /// <summary>
        /// 获取消息推送设置
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PushMsssageSetting GetMsgPushSet();
        /// <summary>
        /// 保存消息设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool SaveMsgPushSet(PushMsssageSetting model);

    }
}
