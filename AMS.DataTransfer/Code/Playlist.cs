using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.ServiceProxy;
using System.IO;

namespace AMS.DataTransfer.Code
{
    public class Playlist
    {
        /// <summary>
        /// 获取播放列表，下载列表中的文件，并执行上传。
        /// 往数据库中添加，如果已存在，则执行更新
        /// 
        ///  开始下载文件之前先判断文件在缓存中是否存在，已经存在则跳过，不再下载。
        ///  下载中某个文件下载错误，则返回下载失败的信息。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetPlaylist(int id)
        {
            try
            {
                FileOperate advertfileOperate = new FileOperate();
                SeatManage.Bll.FileOperate seatmanagefileOperate = new SeatManage.Bll.FileOperate();
                seatmanagefileOperate.Downloaded += new SeatManage.Bll.EventHandleFileTransport(seatmanagefileOperate_Downloaded);
                advertfileOperate.DownloadError += new EventHandleFileOperateError(fileOperate_DownloadError);
                Model.AMS_PlayList model = AMS.ServiceProxy.IPlayListService.GetPlayListByID(id);

                foreach (Model.AMS_VideoItem video in model.MediaFiles)
                {
                    string fileFullName = string.Format(@"{0}{1}", ServiceSet.TempFilePath, video.ReRelativeUrl);
                    //开始下载文件
                    if (!File.Exists(fileFullName))
                    {
                        bool downloadResult =false;
                        string error = advertfileOperate.FileDownLoad(fileFullName, video.ReRelativeUrl, SeatManage.EnumType.SeatManageSubsystem.MediaFiles);

                        if (error == "")
                        {
                            downloadResult = true;
                        }
                        else
                        {
                            downloadResult = false;
                        }
                       
                        if (!downloadResult)
                        {
                            //下载失败，直接返回false
                            return false;
                        }
                    }
                }

                foreach (Model.AMS_VideoItem video in model.MediaFiles)
                {
                    string fileFullName = string.Format(@"{0}{1}", ServiceSet.TempFilePath, video.ReRelativeUrl);
                    //文件下载完成，执行上传操作 
                    bool uploadResult = seatmanagefileOperate.UpdateFile(fileFullName, video.ReRelativeUrl, SeatManage.EnumType.SeatManageSubsystem.MediaFiles);
                    if (!uploadResult)
                    {
                        //上传失败，直接返回false，不再尝试其他操作
                        return false;
                    }
                }

                //判断播放列表是否存在 
                SeatManage.ClassModel.AMS_PlayListMd5 seatPlayList = SeatManage.Bll.AMS_PlayList.GetMd5PlayListByNum(model.Number);
                if (seatPlayList != null)
                {
                    //播放列表存在，执行更新操作
                    SeatManage.ClassModel.AMS_PlayListMd5 playlist = SeatManage.ClassModel.AMS_PlayListMd5.Parse(model.ToXml());
                    seatPlayList.PlayListNo = model.Number;
                    playlist.ReleaseDate = model.ReleaseDate;
                    SeatManage.Bll.AMS_PlayList.UpdateMd5Playlist(playlist);
                    //执行完成通知终端更新播放列表
                    List<SeatManage.ClassModel.TerminalInfoV2> terminalList = SeatManage.Bll.TerminalOperatorService.GetAllTeminalInfo();
                    foreach (SeatManage.ClassModel.TerminalInfoV2 terminal in terminalList)
                    {
                        terminal.IsUpdatePlayList = true;
                        SeatManage.Bll.TerminalOperatorService.UpdateTeminalSetting(terminal);
                    }
                }
                else
                {
                    SeatManage.ClassModel.AMS_PlayListMd5 playlist = SeatManage.ClassModel.AMS_PlayListMd5.Parse(model.ToXml());
                    playlist.ReleaseDate = model.ReleaseDate;
                    SeatManage.Bll.AMS_PlayList.AddMd5Playlist(playlist);
                }
                foreach (Model.AMS_VideoItem video in model.MediaFiles)
                {
                    string fileFullName = string.Format(@"{0}{1}", ServiceSet.TempFilePath, video.ReRelativeUrl);
                    if (File.Exists(fileFullName))
                    {
                        File.Delete(fileFullName);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        static void seatmanagefileOperate_Downloaded(int message)
        {
            SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传播放列表中的文件遇到错误：{0}", message));
        }

        static void fileOperate_DownloadError(string message)
        {
            SeatManage.SeatManageComm.WriteLog.Write(string.Format("下载播放列表中的文件遇到错误：{0}", message));
        }
    }
}
