using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.Bll
{
   public class AMS_Terminal
    {
       /// <summary>
       /// 根据编号删除终端
       /// </summary>
       /// <param name="clintNum"></param>
       /// <returns></returns>
       public static SeatManage.EnumType.HandleResult DeleteTerminal(string clintNum)
       {
           IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
           bool error = false;
           try
           {
               return seatService.DeleteTerminal(clintNum);
           }
           catch (Exception ex)
           {
               error = true;
               SeatManageComm.WriteLog.Write("删除终端失败：" + ex.Message);
               return EnumType.HandleResult.Failed;
           }
          
       }
       /// <summary>
       /// 添加终端信息
       /// </summary>
       /// <param name="clientConfig"></param>
       /// <returns></returns>
       public static SeatManage.EnumType.HandleResult AddClientSetting(SeatManage.ClassModel.TerminalInfo clientConfig)
       {
           IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
           bool error = false;
           try
           {
               if (seatService.AddClientSetting(clientConfig) > 0)
               {
                   return EnumType.HandleResult.Successed;
               }
               else
               {
                   return EnumType.HandleResult.Failed;
               }
           }
           catch (Exception ex)
           {
               error = true;
               SeatManageComm.WriteLog.Write("删除终端失败：" + ex.Message);
               throw;
           }
           
       }
    }
}
