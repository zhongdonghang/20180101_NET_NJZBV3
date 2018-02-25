/******************************************
 * 作者：王昊天
 * 创建日期：2013-6-4
 * 说明：用户阅览室权限接口实现
 * 修改人：
 * 修改时间：
 * ***************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;
using System.Data.SqlClient;
using SeatManage.ClassModel;
using SeatManage.DAL;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        private T_SM_ManagerPotency t_sm_managepotency = new T_SM_ManagerPotency();
        #region 接口实现
        /// <summary>
        /// 根据用户ID获取对应权限的阅览室
        /// </summary>
        /// <param name="LoginID"></param>
        /// <returns></returns>
        public ManagerPotency GetManagerPotencyByLoginID(string LoginID)
        {
            string strWhere = "LoginID='" + LoginID + "'";
            try
            {
                DataSet ds = t_sm_managepotency.GetList(strWhere, null);
                ManagerPotency mp = new ManagerPotency();
                mp.LoginID = LoginID;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    mp.UserName = ds.Tables[0].Rows[0]["UsrName"].ToString();
                }
                List<string> roomNums = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    roomNums.Add(dr["ReadingRoomNo"].ToString());
                }
                if (roomNums.Count == 0)
                {
                    mp.RightRoomList = new List<ReadingRoomInfo>();
                }
                else
                {
                    mp.RightRoomList = GetReadingRoomInfo(roomNums);
                }
                return mp;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 修改用户权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateManagerPotency(ManagerPotency model)
        {
            try
            {
                return t_sm_managepotency.AddUpdate(model);
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}
