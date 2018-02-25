/******************************************
 * 作者：王昊天
 * 创建时间：2013-5-18
 * 说明：黑名单记录操作
 * 修改人：
 * 修改时间：
 * ******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using SeatManage.DAL;
using SeatManage.EnumType;
using System.Data;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        #region 黑名单查询相关操作
        private T_SM_Blacklist t_sm_Blacklist = new T_SM_Blacklist();
        /// <summary>
        /// 获取有效黑名单列表
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public List<BlackListInfo> GetBlacklistInfo(string cardNo)
        {
            List<BlackListInfo> blacklists = new List<BlackListInfo>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" BlacklistState={0}", (int)LogStatus.Valid));

            if (!string.IsNullOrEmpty(cardNo))
            {
                strWhere.Append(string.Format(" and cardNo='{0}'", cardNo));
            }
            try
            {
                DataSet ds = t_sm_Blacklist.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BlackListInfo model = new BlackListInfo();
                    DataRowToBlacklist(ds.Tables[0].Rows[i], ref model);
                    blacklists.Add(model);
                }
            }
            catch
            {
                throw;
            }
            return blacklists;
        }
        /// <summary>
        /// 获取一条黑名单记录
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BlackListInfo GetBlistList(string ID)
        {
            BlackListInfo blacklist = new BlackListInfo();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" BlacklistID='" + ID + "'");
            try
            {
                DataSet ds = t_sm_Blacklist.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRowToBlacklist(ds.Tables[0].Rows[0], ref blacklist);
                }
                else
                {
                    blacklist = null;
                }
            }
            catch
            {
                throw;
            }
            return blacklist;
        }

        public List<BlackListInfo> GetBlacklistInfosByPage(string cardNo, int pageIndex, int pageSize)
        {
            List<BlackListInfo> blacklists = new List<BlackListInfo>();
            DataSet ds = t_sm_Blacklist.GetList(cardNo, pageIndex, pageSize);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                BlackListInfo model = new BlackListInfo();
                DataRowToBlacklist(ds.Tables[0].Rows[i], ref model);
                blacklists.Add(model);
            }
            return blacklists;
        }

        /// <summary>
        /// 查询黑名单记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="status"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<BlackListInfo> GetBlacklistInfos(string cardNo, LogStatus status, string beginDate, string endDate)
        {
            List<BlackListInfo> blacklists = new List<BlackListInfo>();
            StringBuilder strWhere = new StringBuilder();
            if (status != LogStatus.None)
            {
                strWhere.Append(string.Format(" BlacklistState={0}", (int)status));
            }

            if (!string.IsNullOrEmpty(cardNo))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" CardNo = '{0}'", cardNo));
                }
                else
                {
                    strWhere.Append(string.Format("and CardNo = '{0}'", cardNo));
                }
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" AddTime >= '{0}'", beginDate));
                }
                else
                {
                    strWhere.Append(string.Format("and AddTime >= '{0}'", beginDate));
                }
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" AddTime <= '{0}'", endDate));
                }
                else
                {
                    strWhere.Append(string.Format("and AddTime <= '{0}'", endDate));
                }
            }
            try
            {
                DataSet ds = t_sm_Blacklist.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BlackListInfo model = new BlackListInfo();
                    DataRowToBlacklist(ds.Tables[0].Rows[i], ref model);
                    blacklists.Add(model);
                }
            }
            catch
            {
                throw;
            }
            return blacklists;
        }

        /// <summary>
        /// 根据学号模糊查询黑名单记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="status"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<BlackListInfo> GetBlacklistInfos_ByFuzzySearch(string cardNo, LogStatus status, string beginDate, string endDate)
        {
            List<BlackListInfo> blacklists = new List<BlackListInfo>();
            StringBuilder strWhere = new StringBuilder();
            if (status != LogStatus.None)
            {
                strWhere.Append(string.Format(" BlacklistState={0}", (int)status));
            }

            if (!string.IsNullOrEmpty(cardNo))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" CardNo like '%{0}%'", cardNo));
                }
                else
                {
                    strWhere.Append(string.Format("and CardNo like '%{0}%'", cardNo));
                }
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" AddTime >= '{0}'", beginDate));
                }
                else
                {
                    strWhere.Append(string.Format("and AddTime >= '{0}'", beginDate));
                }
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" AddTime <= '{0}'", endDate));
                }
                else
                {
                    strWhere.Append(string.Format("and AddTime <= '{0}'", endDate));
                }
            }
            try
            {
                DataSet ds = t_sm_Blacklist.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BlackListInfo model = new BlackListInfo();
                    DataRowToBlacklist(ds.Tables[0].Rows[i], ref model);
                    blacklists.Add(model);
                }
            }
            catch
            {
                throw;
            }
            return blacklists;
        }

        /// <summary>
        /// 添加一条黑名单记录
        /// </summary>
        /// <param name="blacklist"></param>
        /// <returns>黑名单序列号</returns>
        public int AddBlacklist(BlackListInfo blacklist)
        {
            try
            {
                int i = t_sm_Blacklist.Add(blacklist);
                //黑名单添加后，添加提醒
                //ReaderNoticeInfo blackRni = new ReaderNoticeInfo();
                //blackRni.IsRead = LogStatus.Valid;
                //blackRni.CardNo = blacklist.CardNo;
                //blackRni.Note = blacklist.ReMark;
                //blackRni.Type = NoticeType.AddBlacklistWarning;
                //AddReaderNotice(blackRni);

                PushMsgInfo msg = new PushMsgInfo();
                msg.Title = "您好，你已被加入黑名单";
                msg.MsgType = MsgPushType.EnterBlack;
                msg.StudentNum = blacklist.CardNo;
                msg.Message = blacklist.ReMark;
                msg.AddTime = blacklist.AddTime;
                msg.LeaveDate = blacklist.OutTime;
                msg.IsAutoLeaveBlack = blacklist.OutBlacklistMode == LeaveBlacklistMode.AutomaticMode;
                SendMsg(msg);

                return i;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateBlacklist(BlackListInfo blacklist)
        {
            try
            {
                if (blacklist.BlacklistState == LogStatus.Fail)
                {
                    ////黑名单添加后，添加提醒
                    //ReaderNoticeInfo blackRni = new ReaderNoticeInfo();
                    //blackRni.IsRead = LogStatus.Valid;
                    //blackRni.CardNo = blacklist.CardNo;
                    //blackRni.Type = NoticeType.DeleteBlacklistWarning;
                    //blackRni.Note = "黑名单已过期。";
                    //AddReaderNotice(blackRni);

                    PushMsgInfo msg = new PushMsgInfo();
                    msg.Title = "您好，您的黑名单已失效";
                    msg.MsgType = MsgPushType.LeaveVrBlack;
                    msg.StudentNum = blacklist.CardNo;
                    msg.AddTime = blacklist.AddTime;
                    msg.LeaveDate = DateTime.Now;
                    msg.Message = string.Format("您在{0}的黑名单状态已经到期或被管理员取消，请遵守系统使用规则。",blacklist.AddTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    SendMsg(msg);

                }
                return t_sm_Blacklist.Update(blacklist);
            }
            catch
            {
                throw;
            }
        }

        public int DeleteBlacklist(string cardNo, string roomNum, string beginDate, string endDate)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 数据行转换为实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="blacklist"></param>
        /// <returns></returns>
        private BlackListInfo DataRowToBlacklist(DataRow dr, ref BlackListInfo blacklist)
        {
            blacklist.ID = dr["BlacklistID"].ToString();
            blacklist.CardNo = dr["CardNo"].ToString();
            blacklist.AddTime = DateTime.Parse(dr["AddTime"].ToString());
            blacklist.BlacklistState = (LogStatus)int.Parse(dr["BlacklistState"].ToString());
            blacklist.OutTime = DateTime.Parse(dr["OutTime"].ToString());
            blacklist.ReadingRoomID = dr["ReadingRoomNo"].ToString();
            blacklist.ReadingRoomName = dr["ReadingRoomName"].ToString();
            blacklist.ReMark = dr["Remark"].ToString();
            blacklist.ReaderName = dr["ReaderName"].ToString();
            if (dr["ReaderTypeName"] != null)
            {
                blacklist.TypeName = dr["ReaderTypeName"].ToString();
            }
            if (dr["ReaderDeptName"] != null)
            {
                blacklist.DeptName = dr["ReaderDeptName"].ToString();
            }
            if (dr["Sex"] != null)
            {
                blacklist.Sex = dr["Sex"].ToString();
            }
            blacklist.OutBlacklistMode = (LeaveBlacklistMode)int.Parse(dr["OutBlacklist"].ToString());
            return blacklist;
        }
        #endregion
    }
}
