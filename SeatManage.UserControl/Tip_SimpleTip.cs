using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;
using SeatManage.Bll;

namespace SeatManage
{
    public partial class Tip_SimpleTip : UserControl
    {
        SeatClient.OperateResult.SystemObject clientobject = SeatClient.OperateResult.SystemObject.GetInstance();
        public Tip_SimpleTip(TipType type)
        {
            InitializeComponent();
            switch (type)
            {
                case TipType.WaitSeatFrequent:
                    lblWarning.Text = "    您距离上次设置别人暂离时间不足设定的时长，请稍后再试。";
                    break;
                case TipType.ReaderTypeInconformity:
                    lblWarning.Text = "   您的身份不允许选择该阅览室中的座位，请选择其他阅览室。";
                    break;
                case TipType.ContinuedTime:
                    string mes = "，";
                    string nmes = "。";
                    if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0)
                    {
                        mes = string.Format("，您还有{0}次续时机会,", clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes - clientobject.EnterOutLogData.Student.ContinuedTimeCount - 1);
                    }
                    DateTime dt = new DateTime();
                    DateTime nowDateTime = ServiceDateTime.Now;
                    if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                    {
                        dt = ServiceDateTime.Now.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.DelayTimeLength);
                        nmes = string.Format("请在{0}之前续时。", dt.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime));
                        if (dt > clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                        {
                            dt = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                            nmes = "在阅览室关闭前不用再次续时。";
                        }

                    }
                    else
                    {
                        for (int i = 0; i < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                        {
                            if (nowDateTime < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                            {
                                if (i + 1 < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                {
                                    dt = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                    nmes = string.Format("请在{0}之前续时。", dt.ToShortTimeString());
                                }
                                else
                                {
                                    dt = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                    nmes = "在阅览室关闭前不用再次续时。";
                                }
                                break;
                            }
                        }
                    }
                    lblWarning.Text = string.Format("   您已成功续时，座位使用时间延长至 {0}{1}{2}", dt.ToShortTimeString(), mes, nmes);
                    break;
                case TipType.BeapeatRoomNotExists:
                    string roomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                    lblWarning.Text = string.Format("   您预约的座位所在阅览室{0}不属于该触摸屏管理。", roomName);
                    break;
                case TipType.WaitSeatWithSeat:
                    lblWarning.Text = "   您已经有座位，无法设置其他读者暂离。";
                    break;
                case TipType.ContinuedTimeNoCount:
                    lblWarning.Text = "   您的续时次数不足，无法续时。";
                    break;
                case TipType.ContinuedTimeWithout:
                    lblWarning.Text = "   您可以继续使用座位到闭馆，无需再次续时。";
                    break;
                case TipType.ContinuedTimeNotTime:
                    lblWarning.Text = string.Format("   您使用座位时间过短，还没有到达可续时时间，请在 {0} 后续时。", clientobject.EnterOutLogData.Student.CanContinuedTime.ToShortTimeString());
                    break;
                case TipType.ReadingRoomFull:
                    lblWarning.Text = string.Format("         座位已满。");
                    break;
                case TipType.AutoContinuedTime:
                    string mesact = "。";
                    if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0)
                    {
                        mesact = string.Format("，您还有{0}次续时机会。", clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes - clientobject.EnterOutLogData.Student.ContinuedTimeCount);
                    }
                    DateTime dtact = ServiceDateTime.Now.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.DelayTimeLength);
                    lblWarning.Text = string.Format("   您在暂离期间在座超时，系统已自动续时，座位使用时间延长至 {0}{1}", dtact.ToShortTimeString(), mesact);
                    break;
                case TipType.AutoContinuedTimeNoCount:
                    lblWarning.Text = "   您在暂离期间在座超时，续时次数不足，系统自动续时失败，您的座位将被释放，请重新选择座位。";
                    break;
                case TipType.ShortLeaveSeatOverTime:
                    lblWarning.Text = "   您在暂离期间在座超时，您的座位将被释放，请重新选择座位。";
                    break;
                case TipType.Exception:
                    lblWarning.Text = "   系统执行遇到异常，请重试。如果还是出现该提示，请联系管理员。";
                    break;
            }
        }
    }
}
