using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.JsonModel;

namespace SeatManage.ServiceHelper
{
    public partial class ServiceHelper : IFreeSeat
    {
        public string FreeSeat(string cardNo)
        {
            try
            {
                ReaderInfo reader = null;
                JsonModel.JM_HandleResult result = new JsonModel.JM_HandleResult();
                try
                {
                    reader = seatDataService.GetReader(cardNo, true);
                }
                catch (Exception ex)
                {
                    result.Result = false;
                    result.Msg = "获取读者状态遇到异常";
                }
                if (reader != null)
                {
                    if (reader.EnterOutLog == null)
                    {
                        result.Result = false;
                        result.Msg = "释放座位失败，您还没有选座。"; 
                        return SeatManageComm.JSONSerializer.Serialize(result);
                    }
                    switch (reader.EnterOutLog.EnterOutState)
                    {
                        case EnterOutLogType.BookingConfirmation://预约入座
                        case EnterOutLogType.SelectSeat:    //选座
                        case EnterOutLogType.ContinuedTime: //续时
                        case EnterOutLogType.ComeBack:      //暂离回来
                        case EnterOutLogType.ReselectSeat:  //重新选座
                        case EnterOutLogType.WaitingSuccess: //读者通过等待座位入座
                        case EnterOutLogType.ShortLeave:    //读者暂离 
                            reader.EnterOutLog.EnterOutState = EnterOutLogType.Leave;
                            reader.EnterOutLog.Remark = "读者通过手机客户端释放座位。";
                            reader.EnterOutLog.Flag = Operation.Reader;
                            reader.EnterOutLog.EnterOutTime = DateTime.Now;

                            int newId = -1;
                            HandleResult returnResult = seatDataService.AddEnterOutLogInfo(reader.EnterOutLog, ref newId);
                            if (returnResult == HandleResult.Successed)
                            {
                                result.Result = true;
                                result.Msg = "座位释放成功。";
                            }
                            else
                            {
                                result.Result = false;
                                result.Msg = "未知原因释放座位失败";
                            }
                            break;
                        case EnterOutLogType.Leave:
                        default:
                            result.Result = false;
                            result.Msg = "您当前没有座位。";
                            break;
                    }
                }
                else
                {
                    result.Result = false;
                    result.Msg = "执行遇到错误。";
                }
                return SeatManageComm.JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("释放座位遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
    }
}
