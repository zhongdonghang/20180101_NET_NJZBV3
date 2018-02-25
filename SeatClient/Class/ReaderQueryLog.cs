using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.Bll;
using System.Windows.Forms;
using System.Drawing;
using SeatClient.OperateResult;
using SeatManage.SeatManageComm;

namespace SeatClient.Class
{
    public class ReaderQueryLog
    {
        static SystemObject clientObject = SystemObject.GetInstance();
        /// <summary>
        /// 查询等待记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<WaitSeatLogInfo> QueryWaitSeatLogs(string cardNo, DateTime date)
        {
            //TODO:查询当天的等待记录 
            DateTime beginDate = DateTime.Parse(date.ToShortDateString() + " 0:0:0");
            DateTime endDate = DateTime.Parse(date.ToShortDateString() + " 23:59:59");
            List<WaitSeatLogInfo> waitLogs = T_SM_SeatWaiting.GetWaitSeatList(cardNo, null, beginDate.ToString(), endDate.ToString(), null);
            return waitLogs;
        }
        /// <summary>
        /// 查询预约记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<BespeakLogInfo> QueryBespeakLogs(string cardNo, DateTime date)
        {
            //TODO:查询当天的预约记录 
            DateTime beginDate = DateTime.Parse(date.ToShortDateString() + " 0:0:0");
            DateTime endDate = DateTime.Parse(date.ToShortDateString() + " 23:59:59");
            List<BespeakLogInfo> bespeakLogs = T_SM_SeatBespeak.GetBespeakList(cardNo, null, endDate, 0, null);
            return bespeakLogs;
        }
        /// <summary>
        /// 查询进出记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<EnterOutLogInfo> QueryEnterOugLogs(string cardNo, DateTime date)
        {
            //TODO:查询当天的进出记录 
            DateTime beginDate = DateTime.Parse(date.AddDays(-2).ToShortDateString() + " 0:0:0");
            DateTime endDate = DateTime.Parse(date.ToShortDateString() + " 23:59:59");
            List<EnterOutLogInfo> enterOutLogs = T_SM_EnterOutLog.GetEnterOutLogs(cardNo, null, null, beginDate, endDate);
            IEnumerable<EnterOutLogInfo> query = from items in enterOutLogs orderby items.EnterOutTime select items;
            List<EnterOutLogInfo> enterOutLogsSort = new List<EnterOutLogInfo>();
            foreach (var info in query)
            {
                enterOutLogsSort.Add(info);
            }
            return enterOutLogsSort;
        }
        /// <summary>
        /// 查询提示信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public List<ReaderNoticeInfo> QueryReaderNoticeLogs(string cardNo)
        {
            throw new Exception("未实现的方法");
        }
        /// <summary>
        /// 查询黑名单记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public List<BlackListInfo> QueryBlackListLogs(string cardNo)
        {
            throw new Exception("未实现的方法");
        }
        /// <summary>
        /// 查询违规记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public List<ViolationRecordsLogInfo> QueryViolationRecordsLogs(string cardNo)
        {
            throw new Exception("未实现的方法");
        }
        /// <summary>
        /// 获取label
        /// </summary>
        /// <param name="enterOutLogList"></param>
        /// <returns></returns>
        public List<Label> GetEnterOutLogLabels(List<EnterOutLogInfo> enterOutLogList)
        {
            List<Label> labels = new List<Label>();
            int dayCount = 0;
            for (int i = 0; i < enterOutLogList.Count; i++)
            {
                if (i == 0 || enterOutLogList[i].EnterOutTime.Date != enterOutLogList[i - 1].EnterOutTime.Date)
                {
                    Point pointd = new Point(40, 23 + ((i + dayCount) * 34));
                    Label lblEnterOutMessaged = new Label();
                    lblEnterOutMessaged.Font = new System.Drawing.Font("微软雅黑", 16, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lblEnterOutMessaged.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblEnterOutMessaged.BackColor = Color.Transparent;
                    lblEnterOutMessaged.Location = pointd;
                    lblEnterOutMessaged.Name = "lblLogMessage";
                    lblEnterOutMessaged.Size = new System.Drawing.Size(900, 34);
                    lblEnterOutMessaged.Text = enterOutLogList[i].EnterOutTime.ToLongDateString();// string.Format("{0}年{1}月{2}日", enterOutLogList[i].EnterOutTime.ToLongDateString() enterOutLogList[i].EnterOutTime.ToShortDateString().Split('-')[0], enterOutLogList[i].EnterOutTime.ToShortDateString().Split('-')[1], enterOutLogList[i].EnterOutTime.ToShortDateString().Split('-')[2]);
                    labels.Add(lblEnterOutMessaged);
                    dayCount++;
                }

                Point point = new Point(40, 23 + ((i + dayCount) * 34));
                Label lblEnterOutMessage = new Label();
                lblEnterOutMessage.Font = new System.Drawing.Font("微软雅黑", 15, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lblEnterOutMessage.Image = global::SeatClient.Properties.Resources.ico_arrow;
                lblEnterOutMessage.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
                lblEnterOutMessage.BackColor = Color.Transparent;
                lblEnterOutMessage.Location = point;
                lblEnterOutMessage.Name = "lblLogMessage";
                lblEnterOutMessage.Size = new System.Drawing.Size(900, 34);
                lblEnterOutMessage.Text = string.Format("     {0}  {1}", enterOutLogList[i].EnterOutTime.ToShortTimeString(), enterOutLogList[i].Remark);
                labels.Add(lblEnterOutMessage);
            }
            return labels;
        }

        /// <summary>
        /// 进出记录的消息
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private string EnterOutLogMessage(EnterOutLogInfo log)
        {
            ReadingRoomInfo room = null;
            try
            {
                room = clientObject.ReadingRoomList[log.ReadingRoomNo];
            }
            catch
            {
                room = T_SM_ReadingRoom.GetSingleRoomInfo(log.ReadingRoomNo);
            }
            string strMessage = "";
            if (room != null)
            {
                strMessage = string.Format("{0}  {1}", log.EnterOutTime.ToLongTimeString(), log.Remark);
            }
            else
            {
                strMessage = string.Format("{0}  {1}", log.EnterOutTime.ToLongTimeString(), log.Remark);

            }
            return strMessage;
        }
    }
}
