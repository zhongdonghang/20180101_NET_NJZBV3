using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Collections;

namespace DBUtility
{
    class GetSQL
    {
        public GetSQL()
        { }
        private string t_HXSQ_MZ_I002 = @" SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_I003Name AS 姓名, 
                      MZ_I003Date AS 出生年月, MZ_I003Address AS 家庭住址, MZ_I003Unit AS 原工作单位, MZ_I003Linkman AS 联系人, 
                      MZ_I003LinkmanLXDH AS 联系人电话, MZ_I003CJDJName AS 残疾级别, MZ_I003YDJE AS 优待金额, MZ_I003Remark AS 内容  FROM   dbo.T_HXSQ_MZ_I002 ";
        private string t_HXSQ_MZ_I003 = @"SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_I003Name AS 姓名, 
                      MZ_I003Date AS 出生年月, MZ_I003Address AS 家庭住址, MZ_I003Unit AS 原工作单位, MZ_I003Linkman AS 联系人, 
                      MZ_I003LinkmanLXDH AS 联系人电话, MZ_I003Remark AS 内容  FROM dbo.T_HXSQ_MZ_I003";
        private string t_HXSQ_MZ_I004 = @"SELECT     CreateTime AS 上报时间, A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, MZ_I004PositionName AS 领导小组职位, 
                      MZ_I004PositionName2 AS 工作小组职位, MZ_I004PoliticsName AS 政治面貌, MZ_I004DegreeName AS 文化程度, 
                      MZ_I004Phone AS 联系方式 FROM dbo.T_HXSQ_MZ_I004";
        private string t_HXSQ_MZ_I005 = @"SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_I005LSName AS 烈士姓名, 
                      MZ_I005Name AS 烈属姓名, MZ_I005Sexname AS 性别, MZ_I005Address AS 家庭住址, MZ_I005Remark AS 内容, 
                      MZ_I005Phone AS 联系方式  FROM  dbo.T_HXSQ_MZ_I005";
        private string t_HXSQ_MZ_J001 = @"SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_J001BName AS 被帮扶人姓名, 
                      MZ_J001Name AS 帮扶人姓名, MZ_J001PoliticsName AS 政治面貌, MZ_J001DegreeName AS 文化程度, MZ_J001Remark AS 联系方式, 
                      MZ_J001Phone AS 内容 FROM  dbo.T_HXSQ_MZ_J001";
        private string t_HXSQ_MZ_K001 = @"SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_K001Name AS 姓名, 
                      MZ_K001Remark AS 出生年月, MZ_K001Positionname AS 职位, MZ_K001PoliticsName AS 政治面貌, MZ_K001DegreeName AS 文化程度, 
                      MZ_K001Phone AS 联系方式 FROM dbo.T_HXSQ_MZ_K001";
        private string t_HXSQ_MZ_K002 = @"SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_K002Name AS 姓名, 
                      MZ_K002Sexname AS 性别, MZ_K002Address AS 家庭住址, MZ_K002Phone AS 联系方式, MZ_K002Remark AS 内容 FROM  dbo.T_HXSQ_MZ_K002";
        private string t_HXSQ_MZ_L001 = @"SELECT     A03004name AS 所在社区, A03004, A03003name AS 所在街道, A03003, CreateTime AS 上报时间, MZ_L001Name AS 姓名, 
                      MZ_L001Positionname AS 职位, MZ_L001PoliticsName AS 政治面貌, MZ_L001DegreeName AS 文化程度, MZ_L001Phone AS 联系方式, 
                      MZ_L001Remark AS 内容 FROM dbo.T_HXSQ_MZ_L001";
        private string t_HXSQ_MZ_L003 = @"SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_L003Name AS 姓名, 
                      MZ_L003Positionname AS 职位, MZ_L003PoliticsName AS 政治面貌, MZ_L003Phone AS 联系方式, MZ_L003DegreeName AS 文化程度, 
                      MZ_L003Remark AS 内容 FROM  dbo.T_HXSQ_MZ_L003";
        private string t_HXSQ_MZ_M003 = @"SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_M003Name AS 姓名, 
                      MZ_M003Sexname AS 性别    FROM  dbo.T_HXSQ_MZ_M003";
        private string t_HXSQ_MZ_M009 = @"SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_M009Name AS 姓名, 
                      MZ_M009Sexname AS 性别    FROM  dbo.T_HXSQ_MZ_M009";
        private string t_HXSQ_MZ_N001 = @"SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_N001Name AS 姓名, 
                      MZ_N001Position AS 职位    FROM  dbo.T_HXSQ_MZ_N001";
        private string t_HXSQ_MZ_H007 = @"SELECT     A03003, A03003name AS 所在街道, A03004, A03004name AS 所在社区, CreateTime AS 上报时间, MZ_H007Name AS 姓名, MZ_H007Age AS 年龄, 
                      MZ_H007Birth AS 出生年月, MZ_H007Address AS 家庭住址, MZ_H007Phone AS 联系电话, MZ_H007Remark AS 内容 FROM  dbo.T_HXSQ_MZ_H007";
        /// <summary>
        /// 革命伤残军人联系表

        /// </summary>
        public string T_HXSQ_MZ_I002
        {
            get { return t_HXSQ_MZ_I002; }
        }
        /// <summary>
        /// 因公牺牲家庭联系表

        /// </summary>
        public string T_HXSQ_MZ_I003
        {
            get { return t_HXSQ_MZ_I003; }
        }
        /// <summary>
        /// 双拥工作小组
        /// </summary>
        public string T_HXSQ_MZ_I004
        {
            get { return t_HXSQ_MZ_I004; }
        }
        /// <summary>
        /// 军烈属联系表
        /// </summary>
        public string T_HXSQ_MZ_I005
        {
            get { return t_HXSQ_MZ_I005; }
        }
        /// <summary>
        /// 帮扶小组名单
        /// </summary>
        public string T_HXSQ_MZ_J001
        {
            get { return t_HXSQ_MZ_J001; }
        }
        /// <summary>
        /// 村（居）委会建设网络
        /// </summary>
        public string T_HXSQ_MZ_K001
        {
            get { return t_HXSQ_MZ_K001; }
        }
        /// <summary>
        /// 居委会社区工作者名单

        /// </summary>
        public string T_HXSQ_MZ_K002
        {
            get { return t_HXSQ_MZ_K002; }
        }
        /// <summary>
        /// 五个工作委员会花名册
        /// </summary>
        public string T_HXSQ_MZ_L001
        {
            get { return t_HXSQ_MZ_L001; }
        }
        /// <summary>
        /// 辖区居民组长花名册

        /// </summary>
        public string T_HXSQ_MZ_L003
        {
            get { return t_HXSQ_MZ_L003; }
        }
        /// <summary>
        /// 殡改宣传员、信息员名单
        /// </summary>
        public string T_HXSQ_MZ_M003
        {
            get { return t_HXSQ_MZ_M003; }
        }
        /// <summary>
        /// 老年协会名单
        /// </summary>
        public string T_HXSQ_MZ_M009
        {
            get { return t_HXSQ_MZ_M009; }
        }
        /// <summary>
        /// 服务站成员名单

        /// </summary>
        public string T_HXSQ_MZ_N001
        {
            get { return t_HXSQ_MZ_N001; }
        }
        /// <summary>
        /// 空巢老人名单管理
        /// </summary>
        public string T_HXSQ_MZ_H007
        {
            get { return t_HXSQ_MZ_H007; }
        }
    }
}
