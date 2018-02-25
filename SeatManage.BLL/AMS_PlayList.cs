using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class AMS_PlayList
    {

        /// <summary>
        /// 获取播放列表
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.AMS_PlayList> GetPlayListByStatus(EnumType.LogStatus status)
        { 
         
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetPlayListByStatus(status);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取播放列表失败：" + ex.Message);
                throw ex;
            }
            
             
        }
        /// <summary>
        /// 获取播放列表
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.AMS_PlayList> GetPlayListOverTime(EnumType.LogStatus status)
        {

            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetPlayListOverTime(status);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取播放列表失败：" + ex.Message);
                return new List<ClassModel.AMS_PlayList>();
            }
            
        }
        /// <summary>
        /// 添加播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult AddPlaylist(SeatManage.ClassModel.AMS_PlayList model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddPlaylist(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加播放列表失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        /// 更新播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult UpdatePlaylist(SeatManage.ClassModel.AMS_PlayList model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdatePlaylist(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新播放列表失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        ///删除播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult DeletePlaylist(SeatManage.ClassModel.AMS_PlayList model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeletePlaylist(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除播放列表失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        /// 获取播放列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static SeatManage.ClassModel.AMS_PlayList GetPlayListByNum(string num)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetPlayListByNum(num);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取播放列表失败：" + ex.Message);
                return null;
            }
           

        }



        /// <summary>
        /// 获取Md5播放列表
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.AMS_PlayListMd5> GetMd5PlayListByStatus(EnumType.LogStatus status)
        {

            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetMd5PlayListByStatus(status);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取播放列表失败：" + ex.Message);
                throw ex;
            }
          
        }
        /// <summary>
        /// 获取Md5播放列表
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.AMS_PlayListMd5> GetMd5PlayListOverTime(EnumType.LogStatus status)
        {

            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetMd5PlayListOverTime(status);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取播放列表失败：" + ex.Message);
                return new List<ClassModel.AMS_PlayListMd5>();
            }
           
        }
        /// <summary>
        /// 添加Md5播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult AddMd5Playlist(SeatManage.ClassModel.AMS_PlayListMd5 model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddMd5Playlist(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加播放列表失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        /// 更新Md5播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult UpdateMd5Playlist(SeatManage.ClassModel.AMS_PlayListMd5 model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateMd5Playlist(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新播放列表失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        ///删除Md5播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult DeleteMd5Playlist(SeatManage.ClassModel.AMS_PlayListMd5 model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteMd5Playlist(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除播放列表失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
           
        }
        /// <summary>
        /// 获取Md5播放列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static SeatManage.ClassModel.AMS_PlayListMd5 GetMd5PlayListByNum(string num)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetMd5PlayListByNum(num);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取播放列表失败：" + ex.Message);
                return null;
            }
          
        }
    }
}
