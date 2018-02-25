using SeatManage.IWCFService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        private bool isPushMsg = ConfigurationManager.AppSettings["PushMsg"] == "1";
        public bool SendMsg(SeatManage.ClassModel.PushMsgInfo model)
        {
            try
            {
                SeatManage.ClassModel.PushMsssageSetting setting = GetMsgPushSet();
                if (setting.PushSetting[model.MsgType] || ((model.MsgType == SeatManage.EnumType.MsgPushType.EnterVR || model.MsgType == SeatManage.EnumType.MsgPushType.EnterBlack) && setting.PushSetting[SeatManage.EnumType.MsgPushType.EnterVrBlack]))
                {
                    SeatManage.ClassModel.ReaderNoticeInfo notice = new SeatManage.ClassModel.ReaderNoticeInfo();
                    notice.CardNo = model.StudentNum;
                    notice.Note = model.Message;
                    AddReaderNotice(notice);
                }
                if (!isPushMsg)
                {
                    return true;
                }
                model.SchoolNum = GetSchoolNum();
                model.AddTime = GetServerDateTime();
                pushMsgV2(model);
                return true;
            }
            catch
            {
              //  throw;
                //zdh 
                return true;
            }
        }
    }
}
