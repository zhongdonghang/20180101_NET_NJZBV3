using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using System.ServiceModel;


namespace SeatManage.Bll
{
    public class T_SM_Library
    {
        /// <summary>
        /// 添加图书馆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddNewLibrary(LibraryInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddNewLibrary(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加图书馆失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 更新图书馆信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdataLibraryInfo(LibraryInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateLibrary(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("更新图书馆失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 删除图书馆信息，会连同下属阅览室以及座位信息一同删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DeleteLibrary(LibraryInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteLibrary(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("删除图书馆失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 获取学校信息
        /// </summary>
        /// <returns></returns>
        public static List<LibraryInfo> GetLibraryInfoList(string schoolno, string libraryno,string libraryname)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetLibraryList(schoolno,libraryno,libraryname);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("获取图书馆列表失败：" + ex.Message);
                return new List<LibraryInfo>();
            }
            
        }
    }
}
