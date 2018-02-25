/******************************************
 * 作者：王昊天
 * 创建时间：2013-5-18
 * 说明：添加读者消息记录
 * 修改人：
 * 修改时间：
 * ******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using System.Data;
using SeatManage.DAL;
using SeatManage.EnumType;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        private T_SM_ReaderNotice t_sm_readerNotice_DAL = new T_SM_ReaderNotice();

        /// <summary>
        /// 获取读者消息列表
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ReaderNoticeInfo> GetReaderNoticeList(string cardNum, int pageIndex, int pageSize)
        {
            List<ReaderNoticeInfo> list = new List<ReaderNoticeInfo>();
            DataSet ds = t_sm_readerNotice_DAL.GetList(cardNum, pageIndex, pageSize);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ReaderNoticeInfo notice = dataRowToReaderNotice(ds.Tables[0].Rows[i]);
                list.Add(notice);
            }
            return list;
        }

        /// <summary>
        /// 根据学号和状态获取有效的进出记录
        /// </summary>
        /// <returns></returns>
        public List<ReaderNoticeInfo> GetReaderNoticeByCardNoStatus(string cardNo, LogStatus IsRead)
        {
            List<ReaderNoticeInfo> readerNoticeList = new List<ReaderNoticeInfo>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" cardNo=@CardNo ");
            if (IsRead != LogStatus.None)
            {
                strWhere.Append(" and IsRead =@IsRead order by IsRead desc,AddTime desc");
            }
            else
            {
                strWhere.Append(" and IsRead <>@IsRead order by IsRead desc,AddTime desc");
            }
            SqlParameter[] parameters = { 
                                            new SqlParameter("@CardNo",SqlDbType.NVarChar),
                                            new SqlParameter("@IsRead",SqlDbType.Int)
                                        };
            parameters[0].Value = cardNo;
            parameters[1].Value = (int)IsRead;
            DataSet ds = t_sm_readerNotice_DAL.GetList(strWhere.ToString(), parameters);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                readerNoticeList.Add(dataRowToReaderNotice(ds.Tables[0].Rows[i]));
            }
            return readerNoticeList;

        }
        /// <summary>
        /// 添加读者消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddReaderNotice(ReaderNoticeInfo model)
        {

            try
            {
                model.AddTime = GetServerDateTime();
                //Thread thread = new Thread(new ParameterizedThreadStart(NotifyMsg));
                //thread.Start(model);

                //ThreadPool.QueueUserWorkItem(new WaitCallback(sendSeatStateChanged), model);/



                //pushMsgV2(model);
                return t_sm_readerNotice_DAL.Add(model);
            }
            catch
            {
                throw;
            }
        }

        //private void pushMsg(ReaderNoticeInfo model)
        //{
        //    try
        //    {
        //        MsgPushCenter.MsgPushHandler pushMsgHandler = MsgPushCenter.MsgPushHandler.GetInstance();
        //        SeatNotice notice = new SeatNotice(model.CardNo, GetSchoolNum());
        //        notice.Type = model.Type;
        //        notice.Context = model.Note;
        //        //pushMsgHandler.PushMsg(notice);
        //        ThreadPool.QueueUserWorkItem(new WaitCallback(pushMsgHandler.PushMsg), notice);
        //    }
        //    catch
        //    {

        //    }
        //}
        SocketLib.SimpleSocketClient client = new SocketLib.SimpleSocketClient(ipEndPoint.IP, ipEndPoint.Port);
        private Thread thread;
        private void pushMsgV2(PushMsgInfo model)
        {
            try
            {
                SocketMsgData.SocketRequest request = new SocketMsgData.SocketRequest();
                request.Sender = GetSchoolNum();
                request.MsgType = SocketMsgData.TcpMsgDataType.MsgPush;
                request.Parameters.Add(model.ToString());
                request.SubSystem = SocketMsgData.TcpSeatManageSubSystem.SchoolService;

                //client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
                thread = new Thread(new ParameterizedThreadStart(client.Send));
                thread.Start(request);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(client.Send), SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
            
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 更新读者消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HandleResult UpdateReaderNotice(ReaderNoticeInfo model)
        {
            bool result = t_sm_readerNotice_DAL.Update(model);
            if (result)
            {
                return HandleResult.Successed;
            }
            else
            {
                return HandleResult.Failed;
            }
        }

        #region 私有方法
        /// <summary>
        /// 行转换为实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private ReaderNoticeInfo dataRowToReaderNotice(DataRow dr)
        {
            ReaderNoticeInfo notice = new ReaderNoticeInfo();
            notice.Type = string.IsNullOrEmpty(dr["NoticeTitle"].ToString()) ? NoticeType.None : (NoticeType)int.Parse(dr["NoticeTitle"].ToString());
            notice.AddTime = DateTime.Parse(dr["AddTime"].ToString());
            notice.CardNo = dr["CardNo"].ToString();
            notice.IsRead = (LogStatus)int.Parse(dr["IsRead"].ToString());
            notice.Note = dr["NoticeContent"].ToString();
            notice.NoticeID = int.Parse(dr["NoticeId"].ToString());
            return notice;
        }
        #endregion

        /// <summary>
        /// 设置已读
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public string SetReaderNoteRead(List<ReaderNoticeInfo> modelList)
        {
            string str = "";
            for (int i = 0; i < modelList.Count; i++)
            {
                modelList[i].IsRead = SeatManage.EnumType.LogStatus.Fail;
                str += UpdateReaderNotice(modelList[i]);
            }
            return str;
        }
    }
}
