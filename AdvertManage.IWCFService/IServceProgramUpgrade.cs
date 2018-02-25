using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.IWCFService
{
     [ServiceContract]
    public partial interface IAdvertManageService
    {
         /// <summary>
         /// 根据Id获取程序信息
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
         [OperationContract]
         AdvertManage.Model.ProgramUpgradeModel GetProgramInfoById(int id);
         /// <summary>
         /// 根据程序类型获取对应的程序信息
         /// </summary>
         /// <param name="programType"></param>
         /// <returns></returns>
         [OperationContract]
         AdvertManage.Model.ProgramUpgradeModel GetProgramInfoByProgramType(Model.Enum.SeatManageSubsystem programType);
         /// <summary>
         /// 发布新版本的程序
         /// </summary>
         /// <param name="programModel"></param>
         /// <param name="programType"></param>
         /// <returns></returns>
         [OperationContract]
         Model.Enum.HandleResult ReleaseProgram(AdvertManage.Model.ProgramUpgradeModel programModel );
         /// <summary>
         /// 获取所有已发布的程序信息
         /// </summary>
         /// <returns></returns>
           [OperationContract]
         List<AdvertManage.Model.ProgramUpgradeModel> GetAllProgramInfo();
    }
}
