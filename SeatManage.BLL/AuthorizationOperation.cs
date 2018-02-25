using AuthorizeVerify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SeatManage.Bll
{
    public class AuthorizationOperation
    {
        /// <summary>
        /// 获取功能授权信息（Host服务根目录）
        /// </summary>
        /// <returns></returns>
        public static FunctionAuthorizeInfo GetFunctionAuthorize()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetFunctionAuthorize();
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("获取功能授权失败：" + ex.Message);
                throw new Exception("获取功能授权失败：" + ex.Message);
                // return new TerminalInfo();
            }
           
        }
        /// <summary>
        /// 从指定路径获取授权文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FunctionAuthorizeInfo GetFunctionAuthorizeFile(string filePath)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetFunctionAuthorizeFile(filePath);
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("获取功能授权失败：" + ex.Message);
                throw new Exception("获取功能授权失败：" + ex.Message);
                // return new TerminalInfo();
            }
            
        }
        /// <summary>
        /// 保存服务授权文件到本地(Host服务根目录)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SaveFunctionAuthorize(FunctionAuthorizeInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.SaveFunctionAuthorize(model);
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("保存授权文件失败：" + ex.Message);
                throw new Exception("保存授权文件失败：" + ex.Message);
            }
            
        }
        /// <summary>
        /// 授权功能名称
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public static bool IsAuthorize(string functionName)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.IsAuthorize(functionName);
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("验证授权失败：" + ex.Message);
                throw new Exception("验证授权失败：" + ex.Message);
            }
           
        }
    }
}
