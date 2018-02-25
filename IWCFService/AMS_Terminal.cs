using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.IWCFService
{ 
    public partial interface ISeatManageService : IExceptionService
    {
        #region 终端设置
        /// <summary>
        /// 获取终端设置
        /// </summary>
        /// <param name="clientNo"></param>
        /// <returns></returns>
        [OperationContract]
        TerminalInfo GetTerminalInfo(string clientNo);
        /// <summary> 
        /// 获取所有的终端
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TerminalInfo> GetAllTerminals();
        /// <summary>
        /// 添加新的触摸屏终端
        /// </summary>
        /// <param name="clientConfig"></param>
        /// <returns></returns>
        [OperationContract]
        int AddClientSetting(TerminalInfo clientConfig);
        /// <summary>
        /// 更新触摸屏终端
        /// </summary>
        /// <param name="clientConfig"></param>
        /// <returns></returns>
        [OperationContract]
        int UpdateClient(TerminalInfo clientConfig);

       
        
        /// <summary>
        /// 删除终端
        /// </summary>
        /// <param name="clintNum"></param>
        /// <returns></returns>
        [OperationContract]
        EnumType.HandleResult DeleteTerminal(string clintNum);
        #endregion

    }
}
