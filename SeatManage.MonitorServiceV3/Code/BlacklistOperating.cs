using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatService.MonitorService.Code
{
    public partial class SeatDataOperation
    {
        /// <summary>
        /// 清除黑名单
        /// </summary>
        /// <param name="readingRooms"></param>
        public void BlacklistOperating()
        {
            try
            {
                DateTime nowDateTime = ServiceDateTime.Now;
                List<BlackListInfo> bllist = T_SM_Blacklist.GetBlackListInfo(null);
                if (bllist != null)
                {
                    foreach (BlackListInfo bli in bllist)
                    {
                        if ((bli.OutTime < nowDateTime) && (bli.OutBlacklistMode == LeaveBlacklistMode.AutomaticMode))
                        {
                            bli.BlacklistState = LogStatus.Fail;
                            T_SM_Blacklist.UpdateBlackList(bli);
                            WriteLog.Write("监控服务：读者" + bli.CardNo + "，处罚结束，离开黑名单");

                            //ReaderNoticeInfo notice = new ReaderNoticeInfo();
                            //notice.CardNo = bli.CardNo;
                            //notice.Type = NoticeType.DeleteBlacklistWarning;
                            //notice.Note = "您的黑名单处罚已结束，请遵守座位使用规则，请勿再次违规。";
                            //T_SM_ReaderNotice.AddReaderNotice(notice);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("监控服务：黑名单处理遇到异常：{0}", e.Message));
            }
        }
    }
}
