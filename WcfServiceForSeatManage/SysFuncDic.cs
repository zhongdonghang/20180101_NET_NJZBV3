/******************************************
 * 作者：王昊天
 * 创建时间：2013-6-5
 * 说明：功能页面操作
 * 修改人：
 * 修改时间：
 * ******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;
using System.Data.SqlClient; 
using SeatManage.ClassModel;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        SeatManage.DAL.SysFuncDic sysfuncdic_dal = new SeatManage.DAL.SysFuncDic();
        /// <summary>
        /// 获取功能页面信息列表
        /// </summary>
        /// <returns></returns>
        public List<SysFuncDicInfo> GetFuncPage(string Order,string No)
        {
            string strWhere = "";
            if (!string.IsNullOrEmpty(Order))
            {
                if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = " Orderseq ='" + Order + "'";
                }
                else
                {
                    strWhere = " and Orderseq ='" + Order + "'";
                }
            }
            if (!string.IsNullOrEmpty(No))
            {
                if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = " Modseq ='" + No + "'";
                }
                else
                {
                    strWhere = " and Modseq ='" + No + "'";
                }
            }
            List<SysFuncDicInfo> list = new List<SysFuncDicInfo>();
            try
            {
                DataSet ds = sysfuncdic_dal.GetList(strWhere);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(DataRowToSysFuncDic(dr));
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 添加功能页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddNewFuncPage(SysFuncDicInfo model)
        {
            try
            {
                List<SysFuncDicInfo> Same_No = GetFuncPage(null, model.No);
                if (Same_No.Count > 0)
                {
                    return "功能页面编号重复";
                }
                else
                {
                    bool ok = sysfuncdic_dal.Add(model);
                    if (ok)
                    {
                        return "";
                    }
                    else
                    {
                        return "添加功能页面失败";
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 修改功能页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateFuncPage(SysFuncDicInfo model)
        {
            try
            {
                return sysfuncdic_dal.Update(model);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除功能页面，连表删除，会把菜单以及菜单相关联的权限删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteFuncPage(SysFuncDicInfo model)
        {
            try
            {
                return sysfuncdic_dal.Delete(model);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 数据行转换成model
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private SysFuncDicInfo DataRowToSysFuncDic(DataRow dr)
        {
            //ModSeq,MCaption,MenuLink,OrderSeq
            SysFuncDicInfo sfd = new SysFuncDicInfo();
            sfd.No = dr["ModSeq"].ToString();
            sfd.Name = dr["MCaption"].ToString();
            sfd.PageUrl = dr["MenuLink"].ToString();
            sfd.Order = dr["OrderSeq"].ToString();
            return sfd;
        }
    }
}
