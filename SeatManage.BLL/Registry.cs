using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.SeatManageComm;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class Registry
    {
        /// <summary>
        /// 获取注册表值
        /// </summary>
        /// <returns></returns>
        public static SeatManage.ClassModel.RegistryKey GetRegistryKey()
        {
            IWCFService.ISeatManageService service = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return service.GetRegistryKey();
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write(string.Format("获取注册表出错：{0}", ex.Message));
                throw ex;
            }
            
        }
        /// <summary>
        /// 保存注册表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SaveRegistryKey(SeatManage.ClassModel.RegistryKey model)
        {
            IWCFService.ISeatManageService service = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return service.SaveRegistryKey(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write(string.Format("获取保存注册表出错：{0}", ex.Message));
                throw ex;
            }
            
        }
        /// <summary>
        /// 获取学校编号
        /// </summary>
        /// <returns></returns>
        public static string GetSchoolNum()
        {
            IWCFService.ISeatManageService service = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return service.GetSchoolNum();
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write(string.Format("获取学校编号出错：{0}", ex.Message));
                throw ex;
            }
           
        }
        /// <summary>
        /// 验证查询接口授权
        /// </summary>
        /// <returns></returns>
        public static bool ReadingRoomInterfaceIsAuthorize()
        {
            IWCFService.ISeatManageService service = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return service.ReadingRoomInterfaceIsAuthorize();
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write(string.Format("授权验证出错：{0}", ex.Message));
                throw ex;
            }
           
        }
        /// <summary>
        /// 验证门禁接口授权
        /// </summary>
        /// <returns></returns>
        public static bool AccessInterfaceIsAuthorize()
        {
            IWCFService.ISeatManageService service = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return service.AccessInterfaceIsAuthorize();
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write(string.Format("授权验证出错：{0}", ex.Message));
                throw ex;
            }
           
        }
        /// <summary>
        /// 验证下发工具授权
        /// </summary>
        /// <returns></returns>
        public static bool MediaReleaseIsAuthorize()
        {
            IWCFService.ISeatManageService service = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return service.MediaReleaseIsAuthorize();
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write(string.Format("授权验证出错：{0}", ex.Message));
                throw ex;
            }
            
        }
    }
}
