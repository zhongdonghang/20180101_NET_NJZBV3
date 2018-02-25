/***********************************************
 * 作者：王昊天
 * 创建时间：2013-5-23
 * 说明：后台服务设置
 * 修改人：
 * 修改时间：
 * ********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using SeatManage.DAL;
using SeatManage.EnumType;
using System.Data;
using System.Data.SqlClient;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        T_SM_SystemSet t_sm_service = new T_SM_SystemSet();

        /// <summary>
        /// 获取读者库同步设置
        /// </summary>
        ///<param name="ID">设置编号</param>
        /// <returns></returns>
        public StuLibSyncSetting GetStuLibSync()
        {
            try
            {
                DataSet ds = t_sm_service.GetList(" ServiceSetID = 1", null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return StuLibSyncSetting.Convert(ds.Tables[0].Rows[0]["ServiceSet"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateStuLibSync(StuLibSyncSetting set)
        {
            string strSet = set.ToString();
            SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
            parameters[0].Value = strSet;
            parameters[1].Value = 1;
            return t_sm_service.Update(strSet, parameters);
        }

        /// <summary>
        /// 获取黑名单设置
        /// </summary>
        /// <returns></returns>
        public RegulationRulesSetting GetRegulationRulesSetting()
        {
            DataSet ds = t_sm_service.GetList(" ServiceSetID = 2", null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return RegulationRulesSetting.Convert(ds.Tables[0].Rows[0]["ServiceSet"].ToString());
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更新黑名单设置
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public bool UpdateRegulationRulesSetting(RegulationRulesSetting set)
        {
            string strSet = set.ToString();
            SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
            parameters[0].Value = strSet;
            parameters[1].Value = 2;
            return t_sm_service.Update(strSet, parameters);

        }
        /// <summary>
        /// 更新门禁接口设置
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public bool UpdateAccessSetting(AccessSetting set)
        {
            string strSet = set.ToString();
            SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
            parameters[0].Value = strSet;
            parameters[1].Value = 4;
            return t_sm_service.Update(strSet, parameters);
        }
        /// <summary>
        /// 获取门禁接口设置
        /// </summary>
        /// <returns></returns>
        public AccessSetting GetAccessSetting()
        {
            DataSet ds = t_sm_service.GetList(" ServiceSetID = 4", null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return new AccessSetting(ds.Tables[0].Rows[0]["ServiceSet"].ToString());
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取默认打印模板
        /// </summary>
        /// <returns></returns>
        public string GetDefaultPrintTemplate()
        {
            DataSet ds = t_sm_service.GetList(" ServiceSetID = 5", null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["ServiceSet"].ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取使用手册
        /// </summary>
        /// <returns></returns>
        public UserGuideInfo GetUserGuide()
        {
            try
            {
                DataSet ds = t_sm_service.GetList(" ServiceSetID = 7", null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return UserGuideInfo.ToModel(ds.Tables[0].Rows[0]["ServiceSet"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 更新使用手册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUserGuide(UserGuideInfo model)
        {
            try
            {
                string strSet = model.ToXml();
                SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
                parameters[0].Value = strSet;
                parameters[1].Value = 7;
                return t_sm_service.Update(strSet, parameters);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取移动管理平台的配置
        /// </summary>
        /// <returns></returns>
        public MoveWebAppSetting GetMoveWebAppSetting()
        {
            try
            {
                DataSet ds = t_sm_service.GetList(" ServiceSetID = 10", null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MoveWebAppSetting model = new MoveWebAppSetting();
                    return model.ToModel(ds.Tables[0].Rows[0]["ServiceSet"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 保存移动管理平台配置
        /// </summary>
        /// <returns></returns>
        public bool SaveMoveWebAppSetting(MoveWebAppSetting model)
        {
            try
            {
                string strSet = model.ToString();
                DataSet ds = t_sm_service.GetList(" ServiceSetID = 10", null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
                    parameters[0].Value = strSet;
                    parameters[1].Value = 8;
                    return t_sm_service.Update(strSet, parameters);
                }
                else
                {
                    SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@SetName", SqlDbType.NVarChar,20),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
                    parameters[0].Value = strSet;
                    parameters[1].Value = "移动客户端设置";
                    parameters[2].Value = 8;
                    return t_sm_service.Add(strSet, parameters);
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 获取消息设置
        /// </summary>
        /// <returns></returns>
        public MsgPostSet GetMsgPostSet()
        {
            try
            {
                DataSet ds = t_sm_service.GetList(" ServiceSetID = 9", null);
                if (ds.Tables[0].Rows.Count > 0)
                { 
                    return MsgPostSet.Parse(ds.Tables[0].Rows[0]["ServiceSet"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 保存消息设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveMsgPostSet(MsgPostSet model)
        {
            DataSet ds = t_sm_service.GetList(" ServiceSetID = 9", null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
                parameters[0].Value = model.ToString();
                parameters[1].Value = 9;
                return t_sm_service.Update(null, parameters);
            }
            else
            {
                SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@SetName", SqlDbType.NVarChar,20),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
                parameters[0].Value = model.ToString();
                parameters[1].Value = "消息推送设置";
                parameters[2].Value = 9;
                return t_sm_service.Add(null, parameters);
            }
        }
        /// <summary>
        /// 获取消息设置
        /// </summary>
        /// <returns></returns>
        public PushMsssageSetting GetMsgPushSet()
        {
            try
            {
                DataSet ds = t_sm_service.GetList(" ServiceSetID = 9", null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return PushMsssageSetting.ToModel(ds.Tables[0].Rows[0]["ServiceSet"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 保存消息设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveMsgPushSet(PushMsssageSetting model)
        {
            DataSet ds = t_sm_service.GetList(" ServiceSetID = 9", null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
                parameters[0].Value = model.ToXml();
                parameters[1].Value = 9;
                return t_sm_service.Update(null, parameters);
            }
            else
            {
                SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@SetName", SqlDbType.NVarChar,20),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
                parameters[0].Value = model.ToXml();
                parameters[1].Value = "消息推送设置";
                parameters[2].Value = 9;
                return t_sm_service.Add(null, parameters);
            }
        }


        /// <summary>
        /// 获取手机网站设置
        /// </summary>
        /// <returns></returns>
        public PecketBookWebSetting GetPecketBookWebSetting()
        {
            try
            {
                DataSet ds = t_sm_service.GetList(" ServiceSetID = 8", null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return new PecketBookWebSetting(ds.Tables[0].Rows[0]["ServiceSet"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 更新手机网站设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePecketBookWebSetting(PecketBookWebSetting model)
        {
            try
            {
                string strSet = model.ToString();
                SqlParameter[] parameters = { 
                    new SqlParameter("@ServiceSet", SqlDbType.Text),
                    new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
                parameters[0].Value = strSet;
                parameters[1].Value = 8;
                return t_sm_service.Update(strSet, parameters);
            }
            catch
            {
                throw;
            }
        }


    }
}
